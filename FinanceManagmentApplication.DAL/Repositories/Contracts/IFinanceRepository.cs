using FinanceManagmentApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManagmentApplication.DAL.Repositories.Contracts
{
    public interface IFinanceActionRepository: IRepository<FinanceAction>
    {
        List<FinanceAction> GetFinanceActionsToOperation(int ProjectId);

        (List<FinanceAction>, int) GetPaginationFinanceActions(int PageNumber, int PageSize, DateTime? StartDate, DateTime? EndDate, int[] OperationsId, int[] ProjectsId, int[] ScoresId, int[] CounterPartiesId, int[] Scores2Id);

        int GetSumFinanceAction(int Id);
        (List<FinanceAction>, int) GetPaginationFinanceActions(int PageNumber, int PageSize, DateTime? StartDate, DateTime? EndDate, int[] OperationsId, int[] ProjectsId, int[] ScoresId, int[] CounterPartiesId, int[] Scores2Id, int[] UsersId);
       
    }
}
