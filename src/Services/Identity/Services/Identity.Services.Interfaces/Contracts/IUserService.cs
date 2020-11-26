using Identity.Services.Interfaces.Models.Pagination;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IUserService
    {
        Task<List<UserResponseCoreModel>> GetAllAsync();

        Task<PaginatedList<UserResponseCoreModel>> GetUsingPaginationAsync(int pageNumber, int pageSize);

        Task<UserResponseCoreModel> GetUserByIdAsync(Guid id);

        Task<UserResponseCoreModel> SetUserRole(UserResponseCoreModel userResponseCoreModel);

        Task<RegisterResponseModel> AuthenticateAsync(LoginRequestModel loginCoreModel);

        Task<RegisterResponseModel> RegisterAsync(RegisterCoreModel registerCoreModel);

        Task<UserResponseCoreModel> UpdateAsync(UserResponseCoreModel item);

        Task DeleteAsync(Guid id);
    }
}
