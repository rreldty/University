
class ZUG2Dto
{
	String ZHCONO = "";
	String ZHBRNO = "";
	String ZHUGNO = "";
	String ZHUSNO = "";
	String ZHSYST = "";
	String ZHSTAT = "";
	double ZHRCST = 0;
	double ZHCRDT = 0;
	double ZHCRTM = 0;
	String ZHCRUS = "";
	double ZHCHDT = 0;
	double ZHCHTM = 0;
	String ZHCHUS = "";
	String ZUUSNA = "";
	String ZUNICK = "";

	int PageNumber = 0;
	int PageSize = 0;
	int TotalPage = 0;
	int TotalRecord = 0;
	bool IsSelected = false;
	String SqlFilter = "";
	String SqlSort = "";

	ZUG2Dto({
		this.ZHCONO = "",
		this.ZHBRNO = "",
		this.ZHUGNO = "",
		this.ZHUSNO = "",
		this.ZHSYST = "",
		this.ZHSTAT = "",
		this.ZHRCST = 0,
		this.ZHCRDT = 0,
		this.ZHCRTM = 0,
		this.ZHCRUS = "",
		this.ZHCHDT = 0,
		this.ZHCHTM = 0,
		this.ZHCHUS = "",
		this.ZUUSNA = "",
		this.ZUNICK = "",

		this.PageNumber = 0,
		this.PageSize = 0,
		this.TotalPage = 0,
		this.TotalRecord = 0,
		this.IsSelected = false,
		this.SqlFilter = "",
		this.SqlSort = "",
	});

	factory ZUG2Dto.fromJson(Map<String, dynamic> json) {
		return ZUG2Dto(
			ZHCONO: json["ZHCONO"] ?? "",
			ZHBRNO: json["ZHBRNO"] ?? "",
			ZHUGNO: json["ZHUGNO"] ?? "",
			ZHUSNO: json["ZHUSNO"] ?? "",
			ZHSYST: json["ZHSYST"] ?? "",
			ZHSTAT: json["ZHSTAT"] ?? "",
			ZHRCST: json["ZHRCST"] ?? 0,
			ZHCRDT: json["ZHCRDT"] ?? 0,
			ZHCRTM: json["ZHCRTM"] ?? 0,
			ZHCRUS: json["ZHCRUS"] ?? "",
			ZHCHDT: json["ZHCHDT"] ?? 0,
			ZHCHTM: json["ZHCHTM"] ?? 0,
			ZHCHUS: json["ZHCHUS"] ?? "",
			ZUUSNA: json["ZUUSNA"] ?? "",
			ZUNICK: json["ZUNICK"] ?? "",

			PageNumber: json["PageNumber"] ?? 0,
			PageSize: json["PageSize"] ?? 0,
			TotalPage: json["TotalPage"] ?? 0,
			TotalRecord: json["TotalRecord"] ?? 0,
			IsSelected: json["IsSelected"] ?? false,
			SqlFilter: json["SqlFilter"] ?? "",
			SqlSort: json["SqlSort"] ?? "",
		);
	}

	Map toMap()
	{
		var map = <String, dynamic>{};
		map["ZHCONO"] = ZHCONO;
		map["ZHBRNO"] = ZHBRNO;
		map["ZHUGNO"] = ZHUGNO;
		map["ZHUSNO"] = ZHUSNO;
		map["ZHSYST"] = ZHSYST;
		map["ZHSTAT"] = ZHSTAT;
		map["ZHRCST"] = ZHRCST;
		map["ZHCRDT"] = ZHCRDT;
		map["ZHCRTM"] = ZHCRTM;
		map["ZHCRUS"] = ZHCRUS;
		map["ZHCHDT"] = ZHCHDT;
		map["ZHCHTM"] = ZHCHTM;
		map["ZHCHUS"] = ZHCHUS;
		map["ZUUSNA"] = ZUUSNA;
		map["ZUNICK"] = ZUNICK;

		map["PageNumber"] = PageNumber;
		map["PageSize"] = PageSize;
		map["TotalPage"] = TotalPage;
		map["TotalRecord"] = TotalRecord;
		map["IsSelected"] = IsSelected;
		map["SqlFilter"] = SqlFilter;
		map["SqlSort"] = SqlSort;
		return map;
	}
}
