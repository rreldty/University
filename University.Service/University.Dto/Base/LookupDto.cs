using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace University.Dto.Base
{
    [DataContract]
    public class LookupDto
    {
        [DataMember]
        public string CODE { get; set; }
        [DataMember]
        public string DESC { get; set; }

        [DataMember]
        public string Entity { get; set; }
        [DataMember]
        public int TotalPage { get; set; }
        [DataMember]
        public int TotalRecord { get; set; }
        [DataMember]
        public List<LookupHeaderDto> Headers { get; set; }
        [DataMember]
        public DataTable Rows { get; set; }
        [DataMember]
        public string WindowSize { get; set; }
    }

    public class LookUpMappingDto : Mapper<LookupDto>
    {
        protected override LookupDto PopulateItem(IDataRecord dr)
        {
            LookupDto dto = new LookupDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
