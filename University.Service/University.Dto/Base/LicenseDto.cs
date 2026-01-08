using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace University.Dto.Base
{
    [DataContract]
    public class LicenseDto
    {
        [DataMember]
        public string CONO { get; set; }

        [DataMember]
        public string CONA { get; set; }

        [DataMember]
        public string BRNO { get; set; }

        [DataMember]
        public string BRNA { get; set; }

        [DataMember]
        public string PRNO { get; set; }

        [DataMember]
        public string TNSDSProductCode { get; set; }

        [DataMember]
        public string LicenseTo { get; set; }

        [DataMember]
        public string UserLicense { get; set; }

        [DataMember]
        public string LicenseEdition { get; set; }

        [DataMember]
        public string ClosingIn { get; set; }

        [DataMember]
        public decimal InstalledDate { get; set; }
    }

    public class LicenseMappingDto : Mapper<LicenseDto>
    {
        protected override LicenseDto PopulateItem(IDataRecord dr)
        {
            LicenseDto dto = new LicenseDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}
