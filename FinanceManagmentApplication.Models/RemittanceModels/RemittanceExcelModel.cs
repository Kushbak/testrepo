using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.RemittanceModels
{
    public class RemittanceExcelModel
    {
        public int Id { get; set; }

        public DateTime ActionDate { get; set; }

        public int Sum { get; set; }

        public string Score { get; set; }

        public string Score2 { get; set; }
    }
}
