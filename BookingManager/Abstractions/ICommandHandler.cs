namespace BookingManager.Abstractions
{
    public interface ICommandHandler
    {
        void Handle(string[] inputs);
    }
}
