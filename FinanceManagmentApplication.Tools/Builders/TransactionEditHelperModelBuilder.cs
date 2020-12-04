using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Tools.Builders
{
    public class TransactionEditHelperModelBuilder
    {
        private TransactionEditHelperModel Model;

        public TransactionEditHelperModelBuilder()
        {
            Model = new TransactionEditHelperModel();
        }

        public TransactionEditHelperModelBuilder SetOldScore1(Score score)
        {
            Model.OldScore = score;
            return this;
        }

        public TransactionEditHelperModelBuilder SetNewScore1(Score score)
        {
            Model.NewScore = score;
            return this;
        }

        public TransactionEditHelperModelBuilder SetOldTransactionSum(int sum)
        {
            Model.OldTransactionSum = sum;
            return this;
        }

        public TransactionEditHelperModelBuilder SetNewTransactionSum(int sum)
        {
            Model.NewTransactionSum = sum;
            return this;
        }

        public TransactionEditHelperModelBuilder SetNewOperationTypeId(int Id)
        {
            Model.SetNewOperationTypeId = Id;
            return this;
        }

        public TransactionEditHelperModelBuilder SetOldOperationTypeId(int Id)
        {
            Model.SetOldOperationTypeId = Id;
            return this;
        }

        public TransactionEditHelperModelBuilder SetDefaultTransactionSum(int sum)
        {
            Model.SetDefaultTransactionSum = sum;
            return this;
        }

        public TransactionEditHelperModelBuilder SetDefaultOperationTypeId(int Id)
        {
            Model.SetDefaultOperationTypeId = Id;
            return this;
        }

        public TransactionEditHelperModelBuilder SetDefailtScoreTypeId(Score score)
        {
            Model.SetDefaultScore = score;
            return this;
        }

        public static implicit operator TransactionEditHelperModel(TransactionEditHelperModelBuilder builder)
        {
            return builder.Model;
        }

        public TransactionEditHelperModel Build()
        {
            return Model;
        }
    }
}
