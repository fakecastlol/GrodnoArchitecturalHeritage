using AutoMapper;
using Identity.Domain.Core.Entities;
using Identity.Domain.Core.Entities.Enums;
using Identity.Domain.Interfaces.Repositories;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers.Options;
using Identity.Services.Interfaces.Models.Pagination;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;
using Identity.Services.Interfaces.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;
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
        private readonly FileSettings _fileSettings;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService, IFileService fileService, IOptions<FileSettings> fileSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _fileService = fileService;
            _fileSettings = fileSettings.Value;
        }

        public async Task<PaginatedList<UserResponseCoreModel>> GetUsingPaginationAsync(int pageNumber, int pageSize)
        {
            var fullUserList = await _userRepository.GetAllAsync();

            var paginatedUserList = PaginatedList<UserEntity>.CreateAsync(fullUserList, pageNumber, pageSize);

            var userResponseCoreModelList = _mapper.Map<PaginatedList<UserEntity>, PaginatedList<UserResponseCoreModel>>(paginatedUserList);

            return userResponseCoreModelList;
        }

        public async Task<UserResponseCoreModel> GetUserByIdAsync(Guid id)
        {
            var getEntity = await _userRepository.GetByIdAsync(id);

            var result = _mapper.Map<UserResponseCoreModel>(getEntity);

            return result;
        }

        public async Task<string> GetImageByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var imagePath = user.Avatar ?? _fileSettings.DefaultImage;

            var bytes = await File.ReadAllBytesAsync(imagePath);

            var base64 = Convert.ToBase64String(bytes);

            return base64;
        }

        public async Task<UserResponseCoreModel> SetUserRole(SetRoleRequestModel requestModel)
        {
            var user = await _userRepository.GetByIdAsync(requestModel.Id);
            user.Role = requestModel.Role;

            var setRole = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(setRole);

            return result;
        }

        public async Task<RegisterResponseModel> AuthenticateAsync(LoginRequestModel loginCoreModel)
        {
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

            return loginResponseModel;
        }

        public async Task<RegisterResponseModel> RegisterAsync(RegisterCoreModel registerCoreModel)
        {
            var existingUser = await (await _userRepository.GetAllAsync(u => u.Email.Equals(registerCoreModel.Email)))
                .FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new ValidationException($"User with email {registerCoreModel.Email} already exists", "");
            }

            var user = new UserEntity()
            {
                Email = registerCoreModel.Email,
                Password = BC.HashPassword(registerCoreModel.Password),
                Role = Roles.User,
                RegistrationDate = DateTime.UtcNow,
                Avatar = _fileSettings.DefaultImage
            };

            var createdUserEntity = await _userRepository.CreateAsync(user);

            var userCoreModel = _mapper.Map<UserResponseCoreModel>(createdUserEntity);
            var token = _tokenService.GenerateJwtToken(userCoreModel);

            var registerResponseModel = new RegisterResponseModel()
            {
                Token = token,
                User = userCoreModel
            };

            return registerResponseModel;
        }

        public async Task<UserResponseCoreModel> UpdateProfileAsync(ProfileRequestModel profileRequestModel)
        {
            var user = await _userRepository.GetByIdAsync(profileRequestModel.Id);

            user.Email = profileRequestModel.Email;
            user.FirstName = profileRequestModel.FirstName;
            user.LastName = profileRequestModel.LastName;
            user.Login = profileRequestModel.Login;
            user.Location = profileRequestModel.Location;

            var update = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(update);

            return result;
        }

        public async Task<UserResponseCoreModel> UpdateImageAsync(ImageViewModel imageRequestModel)
        {
            var user = await _userRepository.GetByIdAsync(imageRequestModel.Id);

            user.Avatar = await _fileService.SaveImageFileAsync(imageRequestModel);

            var update = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(update);

            return result;
        }

        public async Task<UserResponseCoreModel> DeleteImageAsync(ImageViewModel imageRequestModel)
        {
            var user = await _userRepository.GetByIdAsync(imageRequestModel.Id);
            if (user == null)
                throw new ValidationException("user is not found", "");

            _fileService.DeleteImageFile(user.Avatar);

            user.Avatar = null;

            var userUpdate = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(userUpdate);

            return result;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}