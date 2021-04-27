using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clock {

  /// <summary>
  /// event publisher:Clock class
  /// </summary> 
  class AlarmClock {
   
    public event Action<AlarmClock> AlarmEvent;

    public event Action<AlarmClock> TickEvent;

    public AlarmClock() {
      CurrentTime = new ClockTime();
    }

    public ClockTime CurrentTime { get; set; }

    public ClockTime AlarmTime { get; set; }

    /// start a clock, and keep it run
    public void Run() {
          while (true) {
            DateTime now = DateTime.Now;
            CurrentTime = new ClockTime(now.Hour, now.Minute, now.Second);
            TickEvent(this);
            if (AlarmTime.Equals(CurrentTime))
              AlarmEvent(this);
            Thread.Sleep(1000);
          }
        }
  }
}
