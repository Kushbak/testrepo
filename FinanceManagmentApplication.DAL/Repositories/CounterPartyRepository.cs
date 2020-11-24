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
    public class CounterPartyRepository: Repository<CounterParty>, ICounterPartyRepository
    {
        public CounterPartyRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {
            DbSet = applicationDbContext.CounterParties;
        }

        public bool Check(int Id)
        {
            return DbSet.Any(i => i.Id == Id);
        }

        public async Task<int> GetNullCounterParty()
        {
            var CounterParty = DbSet.Where(i => i.Name.ToLower() == "без контрагента").FirstOrDefault();
            if (CounterParty == null)
            {
                return await CreateAsync(new CounterParty { IsCompany = true, Name = "без контрагента" });
            }
            return CounterParty.Id;
        }

    }
}
