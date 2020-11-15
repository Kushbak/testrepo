using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Entities
{
    public class Remittance: FinanceAction
    {
        public virtual Score Score2 { get; set; }

        public virtual int Score2Id { get; set; }
    }
}
