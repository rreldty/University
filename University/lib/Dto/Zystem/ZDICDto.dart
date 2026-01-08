
class ZDICDto
{
  String ZDCONO = "";
  String ZDBRNO = "";
  String ZDLGNO = "";
  String ZDDITY = "";
  String ZDFIEL = "";
  String ZDLABL = "";
  String ZDREMA = "";
  String ZDSYST = "";
  String ZDSTAT = "";
  double ZDRCST = 0;
  double ZDCRDT = 0;
  double ZDCRTM = 0;
  String ZDCRUS = "";
  double ZDCHDT = 0;
  double ZDCHTM = 0;
  String ZDCHUS = "";

  ZDICDto({
    this.ZDCONO = "",
    this.ZDBRNO = "",
    this.ZDLGNO = "",
    this.ZDDITY = "",
    this.ZDFIEL = "",
    this.ZDLABL = "",
    this.ZDREMA = "",
    this.ZDSYST = "",
    this.ZDSTAT = "",
    this.ZDRCST = 0,
    this.ZDCRDT = 0,
    this.ZDCRTM = 0,
    this.ZDCRUS = "",
    this.ZDCHDT = 0,
    this.ZDCHTM = 0,
    this.ZDCHUS = "",
  });

  factory ZDICDto.fromJson(Map<String, dynamic> json) {
    return ZDICDto(
      ZDCONO: json["ZDCONO"] ?? "",
      ZDBRNO: json["ZDBRNO"] ?? "",
      ZDLGNO: json["ZDLGNO"] ?? "",
      ZDDITY: json["ZDDITY"] ?? "",
      ZDFIEL: json["ZDFIEL"] ?? "",
      ZDLABL: json["ZDLABL"] ?? "",
      ZDREMA: json["ZDREMA"] ?? "",
      ZDSYST: json["ZDSYST"] ?? "",
      ZDSTAT: json["ZDSTAT"] ?? "",
      ZDRCST: json["ZDRCST"] ?? 0,
      ZDCRDT: json["ZDCRDT"] ?? 0,
      ZDCRTM: json["ZDCRTM"] ?? 0,
      ZDCRUS: json["ZDCRUS"] ?? "",
      ZDCHDT: json["ZDCHDT"] ?? 0,
      ZDCHTM: json["ZDCHTM"] ?? 0,
      ZDCHUS: json["ZDCHUS"] ?? "",
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZDCONO"] = ZDCONO;
    map["ZDBRNO"] = ZDBRNO;
    map["ZDLGNO"] = ZDLGNO;
    map["ZDDITY"] = ZDDITY;
    map["ZDFIEL"] = ZDFIEL;
    map["ZDLABL"] = ZDLABL;
    map["ZDREMA"] = ZDREMA;
    map["ZDSYST"] = ZDSYST;
    map["ZDSTAT"] = ZDSTAT;
    map["ZDRCST"] = ZDRCST;
    map["ZDCRDT"] = ZDCRDT;
    map["ZDCRTM"] = ZDCRTM;
    map["ZDCRUS"] = ZDCRUS;
    map["ZDCHDT"] = ZDCHDT;
    map["ZDCHTM"] = ZDCHTM;
    map["ZDCHUS"] = ZDCHUS;
    return map;
  }
}
