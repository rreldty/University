import 'package:flutter/material.dart';
import 'package:flutter/foundation.dart';
import 'package:shared_preferences/shared_preferences.dart';

import 'package:shimmer/shimmer.dart';
import 'package:intl/intl.dart';
import '../../Common/GlobalStyle.dart';
import '../../Dao/Base/DDLDao.dart';
import '../../Dto/Base/DDLDto.dart';
import '../Common/CommonMethod.dart';
import '../Dto/Base/EntityDto.dart';

enum ComboBoxType {
  Entity,
  Day,
  Month,
  Year,
  RecordStatus,
  Choice,
  MathOperation,
  ListItem,
}

class ComboBox extends StatefulWidget {
  final String entity;
  final String hintText;
  final String selectVal;
  final ComboBoxController controller;
  final Function(DDLDto? obj)? onChanged;
  final bool showEmpty;
  final bool isMandatory;
  final List<DDLDto>? itemList;
  final ComboBoxType comboBoxType;
  final double width;
  final bool? isEnable;
  final bool? isVisible;

  const ComboBox({
    super.key,
    this.comboBoxType = ComboBoxType.Entity,
    this.entity = "",
    this.hintText = "",
    required this.controller,
    this.onChanged,
    this.selectVal = "",
    this.showEmpty = true,
    this.isMandatory = false,
    this.isVisible = true,
    this.itemList,
    this.width = 0,
    this.isEnable,
  });

  @override
  createState() => ComboBoxState();
}

class ComboBoxState extends State<ComboBox> {
  List<DDLDto> _futureItem = [];
  DDLDao dao = DDLDao();
  double widthSize = double.infinity;
  final double heightSize = 30;
  List<DDLDto> lstDDL = [];
  late DDLDto selected;
  bool isFirstLoad = false;

