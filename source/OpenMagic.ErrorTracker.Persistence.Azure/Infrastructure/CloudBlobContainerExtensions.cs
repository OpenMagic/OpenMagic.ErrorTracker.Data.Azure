using System;
using System.Threading.Tasks;
using LazyCache;
using Microsoft.WindowsAzure.Storage.Blob;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    internal static class CloudBlobContainerExtensions
    {
        internal static Task CreateIfNotExistsAsync(this CloudBlobContainer blobContainer, IAppCache cache)
        {
            var cacheKey = $"{nameof(CloudBlobContainerExtensions)}/{nameof(CreateIfNotExistsAsync)}/{blobContainer.Name}";
            var slidingExpiration = TimeSpan.FromMinutes(20);
            return Task.Run(() => cache.GetOrAdd(cacheKey, () => CreateIfNotExists(blobContainer), slidingExpiration));
        }

        private static bool CreateIfNotExists(CloudBlobContainer blobContainer)
        {
            var profiler = new LoggingProfiler();
            var created = blobContainer.CreateIfNotExistsAsync().Result;

            if (created)
            {
                profiler.Log(sw => $"Created blob container '{blobContainer.Name}' in {sw.ElapsedMilliseconds:N0}ms.");
            }
            else
            {
                profiler.Log(sw => $"Checked blob container '{blobContainer.Name}' exists in {sw.ElapsedMilliseconds:N0}ms.");
            }

            return created;
        }
    }
}