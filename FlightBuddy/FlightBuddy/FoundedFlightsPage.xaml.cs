using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.FlightSearchApi;
using FlightBuddy.Model;
using FlightBuddy.ToastNotification;
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
            db = new FlightBuddyContext.FlightBuddyContext();
		}

        private string OriginCode { get; set; }

	    private string DestinationCode { get; set; }

	    private string Date { get; set; }

	    private readonly IFlightsApi flightsApi;

	    private readonly FlightBuddyContext.FlightBuddyContext db;

        protected override async void OnAppearing()
	    {
            var result = await flightsApi.GetFlights(this.OriginCode, this.DestinationCode, this.Date);
	        if (result.Any())
	        {
	            foundedFlightsListView.ItemsSource = result;
	        }
	        else
	        {
	            await Navigation.PopAsync();
	            DependencyService.Get<IMessage>().LongAlert("No flights were found for given parameters");
            }
                
	    }

	    private async void FoundedFlight_Clicked(object sender, ItemTappedEventArgs e)
	    {
	        var itemTapped = e.Item as FlightViewModel;
	        await Navigation.PushAsync(new FlightsPage());
	    }

	    private async void OpenFlightDetails(object sender, ItemTappedEventArgs e)
	    {
	        var itemTapped = e.Item as Model.Flight;

	        if (await this.db.CheckIfFlightExists(itemTapped))
	        {
	            db.AddFlight(itemTapped);
	        }

	        var userFlight = new UserFlight()
	        {
                FlightId = await this.db.GetFlightIdByDetails(itemTapped),
                UserId = App.User.Id
	        };

            db.AddUserFlight(userFlight);
        }
	}
}