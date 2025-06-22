using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Bookings.GetBookings;

public sealed record GetAllBookingsQuery(Guid BookingId) : IQuery<BookingResponse>;