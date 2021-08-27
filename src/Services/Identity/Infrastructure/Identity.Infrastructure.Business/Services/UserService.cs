using AutoMapper;
using Identity.Domain.Core.Entities;
using Identity.Domain.Core.Entities.Enums;
using Identity.Domain.Interfaces.Repositories;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers.Options;
using Identity.Services.Interfaces.Helpers.Rabbit;
using Identity.Services.Interfaces.Models.Filtration;
//using Identity.Services.Interfaces.Models.IntegrationEvent.Bus;
using Identity.Services.Interfaces.Models.Pagination;
using Identity.Services.Interfaces.Models.Sorting;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.ConfirmRegister;
using Identity.Services.Interfaces.Models.User.ForgotPassword;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Profile;
using Identity.Services.Interfaces.Models.User.Register;
using Identity.Services.Interfaces.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Identity.Infrastructure.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly IPasswordService _passwordService;
        private readonly IHeaderService _headerService;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly ILogger _logger;
        private readonly FileSettings _fileSettings;
        //private readonly ILogger/*<UserService> */_logger;
        //private readonly IEventBus _eventBus;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService, IFileService fileService, IEmailService emailService, IPasswordService passwordService, IHeaderService headerService, IOptions<FileSettings> fileSettings, ILogger<UserService> logger, IRabbitMQService rabbitMQService/*, IEventBus eventBus*/)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _fileService = fileService;
            _emailService = emailService;
            _passwordService = passwordService;
            _headerService = headerService;
            _fileSettings = fileSettings.Value;
            _logger = logger;
            _rabbitMQService = rabbitMQService;
            //_eventBus = eventBus;
        }

        public async Task<IndexViewModel<UserResponseCoreModel>> GetUsingPaginationAsync(Guid? user, string email, int pageNumber, int pageSize, SortState sortOrder = SortState.EmailAsc)
        {
            var userList = await _userRepository.GetAllAsync();

            #region filtration
            if (user != null)
            {
                userList = userList.Where(p => p.Id == user);
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                userList = userList.Where(p => p.Email.Contains(email));
            }
            #endregion

            #region sorting
            userList = sortOrder switch
            {
                SortState.EmailDesc => userList.OrderByDescending(s => s.Email),
                SortState.LoginAsc => userList.OrderBy(s => s.Login),
                SortState.LoginDesc => userList.OrderByDescending(s => s.Login),
                _ => userList.OrderBy(s => s.Email),
            };
            #endregion

            #region pagination
            var count = await userList.CountAsync();
            var items = await userList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            #endregion

            var viewItems = _mapper.Map<List<User>, List<UserResponseCoreModel>>(items);

            var viewModel = new IndexViewModel<UserResponseCoreModel>
            {
                PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(viewItems, user, email),
                Users = viewItems
            };

            return viewModel;
        }

        public async Task<UserResponseCoreModel> GetUserByIdAsync(Guid id)
        {
            var getEntity = await _userRepository.GetByIdAsync(id);

            var result = _mapper.Map<UserResponseCoreModel>(getEntity);

            return result;
        }

        public async Task<UserResponseCoreModel> SetUserRole(SetRoleRequestModel requestModel)
        {
            var user = await _userRepository.GetByIdAsync(requestModel.Id);

            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/setrole] - Id: [{requestModel.Id}] - OldRole: [{user.Role}] - NewRole: [{requestModel.Role}]";

            user.Role = requestModel.Role;

            var setRole = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(setRole);

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<RegisterResponseModel> AuthenticateAsync(LoginRequestModel loginCoreModel)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/login]  - Email: [{loginCoreModel.Email}]";

            var userEntity = await (await _userRepository.GetAllAsync(x =>
                    x.Email.Equals(loginCoreModel.Email)))
                .FirstOrDefaultAsync();

            if (userEntity == null || !BC.Verify(loginCoreModel.Password, userEntity.Password))
            {
                throw new ValidationException("Incorrect account name or password", "");
            }

            userEntity.LastVisited = DateTime.UtcNow;

            var userCoreModel = _mapper.Map<UserResponseCoreModel>(userEntity);
            var token = _tokenService.GenerateJwtToken(userCoreModel);
            var loginResponseModel = new RegisterResponseModel()
            {
                Token = token,
                User = userCoreModel
            };
            await _userRepository.UpdateAsync(userEntity);

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return loginResponseModel;
        }

        public async Task<RegisterResponseModel> RegisterAsync(RegisterRequestModel requestModel)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/register]  - Email: [{requestModel.Email}]";

            var existingUser = await (await _userRepository.GetAllAsync(u => u.Email.Equals(requestModel.Email, StringComparison.OrdinalIgnoreCase)))
                .FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new ValidationException($"User with email {requestModel.Email} already exists", "");
            }

            var user = _mapper.Map<User>(requestModel);

            var createdUserEntity = await _userRepository.CreateAsync(user);

            var userCoreModel = _mapper.Map<UserResponseCoreModel>(createdUserEntity);

            var token = _tokenService.GenerateJwtToken(userCoreModel);

            var registerResponseModel = new RegisterResponseModel()
            {
                Token = token,
                User = userCoreModel
            };

            await _emailService.SendConfirmEmailAsync("span4boblol@gmail.com", "Grodno Architectural Heritage", registerResponseModel.Token);

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return registerResponseModel;
        }

        public async Task ConfirmEmailAsync(ConfirmRequestModel model)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/confirmemail]  - Email: [{model.Email}]";

            await _emailService.SendConfirmEmailAsync("span4boblol@gmail.com", "Grodno Architectural Heritage", model.Token);

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ConfirmRegisterAsync()
        {
            //var userId = _headerService.GetUserId();
            var userId = _headerService.GetUserId();

            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/confirmregister]  - Id: [{userId}]";

            _logger.LogWarning($"userId = {userId}");

            try
            {
                var user = await (await _userRepository.GetAllAsync(u => u.Id.Equals(new Guid(userId)))).FirstOrDefaultAsync();

                user.Role = Roles.User;

                _logger.LogWarning($"userRole = {user.Role}");

                var updatedEntity = await _userRepository.UpdateAsync(user);

                _logger.LogWarning($"userRole = {user.Role}");

                var userCoreModel = _mapper.Map<UserResponseCoreModel>(updatedEntity);

                _logger.LogWarning($"userRole = {user.Role}");

                var token = _tokenService.GenerateJwtToken(userCoreModel);

                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestModel model)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/forgotpassword]  - Email: [{model.Email}]";

            var user = await (await _userRepository.GetAllAsync(x =>
                x.Email.Equals(model.Email)))
                .FirstOrDefaultAsync();
            if (user != null)
            {                
                var password = _passwordService.GeneratePassword(10);
                var encryptPassword = BC.HashPassword(password);
                user.Password = encryptPassword;
                var updatedUser = _userRepository.UpdateAsync(user);

                await _emailService.SendForgotPasswordAsync("span4boblol@gmail.com", "Grodno Architectural Heritage", password);
            }

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<UserResponseCoreModel> UpdateProfileAsync(ProfileRequestModel profileRequestModel)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/updateprofile]  - Email: [{profileRequestModel.Email}]";

            var user = await _userRepository.GetByIdAsync(profileRequestModel.Id);

            //_mapper.Map<User>(profileRequestModel);
            user.Email = profileRequestModel.Email;
            user.FirstName = profileRequestModel.FirstName;
            user.LastName = profileRequestModel.LastName;
            user.Login = profileRequestModel.Login;
            user.Location = profileRequestModel.Location;

            var update = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(update);

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<UserResponseCoreModel> UpdateImageAsync(ImageViewModel imageRequestModel)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/updateimage]  - Id: [{imageRequestModel.Id}]";

            var user = await _userRepository.GetByIdAsync(imageRequestModel.Id);

            user.Avatar = await _fileService.SaveImageFileAsync(imageRequestModel);

            var update = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(update);

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<UserResponseCoreModel> DeleteImageAsync(ImageViewModel imageRequestModel)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/deleteimage]  - Id: [{imageRequestModel.Id}]";

            var user = await _userRepository.GetByIdAsync(imageRequestModel.Id);
            if (user == null)
                throw new ValidationException("user is not found", "");

            _fileService.DeleteImageFile(user.Avatar);

            user.Avatar = null;

            var userUpdate = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(userUpdate);

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/deleteuser]  - Id: [{id}]";

            await _userRepository.DeleteAsync(id);

            try
            {
                _rabbitMQService.SendMessageToQueue(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}