using FinanceManagmentApplication.Models.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services.Contracts
{
    public interface IFinanceService
    {
        Task<List<ProjectFinanceModel>> GetFinanceInformationToProjects();
    }
}
