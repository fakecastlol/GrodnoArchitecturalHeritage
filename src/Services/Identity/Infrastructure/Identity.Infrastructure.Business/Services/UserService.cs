using AutoMapper;
using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers;
using Identity.Services.Interfaces.Models;
using Identity.Services.Interfaces.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Identity.Infrastructure.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<IEnumerable<UserCoreModel>> GetAllAsync()
        {
            var result = _mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserCoreModel>>(await _userRepository.GetAllAsync());

            return result;
        }

        public async Task<UserCoreModel> GetIdAsync(int id)
        {
            var getEntity = await _userRepository.GetIdAsync(id);

            var result = _mapper.Map<UserCoreModel>(getEntity);

            return result;
        }

        public Task<UserCoreModel> CreateAsync(UserCoreModel item)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsUserExistAsync(LoginCoreModel model)
        {
            var user = await (await _userRepository.GetAllAsync(
                x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password))).AnyAsync();
            return user;
        }

        private string GenerateJwtToken(UserCoreModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                    //new Claim("role", user.Role.Name),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public async Task<UserCoreModel> Authenticate(LoginCoreModel loginCoreModel)
        //{
        //    var user = await (await _userRepository.GetAllAsync(x =>
        //        x.Email.Equals(loginCoreModel.Email) && (x.Password.Equals(loginCoreModel.Password)),
        //            includes => includes.Include(c => c.Role)))
        //        .FirstOrDefaultAsync();
        //    if (user == null) return null;

        //    var userCoreModel = _mapper.Map<UserCoreModel>(user);
        //    var token = GenerateJwtToken(userCoreModel);

        //    userCoreModel.Token = token;

        //    return userCoreModel;
        //}

        public async Task<UserCoreModel> Authenticate(LoginCoreModel loginCoreModel)
        {
            // get account from database
            var account = await (await _userRepository.GetAllAsync(x =>
                    x.Email.Equals(loginCoreModel.Email)))
                .SingleOrDefaultAsync();
            // check account found and verify password
            if (account == null || !BC.Verify(loginCoreModel.Password, account.Password))
            {
                // authentication failed
                return null;
            }
            else
            {
                // authentication successful
                var userCoreModel = _mapper.Map<UserCoreModel>(account);

                var token = GenerateJwtToken(userCoreModel);

                userCoreModel.Token = token;

                return userCoreModel;
            }
        }

        public async Task<UserCoreModel> CreateAsync(RegisterCoreModel registerCoreModel)
        {
            var user = _mapper.Map<UserEntity>(registerCoreModel);
            var existingUser = await (await _userRepository.GetAllAsync(u => u.Email.Equals(registerCoreModel.Email)))
                .FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new ValidationException($"User with email {user.Email} already exists", "");
            }

            //var role = await (await _roleRepository.GetAllAsync(r => r.Name.Equals("user"))).FirstAsync();
            //user.RoleId = role.Id;

            // hash password
            user.Password = BC.HashPassword(registerCoreModel.Password);

            var create = await _userRepository.CreateAsync(user);

            var result = _mapper.Map<UserCoreModel>(create);

            return result;
        }

        public async Task<UserCoreModel> UpdateAsync(UserCoreModel userCoreModel)
        {
            var user = _mapper.Map<UserEntity>(userCoreModel);

            var update = await _userRepository.UpdateAsync(user);

            var result = _mapper.Map<UserCoreModel>(update);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}