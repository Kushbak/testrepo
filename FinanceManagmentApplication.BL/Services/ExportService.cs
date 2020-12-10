using AutoMapper;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Documents.ExcelDocument;
using FinanceManagmentApplication.Filter;
using FinanceManagmentApplication.Models.RemittanceModels;
using FinanceManagmentApplication.Models.TransactionModels;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApplication.BL.Services
{
    public class ExportService: IExportService
    {   
        private IFinanceActionService FinanceActionService { get; }

        private IUnitOfWorkFactory UnitOfWorkFactory { get; }

        public ExportService(IFinanceActionService financeActionService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            FinanceActionService = financeActionService;
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        //Генерацию отчетов отдельно для переводов и транзакций, оставим до лучших времен.

        //public async Task<byte[]> RemittanceExport()        
        //{
        //    using (var uow = UnitOfWorkFactory.Create())
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            var Remittances = uow.Remittances.GetFullRemittances();
        //            var Models = Mapper.Map<List<RemittanceExcelModel>>(Remittances);
        //            var Report = new RemittanceReport();
        //            var array = Report.CreateExcelDoc(Models, ms);

        //            var bytes = array.ToArray();

        //            return bytes;
        //        }
        //    }
        //}

        //public async Task<byte[]> TransactionExport()
        //{
        //    using (var uow = UnitOfWorkFactory.Create())
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            var Transactions = uow.Transactions.GetTransactionsToIndex();
        //            var Models = Mapper.Map<List<TransactionExcelModel>>(Transactions);
        //            var Report = new TransactionReport();
        //            var array = Report.CreateExcelDoc(Models, ms);

        //            var bytes = array.ToArray();

        //            return bytes;
        //        }

        //    }
        //}

        public async Task<byte[]> FinanceActionsReport(PaginationFilter filter)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    filter.PageNumber = 1;
                    filter.PageSize = await uow.FinanceActions.Count();
                    var Models = await FinanceActionService.FinanceActionPagination(filter);
                    var Report = new FinanceActionReport();
                    var array = Report.CreateExcelDoc(Models.Data, ms);

                    var bytes = array.ToArray();

                    return bytes;
                }
            }

        }
    }
}
