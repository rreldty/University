class ZAUMDto
{
  //Properties

  String ZSCONO = "";
  String ZSBRNO = "";
  String ZSUSNO = "";
  String ZSACNO = "";
  String ZSREMA = "";
  String ZSSYST = "";
  String ZSSTAT = "";
  double ZSRCST = 0;
  double ZSCRDT = 0;
  double ZSCRTM = 0;
  String ZSCRUS = "";
  double ZSCHDT = 0;
  double ZSCHTM = 0;
  String ZSCHUS = "";

  //Custom Properties

  String G1ACNA = "";
  bool IsSelected = false;
  List<ZAUMDto>? listZAUM;
  // String ZRVANA = "";
  String RCST = "";
  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;


  ZAUMDto({
    this.ZSCONO = "",
    this.ZSBRNO = "",
    this.ZSUSNO = "",
    this.ZSACNO = "",
    this.ZSREMA = "",
    this.ZSSYST = "",
    this.ZSSTAT = "",
    this.ZSRCST = 0,
    this.ZSCRDT = 0,
    this.ZSCRTM = 0,
    this.ZSCRUS = "",
    this.ZSCHDT = 0,
    this.ZSCHTM = 0,
    this.ZSCHUS = "",
    this.G1ACNA = "",
    this.IsSelected = false,
    // this.ZRVANA = "",
    this.listZAUM,
    this.RCST = "",
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
  });

  factory ZAUMDto.fromJson(Map<String, dynamic> json) {
    return ZAUMDto(
      ZSCONO: json["ZSCONO"] ?? "",
      ZSBRNO: json["ZSBRNO"] ?? "",
      ZSUSNO: json["ZSUSNO"] ?? "",
      ZSACNO: json["ZSACNO"] ?? "",
      ZSREMA: json["ZSREMA"] ?? "",
      ZSSYST: json["ZSSYST"] ?? "",
      ZSSTAT: json["ZSSTAT"] ?? "",
      ZSRCST: json["ZSRCST"] ?? 0,
      ZSCRDT: json["ZSRCST"] ?? 0,
      ZSCRTM: json["ZSRCST"] ?? 0,
      ZSCRUS: json["ZSCRUS"] ?? "",
      ZSCHDT: json["ZSRCST"] ?? 0,
      ZSCHTM: json["ZSRCST"] ?? 0,
      ZSCHUS: json["ZSCHUS"] ?? "",
      G1ACNA: json["G1ACNA"] ?? "",
      // ZRVANA: json["ZRVANA"] ?? "",
      IsSelected: json["IsSelected"] ?? false,
      listZAUM:json["listZAUM"] != null ? (json["listZAUM"] as List).map((e) => ZAUMDto.fromJson(e)).toList() : null,
      RCST: json["RCST"] ?? "",
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZSCONO"] = ZSCONO;
    map["ZSBRNO"] = ZSBRNO;
    map["ZSUSNO"] = ZSUSNO;
    map["ZSACNO"] = ZSACNO;
    map["ZSREMA"] = ZSREMA;
    map["ZSSYST"] = ZSSYST;
    map["ZSSTAT"] = ZSSTAT;
    map["ZSRCST"] = ZSRCST;
    map["ZSCRDT"] = ZSCRDT;
    map["ZSCRTM"] = ZSCRTM;
    map["ZSCRUS"] = ZSCRUS;
    map["ZSCHDT"] = ZSCHDT;
    map["ZSCHTM"] = ZSCHTM;
    map["ZSCHUS"] = ZSCHUS;
    map["G1ACNA"] = G1ACNA;
    // map["ZRVANA"] = ZRVANA;
    map["RCST"] = RCST;
    map["listZAUM"] = listZAUM != null ? listZAUM?.map((e) => e.toMap()).toList() : null;
    map["IsSelected"] = IsSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    return map;
  }
}

