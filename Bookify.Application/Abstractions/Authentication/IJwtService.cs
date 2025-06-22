using Bookify.Domain.Abstractions;

namespace Bookify.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GenerateAccessTokenAsync(
        string userId,
        string password,
        CancellationToken cancellationToken = default);
}