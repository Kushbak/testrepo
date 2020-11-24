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
    public class FinanceActionRepository : Repository<FinanceAction>, IFinanceActionRepository
    {
        public FinanceActionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            DbSet = applicationDbContext.FinanceActions;
        }

        public List<FinanceAction> GetFinanceActionsToOperation(int ProjectId)
        {
            return DbSet
                .Where(i => i.ProjectId == ProjectId)
                .Include(i => i.Operation)
                .ToList();
        }

        public (List<FinanceAction>, int) GetPaginationFinanceActions(int PageNumber, int PageSize, DateTime? Date, int? OperationId, int? ProjectId, int? ScoreId)
        {
            
            var Count = DbSet.Where(i => Date == null || (Date.Value.Day == i.ActionDate.Day && Date.Value.Year == i.ActionDate.Year && Date.Value.Month == i.ActionDate.Month))
                .Where(i => OperationId == null || OperationId.Value == i.OperationId)
                .Where(i => ProjectId == null || ProjectId.Value == i.ProjectId)
                .Where(i => ScoreId == null || ScoreId.Value == i.ScoreId).Count();

            var FinanceActions = DbSet.Where(i => Date == null || (Date.Value.Day == i.ActionDate.Day && Date.Value.Year == i.ActionDate.Year && Date.Value.Month == i.ActionDate.Month))
                .Where(i => OperationId == null || OperationId.Value == i.OperationId)
                .Where(i => ProjectId == null || ProjectId.Value == i.ProjectId)
                .Where(i => ScoreId == null || ScoreId.Value == i.ScoreId)
                .Skip((PageNumber - 1) * PageSize)
                       .Take(PageSize)
                       .Include(i => i.Operation)
                        .Include(i => i.Project)
                        .Include(i => i.Score)
                        .Include(i => i.Operation.OperationType)
                        .OrderBy(i => i.ActionDate)
                       .ToList();

            return (FinanceActions, Count);
        }
    }

    class CounterHelper
    {
        public int Counter;

        public int Increment()
        {
            return Counter++;
        }
    }
}
