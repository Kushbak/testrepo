using FinanceManagmentApplication.HelperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Helpers.RemittanceEditHandlers.Contracts
{
    public abstract class RemittanceEditBaseHandler
    {
        public RemittanceEditBaseHandler Successor { get; set; }
        public abstract void HandleRequest(RemittanceEditHelperModel Model);
    }
}
