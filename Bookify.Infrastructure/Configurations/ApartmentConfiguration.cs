using Bookify.Domain.Apartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
         builder.ToTable("apartments");
         builder.HasKey(a => a.Id);
         builder.OwnsOne(a => a.Adress);
         builder.Property(a => a.Name)
             .HasMaxLength(200)
             .HasConversion(d => d.Value, value => new Name(value));
         builder.Property(apartment => apartment.Description)
             .HasMaxLength(2000)
             .HasConversion(description => description.Value, value => new Description(value));

         builder.OwnsOne(apartment => apartment.Price, priceBuilder =>
         {
             priceBuilder.Property(money => money.currency)
                 .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
         });

         builder.OwnsOne(apartment => apartment.CleaningFee, priceBuilder =>
         {
             priceBuilder.Property(money => money.currency)
                 .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
         });

         builder.Property<uint>("Version").IsRowVersion();
    }
}
