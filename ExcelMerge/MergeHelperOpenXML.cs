using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;



namespace ExcelMerge
{
    public class MergeHelperOpenXML
    {

        public void MergeExcel(string file1, string file2)
        {

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            DataSet data = new DataSet();

            data.Tables.Add(GenerateDatatable(file1));
            data.Tables.Add(GenerateDatatable(file2));

            var datatable1 = data.Tables[0];
            var datatable2 = data.Tables[1];

            DataTable finalDatatable = new DataTable();
            finalDatatable = BuildMergeExcelSchema();


            for (int i = 0; i <= datatable1.Rows.Count - 1; i++)
            {
                var value = datatable1.Rows[i]["numero_tiers"];
                DataRow DataRow1 = datatable1.Rows[i];
                string expression = "numero_tiers = '" + value.ToString() + "'";
                DataRow DataRow2 = datatable2.Select(expression).FirstOrDefault();
                finalDatatable.Rows.Add(new object[] { DataRow1["numero_tiers"] });
            }


            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls |All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkBook.SaveAs(saveFileDialog.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                ExportDatatableToExcel(finalDatatable, saveFileDialog.FileName);

            }

            
        }

        private DataTable BuildMergeExcelSchema()
        {
            DataTable dt = new DataTable();


            return dt;
        }

        private DataTable GenerateDatatable(string file1)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(file1, false))
            {
                //Read the first Sheet from Excel file.
                Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                //Get the Worksheet instance.
                Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                //Fetch all the rows present in the Worksheet.
                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                foreach (Row row in rows)
                {
                    //Use the first row to add columns to DataTable.
                    if (row.RowIndex.Value == 1)
                    {
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            dt.Columns.Add(GetValue(doc, cell));
                        }
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            if(cell !=null && cell.CellValue!=null)
                            { 
                                dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                                i++;
                            }
                        }
                    }
                }
                return dt;
            }
        }

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            
            
                string value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
                
          
            return value;
        }


        private void ExportDatatableToExcel(DataTable table, string destination)
        {
            using (var workbook = SpreadsheetDocument.Create(destination, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = workbook.AddWorkbookPart();
                workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
                workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();

                uint sheetId = 1;

                
                    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                    sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

                    DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                    string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                    if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Count() > 0)
                    {
                        sheetId =
                            sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                    }

                    DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet() { Id = relationshipId, SheetId = sheetId, Name = table.TableName };
                    sheets.Append(sheet);

                    DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    List<String> columns = new List<string>();
                    foreach (DataColumn column in table.Columns)
                    {
                        columns.Add(column.ColumnName);

                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName);
                        headerRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(headerRow);

                    foreach (DataRow dsrow in table.Rows)
                    {
                        DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                        foreach (String col in columns)
                        {
                            DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString()); //
                            newRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(newRow);
                    }
                
            }
        }
    }
}
