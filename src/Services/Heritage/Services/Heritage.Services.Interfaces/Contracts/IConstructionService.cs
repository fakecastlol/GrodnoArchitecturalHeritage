using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Pagination;

namespace Heritage.Services.Interfaces.Contracts
{
    public interface IConstructionService
    {
        Task<PaginatedList<ConstructionResponseCoreModel>> GetUsingPaginationAsync(int pageNumber, int pageSize);

        Task<ConstructionResponseCoreModel> GetConstructionByIdAsync(Guid id);

        //Task<UserResponseCoreModel> SetUserRole(SetRoleRequestModel userResponseCoreModel);

        //Task<UserResponseCoreModel> UpdateProfileAsync(ProfileRequestModel profileRequestModel);

        //Task<UserResponseCoreModel> UpdateImageAsync(ImageViewModel imageRequestModel);

        Task<ConstructionResponseCoreModel> CreateConstructionAsync(ConstructionRequestCoreModel requestModel);

        //Task<UserResponseCoreModel> UpdateAsync(UserResponseCoreModel item);

        //Task<UserResponseCoreModel> DeleteImageAsync(ImageViewModel imageRequestModel);

        //Task<byte[]> GetImageByIdAsync(Guid id);

        Task DeleteConstructionAsync(Guid id);
    }
}