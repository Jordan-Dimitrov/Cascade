using Domain.Shared.Abstractions;
using Domain.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Domain.RequestFeatures
{
    public class UserParameters : RequestParameters
    {
        public UserParameters()
        {
            OrderBy = "permissionType";
        }
        public UserRole MinRole { get; set; } = UserRole.Visitor;
        public UserRole MaxRole { get; set; } = UserRole.Admin;
        public string? SearchTerm { get; set; }
    }
}
