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
