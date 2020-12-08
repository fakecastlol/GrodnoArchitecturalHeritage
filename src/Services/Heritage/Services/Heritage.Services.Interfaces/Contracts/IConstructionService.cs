using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heritage.Services.Interfaces.Models.Construction;

namespace Heritage.Services.Interfaces.Contracts
{
    public interface IConstructionService
    {
        //Task<List<UserResponseCoreModel>> GetAllAsync();

        //Task<PaginatedList<UserResponseCoreModel>> GetUsingPaginationAsync(int pageNumber, int pageSize);

        Task<ConstructionResponseCoreModel> GetConstructionByIdAsync(Guid id);

        //Task<UserResponseCoreModel> SetUserRole(SetRoleRequestModel userResponseCoreModel);

        //Task<UserResponseCoreModel> UpdateProfileAsync(ProfileRequestModel profileRequestModel);

        //Task<UserResponseCoreModel> UpdateImageAsync(ImageViewModel imageRequestModel);

        //Task<RegisterResponseModel> AuthenticateAsync(LoginRequestModel loginCoreModel);

        //Task<RegisterResponseModel> RegisterAsync(RegisterCoreModel registerCoreModel);

        //Task<UserResponseCoreModel> UpdateAsync(UserResponseCoreModel item);

        //Task<UserResponseCoreModel> DeleteImageAsync(ImageViewModel imageRequestModel);

        //Task<byte[]> GetImageByIdAsync(Guid id);

        //Task DeleteAsync(Guid id);
    }
}