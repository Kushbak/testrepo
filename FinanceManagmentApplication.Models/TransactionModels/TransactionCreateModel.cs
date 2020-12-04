using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.ScoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.TransactionModels
{
    public class TransactionCreateModel
    {   

        public DateTime ActionDate { get; set; }

        public int Sum { get; set; }

        public int OperationId { get; set; }

      // public int UserId { get; set; }

        public int ProjectId { get; set; }

        public int CounterPartyId { get; set; }

        public int ScoreId { get; set; }

        public string Description { get; set; }
    }
}
