using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseCoreModel>> GetAllAsync();

        Task<UserResponseCoreModel> GetUserByIdAsync(Guid id);

        Task<LoginResponseCoreModel> AuthenticateAsync(LoginCoreModel loginCoreModel);

        Task<RegisterResponseModel> RegisterAsync(RegisterCoreModel registerCoreModel);

        Task<UserResponseCoreModel> UpdateAsync(UserResponseCoreModel item);

        Task DeleteAsync(int id);
    }
}
