using FinanceManagmentApplication.BL.Handlers.RemittanceEditHandlers.Contracts;
using FinanceManagmentApplication.BL.Exceptions;
using FinanceManagmentApplication.Models.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FinanceManagmentApplication.BL.HandlersRemittanceEditHandlers
{
    public class RemittanceEditScoreHanlder : RemittanceEditBaseHandler
    {
        public RemittanceEditScoreHanlder()
        {
            Successor = new RemittanceEditSumHandler();
        }

        public override void HandleRequest(RemittanceEditHelperModel Model)
        {
            if (Model.IsScore1Edit)
            {
                ScoreEdit(Model, IsFirstScore:true);   
            }

            if (Model.IsScore2Edit)
            {
                ScoreEdit(Model, IsFirstScore: false);
            }

            Successor.HandleRequest(Model);

        }

        private static void ScoreEdit(RemittanceEditHelperModel Model, bool IsFirstScore)
        {

            var NewScore = IsFirstScore ? Model.NewScore1 : Model.NewScore2;
            var OldScore = IsFirstScore ? Model.OldScore1 : Model.OldScore2;
            if ((NewScore.Balance < Model.NewTransactionSum) && IsFirstScore && !Model.IsSumEdit)
                throw new RemittanceException($"На счету {NewScore.Code} недостаточно денег.");
            if ((OldScore.Balance < Model.NewTransactionSum) && !IsFirstScore && !Model.IsSumEdit)
                throw new RemittanceException($"На счету {OldScore.Code} недостаточно денег.");
            int Sum = IsFirstScore ? Model.NewTransactionSum : -Model.NewTransactionSum;
            OldScore.Balance += Sum;
            NewScore.Balance -= Sum;
        }
    }
}
