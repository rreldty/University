
class ZVARDto
{
  String ZRCONO = "";
  String ZRBRNO = "";
  String ZRVANO = "";
  String ZRVANA = "";
  String ZRVATY = "";
  String ZRVAVL = "";
  String ZRVASQ = "";
  String ZRREMA = "";
  String ZRSYST = "";
  String ZRSTAT = "";
  double ZRRCST = 0;
  double ZRCRDT = 0;
  double ZRCRTM = 0;
  String ZRCRUS = "";
  double ZRCHDT = 0;
  double ZRCHTM = 0;
  String ZRCHUS = "";

  ZVARDto({
    this.ZRCONO = "",
    this.ZRBRNO = "",
    this.ZRVANO = "",
    this.ZRVANA = "",
    this.ZRVATY = "",
    this.ZRVAVL = "",
    this.ZRVASQ = "",
    this.ZRREMA = "",
    this.ZRSYST = "",
    this.ZRSTAT = "",
    this.ZRRCST = 0,
    this.ZRCRDT = 0,
    this.ZRCRTM = 0,
    this.ZRCRUS = "",
    this.ZRCHDT = 0,
    this.ZRCHTM = 0,
    this.ZRCHUS = "",
  });

  factory ZVARDto.fromJson(Map<String, dynamic> json) {
    return ZVARDto(
      ZRCONO: json["ZRCONO"] ?? "",
      ZRBRNO: json["ZRBRNO"] ?? "",
      ZRVANO: json["ZRVANO"] ?? "",
      ZRVANA: json["ZRVANA"] ?? "",
      ZRVATY: json["ZRVATY"] ?? "",
      ZRVAVL: json["ZRVAVL"] ?? "",
      ZRVASQ: json["ZRVASQ"] ?? "",
      ZRREMA: json["ZRREMA"] ?? "",
      ZRSYST: json["ZRSYST"] ?? "",
      ZRSTAT: json["ZRSTAT"] ?? "",
      ZRRCST: json["ZRRCST"] ?? 0,
      ZRCRDT: json["ZRCRDT"] ?? 0,
      ZRCRTM: json["ZRCRTM"] ?? 0,
      ZRCRUS: json["ZRCRUS"] ?? "",
      ZRCHDT: json["ZRCHDT"] ?? 0,
      ZRCHTM: json["ZRCHTM"] ?? 0,
      ZRCHUS: json["ZRCHUS"] ?? "",
    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZRCONO"] = ZRCONO;
    map["ZRBRNO"] = ZRBRNO;
    map["ZRVANO"] = ZRVANO;
    map["ZRVANA"] = ZRVANA;
    map["ZRVATY"] = ZRVATY;
    map["ZRVAVL"] = ZRVAVL;
    map["ZRVASQ"] = ZRVASQ;
    map["ZRREMA"] = ZRREMA;
    map["ZRSYST"] = ZRSYST;
    map["ZRSTAT"] = ZRSTAT;
    map["ZRRCST"] = ZRRCST;
    map["ZRCRDT"] = ZRCRDT;
    map["ZRCRTM"] = ZRCRTM;
    map["ZRCRUS"] = ZRCRUS;
    map["ZRCHDT"] = ZRCHDT;
    map["ZRCHTM"] = ZRCHTM;
    map["ZRCHUS"] = ZRCHUS;
    return map;
  }
}
