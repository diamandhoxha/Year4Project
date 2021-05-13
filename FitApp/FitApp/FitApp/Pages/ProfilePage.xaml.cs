using FitApp.Models;
using FitApp.Services;
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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            GetUserDetail();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetUserDetail();
        }

        private async void GetUserDetail()
        {
            
            var userId = Preferences.Get("userId", 0);
            var user = await ApiServices.GetUserDetail(userId);
            LblName.Text = user.Name;
            LblWeight.Text = user.Weight.ToString();
            LblHeight.Text = user.Height.ToString();
        }

        private async void ImgSave_Tapped(object sender, EventArgs e)
        {
            var userId = Preferences.Get("userId", 0);
            var user = new User()
            {
                Name = LblName.Text,
                Weight = Convert.ToInt32(LblWeight.Text),
                Height = Convert.ToInt32(LblHeight.Text),
            };
            var response = await ApiServices.UpdateUser(userId,user);

            if (response)
            {
                await DisplayAlert("", "Your information has been updated", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}