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
            var fileBytes = await ExportService.RemittanceExport("Documents/", "Remittances", "xlsx");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Remittances.xlsx");
        }

        [HttpGet]
        [Route("GetRemittancePdfReport")]
        public async Task<IActionResult> GetRemittancePdfReport()
        {
            var fileBytes = await ExportService.RemittanceExport("Documents/", "Remittances", "pdf");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Remittances.pdf");
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
