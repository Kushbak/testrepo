using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class Operation: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public OperationType OperationType { get; set; }

        public int OperationTypeId { get; set; }

        public IEnumerable<FinanceAction> Transactions { get; set; }

        public bool IsDelete { get; set; }
    }
}
