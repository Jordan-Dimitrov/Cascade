using Music.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Dtos
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string AlbumName { get; set; }
        public DateTime DateCreated {  get; set; }
        public Guid ArtistId { get; set; }
    }
}
