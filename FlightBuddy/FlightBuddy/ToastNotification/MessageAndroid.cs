using Android.App;
using Android.Widget;
using FlightBuddy.ToastNotification;


[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace FlightBuddy.ToastNotification
{
    public class MessageAndroid : IMessage
    {
        public MessageAndroid()
        {
        }

        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}

