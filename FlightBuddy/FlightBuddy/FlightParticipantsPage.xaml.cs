using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using FlightBuddy.ToastNotification;
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
		    this.BindingContext = this;
		}

        private readonly FlightBuddyContext.FlightBuddyContext db;

        private string FlightId { get; set; }

        protected override async void OnAppearing()
        {
            this.IsBusy = true;

            base.OnAppearing();
            var participants = await this.db.GetFlightParticipants(this.FlightId);
            if (participants.Any())
            {
                flightParticipantsListView.ItemsSource = participants;
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("There are no participants for this flight");
                await Navigation.PopAsync();
            }

            this.IsBusy = false;

        }

        private async void OpenFlightParticipants_Clicked(object sender, ItemTappedEventArgs e)
        {
            var itemTapped = e.Item as UserFriendViewModel;
            await Navigation.PushAsync(new FlightParticipantProfilePage(itemTapped.Id));
        }
    }
}