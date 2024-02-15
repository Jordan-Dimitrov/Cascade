using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.RequestFeatures
{
    public class AlbumParameters : RequestParameters
    {
        public AlbumParameters()
        {
            OrderBy = "dateCreated";
        }
        public DateTime MinDateCreated { get; set; } = DateTime.MinValue;
        public DateTime MaxDateCreated { get; set; } = DateTime.MaxValue;
        public string? SearchTerm { get; set; }
    }
}
