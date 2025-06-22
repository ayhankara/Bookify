namespace Bookify.Domain.Users;

public sealed class Permision
{
    public static readonly Permision UsersRead = new(1, "users:read");
    public Permision(int id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
   // public ICollection<Role> Roles { get; init; } = new List<Role>();

}