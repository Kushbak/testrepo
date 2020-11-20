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

                var Score = await uow.Scores.GetByIdAsync(model.ScoreId);
                var Score2 = await uow.Scores.GetByIdAsync(model.Score2Id);

                if (!validateSum(model.Sum, Score.Balance, 3) && !validateSum(model.Sum, Score2.Balance, 3))
                {
                    return new Response { Status = StatusEnum.Error, Message = "На счету недостаточно денег!" };
                }

                model.UserId = _User.Id;
                var Remittance = Mapper.Map<Remittance>(model);
                Remittance.ProjectId = await uow.Projects.GetNullProjectId();
                Remittance.OperationId = await uow.Operations.GetTransferOperationId();
                await uow.Remittances.CreateAsync(Remittance);


                return new Response { Status = StatusEnum.Accept, Message = "Перевод успешно создан." };

            }

            

        }

        public async Task<RemittanceCreateModel> GetCreateModel()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Model = new RemittanceCreateModel();
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
            int Transfer = 3;

            if (OperationType == Transfer)
            {
                return TransactionSum <= ScoreSum;
            }

            return true;

        }
    }
}
