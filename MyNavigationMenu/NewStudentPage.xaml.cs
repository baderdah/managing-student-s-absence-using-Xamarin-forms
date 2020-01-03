using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyNavigationMenu.Model;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

namespace MyNavigationMenu
{
    public partial class NewStudentPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Student> studentsObsColl;
        Classe classe;
        public NewStudentPage(Classe classe)
        {
            this.classe = classe;
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }


        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Student>();
            base.OnAppearing();
        }


        async void createStudentBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            Nullable<int> intCne = null;
            try { 
                intCne = Int32.Parse(cne.Text); 
            }
            catch (Exception ee)
            {
                intCne = null;
            }
            if ( (intCne != null) && !String.IsNullOrEmpty(firstName.Text)
                && !String.IsNullOrEmpty(lastName.Text) && !String.IsNullOrEmpty(email.Text) && !String.IsNullOrEmpty(phone.Text))
            {

                var student = new Student { cne = (int) intCne, lastName = lastName.Text, phone = phone.Text, firstName = firstName.Text,
                email = email.Text, classeId = this.classe.id};

                var students = _connection.Table<Student>().Where(st => (st.cne == student.cne && st.classeId == this.classe.id));

                if (await students.CountAsync() > 0)
                {
                    await DisplayAlert("alert", "students's Cne Already Exist", "OK");
                }
                else
                {
                    //update student's classe.nbStudents
                    var studentClasse = _connection.Table<Classe>().Where(classe => classe.id == student.classeId);
                    Classe studentClasseBis = await studentClasse.FirstAsync();
                    studentClasseBis.nbStudents = studentClasseBis.nbStudents + 1;
                    await _connection.UpdateAsync(studentClasseBis);

                    //insert the student 
                    await _connection.InsertAsync(student);

                    await DisplayAlert("alert", "Registration success", "OK");

                    await Navigation.PopAsync();
                }

            }
            else
            {
                await DisplayAlert("alert", "informations invalid", "OK");
            }
        }
    }
}
