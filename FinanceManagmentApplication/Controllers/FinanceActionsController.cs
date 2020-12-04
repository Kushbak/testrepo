using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using FinanceManagmentApplication.DAL.Entities;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.FinanceActiveModels;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.WebModels.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("FinanceActions")]
    [ApiController]
    public class FinanceActionsController : ControllerBase
    {
        UserManager<User> _userManager;
        private IFinanceActionService FinanceActionService { get; }
        public FinanceActionsController(IFinanceActionService financeActionService, UserManager<User> userManager)
        {
            _userManager = userManager;
            FinanceActionService = financeActionService;
        }
        /// <summary>
        /// Full list of all transactions with a pagination and filtering.
        /// </summary> 
        

        [System.Web.Http.HttpGet]
        [Route("Index")]

        public async Task<ActionResult<PagedResponse<List<FinanceActiveIndexModel>>>> Index([FromQuery]PaginationFilter filter)
        {
            return await FinanceActionService.FinanceActionPagination(filter);
        }


    }


}
