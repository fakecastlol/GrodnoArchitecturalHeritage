using System;
using System.Threading.Tasks;
using AutoMapper;
using Heritage.Domain.Interfaces.Repositories;
using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Models.Construction;

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

        public async Task<ConstructionResponseCoreModel> GetConstructionByIdAsync(Guid id)
        {
            var getEntity = await _constructionRepository.GetByIdAsync(id);

            var result = _mapper.Map<ConstructionResponseCoreModel>(getEntity);

            return result;
        }


    }
}