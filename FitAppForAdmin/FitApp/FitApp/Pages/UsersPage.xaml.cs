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
    public partial class UsersPage : ContentPage
    {

        private ObservableCollection<Users> UsersCollection;
        private int pageNumber = 0;
        public UsersPage()
        {
            InitializeComponent();
            UsersCollection = new ObservableCollection<Users>();
            GetUsers();
        }

        private async void  GetUsers()
        {
            pageNumber++;
            var users = await ApiServices.GetAllUsers(pageNumber, 10);

            for (int i = 0; i < users.Count(); i++)
            {
                var user = users[i];
                UsersCollection.Add(user);
            }

            CvUsers.ItemsSource = UsersCollection;
        }

        private void CvUsers_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            GetUsers();
        }

        private async void CvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Users;
            if (currentSelection == null) return;
            var result = await DisplayAlert("Alert", "Do you want to delete this user", "Yes", "No");
            if (result)
            {
                var response = await ApiServices.DeleteUsers(currentSelection.Id);
                if (response == false)
                {
                    return;
                }
                else
                {
                    UsersCollection.Clear();
                    pageNumber = 0;
                    GetUsers();
                }
            }

            ((CollectionView)sender).SelectedItem = null;
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}