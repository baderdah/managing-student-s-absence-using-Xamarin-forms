using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class SeancePage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Seance> seanceObsColl;

        public SeancePage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        async protected override void OnAppearing()
        {
            await _connection.CreateTableAsync<Seance>();

            var seance = await _connection.Table<Seance>().ToListAsync();

            seanceObsColl = new ObservableCollection<Seance>(seance);
            seancesListView.ItemsSource = seanceObsColl;

            base.OnAppearing();

        }

        async void seancesListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedSeance = e.SelectedItem as Seance;
            if (selectedSeance != null)
                await Navigation.PushAsync(new SeanceDetailsPage(selectedSeance));
            seancesListView.SelectedItem = null;
        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewSeancePage());
        }

        //delete seance  A modifier  !!!!!!!!!!! suprimer les absences de cette seance
        void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {

            var menuItem = sender as MenuItem;
            Seance selectedSeance = menuItem.CommandParameter as Seance;

            DbOperations.deleteSeance(selectedSeance);
            seanceObsColl.Remove(selectedSeance);

        }
    }
}
