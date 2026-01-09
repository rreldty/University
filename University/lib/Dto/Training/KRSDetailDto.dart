class KRSDetailDto {
  String nim = "";
  String semester = "";
  String kode_matakuliah = "";
  String nama_matakuliah = "";
  double sks = 0;
  int line_no = 0;

  bool isSelected = false;
  int PageNumber = 0;
  int PageSize = 0;
  int TotalPage = 0;
  int TotalRecord = 0;

  KRSDetailDto({
    this.nim = "",
    this.semester = "",
    this.kode_matakuliah = "",
    this.nama_matakuliah = "",
    this.sks = 0,
    this.line_no = 0,
    this.isSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
  });

  factory KRSDetailDto.fromJson(Map<String, dynamic> json) {
    return KRSDetailDto(
      nim: json["nim"] ?? "",
      semester: json["semester"] ?? "",
      kode_matakuliah: json["kode_matakuliah"] ?? "",
      nama_matakuliah: json["nama_matakuliah"] ?? "",
      sks: (json["sks"] ?? 0).toDouble(),
      line_no: (json["line_no"] ?? 0).toInt(),
      isSelected: json["isSelected"] ?? false,
      PageNumber: (json["PageNumber"] ?? 0).toInt(),
      PageSize: (json["PageSize"] ?? 0).toInt(),
      TotalPage: (json["TotalPage"] ?? 0).toInt(),
      TotalRecord: (json["TotalRecord"] ?? 0).toInt(),
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["nim"] = nim;
    map["semester"] = semester;
    map["kode_matakuliah"] = kode_matakuliah;
    map["nama_matakuliah"] = nama_matakuliah;
    map["sks"] = sks;
    map["line_no"] = line_no;
    map["isSelected"] = isSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    return map;
  }
}
