using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogSP.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutLogSP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : MasterDetailPage
    {
        public string firebaseAPIKey = "AIzaSyB7J0wlqzW8sx-W_DFpqMOwbX5sEKqOXvI";

        public List<DashboardMenu> MenuItems { get; set; }

        public Dashboard()
        {
            InitializeComponent();

            GetProfileInfoAndRefToken();

            MenuItems = new List<DashboardMenu>();

            var teamWorkout = new DashboardMenu() { Title = "Team Workouts", Icon = "WorkoutPage.png", TargetType = typeof(TeamWorkoutList) };

            var infoPage = new DashboardMenu() { Title = "Home", Icon = "WorkoutEntries.png", TargetType = typeof(HomePage) };

            var workoutListPage = new DashboardMenu() { Title = "Personal Workouts", Icon = "workEnter.png", TargetType = typeof(WorkoutListPage) };

            var logoutListItem = new DashboardMenu() { Title = "Logout", Icon = "Logout.png",  TargetType = typeof(LoginPage) };

            var teamInfo = new DashboardMenu() { Title = "Team Roster", Icon = "TeamPage.png", TargetType = typeof(CoachTeamInfo) };

            
            if(UserSettings.Role == "Coach")
            {
                MenuItems.Add(teamInfo);
            }
            else if(UserSettings.Role == "Player")
            {
                MenuItems.Add(workoutListPage);
            }

            MenuItems.Add(teamWorkout);
            MenuItems.Add(infoPage);
            MenuItems.Add(logoutListItem);

            navDrawer.ItemsSource = MenuItems;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));

            this.BindingContext = new
            {
                Header = "Workout Tracking Application",
                Image = "LoginImage.png",
                Footer = UserSettings.Email
            };
        }


        void OnPageSelected(Object sender, SelectedItemChangedEventArgs e)
        {
            var item = (DashboardMenu)e.SelectedItem;

            Type page = item.TargetType;

            if (e.SelectedItem.ToString() == "Logout")
            {
                Preferences.Remove("FirebaseRefreshToken");

                UserSettings.ClearAllData();


                Detail = new NavigationPage((Page)(new NavigationPage(new LoginPage())));

                IsPresented = false;

            }


            else
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));

                IsPresented = false;
            }


        }

        async private void GetProfileInfoAndRefToken()
        {
            var authProv = new FirebaseAuthProvider(new FirebaseConfig(firebaseAPIKey));

            try
            {

                var savedAuth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FirebaseRefreshToken", ""));

                var RefCont = await authProv.RefreshAuthAsync(savedAuth);

                Preferences.Set("FirebaseRefreshToken", JsonConvert.SerializeObject(RefCont));
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("LogAlert", "Token has Expired", "Ok");
            }

        }
    }

}