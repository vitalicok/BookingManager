using System.Text.Json.Serialization;

namespace BookingManager.Models.Hotel
{
    public class Room
    {
        [JsonPropertyName("roomId")]

        public string RoomId { get; set; }
        [JsonPropertyName("roomType")]
        public string RoomType { get; set; }
    }
}
