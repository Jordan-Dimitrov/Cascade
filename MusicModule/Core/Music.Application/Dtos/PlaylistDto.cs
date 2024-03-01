using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Dtos
{
    public class PlaylistDto
    {
        public Guid Id { get; set; }
        public string PlaylistName { get; set; }
    }
}
