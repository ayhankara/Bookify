namespace Bookify.Domain.Users;

public sealed class Permission
{
    public static readonly Permission UsersRead = new(1, "users:read");
    public Permission(int id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
   // public ICollection<Role> Roles { get; init; } = new List<Role>();

}