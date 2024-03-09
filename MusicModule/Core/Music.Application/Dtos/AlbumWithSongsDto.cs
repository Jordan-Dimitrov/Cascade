using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Dtos
{
    public class AlbumWithSongsDto
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        public string AlbumName { get; set; }
        public DateTime DateCreated { get; set; }
        public ArtistDto Artist { get; set; }
        public List<SongDto> Songs { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
