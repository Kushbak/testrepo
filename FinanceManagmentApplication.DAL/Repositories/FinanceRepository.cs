using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagmentApplication.DAL.Repositories
{
    public class FinanceActionRepository: Repository<FinanceAction>, IFinanceActionRepository
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
    }
}
