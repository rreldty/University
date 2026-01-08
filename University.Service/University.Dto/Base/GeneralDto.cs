using System.Data;
using System.Runtime.Serialization;
namespace University.Dto.Base
{
    [DataContract]
    public class GeneralDto
    {
        [DataMember]
        public string DATA { get; set; }
    }

    public class GeneralMappingDto : Mapper<GeneralDto>
    {
        protected override GeneralDto PopulateItem(IDataRecord dr)
        {
            GeneralDto dto = new GeneralDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
