using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class NewSeancePage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private List<Classe> classeList;
        private List<Lesson> lessonList;
        public NewSeancePage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        async protected override void OnAppearing()
        {
            await _connection.CreateTableAsync<Classe>();
            await _connection.CreateTableAsync<Lesson>();

            classeList = await _connection.Table<Classe>().ToListAsync();
            lessonList = await _connection.Table<Lesson>().ToListAsync();

            foreach (Classe cl in classeList)
                classePicker.Items.Add(cl.name);
            foreach (Lesson lesson in lessonList)
                lessonPicker.Items.Add(lesson.name);

            base.OnAppearing();

        }

        async void saveBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            String sdate = date.Date.ToString();
            if (classePicker.SelectedIndex >= 0 && lessonPicker.SelectedIndex >= 0)
            {
                String sclasse = classePicker.Items[classePicker.SelectedIndex];
                String slesson = lessonPicker.Items[lessonPicker.SelectedIndex];
                Classe selectedClasse = (from cl in classeList where cl.name == sclasse select cl).FirstOrDefault();
                Lesson selectedLesson = (from alesson in lessonList where alesson.name == slesson select alesson).FirstOrDefault();

                Seance seance = new Seance(sdate, selectedLesson.id, selectedClasse.id);
                
                await _connection.InsertAsync(seance);

                List<Seance> seanceList = await _connection.Table<Seance>().ToListAsync();

                List<Student> students = await _connection.Table<Student>().Where(st => st.classeId == seance.classeId).ToListAsync();

                Absence abs;
                int nb = 0;
                foreach (Student st in students)
                {
                    abs = new Absence();
                    abs.isAbsent = false;
                    abs.lessonId = seance.lessonId;
                    abs.seanceId = seance.id;
                    abs.studentId = st.id;
                    nb += await _connection.InsertAsync(abs);
                }
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert", "Informations non completes !", "OK");
            }
        }
    }
}
