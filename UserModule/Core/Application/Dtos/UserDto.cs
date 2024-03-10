using Domain.Shared.Abstractions;
using Domain.Shared.Constants;

namespace Users.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public UserRole PermissionType { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}