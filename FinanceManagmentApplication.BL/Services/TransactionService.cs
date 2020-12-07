using AutoMapper;
using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.WebModels;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.Models.TransactionModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using FinanceManagmentApplication.BL.Exceptions;
using FinanceManagmentApplication.BL.Handlers.TransactionEditHandlers;
using FinanceManagmentApplication.Models.HelperModel;
using FinanceManagmentApplication.Tools.Builders;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Tools.Helpers;
using FinanceManagmentApplication.WebModels.Wrappers;

namespace FinanceManagmentApplication.BL.Services
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
                    model.CounterPartyId = await uow.CounterParties.GetNullCounterParty();
                if (model.ProjectId == 0)
                    model.ProjectId = await uow.Projects.GetNullProjectId();
                if (!uow.Scores.Check(model.ScoreId))
                    return new Response { Status = StatusEnum.Error, Message = "В транзакции указан несуществующий счет" };
                if (!uow.Operations.Check(model.OperationId))
                    return new Response { Status = StatusEnum.Error, Message = "В транзакции указана несуществующая операция!" };
                if (!uow.Projects.Check(model.ProjectId))
                    return new Response { Status = StatusEnum.Error, Message = "В транзакции указан несуществующий проект!" };
                if (!uow.CounterParties.Check(model.CounterPartyId))
                    return new Response { Status = StatusEnum.Error, Message = "В транзакции указан несуществующий контрагент!" };
  
                var Score = await uow.Scores.GetByIdAsync(model.ScoreId);
                var Operation = await uow.Operations.GetByIdAsync(model.OperationId);

                if (!await validateSum(model.Sum, Score, Operation.OperationTypeId))
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

                Score OldScore = null;
                Operation OldOperation = null;

                var Operation = await uow.Operations.GetByIdAsync(model.OperationId);
                var Score = await uow.Scores.GetByIdAsync(model.ScoreId);
                var OldIds = uow.Transactions.GetScoreIdAndOperationId(Transaction.Id);
                var OldSum = uow.FinanceActions.GetSumFinanceAction(Transaction.Id);

                if (Operation.Id != OldIds.Item1)
                    OldOperation = await uow.Operations.GetByIdAsync(OldIds.Item1);
                if(Score.Id != OldIds.Item2)
                    OldScore = await uow.Scores.GetByIdAsync(OldIds.Item2);

                var HelperModel = new TransactionEditHelperModelBuilder()
                    .SetNewOperationTypeId(Operation.OperationTypeId)
                    .SetOldOperationTypeId(OldOperation != null ? OldOperation.OperationTypeId : Operation.OperationTypeId)
                    .SetNewScore1(Score)
                    .SetOldScore1(OldScore)
                    .SetNewTransactionSum(Transaction.Sum)
                    .SetOldTransactionSum(OldSum)
                    .Build();

                var CheckScoreEdit = await RedactAndCheckTransaction(HelperModel);
                if (!CheckScoreEdit.Item1)
                {
                    return new Response { Status = StatusEnum.Error, Message = "На счету недостаточно денег для редактируемой суммы" };
                }

                await uow.Scores.UpdateAsync(Score);
                if (OldScore != null)
                {
                    await uow.Scores.UpdateAsync(OldScore);
                }
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
                var Transaction = uow.Transactions.GetFullTransaction(Id);
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

        public async Task<List<TransactionExcelModel>> GetAll()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Transactions = await uow.Transactions.GetAllAsync();
                var Models = new List<TransactionExcelModel>();
                foreach (var Transaction in Transactions)
                {

                    var Model = Mapper.Map<TransactionExcelModel>(Transaction);
                    Models.Add(Model);
                }

                return Models;
            }
        }

        private async Task<(bool, string)> RedactAndCheckTransaction(TransactionEditHelperModel model)
        {
            try
            {
                var Handler = new TransactionOperationEditHanlder();
                Handler.HandleRequest(model);
                return (true, null);
            }
            catch (TransactionException e)
            {
                return (false, e.Message);
            }
        }

        private async Task<bool> validateSum(int TransactionSum, Score Score, int OperationType)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                int Income = 1;
                int Expense = 2;

                if (OperationType == Expense)
                {
                    if (TransactionSum > Score.Balance)
                        return false;
                    Score.Balance -= TransactionSum;
                }
                else if(OperationType == Income)
                {
                    Score.Balance += TransactionSum;
                }
                await uow.Scores.UpdateAsync(Score);
                return true;
            }

        }

        public async Task<TransactionExcelModel> GetAllById(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {

                var Transaction = await uow.Transactions.GetByIdAsync(Id);
                var Model = Mapper.Map<TransactionExcelModel>(Transaction);
                return Model;

            }
        }

        public async Task<PagedResponse<List<TransactionExcelModel>>> IndexPagination(PaginationFilter filter)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pagedData = Mapper.Map<List<TransactionExcelModel>>(uow.Transactions.GetPaginationTransactions(filter.PageNumber, filter.PageSize, filter.StartDate, OperationId: null, null, null, null, null));
                var totalRecords = await uow.Transactions.Count();
                var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords);
                return pagedReponse;
            }
        }

      




    }


}

  
