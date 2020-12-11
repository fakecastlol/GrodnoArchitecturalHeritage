using System;
using System.Threading.Tasks;
using AutoMapper;
using Heritage.Domain.Core.Entities;
using Heritage.Domain.Interfaces.Repositories;
using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Pagination;
using Heritage.Services.Interfaces.Validation;
using Microsoft.EntityFrameworkCore;

namespace Heritage.Infrastructure.Business.Services
{
    public class ConstructionService : IConstructionService
    {
        private readonly IConstructionRepository _constructionRepository;
        private readonly IMapper _mapper;

        public ConstructionService(IConstructionRepository constructionRepository, IMapper mapper)
        {
            _constructionRepository = constructionRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ConstructionResponseCoreModel>> GetUsingPaginationAsync(int pageNumber, int pageSize)
        {
            var fullConstructionList = await _constructionRepository.GetAllAsync();

            var paginatedConstructionList = PaginatedList<Construction>.CreateAsync(fullConstructionList, pageNumber, pageSize);

            var constructionResponseCoreModelList = _mapper.Map<PaginatedList<Construction>, PaginatedList<ConstructionResponseCoreModel>>(paginatedConstructionList);

            return constructionResponseCoreModelList;
        }

        public async Task<ConstructionResponseCoreModel> GetConstructionByIdAsync(Guid id)
        {
            var getEntity = await _constructionRepository.GetByIdAsync(id);

            var result = _mapper.Map<ConstructionResponseCoreModel>(getEntity);

            return result;
        }

        public async Task<ConstructionResponseCoreModel> CreateConstructionAsync(ConstructionRequestCoreModel requestModel)
        {
            var existingConstruction = await (await _constructionRepository.GetAllAsync(u => u.Name.Equals(requestModel.Name)))
                .FirstOrDefaultAsync();
            if (existingConstruction != null)
            {
                throw new ValidationException($"User with email {requestModel.Name} already exists", "");
            }

            var construction = new Construction()
            {
                Name = requestModel.Name ?? "",
                Address = requestModel.Address ?? "",
                BuildDate = requestModel.BuildDate,
                ArchitecturalStyle = requestModel.ArchitecturalStyle,
                Article = requestModel.Article ?? "",
                LossCause = requestModel.LossCause,
                Material = requestModel.Material,
                Type = requestModel.Type,
                LossDate = requestModel.LossDate,
                Location = requestModel.Location
            };

            var createdConstructionEntity = await _constructionRepository.CreateAsync(construction);

            var responseModel = _mapper.Map<ConstructionResponseCoreModel>(createdConstructionEntity);

            return responseModel;
        }

        public async Task DeleteConstructionAsync(Guid id)
        {
            await _constructionRepository.DeleteAsync(id);
        }
    }
}