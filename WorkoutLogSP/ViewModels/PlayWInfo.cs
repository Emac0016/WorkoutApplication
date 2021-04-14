using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutLogSP.ViewModels
{
    class PlayWInfo
    {
        public string Name
        {
            get => PlayWSet.Name;
            set
            {
                PlayWSet.Name = value;
            }
        }

        public string Sport
        {
            get => PlayWSet.Sport;
            set
            {
                PlayWSet.Sport = value;
            }
        }

        public string SendWorkout
        {
            get => PlayWSet.SendWorkout;
            set
            {
                PlayWSet.SendWorkout = value;
            }
        }

    }
}
