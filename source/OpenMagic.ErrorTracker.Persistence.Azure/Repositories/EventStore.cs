using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenMagic.ErrorTracker.Core.Events;
using OpenMagic.ErrorTracker.Core.Repositories;
using OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Repositories
{
    public class EventStore : IEventStore
    {
        private readonly EventStoreContext _context;

        public EventStore(EventStoreContext eventStoreContext)
        {
            _context = eventStoreContext;
        }

        public Task SaveEventsAsync<TAggregate>(Guid aggregateId, IEnumerable<IEvent> events)
        {
            return SaveEventsAsync(typeof(TAggregate), aggregateId, events);
        }

        public async Task SaveEventsAsync(Type aggregateType, Guid aggregateId, IEnumerable<IEvent> events)
        {
            var blob = await _context.GetBlobAsync(aggregateType, aggregateId);
            var newAggregate = blob.ExistsAsync();

            lock (new object())
            {
                
            }

            await blob.CreateIfNotExistsAsync();

            throw new NotImplementedException("todo");
            //var currentVersion = await GetCurrentVersionNumberAsync(aggregateType, aggregateId);
            //var batchOperation = new TableBatchOperation();
            //var tableEntities = events.Select((@event, i) => new EventTableEntity(aggregateType, aggregateId, currentVersion + 1 + i, @event));

            //foreach (var tableEntity in tableEntities)
            //{
            //    batchOperation.Insert(tableEntity);
            //}

            //await Table.ExecuteBatchAsync(batchOperation);
        }
    }
}