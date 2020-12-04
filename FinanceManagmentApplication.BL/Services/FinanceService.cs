using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Models.FinanceModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    {
                        Model.Income += Transaction.Sum;
                    }
                    else if (Transaction.Operation.OperationTypeId == 2)
                    {
                        Model.Expense += Transaction.Sum;
                    }


                    
                }

                Model.Profit = Model.Income - Model.Expense;

                return Model;
            }
        }
    }
}
