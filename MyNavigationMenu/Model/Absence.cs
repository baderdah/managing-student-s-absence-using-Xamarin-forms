using SQLite;

namespace MyNavigationMenu.Model
{
    public class Absence
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int studentId { get; set; }
        public int lessonId { get; set; }
        public int seanceId { get; set;}
        public bool isAbsent { get; set; }


    }
}
