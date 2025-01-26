using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookingManager.Utils
{
    public class DateTimeStringConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException("Expected string token for DateTime.");

            var dateString = reader.GetString();

            if (DateTime.TryParseExact(dateString, Constants.Formats.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date.Date;

            throw new JsonException($"Unable to parse '{dateString}' to DateTime with format '{Constants.Formats.DateFormat}'.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Constants.Formats.DateFormat, CultureInfo.InvariantCulture));
        }
    }
}
