using Application.Dtos;
using AutoMapper;
using Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations() 
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
