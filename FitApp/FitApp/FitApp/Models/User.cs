using System;
using System.Collections.Generic;
using System.Text;

namespace FitApp.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double newHeight => Height / 100;
        public double BMI => Math.Round(Weight / (newHeight * newHeight), 2);
        public string MyBmi => "Your BMI is: " + BMI;

    }
}
