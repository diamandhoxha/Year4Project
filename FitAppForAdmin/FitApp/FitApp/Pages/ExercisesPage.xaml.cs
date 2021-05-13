using FitApp.Models;
using FitApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisesPage : ContentPage
    {
        public ObservableCollection<Exercise> ExercisesCollection;
        private int pageNumber = 0;
        public ExercisesPage()
        {
            InitializeComponent();
            ExercisesCollection = new ObservableCollection<Exercise>();
            GetExercises();
        }

        private async void GetExercises()
        {
            pageNumber++;
            var exercises = await ApiServices.GetAllExercises(pageNumber, 6);
            foreach (var exercise in exercises)
            {
                ExercisesCollection.Add(exercise);
            }
            CvMovies.ItemsSource = ExercisesCollection;
        }

        private void CvExercises_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            GetExercises();
        }

        private async void CvExercises_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Exercise;
            if (currentSelection == null) return;
            var result = await DisplayAlert("Alert", "Do you want to delete this exercise", "Yes", "No");
            if (result)
            {
                var response = await ApiServices.DeleteExercise(currentSelection.Id);
                if (response == false)
                {
                    return;
                }
                else
                {
                    ExercisesCollection.Clear();
                    pageNumber = 0;
                    GetExercises();
                }
            }

    ((CollectionView)sender).SelectedItem = null;
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ImgAdd_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AssExercisePage());
        }
    }
}