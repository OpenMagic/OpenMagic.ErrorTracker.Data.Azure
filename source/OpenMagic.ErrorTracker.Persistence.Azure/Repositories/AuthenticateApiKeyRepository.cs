using System;
using System.Threading.Tasks;
using OpenMagic.ErrorTracker.Core.Repositories;

namespace OpenMagic.ErrorTracker.Persistence.Azure.Repositories
{
    public class AuthenticateApiKeyRepository : IAuthenticateApiKeyRepository
    {
        public AuthenticateApiKeyRepository()
        {
            throw new NotImplementedException("todo");
        }

        public Task<bool> AuthenticateAsync(string apiKey)
        {
            throw new NotImplementedException("todo");
        }
    }
}