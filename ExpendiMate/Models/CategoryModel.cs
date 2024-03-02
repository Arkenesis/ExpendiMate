using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace ExpendiMate.Models
{
    public class CategoryModel
    {
        public static readonly CategoryModel Food = new CategoryModel 
        { 
            CategoryId = 1,
            CategoryName = "Food",
            CategoryColor = Color.FromRgba(0, 255, 0, 1), 
        };

        public static readonly CategoryModel Groceries = new CategoryModel 
        { 
            CategoryId = 2,
            CategoryName = "Groceries",
            CategoryColor = Color.FromRgba(0, 0, 255, 1)
        };

        public static readonly CategoryModel Transport = new CategoryModel
        {
            CategoryId = 3,
            CategoryName = "Transport",
            CategoryColor = Color.FromRgba(255, 255, 0, 1)
        };

        public static readonly CategoryModel Gifts = new CategoryModel
        {
            CategoryId = 4,
            CategoryName = "Gifts",
            CategoryColor = Color.FromRgba(255, 102, 255, 1)
        };

        public static readonly CategoryModel Healthcare = new CategoryModel
        {
            CategoryId = 5,
            CategoryName = "Healthcare",
            CategoryColor = Color.FromRgba(255, 0, 0, 1)
        };

        public static readonly CategoryModel Leisure = new CategoryModel
        {
            CategoryId = 6,
            CategoryName = "Leisure",
            CategoryColor = Color.FromRgba(127, 0, 255, 1)
        };

        public static readonly CategoryModel Education = new CategoryModel
        {
            CategoryId = 7,
            CategoryName = "Education",
            CategoryColor = Color.FromRgba(255, 128, 0, 1)
        };

        public static readonly CategoryModel Bills = new CategoryModel
        {
            CategoryId = 8,
            CategoryName = "Bills",
            CategoryColor = Color.FromRgba(0, 255, 255, 1)
        };

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public Color CategoryColor { get; set; }
        //public int CategoryCount { get; set; } = 0;
        public List<ExpenseItemModel> ExpenseItems { get; set; } = new List<ExpenseItemModel>();
    }
}
