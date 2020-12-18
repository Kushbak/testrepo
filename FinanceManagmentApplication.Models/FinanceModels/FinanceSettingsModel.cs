using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.ScoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.FinanceModels
{
    public class FinanceSettingsModel
    {
        public List<OperationIndexModel> Operations { get; set; }

        public List<ScoreIndexModel> Scores { get; set; }

        public List<ProjectIndexModel> Projects { get; set; }

        public List<CounterPartyIndexModel> CounterParties { get; set; }
    }
}
