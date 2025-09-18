using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DEPI_C_AdvancedTask
{
    //Create a simple timer class that raises events for tick and completion.
    internal class SimpleTimer
    {
        private readonly System.Timers.Timer _timer;
        private int _ticks;
        private readonly int _totalTicks;

        public event Action<int>? OnTick;    
        public event Action? OnCompleted;

        public SimpleTimer(int intervalMs, int totalTicks)
        {
            _timer = new System.Timers.Timer(intervalMs); 
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;        
            _ticks = 0;
            _totalTicks = totalTicks;
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            _ticks++;
            OnTick?.Invoke(_ticks);

            if (_ticks >= _totalTicks)
            {
                _timer.Stop();
                OnCompleted?.Invoke();
            }
        }

        public void Start()
        {
            _ticks = 0;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }


    }
}
