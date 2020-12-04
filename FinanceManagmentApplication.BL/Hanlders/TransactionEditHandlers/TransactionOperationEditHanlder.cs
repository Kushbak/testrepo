using FinanceManagmentApplication.BL.Exceptions;
using FinanceManagmentApplication.BL.Handlers.TransactionEditHandlers.Contracts;
using FinanceManagmentApplication.Models.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Handlers.TransactionEditHandlers
{
    public class TransactionOperationEditHanlder : TransactionEditBaseHanlder
    {
        public TransactionOperationEditHanlder()
        {
            Successor = new TransactionScoreEditHandler();
        }
        public override void HandleRequest(TransactionEditHelperModel Model)
        {
            if (!Model.IsEditOperationType)
            {
                Successor.HandleRequest(Model);
                return;
            }

            if (Model.OldOperationTypeId == 1 && Model.OldScore.Balance < Model.OldTransactionSum
                && !Model.IsEditScore && !Model.IsEditSum)
                throw new TransactionException("Недостаточно средств на счете!");
            if (Model.NewOperationTypeId == 2 && Model.NewScore.Balance < Model.NewTransactionSum
                    && !Model.IsEditScore && !Model.IsEditSum)
                throw new TransactionException("Недостаточно средств на счете!");

            Model.NewScore.Balance += Model.NewOperationTypeId == Income ? Model.NewTransactionSum * 2 : 0;
            Model.NewScore.Balance -= Model.NewOperationTypeId == Expense ? Model.NewTransactionSum * 2 : 0;

            Successor.HandleRequest(Model);
        }
    }
}
