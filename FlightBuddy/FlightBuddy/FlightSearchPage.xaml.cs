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
		}

	    private readonly IFlightsApi flightsApi;

	    private async void SearchButton_Clicked(object sender, EventArgs e)
	    {

	        //searchFlightsListView.ItemsSource = await flightsApi.GetFlights(originCodeEntry.Text, destinationCodeEntry.Text,
	        //    datePicker.Date.ToString("yyyy-MM-dd"));
	        await Navigation.PushAsync(new FoundedFlightsPage(originCodeEntry.Text, destinationCodeEntry.Text,
	            datePicker.Date.ToString("yyyy-MM-dd")));

	    }
    }
}