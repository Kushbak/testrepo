using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class CounterParty: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsCompany { get; set; }

        public User User { get; set; }

        public int? UserId { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        public bool IsDelete { get; set; }
    }
}
