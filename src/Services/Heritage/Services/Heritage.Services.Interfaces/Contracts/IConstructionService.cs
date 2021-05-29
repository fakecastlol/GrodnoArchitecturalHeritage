using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Image;
using Heritage.Services.Interfaces.Models.Sorting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heritage.Services.Interfaces.Contracts
{
    public interface IConstructionService
    {
        Task<IndexViewModel<ConstructionResponseCoreModel>> GetUsingPaginationAsync(Guid? construction, string name, int pageNumber, int pageSize, SortState sortOrder);

        Task<ConstructionResponseCoreModel> GetConstructionByIdAsync(Guid id);

        Task<ConstructionResponseCoreModel> UpdateConstructionAsync(ConstructionRequestCoreModel requestCoreModel);

        Task<ConstructionResponseCoreModel> UpdateImageAsync(ImageRequestModel imageRequestModel);

        Task<List<ConstructionResponseCoreModel>> GetAllAsync();

        Task<ConstructionResponseCoreModel> CreateConstructionAsync(ConstructionRequestCoreModel requestModel);

        Task DeleteConstructionAsync(Guid id);
    }
}