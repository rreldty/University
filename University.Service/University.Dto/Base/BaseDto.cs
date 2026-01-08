using System.Data;
using System.Runtime.Serialization;

namespace University.Dto.Base
{
    [DataContract]
    public class BaseDto
    {
        [DataMember]
        public string DTTM { get; set; }

        [DataMember]
        public string REMA { get; set; }
    }

    public class BaseMappingDto : Mapper<BaseDto>
    {
        protected override BaseDto PopulateItem(IDataRecord dr)
        {
            BaseDto dto = new BaseDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
