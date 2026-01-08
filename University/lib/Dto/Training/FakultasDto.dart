class FakultasDto{
  String kode_fakultas = "";
  String nama_fakultas = "";
  double record_status = 0;

  bool isSelected = false;
  double PageNumber = 0;
  double PageSize = 0;
  double TotalPage = 0;
  double TotalRecord = 0;

  FakultasDto({
    this.kode_fakultas = "",
    this.nama_fakultas = "",
    this.record_status = 0,

    this.isSelected = false,
    this.PageNumber = 0,
    this.PageSize = 0,
    this.TotalPage = 0,
    this.TotalRecord = 0,
  });

  factory FakultasDto.fromJson(Map<String, dynamic> json) {
    return FakultasDto(
      kode_fakultas: json["kode_fakultas"] ?? "",
      nama_fakultas: json["nama_fakultas"] ?? "",
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
    map["nama_fakultas"] = nama_fakultas ?? "";
    map["record_status"] = record_status ?? 0;

    map["isSelected"] = isSelected;
    map["PageNumber"] = PageNumber;
    map["PageSize"] = PageSize;
    map["TotalPage"] = TotalPage;
    map["TotalRecord"] = TotalRecord;
    return map;
  }
}