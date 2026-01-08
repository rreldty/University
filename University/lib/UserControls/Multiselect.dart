import 'package:flutter/material.dart';
import 'package:flutter/foundation.dart';

import 'package:intl/intl.dart';
import '../../Common/GlobalStyle.dart';
import '../../Dao/Base/DDLDao.dart';
import '../../Dto/Base/DDLDto.dart';

import '../Common/CommonMethod.dart';
import '../Dto/Base/EntityDto.dart';
import '../Dto/Base/MultiselectDto.dart';
import 'MultiselectField.dart';

enum MultiselectType {
  Entity,
  Day,
  Month,
  Year,
  ListItem,
}

class Multiselect extends StatefulWidget {
  final String entity;
  final String hintText;
  final String selectVal;
  final MultiselectController controller;
  final Function(List<DDLDto> obj)? onChanged;
  final bool isMandatory;
  final List<DDLDto>? itemList;
  final MultiselectType multiselectType;
  final double width;
  final bool? isEnable;
  final bool? isVisible;

  const Multiselect({
    super.key,
    this.multiselectType = MultiselectType.Entity,
    this.entity = "",
    this.hintText = "",
    required this.controller,
    this.onChanged,
    this.selectVal = "",
    this.isMandatory = false,
    this.itemList,
    this.width = 0,
    this.isEnable,
    this.isVisible,
  });

  @override
  createState() => MultiselectState();
}

class MultiselectState extends State<Multiselect> {
  DDLDao dao = DDLDao();
  double widthSize = double.infinity;
  double heightSize = 30;

  List<DDLDto> _futureItems = [];

  @override
  void initState() {
    widget.controller.isEnable = widget.isEnable ?? true;
    widget.controller.isVisible = widget.isVisible ?? true;

    widthSize = double.infinity;
    if(CommonMethod.isWebOrDesktop()){
      if(widget.width > 0){
        widthSize = widget.width;
      }
      else {
        widthSize = GlobalStyle.webControlWidth;
      }
    }
    super.initState();
  }

  //region getList Methods
  List<DDLDto> _getItemDay()
  {
    List<DDLDto> lst = [];
    for (int i = 1; i <= 31; i++)
    {
      lst.add(DDLDto(Code: i.toString(), Dscr: i.toString()));
    }

    return lst;
  }

  List<DDLDto> _getItemMonth()
  {
    List<DDLDto> lst = [];
    for (int i = 1; i <= 12; i++)
    {
      lst.add(DDLDto(Code: NumberFormat("00", "en_US").format(i), Dscr: DateFormat.MMMM().format(DateTime(2000,i,1))));
    }

    return lst;
  }

  List<DDLDto> _getItemYear()
  {
    List<DDLDto> lst = [];

    for (int i = DateTime.now().year; i >= 2000; i--)
    {
      lst.add(DDLDto(Code: i.toString(), Dscr: i.toString()));
    }

    return lst;
  }

  List<DDLDto> _getListItem()
  {
    List<DDLDto> lst = [];

    // debugPrint("_getListItem");
    // debugPrint(widget.itemList);

    if (widget.itemList != null)
    {
      lst = widget.itemList!;
    }

    return lst;
  }

  Future<List<DDLDto>> _getList() async {
    // debugPrint(widget.multiselectType.toString() + "-" + widget.entity + ": _getList");
    // debugPrint(widget.multiselectType.toString() + "-" + widget.entity + "-refresh: " + widget.controller.isRefresh.toString());
    // debugPrint(widget.multiselectType.toString() + "-" + widget.entity + "-futureItems-isnull: " + (_futureItems == null).toString());

    if(_futureItems.isEmpty || widget.controller.isRefresh) {
      if(widget.controller.isEnable){
        switch(widget.multiselectType){
          case MultiselectType.Entity:{
            // debugPrint(widget.entity + ": _getList Http");
            // debugPrint(widget.entity + "-filter: " + widget.controller.filter);

            EntityDto obj = EntityDto(
                Entity: widget.entity,
                Filter: widget.controller.filter
            );
            _futureItems = await dao.getList(obj);
            break;
          }
          case MultiselectType.Day:{
            _futureItems = _getItemDay();
            break;
          }
          case MultiselectType.Month:{
            _futureItems = _getItemMonth();
            break;
          }
          case MultiselectType.Year:{
            _futureItems = _getItemYear();
            break;
          }
          case MultiselectType.ListItem:{
            _futureItems = _getListItem();
            break;
          }
          default:{
            break;
          }
        }
      }
      else{
        _futureItems = [];
      }

      widget.controller.selectedItems = [];
    }

    if(widget.controller.isRefresh){
      widget.controller.isRefresh = false;
    }

    // debugPrint(widget.multiselectType.toString() + "-" + widget.entity + "-futureItems-isnull: " + (_futureItems == null).toString());

    return Future.value(_futureItems);
  }
  //endregion

