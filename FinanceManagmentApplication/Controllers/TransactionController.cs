using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FinanceManagmentApplication.DAL.Context;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Tools.Helpers;
using FinanceManagmentApplication.Models.ErrorModels;
using FinanceManagmentApplication.Models.TransactionModels;
using FinanceManagmentApplication.Models.WebModels;
using FinanceManagmentApplication.WebModels.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceManagmentApplication.BL.Services.Contracts;

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

        /// <summary>
        /// Create a transaction(with token)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / Create a transaction
        ///     {
       ///         "actionDate": "2020-11-25",
       ///         "sum": 1250,
       ///         "operationId": 1,
      ///          "projectId": 1,
      ///          "counterPartyId": 1,
      ///          "scoreId": 1,
      ///          "description": "lalala description"
      ///             }
    /// </remarks>
    /// <param name="model"></param>
    /// <returns>success result "Transaction created successfully!"</returns>
    /// <response code="200">Request is success</response>
    /// <response code="400">Model is null</response>
    ///<response code="401">Unauthorized</response>
    /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize]
        [Route("Create")]
        public async Task<ActionResult<TransactionExcelModel>> Create(TransactionCreateModel model)
        {
            var Result = await TransactionService.Create(model, User);
            return Ok(Result);
        }  



        /// <summary>
        /// Get create model for transaction(only for a back developers)
        /// </summary>
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<TransactionCreateModel>> Create()
        {
            return await TransactionService.GetCreateModel();
        }


        //[HttpGet]
        //[Route("Index")]
        //public async Task<ActionResult<PagedResponse<List<TransactionIndexModel>>>> Index(PaginationFilter filter)
        //{
        //    return await TransactionService.IndexPagination(filter);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<TransactionIndexModel>> Index(int Id)
        //{
        //    return await TransactionService.GetAllById(Id);
        //}




        /// <summary>
        /// Edit a transaction
        /// При редактировании счета, операции, суммы транзакции
        /// происходит синхронизация транзации в соответствии 
        /// с актуальными данными.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT / Edit a transaction
        ///     {
        ///     "id":12
        ///    "actionDate": "2020-11-25",
        ///         "sum": 1250,
        ///        "operationId": 1,
        ///         "projectId": 1,
        ///         "counterPartyId": 1,
        ///          "scoreId": 1,
        ///          "description": "lalala description"
        ///}
        ///     
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>success result "Transaction edited successfully!"</returns>
        /// <response code="200">Request is success</response>
        /// <response code="400">Model is null</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(TransactionEditModel model)
        {
            var result = await TransactionService.Edit(model, User);
            if (result.Status == StatusEnum.Error)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            return Ok(result);
        }


        /// <summary>
        /// Get edit model for transaction(only for a back developers)
        /// </summary>
        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult<TransactionEditModel>> Edit(int Id)
        {
            return await TransactionService.GetEditModel(Id);
        }
        

        
    }
}

