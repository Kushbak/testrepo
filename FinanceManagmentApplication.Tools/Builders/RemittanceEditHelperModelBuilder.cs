using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Models.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Tools.Builders
{
    public class RemittanceEditHelperModelBuilder
    {
        private RemittanceEditHelperModel Model;

        public RemittanceEditHelperModelBuilder()
        {
            Model = new RemittanceEditHelperModel();
        }

        public RemittanceEditHelperModelBuilder SetOldScore1(Score score)
        {
            Model.OldScore1 = score;
            return this;
        }

        public RemittanceEditHelperModelBuilder SetNewScore1(Score score)
        {
            Model.NewScore1 = score;
            return this;
        }

        public RemittanceEditHelperModelBuilder SetOldScore2(Score score)
        {
            Model.OldScore2 = score;
            return this;
        }

        public RemittanceEditHelperModelBuilder SetNewScore2(Score score)
        {
            Model.NewScore2 = score;
            return this;
        }

        public RemittanceEditHelperModelBuilder SetOldTransactionSum(int sum)
        {
            Model.OldTransactionSum = sum;
            return this;
        }

        public RemittanceEditHelperModelBuilder SetNewTransactionSum(int sum)
        {
            Model.NewTransactionSum = sum;
            return this;
        }

        public static implicit operator RemittanceEditHelperModel(RemittanceEditHelperModelBuilder builder)
        {
            return builder.Model;
        }

        public RemittanceEditHelperModel Build()
        {
            return Model;
        }
    }
}
