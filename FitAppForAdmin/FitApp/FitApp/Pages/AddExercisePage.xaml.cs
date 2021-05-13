using FitApp.Models;
using FitApp.Services;
using ImageToArray;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AssExercisePage : ContentPage
    {

        private MediaFile file;

        public AssExercisePage()
        {
            InitializeComponent();
        }


        private async void TapPickImage_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Sorry", "Your device doesn't support this feature.", "OK");
                return;
            }

            file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
            {
                return;
            }

            ImgExercise.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void ImgSave_Tapped(object sender, EventArgs e)
        {
            var imageArray = FromFile.ToArray(file.GetStream());
            var exercise = new ExerciseDetail()
            {
                ExerciseName = EntExerciseName.Text,
                Description = EntDescription.Text,
                Difficulty = EntDifficulty.Text,
                TrailorUrl = EntTrailorUrl.Text,
                ImageArray = imageArray
            };

            var response = await ApiServices.AddExercise(file, exercise);

            if (!response)
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
            else
            {
                await DisplayAlert("", "exercise has been added", "Ok");
                await Navigation.PopModalAsync();
            }
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}