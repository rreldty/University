class SearchDto{
  String CODE = "";
  String DSCR = "";
  String REMA = "";

  SearchDto({
    this.CODE = "",
    this.DSCR = "",
    this.REMA = "",
  });

  factory SearchDto.fromJson(Map<String, dynamic> json) {
    return SearchDto(
      CODE: json["CODE"] ?? "",
      DSCR: json["DSCR"] ?? "",
      REMA: json["REMA"] ?? "",
    );
  }

  Map toMap() {
    var map = <String, dynamic>{};
    map["CODE"] = CODE;
    map["DSCR"] = DSCR;
    map["REMA"] = REMA;

    return map;
  }
}
