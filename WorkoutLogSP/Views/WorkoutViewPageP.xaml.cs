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
    public partial class WorkoutViewPageP : ContentPage
    {
        readonly FirebHelp firebseHelper = new FirebHelp();

        public WorkoutViewPageP()
        {
            InitializeComponent();
        }

        async void OnDeleteClicked(Object sender, EventArgs e)
        {
            string userComp = UserSettings.ID;

            string time = timeCreated.Text.ToString();

            await firebseHelper.DeletePersonalWorkout(userComp, time);

            await Navigation.PopAsync();
        }
    }
}