using System;
using System.Collections.Generic;
using System.Text;

namespace FitApp.Models
{
    public class ExerciseDetail
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public string TrailorUrl { get; set; }
        public string ImageUrl { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
