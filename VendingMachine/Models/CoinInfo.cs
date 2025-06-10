namespace VendingMachine.Models
{
    public static class CoinInfo
    {
        public const decimal NickelValue = 0.05m;
        public const decimal DimeValue = 0.10m;
        public const decimal QuarterValue = 0.25m;

        private static Dictionary<CoinType, decimal> CoinValues = new()
        {
            { CoinType.Nickel, NickelValue },
            { CoinType.Dime, DimeValue },
            { CoinType.Quarter, QuarterValue }
        };

        public static bool IsValidCoin(CoinType coin) => CoinValues.ContainsKey(coin);

        public static decimal GetCoinValue(CoinType coin) =>
            CoinValues.TryGetValue(coin, out var value) ? value : 0m;

        public static CoinType ParseCoin(string coin)
        {
            return coin?.ToLower() switch
            {
                "nickel" => CoinType.Nickel,
                "dime" => CoinType.Dime,
                "quarter" => CoinType.Quarter,
                "penny" => CoinType.Penny,
                _ => CoinType.Unknown
            };
        }
    }
}
