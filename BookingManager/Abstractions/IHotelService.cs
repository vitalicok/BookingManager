using BookingManager.Models.Hotel;

namespace BookingManager.Abstractions
{
    public interface IHotelService
    {
        Hotel? GetHotelById(string id);
    }
}
