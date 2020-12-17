using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.RemittanceModels;
using FinanceManagmentApplication.WebModels.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("Remittance")]
    [ApiController]
    public class RemittanceController : ControllerBase
    {   
        private IRemittanceService RemittanceService { get; set; }

        public RemittanceController(IRemittanceService remittanceService)
        {
            RemittanceService = remittanceService;
        } 

        /// <summary>
        /// Get create model for a remittance(only for a backend testing!!!)
        /// </summary>
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<RemittanceCreateModel>> Create()
        {
            return await RemittanceService.GetCreateModel();
        }


        /// <summary>
        /// Create a remittance(with token)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     { 
        /// "actionDate": "2020-11-25",
        /// "sum": 1200,
        /// "scoreId": 1,
        /// "score2Id": 2,
        /// "description": "testing remittance"
        ///       }
        ///       
        ///</remarks>

        [HttpPost]
        [Authorize]
        [Route("Create")]
        public async Task<IActionResult> Create(RemittanceCreateModel model)
        {
            var Result = await RemittanceService.Create(model, User);
            return Ok(Result);
        }


        /// <summary>
        /// Редактирование внутреннего перевода.
        /// При редактровании суммы, или счетов перевода,
        /// происходит синхроназация счетов по нынешним данным.
        /// </summary>
        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(RemittanceEditModel model)
        {
            var Result = await RemittanceService.Edit(model, User);
            return Ok(Result);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id.HasValue)
            {
                return Ok(await RemittanceService.GetEditModel(Id.Value));
            }
            return StatusCode(StatusCodes.Status200OK, "Error!");
        }

        //public async Task<IActionResult> Get()
        //{
        //    return File();
        //}

        //[HttpGet]
        //[Route("Index")]
        //public async Task<ActionResult<PagedResponse<List<RemittanceIndexModel>>>> Index(PaginationFilter paginationFilter)
        //{
        //    var Result = await RemittanceService.IndexPagination(paginationFilter);
        //    return Result;
        //}

    }
}
