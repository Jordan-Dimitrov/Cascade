using Domain.Shared.Abstractions;

namespace Music.Application.Dtos
{
    public class AlbumWithSongsDto
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        public string AlbumName { get; set; }
        public DateTime DateCreated { get; set; }
        public List<SongDto> Songs { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
