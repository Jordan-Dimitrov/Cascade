using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Wrappers
{
    public class JwtTokenSettings
    {
        public string Token { get; set; } = null!;
        public int MinutesExpiry { get; set; }
    }
}
