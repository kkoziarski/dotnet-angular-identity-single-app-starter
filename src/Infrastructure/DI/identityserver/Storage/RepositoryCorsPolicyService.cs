using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace CleanArchWeb.Infrastructure.DI.identityserver.Storage
{
    public class RepositoryCorsPolicyService : ICorsPolicyService
    {
        private readonly IRepository _repository;
        private readonly string[] _allowedOrigins;

        public RepositoryCorsPolicyService(IRepository repository)
        {
            _repository = repository;
            _allowedOrigins = _repository.All<Client>().SelectMany(x => x.AllowedCorsOrigins).ToArray();
        }

        public Task<bool> IsOriginAllowedAsync(string origin)
            => Task.FromResult(_allowedOrigins.Contains(origin));
    }
}