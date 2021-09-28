using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace CleanArchWeb.Infrastructure.DI.identityserver.Storage
{
    public class RepositoryPersistedGrantStore : IPersistedGrantStore
    {
        protected readonly IRepository _repository;

        public RepositoryPersistedGrantStore(IRepository repository)
        {
            _repository = repository;
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
            // => Task.FromResult(_repository.Where<PersistedGrant>(i => i.SubjectId == subjectId).AsEnumerable());
            => throw new NotImplementedException();
        
        public Task<PersistedGrant> GetAsync(string key)
            => Task.FromResult(_repository.Single<PersistedGrant>(i => i.Key == key));

        public Task RemoveAsync(string key)
        {
            _repository.Delete<PersistedGrant>(i => i.Key == key);
            return Task.CompletedTask;
        }

        public Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            // _repository.Delete<PersistedGrant>(i => i.SubjectId == subjectId && i.ClientId == clientId && i.Type == type);

            throw new NotImplementedException();
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            _repository.Add<PersistedGrant>(grant);
            return Task.CompletedTask;
        }
    }
}