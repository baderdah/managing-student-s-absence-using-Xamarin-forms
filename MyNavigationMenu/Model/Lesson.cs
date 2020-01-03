using System;
using SQLite;

namespace MyNavigationMenu.Model
{
    public class Lesson
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100)]
        public String name { get; set; }
    }
}
