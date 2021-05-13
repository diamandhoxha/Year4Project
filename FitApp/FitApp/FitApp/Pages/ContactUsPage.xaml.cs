using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUsPage : ContentPage
    {
        public ContactUsPage()
        {
            InitializeComponent();
        }

        private async void TapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void BtnCall_Clicked(object sender, EventArgs e)
        {
            PhoneDialer.Open("087 923 2345");
        }

        private void BtnEmail_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EmailPage());
        }

        private void BtnSMS_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SMSPage());
        }
    }
}