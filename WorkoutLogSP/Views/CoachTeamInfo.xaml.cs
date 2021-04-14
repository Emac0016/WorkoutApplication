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
    public partial class CoachTeamInfo : ContentPage
    {
        readonly FirebHelp firebaseHelper = new FirebHelp();

        public CoachTeamInfo()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var role = "Player";

            var sport = UserSettings.Sport;

            var team = UserSettings.TeamName;

            var players = await firebaseHelper.GetTeamRoster(role, sport, team);

            Roster.ItemsSource = players;
        }

        async void OnPlayerSelection(Object sender, SelectedItemChangedEventArgs e)
        {
           if (e.SelectedItem != null)
            {

                PlayWSet.ClearData();

                var tappedPlayer = e.SelectedItem.ToString();

                await firebaseHelper.GetPlayer(tappedPlayer);

                PlayWSet.Name = tappedPlayer;

                PlayWSet.Sport = UserSettings.Sport;

                PlayWSet.SendWorkout = "true";

                await Navigation.PushAsync(new NavigationPage(new CoachPlayerWorkouts()));
            }
        }
    }
}