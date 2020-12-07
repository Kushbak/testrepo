using AutoMapper;
using FinanceManagmentApplication.BL.Services.Contracts;
using FinanceManagmentApplication.DAL.Factories;
using FinanceManagmentApplication.Documents.ExcelDocument;
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
        private IUnitOfWorkFactory UnitOfWorkFactory { get; }

        public ExportService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<byte[]> RemittanceExport(string Path, string Name, string format)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var path = Path + Name + ".xlsx";
                var path2 = Path + Name + "." + format;
                var Remittances = uow.Remittances.GetFullRemittances();
                var Models = Mapper.Map<List<RemittanceExcelModel>>(Remittances);
                var Report = new RemittanceReport();
                Report.CreateExcelDoc(path, Models);
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                ExcelFile workbook = ExcelFile.Load(path);
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                workbook.Save(path2);

                string Files = path2;
                byte[] fileBytes = System.IO.File.ReadAllBytes(Files);
                System.IO.File.WriteAllBytes(Files, fileBytes);
                MemoryStream ms = new MemoryStream(fileBytes);
                File.Delete(path);
                if(format != "xlsx")
                    File.Delete(path2);

                return fileBytes;
            }
        }

        public async Task<byte[]> TransactionExport(string Path, string Name, string format)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var path = Path + Name + ".xlsx";
                var path2 = Path + Name + "." + format;
                var Transactions = uow.Transactions.GetTransactionsToIndex();
                var Models = Mapper.Map<List<TransactionExcelModel>>(Transactions);
                var Report = new TransactionReport();
                Report.CreateExcelDoc(path, Models);

                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                ExcelFile workbook = ExcelFile.Load(path);
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                workbook.Save(path2);

                string Files = path2;
                byte[] fileBytes = System.IO.File.ReadAllBytes(Files);
                System.IO.File.WriteAllBytes(Files, fileBytes);
                MemoryStream ms = new MemoryStream(fileBytes);
                File.Delete(path);
                if (format != "xlsx")
                    File.Delete(path2);

                return fileBytes;
            }
        }
    }
}
