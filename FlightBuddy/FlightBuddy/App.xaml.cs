using System;
using System.Threading.Tasks;
using Akavache;
using FlightBuddy.Model;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FlightBuddy
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }


    public partial class App : Application
    {
        public static MobileServiceClient MobileService =
            new MobileServiceClient(
                "https://flightbuddy.azurewebsites.net"
            );

        public static User User = new User();

        public static IAuthenticate Authenticator { get; private set; }

        

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public App()
        {
            InitializeComponent();
            Registrations.Start("FlightBuddy");
            //LoginIfPossible();
            MainPage = new NavigationPage(new LoginPage());
        }

        private static void LoginIfPossible()
        {
            Registrations.Start("FlightBuddy");
            BlobCache.UserAccount.GetObject<User>("User")
                .Subscribe(x => App.User = x);

            if (User != null)
            {
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
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
