using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Repositories.Contracts
{
    public interface IScoreRepository: IRepository<Score>
    {
        //bool CheckScoreToCounterParty(int Id);

        bool Check(int Id);

        List<Score> GetFullScore();
    }
}
