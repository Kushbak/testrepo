using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.PaymentType;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.ScoreModel
{
    public class ScoreCreateModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public List<PaymentTypeIndexModel> paymentTypes { get; set; }

        public int PaymentTypeId { get; set; }

    }
}
