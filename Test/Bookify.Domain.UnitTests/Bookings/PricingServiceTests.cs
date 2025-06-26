using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.UnitTests.Apartments;
using FluentAssertions;
using Xunit;

namespace Bookify.Domain.UnitTests.Bookings;

public class PricingServiceTests
{
    [Fact]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice()
    {
        // Arrange
        var price = new Money(10.0m, Currency.TRY);
        var period =  DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(10)));
        var expectedTotalPrice = new Money(price.Amount*period.LengthInDays,price.currency);
        Apartment apartment = ApartmentData.Create(price);
        var pricingService = new PricingService();

        // Act
        var pricingDetails = pricingService.CalculatePrice(apartment, period);
        // Assert
 
        pricingDetails.TotalPrice.Should().Be(expectedTotalPrice);
    }

    [Fact]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice_WhenCleaningFeeIsIncluded()
    {
        // Arrange
        var price = new Money(10.0m, Currency.TRY);
        var cleaningFee = new Money(99.99m, Currency.TRY);
        var period = DateRange.Create(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(10)));
        var expectedTotalPrice = new Money(price.Amount * period.LengthInDays+cleaningFee.Amount, price.currency);
        Apartment apartment = ApartmentData.Create(price,cleaningFee);
        var pricingService = new PricingService();

        // Act
        var pricingDetails = pricingService.CalculatePrice(apartment, period);
        // Assert

        pricingDetails.TotalPrice.Should().Be(expectedTotalPrice);
    }
}