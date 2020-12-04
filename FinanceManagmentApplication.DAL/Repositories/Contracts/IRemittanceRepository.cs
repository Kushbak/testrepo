using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Repositories.Contracts
{
    public interface IRemittanceRepository: IRepository<Remittance>
    {
        List<Remittance> GetFullRemittances();

        List<Remittance> GetPaginationTransactions(int PageNumber, int PageSize);

        (int, int) GetRemmiranceScoreId(int Id);

        Remittance GetFullRemittanceToScore(int Id);
    }
}
