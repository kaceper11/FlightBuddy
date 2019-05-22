using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	        if (App.CheckConnectvity())
	        {
	            bool isEmailFilledIn = !string.IsNullOrWhiteSpace(emailEntry.Text);
	            bool isNameFilledIn = !string.IsNullOrWhiteSpace(nameEntry.Text);


                if (isEmailFilledIn && isNameFilledIn)
	            {
	                if (IsPhoneNumber(mobileNumberEntry.Text) && IsValidEmail(emailEntry.Text))
	                {
	                    App.User.Email = emailEntry.Text;
	                    App.User.Name = nameEntry.Text;
	                    App.User.MobileNumber = mobileNumberEntry.Text;
	                    App.User.Bio = bioEntry.Text;

	                    this.db.UpdateUser(App.User);
	                    await Navigation.PopAsync();
	                    DependencyService.Get<IMessage>().LongAlert("Profile has been updated");
	                }
	                else
	                {
	                    DependencyService.Get<IMessage>().LongAlert("Mobile number or email address are not valid");
                    }
	            }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Name and email can't be empty");
                }
	        }

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
	            mobileNumberEntry.Text = currentUser.MobileNumber ??  string.Empty;
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

	    private static bool IsPhoneNumber(string number)
	    {
	        return Regex.Match(number, @"^\+[1-9]{1}[0-9]{3,14}$").Success;
	    }

	    private bool IsValidEmail(string email)
	    {
	        try
	        {
	            var addr = new System.Net.Mail.MailAddress(email);
	            return addr.Address == email;
	        }
	        catch
	        {
	            return false;
	        }
	    }
    }
}