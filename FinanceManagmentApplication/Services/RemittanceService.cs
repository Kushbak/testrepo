using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Helpers;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.RemittanceModels;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.Services.Contracts;
using FinanceManagmentApplication.Wrappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services
{
    public class RemittanceService: IRemittanceService
    {
        private IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        private UserManager<User> UserManager { get; set; }

        public RemittanceService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<User> userManager)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            UserManager = userManager;

        }

        public async Task<List<RemittanceIndexModel>> GetAll()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Remittance = uow.Remittances.GetFullRemittances();
                var Models = Mapper.Map<List<RemittanceIndexModel>>(Remittance);
                return Models;
            }
        }

        public async Task<Response> Create(RemittanceCreateModel model, ClaimsPrincipal User)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var _User = await UserManager.FindByNameAsync(User.Identity.Name);

                if (model.Score2Id == model.ScoreId)
                {
                    return new Response { Status = StatusEnum.Error, Message = "Перевод осуществляется на один и тот же счет!" };
                }
                if (!uow.Scores.Check(model.ScoreId) && !uow.Scores.Check(model.Score2Id))
                {
                    return new Response { Status = StatusEnum.Error, Message = "В переводе указан несуществующий счет" };
                }

                if (!uow.Operations.Check(model.OperationId))
                {
                    return new Response { Status = StatusEnum.Error, Message = "В переводе указана несуществующая операция!" };
                }

                if (!uow.Projects.Check(model.ProjectId))
                {
                    return new Response { Status = StatusEnum.Error, Message = "В переводе указан несуществующий проект!" };
                }
                var Score = await uow.Scores.GetByIdAsync(model.ScoreId);
                var Score2 = await uow.Scores.GetByIdAsync(model.Score2Id);
                var Operation = await uow.Operations.GetByIdAsync(model.OperationId);

                if (!validateSum(model.Sum, Score.Balance, Operation.OperationTypeId) && !validateSum(model.Sum, Score2.Balance, Operation.OperationTypeId))
                {
                    return new Response { Status = StatusEnum.Error, Message = "На счету недостаточно денег!" };
                }

                model.UserId = _User.Id;
                var Remittance = Mapper.Map<Remittance>(model);
                await uow.Remittances.CreateAsync(Remittance);
                return new Response { Status = StatusEnum.Accept, Message = "Перевод успешно создан." };

            }

            

        }

        public async Task<RemittanceCreateModel> GetCreateModel()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Model = new RemittanceCreateModel();
                Model.Operations = Mapper.Map<List<OperationIndexModel>>(await uow.Operations.GetAllAsync());
                Model.Projects = Mapper.Map<List<ProjectIndexModel>>(await uow.Projects.GetAllAsync());
                Model.Scores = Mapper.Map<List<ScoreIndexModel>>(await uow.Scores.GetAllAsync());
                return Model;
            }
        }

        public async Task<PagedResponse<List<RemittanceIndexModel>>> IndexPagination(PaginationFilter filter)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = Mapper.Map<List<RemittanceIndexModel>>(uow.Remittances.GetPaginationTransactions(filter.PageNumber, filter.PageSize));
                var totalRecords = await uow.Remittances.Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords);
                return pagedReponse;
            }
        }

        private bool validateSum(int TransactionSum, int ScoreSum, int OperationType)
        {
            int Income = 1;
            int Expense = 2;

            if (OperationType == Expense)
            {
                return TransactionSum >= ScoreSum;
            }

            return true;

        }
    }
}
