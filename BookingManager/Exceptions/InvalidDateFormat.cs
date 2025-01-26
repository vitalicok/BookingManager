namespace BookingManager.Exceptions
{
    public class InvalidDateFormat : Exception
    {
        public InvalidDateFormat(string date) : base($"{date} is not in a right format.")
        {
        }
    }
}
