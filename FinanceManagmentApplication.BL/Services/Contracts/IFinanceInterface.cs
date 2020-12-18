using FinanceManagmentApplication.Models.FilterModels;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.Models.FinanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface IFinanceService
    {
        Task<List<ProjectFinanceModel>> GetFinanceInformationToProjects();

        Task<List<OperationFinanceModel>> GetFinanceInformationToOperations();

        Task<List<FinanceActiveIndexModel>> GetStatisticsData(StatisticFilter filter);

        Task<FinanceSettingsModel> GetSettingsModel();
    }
}
