using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

using University.Dto.Base;

namespace University.Dto.Zystem
{
    [DataContract]
    public class ZLOGDto
    {
        #region General Property
        [DataMember]
        public string ZLCONO { get; set; }

        [DataMember]
        public string ZLBRNO { get; set; }

        [DataMember]
        public string ZLUSNO { get; set; }

        [DataMember]
        public decimal ZLLGDT { get; set; }

        [DataMember]
        public decimal ZLLGTM { get; set; }

        [DataMember]
        public string ZLLGTY { get; set; }

        [DataMember]
        public string ZLLGIP { get; set; }

        [DataMember]
        public string ZLREMA { get; set; }

        [DataMember]
        public string ZLSYST { get; set; }

        [DataMember]
        public string ZLSTAT { get; set; }

        [DataMember]
        public decimal ZLRCST { get; set; }

        [DataMember]
        public decimal ZLCRDT { get; set; }

        [DataMember]
        public decimal ZLCRTM { get; set; }

        [DataMember]
        public string ZLCRUS { get; set; }

        [DataMember]
        public decimal ZLCHDT { get; set; }

        [DataMember]
        public decimal ZLCHTM { get; set; }

        [DataMember]
        public string ZLCHUS { get; set; }
		#endregion

		#region Custom Property

		[DataMember]
		public string ZUUSNA { get; set; }

		[DataMember]
		public decimal ZLLGDTFr { get; set; }

		[DataMember]
		public decimal ZLLGDTTo { get; set; }

		[DataMember]
		public string ZHUGNO { get; set; }

		[DataMember]
		public string ZRVANA { get; set; }

		[DataMember]
		public List<ZUSRDto> listZUSR { get; set; }

		[DataMember]
		public ZUSRDto objZUSR { get; set; }

		[DataMember]
		public string Result { get; set; }

		[DataMember]
		public int PageNumber { get; set; }

		[DataMember]
		public int PageSize { get; set; }

		[DataMember]
		public int TotalPage { get; set; }

		[DataMember]
		public int TotalRecord { get; set; }

		[DataMember]
		public bool IsSelected { get; set; }

		[DataMember]
		public string SqlFilter { get; set; }

		[DataMember]
		public string SqlSort { get; set; }
		#endregion
	}

	public class MapZLOGDto : Mapper<ZLOGDto>
    {
        protected override ZLOGDto PopulateItem(IDataRecord dr)
        {
            ZLOGDto dto = new ZLOGDto();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                MapProperty(dto, dr.GetName(i), dr[i]);
            }
            return dto;
        }
    }
}