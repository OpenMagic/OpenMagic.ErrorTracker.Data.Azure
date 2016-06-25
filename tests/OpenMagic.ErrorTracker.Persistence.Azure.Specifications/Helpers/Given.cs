using System;
using OpenMagic.ErrorTracker.Core.Events;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Helpers
{
    public class Given
    {
        public IEvent Event { get; set; }
        public Type AggregateType { get; set; }
        public Guid AggregateId { get; set; }
    }
}