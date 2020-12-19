using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Models.FinanceModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.Models.FilterModels;
using FinanceManagmentApplication.Models.CounterPartiesModel;
using FinanceManagmentApplication.Models.ScoreModel;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Models.OperationModels;

namespace FinanceManagmentApplication.BL.Services
{
    public class FinanceService: IFinanceService
    {
        private IUnitOfWorkFactory UnitOfWorkFactory { get; }

        public FinanceService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<List<ProjectFinanceModel>> GetFinanceInformationToProjects()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Models = new List<ProjectFinanceModel>();
                var Projects = await uow.Projects.GetAllAsync();

                foreach (var Project in Projects)
                {
                    Models.Add(GetFinanceInformationToProject(Project));
                }

                return Models;
            }
        }

        public async Task<List<OperationFinanceModel>> GetFinanceInformationToOperations()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Models = new List<OperationFinanceModel>();
                var Operations = await uow.Operations.GetAllAsync();
                var Transactions = await uow.FinanceActions.GetAllAsync();
                foreach (var operation in Operations)
                {
                    Models.Add(GetFinanceInformationToOperation(operation, Transactions));
                }

                return Models;
            }
        }

        public OperationFinanceModel GetFinanceInformationToOperation(Operation operation, List<FinanceAction> transactions)
        {
            var Model = Mapper.Map<OperationFinanceModel>(operation);
            var OperationTransactions = transactions.Where(i => i.OperationId == operation.Id).ToList();
            Model.OperationSum = OperationTransactions.Sum(i => i.Sum);
            Model.OperationCount = OperationTransactions.Count();
            return Model;
        }

        public ProjectFinanceModel GetFinanceInformationToProject(Project Project)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Transactions = uow.FinanceActions.GetFinanceActionsToOperation(Project.Id);
                var Model = Mapper.Map<ProjectFinanceModel>(Project);

                foreach (var Transaction in Transactions)
                {
                    if (Transaction.Operation.OperationTypeId == 1)
                        Model.Income += Transaction.Sum;
                    else if (Transaction.Operation.OperationTypeId == 2)
                        Model.Expense += Transaction.Sum;
                }

                Model.Profit = Model.Income - Model.Expense;

                return Model;
            }
        }

        public async Task<List<FinanceActiveIndexModel>> GetStatisticsData(StatisticFilter filter)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {

                List<FinanceActiveIndexModel> FinanceActionsList = new List<FinanceActiveIndexModel>(); 
                var FinanceActions = uow.FinanceActions.GetFinanceActionsForStatistics(
                    StartDate: filter.StartDate,
                    EndDate: filter.EndDate,
                    OperationsId: filter.OperationsId,
                    ProjectsId: filter.ProjectsId,
                    ScoresId: filter.ScoresId,
                    Scores2Id: filter.Scores2Id,
                    CounterPartiesId: filter.CounterPartiesId,
                    OperationTypesId: filter.OperationTypesId
                    );

                foreach (var FinanceAction in FinanceActions)
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

                return Mapper.Map<List<FinanceActiveIndexModel>>(FinanceActions);

            }
        }

        public async Task<FinanceSettingsModel> GetSettingsModel()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var Model = new FinanceSettingsModel();
                Model.CounterParties = Mapper.Map<List<CounterPartyIndexModel>>(await uow.CounterParties.GetAllAsync());
                Model.Projects = Mapper.Map<List<ProjectIndexModel>>(await uow.Projects.GetAllAsync());
                Model.Operations = Mapper.Map<List<OperationIndexModel>>(await uow.Operations.GetAllAsync());
                Model.Scores = Mapper.Map<List<ScoreIndexModel>>(await uow.Scores.GetAllAsync());

                return Model;
            }
        }
    }
}
