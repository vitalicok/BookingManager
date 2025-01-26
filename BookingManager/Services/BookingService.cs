using BookingManager.Abstractions;
using BookingManager.Models.Booking;
using BookingManager.Utils;
using System.Text.Json;

namespace BookingManager.Services
{
    public class BookingService : IBookingService
    {
        private readonly IReadOnlyCollection<Booking> _bookings;

        public BookingService(string filePath)
            => _bookings = JsonSerializer.Deserialize<IReadOnlyCollection<Booking>>(File.ReadAllText(filePath), new JsonSerializerOptions
            {
                Converters = { new DateTimeStringConverter() },
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            }) ?? throw new FileNotFoundException(filePath);

        public IReadOnlyCollection<Booking> GetBookingsByHotelId(string hotelId)
            => _bookings.Where(b => b.HotelId == hotelId).ToList();
    }
}
