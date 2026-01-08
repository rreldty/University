using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace University.Dto.Base
{
    [DataContract]
    public class LookupHeaderDto
    {
        [DataMember]
        public string HeaderName { get; set; }
        [DataMember]
        public string HeaderType { get; set; }
        [DataMember]
        public string HeaderFormat { get; set; }
        [DataMember]
        public string HeaderWidth { get; set; }
    }

    public class LookupHeaderMappingDto : Mapper<LookupHeaderDto>
    {
        protected override LookupHeaderDto PopulateItem(IDataRecord dr)
        {
            LookupHeaderDto dto = new LookupHeaderDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
