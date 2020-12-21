using FinanceManagmentApplication.Models.HelperModel;
using FinanceManagmentApplication.BL.Handlers.TransactionEditHandlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace FinanceManagmentApplication.BL.Handlers.TransactionEditHandlers
{
    public class TransactionSumEditHanlder : TransactionEditBaseHanlder
    {
        public override void HandleRequest(TransactionEditHelperModel Model)
        {
            if (!Model.IsEditSum)
                return;
            
            Model.NewScore.Balance += Model.NewOperationTypeId == Income ? Model.GetTransactionDifNew_Old : 0;
            Model.NewScore.Balance -= Model.NewOperationTypeId == Expense ? Model.GetTransactionDifNew_Old : 0;

            if (Model.IsEditScore)
            {
                Model.OldScore.Balance -= Model.NewOperationTypeId == Income ? Model.GetTransactionDifNew_Old : 0;
                Model.OldScore.Balance += Model.NewOperationTypeId == Expense ? Model.GetTransactionDifNew_Old : 0;

                if (Model.OldScore.Balance < 0)
                    throw new TransactionException("Недостаточно средств на счете!");
            }

            if (Model.NewScore.Balance < 0)
                throw new TransactionException("Недостаточно средств на счете!");
        }
    }
}
