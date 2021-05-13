using FitApp.Models;
using FitApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutDetailPage : ContentPage
    {
        public WorkoutDetailPage(int workoutId)
        {
            InitializeComponent();
            GetWorkoutDetail(workoutId);
            DeleteWorkout(workoutId);
        }

        private async void DeleteWorkout(int workoutId)
        {
            var result = await DisplayAlert("Alert", "Do you want to delete this workout", "Yes", "No");
            if (result)
            {
                var response = await ApiServices.DeleteWorkout(workoutId);
                if (response == false)
                {
                    return;
                }
                else
                {
                    await Navigation.PopModalAsync();
                }
            }
        }

        private async void GetWorkoutDetail(int workoutId)
        {
                var workout = await ApiServices.GetWorkoutDetail(workoutId);
                LblWorkoutName.Text = workout.WorkoutName;
                LblReps.Text = workout.Reps.ToString();
                LblSets.Text = workout.Sets.ToString();
                LblDate.Text = workout.Date.ToString("MMMM d, yyyy HH:mm");
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}