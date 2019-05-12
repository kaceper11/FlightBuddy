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
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
            db = new FlightBuddyContext.FlightBuddyContext();
		    localDb = new LocalDb.LocalDb();
        }

	    private readonly FlightBuddyContext.FlightBuddyContext db;

	    private readonly LocalDb.LocalDb localDb;

        private async void RegisterButton_Clicked(object sender, EventArgs e)
	    {
	        bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
	        bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
	        bool isConfirmPasswordEmpty = string.IsNullOrEmpty(confirmPasswordEntry.Text);
            bool isNameEmpty = string.IsNullOrEmpty(nameEntry.Text);	        
	        bool isMobileEmpty = string.IsNullOrEmpty(mobileNumberEntry.Text);

	        if (!isEmailEmpty && !isPasswordEmpty && !isConfirmPasswordEmpty && !isNameEmpty && !isMobileEmpty)
	        {
	            if (passwordEntry.Text == confirmPasswordEntry.Text)
	            {
	                var user = new User()
	                {
	                    Email = emailEntry.Text,
                        Password = passwordEntry.Text,
	                    Name = nameEntry.Text,
	                    MobileNumber = mobileNumberEntry.Text,
	                };

	                var userDb = await this.db.GetUserByEmail(emailEntry.Text);

	                if (userDb != null)
	                {
	                    DependencyService.Get<IMessage>().LongAlert("User with that email already exists");
                    }
	                else
	                {
	                    this.db.AddUser(user);
                        App.User = user;
                        await Navigation.PushAsync(new HomePage());
	                }
                }
	            else
	            {
	                DependencyService.Get<IMessage>().LongAlert("Passwords don't match");
                }
            }
	        else
	        {
	            DependencyService.Get<IMessage>().LongAlert("Fields can't be empty");
            }

	    }

	    private void LoginUserButton_Clicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new LoginPage());
	    }


    }
}