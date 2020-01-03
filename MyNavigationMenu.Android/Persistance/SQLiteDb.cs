using System;
using System.IO;
using MyNavigationMenu.Droid.Persistance;
using MyNavigationMenu.Persistance;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace MyNavigationMenu.Droid.Persistance
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteDb()
        {
        }

        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
   
}
