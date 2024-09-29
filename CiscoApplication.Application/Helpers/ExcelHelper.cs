using Ardalis.Result;
using CiscoApplication.Domain.Entities;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
namespace CiscoApplication.Application.Helpers
{
    public class ExcelHelper
    {
        public static Result<Excel.Range> ReadExcelFiles(string filePath, int sheetNo)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            if (xlWorkbook.Sheets.Count < sheetNo)
            {
                return Result.Error($"Maximum Sheets is {xlWorkbook.Sheets.Count}");
            }
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[sheetNo];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            return Result.Success(xlRange);
        }

        public static List<Item> ConvertExcelToItems(Excel.Range xlRange)
        {
            List<Item> items = new List<Item>();
            for (int row = 3; row <= 4 /*xlRange.Rows.Count*/; row++)
            {
                var item = Item.Create
                (
                    Guid.NewGuid(),
                     int.Parse(xlRange.Cells[row, 2].Text),
                     xlRange.Cells[row, 3].Text,
                     xlRange.Cells[row, 4].Text,
                     xlRange.Cells[row, 5].Text,
                     xlRange.Cells[row, 6].Text,
                     decimal.Parse(xlRange.Cells[row, 7].Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US")),
                     float.Parse(xlRange.Cells[RowIndex: row, 8].Text.Replace("%", "")) / 100
                   );
                if (item.Status == ResultStatus.Ok)
                {
                    items.Add(item.Value);
                }
            }
            return items;
        }

        // make excel file from list of objects 
        public static string CreateExcelFile(IReadOnlyList<Item> items, string filePath)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlWorksheet.Cells[2] = string.Concat("Effective Date - ", DateTime.Now.ToString("dd/MM/yyyy"));
            xlWorksheet.Cells[2, 1] = "";
            xlWorksheet.Cells[2, 2] = "Band";
            xlWorksheet.Cells[2, 3] = "Category Code";
            xlWorksheet.Cells[2, 4] = "Manufacturer";
            xlWorksheet.Cells[2, 5] = "Part SKU";
            xlWorksheet.Cells[2, 6] = "Item Description";
            xlWorksheet.Cells[2, 7] = "List Price";
            xlWorksheet.Cells[2, 8] = "Minimum Discount";
            xlWorksheet.Cells[2, 9] = "Discounted Price";
            for (int i = 1; i < items.Count; i++)
            {
                xlWorksheet.Cells[i + 2, 1] = "";
                xlWorksheet.Cells[i + 2, 2] = items[i].Band;
                xlWorksheet.Cells[i + 2, 3] = items[i].CategoryCode;
                xlWorksheet.Cells[i + 2, 4] = items[i].Manufacturer;
                xlWorksheet.Cells[i + 2, 5] = items[i].PartSKU;
                xlWorksheet.Cells[i + 2, 6] = items[i].ItemDescription;
                xlWorksheet.Cells[i + 2, 7] = items[i].ListPrice;
                xlWorksheet.Cells[i + 2, 8] = items[i].MinimumDiscount;
                xlWorksheet.Cells[i + 2, 9] = items[i].DiscountedPrice;
            }
            xlWorkbook.SaveAs(Filename: filePath);
            xlWorkbook.Close();
            xlApp.Quit();
            return filePath;
        }
    }
}
