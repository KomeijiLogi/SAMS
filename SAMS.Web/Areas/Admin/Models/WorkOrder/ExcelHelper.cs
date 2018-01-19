using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class ExcelHelper :IDisposable
    {
        private string fileName = null;    //定义文件名
        private IWorkbook workbook=null;
        private FileStream fs = null;
        private bool disposed;

        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">数据来源</param>
        /// <param name="sheetname">要导入的excel的sheet的名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public FileStream DataTableToExcel(DataTable data,string sheetName,bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName,FileMode.OpenOrCreate,FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx")!=-1)    //2007 version and +
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileName.IndexOf(".xls")!=-1) //2003 version and -
            {
                workbook = new HSSFWorkbook();
            }
            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }
                if (isColumnWritten == true)   //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }
                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs);

                return fs;

            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 将dataTable转换成文件流的形式返回
        /// </summary>
        /// <param name="dt">数据来源</param>
        /// <returns>转换后的文件流</returns>
        public byte[] DataTableExchangeToStream(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
           
            //fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") != -1)    //2007 version and +
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileName.IndexOf(".xls") != -1) //2003 version and -
            {
                workbook = new HSSFWorkbook();
            }
            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return null;
                }
                if (isColumnWritten == true)   //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }
                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                //workbook.Write(fs);

                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                byte[] bytes = ms.ToArray();
                workbook = null;
                ms.Close();
                ms.Dispose();
                return bytes;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                
            }
            return null;
        }

        public  MemoryStream Export(List<DataTable> listDT, int[] widths, string sheetName)
        {
            //先创建一个流
            MemoryStream ms = new MemoryStream();
            if (listDT != null && listDT.Count != 0)
            {
                try
                {
                    //新建一个excel
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    //excel样式
                    HSSFCellStyle style = (HSSFCellStyle)workbook.CreateCellStyle();
                    foreach (DataTable dt in listDT)
                    {
                        //创建一个sheet
                        ISheet sheet = workbook.CreateSheet(sheetName);
                        //给指定sheet的内容设置每列宽度（index从0开始，width1000相当于excel设置的列宽3.81）
                        for (int i = 0; i < widths.Length; i++)
                        {
                            sheet.SetColumnWidth(i, widths[i]);
                        }
                        //在sheet里创建行
                        NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(0);
                        //第一行，列名
                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            row1.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                        }
                        for (var r = 0; r < dt.Rows.Count; r++)
                        {
                            var row_r = sheet.CreateRow(r + 1);
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                row_r.CreateCell(i).SetCellValue(dt.Rows[r][i].ToString());
                            }
                        }
                    }
                    //写入流
                    workbook.Write(ms);
                    ms.Flush();
                    return ms;
                }
                catch (Exception ex)
                {
                    //
                }
            }
            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    if (fs != null)
                        fs.Close();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                fs = null;
                disposed = true;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~ExcelHelper() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}