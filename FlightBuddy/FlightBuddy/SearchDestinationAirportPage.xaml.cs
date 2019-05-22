using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.FlightSearchApi;
using FlightBuddy.LocalDb;
using FlightBuddy.Model;
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
	        
	        this.BindingContext = this;
	    }

	    private readonly IAirportsApi airportsApi;

	    private readonly LocalDb.LocalDb localDb;

	    private IEnumerable<Model.Airport> Airports { get; set; }

	    protected override async void OnAppearing()
	    {
	        this.IsBusy = true;
	        if (App.CheckConnectvity())
	        {
	            this.GetListOfAirports();
	        }
	        else
	        {
	            this.Airports = null;
	        }
            
	        destinationAirportSearch.Focus();

            this.IsBusy = false;
	    }

	    private async void GetListOfAirports()
	    {
	        this.Airports = await this.airportsApi.GetAirports();
	    }

	    private  void SearchAirports_TextChanged(object sender, EventArgs e)
	    {
	        var keyword = destinationAirportSearch.Text;
	        var suggestions = this.Airports.Where(a => a.Name.ToLower().Contains(keyword.ToLower()));
	        airportSuggestions.ItemsSource = suggestions;
	    }

	    private async void SetDestinationAirport(object sender, ItemTappedEventArgs e)
	    {
	        var itemTapped = e.Item as Airport;
	        var flight = this.localDb.GetFlight();
	        if (flight != null)
	        {
	            flight.DestinationCode = itemTapped.Code;
	            flight.Destination = itemTapped.Name;
	            this.localDb.UpdateFlight(flight);
	            await Navigation.PopAsync();
	        }	      
	        else
	        {
                var localFlight = new LocalDb.Flight()
                {
                    DestinationCode = itemTapped.Code,
                    Destination = itemTapped.Name             
                };
                this.localDb.AddFlight(localFlight);
	            await Navigation.PopAsync();
            }
        }
        
    }
}