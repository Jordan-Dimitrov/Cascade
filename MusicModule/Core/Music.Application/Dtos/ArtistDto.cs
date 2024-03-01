using Music.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Dtos
{
    public class ArtistDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FollowCount { get; set; }
    }
}
