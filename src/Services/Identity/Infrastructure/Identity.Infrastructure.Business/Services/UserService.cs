using AutoMapper;
using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers;
using Identity.Services.Interfaces.Models.Jwt;
using Identity.Services.Interfaces.Models.Pagination;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;
using Identity.Services.Interfaces.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Identity.Infrastructure.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _appSettings;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IOptions<JwtSettings> appSettings, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<List<UserResponseCoreModel>> GetAllAsync()
        {
            var result = _mapper.Map<IQueryable<UserEntity>, List<UserResponseCoreModel>>(await _userRepository.GetAllAsync());

            return result;
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

        public async Task<UserResponseCoreModel> SetUserRole(SetRoleRequestModel requestModel)
        {
            var user = await _userRepository.GetByIdAsync(requestModel.Id);
            user.Role = requestModel.Role;
            
            var setRole = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(setRole);

            return result;
        }

        private string GenerateJwtToken(UserResponseCoreModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(Claims.Id.ToString().ToLower(), user.Id.ToString()),
                    new Claim(Claims.Email.ToString().ToLower(), user.Email),
                    new Claim(Claims.Scope.ToString().ToLower(), user.Role.ToString()),
                    new Claim(Claims.FirstName.ToString().ToLower(), /*user.FirstName*/"undefined"),
                    new Claim(Claims.LastName.ToString().ToLower(), /*user.LastName*/"undefined"),
                    new Claim(Claims.Login.ToString().ToLower(), /*user.Login*/"undefined"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "http://jwtauthzsrv.azurewebsites.net",
                Issuer = "http://jwtauthzsrv.azurewebsites.net"

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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

            userEntity.LastVisited = DateTime.Now;

            var userCoreModel = _mapper.Map<UserResponseCoreModel>(userEntity);
            var token = GenerateJwtToken(userCoreModel);
            var loginResponseModel = new RegisterResponseModel()
            {
                Token = token,
                User = userCoreModel
            };

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
                RegistrationDate = DateTime.Now
            };

            var createdUserEntity = await _userRepository.CreateAsync(user);

            var userCoreModel = _mapper.Map<UserResponseCoreModel>(createdUserEntity);
            var token = GenerateJwtToken(userCoreModel);

            var registerResponseModel = new RegisterResponseModel()
            {
                Token = token,
                User = userCoreModel
            };

            return registerResponseModel;
        }

        public Task<UserResponseCoreModel> UpdateAsync(SetRoleRequestModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseCoreModel> UpdateAsync(UserResponseCoreModel userCoreModel)
        {
            var user = _mapper.Map<UserEntity>(userCoreModel);

            var update = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(update);

            return result;
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

        public async Task<UserResponseCoreModel> UpdateImageAsync(ImageRequestModel imageRequestModel)
        {
            var user = await _userRepository.GetByIdAsync(imageRequestModel.Id);

            user.Avatar = imageRequestModel.Avatar;

            var update = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserResponseCoreModel>(update);

            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}