using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_C_AdvancedTask
{
    //Build a notification system using multicast delegates for different notification channels.
    public delegate void NotificationHandler(string message);
    internal class Notification
    {
        public event NotificationHandler? OnNotify;

        public void SendNotification(string message)
        {
            if (OnNotify != null)
            {
                OnNotify(message);
            }
        }

        
       
    }
}
