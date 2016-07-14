using System;
using OpenMagic.ErrorTracker.Persistence.Azure.Settings;
using OpenMagic.Extensions;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Repositories
{
    internal class EventStoreNamer
    {
        private readonly ErrorTrackerAppSettings _appSettings;

        internal EventStoreNamer(ErrorTrackerAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        internal string ContainerName()
        {
            return $"{_appSettings.Environment.ToLower()}-event-store";
        }

        public string GetBlobName(Type aggregateType, Guid aggregateId)
        {
            return $"{GetAggregateName(aggregateType)}/{aggregateId}";
        }

        private static string GetAggregateName(Type aggregateType)
        {
            var name = aggregateType.Name;

            return name.TrimEnd("Aggregate").InsertStringBeforeEachUpperCaseCharacter("-").ToLower();
        }
    }
}