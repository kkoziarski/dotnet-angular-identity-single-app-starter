using System.Threading.Tasks;
using CleanArchWeb.Infrastructure.DI.IdentityServerSample.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace CleanArchWeb.Infrastructure.DI.IdentityServerSample.Store
{
    public class CustomClientStore : IClientStore
    {
        protected IRepository _dbRepository;

        public CustomClientStore(IRepository repository)
        {
            _dbRepository = repository;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _dbRepository.Single<Client>(c => c.ClientId == clientId);

            return Task.FromResult(client);
        }

     
    }
}
