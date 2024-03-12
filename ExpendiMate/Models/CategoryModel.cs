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

        //public static Dictionary<string, Color> Category = new Dictionary<string, Color> 
        //{
        //    { "Food",       Color.FromRgba(0, 255, 0, 1) },
        //    { "Groceries",  Color.FromRgba(0, 0, 255, 1) },
        //    { "Transport",  Color.FromRgba(255, 255, 0, 1) },
        //    { "Gifts",      Color.FromRgba(255, 102, 255, 1) },
        //    { "Healthcare", Color.FromRgba(255, 0, 0, 1) },
        //    { "Leisure",    Color.FromRgba(127, 0, 255, 1) },
        //    { "Education",  Color.FromRgba(255, 128, 0, 1) },
        //    { "Bills",      Color.FromRgba(0, 255, 255, 1) }
        //};
        public static string[] CategoryList = { "Food", "Groceries", "Transport", "Gifts", "Health", "Leisure", "Education", "Bills"};

        public static Dictionary<string, string> Category = new Dictionary<string, string>
        {
            { "Food",       "#FFDD29" },
            { "Groceries",  "#008AD7" },
            { "Transport",  "#26DF16" },
            { "Gifts",      "#58FF59" },
            { "Health",     "#5029FF" },
            { "Leisure",    "#FF8729" },
            { "Education",  "#6454AA" },
            { "Bills",      "#548054" }
        };
    }
}
