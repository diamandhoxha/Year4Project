using System;
using System.Collections.Generic;
using System.Text;

namespace FitApp.Models
{
    public class WorkoutDetail
    {
        public int Id { get; set; }
        public string WorkoutName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
