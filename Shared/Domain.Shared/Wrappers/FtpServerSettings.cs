using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Wrappers
{
    public sealed class FtpServerSettings
    {
        public string Host {  get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
    }
}
