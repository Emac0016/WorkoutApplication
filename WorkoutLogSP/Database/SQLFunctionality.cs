using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WorkoutLogSP.ViewModels;

namespace WorkoutLogSP.ConstantVariables
{
    class SQLFunctionality
    {
        //Sets the db file to the current users name + PersonalWorkouts
        public static string databaseFile = UserSettings.Name + "PersonalWorkouts.db3";


        //Controls functionality of the read and write function of the database
        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache | SQLite.SQLiteOpenFlags.ProtectionCompleteUntilFirstUserAuthentication;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, databaseFile);
            }
        }

    }
}
