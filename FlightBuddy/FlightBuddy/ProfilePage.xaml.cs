using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.ToastNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
	    public ProfilePage()
	    {
	        InitializeComponent();
            db = new FlightBuddyContext.FlightBuddyContext();
	    }

	    private FlightBuddyContext.FlightBuddyContext db;

        protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        var user = await db.GetUserByEmail(App.User.Email);
	        userEmail.Text = user.Email;
	    }

	    private async void LogoutButton_Clicked(object sender, EventArgs e)
	    {
	        App.User = null;
	        await Navigation.PushAsync(new LoginPage());
	        DependencyService.Get<IMessage>().LongAlert("You've been successfully logged out");
        }
    }
}