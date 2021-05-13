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
    public partial class ExerciseDetailPage : ContentPage
    {
        private ExerciseDetail exercise;
        public ExerciseDetailPage(int exerciseId)
        {
            InitializeComponent();
            GetExerciseDetail(exerciseId);
        }

        private async void GetExerciseDetail(int exerciseId)
        {
            exercise = await ApiServices.GetExerciseDetail(exerciseId);
            LblExerciseName.Text = exercise.ExerciseName;
            LblDifficulty.Text = exercise.Difficulty;
            LblExerciseDescription.Text = exercise.Description;
            ImgExercise.Source = exercise.FullImageUrl;
            ImgExerciseCover.Source = exercise.FullImageUrl;
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void TapVideo_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VideoPlayerPage(exercise.TrailorUrl));
        }
    }
}