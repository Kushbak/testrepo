using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Helpers;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.Services.Contracts;
using FinanceManagmentApplication.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services
{
    public class FinanceActionService: IFinanceActionService
    {
        private IUnitOfWorkFactory UnitOfWorkFactory { get; }

        public FinanceActionService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<PagedResponse<List<FinanceActiveIndexModel>>> FinanceActionPagination(PaginationFilter filter)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var FinanceActionsList = new List<FinanceActiveIndexModel>();
                var FinanceActions = uow.FinanceActions.GetPaginationFinanceActions
                    (PageNumber: filter.PageNumber, PageSize: filter.PageSize, OperationId: filter.OperationId, 
                    ProjectId: filter.ProjectId, ScoreId: filter.ScoreId, Date: filter.ActionDate);
                foreach (var FinanceAction in FinanceActions.Item1)
                {   
                    
                    if (FinanceAction.Discriminator.ToLower() == "transaction")
                    {
                        var Transaction = FinanceAction as Transaction;
                        Transaction.CounterParty = await  uow.CounterParties.GetByIdAsync(Transaction.CounterPartyId);
                        FinanceActionsList.Add(Mapper.Map<FinanceActiveIndexModel>(Transaction));
                        
                    }
                    else if (FinanceAction.Discriminator.ToLower() == "remittance")
                    {
                        var Remittance = FinanceAction as Remittance;
                        Remittance.Score2 = await uow.Scores.GetByIdAsync(Remittance.Score2Id);
                        FinanceActionsList.Add(Mapper.Map<FinanceActiveIndexModel>(Remittance));
                    }
                }
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = FinanceActionsList;
                var totalRecords = FinanceActions.Item2;
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords);

                return pagedReponse;
            }
        }

        
    }
}
