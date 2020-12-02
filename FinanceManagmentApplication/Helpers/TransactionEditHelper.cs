using FinanceManagmentApplication.HelperModel;
using FinanceManagmentApplication.Helpers.TransactionEditStates.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Helpers
{
    public class TransactionEditHelper
    {     
        public TransactionEditState State { get; set; }

        public TransactionEditHelperModel Model { get; set; }

        public bool SumEdit;
        public bool ScoreEdit;
        public bool OperationEdit;
    }
}
