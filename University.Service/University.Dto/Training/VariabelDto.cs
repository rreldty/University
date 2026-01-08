using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Training
{
    [DataContract]
    public class VariabelDto
    {
        #region General Property

        [DataMember]
        public string Kode_Variabel { get; set; }

        [DataMember]
        public string Tipe_Variabel { get; set; }

        [DataMember]
        public string Nama_Variabel { get; set; }

        [DataMember]
        public string Nilai_Variabel { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public decimal Record_Status { get; set; }

        #endregion

        #region Custom Property

        [DataMember]
        public string Kode_VariabelFr { get; set; }

        [DataMember]
        public string Kode_VariabelTo { get; set; }

        [DataMember]
        public string Nama_VariabelFr { get; set; }

        #endregion
    }


    public class VariabelMappingDto : Mapper<VariabelDto>
    {
        protected override VariabelDto PopulateItem(IDataRecord dr)
        {
            VariabelDto dto = new VariabelDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}