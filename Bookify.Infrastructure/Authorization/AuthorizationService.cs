using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {

        string cacheKey = $"auth:roles-{identityId}";

        var cacheRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);
        if (cacheRoles is not null)
        {
            return cacheRoles;
        }
         
        var roles = await _dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {

                UserId = u.Id,
                Roles = u.Roles.ToList()
            }).FirstAsync();

        await _cacheService.SetAsync(cacheKey, roles, TimeSpan.FromMinutes(5));

        return roles;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        string cacheKey = $"auth:permissions-{identityId}";
        var cachePermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);

        if (cachePermissions is not null)
        {
            return cachePermissions;
        }

        var permissions = await _dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .SelectMany(u => u.Roles.Select(r => r.Permissions))
            .FirstAsync();
        var permissionsSet = permissions.Select(p => p.Name).ToHashSet() ?? new HashSet<string>();
       
        await _cacheService.SetAsync(cacheKey, permissionsSet, TimeSpan.FromMinutes(5));

        return permissionsSet;
    }
}