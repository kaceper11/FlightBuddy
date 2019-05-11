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

	    protected override void OnAppearing()
	    {
            base.OnAppearing();
	        flightsListView.ItemsSource = this.db.GetUserFlights(App.User.Id);
	    }
	}
}