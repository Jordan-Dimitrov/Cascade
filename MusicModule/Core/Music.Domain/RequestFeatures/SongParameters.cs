using Domain.Shared.Abstractions;

namespace Music.Domain.RequestFeatures
{
    public class SongParameters : RequestParameters
    {
        public SongParameters()
        {
            OrderBy = "songName";
        }
        public DateTime MinDateCreated { get; set; } = DateTime.MinValue;
        public DateTime MaxDateCreated { get; set; } = DateTime.MaxValue;
        public string? SearchTerm { get; set; }
    }
}
