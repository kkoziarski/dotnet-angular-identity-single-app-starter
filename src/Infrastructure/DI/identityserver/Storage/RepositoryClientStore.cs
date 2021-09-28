using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace CleanArchWeb.Infrastructure.DI.identityserver.Storage
{
    public class RepositoryClientStore : IClientStore
    {
        protected IRepository _repository;

        public RepositoryClientStore(IRepository repository) 
            => _repository = repository;

        public Task<Client> FindClientByIdAsync(string clientId)
            => Task.FromResult(_repository.Single<Client>(c => c.ClientId == clientId));
    }
}