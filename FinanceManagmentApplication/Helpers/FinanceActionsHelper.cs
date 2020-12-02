using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Exceptions;
using FinanceManagmentApplication.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Helpers
{
    public class FinanceActionsHelper
    {
        private static void ScoreEdit(Score OldScore, Score NewScore, int TransactionSum, bool IsFirstScore)
        {
            if ((NewScore.Balance < TransactionSum) && IsFirstScore)
                throw new RemittanceException($"На счету {NewScore.Code} недостаточно денег.");
            if ((OldScore.Balance < TransactionSum) && !IsFirstScore)
                throw new RemittanceException($"На счету {OldScore.Code} недостаточно денег.");
            int Sum = IsFirstScore ? TransactionSum : -TransactionSum;
            OldScore.Balance += Sum;
            NewScore.Balance -= Sum;
        }

        private static void TransactionScoreEdit(Score OldScore, Score NewScore, int TransactionSum, int OperationTypeId)
        {
            int Expense = 2;
            int Income = 1;

            if ((NewScore.Balance < TransactionSum) && OperationTypeId == Expense)
                throw new RemittanceException($"На счету {NewScore.Code} недостаточно денег.");
            if ((OldScore.Balance < TransactionSum) && OperationTypeId == Income)
                throw new RemittanceException($"На счету {OldScore.Code} недостаточно денег.");
            if (OperationTypeId != Income && OperationTypeId != Expense)
                return;

            OldScore.Balance += OperationTypeId == Expense ? +TransactionSum : -TransactionSum;
            NewScore.Balance -= OperationTypeId == Income ? +TransactionSum: -TransactionSum;
        }

        public static void TransactionSumEdit(TransactionEditHelperModel model)
        {
            int Expense = 2;
            int Income = 1;

            model.NewScore.Balance += model.NewOperationTypeId == Income ? model.GetTransactionDifNew_Old : 0;
            model.NewScore.Balance += model.NewOperationTypeId == Expense ? -model.GetTransactionDifNew_Old : 0;
        }

        public static void TransactionOperationEdit(TransactionEditHelperModel model)
        {
            int Expense = 2;
            int Income = 1;
            if (model.NewOperationTypeId == Expense)
            {
                model.NewScore.Balance -= (model.OldTransactionSum + model.NewTransactionSum);
            }
            if (model.NewOperationTypeId == Income)
            {
                model.NewScore.Balance += (model.OldTransactionSum + model.NewTransactionSum);
            }
        }

        public static (bool, string) TwoScoresEdit(RemittanceEditHelperModel model)
        {
            try
            {
                if (model.OldScore1 != null)
                    ScoreEdit(model.OldScore1, model.NewScore1, model.OldTransactionSum, true);
                if (model.OldScore2 != null)
                    ScoreEdit(model.OldScore2, model.NewScore2, model.OldTransactionSum, false);
                return (true, null);
            }
            catch (RemittanceException e)
            {
                return (false, e.Message);
            }
        }

        public static (bool, string) SumEdit(RemittanceEditHelperModel model)
        {
            var TransactionDif = model.GetTransactionDifNew_Old;
            if (model.NewScore1.Balance < TransactionDif)
                return (false, $"на счету {model.NewScore1.Code} недостаточно денег!");
            if(model.NewScore2.Balance < Math.Abs(TransactionDif) && TransactionDif < 0)
                return (false, $"на счету {model.NewScore2.Code} недостаточно денег!");

            model.NewScore1.Balance -= TransactionDif;
            model.NewScore2.Balance += TransactionDif;
            return (true, null);
        }

        public static (bool, string) TwoSumAndScoreEdit(RemittanceEditHelperModel model)
        {
            try
            {
                if (model.OldScore1 != null)
                    SumAndScoreEdit(model.OldScore1, model.NewScore1, model.OldTransactionSum, model.NewTransactionSum, true);
                if (model.OldScore2 != null)
                    SumAndScoreEdit(model.OldScore2, model.NewScore2, model.OldTransactionSum, model.NewTransactionSum, false);
                return (true, null);
            }
            catch (RemittanceException e)
            {
                return (false, e.Message);
            }
        }

        public static void SumAndScoreEdit(Score OldScore, Score NewScore, int TransactionSum, int TransactionNewSum, bool IsFirstScore)
        {
            if (NewScore.Balance < TransactionNewSum && IsFirstScore)
                throw new RemittanceException($"На счете {NewScore.Code} недостаточно денег!");
            if(OldScore.Balance < TransactionSum && !IsFirstScore)
                throw new RemittanceException($"На счете {OldScore.Code} недостаточно денег!");

            var OldSum = IsFirstScore ? TransactionSum : -TransactionSum;
            var NewSum = IsFirstScore ? TransactionNewSum : -TransactionNewSum;
            OldScore.Balance += OldSum;
            NewScore.Balance -= NewSum;
        }


    }
}
