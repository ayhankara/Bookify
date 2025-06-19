using Bookify.Domain.Abstractions;

namespace Bookify.Infrastructure.Repositories;

internal abstract class Repository<T>
where T: Entity
{
    protected readonly ApplicationDbContext dbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public void Add(T entity)
    {
        dbContext.Add(entity);
    }
}