using System;
using SQLite;

namespace MyNavigationMenu.Model
{
    public class Classe
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(100)]
        public String name { get; set; }
       
        public int nbStudents { get; set; } = 0;
    }
}
