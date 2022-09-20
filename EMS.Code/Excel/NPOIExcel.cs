/*******************************************************************************
 * 
 * Author: HT
 * Description: HT-XA快速开发平台
 * Website：http://ht-xa.com
*********************************************************************************/
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace EMS.Code.Excel
{
    public class NPOIExcel
    {
        private string _title;
        private string _sheetName;
        private string _filePath;
        private string filePath;
        private string fileName;
        private IWorkbook workbook;
        public NPOIExcel(string fileName)
        {
            this.fileName = fileName;
        }


        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool ToExcel(DataTable table)
        {
            FileStream fs = new FileStream(this._filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            IWorkbook workBook = new HSSFWorkbook();
            this._sheetName = this._sheetName.IsEmpty() ? "sheet1" : this._sheetName;
            ISheet sheet = workBook.CreateSheet(this._sheetName);

            //处理表格标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(this._title);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
            row.Height = 500;

            ICellStyle cellStyle = workBook.CreateCellStyle();
            IFont font = workBook.CreateFont();
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 17;
            cellStyle.SetFont(font);
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.Alignment = HorizontalAlignment.Center;
            row.Cells[0].CellStyle = cellStyle;

            //处理表格列头
            row = sheet.CreateRow(1);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                row.Height = 350;
                sheet.AutoSizeColumn(i);
            }

            //处理数据内容
            for (int i = 0; i < table.Rows.Count; i++)
            {
                row = sheet.CreateRow(2 + i);
                row.Height = 250;
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(table.Rows[i][j].ToString());
                    sheet.SetColumnWidth(j, 256 * 15);
                }
            }

            //写入数据流
            workBook.Write(fs);
            fs.Flush();
            fs.Close();

            return true;
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table"></param>
        /// <param name="title"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public bool ToExcel(DataTable table, string title, string sheetName, string filePath)
        {
            this._title = title;
            this._sheetName = sheetName;
            this._filePath = filePath;
            return ToExcel(table);
        }
        public static XSSFWorkbook GetWorkbook(string strFileName)
        {
            XSSFWorkbook workbook;
            using (FileStream stream = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(stream);
            }
            DocumentSummaryInformation information = PropertySetFactory.CreateDocumentSummaryInformation();
            information.Company = "NPOI";
            //workbook.DocumentSummaryInformation = information;
            SummaryInformation information2 = PropertySetFactory.CreateSummaryInformation();
            information2.Author = "文件作者信息";
            information2.ApplicationName = "创建程序信息";
            information2.LastAuthor = "最后保存者信息";
            information2.Comments = "作者信息";
            information2.Title = "标题信息";
            information2.Subject = "主题信息";
            information2.CreateDateTime = new DateTime?(DateTime.Now);
            // workbook.SummaryInformation = information2;
            return workbook;
        }
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheetAt = null;
            DataTable table = new DataTable();
            int firstRowNum = 0;
            try
            {
                this.workbook = GetWorkbook(this.fileName);
                if (sheetName != null)
                {
                    sheetAt = this.workbook.GetSheet(sheetName);
                    if (sheetAt == null)
                    {
                        sheetAt = this.workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheetAt = this.workbook.GetSheetAt(0);
                }
                if (sheetAt != null)
                {
                    IRow row = sheetAt.GetRow(0);
                    int lastCellNum = row.LastCellNum;
                    if (isFirstRowColumn)
                    {
                        for (int j = row.FirstCellNum; j < lastCellNum; j++)
                        {
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                string stringCellValue = cell.StringCellValue;
                                if (stringCellValue != null)
                                {
                                    DataColumn column = new DataColumn(stringCellValue);
                                    table.Columns.Add(column);
                                }
                            }
                        }
                        firstRowNum = sheetAt.FirstRowNum + 1;
                    }
                    else
                    {
                        firstRowNum = sheetAt.FirstRowNum;
                    }
                    int lastRowNum = sheetAt.LastRowNum;
                    for (int i = firstRowNum; i <= lastRowNum; i++)
                    {
                        IRow row2 = sheetAt.GetRow(i);
                        if (row2 != null)
                        {
                            DataRow row3 = table.NewRow();
                            for (int k = row2.FirstCellNum; k < lastCellNum; k++)
                            {
                                ICell cell2 = row2.GetCell(k);
                                if (cell2 != null)
                                {
                                    if (cell2.CellType == CellType.Numeric)
                                    {
                                        short dataFormat = cell2.CellStyle.DataFormat;

                                        if (((((dataFormat == 14) || (dataFormat == 0x1f)) || (dataFormat == 0x39)) || (dataFormat == 0x3a)) || (dataFormat == 22))
                                        {
                                            row3[k] = cell2.DateCellValue.ToDateTimeString();
                                        }
                                        else
                                        {
                                            row3[k] = cell2.ToString();
                                        }
                                    }
                                    else
                                    {
                                        row3[k] = cell2.ToString();
                                    }
                                }
                            }
                            table.Rows.Add(row3);
                        }
                    }
                }
                //DataTable dt= GetDataTableFromSheet(sheetAt, 0);
                return table;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: " + exception.Message);
                return null;
            }
        }
    }
}
