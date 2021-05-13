using FitApp.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        
        private async void ImgLogin_Tapped(object sender, EventArgs e)
        {
            // if users access token is expired will refresh the access token and log the user in again
            var response = await ApiServices.Login(EntEmail.Text, EntPassword.Text);
            Preferences.Set("email", EntEmail.Text);
            Preferences.Set("password", EntPassword.Text);

            //user login being done
            if (!response)
            {
                //display alert
                await DisplayAlert("Alert", "Information entered incorrectly", "Cancel");
            }
            else
            {
                // maknig the home page the new main page
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
        }

        private void LblSignUp_Tapped(object sender, EventArgs e)
        {
            //adds plage on top of navigation stack
            Navigation.PushAsync(new SignUpPage());
        }
    }
}