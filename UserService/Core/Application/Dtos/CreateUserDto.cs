using Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
