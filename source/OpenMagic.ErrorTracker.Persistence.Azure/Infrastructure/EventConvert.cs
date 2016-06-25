using System;
using System.Text;
using Newtonsoft.Json;
using OpenMagic.ErrorTracker.Core.Events;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Infrastructure
{
    internal class EventConvert
    {
        private const int MaximumLength = 64 * 1024;

        internal static string Serialize(IEvent @event)
        {
            var json = JsonConvert.SerializeObject(@event, Formatting.None);
            var bytes = Encoding.UTF8.GetBytes(json);
            var base64 = Convert.ToBase64String(bytes);

            if (base64.Length > MaximumLength)
            {
                throw new Exception($"Serialized event is {base64.Length:N0} long, it must be less than {MaximumLength:N0}");
            }

            return base64;
        }
    }
}