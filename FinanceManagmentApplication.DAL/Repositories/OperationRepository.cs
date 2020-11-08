using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceManagmentApplication.DAL.Repositories
{
    public class OperationRepository: Repository<Operation>, IOperationRepository
    {
        public OperationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            DbSet = applicationDbContext.Operations;
        }
        public bool Check(int Id)
        {
            return DbSet.Any(i => i.Id == Id);
        }
    }
}
