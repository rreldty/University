using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZMNUDto
    {
        #region Custom Property

        [DataMember]
        public string ZRVANA { get; set; }

        [DataMember]
        public string ZMUGNO { get; set; }

        [DataMember]
        public string ZPPURL { get; set; }

        [DataMember]
        public string RCST { get; set; }

        [DataMember]
        public string ZTUGNO { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

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
        public string ZMCONO { get; set; }

        [DataMember]
        public string ZMBRNO { get; set; }

        [DataMember]
        public string ZMAPNO { get; set; }

        [DataMember]
        public string ZMMENO { get; set; }

        [DataMember]
        public string ZMMENA { get; set; }

        [DataMember]
        public string ZMMETY { get; set; }

        [DataMember]
        public string ZMMEPA { get; set; }

        [DataMember]
        public string ZMMESQ { get; set; }

        [DataMember]
        public string ZMPGNO { get; set; }

        [DataMember]
        public string ZMPARM { get; set; }

        [DataMember]
        public string ZMIURL { get; set; }

        [DataMember]
        public string ZMREMA { get; set; }

        [DataMember]
        public string ZMSYST { get; set; }

        [DataMember]
        public string ZMSTAT { get; set; }

        [DataMember]
        public decimal ZMRCST { get; set; }

        [DataMember]
        public decimal ZMCRDT { get; set; }

        [DataMember]
        public decimal ZMCRTM { get; set; }

        [DataMember]
        public string ZMCRUS { get; set; }

        [DataMember]
        public decimal ZMCHDT { get; set; }

        [DataMember]
        public decimal ZMCHTM { get; set; }

        [DataMember]
        public string ZMCHUS { get; set; }

        #endregion
    }

    public class ZMNUMappingDto : Mapper<ZMNUDto>
    {
        protected override ZMNUDto PopulateItem(IDataRecord dr)
        {
            ZMNUDto dto = new ZMNUDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}