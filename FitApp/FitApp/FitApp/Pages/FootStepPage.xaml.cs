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
    public partial class FootStepPage : ContentPage
    {
        int counter = 0;
        public FootStepPage()
        {
            InitializeComponent();

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs args)
        {
            xResult.Text = $"X: {args.Reading.Acceleration.X}";
            yResult.Text = $"Y: {args.Reading.Acceleration.Y}";
            zResult.Text = $"Z: {args.Reading.Acceleration.Z}";

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (Accelerometer.IsMonitoring)
            {
                Accelerometer.Stop();
            }
            else
            {
                Accelerometer.Start(SensorSpeed.UI);
            }
        }
    }
}