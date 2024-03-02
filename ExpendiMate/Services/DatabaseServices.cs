using ExpendiMate.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Services
{
    internal static class DatabaseServices
    {
        private static string _databaseFile;

        private static string DatabaseFile
        {
            get
            {
                if (_databaseFile == null)
                {
                    string databaseDir = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "data");
                    System.IO.Directory.CreateDirectory(databaseDir);
                    _databaseFile = Path.Combine(databaseDir, "expenses.sqlite");
                }
                return _databaseFile;
            }
        }

        private static SQLiteConnection _connection;

        public static SQLiteConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection(DatabaseFile);
                    _connection.CreateTable<ExpensesModel>();
                    _connection.CreateTable<IncomeModel>();
                    _connection.CreateTable<InstallmentModel>();
                }
                return _connection;
            }
        }
    }
}
