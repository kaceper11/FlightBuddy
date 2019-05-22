﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using FlightBuddy.ToastNotification;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace FlightBuddy
{
    public partial class LoginPage : ContentPage
    {      
        public LoginPage()
        {
            InitializeComponent(); 
            db = new FlightBuddyContext.FlightBuddyContext();
            localDb = new LocalDb.LocalDb();

            var assembly = typeof(LoginPage);
            iconImage.Source = ImageSource.FromResource("FlightBuddy.Assets.Images.app_icon.png", assembly);
        }

        private readonly FlightBuddyContext.FlightBuddyContext db;

        private readonly LocalDb.LocalDb localDb;

        private User User { get; set; }

        private bool Authenticated { get; set; } = false;

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {            
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (App.CheckConnectvity())
            {
                if (!isEmailEmpty || !isPasswordEmpty)
                {
                    var user = await this.db.GetUserByEmail(emailEntry.Text);

                    if (user != null)
                    {
                        if (user.Password == passwordEntry.Text)
                        {
                            App.User = user;
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

        }

        private void RegisterUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private async void LoginViaFacebookButton_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
                this.Authenticated = await App.Authenticator.Authenticate();

            // Set syncItems to true to synchronize the data on startup when offline is enabled.
            if (this.Authenticated)
                await Navigation.PushAsync(new HomePage());
        }
    }
}
