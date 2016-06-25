using System.Threading.Tasks;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    /// <summary>
    ///     <see cref="IRunOnceQueueCreator" /> can create a <see cref="Microsoft.ServiceBus" /> queue but will only attempt to do so once. Subsequent calls will be ignored.
    /// </summary>
    public interface IRunOnceQueueCreator
    {
        /// <summary>
        ///     Creates the queue if does not exist.
        /// </summary>
        /// <param name="settings">
        ///     The settings required to create the queue.
        /// </param>
        Task CreateIfNotExistsAsync(IQueueCreatorSettings settings);
    }
}