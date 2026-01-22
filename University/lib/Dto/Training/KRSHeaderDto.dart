import 'KrsDetailDto.dart';

class KrsHeaderDto{
  String nim = "";
  double semester = 0;
  String kode_fakultas = "";
  String kode_jurusan = "";
  double total_sks = 0;

  KrsDetailDto? objKrsDetail;
  List<KrsDetailDto>? listKrsDetail;
  double PageNumber = 0;
  double PageSize = 0;
  double TotalPage = 0;
  double TotalRecord = 0;

  KrsHeaderDto({
    this.nim = "",
    this.semester = 0,
    this.kode_fakultas = "",
    this.kode_jurusan = "",
    this.total_sks = 0,

    this.objKrsDetail,
    this.listKrsDetail,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
  });

  factory KrsHeaderDto.fromJson(Map<String, dynamic> json) {
    return KrsHeaderDto(
      nim: json["nim"] ?? "",
      semester: json["semester"] ?? 0,
      kode_fakultas: json["kode_fakultas"] ?? "",
      kode_jurusan: json["kode_jurusan"] ?? "",
      total_sks: json["total_sks"] ?? 0,

      objKrsDetail: json["objKrsDetail"] != null ? KrsDetailDto.fromJson(json["objKrsDetail"]) : null,
      listKrsDetail:json["listKrsDetail"] != null ? (json["listKrsDetail"] as List).map((e) => KrsDetailDto.fromJson(e)).toList() : null,
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["nim"] = nim ?? "";
    map["semester"] = semester ?? 0;
    map["kode_fakultas"] = kode_fakultas ?? "";
    map["kode_jurusan"] = kode_jurusan ?? "";
    map["total_sks"] = total_sks ?? 0;

    map["objKrsDetail"] = objKrsDetail != null ? objKrsDetail!.toMap() : null;
    map["listKrsDetail"] = listKrsDetail != null ? listKrsDetail!.map((e) => e.toMap()).toList() : null;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    return map;
  }
}