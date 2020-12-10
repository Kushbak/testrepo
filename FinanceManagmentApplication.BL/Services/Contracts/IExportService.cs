using FinanceManagmentApplication.Filter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface IExportService
    {
        //Task<byte[]> RemittanceExport();

        //Task<byte[]> TransactionExport();

        Task<byte[]> FinanceActionsReport(PaginationFilter filter);


    }
}
