using University.Dto.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace University.Dto.Base
{
    [DataContract]
    public class EntityDto
    {
        [DataMember]
        public string Entity { get; set; }

        [DataMember]
        public string Filter { get; set; }

        [DataMember]
        public string Sort { get; set; }

        [DataMember]
        public string KeyCode { get; set; }

        [DataMember]
        public string FilterMultiselect { get; set; }

        [DataMember]
        public List<string> SearchBys { get; set; }

        [DataMember]
        public List<string> Operators { get; set; }

        [DataMember]
        public List<string> SearchKeys { get; set; }

        [DataMember]
        public int PageNum { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalPage { get; set; }

        [DataMember]
        public int TotalRecord { get; set; }

        [DataMember]
        public string WindowSize { get; set; }

        [DataMember]
        public string DecimalColumn { get; set; }

        [DataMember]
        public string ColumnWidth { get; set; }

        [DataMember]
        public List<ParamDto> Parameters { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FileType { get; set; }
    }

    public class EntityMappingDto : Mapper<EntityDto>
    {
        protected override EntityDto PopulateItem(IDataRecord dr)
        {
            EntityDto dto = new EntityDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
