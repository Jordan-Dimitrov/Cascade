using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Constants
{
    public static class SupportedAudioMimeTypes
    {
        public static List<string> Types = new List<string>(){ Mp3, Ogg };
        public const string Mp3 = ".mp3";
        public const string Ogg = ".ogg";
    }
}
