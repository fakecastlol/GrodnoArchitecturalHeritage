using AutoMapper;
using Identity.Domain.Core.Entities;
using Identity.Services.Interfaces.Models;

namespace Identity.Infrastructure.Business.Support.Adapter
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserEntity, UserCoreModel>();
            CreateMap<UserCoreModel, UserEntity>();

            CreateMap<UserEntity, RequestUserCoreModel>();
            CreateMap<RegisterCoreModel, UserEntity>();

            CreateMap<UserEntity, RegisterCoreModel>();
            CreateMap<RegisterCoreModel, UserEntity>();

            CreateMap<UserEntity, LoginCoreModel>();
            CreateMap<UserCoreModel, UserEntity>();
        }
    }
}
