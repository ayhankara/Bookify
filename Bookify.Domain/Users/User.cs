using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users;

public sealed class User : Entity
{
    public User(Guid id, FirstName firstName, LastName lastName, Email email) 
        : base(id)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Email = email ?? throw new ArgumentNullException(nameof(email));

    }
    private User()
    {

    }

    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }

    public Email Email { get; set; }

    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, email);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }

    public string IdentityId { get; private set; } = string.Empty;
    public void SetIdentityId(string identityId)
    { 
        IdentityId = identityId ?? throw new ArgumentNullException(nameof(identityId));
    }
}