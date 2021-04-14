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
    public partial class CoachPlayerWorkouts : ContentPage
    {
        readonly FirebHelp firebaseHelper = new FirebHelp();

        public CoachPlayerWorkouts()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var playerWorkouts = await firebaseHelper.GetPlayerWorkouts(PlayWSet.Name, PlayWSet.Sport, PlayWSet.SendWorkout);

            PlayerWorkoutList.ItemsSource = playerWorkouts;
        }

        public async void OnItemSelection(Object Sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                await Navigation.PushAsync(new WorkoutViewPageP
                {
                    BindingContext = e.SelectedItem as Workouts
                });
            }
        }
    }
}