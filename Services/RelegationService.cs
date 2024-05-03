namespace Abstiegsrechner.Services
{
    public class RelegationService
    {
        private UpdateService _updateService;

        public readonly RelegationCalculator Calculator;

        public Action? RelegationStatusUpdated;

        public DateTime LastTimeUpdated { get; set; }

        public RelegationService(UpdateService updateService)
        {
            _updateService = updateService;
            _updateService.Update += CheckRelegationStatus;

            Calculator = new RelegationCalculator();
        }

        private void CheckRelegationStatus()
        {
            LastTimeUpdated = DateTime.Now;

            Calculator.Calculate();

            RelegationStatusUpdated?.Invoke();
        }
    }
}
