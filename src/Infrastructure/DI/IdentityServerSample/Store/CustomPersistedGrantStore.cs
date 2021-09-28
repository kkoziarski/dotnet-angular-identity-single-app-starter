using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchWeb.Infrastructure.DI.IdentityServerSample.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace CleanArchWeb.Infrastructure.DI.IdentityServerSample.Store
{
    public class CustomPersistedGrantStore : IPersistedGrantStore
    {
        protected IRepository _dbRepository;

        public CustomPersistedGrantStore(IRepository repository)
        {
            _dbRepository = repository;
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            Validate(filter);

            var result = _dbRepository.Where<PersistedGrant>(
                x => (string.IsNullOrWhiteSpace(filter.SubjectId) || x.SubjectId == filter.SubjectId) &&
                     (string.IsNullOrWhiteSpace(filter.ClientId) || x.ClientId == filter.ClientId) &&
                     (string.IsNullOrWhiteSpace(filter.Type) || x.Type == filter.Type)).ToList();

            return Task.FromResult(result.AsEnumerable());
        }
        
        public Task<PersistedGrant> GetAsync(string key)
        {
            var result = _dbRepository.Single<PersistedGrant>(i => i.Key == key);
            return Task.FromResult(result);
        }

        public Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            Validate(filter);
            
            _dbRepository.Delete<PersistedGrant>(
                x => (string.IsNullOrWhiteSpace(filter.SubjectId) || x.SubjectId == filter.SubjectId) &&
                     (string.IsNullOrWhiteSpace(filter.ClientId) || x.ClientId == filter.ClientId) &&
                     (string.IsNullOrWhiteSpace(filter.Type) || x.Type == filter.Type));

            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            _dbRepository.Delete<PersistedGrant>(i => i.Key == key);
            return Task.FromResult(0);
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            _dbRepository.Add<PersistedGrant>(grant);
            return Task.FromResult(0);
        }
        
        private void Validate(PersistedGrantFilter filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            if (string.IsNullOrWhiteSpace(filter.ClientId) &&
                string.IsNullOrWhiteSpace(filter.SubjectId) &&
                string.IsNullOrWhiteSpace(filter.Type))
            {
                throw new ArgumentException("No filter values set.", nameof(filter));
            }
        }
    }
}
