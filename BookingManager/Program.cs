using BookingManager;
using BookingManager.Abstractions;
using BookingManager.Commands;
using BookingManager.Services;

IHotelService hotelService = new HotelService("Resources/hotels.json");
IBookingService bookingService = new BookingService("Resources/bookings.json");

var commandHandlers = new Dictionary<string, ICommandHandler>
{
    { Constants.Commands.Availability, new AvailabilityCommandHandler(bookingService, hotelService) },
    { Constants.Commands.Search, new SearchCommandHandler(bookingService, hotelService) }
}; 

while (true)
{
    Console.Write("Enter command: ");
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) break;

    var inputs = input.Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);

    var command = inputs[0];
    if (commandHandlers.TryGetValue(command, out var handler))
        handler.Handle(inputs[1..]);
    else
        Console.WriteLine("Unknown command.");
}