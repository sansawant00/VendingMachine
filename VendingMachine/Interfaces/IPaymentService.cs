namespace VendingMachine.Interfaces
{
    public interface IPaymentService
    {
        string DisplayMessage();
        void AcceptCoin(string coin);
        void Reset();
        decimal GetCurrentAmount();
    }
}
