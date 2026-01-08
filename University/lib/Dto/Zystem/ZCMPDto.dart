
class ZCMPDto
{
  String ZCCONO = "";
  String ZCCOTY = "";
  String ZCCONA = "";
  String ZCBINA = "";
  String ZCADR1 = "";
  String ZCADR2 = "";
  String ZCADR3 = "";
  String ZCCTNO = "";
  String ZCZPNO = "";
  String ZCSAR1 = "";
  String ZCCNNO = "";
  String ZCPHN1 = "";
  String ZCFAXN = "";
  String ZCEMAD = "";
  String ZCTXNO = "";
  String ZCTXNA = "";
  String ZCTAD1 = "";
  String ZCTAD2 = "";
  String ZCTAD3 = "";
  String ZCTXCT = "";
  String ZCTXZP = "";
  String ZCNPKP = "";
  double ZCPKPD = 0;
  String ZCACNO = "";
  String ZCBAD1 = "";
  String ZCBAD2 = "";
  String ZCBAD3 = "";
  String ZCBSCY = "";
  String ZCTPNO = "";
  String ZCARHC = "";
  String ZCSOFC = "";
  String ZCTXOF = "";
  String ZCTXOP = "";
  String ZCSLPG = "";
  double ZCETDY = 0;
  double ZCPETD = 0;
  double ZCDCPT = 0;
  double ZCMCSP = 0;
  double ZCSRVD = 0;
  String ZCPFAR = "";
  String ZCCOOL = "";
  String ZCPFCO = "";
  double ZCNAWS = 0;
  double ZCPDW1 = 0;
  double ZCPDW2 = 0;
  double ZCPDW3 = 0;
  double ZCPDW4 = 0;
  double ZCPDW5 = 0;
  double ZCCOWC = 0;
  double ZCUSFG = 0;
  double ZCUSGL = 0;
  double ZCUSCC = 0;
  double ZCUSDE = 0;
  double ZCUSBU = 0;
  double ZCPQFP = 0;
  double ZCPQFR = 0;
  double ZCPPKP = 0;
  String ZCFPPT = "";
  String ZCFRPT = "";
  String ZCFPTT = "";
  double ZCNOVT = 0;
  String ZCSIVA = "";
  double ZCQOFC = 0;
  String ZCDFCR = "";
  String ZCSHNO = "";
  double ZCCAWP = 0;
  double ZCCIVP = 0;
  double ZCNSWK = 0;
  String ZCCUNO = "";
  double ZCICES = 0;
  String ZCREMA = "";
  String ZCSYST = "";
  String ZCSTAT = "";
  double ZCRCST = 0;
  double ZCCRDT = 0;
  double ZCCRTM = 0;
  String ZCCRUS = "";
  double ZCCHDT = 0;
  double ZCCHTM = 0;
  String ZCCHUS = "";


  bool IsReset = false;
  bool IsSelected = false;
  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;
  List<ZCMPDto>? listZCMP;

