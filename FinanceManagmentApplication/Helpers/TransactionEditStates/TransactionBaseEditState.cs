using FinanceManagmentApplication.Helpers.TransactionEditStates.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Helpers.TransactionEditStates
{
    public class TransactionBaseEditState: TransactionEditState
    {
        public TransactionBaseEditState(TransactionEditHelper helper) : base(helper)
        { 
        
        }

        public override void EditOperation()
        {
            if (Model.OldOperationTypeId == 1 && Model.OldScore.Balance < Model.OldTransactionSum
                && !Model.IsEditScore && !Model.IsEditSum && Helper.SumEdit && Helper.ScoreEdit)
                return;
            if (Model.NewOperationTypeId == 2 && Model.NewScore.Balance < Model.NewTransactionSum
                    && !Model.IsEditScore && !Model.IsEditSum && Helper.SumEdit && Helper.ScoreEdit)
                return;
            base.EditOperation();
        }

        public override void EditScore()
        {
            if ((Model.NewScore.Balance < Model.NewTransactionSum) && Model.NewOperationTypeId == Expense
                && !Model.IsEditOperationType && !Model.IsEditSum && Helper.SumEdit && Helper.OperationEdit)
                return;
            if ((Model.OldScore.Balance < Model.NewTransactionSum) && Model.NewOperationTypeId == Income
                && !Model.IsEditOperationType && !Model.IsEditSum && Helper.SumEdit && Helper.OperationEdit)
                return;
            if (Model.NewOperationTypeId != Income && Model.NewOperationTypeId != Expense)
                return;
            base.EditScore();
        }

        public override void EditSum()
        {
            base.EditSum();
        }
    }
}
