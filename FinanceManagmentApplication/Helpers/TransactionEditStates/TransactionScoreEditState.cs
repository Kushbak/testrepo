using FinanceManagmentApplication.Helpers.TransactionEditStates.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Helpers.TransactionEditStates
{
    public class TransactionScoreEditState: TransactionEditState
    {
        public TransactionScoreEditState(TransactionEditHelper helper) : base(helper)
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
            throw new NotImplementedException();
        }

        public override void EditSum()
        {
            Helper.Model.NewScore.Balance += Helper.Model.NewOperationTypeId == 1 ? Helper.Model.GetTransactionDifNew_Old : 0;
            Helper.Model.NewScore.Balance -= Helper.Model.NewOperationTypeId == 2 ? Helper.Model.GetTransactionDifNew_Old : 0;

            Helper.State = new TransactionSumEditState(Helper);
        }
    }
}
