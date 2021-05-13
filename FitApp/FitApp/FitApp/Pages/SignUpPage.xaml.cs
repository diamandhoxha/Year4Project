using FitApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        async void ImgSignup_Tapped(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await ApiServices.RegisterUser(EntName.Text,EntEmail.Text, EntPassword.Text, Convert.ToDouble(EntWeight.Text), Convert.ToDouble(EntHeight.Text));
            if (!response)
            {
                await DisplayAlert("Alert", "Information entered incorrectly", "Cancel");
            }else
            {
                await DisplayAlert("Success", "Your account has been created", "Ok");
                await Navigation.PopToRootAsync();
            }
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}