using System;
using System.Threading.Tasks;
using FlightBuddy.Model;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FlightBuddy
{
    public partial class App : Application
    {
        public static MobileServiceClient MobileService =
            new MobileServiceClient(
                "https://flightbuddy.azurewebsites.net"
            );

        public static User User = new User();

        public static LocalDb.LocalDb LocalDb; 

        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        public interface IAuthenticate
        {
            Task<bool> Authenticate();
        }
     
        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public App(string databaseLocation)
        {
            InitializeComponent();
            DatabaseLocation = databaseLocation;
            LocalDb = new LocalDb.LocalDb();
            if (LoginIfPossible())
            {
                MainPage = new NavigationPage(new LoginPage());
            }
   
        }

        private static bool LoginIfPossible()
        {
            
            if (LocalDb.CheckIfUserEmpty())
            {
                var localUser = LocalDb.GetUser();

                var user = new User()
                {
                    Id = localUser.UserId,
                    Email = localUser.Email,
                    MobileNumber = localUser.MobileNumber,
                    Name = localUser.Name,
                    Password = localUser.Password
                };
                App.User = user;
                App.Current.MainPage = new NavigationPage(new HomePage());
                return false;
            }

            return true;
        }

        protected  override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
