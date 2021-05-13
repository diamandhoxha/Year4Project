using FitApp.Services;
using Microcharts;
using SkiaSharp;
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
    public partial class BMIPage : ContentPage
    {

        public BMIPage()
        {
            InitializeComponent();
            GetUserDetail();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetUserDetail();
        }

        private async void GetUserDetail()
        {
            var userId = Preferences.Get("userId", 0);
            var user = await ApiServices.GetUserDetail(userId);
            var newHeight = user.Height / 100;
            var bmi = Math.Round(user.Weight / (newHeight * newHeight), 2);
            var weightStatus = "";
            if (bmi < 18.5)
            {
                weightStatus = "Underweight";
                chartViewBar.Chart = new BarChart
                {
                    Entries = underweight,
                    LabelTextSize = 40,
                    ValueLabelOrientation = Orientation.Horizontal,
                    LabelOrientation = Orientation.Horizontal
                };
            }
            else if (bmi > 18.4 && bmi < 25)
            {
                weightStatus = "Normal Weight";
                chartViewBar.Chart = new LineChart
                {
                    Entries = normalWeight,
                    LabelTextSize = 40,
                    PointMode = PointMode.Circle,
                    PointSize = 20,
                    ValueLabelOrientation = Orientation.Horizontal,
                    LabelOrientation = Orientation.Horizontal
                };
            }
            else if (bmi > 24.9 && bmi < 30)
            {
                weightStatus = "Overweight";
                chartViewBar.Chart = new RadarChart
                {
                    Entries = overweight,
                    LabelTextSize = 30,
                };
            }
            else
            {
                weightStatus = "Obese";
                chartViewBar.Chart = new PointChart
                {
                    Entries = obese,
                    LabelTextSize = 40,
                    PointMode = PointMode.Circle,
                    PointSize = 20,
                    ValueLabelOrientation = Orientation.Horizontal,
                    LabelOrientation = Orientation.Horizontal
                };
            }
            LblBmi.Text = "Your BMI is: " + bmi + ", which is: " + weightStatus;
        }

        private readonly ChartEntry[] entries = new[]
{
            new ChartEntry(23)
            {
                Label = "Obese",
                ValueLabel = "23%",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(37)
            {
                Label = "Overweight",
                ValueLabel = "37%",
                Color = SKColor.Parse("#77d065")
            },
            new ChartEntry(37)
            {
                Label = "Normal Weight",
                ValueLabel = "37%",
                Color = SKColor.Parse("#b455b6")
            },
            new ChartEntry(2)
            {
                Label = "Underweight",
                ValueLabel = "2%",
                Color = SKColor.Parse("#3498db")
            }
        };

        private readonly ChartEntry[] obese = new[]
{
            new ChartEntry(6)
            {
                Label = "15-24",
                ValueLabel = "6%",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(22)
            {
                Label = "25-34",
                ValueLabel = "22%",
                Color = SKColor.Parse("#77d065")
            },
            new ChartEntry(24)
            {
                Label = "35-44",
                ValueLabel = "24%",
                Color = SKColor.Parse("#b455b6")
            },
            new ChartEntry(27)
            {
                Label = "45-54",
                ValueLabel = "27%",
                Color = SKColor.Parse("#8A2BE2")
            },
            new ChartEntry(34)
            {
                Label = "55-64",
                ValueLabel = "34%",
                Color = SKColor.Parse("#7FFF00")
            },
            new ChartEntry(35)
            {
                Label = "65-74",
                ValueLabel = "35%",
                Color = SKColor.Parse("#00FFFF")
            },
            new ChartEntry(31)
            {
                Label = "75+",
                ValueLabel = "31%",
                Color = SKColor.Parse("#3498db")
            }
        };

        private readonly ChartEntry[] overweight = new[]
{
            new ChartEntry(20)
            {
                Label = "15-24",
                ValueLabel = "20%",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(42)
            {
                Label = "25-34",
                ValueLabel = "42%",
                Color = SKColor.Parse("#77d065")
            },
            new ChartEntry(42)
            {
                Label = "35-44",
                ValueLabel = "42%",
                Color = SKColor.Parse("#b455b6")
            },
            new ChartEntry(52)
            {
                Label = "45-54",
                ValueLabel = "52%",
                Color = SKColor.Parse("#8A2BE2")
            },
            new ChartEntry(48)
            {
                Label = "55-64",
                ValueLabel = "48%",
                Color = SKColor.Parse("#7FFF00")
            },
            new ChartEntry(46)
            {
                Label = "65-74",
                ValueLabel = "46%",
                Color = SKColor.Parse("#00FFFF")
            },
            new ChartEntry(51)
            {
                Label = "75+",
                ValueLabel = "51%",
                Color = SKColor.Parse("#3498db")
            }
        };

        private readonly ChartEntry[] normalWeight = new[]
{
            new ChartEntry(65)
            {
                Label = "15-24",
                ValueLabel = "65%",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(34)
            {
                Label = "25-34",
                ValueLabel = "34%",
                Color = SKColor.Parse("#77d065")
            },
            new ChartEntry(30)
            {
                Label = "35-44",
                ValueLabel = "30%",
                Color = SKColor.Parse("#b455b6")
            },
            new ChartEntry(21)
            {
                Label = "45-54",
                ValueLabel = "21%",
                Color = SKColor.Parse("#8A2BE2")
            },
            new ChartEntry(18)
            {
                Label = "55-64",
                ValueLabel = "18%",
                Color = SKColor.Parse("#7FFF00")
            },
            new ChartEntry(19)
            {
                Label = "65-74",
                ValueLabel = "19%",
                Color = SKColor.Parse("#00FFFF")
            },
            new ChartEntry(18)
            {
                Label = "75+",
                ValueLabel = "18%",
                Color = SKColor.Parse("#3498db")
            }
        };

        private readonly ChartEntry[] underweight = new[]
{
            new ChartEntry(9)
            {
                Label = "15-24",
                ValueLabel = "9%",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(2)
            {
                Label = "25-34",
                ValueLabel = "2%",
                Color = SKColor.Parse("#77d065")
            },
            new ChartEntry(1)
            {
                Label = "35-44",
                ValueLabel = "1%",
                Color = SKColor.Parse("#b455b6")
            },
            new ChartEntry(1)
            {
                Label = "45-54",
                ValueLabel = "1%",
                Color = SKColor.Parse("#8A2BE2")
            },
            new ChartEntry(0)
            {
                Label = "55-64",
                ValueLabel = "<1%",
                Color = SKColor.Parse("#7FFF00")
            },
            new ChartEntry(0)
            {
                Label = "65-74",
                ValueLabel = "<1%",
                Color = SKColor.Parse("#00FFFF")
            },
            new ChartEntry(0)
            {
                Label = "75+",
                ValueLabel = "<1%",
                Color = SKColor.Parse("#3498db")
            }
        };
    }
}