using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZVARDto
    {
        #region General Property

        [DataMember]
        public string ZRCONO { get; set; }

        [DataMember]
        public string ZRBRNO { get; set; }

        [DataMember]
        public string ZRVANO { get; set; }

        [DataMember]
        public string ZRVANA { get; set; }

        [DataMember]
        public string ZRVATY { get; set; }

        [DataMember]
        public string ZRVAVL { get; set; }

        [DataMember]
        public string ZRVASQ { get; set; }

        [DataMember]
        public string ZRREMA { get; set; }

        [DataMember]
        public string ZRSYST { get; set; }

        [DataMember]
        public string ZRSTAT { get; set; }

        [DataMember]
        public decimal ZRRCST { get; set; }

        [DataMember]
        public decimal ZRCRDT { get; set; }

        [DataMember]
        public decimal ZRCRTM { get; set; }

        [DataMember]
        public string ZRCRUS { get; set; }

        [DataMember]
        public decimal ZRCHDT { get; set; }

        [DataMember]
        public decimal ZRCHTM { get; set; }

        [DataMember]
        public string ZRCHUS { get; set; }

        #endregion
    }

    public class ZVARDtoMap : Mapper<ZVARDto>
    {
        protected override ZVARDto PopulateItem(IDataRecord dr)
        {
            ZVARDto dto = new ZVARDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
