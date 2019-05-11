using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		}

	    private readonly FlightBuddyContext.FlightBuddyContext db;

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        friendsListView.ItemsSource = this.db.GetUserFriends(App.User.Id);
	    }
    }
}