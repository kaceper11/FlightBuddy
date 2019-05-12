using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.FlightSearchApi;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FlightSearchPage : ContentPage
	{
		public FlightSearchPage ()
		{
			InitializeComponent ();
            this.flightsApi = new FlightsApi();
            this.localDb = new LocalDb.LocalDb();
		}

	    private readonly IFlightsApi flightsApi;

	    private readonly LocalDb.LocalDb localDb;

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            var flight = localDb.GetFlight();
	        await Navigation.PushAsync(new FoundedFlightsPage(flight.OriginCode, flight.DestinationCode,
	            datePicker.Date.ToString("yyyy-MM-dd")));
	    }

	    private async void OpenOriginAiportSearch(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new SearchOriginAirportPage());
	    }

	    private async void OpenDestinationAiportSearch(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new SearchDestinationAirportPage());
	    }

    }
}