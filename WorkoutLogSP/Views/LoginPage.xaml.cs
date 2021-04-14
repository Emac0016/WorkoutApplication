using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogSP.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutLogSP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public string firebaseAPIKey = "AIzaSyB7J0wlqzW8sx-W_DFpqMOwbX5sEKqOXvI";

       readonly FirebHelp firebase = new FirebHelp();

        async void LoginClicked(Object sender, EventArgs e)
        {
            
            var authProv = new FirebaseAuthProvider(new FirebaseConfig(firebaseAPIKey));


            try
            {
                var authorize = await authProv.SignInWithEmailAndPasswordAsync(EmailLoginEntry.Text, PasswordLoginEntry.Text);
                var cont = await authorize.GetFreshAuthAsync();
                var serialCont = JsonConvert.SerializeObject(cont);
                Preferences.Set("FirebaseRefreshToken", serialCont);

                UserSettings.Email = EmailLoginEntry.Text;

                var user = await firebase.GetUser(EmailLoginEntry.Text);

                if (user != null)
                {
                    UserSettings.Role = user.Role.ToString();
                    UserSettings.ID = user.ID.ToString();
                    UserSettings.Name = user.Name.ToString();
                    UserSettings.TeamCode = user.TeamCode.ToString();
                    UserSettings.TeamName = user.TeamName.ToString();
                    UserSettings.Sport = user.Sport.ToString();
                }

                App.Current.MainPage = new Dashboard();


            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Login Alert", ex.Message, "Ok");
            }
        }

        async void RegisterClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

    }
}