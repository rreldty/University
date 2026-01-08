class ZQRTDto
{
  //Properties
  String ZQCONO = "";
  String ZQBRNO = "";
  String ZQQRNO = "";
  String ZQQRNA = "";
  String ZQQURY = "";
  String ZQREMA = "";
  String ZQSYST = "";
  String ZQSTAT = "";
  double ZQRCST = 0;
  double ZQCRDT = 0;
  double ZQCRTM = 0;
  String ZQCRUS = "";
  double ZQCHDT = 0;
  double ZQCHTM = 0;
  String ZQCHUS = "";

  //Custom Properties

  bool IsSelected = false;
  List<dynamic>? lst;
  // List<ZQRTDto>? listZQRT;
  // String ZRVANA = "";
  // String RCST = "";
  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;
  String SqlFilter = "";
  String SqlSort = "";


  ZQRTDto({
    this.ZQCONO = "",
    this.ZQBRNO = "",
    this.ZQQRNO = "",
    this.ZQQRNA = "",
    this.ZQQURY = "",
    this.ZQREMA = "",
    this.ZQSYST = "",
    this.ZQSTAT = "",
    this.ZQRCST = 0,
    this.ZQCRDT = 0,
    this.ZQCRTM = 0,
    this.ZQCRUS = "",
    this.ZQCHDT = 0,
    this.ZQCHTM = 0,
    this.ZQCHUS = "",
    this.IsSelected = false,

    this.lst,
    // this.listZQRT,
    // this.RCST = "",
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    this.SqlFilter = "",
    this.SqlSort = "",
  });

  factory ZQRTDto.fromJson(Map<String, dynamic> json) {
    return ZQRTDto(
      ZQCONO: json["ZQCONO"] ?? "",
      ZQBRNO: json["ZQBRNO"] ?? "",
      ZQQRNO: json["ZQQRNO"] ?? "",
      ZQQRNA: json["ZQQRNA"] ?? "",
      ZQQURY: json["ZQQURY"] ?? "",
      ZQREMA: json["ZQREMA"] ?? "",
      ZQSYST: json["ZQSYST"] ?? "",
      ZQSTAT: json["ZQSTAT"] ?? "",
      ZQRCST: json["ZQRCST"] ?? 0,
      ZQCRDT: json["ZQCRDT"] ?? 0,
      ZQCRTM: json["ZQCRTM"] ?? 0,
      ZQCRUS: json["ZQCRUS"] ?? "",
      ZQCHDT: json["ZQCHDT"] ?? 0,
      ZQCHTM: json["ZQCHTM"] ?? 0,
      ZQCHUS: json["ZQCHUS"] ?? "",
      // ZRVANA: json["ZRVANA"] ?? "",
      IsSelected: json["IsSelected"] ?? false,
      lst:json["lst"] != null ? (json["lst"] as List).map((e) => ZQRTDto.fromJson(e)).toList() : null,
      // listZQRT:json["listZQRT"] != null ? (json["listZQRT"] as List).map((e) => ZQRTDto.fromJson(e)).toList() : null,
      // RCST: json["RCST"] ?? "",
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
      SqlFilter: json["SqlFilter"] ?? "",
      SqlSort: json["SqlSort"] ?? "",
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZQCONO"] = ZQCONO ?? "";
    map["ZQBRNO"] = ZQBRNO ?? "";
    map["ZQQRNO"] = ZQQRNO ?? "";
    map["ZQQRNA"] = ZQQRNA ?? "";
    map["ZQQURY"] = ZQQURY ?? "";
    map["ZQREMA"] = ZQREMA ?? "";
    map["ZQSYST"] = ZQSYST ?? "";
    map["ZQSTAT"] = ZQSTAT ?? "";
    map["ZQRCST"] = ZQRCST ?? 0;
    map["ZQCRDT"] = ZQCRDT ?? 0;
    map["ZQCRTM"] = ZQCRTM ?? 0;
    map["ZQCRUS"] = ZQCRUS ?? "";
    map["ZQCHDT"] = ZQCHDT ?? 0;
    map["ZQCHTM"] = ZQCHTM ?? 0;
    map["ZQCHUS"] = ZQCHUS ?? "";
    // map["ZRVANA"] = ZRVANA;
    // map["RCST"] = RCST;
    // map["listZQRT"] = listZQRT != null ? listZQRT?.map((e) => e.toMap()).toList() : null;
    map["IsSelected"] = IsSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    map["SqlFilter"] = SqlFilter;
    map["SqlSort"] = SqlSort;
    return map;
  }
}

