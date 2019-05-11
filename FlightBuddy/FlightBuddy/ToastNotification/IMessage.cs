using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBuddy.ToastNotification
{
    public interface IMessage
    {
        void LongAlert(string message);

        void ShortAlert(string message);
    }
}
