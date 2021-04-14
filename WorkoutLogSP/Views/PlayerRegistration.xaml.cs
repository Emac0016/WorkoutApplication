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
    public partial class PlayerRegistration : ContentPage
    {
        readonly FirebHelp firebaseHelper = new FirebHelp();

        public PlayerRegistration()
        {
            InitializeComponent();
        }

        public async void PlayRegButton_Clicked(Object sender, EventArgs e)
        {
            Random PlayerIdGen = new Random();

            int playerId = PlayerIdGen.Next(1, 100000);

            string player = pName.Text;

            string sport = SportPick.SelectedItem.ToString();

            string role = "Player";

            string email = UserSettings.Email;

            int teamCode = Convert.ToInt32(tCode.Text);

            string teamName = tName.Text;

            await firebaseHelper.AddPlayer(role, email, playerId, player, teamName, sport, teamCode);

            pName.Text = string.Empty;
            tCode.Text = string.Empty;

            await App.Current.MainPage.DisplayAlert("Success", "Player Added to team", "Continue");

            await Navigation.PushAsync(new LoginPage());
        }
    }
}