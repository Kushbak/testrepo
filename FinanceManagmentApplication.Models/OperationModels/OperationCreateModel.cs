using FinanceManagmentApplication.Models.OperationTypeModels;
using FinanceManagmentApplication.Models.TransactionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.OperationModels
{
    public class OperationCreateModel
    {
        public string Name { get; set; }
        public List<OperationTypeIndexModel> operationTypes { get; set; }
        public int OperationTypeId { get; set; }
    }
}
