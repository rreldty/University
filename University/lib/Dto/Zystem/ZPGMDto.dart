
class ZPGMDto
{
  String ZPCONO = "";
  String ZPBRNO = "";
  String ZPAPNO = "";
  String ZPPGNO = "";
  String ZPPGNA = "";
  String ZPPURL = "";
  String ZPREMA = "";
  String ZPSYST = "";
  String ZPSTAT = "";
  double ZPRCST = 0;
  double ZPCRDT = 0;
  double ZPCRTM = 0;
  String ZPCRUS = "";
  double ZPCHDT = 0;
  double ZPCHTM = 0;
  String ZPCHUS = "";

  ZPGMDto({
    this.ZPCONO = "",
    this.ZPBRNO = "",
    this.ZPAPNO = "",
    this.ZPPGNO = "",
    this.ZPPGNA = "",
    this.ZPPURL = "",
    this.ZPREMA = "",
    this.ZPSYST = "",
    this.ZPSTAT = "",
    this.ZPRCST = 0,
    this.ZPCRDT = 0,
    this.ZPCRTM = 0,
    this.ZPCRUS = "",
    this.ZPCHDT = 0,
    this.ZPCHTM = 0,
    this.ZPCHUS = "",
  });

  factory ZPGMDto.fromJson(Map<String, dynamic> json) {
    return ZPGMDto(
      ZPCONO: json["ZPCONO"] ?? "",
      ZPBRNO: json["ZPBRNO"] ?? "",
      ZPAPNO: json["ZPAPNO"] ?? "",
      ZPPGNO: json["ZPPGNO"] ?? "",
      ZPPGNA: json["ZPPGNA"] ?? "",
      ZPPURL: json["ZPPURL"] ?? "",
      ZPREMA: json["ZPREMA"] ?? "",
      ZPSYST: json["ZPSYST"] ?? "",
      ZPSTAT: json["ZPSTAT"] ?? "",
      ZPRCST: json["ZPRCST"] ?? 0,
      ZPCRDT: json["ZPCRDT"] ?? 0,
      ZPCRTM: json["ZPCRTM"] ?? 0,
      ZPCRUS: json["ZPCRUS"] ?? "",
      ZPCHDT: json["ZPCHDT"] ?? 0,
      ZPCHTM: json["ZPCHTM"] ?? 0,
      ZPCHUS: json["ZPCHUS"] ?? "",
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZPCONO"] = ZPCONO;
    map["ZPBRNO"] = ZPBRNO;
    map["ZPAPNO"] = ZPAPNO;
    map["ZPPGNO"] = ZPPGNO;
    map["ZPPGNA"] = ZPPGNA;
    map["ZPPURL"] = ZPPURL;
    map["ZPREMA"] = ZPREMA;
    map["ZPSYST"] = ZPSYST;
    map["ZPSTAT"] = ZPSTAT;
    map["ZPRCST"] = ZPRCST;
    map["ZPCRDT"] = ZPCRDT;
    map["ZPCRTM"] = ZPCRTM;
    map["ZPCRUS"] = ZPCRUS;
    map["ZPCHDT"] = ZPCHDT;
    map["ZPCHTM"] = ZPCHTM;
    map["ZPCHUS"] = ZPCHUS;
    return map;
  }
}
