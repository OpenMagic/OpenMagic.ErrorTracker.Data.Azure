using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using OpenMagic.ErrorTracker.Core.Events;
using OpenMagic.ErrorTracker.Core.Queues;
using OpenMagic.ErrorTracker.Core.Serialization;
using OpenMagic.ErrorTracker.Persistence.Azure.Settings;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Queues
{
    public class EventsQueue : IEventsQueue
    {
        private readonly Lazy<MessageSender> _sender;
        private readonly ISerializer _serializer;

        public EventsQueue(EventsQueueSettings settings, ISerializer serializer, IRunOnceQueueCreator runOnceQueueCreator)
        {
            _serializer = serializer;
            _sender = new Lazy<MessageSender>(() => CreateMessageSenderAsync(settings, runOnceQueueCreator).Result);
        }

        private MessageSender Sender => _sender.Value;

        public Task AddAsync(IEvent @event)
        {
            string contentType;

            var json = _serializer.ToJson(@event, out contentType);
            var message = new BrokeredMessage(json)
            {
                ContentType = contentType,
                MessageId = @event.EventId.ToString()
            };

            return Sender.SendAsync(message);
        }

        private static async Task<MessageSender> CreateMessageSenderAsync(EventsQueueSettings settings, IRunOnceQueueCreator runOnceQueueCreator)
        {
            await runOnceQueueCreator.CreateIfNotExistsAsync(settings.ServiceBusEndpoint, settings.Name);

            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(settings.RootSendKeyName, settings.SendKey);

            var messagingFactorySettings = new MessagingFactorySettings
            {
                TransportType = TransportType.Amqp,
                TokenProvider = tokenProvider
            };

            var senderFactory = MessagingFactory.Create(settings.ServiceBusEndpoint, messagingFactorySettings);
            var messageSender = await senderFactory.CreateMessageSenderAsync(settings.Name);

            return messageSender;
        }
    }
}