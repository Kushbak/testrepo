using FinanceManagmentApplication.Models.ScoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.RemittanceModels
{
    public class RemittanceEditModel
    {   
        public int Id { get; set; }

        public DateTime ActionDate { get; set; }

        public int Sum { get; set; }

        public string Score1Name { get; set; }

        public int ScoreId { get; set; }

        public string Score2Name { get; set; }

        public int Score2Id { get; set; }

        public string Description { get; set; }
    }
}
