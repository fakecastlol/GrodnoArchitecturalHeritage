using AutoMapper;
using Identity.API.Models;
using Identity.API.Models.AppViewModel;
using Identity.Services.Interfaces.Models;

namespace Identity.API.Support.Adapter
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        { 
            CreateMap<UserCoreModel, UserViewModel>();
            CreateMap<UserViewModel, UserCoreModel>();

            CreateMap<RoleCoreModel, RoleViewModel>();
            CreateMap<RoleViewModel, RoleCoreModel>();

            CreateMap<RegisterCoreModel, RegisterModel>();
            CreateMap<RegisterModel, RegisterCoreModel>();

            CreateMap<LoginCoreModel, LoginViewModel>();
            CreateMap<LoginViewModel, LoginCoreModel>();
        }
    }
}
