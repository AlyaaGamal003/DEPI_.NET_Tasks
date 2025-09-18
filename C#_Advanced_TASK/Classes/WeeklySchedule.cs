using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    // Build a WeeklySchedule class where you can access daily schedules using day names: schedule["Monday"].
    internal class WeeklySchedule
    {
        private Dictionary<string, string> schedule = new Dictionary<string, string>();
        public string this[string day]
        {
            get
            {
                if (schedule.ContainsKey(day))
                {
                    return schedule[day];
                }
                else
                {
                    return "No Schedule";
                }
            }
            set
            {
                schedule[day] = value;
            }
        }
    }
}
