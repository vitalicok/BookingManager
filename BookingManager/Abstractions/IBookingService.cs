using BookingManager.Models.Booking;

namespace BookingManager.Abstractions
{
    public interface IBookingService
    {
        IReadOnlyCollection<Booking> GetBookingsByHotelId(string hotelId);
    }
}
