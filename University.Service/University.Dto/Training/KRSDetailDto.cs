using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Training
{
    [DataContract]
    public class KRSDetailDto
    {
        #region General Property
        [DataMember] public string nim { get; set; }
        [DataMember] public string semester { get; set; }
        [DataMember] public int line_no { get; set; }
        [DataMember] public string kode_matakuliah { get; set; }
        [DataMember] public decimal sks { get; set; }

        #endregion

        #region Additional Property
        [DataMember] public string nama_matakuliah { get; set; }

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

    public class KRSDetailMappingDto : Mapper<KRSDetailDto>
    {
        protected override KRSDetailDto PopulateItem(IDataRecord dr)
        {
            KRSDetailDto dto = new KRSDetailDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
