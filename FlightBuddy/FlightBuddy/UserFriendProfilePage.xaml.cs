﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Model;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserFriendProfilePage : ContentPage
	{
        public UserFriendProfilePage(string userId)
        {
            InitializeComponent();
            db = new FlightBuddyContext.FlightBuddyContext();
            this.UserFriendId = userId;
            this.BindingContext = this;
        }

        private readonly FlightBuddyContext.FlightBuddyContext db;

        private string  UserFriendId { get; set; }

        private User UserFriend { get; set; }

        protected override async void OnAppearing()
        {
            this.IsBusy = true;
            base.OnAppearing();

            if (App.CheckConnectvity())
            {
                this.SetUserProfilePicture(this.UserFriendId);
                var userFriend = await db.GetUserById(this.UserFriendId);
                userFriendName.Text = userFriend.Name;
                userFriendEmail.Text = userFriend.Email;
                userFriendMobileNumber.Text = userFriend.MobileNumber ?? " - ";
                userFriendBio.Text = userFriend.Bio ?? " - ";
                this.UserFriend = userFriend;
                commonFlightsListView.ItemsSource = await this.db.GetCommonFlights(App.User.Id, userFriend.Id);

                bool isUserMobileNullOrEmpty = string.IsNullOrWhiteSpace(userFriend.MobileNumber);

                if (isUserMobileNullOrEmpty)
                {
                    sendSmsButton.IsVisible = false;
                    makeCallButton.IsVisible = false;
                }
            }
            else
            {
                userFriendName.Text = string.Empty;
                userFriendEmail.Text = string.Empty;
                userFriendMobileNumber.Text = string.Empty;
                userFriendBio.Text = string.Empty;
                commonFlightsListView.ItemsSource = null;

                sendSmsButton.IsVisible = false;
                makeCallButton.IsVisible = false;
                sendEmailButton.IsVisible = false;
            }

            this.IsBusy = false;
        }

	    private void SendEmailButton_Clicked(object sender, EventArgs e)
	    {
	        var emailMessenger = CrossMessaging.Current.EmailMessenger;
	        if (emailMessenger.CanSendEmail)
	        {
	            var email = new EmailMessageBuilder()
	                .To(this.UserFriend.Email)
	                .Subject("FlightBuddy - Common flight ")
	                .Body($"Hey! This is {App.User.Name} from FlightBuddy :D")
	                .Build();

	            emailMessenger.SendEmail(email);
	        }
        }

	    private void MakeCallButton_Clicked(object sender, EventArgs e)
	    {
	        var phoneDialer = CrossMessaging.Current.PhoneDialer;
	        if (phoneDialer.CanMakePhoneCall)
	            phoneDialer.MakePhoneCall(this.UserFriend.MobileNumber);
        }

	    private void SendSmsButton_Clicked(object sender, EventArgs e)
	    {
	        var smsMessenger = CrossMessaging.Current.SmsMessenger;
	        if (smsMessenger.CanSendSms)
	            smsMessenger.SendSms(this.UserFriend.MobileNumber, $"Hey! This is {App.User.Name} from FlightBuddy :D");
        }

	    private async void SetUserProfilePicture(string userId)
	    {
	        var image = await this.db.GetUsersProfilePicture(userId);
	        bool isImageNotNull = !string.IsNullOrEmpty(image);
	        if (isImageNotNull)
	        {
	            profileImage.Source = image;
	        }
	        else
	        {
	            profileImage.Source = string.Empty;
	        }
	    }
    }
}