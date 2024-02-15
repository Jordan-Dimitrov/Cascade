using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.RequestFeatures
{
    public class SongParameters : RequestParameters
    {
        public SongParameters()
        {
            OrderBy = "songName";
        }
        public DateTime MinDateCreated { get; set; } = DateTime.MinValue;
        public DateTime MaxDateCreated { get; set; } = DateTime.MaxValue;
        public string? SearchTerm { get; set; }
    }
}
