using FinanceManagmentApplication.Exceptions;
using FinanceManagmentApplication.HelperModel;
using FinanceManagmentApplication.Helpers.TransactionEditHandlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Helpers.TransactionEditHandlers
{
    public class TransactionScoreEditHandler : TransactionEditBaseHanlder
    {
        public TransactionScoreEditHandler()
        {
            Successor = new TransactionSumEditHanlder();
        }

        public override void HandleRequest(TransactionEditHelperModel Model)
        {
            if (!Model.IsEditScore)
            {
                Successor.HandleRequest(Model);
                return;
            }
            

            if ((Model.NewScore.Balance < Model.NewTransactionSum) && Model.NewOperationTypeId == Expense
                && !Model.IsEditOperationType && !Model.IsEditSum)
                throw new TransactionException("Недостаточно средств на счете!");
            if ((Model.OldScore.Balance < Model.NewTransactionSum) && Model.NewOperationTypeId == Income
                && !Model.IsEditOperationType && !Model.IsEditSum)
                throw new TransactionException("Недостаточно средств на счете!");
            if (Model.NewOperationTypeId != Income && Model.NewOperationTypeId != Expense)
                throw new TransactionException("Нет такого типа операций!");


            Model.OldScore.Balance -= Model.OldOperationTypeId == Income ? Model.NewTransactionSum : 0;
            Model.OldScore.Balance += Model.OldOperationTypeId == Expense ? Model.NewTransactionSum : 0;

            Model.NewScore.Balance += Model.NewOperationTypeId == Income ? Model.NewTransactionSum : 0;
            Model.NewScore.Balance -= Model.NewOperationTypeId == Expense ? Model.NewTransactionSum : 0;


            Successor.HandleRequest(Model);
        }
    }
}
