using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.TransactionModels;
using FinanceManagmentApplication.Models.WebModels;
using FinanceManagmentApplication.WebModels.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services.Contracts
{
    public interface ITransactionService
    {
        Task<Response> Create(TransactionCreateModel model, ClaimsPrincipal User);

        Task<List<TransactionExcelModel>> GetAll();
     //   Task<List<TransactionIndexModel>> PaginationGetAll();
        Task<TransactionExcelModel> GetAllById (int Id);

        Task<TransactionCreateModel> GetCreateModel();

        Task<Response> Edit(TransactionEditModel model, ClaimsPrincipal User);
       
        Task<TransactionEditModel> GetEditModel(int Id);

        Task<TransactionDetailsModel> GetDetailsModel(int Id);

        Task<PagedResponse<List<TransactionExcelModel>>>  IndexPagination(PaginationFilter filter);

    }
}
