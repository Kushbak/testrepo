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

        public (List<FinanceAction>, int) GetPaginationFinanceActions(int PageNumber, int PageSize, DateTime? StartDate, DateTime? EndDate, int[] OperationsId, int[] ProjectsId, int[] ScoresId, int[] CounterPartiesId, int[] Scores2Id , int[] UsersId, int[] OperationTypesId)
        {
            
            var Count = DbSet.Where(i => (StartDate == null || StartDate < i.ActionDate) && (EndDate == null  || EndDate > i.ActionDate))
                .Where(i => OperationTypesId != null ? OperationTypesId.Any(o => i.Operation.OperationTypeId == o) : OperationsId == null || OperationsId.Any(a => a == i.OperationId))
                .Where(i => ProjectsId == null || ProjectsId.Any(a => a == i.ProjectId))
                .Where(i => ScoresId == null || ScoresId.Any(a => a == i.ScoreId))
                .Where(i => UsersId == null || UsersId.Any(a => a == i.UserId))
                .Where(i => CounterPartiesId == null || (i is Transaction && CounterPartiesId.Any(a => a == ((Transaction)i).CounterPartyId)))
                .Where(i => Scores2Id == null || (i is Remittance && Scores2Id.Any( a => a == ((Remittance)i).Score2Id)))
                .Count();


            var FinanceActions = DbSet.Where(i => (StartDate == null || StartDate < i.ActionDate) && (EndDate == null || EndDate > i.ActionDate))
                .Where(i => OperationTypesId != null ? OperationTypesId.Any(o => i.Operation.OperationTypeId == o) : OperationsId == null || OperationsId.Any(a => a == i.OperationId))
                .Where(i => ProjectsId == null || ProjectsId.Any(a => a == i.ProjectId))
                .Where(i => ScoresId == null || ScoresId.Any(a => a == i.ScoreId))
                .Where(i => UsersId == null || UsersId.Any(a => a == i.UserId))
                .Where(i => CounterPartiesId == null || (i is Transaction && CounterPartiesId.Any(a => a == ((Transaction)i).CounterPartyId)))
                .Where(i => Scores2Id == null || (i is Remittance && Scores2Id.Any(a => a == ((Remittance)i).Score2Id)))
                .OrderByDescending(i => i.ActionDate)
                .Skip((PageNumber - 1) * PageSize)  
                       .Take(PageSize)
                       .Include(i => i.Operation)
                        .Include(i => i.Project)
                        .Include(i => i.Score)
                        .Include(i => i.Operation.OperationType)
                        .Include(i => i.User)
                        .OrderBy(i => i.ActionDate)
                       .ToList();

            return (FinanceActions, Count);

        }

        public List<FinanceAction> GetFinanceActionsForStatistics(DateTime? StartDate, DateTime? EndDate, int[] OperationsId, int[] ProjectsId, int[] ScoresId, int[] Scores2Id, int[] CounterPartiesId, int[] OperationTypesId)
        {
            return DbSet.Where(i => (StartDate == null || StartDate < i.ActionDate) && (EndDate == null || EndDate > i.ActionDate))
                .Where(i => OperationTypesId != null ? OperationTypesId.Any(o => i.Operation.OperationTypeId == o) :  OperationsId == null || OperationsId.Any(a => a == i.OperationId))
                .Where(i => ProjectsId == null || ProjectsId.Any(a => a == i.ProjectId))
                .Where(i => ScoresId == null || ScoresId.Any(a => a == i.ScoreId))
                .Where(i => CounterPartiesId == null || (i is Transaction && CounterPartiesId.Any(a => a == ((Transaction)i).CounterPartyId)))
                .Where(i => Scores2Id == null || (i is Remittance && Scores2Id.Any(a => a == ((Remittance)i).Score2Id)))
                        .Include(i => i.Operation)
                        .Include(i => i.Project)
                        .Include(i => i.Score)
                        .Include(i => i.Operation.OperationType)
                        .OrderBy(i => i.ActionDate)
                .ToList();
        }



        public int GetSumFinanceAction(int Id)
        {
            return DbSet.Where(i => i.Id == Id).Select(i => i.Sum).FirstOrDefault();
        }

       
    }

}
