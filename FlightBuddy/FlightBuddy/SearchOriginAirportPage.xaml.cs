using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.FlightSearchApi;
using FlightBuddy.Model;
using FlightBuddy.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Flight = FlightBuddy.LocalDb.Flight;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchOriginAirportPage : ContentPage
	{
		public SearchOriginAirportPage ()
		{
			InitializeComponent ();
		    localDb = new LocalDb.LocalDb();
            airportsApi = new AirportsApi();
            this.GetListOfAirports();
		    originAirportSearch.Focus();
        }

	    private readonly IAirportsApi airportsApi;

	    private readonly LocalDb.LocalDb localDb;

        private IEnumerable<Model.Airport> Airports { get; set; }

	    private async void GetListOfAirports()
	    {
	        this.Airports = await this.airportsApi.GetAirports();
	    }

	    private void SearchAirports_TextChanged(object sender, EventArgs e)
	    {
	        var keyword = originAirportSearch.Text;
	        var suggestions = this.Airports.Where(a => a.Name.ToLower().Contains(keyword.ToLower()));
            airportSuggestions.ItemsSource = suggestions;
	    }

	    private async void SetOriginAirport(object sender, ItemTappedEventArgs e)
	    {
	        var itemTapped = e.Item as Airport;
	        var flight = this.localDb.GetFlight();
	        if (flight != null)
	        {
	            flight.OriginCode = itemTapped.Code;
	            flight.Origin = itemTapped.Name;
                this.localDb.UpdateFlight(flight);
	            await Navigation.PushAsync(new FlightSearchPage());
	        }
	        else
	        {
                var localFlight = new LocalDb.Flight
                {
                    OriginCode = itemTapped.Code,
                    Origin = itemTapped.Name
                };
                this.localDb.AddFlight(localFlight);
	            await Navigation.PushAsync(new FlightSearchPage());
            }

        }


    }
}