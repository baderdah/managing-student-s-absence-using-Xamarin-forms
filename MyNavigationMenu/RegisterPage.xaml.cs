using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class RegisterPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<User> usersObsColl;

        public RegisterPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<User>();

            base.OnAppearing();
        }


        async void loginBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(password.Text) && !String.IsNullOrEmpty(userName.Text)
                && !String.IsNullOrEmpty(firstName.Text) && !String.IsNullOrEmpty(lastName.Text))
            {
                var user = new User { username = userName.Text, password = password.Text, lastName = lastName.Text, firstName = firstName.Text };

                var users = await _connection.Table<User>().ToListAsync();
                usersObsColl = new ObservableCollection<User>(users);

                Boolean userNameAlreadyExist = false;
                foreach (User aUser in usersObsColl)
                {
                    if (user.username.Equals(aUser.username))
                    {
                        userNameAlreadyExist = true;
                        await DisplayAlert("alert", "user Name Already Exist", "OK");
                    }
                }
                if (!userNameAlreadyExist)
                {
                    await _connection.InsertAsync(user);
                    await DisplayAlert("alert", "Registration success", "OK");
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