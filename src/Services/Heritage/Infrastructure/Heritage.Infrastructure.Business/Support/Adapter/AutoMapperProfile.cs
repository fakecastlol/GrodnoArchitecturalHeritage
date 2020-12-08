using AutoMapper;
using Heritage.Domain.Core.Entities;
using Heritage.Services.Interfaces.Models.Construction;

namespace Heritage.Infrastructure.Business.Support.Adapter
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Construction, ConstructionResponseCoreModel>();
            CreateMap<ConstructionResponseCoreModel, Construction>();
        }
    }
}
