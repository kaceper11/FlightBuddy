using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.FlightSearchApi;
using FlightBuddy.ToastNotification;
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
            this.localDb = new LocalDb.LocalDb();
		    this.BindingContext = this;
		}

	    protected override void OnAppearing()
	    {
	        this.IsBusy = true;
            this.SetSearchParameters();
	        this.IsBusy = false;
	    }

	    private readonly LocalDb.LocalDb localDb;

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            var flight = localDb.GetFlight();
            var isFlightOriginCodeEmpty = !string.IsNullOrEmpty(flight.OriginCode);
            var isFlightDestinationCodeEmpty = !string.IsNullOrEmpty(flight.DestinationCode);

            if (isFlightOriginCodeEmpty && isFlightDestinationCodeEmpty)
            {
                await Navigation.PushAsync(new FoundedFlightsPage(flight.OriginCode, flight.DestinationCode,
	            datePicker.Date.ToString("yyyy-MM-dd")));
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("Search parameters can't be empty");
            }
            
	    }

	    private async void OpenOriginAiportSearch(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new SearchOriginAirportPage());
	    }

	    private async void OpenDestinationAiportSearch(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new SearchDestinationAirportPage());
	    }

	    private void SetSearchParameters()
	    {
	        var flight = this.localDb.GetFlight();
	        if (flight !=  null)
	        {
	            var isFlightOriginEmpty = !string.IsNullOrEmpty(flight.Origin);
	            var isFlightDestinationEmpty = !string.IsNullOrEmpty(flight.Destination);

	            if (isFlightOriginEmpty)
	            {
	                originEntry.Text = flight.Origin;
	            }
	            if (isFlightDestinationEmpty)
	            {
	                destinationEntry.Text = flight.Destination;
	            }
            }
        }

    }
}