
class ZAPPDto
{
  String ZACONO = "";
  String ZABRNO = "";
  String ZAAPNO = "";
  String ZAAPNA = "";
  String ZAAURL = "";
  String ZAIURL = "";
  String ZAACLR = "";
  double ZAAPSQ = 0;
  String ZAREMA = "";
  String ZASYST = "";
  String ZASTAT = "";
  double ZARCST = 0;
  double ZACRDT = 0;
  double ZACRTM = 0;
  String ZACRUS = "";
  double ZACHDT = 0;
  double ZACHTM = 0;
  String ZACHUS = "";

  ZAPPDto({
    this.ZACONO = "",
    this.ZABRNO = "",
    this.ZAAPNO = "",
    this.ZAAPNA = "",
    this.ZAAURL = "",
    this.ZAIURL = "",
    this.ZAACLR = "",
    this.ZAAPSQ = 0,
    this.ZAREMA = "",
    this.ZASYST = "",
    this.ZASTAT = "",
    this.ZARCST = 0,
    this.ZACRDT = 0,
    this.ZACRTM = 0,
    this.ZACRUS = "",
    this.ZACHDT = 0,
    this.ZACHTM = 0,
    this.ZACHUS = "",
  });

  factory ZAPPDto.fromJson(Map<String, dynamic> json) {
    return ZAPPDto(
      ZACONO: json["ZACONO"] ?? "",
      ZABRNO: json["ZABRNO"] ?? "",
      ZAAPNO: json["ZAAPNO"] ?? "",
      ZAAPNA: json["ZAAPNA"] ?? "",
      ZAAURL: json["ZAAURL"] ?? "",
      ZAIURL: json["ZAIURL"] ?? "",
      ZAACLR: json["ZAACLR"] ?? "",
      ZAAPSQ: json["ZAAPSQ"] ?? 0,
      ZAREMA: json["ZAREMA"] ?? "",
      ZASYST: json["ZASYST"] ?? "",
      ZASTAT: json["ZASTAT"] ?? "",
      ZARCST: json["ZARCST"] ?? 0,
      ZACRDT: json["ZACRDT"] ?? 0,
      ZACRTM: json["ZACRTM"] ?? 0,
      ZACRUS: json["ZACRUS"] ?? "",
      ZACHDT: json["ZACHDT"] ?? 0,
      ZACHTM: json["ZACHTM"] ?? 0,
      ZACHUS: json["ZACHUS"] ?? "",
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZACONO"] = ZACONO;
    map["ZABRNO"] = ZABRNO;
    map["ZAAPNO"] = ZAAPNO;
    map["ZAAPNA"] = ZAAPNA;
    map["ZAAURL"] = ZAAURL;
    map["ZAIURL"] = ZAIURL;
    map["ZAACLR"] = ZAACLR;
    map["ZAAPSQ"] = ZAAPSQ;
    map["ZAREMA"] = ZAREMA;
    map["ZASYST"] = ZASYST;
    map["ZASTAT"] = ZASTAT;
    map["ZARCST"] = ZARCST;
    map["ZACRDT"] = ZACRDT;
    map["ZACRTM"] = ZACRTM;
    map["ZACRUS"] = ZACRUS;
    map["ZACHDT"] = ZACHDT;
    map["ZACHTM"] = ZACHTM;
    map["ZACHUS"] = ZACHUS;
    return map;
  }
}
