using Domain.Shared.Abstractions;

namespace Music.Domain.RequestFeatures
{
    public class ArtistParameters : RequestParameters
    {
        public ArtistParameters()
        {
            OrderBy = "username";
        }
        public int MinAlbumCount { get; set; } = int.MinValue;
        public int MaxAlbumCount { get; set; } = int.MaxValue;
        public string? SearchTerm { get; set; }
    }
}
