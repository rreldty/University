import 'package:flutter/rendering.dart';

import '../Common/AppConfig.dart';
import '../Dto/Base/LookupDto.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';

import '../Common/CommonMethod.dart';
import '../Common/LabelDictionary.dart';
import '../Dao/Base/LookupDao.dart';
import '../Dto/Base/EntityDto.dart';
import '../Dto/Base/LookupHeaderDto.dart';
import 'EditText.dart';
import 'LookupSearchBox.dart';

class LookupDataGrid extends StatefulWidget {
  final LookupDto initialData;
  final String entity;
  final String filter;
  final String sort;
  final int pageNumber;
  final int pageSize;
  final List<int>? hideColumns;
  final Function(Map<String, dynamic> map)? selectedRowCallback;
  final bool? showPaging;

  const LookupDataGrid({
    super.key,
    required this.initialData,
    required this.entity,
    this.filter = "",
    this.sort = "",
    this.pageNumber = 1,
    this.pageSize = 20,
    this.hideColumns,
    this.selectedRowCallback,
    this.showPaging,
  });

  @override
  createState() => LookupDataGridState();
}

class LookupDataGridState extends State<LookupDataGrid> {
  LookupDto? objLookup;

  int totalPage = 0;
  int totalRecord = 0;
  int intPageNumber = 0;
  int intPageSize = 0;
  List<int> hideColumns = [];

  List<String> lstSearchBy = [];
  List<String> lstOperator = [];
  List<String> lstSearchKey = [];
  List<EditTextController> lstSearchController = [];

  Map<int, TableColumnWidth> mapColWidth = {};
  Map<String, dynamic> numericColumns = {};

  ScrollController verticalScrollController = ScrollController();

  @override
  void initState() {
    super.initState();
    objLookup = widget.initialData;
    intPageNumber = widget.pageNumber;
    intPageSize = widget.pageSize;

    if(widget.hideColumns != null){
      hideColumns = widget.hideColumns!;
    }

    if(objLookup != null) {
      totalPage = objLookup!.TotalPage;
      totalRecord = objLookup!.TotalRecord;

      if(lstSearchBy.isEmpty){
        for(int h = 0; h < objLookup!.Headers!.length; h++){
          LookupHeaderDto objHeader = objLookup!.Headers![h];
          lstSearchBy.add(objHeader.HeaderName);
          lstOperator.add("%");
          lstSearchKey.add("");
          lstSearchController.add(EditTextController());
        }
      }

    }
  }

