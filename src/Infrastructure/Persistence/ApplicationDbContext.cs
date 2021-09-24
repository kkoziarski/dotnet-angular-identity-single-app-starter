using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CleanArchWeb.Infrastructure.Persistence
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        // fake class
    }
    public class ApplicationDbContextTOREMOVE //: IApplicationDbContext, ApiAuthorizationDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContextTOREMOVE(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) //: base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        // public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        // {
        //     foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
        //     {
        //         switch (entry.State)
        //         {
        //             case EntityState.Added:
        //                 entry.Entity.CreatedBy = _currentUserService.UserId;
        //                 entry.Entity.Created = _dateTime.Now;
        //                 break;
        //
        //             case EntityState.Modified:
        //                 entry.Entity.LastModifiedBy = _currentUserService.UserId;
        //                 entry.Entity.LastModified = _dateTime.Now;
        //                 break;
        //         }
        //     }
        //
        //     var result = await base.SaveChangesAsync(cancellationToken);
        //
        //     await DispatchEvents();
        //
        //     return result;
        // }
        //
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //
        //     base.OnModelCreating(builder);
        // }
        //
        // private async Task DispatchEvents()
        // {
        //     while (true)
        //     {
        //         var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
        //             .Select(x => x.Entity.DomainEvents)
        //             .SelectMany(x => x)
        //             .Where(domainEvent => !domainEvent.IsPublished)
        //             .FirstOrDefault();
        //         if (domainEventEntity == null) break;
        //
        //         domainEventEntity.IsPublished = true;
        //         await _domainEventService.Publish(domainEventEntity);
        //     }
        // }
    }
}
