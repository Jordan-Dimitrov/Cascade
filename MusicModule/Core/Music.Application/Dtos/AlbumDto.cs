namespace Music.Application.Dtos
{
    public class AlbumDto
    {
        public Guid Id { get; set; }
        public string AlbumName { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ArtistId { get; set; }
    }
}
