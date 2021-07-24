using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutLogSP.ViewModels
{
    public class Workouts
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string UserComp { get; set; }
        public string WorkoutCompleter { get; set; }
        public string TimeCreated { get; set; }
        public string Sport { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string SendWorkout { get; set; }
    }
}
