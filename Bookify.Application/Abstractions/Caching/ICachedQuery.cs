using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Abstractions.Caching;

public interface ICachedQuery<Tresponse> : IQuery<Tresponse>, ICachedQuery;
public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
}