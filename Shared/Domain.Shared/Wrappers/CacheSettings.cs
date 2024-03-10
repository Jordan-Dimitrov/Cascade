using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Wrappers
{
    public class CacheSettings
    {
        public int SlidingExpirationSeconds {  get; set; }
        public int AbsoluteExpirationRelativeToNowMinutes { get; set; }
    }
}
