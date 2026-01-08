
class ZUSRDto
{
  //Properties
  String ZUCONO = "";
  String ZUBRNO = "";
  String ZUUSNO = "";
  String ZUUSNA = "";
  String ZUNICK = "";
  String ZUEMAD = "";
  String ZUPSWD = "";
  String ZUHASH = "";
  String ZUUSTY = "";
  String ZUMOBN = "";
  String ZUFMKY = "";
  String ZUACNO = "";
  String ZUVENO = "";
  String ZUREMA = "";
  String ZUSYST = "";
  String ZUSTAT = "";
  double ZURCST = 0;
  double ZUCRDT = 0;
  double ZUCRTM = 0;
  String ZUCRUS = "";
  double ZUCHDT = 0;
  double ZUCHTM = 0;
  String ZUCHUS = "";

  //Custom Properties
  String ZSACNO = "";
  String ZRVANA = "";
  String G1ACNA = "";

  String ZCCONA = "";
  String ZBBRNA = "";
  String ZHUGNO = "";
  String NewPassword = "";

  bool IsReset = false;
  bool IsSelected = false;
  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;
  List<ZUSRDto>? listZUSR;

  ZUSRDto({
    this.ZUCONO = "",
    this.ZUBRNO = "",
    this.ZUUSNO = "",
    this.ZUUSNA = "",
    this.ZUNICK = "",
    this.ZUEMAD = "",
    this.ZUPSWD = "",
    this.ZUHASH = "",
    this.ZUUSTY = "",
    this.ZUMOBN = "",
    this.ZUFMKY = "",
    this.ZUACNO = "",
    this.ZUVENO = "",
    this.ZUREMA = "",
    this.ZUSYST = "",
    this.ZUSTAT = "",
    this.ZURCST = 0,
    this.ZUCRDT = 0,
    this.ZUCRTM = 0,
    this.ZUCRUS = "",
    this.ZUCHDT = 0,
    this.ZUCHTM = 0,
    this.ZUCHUS = "",
    this.ZCCONA = "",
    this.ZBBRNA = "",
    this.ZHUGNO = "",

    this.ZSACNO = "",
    this.ZRVANA = "",
    this.G1ACNA = "",
    this.NewPassword = "",

    this.IsReset = false,
    this.IsSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    this.listZUSR,
  });

  factory ZUSRDto.fromJson(Map<String, dynamic> json) {
    return ZUSRDto(
      ZUCONO: json["ZUCONO"] ?? "",
      ZUBRNO: json["ZUBRNO"] ?? "",
      ZUUSNO: json["ZUUSNO"] ?? "",
      ZUUSNA: json["ZUUSNA"] ?? "",
      ZUNICK: json["ZUNICK"] ?? "",
      ZUEMAD: json["ZUEMAD"] ?? "",
      ZUPSWD: json["ZUPSWD"] ?? "",
      ZUHASH: json["ZUHASH"] ?? "",
      ZUUSTY: json["ZUUSTY"] ?? "",
      ZUMOBN: json["ZUMOBN"] ?? "",
      ZUFMKY: json["ZUFMKY"] ?? "",
      ZUREMA: json["ZUREMA"] ?? "",
      ZUSYST: json["ZUSYST"] ?? "",
      ZUSTAT: json["ZUSTAT"] ?? "",
      ZURCST: json["ZURCST"] ?? 0,
      ZUCRDT: json["ZUCRDT"] ?? 0,
      ZUCRTM: json["ZUCRTM"] ?? 0,
      ZUCRUS: json["ZUCRUS"] ?? "",
      ZUCHDT: json["ZUCHDT"] ?? 0,
      ZUCHTM: json["ZUCHTM"] ?? 0,
      ZUCHUS: json["ZUCHUS"] ?? "",
      ZUACNO: json["ZUACNO"] ?? "",
      ZUVENO: json["ZUVENO"] ?? "",
      ZCCONA: json["ZCCONA"] ?? "",
      ZBBRNA: json["ZBBRNA"] ?? "",
      ZHUGNO: json["ZHUGNO"] ?? "",

      ZSACNO: json["ZSACNO"] ?? "",
      ZRVANA: json["ZRVANA"] ?? "",
      G1ACNA: json["G1ACNA"] ?? "",
      NewPassword: json["NewPassword"] ?? "",

      IsReset: json["IsReset"] ?? false,
      IsSelected: json["IsSelected"] ?? false,
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
      listZUSR:json["listZUSR"] != null ? (json["listZUSR"] as List).map((e) => ZUSRDto.fromJson(e)).toList() : null,

    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZUCONO"] = ZUCONO;
    map["ZUBRNO"] = ZUBRNO;
    map["ZUUSNO"] = ZUUSNO;
    map["ZUUSNA"] = ZUUSNA;
    map["ZUNICK"] = ZUNICK;
    map["ZUEMAD"] = ZUEMAD;
    map["ZUPSWD"] = ZUPSWD;
    map["ZUHASH"] = ZUHASH;
    map["ZUUSTY"] = ZUUSTY;
    map["ZUMOBN"] = ZUMOBN;
    map["ZUFMKY"] = ZUFMKY;
    map["ZUACNO"] = ZUACNO;
    map["ZUVENO"] = ZUVENO;
    map["ZUREMA"] = ZUREMA;
    map["ZUSYST"] = ZUSYST;
    map["ZUSTAT"] = ZUSTAT;
    map["ZURCST"] = ZURCST;
    map["ZUCRDT"] = ZUCRDT;
    map["ZUCRTM"] = ZUCRTM;
    map["ZUCRUS"] = ZUCRUS;
    map["ZUCHDT"] = ZUCHDT;
    map["ZUCHTM"] = ZUCHTM;
    map["ZUCHUS"] = ZUCHUS;
    map["ZCCONA"] = ZCCONA;
    map["ZBBRNA"] = ZBBRNA;
    map["ZHUGNO"] = ZHUGNO;

    map["ZSACNO"] = ZSACNO;
    map["ZRVANA"] = ZRVANA;
    map["G1ACNA"] = G1ACNA;
    map["NewPassword"] = NewPassword;

    map["IsReset"] = IsReset;
    map["IsSelected"] = IsSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
     map["listZUSR"] = listZUSR != null ? listZUSR?.map((e) => e.toMap()).toList() : null;

    return map;
  }
}
