using CommunityToolkit.Mvvm.ComponentModel;
using ExpendiMate.Models;
using ExpendiMate.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.ViewModels
{
    public partial class CategoryViewModel : ObservableObject
    {
        public static CategoryViewModel Current { get; set; }

        SQLiteConnection connection;
        public ObservableCollection<ExpensesModel> CategoryItems { get; set; } = new();

        List<ExpensesModel> list;

        [ObservableProperty]
        string iconColor;

        public CategoryViewModel(ExpensesYearModel model)
        {
            Current = this;
            connection = DatabaseServices.Connection;
            list = model;
            foreach (var item in list)
            {
                CategoryItems.Add(item);
            }
        }

        public CategoryViewModel(ExpensesCategoryModel model)
        {
            Current = this;
            connection = DatabaseServices.Connection;
            list = model;
            IconColor = model.IconColor;
            foreach(var item in list)
            {
                CategoryItems.Add(item);
            }
        }

        public double TotalCost
        {
            get
            {
                double sum = 0;
                foreach (var item in list)
                {
                    sum = sum + item.ExpenseCost;
                }
                return sum;
            }
        }

    }
}
