﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLogSP.Database;
using WorkoutLogSP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutLogSP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutListPage : ContentPage
    {
        readonly FirebHelp firebaseHelper = new FirebHelp();

        public WorkoutListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            PersonalWorkoutDatabase workouts = await PersonalWorkoutDatabase.Instance;

            WorkoutList.ItemsSource = await workouts.GetItemsAsync(UserSettings.ID);
            

        }

        async void AddWorkoutClicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutEntry
            {
                BindingContext = new Workouts()
            });
        }

        async void OnItemSelection(Object sender, SelectedItemChangedEventArgs e)
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