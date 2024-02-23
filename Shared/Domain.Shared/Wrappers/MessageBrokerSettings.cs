using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Wrappers
{
    public sealed class MessageBrokerSettings
    {
        public string Host {  get; set; } = string.Empty;
        public string Username {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
