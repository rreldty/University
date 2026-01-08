using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace University.Dto.Base
{
    [DataContract]
    public class DDLDto
    {
        [DataMember]
        public string CODE { get; set; }
        [DataMember]
        public string DSCR { get; set; }
    }

    public class DDLMappingDto : Mapper<DDLDto>
    {
        protected override DDLDto PopulateItem(IDataRecord dr)
        {
            DDLDto dto = new DDLDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
