using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.DAL.Repositories.Contracts
{
    public interface ICounterPartyRepository: IRepository<CounterParty>
    {
        bool Check(int Id);

        Task<int> GetNullCounterParty();
    }
}
