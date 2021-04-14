using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WorkoutLogSP.ViewModels;

namespace WorkoutLogSP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutEntry : ContentPage
    {

        readonly FirebHelp firebaseHelper = new FirebHelp();

        public WorkoutEntry()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            string userComponent = UserSettings.ID;

            string personCompleted = UserSettings.Name;

            string sport = UserSettings.Sport;

            string type = WorkType.SelectedItem.ToString();

            string description = WorkSum.Text;

            string timeCreated = DateTime.Now.ToString();

            string sendWorkout = "false";

            if(sendToCoach.IsChecked)
            {
               sendWorkout = "true";
            }

            await firebaseHelper.AddPersonalWorkout(userComponent, personCompleted, timeCreated, sport, type, description, sendWorkout);

            await Navigation.PopAsync();

        }

    }
}