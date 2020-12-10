using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.WebModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{ 
    public interface IFinanceActionService
    {
        Task<PagedResponse<List<FinanceActiveIndexModel>>> FinanceActionPagination(PaginationFilter filter);

    }
}
