using System;
using SQLite;

namespace MyNavigationMenu.Persistance
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
