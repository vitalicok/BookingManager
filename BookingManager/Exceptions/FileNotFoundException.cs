namespace BookingManager.Exceptions
{
    internal class FileNotFoundException : Exception
    {
        public FileNotFoundException(string filePath) : base($"File was not found at path {filePath}")
        {
        }
    }

}
