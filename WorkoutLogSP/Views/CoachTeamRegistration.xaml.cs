using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogSP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutLogSP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoachTeamRegistration : ContentPage
    {
        readonly FirebHelp firebaseHelper = new FirebHelp();

        public CoachTeamRegistration()
        {
            InitializeComponent();
        }

        public async void TeamRegButton_Clicked(Object sender, EventArgs e)
        {

            Random CoachIdGen = new Random();
            Random TeamCodeGen = new Random();

            int coachId = CoachIdGen.Next(1, 100000);

            int teamCode = TeamCodeGen.Next(1, 100000);

            string coach = Name.Text;

            string team = TName.Text;

            string role = "Coach";

            string email = UserSettings.Email;

            string sp = SportPick.SelectedItem.ToString();

            await firebaseHelper.AddCoach(role, email, coachId, coach, team, sp, teamCode);

            await firebaseHelper.AddTeam(TName.Text, teamCode, Name.Text, sp);

            Name.Text = string.Empty;
            TName.Text = string.Empty;

            await App.Current.MainPage.DisplayAlert("Team Code", "Your Team Code is " + teamCode, "Next");
            await App.Current.MainPage.DisplayAlert("Success", "Coaches Team Registered", "Continue");

            await Navigation.PushAsync(new LoginPage());

        }
    }
}