using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZAUTDto
    {
        #region Custom Property

        [DataMember]
        public string ZMMENO { get; set; }

        [DataMember]
        public string ZMMENA { get; set; }

        [DataMember]
        public string ZMMETY { get; set; }

        [DataMember]
        public string ZMMEPA { get; set; }

        [DataMember]
        public string ZMPARM { get; set; }

        [DataMember]
        public string ZMMESQ { get; set; }

        [DataMember]
        public string ZMREMA { get; set; }

        [DataMember]
        public string ZMIURL { get; set; }

        [DataMember]
        public string ZAAPNA { get; set; }

        [DataMember]
        public string ZAAURL { get; set; }

        [DataMember]
        public string ZAIURL { get; set; }

        [DataMember]
        public string ZAACLR { get; set; }

        [DataMember]
        public decimal ZAAPSQ { get; set; }

        [DataMember]
        public string ZAREMA { get; set; }

        [DataMember]
        public string ZPPURL { get; set; }

        [DataMember]
        public string ZMPGNO { get; set; }

        [DataMember]
        public string ZRVANA { get; set; }

        [DataMember]
        public string ZPREMA { get; set; }

        [DataMember]
        public string ZGUGNA { get; set; }

        [DataMember]
        public string ZHUSNO { get; set; }

        [DataMember]
        public string ZTRCNA { get; set; }

        [DataMember]
        public bool IsSuperAdmin { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public List<ZAUTDto> lstZAUT { get; set; }

        [DataMember]
        public List<ZMNUDto> lstZMNU { get; set; }

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
        public string ZTCONO { get; set; }

        [DataMember]
        public string ZTBRNO { get; set; }

        [DataMember]
        public string ZTUGNO { get; set; }

        [DataMember]
        public string ZTAPNO { get; set; }

        [DataMember]
        public string ZTMENO { get; set; }

        [DataMember]
        public string ZTRIGH { get; set; }

        [DataMember]
        public string ZTSYST { get; set; }

        [DataMember]
        public string ZTSTAT { get; set; }

        [DataMember]
        public decimal ZTRCST { get; set; }

        [DataMember]
        public decimal ZTCRDT { get; set; }

        [DataMember]
        public decimal ZTCRTM { get; set; }

        [DataMember]
        public string ZTCRUS { get; set; }

        [DataMember]
        public decimal ZTCHDT { get; set; }

        [DataMember]
        public decimal ZTCHTM { get; set; }

        [DataMember]
        public string ZTCHUS { get; set; }

        #endregion        
    }

    public class ZAUTMappingDto : Mapper<ZAUTDto>
    {
        protected override ZAUTDto PopulateItem(IDataRecord dr)
        {
            ZAUTDto dto = new ZAUTDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
