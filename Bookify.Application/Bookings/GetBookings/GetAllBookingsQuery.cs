using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Bookings.GetBooking;

public sealed record GetAllBookingsQuery(Guid BookingId):IQuery<BookingResponse>;