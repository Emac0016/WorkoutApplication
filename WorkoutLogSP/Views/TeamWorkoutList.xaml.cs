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
    public partial class TeamWorkoutList : ContentPage
    {

        readonly FirebHelp firebaseHelper = new FirebHelp();
        public TeamWorkoutList()
        {
            InitializeComponent();

            if(UserSettings.Role == "Coach")
            {
                addBtn.IsEnabled = true;
                addBtn.IsVisible = true;
            }
            else
            {
                addBtn.IsEnabled = false;
                addBtn.IsVisible = false;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var workouts =  await firebaseHelper.GetAllWorkouts(UserSettings.TeamCode);

            TeamWorkouts.ItemsSource = workouts;


        }

        async void AddWorkoutClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoachWorkoutEntry
            {
                BindingContext = new TeamWorkouts()
            });
        }


        async void OnItemSelection(Object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new WorkoutViewPage
                {
                    BindingContext = e.SelectedItem as TeamWorkouts
                });
            }
        }
    }
}