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
    public partial class EmailPage : ContentPage
    {
        public EmailPage()
        {
            InitializeComponent();
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            var email = "Diamand456@gmail.com";
            var message = new EmailMessage(EntSubject.Text, EntBody.Text, email);
            message.BodyFormat = EmailBodyFormat.PlainText;
            await Email.ComposeAsync(message);
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}