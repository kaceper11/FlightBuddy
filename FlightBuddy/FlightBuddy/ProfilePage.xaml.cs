using System;
using System.Collections.Generic;
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
            localDb = new LocalDb.LocalDb();
	    }

	    private readonly FlightBuddyContext.FlightBuddyContext db;

	    private readonly LocalDb.LocalDb localDb;

        protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        var user = await db.GetUserByEmail(App.User.Email);
	        userEmail.Text = user.Email;
	    }

	    private async void LogoutButton_Clicked(object sender, EventArgs e)
	    {
	        App.User = null;
            this.DeleteFromLocalDb();
            await Navigation.PushAsync(new LoginPage());
	        DependencyService.Get<IMessage>().LongAlert("You've been successfully logged out");
        }

	    private void DeleteFromLocalDb()
	    {
	        localDb.DeleteUsers();	        
	    }
    }
}