using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    // ReSharper disable once InconsistentNaming
    internal static class CloudAppendBlobExtensions
    {
        internal static async Task<bool> CreateIfNotExistsAsync(this CloudAppendBlob blob)
        {
            if (await blob.ExistsAsync())
            {
                return false;
            }

            await blob.CreateImplAsync();
            return true;
        }

        internal static async Task CreateAsync(this CloudAppendBlob blob)
        {
            if (await blob.ExistsAsync())
            {
                throw new CloudBlobExistsException(blob);
            }

            await blob.CreateImplAsync();
        }

        private static async Task CreateImplAsync(this CloudAppendBlob blob)
        {
            try
            {
                await blob.CreateOrReplaceAsync();
            }
            catch (StorageException exception)
            {
                throw new InformativeStorageException(exception);
            }
        }
    }
}
