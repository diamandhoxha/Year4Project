using System;
using System.Collections.Generic;
using System.Text;

namespace FitApp.Models
{
    public class Nutrient
    {
        public string food_name { get; set; }
        public double serving_qty { get; set; }
        public double serving_weight_grams { get; set; }
        public double nf_calories { get; set; }
        public double nf_total_fat { get; set; }
        public double nf_saturated_fat { get; set; }
        public double nf_cholesterol { get; set; }
        public double nf_sodium { get; set; }
        public double nf_potal_carohydrate { get; set; }
        public double nf_protein { get; set; }
    }
}
