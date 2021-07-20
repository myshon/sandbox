using System;
using NodaTime;

namespace EfCoreSamples.Contracts
{
    public class Audit
    {
        public Guid CreatorId { get; private set; }

        public Instant CreationTime { get; private set; }

        public Instant? UpdateTime { get; private set; }
        
        public DateTime? Timestamp { get; private set; }
        
        public DateTimeOffset? Timestamp2 { get; private set; }

        public Audit(Guid creatorId, Instant creationTime, Instant? updateTime, DateTimeOffset? timestamp2, DateTime? timestamp)
        {
            CreatorId = creatorId;
            CreationTime = creationTime;
            UpdateTime = updateTime;
            Timestamp2 = timestamp2;
            Timestamp = timestamp;
        }
    }
}
