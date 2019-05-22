using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.ToastNotification;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
	    public ProfilePage()
	    {
	        InitializeComponent();
            db = new FlightBuddyContext.FlightBuddyContext();
            localDb = new LocalDb.LocalDb();
	        this.BindingContext = this;
	    }

	    private readonly FlightBuddyContext.FlightBuddyContext db;

	    private readonly LocalDb.LocalDb localDb;

        protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        this.IsBusy = true;
	        if (App.CheckConnectvity())
	        {
	            var user = await this.db.GetUserById(App.User.Id);
	            userEmail.Text = user.Email;
	            userMobileNumber.Text = user.MobileNumber ?? " - ";
	            userName.Text = user.Name;
	            userBio.Text = user.Bio ?? " - ";
	            SetUserProfilePicture();
	        }
	        else
	        {
	            userEmail.Text = string.Empty;
	            userMobileNumber.Text = string.Empty;
                userName.Text = string.Empty;
                userBio.Text = string.Empty;
            }

	        this.IsBusy = false;
	    }

	    private async void LogoutButton_Clicked(object sender, EventArgs e)
	    {
	        App.User = null;
            this.DeleteFromLocalDb();
            await Navigation.PushAsync(new LoginPage());
	        DependencyService.Get<IMessage>().LongAlert("You've been successfully logged out");
        }

	    private void DeleteFromLocalDb()
	    {
	        localDb.DeleteUsers();	
            localDb.DeleteFlights();
	    }

	    private async void PictureButton_Clicked(object sender, EventArgs e)
	    {
	        await CrossMedia.Current.Initialize();

	        if (!CrossMedia.Current.IsPickPhotoSupported)
	        {
	            DependencyService.Get<IMessage>().LongAlert("This is not supported on your device");
	            return;
	        }

	        var mediaOptions = new PickMediaOptions()
	        {
	            PhotoSize = PhotoSize.Medium
	        };

	        var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

	        if (selectedImageFile == null)
	        {
	            DependencyService.Get<IMessage>().LongAlert("There was a error when trying to get your image");
                return;
            }


	        await UploadImage(selectedImageFile.GetStream());
	    }

	    private async Task<string> UploadImage(Stream stream)
	    {
	        var account = CloudStorageAccount.Parse(
	            "DefaultEndpointsProtocol=https;AccountName=flightbuddystorage;AccountKey=WTXwA6tF6pr7ziQlWrCwJ+h2YNFomhxm3sr6OAA+udn9Y62pizWnZYZb1xnV6OPVpQM4yNdhZvY+l8cx/nRpZQ==;EndpointSuffix=core.windows.net");

	        var client = account.CreateCloudBlobClient();
	        var container = client.GetContainerReference("imagecontainer");
	        await container.CreateIfNotExistsAsync();

	        var name = Guid.NewGuid().ToString();
	        var blockBlob = container.GetBlockBlobReference($"{name}.jpg");

	        await blockBlob.UploadFromStreamAsync(stream);
	        this.UpdateUserProfilePicture(blockBlob.Uri.OriginalString);

	        selectedProfileImage.Source = blockBlob.Uri.OriginalString;
            App.User.ProfilePictureUrl = blockBlob.Uri.OriginalString;

            return blockBlob.Uri.OriginalString;

	    }

	    private async void SetUserProfilePicture()
	    {
	        if (selectedProfileImage.Source == null)
	        {
	            var image = await this.db.GetUsersProfilePicture(App.User.Id);
	            bool isImageNotNull = !string.IsNullOrEmpty(image);
	            if (isImageNotNull)
	            {
	                selectedProfileImage.Source = image;
	                App.User.ProfilePictureUrl = image;
	            }
	            else
	            {
	                selectedProfileImage.Source = string.Empty;
	            }
	        }	      

	    }

	    private async void UpdateUserProfilePicture(string pictureUrl)
	    {
	        var user = await this.db.GetUserById(App.User.Id);
	        user.ProfilePictureUrl = pictureUrl;
            this.db.UpdateUser(user);
	    }

	    private async void  UpdateProfileButton_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new UpdateProfilePage());
	    }

	    private async void ChangePasswordButton_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new ChangePasswordPage());
	    }
	}
}