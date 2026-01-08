using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZBUMDto
    {
        #region Additional Property

        [DataMember]
        public string ZBBRNA { get; set; }

        [DataMember]
        public string ZCCONA { get; set; }
        
        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalPage { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }

        #endregion

        #region General Property

        [DataMember]
        public string ZVCONO { get; set; }

        [DataMember]
        public string ZVBRNO { get; set; }

        [DataMember]
        public string ZVUSNO { get; set; }

        [DataMember]
        public string ZVREMA { get; set; }

        [DataMember]
        public string ZVSYST { get; set; }

        [DataMember]
        public string ZVSTAT { get; set; }

        [DataMember]
        public decimal ZVRCST { get; set; }

        [DataMember]
        public decimal ZVCRDT { get; set; }

        [DataMember]
        public decimal ZVCRTM { get; set; }

        [DataMember]
        public string ZVCRUS { get; set; }

        [DataMember]
        public decimal ZVCHDT { get; set; }

        [DataMember]
        public decimal ZVCHTM { get; set; }

        [DataMember]
        public string ZVCHUS { get; set; }

        #endregion
    }

    public class MapZBUMDto : Mapper<ZBUMDto>
    {
        protected override ZBUMDto PopulateItem(IDataRecord dr)
        {
            ZBUMDto dto = new ZBUMDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}