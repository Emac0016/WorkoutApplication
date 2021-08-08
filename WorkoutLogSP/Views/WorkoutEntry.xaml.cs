using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkoutLogSP.ViewModels;
using SQLite;
using WorkoutLogSP.Database;
using WorkoutLogSP.Constants;

namespace WorkoutLogSP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutEntry : ContentPage
    {

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(SQLFunctionality.DatabasePath, SQLFunctionality.Flags);
        });

        static SQLiteAsyncConnection WorkoutDatabase => lazyInitializer.Value;
        static bool initialized = false;

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!WorkoutDatabase.TableMappings.Any(m => m.MappedType.Name == typeof(Workouts).Name))

                    await WorkoutDatabase.CreateTablesAsync(CreateFlags.None, typeof(Workouts)).ConfigureAwait(false);

                initialized = true;
            }
        }

        readonly FirebHelp firebaseHelper = new FirebHelp();

        public WorkoutEntry()
        {
            InitializeComponent();

            InitializeAsync().SafeFireAndForget(false);
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {

            //Adds the personal workout to the local device
            Workouts personalWorkout = new Workouts
            {
                UserComp = UserSettings.ID,

                WorkoutCompleter = UserSettings.Name,

                Sport = UserSettings.Sport,

                Type = WorkType.SelectedItem.ToString(),

                Description = WorkSum.Text,

                TimeCreated = DateTime.Now.ToString(),

                SendWorkout = "false"
            };


            //Adds information for the workouts sent to the coach 
            string usersTeam = UserSettings.TeamName;

            string sport = UserSettings.Sport;

            string type = WorkType.SelectedItem.ToString();

            string description = WorkSum.Text;

            string timeCreated = DateTime.Now.ToString();

            if(sendToCoach.IsChecked)
            {

                await firebaseHelper.AddWorkout(usersTeam, timeCreated, sport, type, description);

                await Navigation.PopAsync();

            }

            PersonalWorkoutDatabase database = await PersonalWorkoutDatabase.Instance;
            await database.SaveItemAsync(personalWorkout);

            // Navigate backwards
            await Navigation.PopAsync();

        }


    }

}