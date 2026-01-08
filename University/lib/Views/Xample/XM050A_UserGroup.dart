import 'dart:async';

import '../../Common/GlobalStyle.dart';
import '../../UserControls/ComboBox.dart';
import 'package:flutter/material.dart' hide RadioGroup;
import '../../UserControls/Breadxcrumb.dart';
import '../../UserControls/DataListExtender.dart';
import '../../UserControls/FlatEditText.dart';
import '../../UserControls/SearchBox.dart';
import '../../Common/CommonMethod.dart';
import '../../Common/PageBase.dart';
import '../../Dao/Base/VariableDao.dart';
import '../../Dao/Zystem/ZUG1Dao.dart';
import '../../Dao/Zystem/ZUG2Dao.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Base/ModalPopupResult.dart';
import '../../Dto/Zystem/ZUG1Dto.dart';
import '../../Dto/Zystem/ZUG2Dto.dart';
import '../../UserControls/ButtonExtender.dart';
import '../../UserControls/CheckboxExtender.dart';
import '../../UserControls/DataGridExtender.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/Lookup.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/ModalDialog.dart';
import '../../UserControls/PageContent.dart';
import '../../UserControls/RadioGroup.dart';
import '../../UserControls/ToolbarBox.dart';
import '../../UserControls/DateBox.dart';
import 'XM050B_UserGroup.dart';

class XM050A_UserGroup extends StatefulWidget {
  static const String route = "/Xample/XM050A_UserGroup";

  @override
  createState() => XM050A_UserGroupState();
}

