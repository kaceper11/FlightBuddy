using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.FlightSearchApi;
using FlightBuddy.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoundedFlightsPage : ContentPage
	{
		public FoundedFlightsPage (string originCode, string destinationCode, string date)
		{
			InitializeComponent ();
		    this.OriginCode = originCode;
		    this.DestinationCode = destinationCode;
		    this.Date = date;
            flightsApi = new FlightsApi();
		}

        private string OriginCode { get; set; }

	    private string DestinationCode { get; set; }

	    private string Date { get; set; }

	    private readonly IFlightsApi flightsApi;

	    protected override async void OnAppearing()
	    {
	        foundedFlightsListView.ItemsSource = await flightsApi.GetFlights(this.OriginCode, this.DestinationCode, this.Date);	       
	    }

	    private async void FoundedFlight_Clicked(object sender, ItemTappedEventArgs e)
	    {
	        var itemTapped = e.Item as FlightViewModel;
	        await Navigation.PushAsync(new FlightsPage());
	    }
    }
}