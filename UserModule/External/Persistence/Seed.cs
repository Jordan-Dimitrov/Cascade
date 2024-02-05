using Domain.Abstractions;
using Domain.Aggregates.UserAggregate;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        private readonly ApplicationDbContext _Context;
        private readonly IAuthService _AuthService;
        public Seed(ApplicationDbContext context, IAuthService authService)
        {
            _Context = context;
            _AuthService = authService;
        }
        public void SeedApplicationContext()
        {
            UserPassword pass = _AuthService.CreatePasswordHash("prototype");

            if (!_Context.Users.Any())
            {
                List<User> users = new List<User>();

                users.Add(User.CreateUser("TOMAAAA", pass.PasswordHash, pass.PasswordSalt,
                    _AuthService.GenerateRefreshToken(), UserRole.Admin));

                users.Add(User.CreateUser("KristiQn Enchev", pass.PasswordHash, pass.PasswordSalt,
                    _AuthService.GenerateRefreshToken(), UserRole.User));

                _Context.Users.AddRange(users);
                _Context.SaveChanges();
            }
        }
    }
}
