using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class Project: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
