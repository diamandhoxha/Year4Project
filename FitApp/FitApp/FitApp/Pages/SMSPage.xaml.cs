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
    public partial class SMSPage : ContentPage
    {
        public SMSPage()
        {
            InitializeComponent();
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            try
            {
                var number = "0873601227";
                await Sms.ComposeAsync(new SmsMessage(EntBody.Text, number));
            }
            catch(Exception)
            {

            }
            
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}