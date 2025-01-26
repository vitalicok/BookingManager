using BookingManager.Abstractions;
using BookingManager.Exceptions;
using System.Globalization;

namespace BookingManager.Commands
{
    public class AvailabilityCommandHandler : ICommandHandler
    {
        private readonly IBookingService _bookingService;
        private readonly IHotelService _hotelService;

        public AvailabilityCommandHandler(IBookingService bookingService, IHotelService hotelService)
        {
            _bookingService = bookingService;
            _hotelService = hotelService;
        }

        public void Handle(string[] inputs)
        {
            if (inputs.Length != 3)
                throw new ArgumentException("Invalid number of arguments. Expected format: [HotelId, DateRange, RoomType]");

            var hotelId = inputs[0].Trim();
            var dateRange = inputs[1].Split("-", StringSplitOptions.TrimEntries);
            var roomType = inputs[2].Trim();

            if (!DateTime.TryParseExact(dateRange[0], Constants.Formats.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate))
                throw new InvalidDateFormat(dateRange[0]);

            DateTime endDate;

            if (dateRange.Length > 1 && !DateTime.TryParseExact(dateRange[1], Constants.Formats.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                throw new InvalidDateFormat(dateRange[1]);
            else endDate = startDate;

            if (endDate < startDate)
                throw new ArgumentException("End date cannot be earlier than start date.");

            var hotel = _hotelService.GetHotelById(hotelId);
            if (hotel is null)
            {
                Console.WriteLine($"Hotel with id {hotelId} not found.");
                return;
            }

            var booking = _bookingService.GetBookingsByHotelId(hotelId);
            var allRooms = hotel.Rooms.Count(a => a.RoomType == roomType);
            var unavailableRooms = booking.Count(a => a.RoomType == roomType && a.Arrival <= endDate && a.Departure >= startDate);

            var availableRooms = allRooms - unavailableRooms;
            Console.WriteLine($"Rooms available : {availableRooms}");
        }
    }
}
