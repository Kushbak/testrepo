using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Exceptions
{
    public class RemittanceException: Exception
    {
        public RemittanceException(string Message): base(Message)
        {

        }
    }
}
