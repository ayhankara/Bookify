﻿using FluentValidation;

namespace Bookify.Application.Bookings.ReserveBooking;

public class ReserveBookingCommandValidator :AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(x => x.ApartmentId)
            .NotEmpty()
            .WithMessage("Apartment ID is required.");
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(x => x.StartDate).LessThan(c => c.EndDate);
    }
}