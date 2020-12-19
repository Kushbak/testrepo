using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class User: IdentityUser<int>,  IEntity
    {
        public string? Surname { get; set; }
        public string? Name { get; set; }

        public IEnumerable<CounterParty> CounterParties { get; set; }
        public IEnumerable<FinanceAction> Transactions { get; set; }

        public bool IsDelete { get; set; }
    }
}
