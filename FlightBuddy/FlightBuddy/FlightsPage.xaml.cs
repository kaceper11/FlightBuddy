using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FlightsPage : ContentPage
	{
		public FlightsPage ()
		{
			InitializeComponent ();
            db = new FlightBuddyContext.FlightBuddyContext();
		}

	    private readonly FlightBuddyContext.FlightBuddyContext db;

	    protected override async void OnAppearing()
	    {
            base.OnAppearing();
	        flightsListView.ItemsSource = await this.db.GetUserFlights(App.User.Id);
	    }

        //private void flightsListView_Refreshing(object sender, EventArgs e)
        //{
        //    flightsListView.ItemsSource = this.db.GetUserFlights(App.User.Id);
        //    flightsListView.IsRefreshing = false;
	    //}

	    private async void UserFlight_Clicked(object sender, ItemTappedEventArgs e)
	    {
	        //var itemTapped = e.Item as UserFriendViewModel;
	        //await Navigation.PushAsync(new UserFriendProfilePage(itemTapped.Email));
	    }

    }
}