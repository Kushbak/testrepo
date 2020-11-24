using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.Services.Contracts;
using FinanceManagmentApplication.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("FinanceActions")]
    [ApiController]
    public class FinanceActionsController : ControllerBase
    {
        private IFinanceActionService FinanceActionService { get; }

        public FinanceActionsController(IFinanceActionService financeActionService)
        {
            FinanceActionService = financeActionService;
        }

        [Route("Index")]
        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<FinanceActiveIndexModel>>>> Index([FromQuery]PaginationFilter filter)
        {
            return await FinanceActionService.FinanceActionPagination(filter);
        }
    }


}
