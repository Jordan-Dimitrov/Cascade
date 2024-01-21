using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public UserRole Role { get; set; }
    }
}