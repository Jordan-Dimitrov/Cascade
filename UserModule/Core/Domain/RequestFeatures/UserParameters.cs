using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RequestFeatures
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
