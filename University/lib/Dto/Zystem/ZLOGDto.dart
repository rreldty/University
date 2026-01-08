class ZLOGDto {
  //General Properties
  String ZLCONO = "";
  String ZLBRNO = "";
  String ZLUSNO = "";
  double ZLLGDT = 0;
  double ZLLGTM = 0;
  String ZLLGTY = "";
  String ZLLGIP = "";
  String ZLREMA = "";
  String ZLSYST = "";
  String ZLSTAT = "";
  double ZLRCST = 0;
  double ZLCRDT = 0;
  double ZLCRTM = 0;
  String ZLCRUS = "";
  double ZLCHDT = 0;
  double ZLCHTM = 0;
  String ZLCHUS = "";
  String SqlFilter = "";
  String SqlSort = "";

  //Custom Properties
  double ZLLGDTFr = 0; // Brief Date From
  double ZLLGDTTo = 0; // Brief Date To

  String ZHUGNO = "";
  String ZUUSNA = "";
  String ZRVANA = "";
  String RCST = "";
  String Summary = "";
  double Sum = 0;
  List<ZLOGDto>? listZLOG;
  bool IsSelected = false;
  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;

  ZLOGDto({
    this.ZLCONO = "",
    this.ZLBRNO = "",
    this.ZLUSNO = "",
    this.ZLLGDT = 0,
    this.ZLLGTM = 0,
    this.ZLLGTY = "",
    this.ZLLGIP = "",
    this.ZLREMA = "",
    this.ZLSYST = "",
    this.ZLSTAT = "",
    this.ZLRCST = 0,
    this.ZLCRDT = 0,
    this.ZLCRTM = 0,
    this.ZLCRUS = "",
    this.ZLCHDT = 0,
    this.ZLCHTM = 0,
    this.ZLCHUS = "",

    this.SqlFilter = "",
    this.SqlSort = "",
    this.ZLLGDTFr = 0,
    this.ZLLGDTTo = 0,

    this.ZUUSNA = "",
    this.ZHUGNO = "",
    this.ZRVANA = "",
    this.RCST = "",
    this.Summary = "",
    this.Sum = 0,
    this.listZLOG,
    this.IsSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
  });

  factory ZLOGDto.fromJson(Map<String, dynamic> json) {
    return ZLOGDto(
      ZLCONO: json["ZLCONO"] ?? "",
      ZLBRNO: json["ZLBRNO"] ?? "",
      ZLUSNO: json["ZLUSNO"] ?? "",
      ZLLGDT: json["ZLLGDT"] ?? 0,
      ZLLGTM: json["ZLLGTM"] ?? 0,
      ZLLGTY: json["ZLLGTY"] ?? "",
      ZLLGIP: json["ZLLGIP"] ?? "",
      ZLREMA: json["ZLREMA"] ?? "",
      ZLSYST: json["ZLSYST"] ?? "",
      ZLSTAT: json["ZLSTAT"] ?? "",
      ZLRCST: json["ZLRCST"] ?? 0,
      ZLCRDT: json["ZLCRDT"] ?? 0,
      ZLCRTM: json["ZLCRTM"] ?? 0,
      ZLCRUS: json["ZLCRUS"] ?? "",
      ZLCHDT: json["ZLCHDT"] ?? 0,
      ZLCHTM: json["ZLCHTM"] ?? 0,
      ZLCHUS: json["ZLCHUS"] ?? "",


      SqlFilter: json["SqlFilter"] ?? "",
      SqlSort: json["SqlSort"] ?? "",
      ZLLGDTFr: json["ZLLGDTFr"] ?? 0,
      ZLLGDTTo: json["ZLLGDTTo"] ?? 0,

      ZUUSNA: json ["ZUUSNA"] ?? "",
      ZHUGNO: json ["ZHUGNO"] ?? "",
      ZRVANA: json["ZRVANA"] ?? "",
      RCST: json["RCST"] ?? "",
      Summary: json["Summary"] ?? "",
      Sum: json["Sum"] ?? 0,
      listZLOG: json["listZLOG"] != null
          ? (json["listZLOG"] as List).map((e) => ZLOGDto.fromJson(e)).toList()
          : null,
      IsSelected: json["IsSelected"] ?? false,
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
    );
  }


  Map toMap() {
    var map = <String, dynamic>{};
    map["ZLCONO"] = ZLCONO ?? "";
    map["ZLBRNO"] = ZLBRNO ?? "";
    map["ZLUSNO"] = ZLUSNO ?? "";
    map["ZLLGDT"] = ZLLGDT ?? 0;
    map["ZLLGTM"] = ZLLGTM ?? 0;
    map["ZLLGTY"] = ZLLGTY ?? "";
    map["ZLLGIP"] = ZLLGIP ?? "";
    map["ZLREMA"] = ZLREMA ?? "";
    map["ZLSYST"] = ZLSYST ?? "";
    map["ZLSTAT"] = ZLSTAT ?? "";
    map["ZLRCST"] = ZLRCST ?? 0;
    map["ZLCRDT"] = ZLCRDT ?? 0;
    map["ZLCRTM"] = ZLCRTM ?? 0;
    map["ZLCRUS"] = ZLCRUS ?? "";
    map["ZLCHDT"] = ZLCHDT ?? 0;
    map["ZLCHTM"] = ZLCHTM ?? 0;
    map["ZLCHUS"] = ZLCHUS ?? "";
    map["SqlFilter"] = SqlFilter;
    map["SqlSort"] = SqlSort;

    map["ZLLGDTFr"] = ZLLGDTFr;
    map["ZLLGDTTo"] = ZLLGDTTo;

    map["ZUUSNA"] = ZUUSNA;
    map["ZHUGNO"] = ZHUGNO;
    map["ZRVANA"] = ZRVANA;
    map["RCST"] = RCST;
    map["Summary"] = Summary;
    map["Sum"] = Sum;
    map["listZLOG"] =
    listZLOG != null ? listZLOG?.map((e) => e.toMap()).toList() : null;
    map["IsSelected"] = IsSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;

    return map;
  }
}