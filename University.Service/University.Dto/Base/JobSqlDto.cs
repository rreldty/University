using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace University.Dto.Base
{
    [DataContract]
    public class JobSqlDto
    {
        [DataMember]
        public string JobName { get; set; }
        [DataMember]
        public string JobDescription { get; set; }
        [DataMember]
        public string RunStatus { get; set; }
        [DataMember]
        public string StartRunningDate { get; set; }
        [DataMember]
        public string LastRunningDate { get; set; }
        [DataMember]
        public string NextRunningDate { get; set; }
    }

    public class JobSqlMappingDto : Mapper<JobSqlDto>
    {
        protected override JobSqlDto PopulateItem(IDataRecord dr)
        {
            JobSqlDto dto = new JobSqlDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
