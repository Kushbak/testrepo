using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class User: IdentityUser<int>,  IEntity
    {
        public IEnumerable<CounterParty> CounterParties { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
