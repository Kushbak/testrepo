using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagmentApplication.BL.Services.Contracts;
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

        [HttpGet]
        [Route("GetRemittanceExcelReport")]
        public async Task<IActionResult> GetRemittanceExcelReport()
        {
            try
            {
                var fileBytes = new byte[10];
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Remittances");
            }
            catch(Exception e)
            {
                return Ok(e.StackTrace);
            }
        }

        [HttpGet]
        [Route("GetRemittancePdfReport")]
        public async Task<IActionResult> GetRemittancePdfReport()
        {
            try
            {
                var fileBytes = await ExportService.RemittanceExport("Documents/", "Remittances", "pdf");
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Remittances.pdf");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpGet]
        [Route("GetTransactionsExcelReport")]
        public async Task<IActionResult> GetTransactionReport()
        {
            var fileBytes = await ExportService.TransactionExport("Documents/", "Transactions", "xlsx");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Transactions.xlsx");
        }

        [HttpGet]
        [Route("GetTransactionsPdfReport")]
        public async Task<IActionResult> GetTransactionPdfReport()
        {
            var fileBytes = await ExportService.TransactionExport("Documents/", "Transactions", "pdf");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Transactions.pdf");
        }
    } 
}
