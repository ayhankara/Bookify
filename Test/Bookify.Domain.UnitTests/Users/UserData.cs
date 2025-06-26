using Bookify.Domain.Users;

namespace Bookify.Domain.UnitTests.Users;

internal static class UserData
{
    public static readonly FirstName FirstName = new FirstName("Ayhan");
    public static readonly LastName LastName = new LastName("Kara");
    public static readonly Email Email = new Email("ayhankara@outlook.com");
}