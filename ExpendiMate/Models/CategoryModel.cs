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
        public static string[] CategoryList = { "Food", "Groceries", "Transport", "Gifts", "Health", "Leisure", "Education", "Bills"};

        public static Dictionary<string, string> Category = new Dictionary<string, string>
        {
            { "Food",       "#FF6400" },
            { "Groceries",  "#F3F200" },
            { "Transport",  "#0000FF" },
            { "Gifts",      "#FF00FF" },
            { "Health",     "#FF0000" },
            { "Leisure",    "#00FFFF" },
            { "Education",  "#9B00D2" },
            { "Bills",      "#32BB00" }
        };
    }
}
