using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZPGMDto
    {
        #region General Property

        [DataMember]
        public string ZPCONO { get; set; }

        [DataMember]
        public string ZPBRNO { get; set; }

        [DataMember]
        public string ZPAPNO { get; set; }

        [DataMember]
        public string ZPPGNO { get; set; }

        [DataMember]
        public string ZPPGNA { get; set; }

        [DataMember]
        public string ZPPURL { get; set; }

        [DataMember]
        public string ZPREMA { get; set; }

        [DataMember]
        public string ZPSYST { get; set; }

        [DataMember]
        public string ZPSTAT { get; set; }

        [DataMember]
        public decimal ZPRCST { get; set; }

        [DataMember]
        public decimal ZPCRDT { get; set; }

        [DataMember]
        public decimal ZPCRTM { get; set; }

        [DataMember]
        public string ZPCRUS { get; set; }

        [DataMember]
        public decimal ZPCHDT { get; set; }

        [DataMember]
        public decimal ZPCHTM { get; set; }

        [DataMember]
        public string ZPCHUS { get; set; }

        #endregion

        #region Custom Property
    
        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalPage { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }

        #endregion
    }

    public class ZPGMMappingDto : Mapper<ZPGMDto>
    {
        protected override ZPGMDto PopulateItem(IDataRecord dr)
        {
            ZPGMDto dto = new ZPGMDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
