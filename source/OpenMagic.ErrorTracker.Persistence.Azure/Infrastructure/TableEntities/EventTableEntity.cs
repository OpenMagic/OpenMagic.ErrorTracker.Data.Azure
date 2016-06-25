using System;
using Microsoft.WindowsAzure.Storage.Table;
using OpenMagic.ErrorTracker.Core.Events;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure.TableEntities
{
    internal class EventTableEntity : TableEntity
    {
        internal EventTableEntity(Type aggregateType, Guid aggregateId, long aggregateVersionNumber, IEvent @event)
            : base(aggregateType.ToPartititonKey(aggregateId), aggregateVersionNumber.ToRowKey())
        {
            EventType = @event.GetType().FullName;
            Event = EventConvert.Serialize(@event);
        }

        internal string EventType { get; set; }
        internal string Event { get; set; }
    }
}