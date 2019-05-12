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
	public partial class SearchOriginAirportPage : ContentPage
	{
		public SearchOriginAirportPage ()
		{
			InitializeComponent ();
		    airportsApi = new AirportsApi();
            this.GetListOfAirports();
        }

	    private readonly IAirportsApi airportsApi;

        private IEnumerable<Model.Airport> Airports { get; set; }

	    private async void GetListOfAirports()
	    {
	        this.Airports = await this.airportsApi.GetAirports();
	    }

	    private async void SearchAirports_Clicked(object sender, EventArgs e)
	    {
	        var keyword = originAirportSearch.Text;
            //var listOfAirports = await this.airportsApi.GetAirports();
            //var suggestions = listOfAirports.Where(a => a.Name.ToLower().Contains(keyword.ToLower()));
	        var suggestions = this.Airports.Where(a => a.Name.ToLower().Contains(keyword.ToLower()));
            airportSuggestions.ItemsSource = suggestions;

	    }
    }
}