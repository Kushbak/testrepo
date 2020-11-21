using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Helpers;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.TransactionModels;
using FinanceManagmentApplication.Services.Contracts;
using FinanceManagmentApplication.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagmentApplication.Controllers
{
    [Route("Transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        
        private ITransactionService TransactionService { get; }

        public TransactionController(ITransactionService transactionService)
        {
            TransactionService = transactionService;
        }
        

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<TransactionIndexModel>> Create(TransactionCreateModel model)
        {
            var Result = await TransactionService.Create(model, User);
            return Ok(Result);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<TransactionCreateModel>> Create()
        {
            return await TransactionService.GetCreateModel();
        }

        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<PagedResponse<List<TransactionIndexModel>>>> Index([FromQuery] PaginationFilter filter)
        {
            return await TransactionService.IndexPagination(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionIndexModel>> Index(int Id)
        {
            return await TransactionService.GetAllById(Id);
        }
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(TransactionEditModel model)
        {
            var result = await TransactionService.Edit(model, User);
            if (result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult<TransactionEditModel>> Edit(int Id)
        {
            return await TransactionService.GetEditModel(Id);
        }
        

        
    }
}

