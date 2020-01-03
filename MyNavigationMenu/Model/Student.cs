using System;
using SQLite;

namespace MyNavigationMenu.Model
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int cne { get; set; }

        [MaxLength(100)]
        public string firstName { get; set; }

        [MaxLength(100)]
        public string lastName { get; set; }

        [MaxLength(100)]
        public string email { get; set; }

        [MaxLength(100)]
        public string phone { get; set; }

        [MaxLength(100)]
        public int classeId { get; set; }
    }
}
