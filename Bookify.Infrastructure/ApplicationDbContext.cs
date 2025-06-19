using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure;

public sealed class ApplicationDbContext : DbContext,IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

   public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);


        return result;
    }

   private async Task PublishDomainEventsAsync(CancellationToken cancellationToken = default)
   {
       var domainEntities = ChangeTracker
           .Entries<Entity>()
           .Select(e => e.Entity)
           .SelectMany(entry =>
           {
               var domainEvents = entry.GetDomainEvents();
               entry.ClearDomainEvents();
               return domainEvents;

           }).ToList();

       foreach (var domainEntity in domainEntities)
       {
           await _publisher.Publish(domainEntity, cancellationToken);
        }


    }
    public async Task<int> SaveChangesWithDomainEventsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync(cancellationToken);
        

            return result;
        }
        catch (DbUpdateConcurrencyException e)
        {
            Console.WriteLine(e);
            throw new ConcurrencyException("Concurrency exception occurred.", e);
        }
    }

}
 