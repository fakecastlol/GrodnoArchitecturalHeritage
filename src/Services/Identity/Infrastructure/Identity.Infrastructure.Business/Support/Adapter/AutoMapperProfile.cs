using AutoMapper;
using Identity.Domain.Core.Entities;
using Identity.Services.Interfaces.Models.Sorting;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Profile;
using Identity.Services.Interfaces.Models.User.Register;
using System;
using BC = BCrypt.Net.BCrypt;

namespace Identity.Infrastructure.Business.Support.Adapter
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap(typeof(IndexViewModel<>), typeof(IndexViewModel<>));

            CreateMap<SortViewModel, IndexViewModel<UserResponseCoreModel>>();
            CreateMap<SortViewModel, IndexViewModel<UserResponseCoreModel>>().ReverseMap();

            CreateMap<UserResponseCoreModel, IndexViewModel<UserResponseCoreModel>>();
            CreateMap<UserResponseCoreModel, IndexViewModel<UserResponseCoreModel>>().ReverseMap();

            CreateMap<UserResponseCoreModel, IndexViewModel<UserResponseCoreModel>>();
            CreateMap<UserResponseCoreModel, IndexViewModel<UserResponseCoreModel>>().ReverseMap();

            CreateMap<User, UserResponseCoreModel>();
            CreateMap<UserResponseCoreModel, User>();

            CreateMap<User, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, User>()
                .ForMember(destinationMember => destinationMember.Password, x => x.MapFrom(x => BC.HashPassword(x.Password)))
                .ForMember(destinationMember => destinationMember.Role, x => x.MapFrom(x => Domain.Core.Entities.Enums.Roles.Unchecked))
                .ForMember(destinationMember => destinationMember.RegistrationDate, x => x.MapFrom(x => DateTime.UtcNow))
                .ForMember(destinationMember => destinationMember.Avatar, x => x.MapFrom(x => "/files/default/default-image.jpg"/*_fileSettings.DefaultImage*/));

            CreateMap<User, LoginResponseModel>();
            CreateMap<LoginResponseModel, User>();

            CreateMap<User, ImageViewModel>();
            CreateMap<ImageViewModel, User>();

            CreateMap<ProfileRequestModel, User>();
            CreateMap<ProfileRequestModel, User>().ReverseMap();
        }
    }
}
