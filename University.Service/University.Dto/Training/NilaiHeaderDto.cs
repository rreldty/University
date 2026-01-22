using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;
using University.Dto.Base;
using University.Dto.Training;

namespace University.Dto.Training
{
    [DataContract]
    public class NilaiHeaderDto
    {
        [DataMember] public string nim { get; set; }
        [DataMember] public string semester { get; set; }
        [DataMember] public string kode_fakultas { get; set; }
        [DataMember] public string kode_jurusan { get; set; }
        [DataMember] public double nilai { get; set; }

        [DataMember] public bool isSelected { get; set; }
        [DataMember] public int PageNumber { get; set; }
        [DataMember] public int PageSize { get; set; }
        [DataMember] public int TotalPage { get; set; }
        [DataMember] public int TotalRecord { get; set; }

        [DataMember] public List<NilaiDetailDto> Details { get; set; }
        [DataMember] public NilaiDetailDto objLine { get; set; }

        public NilaiHeaderDto()
        {
            Details = new List<NilaiDetailDto>();
        }
    }

    public class NilaiHeaderMappingDto : Mapper<NilaiHeaderDto>
    {
        protected override NilaiHeaderDto PopulateItem(IDataRecord dr)
        {
            NilaiHeaderDto dto = new NilaiHeaderDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
