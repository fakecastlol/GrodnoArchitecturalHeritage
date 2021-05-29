using AutoMapper;
using Heritage.Domain.Core.Entities;
using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Image;
using Heritage.Services.Interfaces.Models.Sorting;

namespace Heritage.Infrastructure.Business.Support.Adapter
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap(typeof(IndexViewModel<>), typeof(IndexViewModel<>));

            CreateMap<SortViewModel, IndexViewModel<ConstructionResponseCoreModel>>();
            CreateMap<SortViewModel, IndexViewModel<ConstructionResponseCoreModel>>().ReverseMap();

            CreateMap<ConstructionResponseCoreModel, IndexViewModel<ConstructionResponseCoreModel>>();
            CreateMap<ConstructionResponseCoreModel, IndexViewModel<ConstructionResponseCoreModel>>().ReverseMap();

            CreateMap<Construction, ConstructionResponseCoreModel>()
                .ForMember(destinationMember => destinationMember.Longitude, x => x.MapFrom(x => x.Location.X))
                .ForMember(destinationMember => destinationMember.Latitude, y => y.MapFrom(y => y.Location.Y));
            CreateMap<ConstructionResponseCoreModel, Construction>();

            CreateMap<Construction, ConstructionRequestCoreModel>();
            CreateMap<ConstructionRequestCoreModel, Construction>()
                .ForMember(destinationMember => destinationMember.Location, source => source.Ignore());

            CreateMap<ConstructionResponseCoreModel, ConstructionRequestCoreModel>();
            CreateMap<ConstructionRequestCoreModel, ConstructionResponseCoreModel>();

            CreateMap<Image, ImageCoreModel>();
            CreateMap<Image, ImageCoreModel>().ReverseMap();
        }
    }
}
