using System;
using System.Threading.Tasks;
using LazyCache;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure;
using OpenMagic.ErrorTracker.Persistence.Azure.Settings;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Repositories
{
    public class EventStoreContext
    {
        private readonly CloudBlobClient _blobClient;
        private readonly EventStoreNamer _eventStoreNamer;
        private readonly IAppCache _cache;

        internal EventStoreContext(AzureStorageSettings storageSettings, EventStoreNamer eventStoreNamer, IAppCache cache)
        {
            _eventStoreNamer = eventStoreNamer;
            _cache = cache;
            _blobClient = CreateBlobClient(storageSettings);
        }

        private static CloudBlobClient CreateBlobClient(AzureStorageSettings storageSettings)
        {
            var storageAccount = CloudStorageAccount.Parse(storageSettings.ConnectionString);
            return storageAccount.CreateCloudBlobClient();
        }

        public async Task<CloudAppendBlob> GetBlobAsync(Type aggregateType, Guid aggregateId)
        {
            var blobName = _eventStoreNamer.GetBlobName(aggregateType, aggregateId);
            var container = await GetBlobContainerAsync();
            var blob = container.GetAppendBlobReference(blobName);

            return blob;
        }

        private async Task<CloudBlobContainer> GetBlobContainerAsync()
        {
            try
            {
                var name = _eventStoreNamer.ContainerName();
                var container = _blobClient.GetContainerReference(name);

                await container.CreateIfNotExistsAsync(_cache);

                return container;
            }
            catch (StorageException exception)
            {
                throw new InformativeStorageException(exception);
            }
        }
    }
}