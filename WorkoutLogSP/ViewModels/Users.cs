using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutLogSP.ViewModels
{
    public class Users
    {
        public string Role { get; set; }
        public string Email { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public string Sport { get; set; }
        public int TeamCode { get; set; }

        public override string ToString() => Name;
    }

}
