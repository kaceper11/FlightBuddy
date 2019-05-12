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

           var user = await db.GetUserById(this.UserEmail);
            userFriendName.Text = user.Name;
            userFriendEmail.Text = user.Email;
            userFriendMobileNumber.Text = user.MobileNumber;
        }
    }
}