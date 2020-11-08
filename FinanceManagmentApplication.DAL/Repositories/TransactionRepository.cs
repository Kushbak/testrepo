using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FinanceManagmentApplication.DAL.Repositories
{
    public class TransactionRepository: Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            DbSet = applicationDbContext.Transactions;
        }



        public bool CheckToScore(int ScoreId)
        {
            return DbSet.Any(i => i.ScoreId == ScoreId);
        }

        public bool CheckTransactionToOperation(int OperationId)
        {
            return DbSet.Any(i => i.OperationId == OperationId);
        }

        public bool CheckTransactionToCounterPart(int CounterPartyId)
        {
            return DbSet.Any(i => i.CounterPartyId == CounterPartyId);
        }

        public Transaction GetFullTransaction(int Id)
        {
            return DbSet.Where(i => i.Id == Id)
                .Include(i => i.Operation)
                .Include(i => i.Project)
                .Include(i => i.Score)
                .Include(i => i.CounterParty)
                .Include(i => i.User)
                .FirstOrDefault();
        }

        public List<Transaction> GetTransactionsToIndex()
        {
            return DbSet
                .Include(i => i.Operation)
                .Include(i => i.Project)
                .Include(i => i.Score)
                .Include(i => i.CounterParty)
                .Include(i => i.Operation.OperationType)
                .ToList();
        }

        public List<Transaction> GetTransactionToOperation(int ProjectId)
        {
            return DbSet
                .Where(i => i.ProjectId == ProjectId)
                .Include(i => i.Operation)
                .ToList();
        }

    }
}
