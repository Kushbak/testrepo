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

        public List<Transaction> GetPaginationTransactions(int PageNumber, int PageSize, DateTime? Date, int? OperationId, int? ProjectId, int? ScoreId, int? CounterPartyId, int? UserId)
        { 
            return DbSet.Where(i => Date == null || Date.Value == i.ActionDate )
                .Where(i => OperationId == null || OperationId.Value == i.OperationId)
                .Where(i => ProjectId == null || ProjectId.Value == i.ProjectId)
                .Where(i => ScoreId == null || ScoreId.Value == i.ScoreId)
                .Where(i => UserId == null || UserId.Value == i.UserId)
                .Where(i => CounterPartyId == null || CounterPartyId.Value == i.CounterPartyId)
                .Skip((PageNumber - 1) * PageSize)
                       .Take(PageSize)
                       .Include(i => i.Operation)
                        .Include(i => i.Project)
                        .Include(i => i.Score)
                        .Include(i => i.CounterParty)
                        .Include(i => i.Operation.OperationType)
                        .Include(i => i.User)
                       .ToList();
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
                .OrderByDescending(i => i.ActionDate)
                .ToList();
        }

        public List<Transaction> GetTransactionToOperation(int ProjectId)
        {
            return DbSet
                .Where(i => i.ProjectId == ProjectId)
                .Include(i => i.Operation)
                .ToList();
        }

        public Transaction GetTransactionsToIndexById(int Id)
        {
            return DbSet.Where(i => i.Id == Id)
                .Include(i => i.Operation)
                .Include(i => i.Project)
                .Include(i => i.Score)
                .Include(i => i.CounterParty)
                .Include(i => i.Operation.OperationType)
                .Include(i => i.User)
                .FirstOrDefault();
        }

        public (int, int) GetScoreIdAndOperationId(int Id)
        {
            var OperationId = DbSet.Where(i => i.Id == Id)
                .Select(i => i.OperationId).FirstOrDefault();
            var ScoreId = DbSet.Where(i => i.Id == Id)
                .Select(i => i.ScoreId).FirstOrDefault();

            return (OperationId, ScoreId);

        }

    }
}
