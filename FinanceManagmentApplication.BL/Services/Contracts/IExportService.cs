using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface IExportService
    {
        Task<byte[]> RemittanceExport(string Path, string Name, string format);

        Task<byte[]> TransactionExport(string Path, string Name, string format);
    }
}
