using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //not an actual page - used for tabbed format(TabbedPage)
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();

            ToolbarItem item = new ToolbarItem();
            item.Text = "Log Out";
            item.Priority = 5;
            item.Order = ToolbarItemOrder.Secondary;
            item.Clicked += Profile_Clicked;
            item.Clicked += ContactUs_Clicked;
            item.Clicked += Tblogout_Clicked;
        }

        private void Tblogout_Clicked(object sender, System.EventArgs e)
        {
            Preferences.Set("accessToken", string.Empty);
            Preferences.Set("tokenExpirationTime", 0);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private async void Profile_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ProfilePage());
        }

        private async void ContactUs_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ContactUsPage());
        }

        private async void Steps_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new FootStepPage());
        }
    }
}