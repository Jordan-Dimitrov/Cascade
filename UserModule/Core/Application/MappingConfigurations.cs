using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Dtos;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.ValueObjects;

namespace Users.Application
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations()
        {
            CreateMap<User, UserDto>()
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
               .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => src.PermissionType));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => new Username(src.Username)))
                .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => src.PermissionType))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserPatchDto, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => new Username(src.Username)))
                .ReverseMap();
        }
    }
}
