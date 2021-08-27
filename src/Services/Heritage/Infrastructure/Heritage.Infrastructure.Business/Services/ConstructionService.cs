using AutoMapper;
using Heritage.Domain.Core.Entities;
using Heritage.Domain.Interfaces.Repositories;
using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Filtration.FilterSortPagingApp.Models;
using Heritage.Services.Interfaces.Models.Image;
using Heritage.Services.Interfaces.Models.Pagination;
using Heritage.Services.Interfaces.Models.Sorting;
using Heritage.Services.Interfaces.Validation;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IndexViewModel<ConstructionResponseCoreModel>> GetUsingPaginationAsync(Guid? construction, string name, int pageNumber, int pageSize, SortState sortOrder = SortState.NameAsc)
        {
            var constructionList = await _constructionRepository.GetAllConstructionsAsync();

            #region filtration
            if (construction != null)
            {
                constructionList = constructionList.Where(p => p.Id == construction);
            }
            if (!string.IsNullOrEmpty(name))
            {
                constructionList = constructionList.Where(p => p.Name.Contains(name));
            }
            #endregion

            #region sorting
            constructionList = sortOrder switch
            {
                SortState.NameDesc => constructionList.OrderByDescending(s => s.Name),
                SortState.AddressAsc => constructionList.OrderBy(s => s.Address),
                SortState.AddressDesc => constructionList.OrderByDescending(s => s.Address),
                _ => constructionList.OrderBy(s => s.Name),
            };
            #endregion

            #region pagination
            var count = await constructionList.CountAsync();
            var items = await constructionList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            #endregion

            var viewItems = _mapper.Map<List<Construction>, List<ConstructionResponseCoreModel>>(items);

            var viewModel = new IndexViewModel<ConstructionResponseCoreModel>
            {
                PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(viewItems, construction, name),
                Constructions = viewItems
            };

            return viewModel;
        }

        public async Task<List<ConstructionResponseCoreModel>> GetAllAsync()
        {
            var constructions = await _constructionRepository.GetAllConstructionsAsync();

            var result = _mapper.Map<IQueryable<Construction>, List<ConstructionResponseCoreModel>>(constructions);

            return result;
        }

        public async Task<ConstructionResponseCoreModel> GetConstructionByIdAsync(Guid id)
        {
            var getEntity = await _constructionRepository.GetConstructionByIdAsync(id);

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

            var loc = requestModel.Location?.Replace(" ", "").Split(',').Select(double.Parse).ToList();

            var construction = _mapper.Map<Construction>(requestModel);

            construction.Location = requestModel.Location is null ? null : new Point(loc[0], loc[1])
            {
                SRID = 4326
            };

            var createdConstructionEntity = await _constructionRepository.CreateAsync(construction);

            var responseModel = _mapper.Map<ConstructionResponseCoreModel>(createdConstructionEntity);

            return responseModel;
        }

        public async Task<ConstructionResponseCoreModel> UpdateConstructionAsync(ConstructionRequestCoreModel requestModel)
        {
            var construction = await _constructionRepository.GetByIdAsync(requestModel.Id);
            var loc = requestModel.Location?.Replace(" ", "").Split(',').Select(double.Parse).ToList();

            construction.Location = requestModel.Location is null ? null : new Point(loc[0], loc[1])
            {
                SRID = 4326
            };

            _mapper.Map(requestModel, construction);

            var update = await _constructionRepository.UpdateAsync(construction);

            var result = _mapper.Map<ConstructionResponseCoreModel>(update);

            return result;
        }

        public async Task DeleteConstructionAsync(Guid id)
        {
            await _constructionRepository.DeleteAsync(id);
        }

        public Task<ConstructionResponseCoreModel> UpdateImageAsync(ImageRequestModel imageRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}