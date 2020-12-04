using FinanceManagmentApplication.BL.Exceptions;
using FinanceManagmentApplication.Models.HelperModel;
using FinanceManagmentApplication.BL.Handlers.RemittanceEditHandlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FinanceManagmentApplication.BL.HandlersRemittanceEditHandlers
{
    public class RemittanceEditSumHandler : RemittanceEditBaseHandler
    {
        public override void HandleRequest(RemittanceEditHelperModel Model)
        {
            if (!Model.IsSumEdit)
                return;

            var TransactionDif = Model.GetTransactionDifNew_Old;
            if (Model.NewScore1.Balance < TransactionDif)
                throw new RemittanceException($"на счету {Model.NewScore2.Code} недостаточно денег!");
            if (Model.NewScore2.Balance < Math.Abs(TransactionDif) && TransactionDif < 0)
                throw new RemittanceException( $"на счету {Model.NewScore2.Code} недостаточно денег!");

            Model.NewScore1.Balance -= TransactionDif;
            Model.NewScore2.Balance += TransactionDif;
        }
    }
}
