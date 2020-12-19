using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class OperationType: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDelete { get; set; }

        public IEnumerable<Operation> Operations { get; set; }

    }
}
