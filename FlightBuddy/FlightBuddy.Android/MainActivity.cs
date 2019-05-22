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
using Android.Webkit;
using FlightBuddy.Auth;
using FlightBuddy.ToastNotification;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Environment = System.Environment;

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

            string dbName = "FlightBuddy_DB.sqlite";
            string folderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbName);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            App.Init((IAuthenticate)this);

            LoadApplication(new App(fullPath));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private MobileServiceUser User { get; set; }

        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            try
            {
                if (User == null)
                {
                    
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    User = await App.MobileService.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook, Constants.URLScheme);
                    if (User != null)
                    {

                        DependencyService.Get<IMessage>().LongAlert("You are now logged in via Facebook");
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert($"{ex}, Authentication failed");
            }
            return success;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (User != null)
                {
                    CookieManager.Instance.RemoveAllCookie();
                    await App.MobileService.LogoutAsync();
                    
                }
                User = null;
                success = true;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().LongAlert($"{ex}, Logout failed");
            }

            return success;
        }
    }
}