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
                System.Diagnostics.Debug.WriteLine("WHERE IS MY FILE: "+_databaseFile);
                var file = new FileInfo(_databaseFile);
                System.Diagnostics.Debug.WriteLine("File Size in byte : " + (double) file.Length);
                System.Diagnostics.Debug.WriteLine("File Size in Kb : " + (double) file.Length/1024);
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
                    _connection.CreateTable<ExpenseItemModel>();
                    _connection.CreateTable<UserModel>();
                    _connection.CreateTable<InstallmentModel>();
                }
                return _connection;
            }
        }
    }
}
