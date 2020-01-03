using System.Collections.Generic;
using MyNavigationMenu.Model;
using SQLite;
using Xamarin.Forms;
namespace MyNavigationMenu.Persistance
{
    public class DbOperations
    {
        private static SQLiteAsyncConnection _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        static public async void deleteClasse(Classe classe)
        {
            List<Seance> seances = await _connection.Table<Seance>()
                .Where(s => s.classeId == classe.id).ToListAsync();

            seances.ForEach(s => deleteSeance(s));

            await _connection.Table<Student>()
                .Where(stud => stud.classeId == classe.id)
                .DeleteAsync();

            await _connection.Table<Classe>()
                .Where(cl => cl.id == classe.id)
                .DeleteAsync();
        }

        static public async void deleteSeance(Seance seance)
        {
            await _connection.Table<Absence>()
                .Where(abs => abs.seanceId == seance.id)
                .DeleteAsync();

            await _connection.Table<Seance>()
                .Where(s => s.id == seance.id)
                .DeleteAsync();
        }

        static public async void deleteLesson(Lesson lesson)
        {
            await _connection.Table<Absence>()
                .Where(abs => abs.lessonId == lesson.id)
                .DeleteAsync();

            await _connection.Table<Lesson>()
                .Where(less => less.id == lesson.id)
                .DeleteAsync();
        }

    }
}
