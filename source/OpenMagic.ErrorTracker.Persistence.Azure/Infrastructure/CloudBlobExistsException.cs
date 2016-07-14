using System;
using Microsoft.WindowsAzure.Storage.Blob;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    internal class CloudBlobExistsException : Exception
    {
        public CloudBlobExistsException(CloudBlob blob)
            : base(CreateErrorMessage(blob))
        {
        }

        private static string CreateErrorMessage(CloudBlob blob)
        {
            try
            {
                return $"Cannot create blob '{blob.Container.Name}/{blob.Name}' because it already exists.";
            }
            catch (Exception exception)
            {
                return $"Cannot create {nameof(CloudBlobExistsException)} error message. {exception.Message}";
            }
        }
    }
}