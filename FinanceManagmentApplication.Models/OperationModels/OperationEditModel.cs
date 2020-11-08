using FinanceManagmentApplication.Models.OperationTypeModels;
using FinanceManagmentApplication.Models.TransactionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.OperationModels
{
    public class OperationEditModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int OperationTypeId { get; set; }

        public List<OperationTypeIndexModel> operationTypes{ get; set; }
    }
}
