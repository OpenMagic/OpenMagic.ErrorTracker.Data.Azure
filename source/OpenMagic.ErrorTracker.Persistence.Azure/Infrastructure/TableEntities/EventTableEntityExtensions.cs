using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure.TableEntities
{
    internal static class EventTableEntityExtensions
    {
        /// <summary>
        ///     Converts an event's <paramref name="aggregateType" /> and <paramref name="aggregateId" />
        ///     to a <see cref="ITableEntity.PartitionKey">partition key</see>.
        /// </summary>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="aggregateId">The aggregate identifier.</param>
        /// <returns>
        ///     <see cref="ToPartititonKey" /> returns a partition key for the Events table.
        ///     The format of the partition key is "{aggregate name}-{aggregate id}".
        /// </returns>
        internal static string ToPartititonKey(this Type aggregateType, Guid aggregateId)
        {
            return $"{aggregateType.Name}-{aggregateId}";
        }

        /// <summary>
        ///     Converts an event's version number to a <see cref="ITableEntity.RowKey">row key</see>.
        /// </summary>
        /// <param name="version">The version number to convert to a row key.</param>
        /// <returns>
        ///     <see cref="ToRowKey" /> returns the version number as a string with leading zeros
        ///     to the length of 19 characters.
        /// </returns>
        internal static string ToRowKey(this long version)
        {
            return version.ToString("D19");
        }
    }
}