using System.Threading.Tasks;
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

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
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
