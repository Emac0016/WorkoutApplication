using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutLogSP.ViewModels
{
    public static class UserSettings
    {

        static ISettings AppSettings
        {
            get {
                return CrossSettings.Current;
            }
        }

        public static string Email
        {
            get => AppSettings.GetValueOrDefault(nameof(Email), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Email), value);
        }
      
        public static string ID
        {
            get => AppSettings.GetValueOrDefault(nameof(ID), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(ID), value);
        }
       
        public static string Role
        {
            get => AppSettings.GetValueOrDefault(nameof(Role), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Role), value);
        }

        public static string Name
        {
            get => AppSettings.GetValueOrDefault(nameof(Name), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Name), value);
        }

        public static string Sport
        {
            get => AppSettings.GetValueOrDefault(nameof(Sport), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Sport), value);
        }

        public static string TeamName
        {
            get => AppSettings.GetValueOrDefault(nameof(TeamName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(TeamName), value);
        }

        public static string TeamCode
        {
            get => AppSettings.GetValueOrDefault(nameof(TeamCode), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(TeamCode), value);
        }

        public static void ClearAllData()
        {
            AppSettings.Clear();
        }
    }
}
