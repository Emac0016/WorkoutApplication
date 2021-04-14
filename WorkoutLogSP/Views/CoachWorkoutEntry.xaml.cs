using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogSP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutLogSP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoachWorkoutEntry : ContentPage
    {

        readonly FirebHelp firebaseHelper = new FirebHelp();

        public CoachWorkoutEntry()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(Object sender, EventArgs e)
        {

            string userComponent = UserSettings.TeamCode;

            string sport = UserSettings.Sport;

            string type = WorkType.SelectedItem.ToString();

            string description = WorkSum.Text;

            string timeCreated = DateTime.Now.ToString();

            await firebaseHelper.AddWorkout(userComponent, timeCreated, sport, type, description);

            await Navigation.PopAsync();

        }
    }
}