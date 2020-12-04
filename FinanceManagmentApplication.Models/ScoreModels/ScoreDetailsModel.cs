using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.PaymentType;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.ScoreModel
{
    public class ScoreDetailsModel
    {   
        public int Id { get; set; }

        public string Code { get; set; }

        public string PaymentType { get; set; }

        public string Name { get; set; }

        public int Sum { get; set; }

    }
}
