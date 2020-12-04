using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Exceptions
{
    public class TransactionException : Exception
    {
        public TransactionException(string Message) : base(Message)
        {

        }
    }
}
