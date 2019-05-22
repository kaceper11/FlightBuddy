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
	public partial class ChangePasswordPage : ContentPage
	{
		public ChangePasswordPage ()
		{
			InitializeComponent ();
            db = new FlightBuddyContext.FlightBuddyContext();
		}

	    private readonly FlightBuddyContext.FlightBuddyContext db;


        private async void SaveNewPassword_Clicked(object sender, EventArgs e)
        {
            var user = await this.db.GetUserByEmail(App.User.Email);

            if (user.Password == oldPasswordEntry.Text)
            {
                if (newPasswordEntry.Text == confirmNewPasswordEntry.Text)
                {
                    App.User.Password = newPasswordEntry.Text;
                    this.db.UpdateUser(App.User);
                    await Navigation.PopAsync();
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("New password doesn't match");
                }
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("Old password doesn't match current your password");
            }
        }
	}
}