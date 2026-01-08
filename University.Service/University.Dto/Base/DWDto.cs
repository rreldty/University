using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace University.Dto.Base
{
    public enum QuerySource
    {
        StoredProcedure,
        Embedded
    }

    public enum OutputType
    {
        Grid,
        ColumnHeader,
        Chart,
        Excel
    }

    [DataContract]
    public class DWColumns
    {
        [DataMember]
        public List<int> DateColumn { get; set; }

        [DataMember]
        public List<int> TimeColumn { get; set; }

        [DataMember]
        public List<int> DateTimeColumn { get; set; }

        [DataMember]
        public List<int> PercentageColumn { get; set; }
    }

    [DataContract]
    public class DWDto
    {
        [DataMember]
        public string Param { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public DbType ParamType { get; set; }
    }

    public class DWMappingDto : Mapper<DWDto>
    {
        protected override DWDto PopulateItem(IDataRecord dr)
        {
            DWDto dto = new DWDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }

    public class DWHeaderDto
    {
        #region Property

        [DataMember]
        public List<string> ColumnHeader { get; set; }

        [DataMember]
        public List<DWColumnDto> ColumnModel { get; set; }

        #endregion
    }

    public class DWColumnDto
    {
        #region Property

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string index { get; set; }

        [DataMember]
        public string stype { get; set; }

        [DataMember]
        public string align { get; set; }

        [DataMember]
        public string formatter { get; set; }

        [DataMember]
        public string summaryType { get; set; }

        [DataMember]
        public string summaryTpl { get; set; }

        [DataMember]
        public int width { get; set; }

        #endregion
    }
}
