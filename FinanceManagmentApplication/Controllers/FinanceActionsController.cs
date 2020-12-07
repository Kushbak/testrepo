using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.WebModels.Wrappers;
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
        /// <summary>
        /// Full list of all transactions with a pagination and filtering.(Не актуально)
        /// </summary> 
        

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("Index")]
        public async Task<ActionResult<PagedResponse<List<FinanceActiveIndexModel>>>> Index([FromQuery]PaginationFilter filter)
        {
            return await FinanceActionService.FinanceActionPagination(filter);
        }


    }


}
