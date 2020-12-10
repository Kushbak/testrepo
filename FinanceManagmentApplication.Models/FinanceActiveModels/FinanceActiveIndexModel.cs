using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.FinanceActiveModels
{
    public class FinanceActiveIndexModel
    {
        public int Id { get; set; }

        public string ActionDate { get; set; }

        public int Sum { get; set; }

        public string UserName { get; set; }

        public string OperationName { get; set; }

        public string TransactionType { get; set; }

        public string ProjectName { get; set; }

        public string Score { get; set; }

        public string TargetEntity { get; set; }

        public string Discriminator { get; set; }
    }
}
