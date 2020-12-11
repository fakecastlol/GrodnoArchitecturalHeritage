using AutoMapper;
using Heritage.Domain.Core.Entities;
using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Pagination;

namespace Heritage.Infrastructure.Business.Support.Adapter
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));

            CreateMap<Construction, ConstructionResponseCoreModel>();
            CreateMap<ConstructionResponseCoreModel, Construction>();

            CreateMap<Construction, ConstructionRequestCoreModel>();
            CreateMap<ConstructionRequestCoreModel, Construction>();

            CreateMap<ConstructionResponseCoreModel, ConstructionRequestCoreModel>();
            CreateMap<ConstructionRequestCoreModel, ConstructionResponseCoreModel>();
        }
    }
}
