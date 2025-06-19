namespace Bookify.Domain.Apartments;

public record Adress(
    string Country,
    string City,
    string State,
    string Street,
    string ZipCode
    );