  ZCMPDto({
    this.ZCCONO = "",
    this.ZCCOTY = "",
    this.ZCCONA = "",
    this.ZCBINA = "",
    this.ZCADR1 = "",
    this.ZCADR2 = "",
    this.ZCADR3 = "",
    this.ZCCTNO = "",
    this.ZCZPNO = "",
    this.ZCSAR1 = "",
    this.ZCCNNO = "",
    this.ZCPHN1 = "",
    this.ZCFAXN = "",
    this.ZCEMAD = "",
    this.ZCTXNO = "",
    this.ZCTXNA = "",
    this.ZCTAD1 = "",
    this.ZCTAD2 = "",
    this.ZCTAD3 = "",
    this.ZCTXCT = "",
    this.ZCTXZP = "",
    this.ZCNPKP = "",
    this.ZCPKPD = 0,
    this.ZCACNO = "",
    this.ZCBAD1 = "",
    this.ZCBAD2 = "",
    this.ZCBAD3 = "",
    this.ZCBSCY = "",
    this.ZCTPNO = "",
    this.ZCARHC = "",
    this.ZCSOFC = "",
    this.ZCTXOF = "",
    this.ZCTXOP = "",
    this.ZCSLPG = "",
    this.ZCETDY = 0,
    this.ZCPETD = 0,
    this.ZCDCPT = 0,
    this.ZCMCSP = 0,
    this.ZCSRVD = 0,
    this.ZCPFAR = "",
    this.ZCCOOL = "",
    this.ZCPFCO = "",
    this.ZCNAWS = 0,
    this.ZCPDW1 = 0,
    this.ZCPDW2 = 0,
    this.ZCPDW3 = 0,
    this.ZCPDW4 = 0,
    this.ZCPDW5 = 0,
    this.ZCCOWC = 0,
    this.ZCUSFG = 0,
    this.ZCUSGL = 0,
    this.ZCUSCC = 0,
    this.ZCUSDE = 0,
    this.ZCUSBU = 0,
    this.ZCPQFP = 0,
    this.ZCPQFR = 0,
    this.ZCPPKP = 0,
    this.ZCFPPT = "",
    this.ZCFRPT = "",
    this.ZCFPTT = "",
    this.ZCNOVT = 0,
    this.ZCSIVA = "",
    this.ZCQOFC = 0,
    this.ZCDFCR = "",
    this.ZCSHNO = "",
    this.ZCCAWP = 0,
    this.ZCCIVP = 0,
    this.ZCNSWK = 0,
    this.ZCCUNO = "",
    this.ZCICES = 0,
    this.ZCREMA = "",
    this.ZCSYST = "",
    this.ZCSTAT = "",
    this.ZCRCST = 0,
    this.ZCCRDT = 0,
    this.ZCCRTM = 0,
    this.ZCCRUS = "",
    this.ZCCHDT = 0,
    this.ZCCHTM = 0,
    this.ZCCHUS = "",

    this.IsReset = false,
    this.IsSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
    this.listZCMP,
  });

