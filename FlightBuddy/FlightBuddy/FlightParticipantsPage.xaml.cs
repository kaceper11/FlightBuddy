using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using FlightBuddy.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FlightParticipantsPage : ContentPage
	{
		public FlightParticipantsPage (string flightId)
		{
			InitializeComponent ();
		    db = new FlightBuddyContext.FlightBuddyContext();
		    this.FlightId = flightId;
		}

        private readonly FlightBuddyContext.FlightBuddyContext db;

        private string FlightId { get; set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            flightParticipantsListView.ItemsSource = await this.db.GetFlightParticipants(App.User.Id);
        }

        private async void OpenFlightParticipants_Clicked(object sender, ItemTappedEventArgs e)
        {
            var itemTapped = e.Item as UserFriendViewModel;
            await Navigation.PushAsync(new FlightParticipantProfilePage(itemTapped.Id));
        }
    }
}