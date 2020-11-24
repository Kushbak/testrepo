using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services.Contracts
{
    public interface IFinanceActionService
    {
        Task<PagedResponse<List<FinanceActiveIndexModel>>> FinanceActionPagination(PaginationFilter filter);
    }
}
