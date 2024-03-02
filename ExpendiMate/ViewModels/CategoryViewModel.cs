using ExpendiMate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.ViewModels
{
    internal class CategoryViewModel
    {
        public ObservableCollection<CategoryModel> Categories { get; } = new ObservableCollection<CategoryModel>();

        public void AddCategory(string CategoryName, Color CategoryColor)
        {
            var NewCategory = new CategoryModel
            {
                CategoryId = Categories.Count + 1,
                CategoryName = CategoryName,
                CategoryColor = CategoryColor
            };

            Categories.Add(NewCategory);
        }
    }
}
