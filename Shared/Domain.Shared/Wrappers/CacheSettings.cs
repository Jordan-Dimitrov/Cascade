namespace Domain.Shared.Wrappers
{
    public class CacheSettings
    {
        public int SlidingExpirationSeconds { get; set; }
        public int AbsoluteExpirationRelativeToNowMinutes { get; set; }
    }
}
