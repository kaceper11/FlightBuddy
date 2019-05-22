using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlightBuddy.Auth;
using FlightBuddy.Model;
using FlightBuddy.ToastNotification;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FacebookProfilePage : ContentPage
	{
	    private const string ClientId = "440992143336020";

	    private readonly FlightBuddyContext.FlightBuddyContext db;

        public FacebookProfilePage ()
		{
			InitializeComponent ();
            db = new FlightBuddyContext.FlightBuddyContext();

            var apiRequest =
                "https://www.facebook.com/v3.3/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

	    private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = this.ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != string.Empty)
            {
                var userFb = await GetFacebookProfileAsync(accessToken);

                if (userFb != null)
                {
                    var userDb = new User()
                    {
                        Email = userFb.Email,
                        Password = userFb.Email,
                        MobileNumber = null,
                        Name = userFb.Name,
                        ProfilePictureUrl = userFb.Picture.Data.Url,
                        FacebookId = userFb.Id
                    };

                    var userToBeAdded = await this.db.GetUserByEmail(userFb.Email);

                    if (userToBeAdded != null)
                    {
                        App.User = userToBeAdded;
                        await Navigation.PushAsync(new HomePage(true));
                    }
                    else
                    {
                        this.db.AddUser(userDb);
                        App.User = userDb;
                        await Navigation.PushAsync(new HomePage(true));
                    }
                }
            }
        }
            
        

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                var accessToken = at.Remove(at.IndexOf("&data_access_expiration_time="));

                return accessToken;
            }

            return string.Empty;
        }

        private async Task<FacebookProfileModel> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v3.3/me/?fields=name,picture,age_range,birthday,email,first_name,last_name,gender,hometown,is_verified&access_token="
                             + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            return JsonConvert.DeserializeObject<FacebookProfileModel>(userJson);
        }
    }
}
