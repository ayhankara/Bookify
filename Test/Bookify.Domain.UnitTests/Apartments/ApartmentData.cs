using Bookify.Domain.Apartments;

namespace Bookify.Domain.UnitTests.Apartments;

public class ApartmentData
{
    public static Apartment Create(Money price, Money? cleaningFee = null) => new(
        Guid.NewGuid(),
        new Name("Test Apartment"),
        new Description("Test Description"),
        new Adress("Test Country", "Test City", "Test State","TestStreet","TestZipCode"),
        price,
        cleaningFee ?? Money.Zero(), []);
}