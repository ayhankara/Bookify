using Bookify.Domain.Abstractions;

namespace Bookify.Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    public static T AssertDomainEventWasPublished<T>(Entity entity)
        where T : IDomainEvent
    {
       var domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();
       if (domainEvent==null)
       {
           throw new Exception($"Expected domain event of type {typeof(T).Name} to be published, but none was found.");

        }
        return  domainEvent;
    }
}