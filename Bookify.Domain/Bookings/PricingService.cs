using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Apartments;

namespace Bookify.Domain.Bookings;

public class PricingService
{

    public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
    {
        var currency = apartment.Price.currency;

        var priceForPeriod = new Money(


            apartment.Price.Amount * period.LengthInDays, currency);
        decimal percentageUpCharge = 0;
        foreach (var amenity in apartment.Amenities)
        {
            percentageUpCharge += amenity switch
            {
                Amenity.GardenView or Amenity.mountainView => 0.05m, // 5% upcharge
                Amenity.airConditioning => 0.10m, // 10% upcharge
                Amenity.parking => 0.15m, // 15% upcharge
                _ => 0 // no upcharge for other amenities
            };

        }

        var ameniriesUpCharge = Money.Zero();
        if (percentageUpCharge > 0)
        {
            ameniriesUpCharge = new Money(priceForPeriod.Amount * percentageUpCharge, currency);
        }

        var totalPrice = Money.Zero();
        totalPrice += priceForPeriod ;

        if (!apartment.CleaningFee.IsZero())
        {
            totalPrice+= apartment.CleaningFee;
        }
        totalPrice+= ameniriesUpCharge;
        return new PricingDetails(
             priceForPeriod,
             apartment.CleaningFee,
             ameniriesUpCharge,
             totalPrice);


    }
}