  @override
  Widget build(BuildContext context) {
    if(widget.controller.isVisible){
      return FutureBuilder(
        future: _getList(),
        builder: (BuildContext context, AsyncSnapshot<List<DDLDto>> snapshot){
          if(snapshot.hasData){
            // debugPrint("[${widget.entity}] has data");

            List<DDLDto> _lstDDL = [];

            for (int i = 0; i < snapshot.data!.length; i++) {
              _lstDDL.add(snapshot.data![i]);
            }

            // debugPrint("[${widget.entity}] build multiselect");
            return buildMultiselect(_lstDDL);
          }
          else{
            return Container(
              margin: const EdgeInsets.only(top: 5),
              padding: const EdgeInsets.only(left: 10, top: 5, right: 10, bottom: 5),
              decoration: BoxDecoration(
                color: !widget.controller.isEnable ? GlobalStyle.disableColor : Colors.white,
                border: Border.all(color: Colors.grey, width: 1),
                borderRadius: BorderRadius.circular(5),
              ),
              width: widthSize,
              height: heightSize,
              child: const Text(""),
            );
          }
        },
      );
    }
    else{
      return Container();
    }
  }

  Widget buildMultiselect(List<DDLDto> _lstDDL){
    // debugPrint("[${widget.entity}] data length ${_lstDDL.length}");
    // debugPrint("buildMultiselect " + widget.entity);
    // debugPrint("Enable: " + widget.controller.isEnable.toString());
    // debugPrint("SelectedItems: " + widget.controller.selectedItems.isNotEmpty.toString());

    if (widget.controller.isEnable) {
      return Container(
        margin: const EdgeInsets.only(top: 5),
        child: SizedBox(
          width: widthSize,
          //height: heightSize,
          child: MultiselectField<DDLDto>(
              validator: validateItems,
              onConfirm: (items) {
                widget.controller.selectedItems = items;

                if(widget.onChanged != null){
                  widget.onChanged!(items);
                }
              },
              items: _lstDDL.map((obj) => MultiselectDto<DDLDto>(obj, obj.Dscr)).toList(),
              initialValue: widget.controller.selectedItems,
          ),
        ),
      );
    }
    else {
      return Container(
        margin: const EdgeInsets.only(top: 5),
        padding: const EdgeInsets.only(left: 10, top: 5, right: 10, bottom: 5),
        decoration: BoxDecoration(
          color: GlobalStyle.disableColor,
          border: Border.all(color: Colors.grey, width: 1),
          borderRadius: BorderRadius.circular(5),
        ),
        width: widthSize,
        height: heightSize,
        child: Text(widget.controller.selectedItems != null && widget.controller.selectedItems!.isNotEmpty ? "Some" : "-- All --"),
      );
    }
  }

  String? validateItems(List<DDLDto>? selectedItems){
    String strResult = "";

    if(widget.isMandatory){
      if(selectedItems != null && selectedItems.isEmpty)
      {
        strResult = "*${widget.hintText} select 1 or more";
      }
    }

    if(strResult.isNotEmpty) {
      return strResult;
    }
    return null;
  }
}

class MultiselectController{
  String filter = "";
  bool isRefresh = false;
  bool isEnable = true;
  bool isVisible = true;

  List<DDLDto>? selectedItems;

  List<String> get selectedValues{
    if(selectedItems != null){
      return selectedItems!.map((e) => e.Code).toList();
    }
    return [];
  }

  String get selectedString{
    if(selectedItems != null){
      return selectedItems!.map((e) => e.Code).toList().join(",");
    }
    return "";
  }

  void reset(){
    selectedItems = [];
    isRefresh = true;
  }
}