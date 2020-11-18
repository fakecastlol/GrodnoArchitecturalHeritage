using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IUserService
    {
        Task<IQueryable<UserResponseCoreModel>> GetAllAsync();

        Task<UserResponseCoreModel> GetUserByIdAsync(Guid id);

        Task<LoginResponseModel> AuthenticateAsync(LoginRequestModel loginCoreModel);

        Task<RegisterResponseModel> RegisterAsync(RegisterCoreModel registerCoreModel);

        Task<UserResponseCoreModel> UpdateAsync(UserResponseCoreModel item);

        Task DeleteAsync(int id);
    }
}
