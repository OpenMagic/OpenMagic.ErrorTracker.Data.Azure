using System;
using System.Collections;
using System.Collections.Generic;
using Mindscape.Raygun4Net.Messages;
using OpenMagic.ErrorTracker.Core.Events;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Helpers
{
    public class DummyFactory : Dummy
    {
        public DummyFactory()
        {
            InstanceFactories.Add(typeof(RaygunMessageReceived), () => new RaygunMessageReceived(RaygunApiKey(), Value<RaygunMessage>()));
            InstanceFactories.Add(typeof(RaygunIdentifierMessage), () => new RaygunIdentifierMessage(RaygunUser()));
            ValueFactories.Add(typeof(RaygunErrorMessage), RaygunErrorMessage);
        }

        public string RaygunUser()
        {
            return Value<string>();
        }

        private RaygunErrorMessage RaygunErrorMessage()
        {
            var errorMessage = new RaygunErrorMessage
            {
                ClassName = Value<string>(),
                Data = new Dictionary<string, string>(),
                Message = Value<string>(),
                StackTrace = Value<RaygunErrorStackTraceLineMessage[]>()
            };

            for (var i = 0; i < RandomNumber.NextInt(0, 5); i++)
            {
                errorMessage.Data.Add(Value<string>(), Value<string>());
            }

            return errorMessage;
        }

        public string RaygunApiKey()
        {
            return Guid.NewGuid().ToString();
        }
    }
}