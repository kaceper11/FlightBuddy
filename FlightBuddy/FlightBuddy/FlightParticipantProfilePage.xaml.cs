﻿using System;
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
	public partial class FlightParticipantProfilePage : ContentPage
	{
		public FlightParticipantProfilePage (string userId)
		{
			InitializeComponent ();
		    db = new FlightBuddyContext.FlightBuddyContext();
		    this.UserFriendId = userId;
        }
	    private readonly FlightBuddyContext.FlightBuddyContext db;

	    private string UserFriendId { get; set; }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        var userFriend = await this.db.GetUserById(this.UserFriendId);
	        flightParticipantName.Text = userFriend.Name;
	        flightParticipantBio.Text = userFriend.Bio;
	        profileImage.Source = userFriend.ProfilePictureUrl;
	    }

        private async void LikeButton_Clicked(object sender, EventArgs e)
	    {
            var userFriend = new UserFriend()
            {
                FriendId = this.UserFriendId,
                UserId = App.User.Id
            };
	        if (await this.db.CheckIfUserFriendExists(userFriend))
	        {
	            this.db.AddUserFriend(userFriend);
	            await Navigation.PopAsync();
	            DependencyService.Get<IMessage>().LongAlert("User has been liked");
            }
	        else
	        {
	            DependencyService.Get<IMessage>().LongAlert("You have already liked that user");
            }       
	    }
	}
}