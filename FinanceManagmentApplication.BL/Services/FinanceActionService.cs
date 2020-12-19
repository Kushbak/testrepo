using AutoMapper;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceManagmentApplication.Tools.Helpers;
using FinanceManagmentApplication.WebModels.Wrappers;

namespace FinanceManagmentApplication.BL.Services
{
    public class FinanceActionService : IFinanceActionService
    {

        private IUnitOfWorkFactory UnitOfWorkFactory { get; }
        private readonly UserManager<User> UserManager;
        public FinanceActionService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<User> userManager)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            UserManager = userManager;
        }

        public async Task<PagedResponse<List<FinanceActiveIndexModel>>> FinanceActionPagination(PaginationFilter filter)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var FinanceActionsList = new List<FinanceActiveIndexModel>();
                var FinanceActions = uow.FinanceActions.GetPaginationFinanceActions
                    (PageNumber: filter.PageNumber, PageSize: filter.PageSize, OperationsId: filter.OperationsId,
                    ProjectsId: filter.ProjectsId, ScoresId: filter.ScoresId, StartDate: filter.StartDate, EndDate: filter.EndDate,
                    Scores2Id: filter.Scores2Id, CounterPartiesId: filter.CounterPartiesId, UsersId: filter.UsersId, OperationTypesId: filter.OperationTypesId);
                foreach (var FinanceAction in FinanceActions.Item1)
                {

                    if (FinanceAction.Discriminator.ToLower() == "transaction")
                    {
                        var Transaction = FinanceAction as Transaction;
                        Transaction.CounterParty = await uow.CounterParties.GetByIdAsync(Transaction.CounterPartyId);
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
 

        
