using Bookify.Domain.Apartments;

namespace Bookify.Application.UnitTests.Apartments;

internal static class ApartmentData
{
    public static Apartment Create() => new (
        Guid.NewGuid(),
        new Name("Test Apartment"),
        new Description("Test Description"),
        new Adress("Test Street", "123", "Test City", "12345", "Test Country"),
        new Money(100, Currency.TRY),
        Money.Zero(),
        []);

}