using MongoDB.Driver;

namespace CleanArchWeb.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        IMongoRepository Repository { get; }
        IMongoDatabase Database { get; }

        // DbSet<TodoList> TodoLists { get; set; }
        //
        // DbSet<TodoItem> TodoItems { get; set; }
        //
        // Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}