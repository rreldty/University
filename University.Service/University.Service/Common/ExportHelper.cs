using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Web;
using System.IO;
using System.Threading;

using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace University.Api.Common
{
    public enum ExportFileType
    {
        Excel,
        CSV
    }

    public class ExportHelper
    {
        public void ExportFile(DataTable dtData, ExportFileType _FileType, FileInfo objFileInfo, string strCsvSeperator = ";")
        {
            if (!Directory.Exists(objFileInfo.DirectoryName))
            {
                Directory.CreateDirectory(objFileInfo.DirectoryName);
            }

            List<FileInfo> lstFileInfo = new DirectoryInfo(objFileInfo.DirectoryName).GetFiles().Where(f => f.CreationTime.Date < DateTime.Now.AddDays(-1).Date || f.LastWriteTime.Date < DateTime.Now.AddDays(-1).Date).ToList();
            foreach (FileInfo fi in lstFileInfo)
            {
                fi.Delete();
            }

            switch (_FileType)
            {
                case ExportFileType.Excel:
                    {
                        ExcelPackage excel = CreateExcelFile(dtData);
                        excel.SaveAs(objFileInfo);
                        break;
                    }
                case ExportFileType.CSV:
                    {
                        CreateCsvDocument(dtData, objFileInfo.FullName, strCsvSeperator);
                        break;
                    }
                default:
                    break;
            }
        }

        #region Create Excel File
        public ExcelPackage CreateExcelFile(DataTable dtData)
        {
            ExcelPackage excel = new ExcelPackage();

            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            //Header of table  
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            workSheet.Row(1).Style.Font.Bold = true;

            for (int y = 0; y < dtData.Columns.Count; y++)
            {
                workSheet.Cells[1, (y + 1)].Value = dtData.Columns[y].ColumnName;
            }

            //Body of table 
            for (int x = 0; x < dtData.Rows.Count; x++)
            {
                for (int y = 0; y < dtData.Columns.Count; y++)
                {
                    workSheet.Cells[(x + 2), (y + 1)].Value = dtData.Rows[x][y];
                }
            }

            for (int y = 0; y < dtData.Columns.Count; y++)
            {
                workSheet.Column(y + 1).AutoFit();
            }

            return excel;
        }

        public void CreateCsvDocument(DataTable dtData, string strFullPath, string strCsvSeperator = ";")
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = dtData.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(strCsvSeperator, columnNames));

            foreach (DataRow dr in dtData.Rows)
            {
                IEnumerable<string> fields = dr.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(strCsvSeperator, fields));
            }

            if (File.Exists(strFullPath))
            {
                File.Delete(strFullPath);
            }

            File.WriteAllText(strFullPath, sb.ToString());
        }
        #endregion

        #region List<T> To Data Table
        //This function is adapated from: http://www.codeguru.com/forum/showthread.php?t=450171
        private DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[info.Name] = info.GetValue(t, null);
                    else
                        row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        private Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }

        private bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }
        #endregion

    }
}