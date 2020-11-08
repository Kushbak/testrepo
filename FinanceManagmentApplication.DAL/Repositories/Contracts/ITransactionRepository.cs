using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Repositories.Contracts
{
    public interface ITransactionRepository: IRepository<Transaction>
    {
        bool CheckTransactionToOperation(int Id);

        bool CheckToScore(int Id);

        Transaction GetFullTransaction(int Id);

        List<Transaction> GetTransactionsToIndex();

        List<Transaction> GetTransactionToOperation(int ProjectId);

        bool CheckTransactionToCounterPart(int CounterPartyId);
    }
}
