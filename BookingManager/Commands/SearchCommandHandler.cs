using BookingManager.Abstractions;

namespace BookingManager.Commands
{
    public class SearchCommandHandler : ICommandHandler
    {
        private readonly IBookingService _bookingService;
        private readonly IHotelService _hotelService;

        public SearchCommandHandler(IBookingService bookingService, IHotelService hotelService)
        {
            _bookingService = bookingService;
            _hotelService = hotelService;
        }

        public void Handle(string[] inputs)
        {
            if (inputs.Length != 3)
                throw new ArgumentException("Invalid number of arguments. Expected format: [HotelId, DaysNumber, RoomType]");

            var hotelId = inputs[0];
            int daysAhead = default;
            if(!int.TryParse(inputs[1], out daysAhead)) {
                throw new ArgumentException("Invalid number of days ahead.");
            }
            var roomType = inputs[2].Trim();

            var hotel = _hotelService.GetHotelById(hotelId);
            if (hotel is null)
            {
                Console.WriteLine($"Hotel with id {hotelId} not found.");
                return;
            }

            var booking = _bookingService.GetBookingsByHotelId(hotelId);
            var currentDate = DateTime.Now;
            var daysDiff =  booking.OrderByDescending(b => b.Departure).FirstOrDefault(r => r.RoomType == roomType)?.Departure.Date.Subtract(currentDate).TotalDays;
            var totalRooms = hotel.Rooms.Count(r => r.RoomType == roomType);
            var results = new List<string>();
            int? currentAvailability = null;
            DateTime? rangeStart = null;

            for (int i = 0; i < daysAhead; i++)
            {
                var date = currentDate.AddDays(i);

                var occupiedRooms = booking.Count(b =>
                    b.RoomType == roomType &&
                    b.Arrival <= date &&
                    b.Departure > date);

                var availability = totalRooms - occupiedRooms;

                if (availability > 0)
                {
                    if (currentAvailability == availability && rangeStart.HasValue) continue;
                    if (currentAvailability != null && rangeStart.HasValue)
                        results.Add($"({rangeStart:yyyyMMdd}-{date.AddDays(-1):yyyyMMdd}, {currentAvailability})");

                    rangeStart = date;
                    currentAvailability = availability;
                }
                else if (rangeStart.HasValue)
                {
                    results.Add($"({rangeStart:yyyyMMdd}-{date.AddDays(-1):yyyyMMdd}, {currentAvailability})");
                    rangeStart = null;
                    currentAvailability = null;
                }

                if (rangeStart.HasValue && currentAvailability.HasValue && i >= daysDiff)
                    results.Add($"({rangeStart:yyyyMMdd}-{currentDate.AddDays(daysAhead - 1):yyyyMMdd}, {currentAvailability})");
            }
            Console.WriteLine(string.Join(", ", results));
        }
    }
}
