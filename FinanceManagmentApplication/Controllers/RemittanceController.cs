using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.RemittanceModels;
using FinanceManagmentApplication.Services.Contracts;
using FinanceManagmentApplication.Wrappers;
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

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult<RemittanceCreateModel>> Create()
        {
            return await RemittanceService.GetCreateModel();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(RemittanceCreateModel model)
        {
            var Result = await RemittanceService.Create(model, User);
            return Ok(Result);
        }

        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult<PagedResponse<List<RemittanceIndexModel>>>> Index([FromQuery] PaginationFilter paginationFilter)
        {
            var Result = await RemittanceService.IndexPagination(paginationFilter);
            return Result;
        }

    }
}
