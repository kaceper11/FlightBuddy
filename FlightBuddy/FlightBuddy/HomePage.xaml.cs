using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlightBuddy
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : TabbedPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		    localDb = new LocalDb.LocalDb();
            this.AddToLocalDb();
        }

	    private readonly LocalDb.LocalDb localDb;

	    private void AddToLocalDb()
	    {
	        var localUser = new LocalDb.User()
	        {
	            UserId = App.User.Id,
	            Email = App.User.Email,
	            MobileNumber = App.User.MobileNumber,
	            Name = App.User.Name,
	            Password = App.User.Password
	        };
	        localDb.DeleteUsers();
	        localDb.AddUser(localUser);
	    }
    }
}