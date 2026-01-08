class JurusanDto {
  String kode_fakultas = "";
  String kode_jurusan = "";
  String nama_jurusan = "";
  double record_status = 0;

  bool isSelected = false;
  double PageNumber = 0;
  double PageSize = 0;
  double TotalPage = 0;
  double TotalRecord = 0;

  JurusanDto({
    this.kode_fakultas = "",
    this.kode_jurusan = "",
    this.nama_jurusan = "",
    this.record_status = 0,
    this.isSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
  });

  factory JurusanDto.fromJson(Map<String, dynamic> json) {
    return JurusanDto(
      kode_fakultas: json["kode_fakultas"] ?? "",
      kode_jurusan: json["kode_jurusan"] ?? "",
      nama_jurusan: json["nama_jurusan"] ?? "",
      record_status: json["record_status"] ?? 0,
      isSelected: json["isSelected"] ?? false,
      PageNumber: json["PageNumber"] ?? 0,
      PageSize: json["PageSize"] ?? 0,
      TotalPage: json["TotalPage"] ?? 0,
      TotalRecord: json["TotalRecord"] ?? 0,
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["kode_fakultas"] = kode_fakultas ?? "";
    map["kode_jurusan"] = kode_jurusan ?? "";
    map["nama_jurusan"] = nama_jurusan ?? "";
    map["record_status"] = record_status ?? 0;
    map["isSelected"] = isSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    return map;
  }
}
