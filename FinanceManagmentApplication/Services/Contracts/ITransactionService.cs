﻿using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.TransactionModels;
using FinanceManagmentApplication.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.Services.Contracts
{
    public interface ITransactionService
    {
        Task<Response> Create(TransactionCreateModel model, ClaimsPrincipal User);

        Task<List<TransactionIndexModel>> GetAll();
     //   Task<List<TransactionIndexModel>> PaginationGetAll();
        Task<TransactionIndexModel> GetAllById (int Id);

        Task<TransactionCreateModel> GetCreateModel();

        Task<Response> Edit(TransactionEditModel model, ClaimsPrincipal User);
       
        Task<TransactionEditModel> GetEditModel(int Id);

        Task<TransactionDetailsModel> GetDetailsModel(int Id);

        Task<PagedResponse<List<TransactionIndexModel>>>  IndexPagination(PaginationFilter filter);

    }
}
