using System;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.Diagnostics;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;

namespace Utils
{
    public static class NPOIExcelHelper
    {
        /// <summary>
        /// open excel workbook(工作簿) 
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static IWorkbook OpenWorkbook(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Write);
            NPOI.SS.UserModel.IWorkbook workbook = WorkbookFactory.Create(fileStream);
            //var ext = System.IO.Path.GetExtension(path);
            //if (ext == ".xls")
            //	workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(fileStream);
            //else if (ext == ".xlsx")
            //	workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(fileStream);
            //else
            //{ }
            return workbook;
        }
        /// <summary>
        /// search first or default excel worksheet whose name is started with pattern(在工作簿中查找以pattern开头的工作表)
        /// </summary>
        /// <param name="workbook">要查找的工作簿</param>
        /// <param name="pattern">指定开头字符串</param>
        /// <returns></returns>
        public static ISheet FirstOrDefaultWorksheetNameStartWith(IWorkbook workbook, string pattern)
        {
            NPOI.SS.UserModel.ISheet worksheet = null;
            var length = workbook.NumberOfSheets;
            for (int i = 0; i < length; i++)
            {
                worksheet = workbook.GetSheetAt(i);
                if (worksheet.SheetName.StartsWith(pattern))
                    break;
                worksheet = null;
            }
            return worksheet;
        }
        /// <summary>
        /// read excel worksheet
        /// </summary>
        /// <param name="worksheet">要读取的工作表</param>
        /// <param name="rows">指定读取多少行</param>
        /// <param name="columns">指定读取多少列</param>
        /// <param name="startIndex">通常前3行是:注释,字段名和字段类型,如果注释全部不填,该行也应该读取</param>
        /// <returns></returns>
        public static List<string[]> ReadLines(ISheet worksheet, int rows = 0, int columns = 0, int startIndex = 3)
        {
            if (rows <= 0)
                rows = worksheet.LastRowNum + 1;
            else
                rows = Math.Min(rows, worksheet.LastRowNum + 1);
            if (rows == 0)
                return new List<string[]>();

            int rownum = 0;
            IRow row = worksheet.GetRow(rownum);
            if (columns <= 0)
                columns = row.LastCellNum + 1;
            else
                columns = Math.Min(columns, row.LastCellNum + 1);
            //List<>的每一个元素是一行数据,string[]的每一个元素是对应单元格的string值
            List<string[]> lines = new List<string[]>(rows);
            while (rownum < rows)
            {
                if (row != null)
                {
                    bool add = false;
                    var line = new string[columns];
                    for (int i = 0; i < columns; i++)
                    {
                        var cell = row.GetCell(i);
                        if (cell != null)
                        {
                            if (cell.CellType != CellType.Formula)
                                line[i] = cell.ToString().Trim();
                            else
                            {
                                if (cell.CachedFormulaResultType == CellType.Numeric)
                                    line[i] = cell.NumericCellValue.ToString();
                                else if (cell.CachedFormulaResultType == CellType.String)
                                    line[i] = cell.StringCellValue.Trim();
                                else if (cell.CachedFormulaResultType == CellType.Boolean)
                                    line[i] = cell.BooleanCellValue.ToString();
                            }
                        }
                        else
                        {
                            line[i] = null;
                        }
                        if (!string.IsNullOrEmpty(line[i]))
                            add = true;
                    }
                    if (add)
                        lines.Add(line);
                }
                else
                {
                    if (rownum < startIndex)
                    {
                        var line = new string[columns];
                        lines.Add(line);
                    }
                }
                rownum++;
                row = worksheet.GetRow(rownum);
            }
            return lines;
        }
        /// <summary>
        /// close excel workbook
        /// </summary>
        /// <param name="workbook"></param>
        public static void CloseWorkbook(IWorkbook workbook)
        {
            workbook.Close();
        }

        public static Stream RenderDataTableToExcel(DataTable SourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(0);

            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value. 
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in SourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }

        public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            ISheet sheet = workbook.GetSheetAt(SheetIndex);

            DataTable table = new DataTable();

            IRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)

                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

    }
}