using AutoMapper;
using FinanceManagmentApplication.Tools.Builders;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.BL.Exceptions;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.HelperModel;
using FinanceManagmentApplication.BL.Handlers.RemittanceEditHandlers;
using FinanceManagmentApplication.BL.Handlers.RemittanceEditHandlers.Contracts;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.OperationModels;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.RemittanceModels;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.WebModels.Wrappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.WebModels;
using FinanceManagmentApplication.Tools.Helpers;
using FinanceManagmentApplication.BL.HandlersRemittanceEditHandlers;

namespace FinanceManagmentApplication.BL.Services
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
                var Score = await uow.Scores.GetByIdAsync(model.ScoreId);
                var Score2 = await uow.Scores.GetByIdAsync(model.Score2Id);


                if (model.Score2Id == model.ScoreId)
                {
                    return new Response { Status = StatusEnum.Error, Message = "Перевод осуществляется на один и тот же счет!" };
                }
                if (!uow.Scores.Check(model.ScoreId))
                    return new Response { Status = StatusEnum.Error, Message = $"Нет такого счета! ({Score.Name})" };
                if (!uow.Scores.Check(model.Score2Id))
                    return new Response { Status = StatusEnum.Error, Message = $"Нет такого счета! ({Score2.Name})" };

                if (!await validateSum(model.Sum, Score, Score2))
                {
                    return new Response { Status = StatusEnum.Error, Message = $"На счету {Score.Name} недостаточно денег!" };
                }

                var Remittance = Mapper.Map<Remittance>(model);
                Remittance.ProjectId = await uow.Projects.GetNullProjectId();
                Remittance.OperationId = await uow.Operations.GetTransferOperationId();
                Remittance.UserId = _User.Id;
                await uow.Remittances.CreateAsync(Remittance);


                return new Response { Status = StatusEnum.Accept, Message = "Перевод успешно создан." };

            }

            

        }

        public async Task<RemittanceCreateModel> GetCreateModel()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Model = new RemittanceCreateModel();
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

        public async Task<Response> Edit(RemittanceEditModel model, ClaimsPrincipal User)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                if (model == null)
                    return new Response { Status = StatusEnum.Error, Message = "ничего на сервер не отправлено" };
                if (model.Score2Id == model.ScoreId)
                    return new Response { Status = StatusEnum.Error, Message = "Перевод осуществляется на один и тот же счет!" };
                if (!uow.Scores.Check(model.ScoreId))
                    return new Response { Status = StatusEnum.Error, Message = $"Нет такого счета!" };
                if(!uow.Scores.Check(model.Score2Id))
                    return new Response { Status = StatusEnum.Error, Message = $"Нет такого счета!" };

                Score OldScore1 = null;
                Score OldScore2 = null;
                Score NewScore1 = await uow.Scores.GetByIdAsync(model.ScoreId);
                Score NewScore2 = await uow.Scores.GetByIdAsync(model.Score2Id);

                var Remittance = Mapper.Map<Remittance>(model);
                Remittance.Discriminator = "Remittance";

                var OldScoresId = uow.Remittances.GetRemmiranceScoreId(Remittance.Id);
                

                if (OldScoresId.Item1 != Remittance.ScoreId)
                    OldScore1 = await uow.Scores.GetByIdAsync(OldScoresId.Item1);

                if (OldScoresId.Item2 != Remittance.Score2Id)
                    OldScore2 = await uow.Scores.GetByIdAsync(OldScoresId.Item2);

                var OldSum = uow.FinanceActions.GetSumFinanceAction(Remittance.Id);

                var HelperModel = new RemittanceEditHelperModelBuilder()
                    .SetOldScore1(OldScore1)
                    .SetOldScore2(OldScore2)
                    .SetNewScore1(NewScore1)
                    .SetNewScore2(NewScore2)
                    .SetOldTransactionSum(OldSum)
                    .SetNewTransactionSum(Remittance.Sum)
                    .Build();

                var Result = await CheckEditScore(HelperModel);

                if (!Result.Item1)
                    return new Response { Status = StatusEnum.Error, Message = Result.Item2 };

                var _User = await UserManager.FindByNameAsync(User.Identity.Name);
                Remittance.UserId = _User.Id;
                Remittance.ProjectId = await uow.Projects.GetNullProjectId();
                Remittance.OperationId = await uow.Operations.GetTransferOperationId();

                await uow.Remittances.UpdateAsync(Remittance);

                return new Response { Status = StatusEnum.Accept, Message = "Редактирование перевода прошло успешно." };
            }
        }


        private async Task<(bool, string)> CheckEditScore(RemittanceEditHelperModel model)
        {
            try
            {
                RemittanceEditBaseHandler handler = new RemittanceEditScoreHanlder();
                handler.HandleRequest(model);
                return (true, null);
            }
            catch (RemittanceException re)
            {
                return (false, re.Message);
            }

        }

        

        private async Task<bool> validateSum(int TransactionSum, Score Score1, Score Score2)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {

                if (TransactionSum <= Score1.Balance)
                {
                    Score1.Balance -= TransactionSum;
                    Score2.Balance += TransactionSum;
                    await uow.Scores.UpdateAsync(Score1);
                    await uow.Scores.UpdateAsync(Score2);
                    return true;
                }
                return false;
            }

        }

        public async Task<RemittanceEditModel> GetEditModel(int Id)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Remittance = uow.Remittances.GetFullRemittanceToScore(Id);
                return Mapper.Map<RemittanceEditModel>(Remittance);
            }
        }
    }
}
