using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using OpenMagic.ErrorTracker.Core.Events;
using OpenMagic.ErrorTracker.Core.Repositories;
using OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure;
using OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure.TableEntities;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Repositories
{
    public class EventStore : IEventStore
    {
        private readonly Lazy<CloudTable> _table;

        public EventStore(EventStoreSettings settings, TableManager tableManager)
        {
            _table = new Lazy<CloudTable>(() => tableManager.GetTable(settings.TableName));
        }

        private CloudTable Table => _table.Value;

        public Task SaveEventsAsync<TAggregate>(Guid aggregateId, IEnumerable<IEvent> events)
        {
            return SaveEventsAsync(typeof(TAggregate), aggregateId, events);
        }

        public async Task SaveEventsAsync(Type aggregateType, Guid aggregateId, IEnumerable<IEvent> events)
        {
            var currentVersion = await GetCurrentVersionNumberAsync(aggregateType, aggregateId);
            var batchOperation = new TableBatchOperation();
            var tableEntities = events.Select((@event, i) => new EventTableEntity(aggregateType, aggregateId, currentVersion + 1 + i, @event));

            foreach (var tableEntity in tableEntities)
            {
                batchOperation.Insert(tableEntity);
            }

            await Table.ExecuteBatchAsync(batchOperation);
        }

        private async Task<int> GetCurrentVersionNumberAsync(Type aggregateType, Guid aggregateId)
        {
            var partititonKey = aggregateType.ToPartititonKey(aggregateId);
            var query = new TableQuery<TableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partititonKey));

            var items = 0;
            TableContinuationToken token = null;

            do
            {
                var segment = await Table.ExecuteQuerySegmentedAsync(query, token);
                token = segment.ContinuationToken;
                items += segment.Count();
            } while (token != null);

            return items;
        }
    }
}