using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.RemittanceModels;
using FinanceManagmentApplication.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services.Contracts
{
    public interface IRemittanceService
    {
        Task<Response> Create(RemittanceCreateModel model, ClaimsPrincipal User);

        Task<RemittanceCreateModel> GetCreateModel();

        Task<List<RemittanceIndexModel>> GetAll();

        Task<PagedResponse<List<RemittanceIndexModel>>> IndexPagination(PaginationFilter filter);

        Task<Response> Edit(RemittanceEditModel model, ClaimsPrincipal User);
    }
}
