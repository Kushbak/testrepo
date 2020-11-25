using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.FinanceModels;
using FinanceManagmentApplication.Services;
using FinanceManagmentApplication.Services.Contracts;
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

        [HttpGet]
        [Route("Projects")]
        public async Task<ActionResult<List<ProjectFinanceModel>>> Projects()
        {
            return await financeService.GetFinanceInformationToProjects();
        }

        [HttpGet]
        [Route("Operations")]
        public async Task<ActionResult<List<OperationFinanceModel>>> Operations()
        {
            return await financeService.GetFinanceInformationToOperations();
        }
    }
}
