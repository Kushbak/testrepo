using FinanceManagmentApplication.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Helpers.TransactionEditStates.Contracts
{
    public abstract class TransactionEditState
    {
        protected int Expense = 2;

        protected int Income = 1;

        protected TransactionEditHelper Helper;

        protected TransactionEditHelperModel Model;

        public TransactionEditState(TransactionEditHelper helper)
        {
            Helper = helper;
            Model = Helper.Model;
        }

        public virtual void EditOperation()
        {   
            Model.NewScore.Balance += Model.NewOperationTypeId == Income ? Model.NewTransactionSum * 2 : 0;
            Model.NewScore.Balance -= Model.NewOperationTypeId == Expense ? Model.NewTransactionSum * 2 : 0;

            Helper.OperationEdit = true;

            Helper.State = new TransactionOperationEditState(Helper);
        }

        public virtual void EditScore()
        {
            Model.OldScore.Balance -= Model.NewOperationTypeId == Income ? Model.NewTransactionSum : 0;
            Model.OldScore.Balance += Model.NewOperationTypeId == Expense ? Model.NewTransactionSum : 0;

            Model.OldScore.Balance += Model.NewOperationTypeId == Income ? Model.NewTransactionSum : 0;
            Model.OldScore.Balance -= Model.NewOperationTypeId == Expense ? Model.NewTransactionSum : 0;

            Helper.ScoreEdit = true;

            Helper.State = new TransactionScoreEditState(Helper);
        }

        public virtual void EditSum()
        {
            Model.NewScore.Balance += Model.NewOperationTypeId == Income ? Model.GetTransactionDifNew_Old : 0;
            Model.NewScore.Balance -= Model.NewOperationTypeId == Expense ? Model.GetTransactionDifNew_Old : 0;

            Helper.SumEdit = true;

            Helper.State = new TransactionSumEditState(Helper);
        }
    }
}
