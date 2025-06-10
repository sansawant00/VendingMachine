using VendingMachine.Interfaces;
using VendingMachine.Models;

namespace VendingMachine.Services
{
    public class PaymentService : IPaymentService
    {
        private decimal _currentAmount = 0;

        public void AcceptCoin(string coinInput)
        {
            var coin = CoinInfo.ParseCoin(coinInput);
            if (CoinInfo.IsValidCoin(coin))
                _currentAmount += CoinInfo.GetCoinValue(coin);
            // else simulate coin return (log, notify etc.)
        }

        public string DisplayMessage() =>
            _currentAmount == 0 ? "INSERT COIN" : $"Current amount: ${_currentAmount:F2}";

        public void Reset() => _currentAmount = 0;

        public decimal GetCurrentAmount() => _currentAmount;
    }

}
