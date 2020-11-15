using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class Transaction: FinanceAction
    {

        public CounterParty CounterParty { get; set; }

        public int CounterPartyId { get; set; }

    }
}
