using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public static class ApartmentErrors
{
    public static readonly Error NotFound = new Error("Apartment.NotFound", "Apartment not found.");
    public static readonly Error AlreadyExists = new Error("Apartment.AlreadyExists", "Apartment already exists.");
    public static readonly Error InvalidPrice = new Error("Apartment.InvalidPrice", "Invalid price for the apartment.");
    public static readonly Error InvalidAddress = new Error("Apartment.InvalidAddress", "Invalid address for the apartment.");
    public static readonly Error InvalidDescription = new Error("Apartment.InvalidDescription", "Invalid description for the apartment.");

}