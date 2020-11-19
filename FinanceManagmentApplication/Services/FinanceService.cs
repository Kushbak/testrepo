using AutoMapper;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Models.ProjectModels;
using FinanceManagmentApplication.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services
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