  factory ZCMPDto.fromJson(Map<String, dynamic> json) {
    return ZCMPDto(
      ZCCONO: json["ZCCONO"] ?? "",
      ZCCOTY: json["ZCCOTY"] ?? "",
      ZCCONA: json["ZCCONA"] ?? "",
      ZCBINA: json["ZCBINA"] ?? "",
      ZCADR1: json["ZCADR1"] ?? "",
      ZCADR2: json["ZCADR2"] ?? "",
      ZCADR3: json["ZCADR3"] ?? "",
      ZCCTNO: json["ZCCTNO"] ?? "",
      ZCZPNO: json["ZCZPNO"] ?? "",
      ZCSAR1: json["ZCSAR1"] ?? "",
      ZCCNNO: json["ZCCNNO"] ?? "",
      ZCPHN1: json["ZCPHN1"] ?? "",
      ZCFAXN: json["ZCFAXN"] ?? "",
      ZCEMAD: json["ZCEMAD"] ?? "",
      ZCTXNO: json["ZCTXNO"] ?? "",
      ZCTXNA: json["ZCTXNA"] ?? "",
      ZCTAD1: json["ZCTAD1"] ?? "",
      ZCTAD2: json["ZCTAD2"] ?? "",
      ZCTAD3: json["ZCTAD3"] ?? "",
      ZCTXCT: json["ZCTXCT"] ?? "",
      ZCTXZP: json["ZCTXZP"] ?? "",
      ZCNPKP: json["ZCNPKP"] ?? "",
      ZCPKPD: json["ZCPKPD"] ?? 0,
      ZCACNO: json["ZCACNO"] ?? "",
      ZCBAD1: json["ZCBAD1"] ?? "",
      ZCBAD2: json["ZCBAD2"] ?? "",
      ZCBAD3: json["ZCBAD3"] ?? "",
      ZCBSCY: json["ZCBSCY"] ?? "",
      ZCTPNO: json["ZCTPNO"] ?? "",
      ZCARHC: json["ZCARHC"] ?? "",
      ZCSOFC: json["ZCSOFC"] ?? "",
      ZCTXOF: json["ZCTXOF"] ?? "",
      ZCTXOP: json["ZCTXOP"] ?? "",
      ZCSLPG: json["ZCSLPG"] ?? "",
      ZCETDY: json["ZCETDY"] ?? 0,
      ZCPETD: json["ZCPETD"] ?? 0,
      ZCDCPT: json["ZCDCPT"] ?? 0,
      ZCMCSP: json["ZCMCSP"] ?? 0,
      ZCSRVD: json["ZCSRVD"] ?? 0,
      ZCPFAR: json["ZCPFAR"] ?? "",
      ZCCOOL: json["ZCCOOL"] ?? "",
      ZCPFCO: json["ZCPFCO"] ?? "",
      ZCNAWS: json["ZCNAWS"] ?? 0,
      ZCPDW1: json["ZCPDW1"] ?? 0,
      ZCPDW2: json["ZCPDW2"] ?? 0,
      ZCPDW3: json["ZCPDW3"] ?? 0,
      ZCPDW4: json["ZCPDW4"] ?? 0,
      ZCPDW5: json["ZCPDW5"] ?? 0,
      ZCCOWC: json["ZCCOWC"] ?? 0,
      ZCUSFG: json["ZCUSFG"] ?? 0,
      ZCUSGL: json["ZCUSGL"] ?? 0,
      ZCUSCC: json["ZCUSCC"] ?? 0,
      ZCUSDE: json["ZCUSDE"] ?? 0,
      ZCUSBU: json["ZCUSBU"] ?? 0,
      ZCPQFP: json["ZCPQFP"] ?? 0,
      ZCPQFR: json["ZCPQFR"] ?? 0,
      ZCPPKP: json["ZCPPKP"] ?? 0,
      ZCFPPT: json["ZCFPPT"] ?? "",
      ZCFRPT: json["ZCFRPT"] ?? "",
      ZCFPTT: json["ZCFPTT"] ?? "",
      ZCNOVT: json["ZCNOVT"] ?? 0,
      ZCSIVA: json["ZCSIVA"] ?? "",
      ZCQOFC: json["ZCQOFC"] ?? 0,
      ZCDFCR: json["ZCDFCR"] ?? "",
      ZCSHNO: json["ZCSHNO"] ?? "",
      ZCCAWP: json["ZCCAWP"] ?? 0,
      ZCCIVP: json["ZCCIVP"] ?? 0,
      ZCNSWK: json["ZCNSWK"] ?? 0,
      ZCCUNO: json["ZCCUNO"] ?? "",
      ZCICES: json["ZCICES"] ?? 0,
      ZCREMA: json["ZCREMA"] ?? "",
      ZCSYST: json["ZCSYST"] ?? "",
      ZCSTAT: json["ZCSTAT"] ?? "",
      ZCRCST: json["ZCRCST"] ?? 0,
      ZCCRDT: json["ZCCRDT"] ?? 0,
      ZCCRTM: json["ZCCRTM"] ?? 0,
      ZCCRUS: json["ZCCRUS"] ?? "",
      ZCCHDT: json["ZCCHDT"] ?? 0,
      ZCCHTM: json["ZCCHTM"] ?? 0,
      ZCCHUS: json["ZCCHUS"] ?? "",

      IsReset: json["IsReset"] ?? false,
      IsSelected: json["IsSelected"] ?? false,
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
      listZCMP:json["listZCMP"] != null ? (json["listZCMP"] as List).map((e) => ZCMPDto.fromJson(e)).toList() : null,

    );
  }

