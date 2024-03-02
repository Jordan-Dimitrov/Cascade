using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Shared.Constants
{
    public static class RateLimits
    {
        public const int Limit = 30;
        public const string Period = "1m";
        public const string Endpoint = "*";
    }
}
