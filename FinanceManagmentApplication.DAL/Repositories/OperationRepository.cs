using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<int> GetTransferOperationId()
        {
            var Operation = DbSet.Where(i => i.Name == "внутренние переводы").FirstOrDefault();
            if (Operation == null)
            {
                return await CreateAsync(new Operation { Name ="Внутренние переводы", OperationTypeId = 3});
            }
            return Operation.Id;
        }
    }
}
