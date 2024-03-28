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

        public static string DatabaseFile
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

        private static SQLiteConnection _connection { get; set; }

        public static SQLiteConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection(DatabaseFile);
                    _connection.CreateTable<ExpensesModel>();
                    _connection.CreateTable<UserModel>();
                    _connection.CreateTable<InstallmentModel>();
                }
                return _connection;
            }
        }

        public static void closeConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
