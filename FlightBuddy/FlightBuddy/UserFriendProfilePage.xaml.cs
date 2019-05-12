using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserFriendProfilePage : ContentPage
	{
		//public UserFriendProfilePage ()
		//{
		//	InitializeComponent ();
  //          db = new FlightBuddyContext.FlightBuddyContext();
		//}

        public UserFriendProfilePage(string email)
        {
            InitializeComponent();
            db = new FlightBuddyContext.FlightBuddyContext();
            this.UserEmail = email;
        }


        private readonly FlightBuddyContext.FlightBuddyContext db;

        private string  UserEmail { get; set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

           var user = await db.GetUserByEmail(this.UserEmail);
            userFriendEmail.Text = user.Email;//"kacper gg";
        }
    }
}