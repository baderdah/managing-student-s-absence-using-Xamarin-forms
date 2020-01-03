using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class NewClassePage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Classe> classeObsColl;

        public NewClassePage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        async protected override void OnAppearing()
        {
            await _connection.CreateTableAsync<Classe>();
            base.OnAppearing();
        }

        async void CreateBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(name.Text))
            {
                var classe = new Classe { name = name.Text };

                var classes = await _connection.Table<Classe>().ToListAsync();

                classeObsColl = new ObservableCollection<Classe>(classes);

                Boolean classeNameAlreadyExist = false;
                foreach (Classe aClasse in classeObsColl)
                {
                    if (classe.name.Equals(aClasse.name))
                    {
                        classeNameAlreadyExist = true;
                        await DisplayAlert("alert", "classe Name Already Exist", "OK");
                    }
                }
                if (!classeNameAlreadyExist)
                {
                    await _connection.InsertAsync(classe);
                    await DisplayAlert("alert", "success", "OK");
                    await Navigation.PopAsync();
                }

            }
            else
            {
                await DisplayAlert("alert", "informations not complet", "OK");
            }

        }
    }
}
