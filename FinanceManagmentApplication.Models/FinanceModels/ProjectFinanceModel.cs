using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.FinanceModels
{
    public class ProjectFinanceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Income { get; set; }

        public int Expense { get; set; }

        public int Profit { get; set; }
    }
}
