using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlightBuddy
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(loginEmailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(loginPasswordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {
                
            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
            
        }
    }
}
