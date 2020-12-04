using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.FinanceModels;
using FinanceManagmentApplication.BL.Services;
using FinanceManagmentApplication.BL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("Finance")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        IFinanceService financeService { get; set; }

        public FinanceController(IFinanceService service)
        {
            financeService = service;
        }
        /// <summary>
        ///Full finance list by projects
        /// </summary>
        [HttpGet]
        [Route("Projects")]
        public async Task<ActionResult<List<ProjectFinanceModel>>> Projects()
        {
            return await financeService.GetFinanceInformationToProjects();
        }
        /// <summary>
        ///Full finance list by operations
        /// </summary>
        [HttpGet]
        [Route("Operations")]
        public async Task<ActionResult<List<OperationFinanceModel>>> Operations()
        {
            return await financeService.GetFinanceInformationToOperations();
        }
    }
}
