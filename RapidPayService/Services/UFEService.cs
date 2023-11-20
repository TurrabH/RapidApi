namespace RapidPayService.Services
{
    public class UFEService
    {
        public decimal FeeAmount { get; set; } = 1;
        private Timer timer;

        public UFEService()
        {
            timer = new Timer(UpdateFeePrice, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        private void UpdateFeePrice(object? state)
        {
            var randFee = new Random().NextDouble();
            FeeAmount *= ((decimal)randFee * 2);
        }
    }
}
