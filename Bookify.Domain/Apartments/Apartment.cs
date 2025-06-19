using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity
{
    public Apartment(
        Guid id,
        Name name,
        Description description,
        Adress adress,
        Money price,
        Money cleaningFee,
        List<Amenity> amenities) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Adress = adress ?? throw new ArgumentNullException(nameof(adress));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        CleaningFee = cleaningFee ?? throw new ArgumentNullException(nameof(cleaningFee));
        Amenities = amenities ?? throw new ArgumentNullException(nameof(amenities));

    }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Adress Adress { get; private set; }

    public Money Price { get; private set; }
    public Money CleaningFee { get; private set; }

    public DateTime? LastBookedOnUtc { get; internal set; }

    public List<Amenity> Amenities { get; private set; } = new List<Amenity>();



}