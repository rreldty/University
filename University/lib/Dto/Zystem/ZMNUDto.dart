
class ZMNUDto
{
  String ZMCONO = "";
  String ZMBRNO = "";
  String ZMAPNO = "";
  String ZMMENO = "";
  String ZMMENA = "";
  String ZMMETY = "";
  String ZMMEPA = "";
  String ZMMESQ = "";
  String ZMPGNO = "";
  String ZMPARM = "";
  String ZMIURL = "";
  String ZMREMA = "";
  String ZMSYST = "";
  String ZMSTAT = "";
  double ZMRCST = 0;
  double ZMCRDT = 0;
  double ZMCRTM = 0;
  String ZMCRUS = "";
  double ZMCHDT = 0;
  double ZMCHTM = 0;
  String ZMCHUS = "";
  String ZTUGNO = "";
  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;
  bool IsSelected = false;
  String SqlFilter = "";
  String SqlSort = "";

  ZMNUDto({
    this.ZMCONO = "",
    this.ZMBRNO = "",
    this.ZMAPNO = "",
    this.ZMMENO = "",
    this.ZMMENA = "",
    this.ZMMETY = "",
    this.ZMMEPA = "",
    this.ZMMESQ = "",
    this.ZMPGNO = "",
    this.ZMPARM = "",
    this.ZMIURL = "",
    this.ZMREMA = "",
    this.ZMSYST = "",
    this.ZMSTAT = "",
    this.ZMRCST = 0,
    this.ZMCRDT = 0,
    this.ZMCRTM = 0,
    this.ZMCRUS = "",
    this.ZMCHDT = 0,
    this.ZMCHTM = 0,
    this.ZMCHUS = "",
    this.ZTUGNO = "",
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    this.IsSelected = false,
    this.SqlFilter = "",
    this.SqlSort = "",
  });

  factory ZMNUDto.fromJson(Map<String, dynamic> json) {
    return ZMNUDto(
      ZMCONO: json["ZMCONO"] ?? "",
      ZMBRNO: json["ZMBRNO"] ?? "",
      ZMAPNO: json["ZMAPNO"] ?? "",
      ZMMENO: json["ZMMENO"] ?? "",
      ZMMENA: json["ZMMENA"] ?? "",
      ZMMETY: json["ZMMETY"] ?? "",
      ZMMEPA: json["ZMMEPA"] ?? "",
      ZMMESQ: json["ZMMESQ"] ?? "",
      ZMPGNO: json["ZMPGNO"] ?? "",
      ZMPARM: json["ZMPARM"] ?? "",
      ZMIURL: json["ZMIURL"] ?? "",
      ZMREMA: json["ZMREMA"] ?? "",
      ZMSYST: json["ZMSYST"] ?? "",
      ZMSTAT: json["ZMSTAT"] ?? "",
      ZMRCST: json["ZMRCST"] ?? 0,
      ZMCRDT: json["ZMCRDT"] ?? 0,
      ZMCRTM: json["ZMCRTM"] ?? 0,
      ZMCRUS: json["ZMCRUS"] ?? "",
      ZMCHDT: json["ZMCHDT"] ?? 0,
      ZMCHTM: json["ZMCHTM"] ?? 0,
      ZMCHUS: json["ZMCHUS"] ?? "",
      ZTUGNO: json["ZTUGNO"] ?? "",
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
    map["ZMCONO"] = ZMCONO;
    map["ZMBRNO"] = ZMBRNO;
    map["ZMAPNO"] = ZMAPNO;
    map["ZMMENO"] = ZMMENO;
    map["ZMMENA"] = ZMMENA;
    map["ZMMETY"] = ZMMETY;
    map["ZMMEPA"] = ZMMEPA;
    map["ZMMESQ"] = ZMMESQ;
    map["ZMPGNO"] = ZMPGNO;
    map["ZMPARM"] = ZMPARM;
    map["ZMIURL"] = ZMIURL;
    map["ZMREMA"] = ZMREMA;
    map["ZMSYST"] = ZMSYST;
    map["ZMSTAT"] = ZMSTAT;
    map["ZMRCST"] = ZMRCST;
    map["ZMCRDT"] = ZMCRDT;
    map["ZMCRTM"] = ZMCRTM;
    map["ZMCRUS"] = ZMCRUS;
    map["ZMCHDT"] = ZMCHDT;
    map["ZMCHTM"] = ZMCHTM;
    map["ZMCHUS"] = ZMCHUS;
    map["ZTUGNO"] = ZTUGNO;
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
