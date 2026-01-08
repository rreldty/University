using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZUG1Dto
    {
        #region Custom Property

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public List<ZUG2Dto> lstZUG2 { get; set; }

        [DataMember]
        public List<ZUSRDto> lstZUSR { get; set; }

        [DataMember]
        public ZUG2Dto objZUG2 { get; set; }
    
        [DataMember]
        public string Result { get; set; }

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
        public string ZGCONO { get; set; }

        [DataMember]
        public string ZGBRNO { get; set; }

        [DataMember]
        public string ZGUGNO { get; set; }

        [DataMember]
        public string ZGUGNA { get; set; }

        [DataMember]
        public string ZGREMA { get; set; }

        [DataMember]
        public string ZGSYST { get; set; }

        [DataMember]
        public string ZGSTAT { get; set; }

        [DataMember]
        public decimal ZGRCST { get; set; }

        [DataMember]
        public decimal ZGCRDT { get; set; }

        [DataMember]
        public decimal ZGCRTM { get; set; }

        [DataMember]
        public string ZGCRUS { get; set; }

        [DataMember]
        public decimal ZGCHDT { get; set; }

        [DataMember]
        public decimal ZGCHTM { get; set; }

        [DataMember]
        public string ZGCHUS { get; set; }
       

        #endregion
    }

    public class ZUG1MappingDto : Mapper<ZUG1Dto>
    {
        protected override ZUG1Dto PopulateItem(IDataRecord dr)
        {
            ZUG1Dto dto = new ZUG1Dto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
