using AutoMapper;
using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Helpers;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.Models.TransactionModels;
using FinanceManagmentApplication.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FinanceManagmentApplication.Wrappers;

namespace FinanceManagmentApplication.Services
{
    public class TransactionService : ITransactionService
    {
        private IUnitOfWorkFactory UnitOfWorkFactory { get; }
        private UserManager<User> UserManager { get; }
        public TransactionService(IUnitOfWorkFactory unitOfWorkFactory, UserManager<User> userManager)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
            UserManager = userManager;
        }

        public async Task<Response> Create(TransactionCreateModel model, ClaimsPrincipal User)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var _User = await UserManager.FindByNameAsync(User.Identity.Name);

                if (model.CounterPartyId == 0)
                {
                    model.CounterPartyId = await uow.CounterParties.GetNullCounterParty();
                }
                if (model.ProjectId == 0)
                {
                    model.ProjectId = await uow.Projects.GetNullProjectId();
                }
                if (!uow.Scores.Check(model.ScoreId))
                {
                    return new Response { Status = StatusEnum.Error, Message = "В транзакции указан несуществующий счет" };
                }

                if (!uow.Operations.Check(model.OperationId))
                {
                    return new Response { Status = StatusEnum.Error, Message = "В транзакции указана несуществующая операция!" };
                }

                if (!uow.Projects.Check(model.ProjectId))
                {
                    return new Response { Status = StatusEnum.Error, Message = "В транзакции указан несуществующий проект!" };
                }
                if (!uow.CounterParties.Check(model.CounterPartyId))
                {
                    return new Response { Status = StatusEnum.Error, Message = "В транзакции указан несуществующий контрагент!" };
                }
                var Score = await uow.Scores.GetByIdAsync(model.ScoreId);
                var Operation = await uow.Operations.GetByIdAsync(model.OperationId);

                if (!validateSum(model.Sum, Score.Balance, Operation.OperationTypeId))
                {
                    return new Response { Status = StatusEnum.Error, Message = "На счету недостаточно денег!" };
                }
                
                var Transaction = Mapper.Map<Transaction>(model);
                Transaction.UserId = _User.Id;
                await uow.Transactions.CreateAsync(Transaction);
                return new Response { Status = StatusEnum.Accept, Message = "Транзакция успешно создана." };

            }
        }

        public async Task<TransactionCreateModel> GetCreateModel()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Model = new TransactionCreateModel();
                return Model;
            }
        }

        public async Task<Response> Edit(TransactionEditModel model, ClaimsPrincipal User)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {

                var Transaction = Mapper.Map<Transaction>(model);
                Transaction.Discriminator = "Transaction";
                if (model == null)
                    return new Response { Status = StatusEnum.Error, Message = "ничего на сервер не отправлено" };
                if (!uow.Operations.Check(model.OperationId))
                    return new Response { Status = StatusEnum.Error, Message = "Нет такого типа операций" };
                if (!uow.Projects.Check(model.ProjectId))
                    return new Response { Status = StatusEnum.Error, Message = "Нет такого проекта!" };
                if (!uow.Scores.Check(model.ScoreId))
                    return new Response { Status = StatusEnum.Error, Message = "Нет такого счета!" };
                var _User = await UserManager.FindByNameAsync(User.Identity.Name);
                Transaction.UserId = _User.Id;
                Transaction.ActionDate = Transaction.ActionDate;
                await uow.Transactions.UpdateAsync(Transaction);
                return new Response { Status = StatusEnum.Accept, Message = "Редактирование транзакции прошло успешно." };

            }
        }

        public async Task<TransactionEditModel> GetEditModel(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Transaction = await uow.Transactions.GetByIdAsync(Id);
                var Model = Mapper.Map<TransactionEditModel>(Transaction);
                return Model;
            }
        }

        public async Task<TransactionDetailsModel> GetDetailsModel(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Transaction = await uow.Transactions.GetByIdAsync(Id);
                var Model = Mapper.Map<TransactionDetailsModel>(Transaction);
                return Model;
            }
        }

        public async Task<List<TransactionIndexModel>> GetAll()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Transactions = await uow.Transactions.GetAllAsync();
                var Models = new List<TransactionIndexModel>();
                foreach (var Transaction in Transactions)
                {

                    var Model = Mapper.Map<TransactionIndexModel>(Transaction);
                    Models.Add(Model);
                }

                return Models;
            }
        }

        private bool validateSum(int TransactionSum, int ScoreSum, int OperationType)
        {
            int Income = 1;
            int Expense = 2;

            if (OperationType == Expense)
            {
                return TransactionSum <= ScoreSum;
            }

            return true;

        }

        public async Task<TransactionIndexModel> GetAllById(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {

                var Transaction = await uow.Transactions.GetByIdAsync(Id);
                var Model = Mapper.Map<TransactionIndexModel>(Transaction);
                return Model;

            }
        }

        public async Task<PagedResponse<List<TransactionIndexModel>>> IndexPagination(PaginationFilter filter)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = Mapper.Map<List<TransactionIndexModel>>(uow.Transactions.GetPaginationTransactions(filter.PageNumber, filter.PageSize, filter.StartDate, OperationId: null, null, null, null));
                var totalRecords = await uow.Transactions.Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords);
                return pagedReponse;
            }
        }




    }


}

  
