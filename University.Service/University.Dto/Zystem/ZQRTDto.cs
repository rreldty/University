#region Summary
//''''''''''''''''''''''''''''S U M M A R Y '''''''''''''''''''''''''''''
//'File Name     : ZQRTDto.cs
//'Author        : Vinno
//'Creation Date : 6/21/2016
//'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
#endregion

#region Reference
using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;
#endregion

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZQRTDto
    {
        #region General Property
        [DataMember]
        public string ZQCONO { get; set; }

        [DataMember]
        public string ZQBRNO { get; set; }

        [DataMember]
        public string ZQQRNO { get; set; }

        [DataMember]
        public string ZQQRNA { get; set; }

        [DataMember]
        public string ZQQURY { get; set; }

        [DataMember]
        public string ZQREMA { get; set; }

        [DataMember]
        public string ZQSYST { get; set; }

        [DataMember]
        public string ZQSTAT { get; set; }

        [DataMember]
        public decimal ZQRCST { get; set; }

        [DataMember]
        public decimal ZQCRDT { get; set; }

        [DataMember]
        public decimal ZQCRTM { get; set; }

        [DataMember]
        public string ZQCRUS { get; set; }

        [DataMember]
        public decimal ZQCHDT { get; set; }

        [DataMember]
        public decimal ZQCHTM { get; set; }

        [DataMember]
        public string ZQCHUS { get; set; }

        #endregion

        #region Custom Property

        [DataMember]
        public bool IsSelected { get; set; }

        //[DataMember]
        //public List<ZQRTDto> lstZQRT { get; set; }

        //[DataMember]
        //public string Result { get; set; }

        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalPage { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }

        [DataMember]
        public string SqlFilter { get; set; }


        [DataMember]
        public string SqlSort { get; set; }

        #endregion

    }

    public class ZQRTDtoMap : Mapper<ZQRTDto>
    {
        protected override ZQRTDto PopulateItem(IDataRecord dr)
        {
            ZQRTDto dto = new ZQRTDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