  Future<LookupDto> getList() async {
    //debugPrint("[Lookup] getData");

    if(lstSearchController.isNotEmpty){
      lstSearchKey.clear();
      for(int n = 0; n < lstSearchController.length; n++){
        lstSearchKey.add(lstSearchController[n].text);
      }
    }

    EntityDto obj = EntityDto(
      Entity: widget.entity,
      Filter: widget.filter,
      Sort: widget.sort,
      PageNum: intPageNumber,
      PageSize: intPageSize,
      SearchBys: lstSearchBy,
      Operators: lstOperator,
      SearchKeys: lstSearchKey,
    );

    LookupDao dao = LookupDao();
    debugPrint("[Lookup] return getData");
    return dao.listLookup(obj);
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      key: UniqueKey(),
      width: double.infinity,
      alignment: Alignment.topLeft,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.start,
        mainAxisSize: MainAxisSize.max,
        children: [
          SizedBox(
            width: double.infinity,
            child: Table(
              columnWidths: mapColWidth,
              children: lookupHeader(),
            ),
          ),
          Expanded(
            child: builderRows(),
          ),
          (widget.showPaging ?? true)? lookupFooter() : Container(),
        ],
      ),
    );

  }

  Widget builderRows(){
    return FutureBuilder(
        future: getList(),
        builder: (context, snapshot) {
          Widget child = const SizedBox(
              key: ValueKey(0),
              width: double.infinity,
              height: double.infinity,
              child: Center(child: Text("No Data Found"),)
          );

          if(snapshot.connectionState == ConnectionState.done){
            if(snapshot.hasData){
              objLookup = snapshot.data as LookupDto;
              child = SizedBox(

                key: const ValueKey(1),
                width: double.infinity,
                height: double.infinity,
                child: Scrollbar(
                  controller: verticalScrollController,
                  thumbVisibility: true,
                  trackVisibility: true,
                  child: SingleChildScrollView(
                    controller: verticalScrollController,
                    scrollDirection: Axis.vertical,
                    child: lookupRows(),
                  ),
                ),
              );
            }
          }
          else{
            child = const SizedBox(
              key: ValueKey(2),
              width: double.infinity,
              height: double.infinity,
              child: Center(
                child: CircularProgressIndicator(),
              ),
            );
          }

          return AnimatedSwitcher(
              duration: const Duration(seconds: 1),
              child: child,
          );
        }
    );
  }

  List<TableRow> lookupHeader(){
    List<TableRow> tableHeader = [];
    List<Widget> columnCells = [];
    List<Widget> searchCells = [];

    for(int h = 0; h < objLookup!.Headers!.length; h++){
      LookupHeaderDto objHeader = objLookup!.Headers![h];

      bool isNum = false;
      if(objHeader.HeaderType == "Decimal"){
        if(objHeader.HeaderFormat.contains("Date") || objHeader.HeaderFormat.contains("Time")){
          isNum = false;
        }else{
          isNum = true;
        }
      }

      numericColumns[objHeader.HeaderName] = isNum;

      if(objHeader.HeaderWidth.isNotEmpty) {
        if(objHeader.HeaderWidth == "*"){
          mapColWidth[h] = const FlexColumnWidth(1);
        }else{
          mapColWidth[h] = FixedColumnWidth(double.parse(objHeader.HeaderWidth));
        }
      }
      else {
        if(h == 0) {
          mapColWidth[h] = const FixedColumnWidth(140);
        }else {
          mapColWidth[h] = const FlexColumnWidth(1);
        }
      }

      String strLabel = objHeader.HeaderName;
      strLabel = LabelDictionary.getLabelDictionary(strLabel);
      //debugPrint("[Lookup] label $strLabel");

      var cc = Container(
        padding: const EdgeInsets.only(left: 2, top: 5, right: 5, bottom: 5),
        child: Text(
          strLabel,
          style: TextStyle(fontSize: GlobalStyle.fontSize, color: GlobalStyle.gridFontColor),
          textAlign: isNum ? TextAlign.end : TextAlign.start,
        ),
      );
      if(!hideColumns.contains(h)){
        columnCells.add(cc);
      }

      var sc = Container(
        padding: const EdgeInsets.all(0),
        child: LookupSearchBox(
            controller: lstSearchController[h],
            onLostFocus: (){
              setState((){
                intPageNumber = 1;
              });
            }
        ),
      );

      if(!hideColumns.contains(h)){
        searchCells.add(sc);
      }
    }

    var trc = TableRow(
      decoration: BoxDecoration(
        color: GlobalStyle.gridColor,
        border: Border(
          top: BorderSide(color: GlobalStyle.toolbarBorderColor, width: 1),
        ),
      ),
      children: columnCells,
    );

    var tsc = TableRow(
      decoration: BoxDecoration(
        color: GlobalStyle.gridColor,
        border: Border(
          bottom: BorderSide(color: GlobalStyle.toolbarBorderColor, width: 1),
        ),
      ),
      children: searchCells,
    );

    tableHeader.add(trc);
    tableHeader.add(tsc);

    return tableHeader;
  }

  Widget lookupRows(){
    List<TableRow> tableRows = [];

    for(int r = 0; r < objLookup!.Rows!.length; r++){
      Map<String, dynamic> map = objLookup!.Rows![r];

      Color rowColor = GlobalStyle.gridItemAlternateColor;

      if(r % 2 == 0){
        rowColor = Colors.white;
      }

      List<Widget> rowCells = [];
      for(int h = 0; h < objLookup!.Headers!.length; h++) {
        LookupHeaderDto objHeader = objLookup!.Headers![h];

        String strCellVal = "";
        bool isNum = false;
        if(objHeader.HeaderType == "Decimal"){
          if(objHeader.HeaderFormat.contains("Date") || objHeader.HeaderFormat.contains("Time")){
            isNum = false;
          }else{
            isNum = true;
          }

          double dblVal = double.tryParse(map[objHeader.HeaderName].toString()) ?? 0;
          switch(objHeader.HeaderFormat){
            case "Date":{
              strCellVal = CommonMethod.NumericToDateString(dblVal);
              break;
            }
            case "Time":{
              strCellVal = CommonMethod.NumericToTimeString(dblVal);
              break;
            }
            case "Time2":{
              strCellVal = CommonMethod.NumericToTimeString(dblVal, showSecond: true);
              break;
            }
            case "DateTime":{
              strCellVal = CommonMethod.NumericToDateTimeString(dblVal);
              break;
            }
            case "Unit":{
              strCellVal = CommonMethod.NumericToStringFormat(NumericType.Unit, dblVal);
              break;
            }
            case "Amount":{
              strCellVal = CommonMethod.NumericToStringFormat(NumericType.Amount, dblVal);
              break;
            }
            case "Percent":{
              strCellVal = CommonMethod.NumericToStringFormat(NumericType.Percent, dblVal);
              break;
            }
            case "Price":{
              strCellVal = CommonMethod.NumericToStringFormat(NumericType.Price, dblVal);
              break;
            }
            case "Quantity":{
              strCellVal = CommonMethod.NumericToStringFormat(NumericType.Quantity, dblVal);
              break;
            }
            case "Rate":{
              strCellVal = CommonMethod.NumericToStringFormat(NumericType.Rate, dblVal);
              break;
            }
            default:
              {
                strCellVal = CommonMethod.NumericToStringFormat(NumericType.Amount, dblVal);
                break;
              }
          }

        }
        else{
          strCellVal = map[objHeader.HeaderName].toString();
        }

        var rc = InkWell(
          onTap: () {
            if(widget.selectedRowCallback != null){
              widget.selectedRowCallback!(map);
            }
          },
          child: Container(
              padding: const EdgeInsets.only(left: 2, top: 5, right: 5, bottom: 5),
              alignment: isNum ? Alignment.centerRight : Alignment.centerLeft,
              child: Text(
                strCellVal,
                style: TextStyle(fontSize: GlobalStyle.fontSize, color: Colors.black),
              )
          ),
        );

        if(!hideColumns.contains(h)){
          rowCells.add(rc);
        }
      }

      var tr = TableRow(
        decoration: BoxDecoration(
          color: rowColor,
        ),
        children: rowCells,
      );

      tableRows.add(tr);
    }

    return Table(
      columnWidths: mapColWidth,
      children: tableRows,
    );
  }

  Widget lookupFooter(){
    return Container(
      width: double.infinity,
      height: 30,
      decoration: BoxDecoration(
          color: GlobalStyle.gridColor
      ),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        mainAxisAlignment: MainAxisAlignment.start,
        mainAxisSize: MainAxisSize.max,
        children: [
          InkWell(
            onTap: (){
              if(intPageNumber > 1) {
                setState((){
                  intPageNumber = 1;
                });
              }
            },
            child: Container(
              padding: const EdgeInsets.all(5),
              alignment: Alignment.center,
              child: Icon(Icons.first_page, color: GlobalStyle.gridFontColor,),
            ),
          ),
          InkWell(
            onTap: (){
              if(intPageNumber > 1) {
                setState((){
                  intPageNumber -= 1;
                });
              }
            },
            child: Container(
              padding: const EdgeInsets.all(5),
              alignment: Alignment.center,
              child: Icon(Icons.navigate_before, color: GlobalStyle.gridFontColor,),
            ),
          ),
          InkWell(
            onTap: (){
              if(intPageNumber < totalPage) {
                setState((){
                  intPageNumber += 1;
                });
              }
            },
            child: Container(
              padding: const EdgeInsets.all(5),
              alignment: Alignment.center,
              child: Icon(Icons.navigate_next, color: GlobalStyle.gridFontColor,),
            ),
          ),
          InkWell(
            onTap: (){
              if(intPageNumber < totalPage) {
                setState(() {
                  intPageNumber = totalPage;
                });
              }
            },
            child: Container(
              padding: const EdgeInsets.all(5),
              alignment: Alignment.center,
              child: Icon(Icons.last_page, color: GlobalStyle.gridFontColor,),
            ),
          ),
          Container(
            margin: const EdgeInsets.only(left: 5),
            child: Text(
              "Page ${totalPage > 0 ? intPageNumber : 0} of $totalPage",
              style: TextStyle(fontSize: GlobalStyle.fontSize, color: GlobalStyle.gridFontColor),
            ),
          ),
          Container(
            margin: const EdgeInsets.only(left: 5),
            child: Text(
              "Record ${totalRecord > 0 ? (((intPageNumber - 1) * intPageSize) + 1) : 0} - ${totalRecord > (intPageNumber * intPageSize) ? (intPageNumber * intPageSize) : totalRecord} of $totalRecord",
              style: TextStyle(fontSize: GlobalStyle.fontSize, color: GlobalStyle.gridFontColor),
            ),
          ),
        ],
      ),
    );
  }
}