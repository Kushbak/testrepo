using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.TransactionModels
{
    public class TransactionExcelModel
    {
        public int Id { get; set; }

        public DateTime ActionDate { get; set; }

        public int Sum { get; set; }

        public string OperationName { get; set; }

        public string TransactionType { get; set; }

        public string ProjectName { get; set; }

        public string Score { get; set; }

        public string CounterPartyName { get ; set; }

    }
}
