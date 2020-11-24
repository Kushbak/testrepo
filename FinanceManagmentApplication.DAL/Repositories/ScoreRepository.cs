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
    public class ScoreRepository: Repository<Score>, IScoreRepository
    {
        public ScoreRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            DbSet = applicationDbContext.Scores;
        }

        //public bool CheckScoreToCounterParty(int Id)
        //{
        //    return DbSet.Any(i => i.CounterPartyId == Id);
        //}

        public bool Check(int Id)
        {
            return DbSet.Any(i => i.Id == Id);
        }

        public List<Score> GetFullScore()
        {
            return DbSet
                .Include(i => i.PaymentType).ToList();
        }

        
    }
}
