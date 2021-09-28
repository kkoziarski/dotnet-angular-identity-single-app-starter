using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace CleanArchWeb.Infrastructure.DI.identityserver.Storage
{
    public class RepositoryResourceStore: IResourceStore
    {
        protected IRepository _repository;

        public RepositoryResourceStore(IRepository repository)
        {
            _repository = repository;
            throw new NotImplementedException();
        }

        private IEnumerable<ApiResource> GetAllApiResources()
            => _repository.All<ApiResource>();

        private IEnumerable<IdentityResource> GetAllIdentityResources()
            => _repository.All<IdentityResource>();

        public Task<ApiResource> FindApiResourceAsync(string name)
            => Task.FromResult(_repository.Single<ApiResource>(a => a.Name == name));

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var apis = _repository.All<ApiResource>().ToList();
            // var list = apis.Where<ApiResource>(a =>  a.Scopes.Any(s =>  scopeNames.Contains(s.Name))).AsEnumerable();
            // return Task.FromResult(list);
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
            => Task.FromResult(_repository.Where<IdentityResource>(e => scopeNames.Contains(e.Name)).AsEnumerable());

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
            // => Task.FromResult(new Resources(GetAllIdentityResources(), GetAllApiResources()));
            => throw new NotImplementedException();

        private Func<IdentityResource, bool> BuildPredicate(Func<IdentityResource, bool> predicate)
            => predicate;
    }
}