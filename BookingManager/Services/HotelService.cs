using BookingManager.Abstractions;
using BookingManager.Models.Hotel;
using System.Text.Json;

namespace BookingManager.Services
{
    public class HotelService : IHotelService
    {
        private readonly IReadOnlyCollection<Hotel> _hotels;

        public HotelService(string filePath)
            => _hotels = JsonSerializer.Deserialize<IReadOnlyCollection<Hotel>>(File.ReadAllText(filePath)) 
                    ?? throw new FileNotFoundException(filePath);

        public Hotel? GetHotelById(string id) => _hotels.FirstOrDefault(h => h.Id == id);
    }
}
