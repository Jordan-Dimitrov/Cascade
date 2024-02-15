using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Domain.RequestFeatures
{
    public class ListenerParameters : RequestParameters
    {
        public ListenerParameters()
        {
            OrderBy = "username";
        }
        public int MinPlaylistCount { get; set; } = 1;
        public int MaxPlaylistCount { get; set; } = int.MaxValue;
        public string? SearchTerm { get; set; }
    }
}