  @override
  void initState() {
    widget.controller.isEnable = widget.isEnable ?? true;
    widget.controller.isVisible = widget.isVisible ?? true;

    widthSize = double.infinity;

    if(CommonMethod.isWebOrDesktop()){
      if (widget.width > 0) {
        widthSize = widget.width;
      } else {
        widthSize = GlobalStyle.webControlWidth;
      }
    }

    super.initState();

    WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      bool isAppInitRun = prefs.getBool("isAppInitRun") ?? true;

      // debugPrint("[ComboBox] isAppInitRun: ${isAppInitRun.toString()}");

      var futureWithTheLoop = () async {
        while(isAppInitRun){
          isAppInitRun = prefs.getBool("isAppInitRun") ?? true;
          // debugPrint("[ComboBox] isAppInitRun: ${isAppInitRun.toString()}");
          await Future.delayed(const Duration(milliseconds: 10));
        }
      }();

      Future.wait([futureWithTheLoop]).whenComplete((){
        if(!isAppInitRun) {
          // debugPrint("ComboBox Start");
          isFirstLoad = true;
        }
      });

      //
      // setState(() {
      //   isFirstLoad = !(prefs.getBool("isAppInitRun") ?? false);
      // });
    });
  }

  //region getList Methods
  List<DDLDto> _getItemDay() {
    List<DDLDto> lst = [];
    for (int i = 1; i <= 31; i++) {
      lst.add(DDLDto(Code: i.toString(), Dscr: i.toString()));
    }

    return lst;
  }

  List<DDLDto> _getItemMonth() {
    List<DDLDto> lst = [];
    for (int i = 1; i <= 12; i++) {
      lst.add(DDLDto(Code: NumberFormat("00", "en_US").format(i), Dscr: DateFormat.MMMM().format(DateTime(2000, i, 1))));
    }

    return lst;
  }

  List<DDLDto> _getItemYear() {
    List<DDLDto> lst = [];

    for (int i = DateTime.now().year; i >= 2000; i--) {
      lst.add(DDLDto(Code: i.toString(), Dscr: i.toString()));
    }

    return lst;
  }

  List<DDLDto> _getListItem() {
    List<DDLDto> lst = [];

    if (widget.itemList != null) {
      lst = widget.itemList!;
    }

    return lst;
  }

  List<DDLDto> _getRecordStatus() {
    List<DDLDto> lst = [];
    lst.add(DDLDto(Code: "1", Dscr: "Active"));
    lst.add(DDLDto(Code: "0", Dscr: "Inactive"));

    return lst;
  }

  List<DDLDto> _getChoice() {
    List<DDLDto> lst = [];
    lst.add(DDLDto(Code: "1", Dscr: "Yes"));
    lst.add(DDLDto(Code: "0", Dscr: "No"));

    return lst;
  }

  List<DDLDto> _getMathOperation() {
    List<DDLDto> lst = [];
    lst.add(DDLDto(Code: "ME", Dscr: "More or Equal"));
    lst.add(DDLDto(Code: "EQ", Dscr: "Equal"));
    lst.add(DDLDto(Code: "LE", Dscr: "Less or Equal"));

    return lst;
  }

  Future<List<DDLDto>> _getList() async {
    debugPrint("[ComboBox] [${widget.entity}] _getList");
    debugPrint("[ComboBox] [${widget.entity}] isFirstLoad $isFirstLoad");
    debugPrint("[ComboBox] [${widget.entity}] isRefresh ${widget.controller.isRefresh}");
    // debugPrint("[ComboBox] [${widget.entity}] filter ${widget.controller.filter}");
    // debugPrint("[ComboBox] [${widget.entity}] value ${widget.controller.value}");
    // debugPrint("[ComboBox] [${widget.entity}] item ${widget.controller.selectedItem != null ? widget.controller.selectedItem!.Code : ""}");

    bool isRefreshData = widget.controller.isRefresh;

    if(isFirstLoad)
    {
      isRefreshData = true;
      isFirstLoad = false;
    }

    if(widget.controller.selectedItem != null && !widget.controller.isEnable ){
      if(widget.controller.selectedItem!.Code != widget.controller.value && !widget.controller.isRefresh){
        // widget.controller.isRefresh = true;
        isRefreshData = true;
      }
    }

    debugPrint("[ComboBox] [${widget.entity}] isRefreshData $isRefreshData");

    if (isRefreshData) {
      switch (widget.comboBoxType) {
        case ComboBoxType.Entity:
          {
            debugPrint("[ComboBox] [${widget.entity}] : _getList Http");
            debugPrint("[ComboBox] [${widget.entity}] filter ${widget.controller.filter}");

            EntityDto obj = EntityDto(Entity: widget.entity, Filter: widget.controller.filter, KeyCode: !widget.controller.isEnable ? widget.controller.value : "");
            _futureItem = await dao.getList(obj);
            break;
          }
        case ComboBoxType.Day:
          {
            _futureItem = _getItemDay();
            break;
          }
        case ComboBoxType.Month:
          {
            _futureItem = _getItemMonth();
            break;
          }
        case ComboBoxType.Year:
          {
            _futureItem = _getItemYear();
            break;
          }
        case ComboBoxType.RecordStatus:
          {
            _futureItem = _getRecordStatus();
            break;
          }
        case ComboBoxType.Choice:
          {
            _futureItem = _getChoice();
            break;
          }
        case ComboBoxType.MathOperation:
          {
            _futureItem = _getMathOperation();
            break;
          }
        case ComboBoxType.ListItem:
          {
            _futureItem = _getListItem();
            break;
          }
        default:
          {
            break;
          }
      }
    }

    if (widget.controller.isRefresh) {
      widget.controller.isRefresh = false;
    }

    return Future.value(_futureItem);
  }

  //endregion

  @override
  Widget build(BuildContext context) {
    if (widget.controller.isVisible) {
      return FutureBuilder(
        future: _getList(),
        builder: (BuildContext context, AsyncSnapshot<List<DDLDto>> snapshot) {
          if (snapshot.hasData) {
            // debugPrint("[ComboBox] ${widget.entity}: has data");
            // debugPrint("[ComboBox] ${widget.entity}: data length ${snapshot.data!.length}");

            lstDDL = [];

            if (widget.showEmpty) {
              lstDDL.add(DDLDto(Code: "", Dscr: "-- Empty --"));
            }

            lstDDL.addAll(snapshot.data!);

            return buildComboBox();
          } else {
            return Container(
              margin: const EdgeInsets.only(top: 5),
              padding: const EdgeInsets.only(left: 9, top: 4, right: 10, bottom: 6),
              decoration: BoxDecoration(
                color: !widget.controller.isEnable ? GlobalStyle.disableColor : Colors.white,
                border: Border.all(color: Colors.grey, width: 1),
                borderRadius: BorderRadius.circular(5),
              ),
              width: widthSize,
              height: heightSize + 1,
              child: Text("", style: TextStyle(fontSize: GlobalStyle.fontSize)),
            );
          }
        },
      );
    } else {
      return Container();
    }
  }

  Widget buildComboBox() {
    if (lstDDL.isNotEmpty) {
      int selectedIndex = widget.controller.selectedIndex > (lstDDL.length - 1) ? 0 : widget.controller.selectedIndex;
      DDLDto objSelectedItemIndex = lstDDL[selectedIndex];
      DDLDto objSelectedItemValue = lstDDL.firstWhere((obj) => obj.Code == widget.controller.value, orElse: () => lstDDL[0],);

      // debugPrint("[ComboBox] ${widget.entity}: ${objSelectedItemIndex.Code}");
      // debugPrint("[ComboBox] ${widget.entity}: ${objSelectedItemValue.Code}");

      if (objSelectedItemIndex.Code != objSelectedItemValue.Code) {
        if(widget.controller._isValueChanged){
          objSelectedItemIndex = objSelectedItemValue;
          widget.controller._selectedIndex = lstDDL.indexOf(objSelectedItemValue);
          widget.controller._isValueChanged = false;
        }

        if(widget.controller._isIndexChanged){
          objSelectedItemValue = objSelectedItemIndex;
          widget.controller._isIndexChanged = false;
        }
      }

      widget.controller.value = objSelectedItemValue.Code;
      widget.controller.selectedText = objSelectedItemValue.Dscr;
      widget.controller.selectedItem = objSelectedItemValue;

      // debugPrint("[ComboBox] ${widget.entity}: ${widget.controller.selectedText}");
      // debugPrint("[ComboBox] ${widget.entity}: ${widget.controller.selectedItem!.Dscr}");
    }

    if (widget.controller.isEnable) {
      // debugPrint("[ComboBox] ${widget.entity}: build dropdown");
      return Container(
        margin: const EdgeInsets.only(top: 5),
        child: SizedBox(
          width: widthSize,
          // height: heightSize,
          child: DropdownButtonHideUnderline(
            child: DropdownButtonFormField<DDLDto>(
              iconSize: heightSize - 3,
              decoration: InputDecoration(
                contentPadding: const EdgeInsets.fromLTRB(10, 6, 10, 6),
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(5.0),
                  borderSide: const BorderSide(
                    color: Color(0xffbababa),
                    width: 1.0,
                  ),
                ),
                disabledBorder: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(5.0),
                  borderSide: const BorderSide(
                    color: Color(0xffbababa),
                    width: 1.0,
                  ),
                ),
                enabledBorder: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(5.0),
                  borderSide: const BorderSide(
                    color: Color(0xffbababa),
                    width: 1.0,
                  ),
                ),
                errorBorder: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(5.0),
                  borderSide: BorderSide(
                    color: GlobalStyle.errorColor,
                    width: 1.0,
                  ),
                ),
                isDense: true,
              ),
              isExpanded: true,
              isDense: true,
              value: widget.controller.selectedItem,
              hint: Text(widget.hintText),
              validator: validateItem,
              items: lstDDL.map((model) {
                return DropdownMenuItem<DDLDto>(
                  value: model,
                  child: Text(
                      model.Dscr,
                      maxLines: 1,
                      style: TextStyle(fontSize: GlobalStyle.fontSize)
                  ),
                );
              }).toList(),
              onChanged: (DDLDto? obj) {
                // FocusScope.of(context).requestFocus(FocusNode());

                widget.controller.value = obj!.Code;
                widget.controller.selectedItem = obj;

                if (widget.onChanged != null) {
                  widget.onChanged!(obj);
                }
              },
            ),
          ),
        ),
      );
    } else {
      return Container(
        margin: const EdgeInsets.only(top: 5),
        padding: const EdgeInsets.only(left: 9, top: 4, right: 10, bottom: 6),
        decoration: BoxDecoration(
          color: GlobalStyle.disableColor,
          border: Border.all(color: Colors.grey, width: 1),
          borderRadius: BorderRadius.circular(5),
        ),
        width: widthSize,
        height: heightSize + 1,
        child: Text(widget.controller.selectedItem!.Dscr,
            maxLines: 1,
            style: TextStyle(fontSize: GlobalStyle.fontSize,color: Colors.black,)
        ),
      );
    }
  }

  String? validateItem(DDLDto? item) {
    String strResult = "";

    if (widget.isMandatory) {
      if (item != null && item.Code.isEmpty) {
        strResult = "Please fill this field";
      }
    }

    if (strResult.isNotEmpty) {
      return strResult;
    }
    return null;
  }
}

class ComboBoxController {
  String selectedText = "";
  DDLDto? selectedItem;
  String filter = "";
  bool isRefresh = false;
  bool isEnable = true;
  bool isVisible = true;

  bool _isValueChanged = false;
  String _value = "";
  String get value{
    return _value;
  }
  set value(String value){
    _value = value;
    _isValueChanged = true;
  }

  bool _isIndexChanged = false;
  int _selectedIndex = 0;
  int get selectedIndex{
    return _selectedIndex;
  }
  set selectedIndex(int value){
    _selectedIndex = value;
    _isIndexChanged = true;
  }
}
