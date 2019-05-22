using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.ToastNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateProfilePage : ContentPage
	{
		public UpdateProfilePage ()
		{
			InitializeComponent ();
            db = new FlightBuddyContext.FlightBuddyContext();
		    this.BindingContext = this;
		}

	    private readonly FlightBuddyContext.FlightBuddyContext db;

	    private async void UpdateProfile_Clicked(object sender, EventArgs e)
	    {
	        App.User.Email = emailEntry.Text;
	        App.User.Name = nameEntry.Text;
	        App.User.Email = emailEntry.Text;
	        App.User.MobileNumber = mobileNumberEntry.Text;
	        App.User.Bio = bioEntry.Text;

            this.db.UpdateUser(App.User);
	        await Navigation.PopAsync();
            DependencyService.Get<IMessage>().LongAlert("Profile has been updated");
	    }

        protected override void OnAppearing()
        {
            this.IsBusy = true;

            base.OnAppearing();
            this.SetUserProfileData();

            this.IsBusy = false;
        }

	    private async void SetUserProfileData()
	    {
	        if (App.CheckConnectvity())
	        {
	            var currentUser = await this.db.GetUserById(App.User.Id);
	            nameEntry.Text = currentUser.Name;
	            emailEntry.Text = currentUser.Email;
	            mobileNumberEntry.Text = currentUser.MobileNumber;
	            bioEntry.Text = currentUser.Bio ?? string.Empty;
	        }
	        else
	        {
	            nameEntry.Text = string.Empty;
	            emailEntry.Text = string.Empty;
                mobileNumberEntry.Text = string.Empty;
                bioEntry.Text = string.Empty;
            }

	    }
	}
}