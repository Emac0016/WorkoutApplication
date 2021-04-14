using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutLogSP.ViewModels
{
    class UserInfo
    {
        public string Email
        {
            get => UserSettings.Email;
            set
            {
                UserSettings.Email = value;
            }
        }

        public string Name
        {
            get => UserSettings.Name;
            set
            {
                UserSettings.Name = value;
            }
        }

        public string ID
        {
            get => UserSettings.ID;
            set
            {
                UserSettings.ID = value;
            }
        }

        public string Role
        {
            get => UserSettings.Role;
            set => UserSettings.Role = value;
        }

        public string TeamName
        {
            get => UserSettings.TeamName;
            set
            {
                UserSettings.TeamName = value;
            }
        }

        public string TeamCode
        {
            get => UserSettings.TeamCode;
            set
            {
                UserSettings.TeamCode = value;
            }
        }
    }
}
