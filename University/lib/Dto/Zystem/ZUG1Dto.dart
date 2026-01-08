import 'package:university/Dto/Zystem/ZUG2Dto.dart';

import 'ZUSRDto.dart';

class ZUG1Dto
{
	String ZGCONO = "";
	String ZGBRNO = "";
	String ZGUGNO = "";
	String ZGUGNA = "";
	String ZGREMA = "";
	String ZGSYST = "";
	String ZGSTAT = "";
	double ZGRCST = 0;
	double ZGCRDT = 0;
	double ZGCRTM = 0;
	String ZGCRUS = "";
	double ZGCHDT = 0;
	double ZGCHTM = 0;
	String ZGCHUS = "";
	List<ZUG2Dto>? lstZUG2;
	List<ZUSRDto>? lstZUSR;

	ZUG1Dto({
		this.ZGCONO = "",
		this.ZGBRNO = "",
		this.ZGUGNO = "",
		this.ZGUGNA = "",
		this.ZGREMA = "",
		this.ZGSYST = "",
		this.ZGSTAT = "",
		this.ZGRCST = 0,
		this.ZGCRDT = 0,
		this.ZGCRTM = 0,
		this.ZGCRUS = "",
		this.ZGCHDT = 0,
		this.ZGCHTM = 0,
		this.ZGCHUS = "",
		this.lstZUG2,
		this.lstZUSR,
	});

	factory ZUG1Dto.fromJson(Map<String, dynamic> json) {
		return ZUG1Dto(
			ZGCONO: json["ZGCONO"] ?? "",
			ZGBRNO: json["ZGBRNO"] ?? "",
			ZGUGNO: json["ZGUGNO"] ?? "",
			ZGUGNA: json["ZGUGNA"] ?? "",
			ZGREMA: json["ZGREMA"] ?? "",
			ZGSYST: json["ZGSYST"] ?? "",
			ZGSTAT: json["ZGSTAT"] ?? "",
			ZGRCST: json["ZGRCST"] ?? 0,
			ZGCRDT: json["ZGCRDT"] ?? 0,
			ZGCRTM: json["ZGCRTM"] ?? 0,
			ZGCRUS: json["ZGCRUS"] ?? "",
			ZGCHDT: json["ZGCHDT"] ?? 0,
			ZGCHTM: json["ZGCHTM"] ?? 0,
			ZGCHUS: json["ZGCHUS"] ?? "",
			lstZUG2:json["lstZUG2"] != null ? (json["lstZUG2"] as List).map((e) => ZUG2Dto.fromJson(e)).toList() : null,
			lstZUSR:json["lstZUSR"] != null ? (json["lstZUSR"] as List).map((e) => ZUSRDto.fromJson(e)).toList() : null,
		);
	}

	Map toMap()
	{
		var map = <String, dynamic>{};
		map["ZGCONO"] = ZGCONO;
		map["ZGBRNO"] = ZGBRNO;
		map["ZGUGNO"] = ZGUGNO;
		map["ZGUGNA"] = ZGUGNA;
		map["ZGREMA"] = ZGREMA;
		map["ZGSYST"] = ZGSYST;
		map["ZGSTAT"] = ZGSTAT;
		map["ZGRCST"] = ZGRCST;
		map["ZGCRDT"] = ZGCRDT;
		map["ZGCRTM"] = ZGCRTM;
		map["ZGCRUS"] = ZGCRUS;
		map["ZGCHDT"] = ZGCHDT;
		map["ZGCHTM"] = ZGCHTM;
		map["ZGCHUS"] = ZGCHUS;
		map["lstZUG2"] = lstZUG2 != null ? lstZUG2!.map((e) => e.toMap()).toList() : null;
		map["lstZUSR"] = lstZUSR != null ? lstZUSR!.map((e) => e.toMap()).toList() : null;
		return map;
	}
}
