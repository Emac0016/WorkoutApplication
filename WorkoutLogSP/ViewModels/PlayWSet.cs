using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutLogSP.ViewModels
{
    public static class PlayWSet
    {

        static ISettings PlSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string Name
        {
            get => PlSettings.GetValueOrDefault(nameof(Name), string.Empty);
            set => PlSettings.AddOrUpdateValue(nameof(Name), value);
        }

        public static string Sport
        {
            get => PlSettings.GetValueOrDefault(nameof(Sport), string.Empty);
            set => PlSettings.AddOrUpdateValue(nameof(Sport), value);
        }

        public static string SendWorkout 
        {
            get => PlSettings.GetValueOrDefault(nameof(SendWorkout), string.Empty);
            set => PlSettings.AddOrUpdateValue(nameof(SendWorkout), value);
        }

        public static void ClearData()
        {
            PlSettings.Clear();
        }
    }
}
