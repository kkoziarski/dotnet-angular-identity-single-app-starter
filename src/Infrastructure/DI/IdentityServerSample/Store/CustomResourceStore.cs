using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchWeb.Infrastructure.DI.IdentityServerSample.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace CleanArchWeb.Infrastructure.DI.IdentityServerSample.Store
{
    public class CustomResourceStore : IResourceStore
    {
        protected readonly IRepository _dbRepository;

        public CustomResourceStore(IRepository repository)
        {
            _dbRepository = repository;
            throw new NotImplementedException();
        }

        private IEnumerable<ApiResource> GetAllApiResources()
        {
            return _dbRepository.All<ApiResource>();
        }

        private IEnumerable<IdentityResource> GetAllIdentityResources()
        {
            return _dbRepository.All<IdentityResource>();
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            return Task.FromResult(_dbRepository.Single<ApiResource>(a => a.Name == name));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
            // var list = _dbRepository.Where<ApiResource>(a => a.Scopes.Any(s => scopeNames.Contains(s.Name)));

            // return Task.FromResult(list.AsEnumerable());
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var list = _dbRepository.Where<IdentityResource>(e => scopeNames.Contains(e.Name));
            return Task.FromResult(list.AsEnumerable());
        }

        public Task<Resources> GetAllResources()
        {
            throw new NotImplementedException();
            // var result = new Resources(GetAllIdentityResources(), GetAllApiResources());
            // return Task.FromResult(result);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            throw new NotImplementedException();
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            throw new NotImplementedException();
            //var result = new Resources(GetAllIdentityResources(), GetAllApiResources());
            // return Task.FromResult(result);
        }
    }
}
