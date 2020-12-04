using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.ScoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.RemittanceModels
{
    public class RemittanceCreateModel
    {

        public DateTime ActionDate { get; set; }

        public int Sum { get; set; }

        public int ScoreId { get; set; }

        public int Score2Id { get; set; }

        //public int UserId { get; set; }

        public string Description { get; set; }
    }
}
