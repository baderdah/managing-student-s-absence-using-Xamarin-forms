using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class LoginPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<User> usersObsColl;
        public LoginPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }
        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<User>();

            // a supprimer
            var user = new User { password = "bader", username = "bader" };
            await _connection.InsertAsync(user);
            ////////////////////////////////////

            base.OnAppearing();
        }



        async void loginBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            if(!String.IsNullOrEmpty(password.Text) && !String.IsNullOrEmpty(userName.Text))
            {
                var user = new User { username = userName.Text, password = password.Text };
                var users = await _connection.Table<User>().ToListAsync();
                User finalUser = null;

                usersObsColl = new ObservableCollection<User>(users);
                foreach (User aUser in usersObsColl)
                {
                    if (user.username.Equals(aUser.username) && user.password.Equals(aUser.password))
                    {

                        finalUser = aUser;
                    }
                }


                if (finalUser != null)
                {
                    // login
                    await Navigation.PushAsync(new MyMenu(user));
                }
                else
                {
                    //send alert username or password are wrong
                    await DisplayAlert("alert", "password or login incorrect", "OK");
                }
            }
            else
            {
                await DisplayAlert("alert", "information invalid", "OK");
            }
        }
    }
}
