using FitApp.Models;
using FitApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        private ObservableCollection<Workouts> WorkoutsCollection;
        private int pageNumber = 0;

        public WorkoutPage()
        {
            InitializeComponent();
            WorkoutsCollection = new ObservableCollection<Workouts>();
            GetWorkouts();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            WorkoutsCollection.Clear();
            pageNumber = 0;
            GetWorkouts();
        }

        public async void GetWorkouts()
        {
            pageNumber++;
            var userId = Preferences.Get("userId", 0);
            var workouts = await ApiServices.GetWorkout(userId, pageNumber, 10);

            for (int i = 0; i < workouts.Count(); i++)
            {
                var workout = workouts[i];
                WorkoutsCollection.Add(workout);
            }

            CvWorkouts.ItemsSource = WorkoutsCollection;
        }


        private void CvWorkouts_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            GetWorkouts();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddWorkoutPage());
        }

        private async void CvWorkouts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Workouts;
            if (currentSelection == null) return;
            var result = await DisplayAlert("Alert", "Do you want to delete this workout", "Yes", "No");
            if (result)
            {
                var response = await ApiServices.DeleteWorkout(currentSelection.Id);
                if (response == false)
                {
                    return;
                }
                else
                {
                    WorkoutsCollection.Clear();
                    pageNumber = 0;
                    GetWorkouts();
                }
            }

            ((CollectionView)sender).SelectedItem = null;
        }

        private void Search_Clicked(object sender, EventArgs e)
        {

        }
    }
}