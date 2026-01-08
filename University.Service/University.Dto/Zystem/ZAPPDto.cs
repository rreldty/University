using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZAPPDto
    {
        #region General Property

        [DataMember]
        public string ZACONO { get; set; }

        [DataMember]
        public string ZABRNO { get; set; }

        [DataMember]
        public string ZAAPNO { get; set; }

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
        public string ZASYST { get; set; }

        [DataMember]
        public string ZASTAT { get; set; }

        [DataMember]
        public decimal ZARCST { get; set; }

        [DataMember]
        public decimal ZACRDT { get; set; }

        [DataMember]
        public decimal ZACRTM { get; set; }

        [DataMember]
        public string ZACRUS { get; set; }

        [DataMember]
        public decimal ZACHDT { get; set; }

        [DataMember]
        public decimal ZACHTM { get; set; }

        [DataMember]
        public string ZACHUS { get; set; }

        #endregion

        #region Additional Property

        [DataMember]
        public string ZHUSNO { get; set; }

        [DataMember]
        public string ZAAPNOFr { get; set; }

        [DataMember]
        public string ZAAPNOTo { get; set; }

        //ZHUSNO
        #endregion
    }

    public class ZAPPMappingDto : Mapper<ZAPPDto>
    {
        protected override ZAPPDto PopulateItem(IDataRecord dr)
        {
            ZAPPDto dto = new ZAPPDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
