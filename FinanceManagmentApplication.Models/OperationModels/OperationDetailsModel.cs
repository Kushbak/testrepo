using FinanceManagmentApplication.Models.OperationTypeModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.Models.OperationModels
{
      public  class OperationDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OperationTypeIndexModel> operationTypes { get; set; }

        public int OperationTypeId { get; set; }
    }
}
