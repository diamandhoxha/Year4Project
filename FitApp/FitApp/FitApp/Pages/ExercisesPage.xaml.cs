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
            var exercises = await ApiServices.GetAllExercises(pageNumber, 5);
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

        private void CvExercises_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Exercise;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new ExerciseDetailPage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}