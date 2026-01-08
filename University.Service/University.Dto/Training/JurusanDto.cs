using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Training
{
    [DataContract]
    public class JurusanDto
    {
        #region General Property
        [DataMember] public string kode_fakultas { get; set; }
        [DataMember] public string kode_jurusan { get; set; }
        [DataMember] public string nama_jurusan { get; set; }
        [DataMember] public decimal record_status { get; set; }

        #endregion

        #region Additional Property

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

    public class JurusanMappingDto : Mapper<JurusanDto>
    {
        protected override JurusanDto PopulateItem(IDataRecord dr)
        {
            JurusanDto dto = new JurusanDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
