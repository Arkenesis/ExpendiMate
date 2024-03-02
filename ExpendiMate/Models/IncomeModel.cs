﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpendiMate.Models
{
    //Income
    public class IncomeModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public double Income { get; set; } = 0;
        public double BudgetPercent { get; set; }
        public double Budget
        {
            get
            {
                return (double)Income / BudgetPercent;
            }
        }
        public double BudgetBalance { get; set; }
    }
}