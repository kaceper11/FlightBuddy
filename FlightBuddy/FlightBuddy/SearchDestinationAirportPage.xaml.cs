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
	public partial class SearchDestinationAirportPage : ContentPage
	{

	    public SearchDestinationAirportPage()
	    {
	        InitializeComponent();
	        airportsApi = new AirportsApi();
            localDb = new LocalDb.LocalDb();
	        this.GetListOfAirports();
	    }

	    private readonly IAirportsApi airportsApi;

	    private readonly LocalDb.LocalDb localDb;

	    private IEnumerable<Model.Airport> Airports { get; set; }

	    private async void GetListOfAirports()
	    {
	        this.Airports = await this.airportsApi.GetAirports();
	    }

	    private async void SearchAirports_Clicked(object sender, EventArgs e)
	    {
	        var keyword = destinationAirportSearch.Text;
	        //var listOfAirports = await this.airportsApi.GetAirports();
	        //var suggestions = listOfAirports.Where(a => a.Name.ToLower().Contains(keyword.ToLower()));
	        var suggestions = this.Airports.Where(a => a.Name.ToLower().Contains(keyword.ToLower()));
	        airportSuggestions.ItemsSource = suggestions;

	    }

	    private void SetOriginAirport(object sender, EventArgs e)
	    {
	        if (this.localDb.CheckIfFlightEmpty())
	        {

	        }

	    }
        
    }
}