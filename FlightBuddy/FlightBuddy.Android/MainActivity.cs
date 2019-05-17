using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Net;
using Plugin.CurrentActivity;
using Environment = System.Environment;

namespace FlightBuddy.Droid
{
    [Activity(Label = "FlightBuddy", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity //, App.IAuthenticate
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            CurrentPlatform.Init();

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            string dbName = "FlightBuddy_DB.sqlite";
            string folderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbName);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //App.Init((App.IAuthenticate)this);

            LoadApplication(new App(fullPath));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private MobileServiceUser User { get; set; }

        //public async Task<bool> Authenticate()
        //{
        //    var success = false;
        //    var message = string.Empty;
        //    try
        //    {
        //        var xd = App.Authenticator.Authenticate()
        //        // Sign in with Facebook login using a server-managed flow.
        //        this.User = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
        //            MobileServiceAuthenticationProvider.Facebook, "https://flightbuddy.azurewebsites.net");
        //        if (this.User != null)
        //        {
        //            message = string.Format("you are now signed-in as {0}.",
        //                this.User.UserId);
        //            success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    // Display the success or failure message.
        //    AlertDialog.Builder builder = new AlertDialog.Builder(this);
        //    builder.SetMessage(message);
        //    builder.SetTitle("Sign-in result");
        //    builder.Create().Show();

        //    return success;
        //}
    }
}