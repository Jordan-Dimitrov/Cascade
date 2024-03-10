using Domain.Shared.Abstractions;
using Domain.Shared.Constants;

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
