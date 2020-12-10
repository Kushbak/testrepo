using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApplication.Controllers
{
    [Route("Reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IExportService ExportService { get; set; }

        public ReportController(IExportService exportService)
        {
            ExportService = exportService;
        }

        //[HttpGet]
        //[Route("GetRemittanceExcelReport")]
        //public async Task<IActionResult> GetRemittanceExcelReport()
        //{
        //    try
        //    {
        //        var fileBytes = await ExportService.RemittanceExport();
        //        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Remittances.xlsx");
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(e.StackTrace);
        //    }
        //}

        //[HttpGet]
        //[Route("GetTransactionsExcelReport")]
        //public async Task<IActionResult> GetTransactionReport()
        //{
        //    var fileBytes = await ExportService.TransactionExport();
        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Transactions.xlsx");
        //}

        [HttpGet]
        [Route("GetFinanceActionsExcelReport")]
        public async Task<IActionResult> GetFinanceActionsReport([FromQuery] PaginationFilter filter)
        {
            var fileBytes = await ExportService.FinanceActionsReport(filter);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "FinanceActions.xlsx");
        }
    }
}