class XM050A_UserGroupState extends PageBase<XM050A_UserGroup> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  EditTextController edtZGUGNO = EditTextController();
  EditTextController edtZGUGNA = EditTextController();
  EditTextController edtZGREMA = EditTextController();
  CheckboxExtenderController chbZGRCST = CheckboxExtenderController();
  DataGridExtenderController dgeUGNO = DataGridExtenderController();
  DataListExtenderController dleUGNO = DataListExtenderController();
  DateController dtbDateFrom = DateController();
  DateController dtbDateTo = DateController();
  EditTextController edtZGUGNO01 = EditTextController();
  EditTextController edtZGUGNA01 = EditTextController();

  ButtonController btnAddLine = ButtonController();
  ButtonController btnDeleteLine = ButtonController();
  ButtonController btnTest = ButtonController();

  RadioGroupController rgpTestVertical = RadioGroupController();
  RadioGroupController rgpTestHorizontal = RadioGroupController();

  ComboBoxController cbxYear = ComboBoxController();
  ComboBoxController cbxMonth = ComboBoxController();

  List<SearchBoxController> lstScbUsnoController = [];
  String strS4RQBY = "";

  String strDFVL_ITCE = "";

  double dblGridWidth = 1000;
  //endregion

  //region Init
  @override
  void appInit(PagePlatform pagePlatform) {
    // CommonMethod.GetVariableValue("DFVL_ITCE").then((value) {
    //   strDFVL_ITCE = value;
    //   pageBehaviour(PageMode.Add);
    // });

    edtZGUGNO.text = "ACC";

    getData();
    //dgeUGNO.isRefresh = true;

    // setState(() {
    //
    // });
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    // TODO: implement pageBehaviour
    switch (pageMode) {
      case PageMode.Add:
        {
          setState(() {
            edtZGUGNO.text = "";
            edtZGUGNA.text = "";
            edtZGREMA.text = strDFVL_ITCE;
            chbZGRCST.isChecked = true;
            edtZGUGNO.isEnable = true;
            dgeUGNO.isRefresh = true;
          });

          break;
        }
      case PageMode.Edit:
        {
          setState(() {
            edtZGUGNO.isEnable = false;
            dgeUGNO.isRefresh = true;
          });
          break;
        }
      case PageMode.Copy:
        {
          break;
        }
      case PageMode.View:
        {
          setState(() {});
          break;
        }
    }
  }
  //endregion

  //region Events
  void tlbNew_Click() {
    pageBehaviour(PageMode.Add);
  }

  void tlbSave_Click() async {
    if (form1.currentState!.validate()) {
      DialogResult? dlgResult = await MessageBox.show(
          context: context,
          title: "Save Detail",
          message: "Do you want to save selected detail?",
          dialogButton: DialogButton.OkCancel);

      String strResult = "";

      if (strResult.isEmpty) {
        showModalProgress.value = true;
        try {
          ZUG1Dto objInfo = collectionInfo();
          List<ZUG2Dto>? lstT = dgeUGNO.gridItem
              .map((e) => ZUG2Dto.fromJson(e))
              .toList();
          //objInfo.listZUG2 = lstT;

          ZUG1Dao dao = ZUG1Dao();
          strResult = await dao.Save(objInfo);
        } catch (e) {
          strResult = e.toString();
        }

        showModalProgress.value = false;

        if (strResult.isEmpty) {
          await MessageBox.show(
              context: context,
              message: "Save successfully",
              title: "Save Success",
              dialogButton: DialogButton.OK);

          setState(() {
            dgeUGNO.isRefresh = true;
          });
        } else {
          await MessageBox.show(
              context: context,
              message: strResult,
              title: "Save Failed",
              dialogButton: DialogButton.OK);
        }
      } else {
        await MessageBox.show(
            context: context,
            message: strResult,
            title: "Save Failed",
            dialogButton: DialogButton.OK);
      }
    }
  }

  void edtZGUGNO_LostFocus() async {
    if (edtZGUGNO.text.isNotEmpty) {
      getData();
    }
  }

  void btnAddLine_Click() async {
    if (form1.currentState!.validate()) {
      ZUG2Dto? objZUG2;

      ModalPopupResult? popupResult = await ModalDialog.show(
        context: context,
        title: "Add User",
        child: XM050B_UserGroup(
          objZUG1: collectionInfo(),
          callback: (obj) {
            //opsional
            objZUG2 = obj;
          },
        ),
      );

      if (popupResult!.dialogResult == DialogResult.OK) {
        showModalProgress.value = true;

        setState(() {
          dgeUGNO.isRefresh = true;
        });

        showModalProgress.value = false;
      }
    }
  }

  void btnDeleteLine_Click() async {
    if (form1.currentState!.validate()) {
      DialogResult? dlgResult = await MessageBox.show(
          context: context,
          title: "Delete Detail",
          message: "Do you want to delete selected detail?",
          dialogButton: DialogButton.OkCancel);
      if (dlgResult == DialogResult.OK) {
        String strResult = "";

        showModalProgress.value = true;

        try {
          List<ZUG2Dto> lstInfo = [];
          List<ZUG2Dto>? lstT = dgeUGNO.gridItem
              .map((e) => ZUG2Dto.fromJson(e))
              .toList();

          if(lstT.isNotEmpty) {
            for (int i = 0; i < lstT.length; i++) {
              ZUG2Dto objT = lstT[i];
              if (objT.IsSelected) {
                lstInfo.add(objT);
              }
            }
          }

          if(lstInfo.isNotEmpty){
            strResult = "${lstInfo.length} selected";
          }else{
            strResult = "Please select line";
          }
        } catch (ex) {
          strResult = ex.toString();
        }

        showModalProgress.value = false;

        if (strResult.isEmpty) {
          await MessageBox.show(
              context: context,
              message: "Delete Detail successfully",
              title: "Delete Detail Success",
              dialogButton: DialogButton.OK);

          setState(() {
            dgeUGNO.isRefresh = true;
          });
        } else {
          await MessageBox.show(
              context: context,
              message: strResult,
              title: "Delete Detail Failed",
              dialogButton: DialogButton.OK);
        }
      }
    }
  }

  void btnTest_Click() {
    setState(() {
      if(dblGridWidth == 5000){
        dblGridWidth = 1000;
      }else {
        dblGridWidth = 5000;
      }
    });
  }
  //endregion

  //region Methods
  void getData() async {
    String strResult = "";

    showModalProgress.value = true;

    try {
      ZUG1Dto objInfo = collectionInfo();
      ZUG1Dao dao = ZUG1Dao();
      ZUG1Dto? obj = await dao.oneData(objInfo);
      if(obj != null){
        setState(() {
          edtZGUGNA.text = obj.ZGUGNA;
          edtZGREMA.text = obj.ZGREMA;
          chbZGRCST.isChecked = (obj.ZGRCST == 1 ? true : false);
        });

        pageBehaviour(PageMode.Edit);
      }
    } catch (ex) {
      strResult = ex.toString();
    }

    if (strResult.isNotEmpty) {
      await MessageBox.show(
          context: context, message: strResult, title: "Get Data");
    }else{
      setState(() {
        dgeUGNO.isRefresh = true;
      });
    }
  }

  Future<dynamic> getListUSNO(intPageNumber, intPageSize, strSqlFilter, strSqlSort) async {
    debugPrint("getListUSNO");
    debugPrint("Where $strSqlFilter");
    debugPrint("Order By $strSqlSort");

    List<dynamic>? lst;
    if(edtZGUGNO.text.isNotEmpty) {
      ZUG2Dto objInfoG2 = ZUG2Dto(
        ZHUGNO: edtZGUGNO.text,
        PageNumber: intPageNumber,
        PageSize: intPageSize,
        SqlFilter: strSqlFilter,
        SqlSort: strSqlSort,
      );

      ZUG2Dao daoZG2 = ZUG2Dao();
      // lst = await daoZG2.listPaging(objInfoG2);
      lst = await daoZG2.tablePaging(objInfoG2);
    }
    //Clear controller if grid rows changing
    // debugPrint("clear scb");
    // lstScbUsnoController.clear();
    return lst;
  }

  ZUG1Dto collectionInfo() {
    ZUG1Dto objInfo = ZUG1Dto();
    objInfo.ZGCONO = "";
    objInfo.ZGBRNO = "";
    objInfo.ZGUGNO = edtZGUGNO.text.trim();
    objInfo.ZGUGNA = edtZGUGNA.text.trim();
    objInfo.ZGREMA = edtZGREMA.text.trim();
    objInfo.ZGRCST = chbZGRCST.isChecked ? 1 : 0;
    objInfo.ZGCRUS = GlobalDto.USNO;
    objInfo.ZGCHUS = GlobalDto.USNO;
    objInfo.ZGBRNO = strDFVL_ITCE;
    return objInfo;
  }

  //endregion

  //region Layout
  @override
  Widget build(BuildContext context) {
    return PageContent(
      formKey: form1,
      showModalProgress: showModalProgress,
      toolbar: Breadxcrumb(
        crumbItems: [
          CrumbItem(label: "User Group", onTap: (){ navigateTo(XM050A_UserGroup.route); })
        ],
      ),
      // toolbar: ToolbarBox(
      //   toolbarBoxMode: ToolbarBoxMode.All,
      //   onNew: tlbNew_Click,
      //   onSave: tlbSave_Click,
      //   isNextEnable: false,
      //   isApproveEnable: false,
      //   isReviseEnable: false,
      //   isRejectEnable: false,
      //   listEntity: "UGNO-01",
      //   listTitle: "List of User Group",
      //   listHideColumns: const [],
      //   listOnSelected: (map) {
      //     setState(() {
      //       edtZGUGNO.text = map[map.keys.elementAt(0)] ?? "";
      //     });
      //     edtZGUGNO_LostFocus();
      //   },
      // ),
      builder: (context, constraints) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            Table(
              defaultVerticalAlignment: TableCellVerticalAlignment.top,
              columnWidths: const {
                0: FixedColumnWidth(165),
                1: FixedColumnWidth(350),
                2: FixedColumnWidth(20),
                3: FixedColumnWidth(180),
                4: FixedColumnWidth(500),
              },
              children: [
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "UGNO",
                        isMandatory: true,
                      ),
                      EditText(
                        controller: edtZGUGNO,
                        textMode: TextInputType.text,
                        isMandatory: true,
                        //onLostFocus: edtZGUGNO_LostFocus,
                        maxLength: 10,
                      ),
                      Container(),
                      const LabelText(
                        labelText: "REMA",
                      ),
                      EditText(
                        controller: edtZGREMA,
                        textMode: TextInputType.multiline,
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "UGNA",
                        isMandatory: true,
                      ),
                      EditText(
                        controller: edtZGUGNA,
                        textMode: TextInputType.text,
                        isMandatory: true,
                        maxLength: 60,
                      ),
                      Container(),
                      const LabelText(
                        labelText: "Radio Group Horizontal",
                      ),
                      RadioGroup(
                        controller: rgpTestHorizontal,

                        direction: Direction.Horizontal,
                        onChange: (rowItem) {
                          strS4RQBY = rowItem;
                        },
                        radioGroupList: [
                          RadioListChild(name: "All", value: "2"),
                          RadioListChild(name: "Active", value: "1"),
                          RadioListChild(name: "Inactive", value: "0"),
                        ],
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "RCST",
                      ),
                      CheckboxExtender(
                        controller: chbZGRCST,
                        label: "Active",
                        isChecked: true,
                      ),
                      Container(),
                      const LabelText(
                        labelText: "Radio Group Vertical",
                      ),
                      RadioGroup(
                        controller: rgpTestVertical,
                        direction: Direction.Vertical,
                        initialValue: "Y",
                        radioGroupList: [
                          RadioListChild(
                            name: "Year",
                            value: "Y",
                            child: ComboBox(
                              controller: cbxYear,
                              comboBoxType: ComboBoxType.Year,
                            ),
                            childPosition: ChildPosition.Right,
                          ),
                          RadioListChild(
                            name: "Month",
                            value: "M",
                            child: ComboBox(
                              controller: cbxMonth,
                              comboBoxType: ComboBoxType.Month,
                            ),
                            childPosition: ChildPosition.Bottom,
                          ),
                          RadioListChild(
                            name: "Day",
                            value: "D"
                          ),
                        ],
                        onChange: (value) {
                          setState(() {
                            // debugPrint("RadioGroup - onChange value $value");
                            switch(value){
                              case "A":{
                                btnAddLine.isEnable = false;
                                btnDeleteLine.isEnable = false;
                                btnTest.isEnable = false;
                                edtZGUGNA.isEnable = false;
                                strDFVL_ITCE = value;

                                break;
                              }
                              case "N":{
                                btnAddLine.isEnable = true;
                                btnDeleteLine.isEnable = false;
                                btnTest.isEnable = false;
                                edtZGUGNA.isEnable = false;
                                strDFVL_ITCE = value;

                                break;
                              }
                              case "S":{
                                edtZGUGNA.isEnable = true;
                                btnDeleteLine.isEnable = true;
                                btnTest.isEnable = true;
                                strDFVL_ITCE = value;

                                break;
                              }
                            }
                            //btnTest.isEnable = !btnTest.isEnable;
                          });

                        },
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "Date Range",
                      ),
                      Row(
                        children: [
                          DateBox(
                            controller: dtbDateFrom,
                          ),
                          Container(
                            margin: const EdgeInsets.only(left:5),
                            child: const LabelText(
                              labelText: "to",
                              width: 20,
                            ),
                          ),
                          DateBox(
                            controller: dtbDateTo,
                          ),
                        ],
                      ),
                      Container(),
                      Container(),
                      Container(),
                    ]
                ),
              ],
            ),
            DataGridExtender(
              controller: dgeUGNO,
              futureData: getListUSNO,
              title: "User",
              // isFreezeHeader: true,
              autoGeneratedColumns: true,
              //width: double.infinity,
              // onAddLine: btnAddLine_Click,
              // onDeleteLine: btnDeleteLine_Click,
              // isAddLineVisible: true,
              // isDeleteLineVisible: true,
              // isFilterVisible: true,
              // isRefreshOnFilter: true,
              isSubTotalVisible: true,
              height: 400,
              // isListView: true,
              onAfterRefresh: (){
                showModalProgress.value = false;
              },
              popupMenuItems: const [
                PopupMenuItem(child: Text("Action 1"),),
                PopupMenuItem(child: Text("Action 2")),
                PopupMenuItem(child: Text("Action 3")),
              ],
              // listViewItemBuilder: (rowItem){
              //   return Column(
              //       children: [
              //         Row(
              //             children: [
              //               LabelText(
              //                 labelText: "UGNO",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHUGNO"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //             ]
              //         ),
              //         Row(
              //           children: [
              //             LabelText(
              //               labelText: "USNO",
              //               labelColor: GlobalStyle.listItemTextColor,
              //             ),
              //             LabelText(
              //               labelText: rowItem["ZHUSNO"].toString(),
              //               labelColor: GlobalStyle.listItemTextColor,
              //             ),
              //             LabelText(
              //               labelText: "USNA",
              //               labelColor: GlobalStyle.listItemTextColor,
              //             ),
              //             LabelText(
              //               labelText: rowItem["ZUUSNA"].toString(),
              //               labelColor: GlobalStyle.listItemTextColor,
              //             ),
              //           ]
              //         ),
              //         Row(
              //             children: [
              //               LabelText(
              //                 labelText: "CRDT",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHCRDT"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: "CHDT",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHCHDT"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //             ]
              //         ),
              //         Row(
              //             children: [
              //               LabelText(
              //                 labelText: "CRTM",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHCRTM"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: "CHTM",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHCHTM"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //             ]
              //         ),
              //
              //       ]
              //   );
              //
              // },
              // autoGeneratedColumnStyle:[
              //   DEColumnStyle(
              //     columnName: "ZHCRDT",
              //     columnType: DEColumnType.Numeric,
              //     columnFormat: DENumericFormat.Date
              //     // columnWidth: 240,
              //   ),
              //   DEColumnStyle(
              //       columnName: "ZHCRTM",
              //       columnType: DEColumnType.Numeric,
              //       columnFormat: DENumericFormat.Time
              //     // columnWidth: 240,
              //   ),
              // ],
              deColumns: [
                // DEColumn(
                //   columnName: "IsSelected",
                //   columnType: DEColumnType.DECheckbox,
                //   onSelected: (rowItem) {
                //     if(rowItem.containsKey("IsSelected")){
                //       //if false than selection is happen at checkbox row
                //       debugPrint("IsSelected ${rowItem["IsSelected"] as bool}");
                //       if(rowItem["IsSelected"] as bool){
                //         //do something if true
                //       }else{
                //         //do something if false
                //       }
                //     }else{
                //       //if true than selection is happen at checkbox header
                //     }
                //
                //     //rowItem["IsSelectedAll"] will always have value true / false
                //     debugPrint("IsSelectedAll ${rowItem["IsSelectedAll"] as bool}");
                //     if(rowItem["IsSelectedAll"] as bool){
                //       //do something if true
                //     }else{
                //       //do something if false
                //     }
                //   },
                // ),
                // DEColumn(
                //   headerText: "ZHUSNO",
                //   columnName: "ZHUSNO",
                //   columnType: DEColumnType.String,
                //   columnWidth: 140,
                // ),
                // DEColumn(
                //   headerText: "Nama",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   // columnWidth: 240,
                // ),
                // DEColumn(
                //   headerText: "Nama WT1",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   columnWidth: 200,
                // ),
                // DEColumn(
                //   headerText: "Nama WT1",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   columnWidth: 200,
                // ),
                // DEColumn(
                //   headerText: "Nama WT1",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   columnWidth: 200,
                // ),
                // DEColumn(
                //   headerText: "Nama WT1",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   columnWidth: 200,
                // ),
                // DEColumn(
                //   headerText: "User",
                //   columnName: "ZHUSNO",
                //   columnType: DEColumnType.Widget,
                //   columnWidth: 370,
                //   cellBuilder: (rowIndex, rowItem) {
                //     EditTextController edtDEController = EditTextController();
                //     // edtDEController.text = rowItem["ZHUSNO"];
                //     edtDEController.isEnable = true;
                //
                //     // if (edtDEController.text.isNotEmpty) {
                //     //   edtDEController.isEnable = false;
                //     // } else {
                //     //   edtDEController.isEnable = true;
                //     // }
                //
                //     return EditText(
                //       controller: edtDEController,
                //       textMode: TextInputType.text,
                //       onLostFocus: () {
                //         rowItem["ZHUSNO"] = edtDEController.text.replaceAll(r"\,", "");
                //         dgeUGNO.gridItem[rowIndex] = rowItem;
                //       },
                //     );
                //   },
                // ),
                // DEColumn(
                //   headerText: "User",
                //   columnName: "ZHUSNO",
                //   columnType: DEColumnType.Widget,
                //   columnWidth: 370,
                //   cellBuilder: (rowIndex, rowItem) {
                //
                //     SearchBoxController scbUsno = SearchBoxController();
                //     scbUsno.text = rowItem["ZHUSNO"] ?? "";
                //
                //     // if(!lstScbUsnoController.asMap().containsKey(rowIndex)){
                //     //   debugPrint("add scb");
                //     //   SearchBoxController scbUsnoController = SearchBoxController();
                //     //   scbUsnoController.text = rowItem["ZHUSNO"] ?? "";
                //     //
                //     //   lstScbUsnoController.add(scbUsnoController);
                //     // }
                //
                //     return SearchBoxFormField(
                //       controller: scbUsno,
                //       // controller: lstScbUsnoController[rowIndex],
                //       title: "List Of User",
                //       entity: "USNO-01",
                //       onLostFocus: (item) {
                //         setState(() {
                //           rowItem["ZHUSNO"] = item["ZHUSNO"];
                //           dgeUGNO.gridItem[rowIndex] = rowItem;
                //         });
                //       },
                //     );
                //   },
                // ),
                // DEColumn(
                //   headerText: "Nickname",
                //   columnName: "ZUNICK",
                //   columnType: DEColumnType.String,
                //   columnWidth: 140,
                // ),
                // DEColumn(
                //   columnType: DEColumnType.DEMoreMenu,
                //   popupMenuItems: [
                //     PopupMenuItem(child: Text("Action 1"),),
                //     PopupMenuItem(child: Text("Action 2")),
                //     PopupMenuItem(child: Text("Action 3")),
                //   ],
                // ),
              ],
            ),
            Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                ButtonExtender(
                  controller: btnAddLine,
                  buttonText: "Add",
                  onPressed: btnAddLine_Click,
                ),
                Container(
                  margin: const EdgeInsets.only(left: 10),
                  child: ButtonExtender(
                    controller: btnDeleteLine,
                    buttonText: "Delete",
                    onPressed: btnDeleteLine_Click,
                  ),
                ),
                Container(
                  margin: const EdgeInsets.only(left: 10),
                  child: ButtonExtender(
                    controller: btnTest,
                    buttonText: "Test",
                    onPressed: btnTest_Click,
                  ),
                ),
              ],
            ),
          ],

        );
      },
      builderPhone: (context, constraints) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            Table(
              defaultVerticalAlignment: TableCellVerticalAlignment.top,
              columnWidths: const {
                0: FixedColumnWidth(165),
                1: FixedColumnWidth(350),
              },
              children: [
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "UGNO",
                        isMandatory: true,
                      ),
                      EditText(
                        controller: edtZGUGNO,
                        textMode: TextInputType.text,
                        isMandatory: true,
                        //onLostFocus: edtZGUGNO_LostFocus,
                        maxLength: 10,
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "REMA",
                      ),
                      EditText(
                        controller: edtZGREMA,
                        textMode: TextInputType.multiline,
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "Radio Group Horizontal",
                      ),
                      RadioGroup(
                        controller: rgpTestHorizontal,

                        direction: Direction.Horizontal,
                        onChange: (rowItem) {
                          strS4RQBY = rowItem;
                        },
                        radioGroupList: [
                          RadioListChild(name: "All", value: "2"),
                          RadioListChild(name: "Active", value: "1"),
                          RadioListChild(name: "Inactive", value: "0"),
                        ],
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "UGNA",
                        isMandatory: true,
                      ),
                      EditText(
                        controller: edtZGUGNA,
                        textMode: TextInputType.text,
                        isMandatory: true,
                        maxLength: 60,
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "RCST",
                      ),
                      CheckboxExtender(
                        controller: chbZGRCST,
                        label: "Active",
                        isChecked: true,
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "Radio Group Vertical",
                      ),
                      RadioGroup(
                        controller: rgpTestVertical,
                        direction: Direction.Vertical,
                        initialValue: "Y",
                        radioGroupList: [
                          RadioListChild(
                            name: "Year",
                            value: "Y",
                            child: ComboBox(
                              controller: cbxYear,
                              comboBoxType: ComboBoxType.Year,
                            ),
                            childPosition: ChildPosition.Right,
                          ),
                          RadioListChild(
                            name: "Month",
                            value: "M",
                            child: ComboBox(
                              controller: cbxMonth,
                              comboBoxType: ComboBoxType.Month,
                            ),
                            childPosition: ChildPosition.Bottom,
                          ),
                          RadioListChild(
                              name: "Day",
                              value: "D"
                          ),
                        ],
                        onChange: (value) {
                          setState(() {
                            // debugPrint("RadioGroup - onChange value $value");
                            switch(value){
                              case "A":{
                                btnAddLine.isEnable = false;
                                btnDeleteLine.isEnable = false;
                                btnTest.isEnable = false;
                                edtZGUGNA.isEnable = false;
                                strDFVL_ITCE = value;

                                break;
                              }
                              case "N":{
                                btnAddLine.isEnable = true;
                                btnDeleteLine.isEnable = false;
                                btnTest.isEnable = false;
                                edtZGUGNA.isEnable = false;
                                strDFVL_ITCE = value;

                                break;
                              }
                              case "S":{
                                edtZGUGNA.isEnable = true;
                                btnDeleteLine.isEnable = true;
                                btnTest.isEnable = true;
                                strDFVL_ITCE = value;

                                break;
                              }
                            }
                            //btnTest.isEnable = !btnTest.isEnable;
                          });

                        },
                      ),
                    ]
                ),
                TableRow(
                    children: [
                      const LabelText(
                        labelText: "Date Range",
                      ),
                      Row(
                        children: [
                          DateBox(
                            controller: dtbDateFrom,
                          ),
                          Container(
                            margin: const EdgeInsets.only(left:5),
                            child: const LabelText(
                              labelText: "to",
                              width: 20,
                            ),
                          ),
                          DateBox(
                            controller: dtbDateTo,
                          ),
                        ],
                      ),
                    ]
                ),
              ],
            ),
            DataListExtender(
              controller: dleUGNO,
              futureData: getListUSNO,
              title: "User",
              // isFreezeHeader: true,
              autoGeneratedColumns: true,
              //width: double.infinity,
              // onAddLine: btnAddLine_Click,
              // onDeleteLine: btnDeleteLine_Click,
              // isAddLineVisible: true,
              // isDeleteLineVisible: true,
              // isFilterVisible: true,
              // isRefreshOnFilter: true,
              isSubTotalVisible: true,
              height: 400,
              // isListView: true,
              onAfterRefresh: (){
                showModalProgress.value = false;
              },
              popupMenuItems: const [
                PopupMenuItem(child: Text("Action 1"),),
                PopupMenuItem(child: Text("Action 2")),
                PopupMenuItem(child: Text("Action 3")),
              ],
              // listViewItemBuilder: (rowItem){
              //   return Column(
              //       children: [
              //         Row(
              //             children: [
              //               LabelText(
              //                 labelText: "UGNO",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHUGNO"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //             ]
              //         ),
              //         Row(
              //           children: [
              //             LabelText(
              //               labelText: "USNO",
              //               labelColor: GlobalStyle.listItemTextColor,
              //             ),
              //             LabelText(
              //               labelText: rowItem["ZHUSNO"].toString(),
              //               labelColor: GlobalStyle.listItemTextColor,
              //             ),
              //             LabelText(
              //               labelText: "USNA",
              //               labelColor: GlobalStyle.listItemTextColor,
              //             ),
              //             LabelText(
              //               labelText: rowItem["ZUUSNA"].toString(),
              //               labelColor: GlobalStyle.listItemTextColor,
              //             ),
              //           ]
              //         ),
              //         Row(
              //             children: [
              //               LabelText(
              //                 labelText: "CRDT",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHCRDT"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: "CHDT",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHCHDT"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //             ]
              //         ),
              //         Row(
              //             children: [
              //               LabelText(
              //                 labelText: "CRTM",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHCRTM"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: "CHTM",
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //               LabelText(
              //                 labelText: rowItem["ZHCHTM"].toString(),
              //                 labelColor: GlobalStyle.listItemTextColor,
              //               ),
              //             ]
              //         ),
              //
              //       ]
              //   );
              //
              // },
              // autoGeneratedColumnStyle:[
              //   DEColumnStyle(
              //       columnName: "ZHCRDT",
              //       columnType: DEColumnType.Numeric,
              //       columnFormat: DENumericFormat.Date
              //     // columnWidth: 240,
              //   ),
              //   DEColumnStyle(
              //       columnName: "ZHCRTM",
              //       columnType: DEColumnType.Numeric,
              //       columnFormat: DENumericFormat.Time
              //     // columnWidth: 240,
              //   ),
              // ],
              deColumns: [
                // DEColumn(
                //   columnName: "IsSelected",
                //   columnType: DEColumnType.DECheckbox,
                //   onSelected: (rowItem) {
                //     if(rowItem.containsKey("IsSelected")){
                //       //if false than selection is happen at checkbox row
                //       debugPrint("IsSelected ${rowItem["IsSelected"] as bool}");
                //       if(rowItem["IsSelected"] as bool){
                //         //do something if true
                //       }else{
                //         //do something if false
                //       }
                //     }else{
                //       //if true than selection is happen at checkbox header
                //     }
                //
                //     //rowItem["IsSelectedAll"] will always have value true / false
                //     debugPrint("IsSelectedAll ${rowItem["IsSelectedAll"] as bool}");
                //     if(rowItem["IsSelectedAll"] as bool){
                //       //do something if true
                //     }else{
                //       //do something if false
                //     }
                //   },
                // ),
                // DEColumn(
                //   headerText: "ZHUSNO",
                //   columnName: "ZHUSNO",
                //   columnType: DEColumnType.String,
                //   columnWidth: 140,
                // ),
                // DEColumn(
                //   headerText: "Nama",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   // columnWidth: 240,
                // ),
                // DEColumn(
                //   headerText: "Nama WT1",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   columnWidth: 200,
                // ),
                // DEColumn(
                //   headerText: "Nama WT1",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   columnWidth: 200,
                // ),
                // DEColumn(
                //   headerText: "Nama WT1",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   columnWidth: 200,
                // ),
                // DEColumn(
                //   headerText: "Nama WT1",
                //   columnName: "ZUUSNA",
                //   columnType: DEColumnType.String,
                //   columnWidth: 200,
                // ),
                // DEColumn(
                //   headerText: "User",
                //   columnName: "ZHUSNO",
                //   columnType: DEColumnType.Widget,
                //   columnWidth: 370,
                //   cellBuilder: (rowIndex, rowItem) {
                //     EditTextController edtDEController = EditTextController();
                //     // edtDEController.text = rowItem["ZHUSNO"];
                //     edtDEController.isEnable = true;
                //
                //     // if (edtDEController.text.isNotEmpty) {
                //     //   edtDEController.isEnable = false;
                //     // } else {
                //     //   edtDEController.isEnable = true;
                //     // }
                //
                //     return EditText(
                //       controller: edtDEController,
                //       textMode: TextInputType.text,
                //       onLostFocus: () {
                //         rowItem["ZHUSNO"] = edtDEController.text.replaceAll(r"\,", "");
                //         dgeUGNO.gridItem[rowIndex] = rowItem;
                //       },
                //     );
                //   },
                // ),
                // DEColumn(
                //   headerText: "User",
                //   columnName: "ZHUSNO",
                //   columnType: DEColumnType.Widget,
                //   columnWidth: 370,
                //   cellBuilder: (rowIndex, rowItem) {
                //
                //     SearchBoxController scbUsno = SearchBoxController();
                //     scbUsno.text = rowItem["ZHUSNO"] ?? "";
                //
                //     // if(!lstScbUsnoController.asMap().containsKey(rowIndex)){
                //     //   debugPrint("add scb");
                //     //   SearchBoxController scbUsnoController = SearchBoxController();
                //     //   scbUsnoController.text = rowItem["ZHUSNO"] ?? "";
                //     //
                //     //   lstScbUsnoController.add(scbUsnoController);
                //     // }
                //
                //     return SearchBoxFormField(
                //       controller: scbUsno,
                //       // controller: lstScbUsnoController[rowIndex],
                //       title: "List Of User",
                //       entity: "USNO-01",
                //       onLostFocus: (item) {
                //         setState(() {
                //           rowItem["ZHUSNO"] = item["ZHUSNO"];
                //           dgeUGNO.gridItem[rowIndex] = rowItem;
                //         });
                //       },
                //     );
                //   },
                // ),
                // DEColumn(
                //   headerText: "Nickname",
                //   columnName: "ZUNICK",
                //   columnType: DEColumnType.String,
                //   columnWidth: 140,
                // ),
                // DEColumn(
                //   columnType: DEColumnType.DEMoreMenu,
                //   popupMenuItems: [
                //     PopupMenuItem(child: Text("Action 1"),),
                //     PopupMenuItem(child: Text("Action 2")),
                //     PopupMenuItem(child: Text("Action 3")),
                //   ],
                // ),
              ],
            ),
            Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                ButtonExtender(
                  controller: btnAddLine,
                  buttonText: "Add",
                  onPressed: btnAddLine_Click,
                ),
                Container(
                  margin: const EdgeInsets.only(left: 10),
                  child: ButtonExtender(
                    controller: btnDeleteLine,
                    buttonText: "Delete",
                    onPressed: btnDeleteLine_Click,
                  ),
                ),
                Container(
                  margin: const EdgeInsets.only(left: 10),
                  child: ButtonExtender(
                    controller: btnTest,
                    buttonText: "Test",
                    onPressed: btnTest_Click,
                  ),
                ),
              ],
            ),
          ],

        );
      },
    );
  }
}