  Map toMap()
  {
    var map = <String, dynamic>{};
    map["ZCCONO"] = ZCCONO;
    map["ZCCOTY"] = ZCCOTY;
    map["ZCCONA"] = ZCCONA;
    map["ZCBINA"] = ZCBINA;
    map["ZCADR1"] = ZCADR1;
    map["ZCADR2"] = ZCADR2;
    map["ZCADR3"] = ZCADR3;
    map["ZCCTNO"] = ZCCTNO;
    map["ZCZPNO"] = ZCZPNO;
    map["ZCSAR1"] = ZCSAR1;
    map["ZCCNNO"] = ZCCNNO;
    map["ZCPHN1"] = ZCPHN1;
    map["ZCFAXN"] = ZCFAXN;
    map["ZCEMAD"] = ZCEMAD;
    map["ZCTXNO"] = ZCTXNO;
    map["ZCTXNA"] = ZCTXNA;
    map["ZCTAD1"] = ZCTAD1;
    map["ZCTAD2"] = ZCTAD2;
    map["ZCTAD3"] = ZCTAD3;
    map["ZCTXCT"] = ZCTXCT;
    map["ZCTXZP"] = ZCTXZP;
    map["ZCNPKP"] = ZCNPKP;
    map["ZCPKPD"] = ZCPKPD;
    map["ZCACNO"] = ZCACNO;
    map["ZCBAD1"] = ZCBAD1;
    map["ZCBAD2"] = ZCBAD2;
    map["ZCBAD3"] = ZCBAD3;
    map["ZCBSCY"] = ZCBSCY;
    map["ZCTPNO"] = ZCTPNO;
    map["ZCARHC"] = ZCARHC;
    map["ZCSOFC"] = ZCSOFC;
    map["ZCTXOF"] = ZCTXOF;
    map["ZCTXOP"] = ZCTXOP;
    map["ZCSLPG"] = ZCSLPG;
    map["ZCETDY"] = ZCETDY;
    map["ZCPETD"] = ZCPETD;
    map["ZCDCPT"] = ZCDCPT;
    map["ZCMCSP"] = ZCMCSP;
    map["ZCSRVD"] = ZCSRVD;
    map["ZCPFAR"] = ZCPFAR;
    map["ZCCOOL"] = ZCCOOL;
    map["ZCPFCO"] = ZCPFCO;
    map["ZCNAWS"] = ZCNAWS;
    map["ZCPDW1"] = ZCPDW1;
    map["ZCPDW2"] = ZCPDW2;
    map["ZCPDW3"] = ZCPDW3;
    map["ZCPDW4"] = ZCPDW4;
    map["ZCPDW5"] = ZCPDW5;
    map["ZCCOWC"] = ZCCOWC;
    map["ZCUSFG"] = ZCUSFG;
    map["ZCUSGL"] = ZCUSGL;
    map["ZCUSCC"] = ZCUSCC;
    map["ZCUSDE"] = ZCUSDE;
    map["ZCUSBU"] = ZCUSBU;
    map["ZCPQFP"] = ZCPQFP;
    map["ZCPQFR"] = ZCPQFR;
    map["ZCPPKP"] = ZCPPKP;
    map["ZCFPPT"] = ZCFPPT;
    map["ZCFRPT"] = ZCFRPT;
    map["ZCFPTT"] = ZCFPTT;
    map["ZCNOVT"] = ZCNOVT;
    map["ZCSIVA"] = ZCSIVA;
    map["ZCQOFC"] = ZCQOFC;
    map["ZCDFCR"] = ZCDFCR;
    map["ZCSHNO"] = ZCSHNO;
    map["ZCCAWP"] = ZCCAWP;
    map["ZCCIVP"] = ZCCIVP;
    map["ZCNSWK"] = ZCNSWK;
    map["ZCCUNO"] = ZCCUNO;
    map["ZCICES"] = ZCICES;
    map["ZCREMA"] = ZCREMA;
    map["ZCSYST"] = ZCSYST;
    map["ZCSTAT"] = ZCSTAT;
    map["ZCRCST"] = ZCRCST;
    map["ZCCRDT"] = ZCCRDT;
    map["ZCCRTM"] = ZCCRTM;
    map["ZCCRUS"] = ZCCRUS;
    map["ZCCHDT"] = ZCCHDT;
    map["ZCCHTM"] = ZCCHTM;
    map["ZCCHUS"] = ZCCHUS;
    map["IsReset"] = IsReset;
    map["IsSelected"] = IsSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    map["listZCMP"] = listZCMP != null ? listZCMP?.map((e) => e.toMap()).toList() : null;


    return map;
  }
}
