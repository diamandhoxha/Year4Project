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
    public partial class AddWorkoutPage : ContentPage
    {
        public AddWorkoutPage()
        {
            InitializeComponent();
        }

        private async void ImgBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ImgSave_Tapped(object sender, EventArgs e)
        {
            var workout = new WorkoutDetail()
            {
                UserId = Preferences.Get("userId", 0),
                WorkoutName = EntWorkoutName.Text,
                Sets = Convert.ToInt32(EntSets.Text),
                Reps = Convert.ToInt32(EntReps.Text),
                Date = DateTime.UtcNow
            };
            var response = await ApiServices.AddWorkout(workout);

            if (response)
            {
                await DisplayAlert("", "Your workout has been added", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
        }
    }
}