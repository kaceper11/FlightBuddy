using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace FlightBuddy.Droid
{
    [Activity(Label = "FlightBuddy", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            CurrentPlatform.Init();

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        private MobileServiceUser user;

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            //try
            //{
            //    // Sign in with Facebook login using a server-managed flow.
            //    user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
            //        MobileServiceAuthenticationProvider.Facebook, "https://flightbuddy.azurewebsites.net");
            //    if (user != null)
            //    {
            //        message = string.Format("you are now signed-in as {0}.",
            //            user.UserId);
            //        success = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    message = ex.Message;
            //}

            //// Display the success or failure message.
            //AlertDialog.Builder builder = new AlertDialog.Builder(this);
            //builder.SetMessage(message);
            //builder.SetTitle("Sign-in result");
            //builder.Create().Show();

            return success;
        }
    }
}