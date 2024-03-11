using Domain.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Users.Application.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.Wrappers;

namespace Persistence
{
    public class UserSeed
    {
        private readonly ApplicationDbContext _Context;
        private readonly IAuthService _AuthService;
        public UserSeed(ApplicationDbContext context, IAuthService authService)
        {
            _Context = context;
            _AuthService = authService;
        }
        public void SeedApplicationContext()
        {
            _Context.Database.Migrate();

            UserPassword pass = _AuthService.CreatePasswordHash("prototype");

            if (!_Context.Users.Any())
            {
                List<User> users = new List<User>();

                users.Add(User.CreateUser("TOMAAAA", pass.PasswordHash, pass.PasswordSalt,
                    UserRole.Admin));

                users.Add(User.CreateUser("KristiQn Enchev", pass.PasswordHash, pass.PasswordSalt,
                    UserRole.Artist));

                _Context.Users.AddRange(users);
                _Context.SaveChanges();
            }
        }
    }
}
