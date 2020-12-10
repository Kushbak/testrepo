using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using FinanceManagmentApplication.Models.RemittanceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinanceManagmentApplication.Documents.ExcelDocument
{
    public class RemittanceReport
    {
        public MemoryStream CreateExcelDoc(List<RemittanceExcelModel> Remittances, MemoryStream mem)
        {   

            
                SpreadsheetDocument document = SpreadsheetDocument.
                    Create(mem, SpreadsheetDocumentType.Workbook);

                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                // Adding style
                WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylePart.Stylesheet = GenerateStylesheet();
                stylePart.Stylesheet.Save();

                // Setting up columns
                Columns columns = new Columns(
                        new Column // Id column
                        {
                            Min = 1,
                            Max = 1,
                            Width = 4,
                            CustomWidth = true
                        },
                        new Column // Name and Birthday columns
                        {
                            Min = 2,
                            Max = 3,
                            Width = 15,
                            CustomWidth = true
                        },
                        new Column // Salary column
                        {
                            Min = 4,
                            Max = 5,
                            Width = 15,
                            CustomWidth = true
                        });

                worksheetPart.Worksheet.AppendChild(columns);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Remittance" };

                sheets.Append(sheet);

                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                // Constructing header
                Row row = new Row();

                row.Append(
                    ConstructCell("Id", CellValues.String, 2),
                    ConstructCell("Дата перевода", CellValues.String, 2),
                    ConstructCell("Сумма", CellValues.String, 2),
                    ConstructCell("Счет 1", CellValues.String, 2),
                    ConstructCell("Счет 2", CellValues.String, 2));

                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);

                // Inserting each employee
                foreach (var remittance in Remittances)
                {
                    row = new Row();

                    row.Append(
                        ConstructCell(remittance.Id.ToString(), CellValues.Number, 1),
                        ConstructCell(remittance.ActionDate.ToString("yyyy/MM/dd"), CellValues.String, 1),
                        ConstructCell(remittance.Sum.ToString(), CellValues.String, 1),
                        ConstructCell(remittance.Score, CellValues.String, 1),
                        ConstructCell(remittance.Score2, CellValues.String, 1));

                    sheetData.AppendChild(row);
                }

                worksheetPart.Worksheet.Save();
                document.Close();
                return mem;
        }

        private Cell ConstructCell(string value, CellValues dataType, uint styleIndex = 0)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex
            };
        }

        private Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - default
                    new FontSize() { Val = 10 }

                ),
                new Font( // Index 1 - header
                    new FontSize() { Val = 10 },
                    new Bold(),
                    new Color() { Rgb = "FFFFFF" }

                ));

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "66666666" } })
                    { PatternType = PatternValues.Solid }) // Index 2 - header
                );

            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                );

            CellFormats cellFormats = new CellFormats(
                    new CellFormat(), // default
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true } // header
                );

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }
    }
}
