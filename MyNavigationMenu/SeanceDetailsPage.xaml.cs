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
    public class studentDetails
    {
        public Student st { get; set; }
        public Boolean absent { get; set; }
        public int nbOfAbs { get; set; }

        public studentDetails(Student st, Boolean ab, int nbOfAbsenceInthisLesson)
        {
            this.st = st;
            this.absent = ab;
            this.nbOfAbs = nbOfAbsenceInthisLesson;
        }
    }

    public partial class SeanceDetailsPage : ContentPage
    {
        private Seance seance;
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<studentDetails> studentsDetailsObsColl;
        private List<int> stateChangedStudentId;
        public SeanceDetailsPage(Seance seance)
        {
            InitializeComponent();
            studentsDetailsObsColl = new ObservableCollection<studentDetails>();
            this.seance = seance;
            stateChangedStudentId = new List<int>();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        async protected override void OnAppearing()
        {
            List<Absence> absence  = await _connection.Table<Absence>().Where(abs => abs.lessonId == seance.lessonId).ToListAsync();
            List<Student> students = await _connection.Table<Student>().Where(st => st.classeId == this.seance.classeId).ToListAsync();

            foreach (Student st in students)
            {
                List<Absence> ListAbs = (from ab in absence where ab.studentId == st.id && ab.isAbsent == true select ab).ToList<Absence>();
                    
                int nbAbs = ListAbs.Count;

                Absence abs = (from ab in absence where ab.studentId == st.id && ab.seanceId == seance.id select ab).FirstOrDefault();

                if(abs == null)
                {
                    Absence newabs = new Absence();
                    newabs.isAbsent = false;
                    newabs.lessonId = seance.lessonId;
                    newabs.seanceId = seance.id;
                    newabs.studentId = st.id;
                    await _connection.InsertAsync(abs);
                    studentsDetailsObsColl.Add(new studentDetails(st, false, nbAbs));
                }
                else
                {
                    studentsDetailsObsColl.Add(new studentDetails(st, abs.isAbsent, nbAbs));
                }
            }
            seancesListView.ItemsSource = studentsDetailsObsColl;
        }

        void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            Switch switchh = sender as Switch;
            studentDetails studentdetails = switchh.BindingContext as studentDetails;

            if (!stateChangedStudentId.Contains(studentdetails.st.id))
            {
                stateChangedStudentId.Add(studentdetails.st.id);
            }

            var stDetail = studentsDetailsObsColl.FirstOrDefault(std => std.st.id == studentdetails.st.id);

            stDetail.absent = switchh.IsToggled;
        }
        async void markAbsent_Clicked(System.Object sender, System.EventArgs e)
        {
            foreach(int id in stateChangedStudentId)
            {
                var stDetail = studentsDetailsObsColl.FirstOrDefault(std => std.st.id == id);
                var abs = await _connection.Table<Absence>().Where(aabs => aabs.studentId == id && aabs.seanceId == seance.id).FirstOrDefaultAsync();
                    
                    abs.isAbsent = stDetail.absent;
                    int nb = await _connection.UpdateAsync(abs);
            }

        }
    }
}
