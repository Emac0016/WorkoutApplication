using Firebase.Auth;
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
    public partial class RegistrationPage : ContentPage
    {
        public string firebaseAPIKey = "AIzaSyB7J0wlqzW8sx-W_DFpqMOwbX5sEKqOXvI";

        public RegistrationPage()
        {
            InitializeComponent();
        }

        async void RegisterClicked(Object sender, EventArgs e)
        {
            try
            {
                if (CoachesBox.IsChecked)
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseAPIKey));
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
                    string getToken = auth.FirebaseToken;
                    await App.Current.MainPage.DisplayAlert("Success", "Registered Successfully", "OK");

                    UserSettings.Email = EmailEntry.Text;


                    await Navigation.PushAsync(new CoachTeamRegistration());
                }

                else if(PlayersBox.IsChecked)
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseAPIKey));
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
                    string getToken = auth.FirebaseToken;
                    await App.Current.MainPage.DisplayAlert("Success", "Registered Successfully", "OK");

                    UserSettings.Email = EmailEntry.Text;

                    await Navigation.PushAsync(new PlayerRegistration());
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Must Select Player or Coach", "Ok");

                    await Navigation.PushAsync(new RegistrationPage());
                }
                
            }
            catch (Exception exc)
            {
                await App.Current.MainPage.DisplayAlert("Alert", exc.Message, "OK");
            }
        }
    }
}