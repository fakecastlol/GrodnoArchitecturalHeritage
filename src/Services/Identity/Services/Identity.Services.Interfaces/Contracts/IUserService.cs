using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Services.Interfaces.Contracts.Generic;
using Identity.Services.Interfaces.Models;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IUserService : IGenericService<UserCoreModel>
    {
        Task<IEnumerable<RequestUserCoreModel>> GetAllAsync();

        Task<bool> IsUserExistAsync(LoginCoreModel model);

        Task<UserCoreModel> Authenticate(LoginCoreModel loginCoreModel);

        Task<UserCoreModel> CreateAsync(RegisterCoreModel registerCoreModel);
    }
}
