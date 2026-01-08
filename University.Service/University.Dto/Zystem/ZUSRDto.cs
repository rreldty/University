using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;
using University.Dto;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZUSRDto
    {
        #region Custom Property

        [DataMember] 
        public List<ZUSRDto> listZUSR { get; set; }

        [DataMember]
        public string NewPassword { get; set; }

        [DataMember]
		public string ZSACNO { get; set; }

		[DataMember]
		public string G1ACNA { get; set; }

		[DataMember]
        public bool isReset { get; set; }

        [DataMember]
        public bool IsExpire { get; set; }

        [DataMember]
        public bool IsSuperAdmin { get; set; }

        [DataMember]
        public string GAPRNA { get; set; }

        [DataMember]
        public string ZCCONA { get; set; }

        [DataMember]
        public string ZCCOTY { get; set; }

        [DataMember]
        public string ZCBSCY { get; set; }

        [DataMember]
        public string ZCOPTY { get; set; }

        [DataMember]
        public string ZBBRNA { get; set; }

        [DataMember]
        public string ZBBRTY { get; set; }

        [DataMember]
        public string ZRVANA { get; set; }

        [DataMember]
        public string ZLLGIP { get; set; }

        [DataMember]
        public string ZHUGNO { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public decimal LoginDate { get; set; }

        [DataMember]
        public List<ZBUMDto> listZBUM { get; set; }

        [DataMember]
        public ZBUMDto objZBUM { get; set; }

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
        [DataMember] public string ZUCONO { get; set; }
        [DataMember] public string ZUBRNO { get; set; }
        [DataMember] public string ZUUSNO { get; set; }
        [DataMember] public string ZUUSNA { get; set; }
        [DataMember] public string ZUNICK { get; set; }
        [DataMember] public string ZUEMAD { get; set; }
        [DataMember] public string ZUPSWD { get; set; }
        [DataMember] public string ZUHASH { get; set; }
        [DataMember] public string ZUUSTY { get; set; }
        [DataMember] public string ZUMOBN { get; set; }
        [DataMember] public string ZUFMKY { get; set; }
        [DataMember] public string ZUACNO { get; set; }
        [DataMember] public string ZUVENO { get; set; }
        [DataMember] public decimal ZUMBFL { get; set; }
        [DataMember] public string ZUREMA { get; set; }
        [DataMember] public string ZUSYST { get; set; }
        [DataMember] public string ZUSTAT { get; set; }
        [DataMember] public decimal ZURCST { get; set; }
        [DataMember] public decimal ZUCRDT { get; set; }
        [DataMember] public decimal ZUCRTM { get; set; }
        [DataMember] public string ZUCRUS { get; set; }
        [DataMember] public decimal ZUCHDT { get; set; }
        [DataMember] public decimal ZUCHTM { get; set; }
        [DataMember] public string ZUCHUS { get; set; }
        #endregion
    }

    public class ZUSRMappingDto : Mapper<ZUSRDto>
    {
        protected override ZUSRDto PopulateItem(IDataRecord dr)
        {
            ZUSRDto dto = new ZUSRDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}