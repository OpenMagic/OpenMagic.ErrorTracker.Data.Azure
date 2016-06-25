using System.Collections.Concurrent;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    public class TableManager
    {
        private static readonly ConcurrentDictionary<string, bool> CheckedExistence = new ConcurrentDictionary<string, bool>();

        private readonly CloudTableClient _tableClient;

        public TableManager(TableManagerSettings settings)
        {
            var connection = CloudStorageAccount.Parse(settings.ConnectionString);
            _tableClient = connection.CreateCloudTableClient();
        }

        public CloudTable GetTable(string tableName)
        {
            var table = _tableClient.GetTableReference(tableName);

            CreateTableIfNotExists(table);

            return table;
        }

        /// <summary>
        /// Creates the table if not exists. Table existence is checked once for application lifetime.
        /// </summary>
        /// <param name="table">The table.</param>
        private static void CreateTableIfNotExists(CloudTable table)
        {
            CheckedExistence.GetOrAdd(table.Name, name => table.CreateIfNotExists());
        }
    }
}