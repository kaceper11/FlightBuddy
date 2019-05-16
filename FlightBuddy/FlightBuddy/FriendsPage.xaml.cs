using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendsPage : ContentPage
	{
		public FriendsPage ()
		{
			InitializeComponent ();
            db = new FlightBuddyContext.FlightBuddyContext();
		    this.BindingContext = this;
        }

	    private readonly FlightBuddyContext.FlightBuddyContext db;

        protected  override async void OnAppearing()
        {
            base.OnAppearing();
            this.IsBusy = true;
            friendsListView.ItemsSource = await this.db.GetUserFriends(App.User.Id);
            this.IsBusy = false;
        }

        private async void UserFriend_Clicked(object sender, ItemTappedEventArgs e)
        {
            var itemTapped = e.Item as UserFriendViewModel;
            await Navigation.PushAsync(new UserFriendProfilePage(itemTapped.Id));
        }


    }
}