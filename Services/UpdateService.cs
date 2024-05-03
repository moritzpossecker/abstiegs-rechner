using System.Timers;

namespace Abstiegsrechner.Services
{
    public class UpdateService
    {
        public Action? Update;

        private System.Timers.Timer _timer;
        public UpdateService() 
        {
            _timer = new ();
            _timer.Interval = 5000;
            _timer.Elapsed += (sender, e) => InvokeUpdate();
            _timer.Start();
        }

        private void InvokeUpdate()
        {
            Update?.Invoke();
            if(DateTime.Now.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                _timer.Interval = 3 * 60000;
            }
            else
            {
                _timer.Interval = 60 * 60000;
            }
            _timer.Start();
        }
    }
}
