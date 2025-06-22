using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
 
    public override void Add(User entity)
    {
        foreach (var role in entity.Roles)
        {
            dbContext.Attach(role);

        }

        dbContext.Add(entity);
    }

}