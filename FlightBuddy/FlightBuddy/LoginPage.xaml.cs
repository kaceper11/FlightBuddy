using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using FlightBuddy.ToastNotification;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using System.Reactive.Linq;
using Akavache;

namespace FlightBuddy
{
    public partial class LoginPage : ContentPage
    {      
        public LoginPage()
        {
            InitializeComponent(); 
            db = new FlightBuddyContext.FlightBuddyContext();

            //BlobCache.UserAccount.GetObject<User>("User")
            //    .Subscribe(x => User = x);
            //if (this.User != null)
            //{
            //    App.User = this.User;
            //    Navigation.PushAsync(new HomePage());
            //}
        }

        private FlightBuddyContext.FlightBuddyContext db;

        private User User { get; set; }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {            
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (!isEmailEmpty || !isPasswordEmpty)
            {
                var user = await this.db.GetUserByEmail(emailEntry.Text);

                if (user != null)
                {
                    if (user.Password == passwordEntry.Text)
                    {
                        App.User = user;
                        //await BlobCache.UserAccount.Invalidate("User");
                        await BlobCache.UserAccount.InsertObject("User", user, DateTimeOffset.Now.AddDays(7));
                        BlobCache.UserAccount.GetObject<User>("User")
                            .Subscribe(x => this.User = x, ex => Console.WriteLine("No Key!"));
                        await Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Email or password are incorrect");
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("There was an error logging you in");
                }
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("Email and password can't be empty");
            }
            
        }

        private void RegisterUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
