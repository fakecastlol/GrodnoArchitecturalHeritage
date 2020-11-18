using Identity.Services.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseCoreModel>> GetAllAsync();

        Task<UserResponseCoreModel> GetIdAsync(int id);

        Task<LoginResponseCoreModel> AuthenticateAsync(LoginCoreModel loginCoreModel);

        Task<RegisterResponseModel> RegisterAsync(RegisterCoreModel registerCoreModel);

        Task<UserResponseCoreModel> UpdateAsync(UserResponseCoreModel item);

        Task DeleteAsync(int id);
    }
}
