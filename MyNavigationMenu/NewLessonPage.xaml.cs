using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class NewLessonPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Lesson> lessonObsColl;
        public NewLessonPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        async protected override void OnAppearing()
        {
            await _connection.CreateTableAsync<Lesson>();
            base.OnAppearing();
        }

        async void CreateBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(name.Text))
            {
                var lesson = new Lesson { name = name.Text };

                var lessons = await _connection.Table<Lesson>().ToListAsync();

                lessonObsColl = new ObservableCollection<Lesson>(lessons);

                Boolean lessonNameAlreadyExist = false;

                foreach (Lesson aLesson in lessonObsColl)
                {
                    if (lesson.name.Equals(aLesson.name))
                    {
                        lessonNameAlreadyExist = true;
                        await DisplayAlert("alert", "lesson's Name Already Exist", "OK");
                    }
                }
                if (!lessonNameAlreadyExist)
                {
                    await _connection.InsertAsync(lesson);
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
