using AutoMapper;
using Identity.Domain.Core.Entities;
using Identity.Services.Interfaces.Models.Pagination;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Register;

namespace Identity.Infrastructure.Business.Support.Adapter
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));

            CreateMap<UserEntity, UserResponseCoreModel>();
            CreateMap<UserResponseCoreModel, UserEntity>();

            CreateMap<UserEntity, RegisterCoreModel>();
            CreateMap<RegisterCoreModel, UserEntity>();

            CreateMap<UserEntity, LoginResponseModel>();
            CreateMap<LoginResponseModel, UserEntity>();

            CreateMap<RegisterCoreModel, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, RegisterCoreModel>();

            CreateMap<UserEntity, ImageViewModel>();
            CreateMap<ImageViewModel, UserEntity>();
        }
    }
}
