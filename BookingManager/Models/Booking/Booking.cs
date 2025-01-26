using BookingManager.Utils;
using System.Text.Json.Serialization;

namespace BookingManager.Models.Booking
{
    public class Booking
    {
        [JsonPropertyName("hotelId")]
        public string HotelId { get; set; }

        [JsonPropertyName("arrival")]
        [JsonConverter(typeof(DateTimeStringConverter))]
        public DateTime Arrival { get; set; }

        [JsonConverter(typeof(DateTimeStringConverter))]
        [JsonPropertyName("departure")]
        public DateTime Departure { get; set; }

        [JsonPropertyName("roomType")]
        public string RoomType { get; set; }

        [JsonPropertyName("roomRate")]
        public string RoomRate { get; set; }
    }
}
