using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StopWatchPage : ContentPage
    {
        Stopwatch stopwatch;
        public StopWatchPage()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            lblStopWatch.Text = "00:00:00.00000";
        }

        private void btnStart_Clicked(object sender, EventArgs e)
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();

                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    lblStopWatch.Text = stopwatch.Elapsed.ToString();

                    if (!stopwatch.IsRunning)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                });
            }

        }

        private void btnStop_Clicked(object sender, EventArgs e)
        {
            btnStart.Text = "Resume";
            stopwatch.Stop();
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            lblStopWatch.Text = "00:00:00.00000";
            btnStart.Text = "Start";
            stopwatch.Reset();
        }
    }
}