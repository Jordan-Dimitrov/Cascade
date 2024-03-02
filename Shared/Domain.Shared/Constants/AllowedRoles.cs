using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Constants
{
    public static class AllowedRoles
    {
        public const string All = "User,Artist,Admin";
        public const string Admin = "Admin";
        public const string Artist = "Artist";
    }
}
