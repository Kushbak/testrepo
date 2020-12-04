using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.RemittanceModels;
using FinanceManagmentApplication.Models.WebModels;
using FinanceManagmentApplication.WebModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface IRemittanceService
    {
        Task<Response> Create(RemittanceCreateModel model, ClaimsPrincipal User);

        Task<RemittanceCreateModel> GetCreateModel();

        Task<List<RemittanceIndexModel>> GetAll();

        Task<PagedResponse<List<RemittanceIndexModel>>> IndexPagination(PaginationFilter filter);

        Task<Response> Edit(RemittanceEditModel model, ClaimsPrincipal User);

        Task<RemittanceEditModel> GetEditModel(int Id);
    }
}
