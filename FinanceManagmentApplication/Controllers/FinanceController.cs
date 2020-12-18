using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.Models.FinanceModels;
using FinanceManagmentApplication.BL.Services;
using FinanceManagmentApplication.BL.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinanceManagmentApplication.Models.FilterModels;
using FinanceManagmentApplication.Models.FinanceActiveModels;

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
        ///Выдает статистику по каждому проекту
        /// </summary>
        [HttpGet]
        [Route("Projects")]
        public async Task<ActionResult<List<ProjectFinanceModel>>> Projects()
        {
            return await financeService.GetFinanceInformationToProjects();
        }
        /// <summary>
        ///Выдает статистику по каждой операции
        /// </summary>
        [HttpGet]
        [Route("Operations")]
        public async Task<ActionResult<List<OperationFinanceModel>>> Operations()
        {
            return await financeService.GetFinanceInformationToOperations();
        }

        [HttpGet]
        [Route("Statistics")]
        public async Task<ActionResult<List<FinanceActiveIndexModel>>> Statistics([FromQuery] StatisticFilter filter)
        {
            return await financeService.GetStatisticsData(filter);
        }

        [HttpGet]
        [Route("Settings")]
        public async Task<ActionResult> Settings()
        {
            return Ok(await financeService.GetSettingsModel());
        }
    }
}
