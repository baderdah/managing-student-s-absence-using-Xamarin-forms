using System;
using System.Collections.Generic;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class WelcomePage : ContentPage
    {
        private SQLiteAsyncConnection _connection;

        public WelcomePage()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<User>();
            await _connection.CreateTableAsync<Absence>();
            await _connection.CreateTableAsync<Classe>();
            await _connection.CreateTableAsync<Seance>();
            await _connection.CreateTableAsync<Student>();

            base.OnAppearing();
        }


        async void loginBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            var btnClicked = sender as Button;
            if (btnClicked.Text.Equals("Login"))
            {
               await Navigation.PushAsync(new LoginPage());

            }else if (btnClicked.Text.Equals("Register"))
            {
                await Navigation.PushAsync(new RegisterPage());
            }
        }
    }
}
