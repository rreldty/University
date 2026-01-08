using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using University.Dao.Base;
using University.Dto.Base;

namespace University.Dao.Entity
{
    public class DWDao : BaseDW
    {
        public DWDao(DataSource dbSource)
        {
            this.MainDataSource = dbSource;
        }

        public DWDao()
        {
            this.MainDataSource = DataSource.University;
        }

        public DataTable GetData(out int intTotalPage, out int intTotalRecord, OutputType _OutType, List<DWDto> lstParam, int intPageNumber, int intPageSize, string strOrderBy, QuerySource objQSrc, string strReportQuery, DWColumns _columnsDW, int[] HideColumn)
        {
            intTotalPage = 0;
            intTotalRecord = 0;

            if (_OutType != OutputType.ColumnHeader)
            {
                DataTable dttDW = GetDataBase(out intTotalPage, out intTotalRecord, _OutType, lstParam, intPageNumber, intPageSize, strOrderBy, objQSrc, strReportQuery, _columnsDW, HideColumn);

                foreach (DataColumn dtc in dttDW.Columns)
                {
                    if (dtc.ColumnName.Contains("N:1"))
                    {
                        dtc.ColumnName = dtc.ColumnName.Replace("N:1", "").Trim();
                    }
                    else if (dtc.ColumnName.Contains("N:2"))
                    {
                        dtc.ColumnName = dtc.ColumnName.Replace("N:2", "").Trim();
                    }
                }

                return dttDW;
            }
            else
            {
                return null;
            }
        }

        public DWHeaderDto GetColumn(out int intTotalPage, out int intTotalRecord, List<DWDto> lstParam, int intPageNumber, int intPageSize, string strOrderBy, QuerySource objQSrc, string strReportQuery, DWColumns _columnsDW, int[] HideColumn)
        {
            intTotalPage = 0;
            intTotalRecord = 0;

            DataTable dttDW = GetDataBase(out intTotalPage, out intTotalRecord, OutputType.ColumnHeader, lstParam, intPageNumber, intPageSize, strOrderBy, objQSrc, strReportQuery, _columnsDW, HideColumn);

            DWHeaderDto dto = null;

            if (dttDW != null)
            {
                List<string> lstColHead = new List<string>();
                List<DWColumnDto> lstColModel = new List<DWColumnDto>();

                foreach (DataColumn dtc in dttDW.Columns)
                {
                    if (dtc.ColumnName.Contains("N:1"))
                    {
                        lstColHead.Add(dtc.ColumnName.Replace("N:1", "").Trim());
                    }
                    else if (dtc.ColumnName.Contains("N:2"))
                    {
                        lstColHead.Add(dtc.ColumnName.Replace("N:2", "").Trim());
                    }
                    else
                    {
                        lstColHead.Add(dtc.ColumnName);
                    }

                    DWColumnDto dtoCol = new DWColumnDto();
                    if (dtc.ColumnName.Contains("N:1"))
                    {
                        dtoCol.name = dtc.ColumnName.Replace("N:1", "").Trim();
                        dtoCol.index = dtc.ColumnName.Replace("N:1", "").Trim();
                    }
                    else if (dtc.ColumnName.Contains("N:2"))
                    {
                        dtoCol.name = dtc.ColumnName.Replace("N:2", "").Trim();
                        dtoCol.index = dtc.ColumnName.Replace("N:2", "").Trim();
                    }
                    else
                    {
                        dtoCol.name = dtc.ColumnName;
                        dtoCol.index = dtc.ColumnName;
                    }


                    if (dtc.DataType == Type.GetType("System.String"))
                    {
                        dtoCol.stype = "text";
                        dtoCol.formatter = "";

                        if (dtc.ColumnName.Contains('%'))
                        {
                            dtoCol.align = "right";
                        }
                        else
                        {
                            dtoCol.align = "left";
                        }
                    }

                    if (dtc.DataType == Type.GetType("System.Decimal"))
                    {
                        dtoCol.stype = "number";
                        dtoCol.align = "right";
                        dtoCol.formatter = "currency";
                        dtoCol.summaryType = "sum";

                        if (dtc.ColumnName.Contains("N:1"))
                        {
                            dtoCol.formatter = "cover";
                            dtoCol.summaryType = "sum";
                        }
                        else if (dtc.ColumnName.Contains("N:2"))
                        {
                            dtoCol.formatter = "number";
                            dtoCol.summaryType = "sum";
                        }
                        else
                        {
                            dtoCol.formatter = "currency";
                            dtoCol.summaryType = "sum";
                        }
                    }

                    if (dtc.DataType == Type.GetType("System.Int32"))
                    {
                        dtoCol.stype = "number";
                        dtoCol.align = "right";
                        dtoCol.formatter = "currency";
                        dtoCol.summaryType = "sum";

                        if (dtc.ColumnName.Contains("N:1"))
                        {
                            dtoCol.formatter = "cover";
                            dtoCol.summaryType = "sum";
                        }
                        else if (dtc.ColumnName.Contains("N:2"))
                        {
                            dtoCol.formatter = "number";
                            dtoCol.summaryType = "sum";
                        }
                        else
                        {
                            dtoCol.formatter = "currency";
                            dtoCol.summaryType = "sum";
                        }
                    }

                    lstColModel.Add(dtoCol);
                }

                dto = new DWHeaderDto();
                dto.ColumnHeader = lstColHead;
                dto.ColumnModel = lstColModel;
            }


            return dto;
        }
    }
}
