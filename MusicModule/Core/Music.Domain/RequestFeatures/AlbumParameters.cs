using Domain.Shared.Abstractions;

namespace Music.Domain.RequestFeatures
{
    public class AlbumParameters : RequestParameters
    {
        public AlbumParameters()
        {
            OrderBy = "dateCreated";
        }
        public DateTime MinDateCreated { get; set; } = new DateTime(2023, 8, 1, 0, 0, 0, DateTimeKind.Utc);
        public DateTime MaxDateCreated { get; set; } = DateTime.UtcNow;

        public string? SearchTerm { get; set; }
    }
}
