
class MenuNodeDto{
  String key = "";
  String label = "";
  bool expanded = false;
  dynamic data;
  List<MenuNodeDto>? children;

  MenuNodeDto({
    this.key = "",
    this.label = "",
    this.expanded = false,
    this.data,
    this.children,
  });
}
