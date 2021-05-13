using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            ToolbarItem item = new ToolbarItem();
            item.Text = "Log Out";
            item.Priority = 5;
            item.Order = ToolbarItemOrder.Secondary;
            item.Clicked += Tblogout_Clicked;
        }

        private void Tblogout_Clicked(object sender, System.EventArgs e)
        {
            Preferences.Set("accessToken", string.Empty);
            Preferences.Set("tokenExpirationTime", 0);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private void BtnUsers_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new UsersPage());
        }

        private void BtnExercises_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new ExercisesPage());
        }
    }
}