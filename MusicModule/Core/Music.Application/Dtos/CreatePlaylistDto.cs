using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Dtos
{
    public class CreatePlaylistDto
    {
        public string PlaylistName { get; set; }
        public Guid SongId { get; set; }
    }
}
