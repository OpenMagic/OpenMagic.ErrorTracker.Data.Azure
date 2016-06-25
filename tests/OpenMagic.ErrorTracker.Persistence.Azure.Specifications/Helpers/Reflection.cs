using System;
using System.Linq;
using System.Reflection;
using OpenMagic.ErrorTracker.Core.Events;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Specifications.Helpers
{
    public static class Reflection
    {
        public static Type GetCoreTypeByName(string typeName)
        {
            var type = Assembly.GetAssembly(typeof(IEvent)).GetTypes().SingleOrDefault(t => t.Name == typeName);

            if (type == null)
            {
                throw new ArgumentOutOfRangeException(nameof(typeName), $"Cannot not find type '{typeName}' in core library.");
            }

            return type;
        }
    }
}