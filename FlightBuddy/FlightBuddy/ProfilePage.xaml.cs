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
	    }

	    private readonly FlightBuddyContext.FlightBuddyContext db;

	    private readonly LocalDb.LocalDb localDb;

        protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        var user = await this.db.GetUserByEmail(App.User.Email);
	        userEmail.Text = user.Email;
	        userMobileNumber.Text = user.MobileNumber;
	        userName.Text = user.Name;
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

	        selectedProfileImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());

	        UploadImage(selectedImageFile.GetStream());
	    }

	    private async void UploadImage(Stream stream)
	    {
	        var account = CloudStorageAccount.Parse(
	            "DefaultEndpointsProtocol=https;AccountName=flightbuddystorage;AccountKey=WTXwA6tF6pr7ziQlWrCwJ+h2YNFomhxm3sr6OAA+udn9Y62pizWnZYZb1xnV6OPVpQM4yNdhZvY+l8cx/nRpZQ==;EndpointSuffix=core.windows.net");

	        var client = account.CreateCloudBlobClient();
	        var container = client.GetContainerReference("imagecontainer");
	        await container.CreateIfNotExistsAsync();

	        var name = Guid.NewGuid().ToString();
	        var blockBlob = container.GetBlockBlobReference($"{name}.jpg");

	        await blockBlob.UploadFromStreamAsync(stream);
	        string url = blockBlob.Uri.OriginalString;

	    }
	}
}