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
    public partial class WorkoutViewPage : ContentPage
    {
        readonly FirebHelp firebseHelper = new FirebHelp();

        public WorkoutViewPage()
        {
            InitializeComponent();

            if(UserSettings.Role == "Coach")
            {
                Del.IsVisible = true;
                Del.IsEnabled = true;
            }
            else
            {
                Del.IsVisible = false;
                Del.IsEnabled = false;
            }


        }

        async void OnDeleteClicked(Object sender, EventArgs e)
        {
            string userComp = UserSettings.TeamCode;

            string time = timeCreated.Text.ToString();

            await firebseHelper.DeleteWorkout(userComp, time);

            await Navigation.PopAsync();
        }
    }
}