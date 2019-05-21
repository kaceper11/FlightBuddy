using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using FlightBuddy.ToastNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyNewFlightPage : ContentPage
	{
		public MyNewFlightPage (Flight flight)
		{
			InitializeComponent ();
		    this.Flight = flight;
		    db = new FlightBuddyContext.FlightBuddyContext();
		    this.BindingContext = this;
		}

        private Flight Flight { get; set; }

	    private string FlightId { get; set; }

        private readonly FlightBuddyContext.FlightBuddyContext db;

        protected override async void OnAppearing()
        {
            this.IsBusy = true;
            this.FlightId = await this.db.GetFlightIdByFlightNumber(this.Flight);
            flightNumber.Text = this.Flight.FlightNumber;
            flightOrigin.Text = this.Flight.Origin;
            flightDestination.Text = this.Flight.Destination;
            flightArrivalTime.Text = this.Flight.ArriveTimeAirport.ToString("dd/MM/yyyy HH:mm");
            flightLeaveTime.Text = this.Flight.LeaveTimeAirport.ToString("dd/MM/yyyy HH:mm");
            flightAirline.Text = this.Flight.Airline;
            this.IsBusy = false;
        }

	    private async void AddToMyFlights_Clicked(object sender, EventArgs e)
	    {
            var userFlight = new UserFlight()
            {
                FlightId = this.FlightId,
                UserId = App.User.Id
            };

	        if (await this.db.CheckIfUserFlightExists(userFlight))
	        {
	            this.db.AddUserFlight(userFlight);
	            await Navigation.PushAsync(new FlightParticipantsPage(this.FlightId));
	            DependencyService.Get<IMessage>().LongAlert("Your flight has been added");
            }
	        else
	        {
	            DependencyService.Get<IMessage>().LongAlert("You have already added this flight to your flights");
	        }         
	    }
	}
}