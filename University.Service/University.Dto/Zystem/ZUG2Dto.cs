using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZUG2Dto
    {
        #region General Property

        [DataMember]
        public string ZHCONO { get; set; }

        [DataMember]
        public string ZHBRNO { get; set; }

        [DataMember]
        public string ZHUGNO { get; set; }

        [DataMember]
        public string ZHUSNO { get; set; }

        [DataMember]
        public string ZHSYST { get; set; }

        [DataMember]
        public string ZHSTAT { get; set; }

        [DataMember]
        public decimal ZHRCST { get; set; }

        [DataMember]
        public decimal ZHCRDT { get; set; }

        [DataMember]
        public decimal ZHCRTM { get; set; }

        [DataMember]
        public string ZHCRUS { get; set; }

        [DataMember]
        public decimal ZHCHDT { get; set; }

        [DataMember]
        public decimal ZHCHTM { get; set; }

        [DataMember]
        public string ZHCHUS { get; set; }


        #endregion        

        #region Custom Property

        [DataMember]
        public string ZUUSNA { get; set; }

        [DataMember]
        public string ZUNICK { get; set; }

        [DataMember]
        public decimal ZURCST { get; set; }

        [DataMember]
        public string ZRVANA { get; set; }

        [DataMember]
        public List<ZUSRDto> listZUSR { get; set; }

        [DataMember]
        public ZUSRDto objZUSR { get; set; }

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

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public string SqlFilter { get; set; }

        [DataMember]
        public string SqlSort { get; set; }
        #endregion
    }

    public class ZUG2MappingDto : Mapper<ZUG2Dto>
    {
        protected override ZUG2Dto PopulateItem(IDataRecord dr)
        {
            ZUG2Dto dto = new ZUG2Dto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
