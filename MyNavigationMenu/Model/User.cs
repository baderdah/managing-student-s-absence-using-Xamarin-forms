using System;
using SQLite;

namespace MyNavigationMenu.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(100)]
        public String username { get; set; }

        [MaxLength(100)]
        public String password { get; set; }

        [MaxLength(100)]
        public String firstName { get; set; }

        [MaxLength(100)]
        public String lastName { get; set; }

    }
}
