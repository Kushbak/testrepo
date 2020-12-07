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
    public class RemittanceRepository: Repository<Remittance>, IRemittanceRepository
    {
        public RemittanceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            DbSet = applicationDbContext.Remittances;
        }

        public List<Remittance> GetFullRemittances()
        {
            return DbSet
                .Include(i => i.Operation)
                .Include(i => i.Project)
                .Include(i => i.Score)
                .Include(i => i.Score2)
                .OrderByDescending(i => i.ActionDate)
                .ToList();
        }

        public Remittance GetFullRemittanceToScore(int Id)
        {
            return DbSet
                .Where(i => i.Id == Id)
                .Include(i => i.Score)
                .Include(i => i.Score2)
                .FirstOrDefault();
        }

        public List<Remittance> GetPaginationTransactions(int PageNumber, int PageSize)
        {
            return DbSet.Skip((PageNumber - 1) * PageSize)
                       .Take(PageSize)
                       .Include(i => i.Operation)
                        .Include(i => i.Project)
                        .Include(i => i.Score)
                        .Include(i => i.Score2)
                        .Include(i => i.Operation.OperationType)
                        .Include(i => i.User)
                       .ToList();
        }

        public (int,int) GetRemmiranceScoreId(int Id)
        {
            int FirstScoreId = DbSet.Where(i => i.Id == Id).Select(i => i.ScoreId).FirstOrDefault();
            int SecondScoreId = DbSet.Where(i => i.Id == Id).Select(i => i.Score2Id).FirstOrDefault();
            return (FirstScoreId, SecondScoreId);
        }
    }
}
