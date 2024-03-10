namespace Music.Application.Dtos
{
    public class SongDto
    {
        public Guid Id { get; set; }
        public string SongName { get; set; }
        public string AudioFile { get; set; }
        public DateTime DateCreated { get; set; }
        public string SongCategory { get; set; }
    }
}
