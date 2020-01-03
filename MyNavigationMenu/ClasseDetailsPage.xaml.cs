using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class ClasseDetailsPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Student> studentObsColl;

        Classe classe;

        public ClasseDetailsPage(Classe classe)
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            this.classe = classe;
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {

            var clas = _connection.Table<Classe>().Where(cl => cl.id == this.classe.id);
            classe = await clas.FirstAsync();

            if (classe.nbStudents == 0)
            {
                label.Text = "the classe you selected is empty you can add some students ... :/ ";
            }
            else
            {
                var students = await _connection.Table<Student>().Where(student => student.classeId == this.classe.id).ToListAsync();
                studentObsColl = new ObservableCollection<Student>(students);
                studentsListView.ItemsSource = studentObsColl;
            }

            base.OnAppearing();
        }

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewStudentPage(this.classe)); ;
        }

        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            Student student = menuItem.CommandParameter as Student;

            var studentFromDb = _connection.Table<Student>().Where(stud => stud.id == student.id);
            var studentFromDbBis = await studentFromDb.FirstAsync();

            var studentClasse = _connection.Table<Classe>().Where(classe => classe.id == student.classeId);
            Classe studentClasseBis = await studentClasse.FirstAsync();
            studentClasseBis.nbStudents = studentClasseBis.nbStudents - 1;


            await _connection.UpdateAsync(studentClasseBis);
            await _connection.DeleteAsync(studentFromDbBis);

            studentObsColl.Remove(student);
        }

    }


}
