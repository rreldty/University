using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Training
{
    [DataContract]
    public class KRSHeaderDto
    {
        #region General Property
        [DataMember] public string nim { get; set; }
        [DataMember] public string semester { get; set; }
        [DataMember] public string kode_fakultas { get; set; }
        [DataMember] public string kode_jurusan { get; set; }
        [DataMember] public decimal total_sks { get; set; }

        #endregion

        #region Additional Property
        [DataMember] public string nama_fakultas { get; set; }
        [DataMember] public string nama_jurusan { get; set; }
        [DataMember] public string nama_mahasiswa { get; set; }

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

        [DataMember]
        public List<KRSDetailDto> Details { get; set; }

        [DataMember]
        public KRSDetailDto objLine { get; set; }

        #endregion
    }

    public class KRSHeaderMappingDto : Mapper<KRSHeaderDto>
    {
        protected override KRSHeaderDto PopulateItem(IDataRecord dr)
        {
            KRSHeaderDto dto = new KRSHeaderDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
