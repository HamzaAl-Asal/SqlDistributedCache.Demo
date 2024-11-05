namespace SqlDistributedCache.Demo.DataAccess.Entities
{
    public class CachedItems
    {
        public string Id { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset ExpiresAtTime { get; set; }
        public DateTimeOffset AbsoluteExpiration { get; set; }
        public int? SlidingExpirationInSeconds { get; set; }
    }
}