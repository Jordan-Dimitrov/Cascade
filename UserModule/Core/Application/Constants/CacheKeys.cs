using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Constants
{
    public static class CacheKeys
    {
        public const string UsersKey = "users";
        public static string GetUserKey(Guid id)
        {
            return $"user-{id}";
        }
    }
}
