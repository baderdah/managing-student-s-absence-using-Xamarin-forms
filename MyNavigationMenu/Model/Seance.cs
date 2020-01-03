using System;
using SQLite;

namespace MyNavigationMenu.Model
{
    public class Seance
    {
        public static int lastId { get; set; } = 0;

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String date { get; set; }
        public int lessonId { get; set; }
        public int classeId { get; set; }
        public Seance(String date, int lessonId, int classeId) {
            this.date = date;
            this.lessonId = lessonId;
            this.classeId = classeId;
        }
        public Seance(){ }
    }
}
