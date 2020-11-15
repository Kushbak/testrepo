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
        public int modelId { get; set; }

        public DateTime Date { get; set; }

        public int Sum { get; set; }

        public List<OperationIndexModel> Operations { get; set; }

        public int OperationId { get; set; }

        public List<ProjectIndexModel> Projects { get; set; }

        public int ProjectId { get; set; }

        public List<ScoreIndexModel> Scores { get; set; }

        public int ScoreId { get; set; }

        public int Score2Id { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }
    }
}
