import '../../UserControls/Multiselect.dart';
import 'package:flutter/material.dart';
import '../../UserControls/TooltipButton.dart';
import '../../UserControls/SearchBox.dart';
import '../../UserControls/TabbarExtender.dart';
import '../../UserControls/ButtonExtender.dart';
import '../../UserControls/ComboBox.dart';
import '../../UserControls/Lookup.dart';
import "../../UserControls/ModalProgressExtender.dart";
import '../../Common/PageBase.dart';
import '../../Dao/Zystem/ZAPPDao.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Zystem/ZAPPDto.dart';
import '../../UserControls/CheckboxExtender.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/PageContent.dart';
import '../../UserControls/TimeBox.dart';
import '../../UserControls/ToolbarBox.dart';
import '../../UserControls/UploadBox.dart';
import '../../UserControls/DateBox.dart';

class XM010A_Application extends StatefulWidget {
  static const String route = "/Xample/XM010A_Application";

  @override
  createState() => XM010A_ApplicationState();
}

class XM010A_ApplicationState extends PageBase<XM010A_Application> {
  //region Variables
  final form1 = GlobalKey<FormState>();

  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  EditTextController edtZAAPNO = EditTextController();
  EditTextController edtZAAPNA = EditTextController();
  EditTextController edtZAAURL = EditTextController();
  EditTextController edtZAIURL = EditTextController();
  EditTextController edtZAREMA = EditTextController();
  CheckboxExtenderController chbZARCST = CheckboxExtenderController();
  UploadBoxController upbFile = UploadBoxController();
  LookupController lupApno = LookupController();
  ToolbarBoxController tlbPage = ToolbarBoxController();
  SearchBoxController scbApno = SearchBoxController();
  TimeController tmbTime = TimeController();

  var dtbDateTest = DateController();
  var cbxComboTest1 = ComboBoxController();
  var cbxComboTest2 = ComboBoxController();

  var mtsMultiselectTest1 = MultiselectController();
  var mtsMultiselectTest2 = MultiselectController();

  int t1 = 0;
  int t2 = 0;
  int t3 = 0;

  bool isTab1Visible = true;
  bool isTab2Visible = true;
  bool isTab3Visible = true;

  List<TabbarItem> tabItems = [];
  //endregion

  //region Load
  @override
  void appInit(PagePlatform pagePlatform) {
    // TODO: implement appInit
    lupApno.text = "AGLiS";
    mtsMultiselectTest1.reset();
    // cbxComboTest.value = "2022";
    // lupApno.isEnable = false;
    // cbxComboTest.value = "AGLiS";
    // cbxComboTest.isEnable = false;

    cbxComboTest1.filter = "HWCONO = '${GlobalDto.CONO}' AND HWBRNO = '${GlobalDto.BRNO}'";
    cbxComboTest1.isRefresh = true;

    tabItems = [
      TabbarItem(
        title: "Tab 1",
        isVisible: isTab1Visible,
        content: builderTab1(),
      ),
      TabbarItem(
        title: "Tab 2",
        isVisible: isTab2Visible,
        content: builderTab2(),
      ),
      TabbarItem(
        title: "Tab 3",
        isVisible: isTab3Visible,
        content: const Text("Content 3"),
        isEnable: false,
      ),
    ];

    // for(int i = 4; i < 20; i++){
    //   tabItems.add(
    //     TabbarItem(
    //       title: "Tab $i",
    //       content: Text("Content $i"),
    //     ),
    //   );
    // }
  }

  //endregion

  //region Events
  void tlbNew_Click() {
    pageBehaviour(PageMode.Add);
  }

  void tlbSave_Click() async {
    if (form1.currentState!.validate()) {
      String strResult = "";

      // if (strResult.isEmpty) {
      //   setState(() {
      //     showModalProgress = true;
      //   });
      //   try {
      //     ZAPPDto objInfo = collectionInfo();
      //     ZAPPDao dao = ZAPPDao();
      //     strResult = await dao.Save(objInfo);
      //   } catch (e) {
      //     strResult = e.toString();
      //   }
      //
      //   setState(() {
      //     showModalProgress = false;
      //   });
      //
      //   if (strResult.isEmpty) {
      //     await MessageBox.show(
      //         context: context,
      //         message: "Save successfully",
      //         title: "Save Success",
      //         dialogButton: DialogButton.OK);
      //   } else {
      //     await MessageBox.show(
      //         context: context,
      //         message: strResult,
      //         title: "Save Failed",
      //         dialogButton: DialogButton.OK);
      //   }
      // } else {
      //   await MessageBox.show(
      //       context: context,
      //       message: strResult,
      //       title: "Save Failed",
      //       dialogButton: DialogButton.OK);
      // }
    }
  }

  void edtZAAPNO_LostFocus() async {
    if(edtZAAPNO.text.isNotEmpty) {
      getData();
    }
  }

  //endregion

  //region Methods
  void getData() async {
    String strResult = "";

    showModalProgress.value = true;

    try{
      ZAPPDto objInfo = collectionInfo();
      ZAPPDao dao = ZAPPDao();
      ZAPPDto? obj = await dao.oneData(objInfo);
      if(obj != null)
      {
        setState(() {
          edtZAAPNO.text = obj.ZAAPNO;
          edtZAAPNA.text = obj.ZAAPNA;
          edtZAAURL.text = obj.ZAAURL;
          edtZAIURL.text = obj.ZAIURL;
          edtZAREMA.text = obj.ZAREMA;
          chbZARCST.isChecked = (obj.ZARCST == 1 ? true : false);
        });
        pageBehaviour(PageMode.Edit);
      }
    }catch(ex){
      strResult = ex.toString();
    }

    showModalProgress.value = false;

    if(strResult.isNotEmpty){
      await MessageBox.show(context: context, message: strResult, title: "Get Data");
    }
  }

  ZAPPDto collectionInfo() {
    ZAPPDto objInfo = ZAPPDto();
    objInfo.ZACONO = "";
    objInfo.ZABRNO = "";
    objInfo.ZAAPNO = edtZAAPNO.text.trim();
    objInfo.ZAAPNA = edtZAAPNA.text.trim();
    objInfo.ZAAURL = edtZAAURL.text.trim();
    objInfo.ZAIURL = edtZAIURL.text.trim();
    objInfo.ZAREMA = edtZAREMA.text.trim();
    objInfo.ZARCST = chbZARCST.isChecked ? 1 : 0;
    objInfo.ZACRUS = GlobalDto.USNO;
    objInfo.ZACHUS = GlobalDto.USNO;
    return objInfo;
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    // TODO: implement pageBehaviour
    switch (pageMode) {
      case PageMode.Add:
        {
          setState(() {
            edtZAAPNO.text = "";
            edtZAAPNA.text = "";
            edtZAAURL.text = "";
            edtZAIURL.text = "";
            edtZAREMA.text = "";
            chbZARCST.isChecked = true;
            edtZAAPNO.isEnable = true;
          });

          break;
        }
      case PageMode.Edit:
        {
          setState(() {
            edtZAAPNO.isEnable = false;
          });
          break;
        }
      case PageMode.Copy:
        {
          break;
        }
      case PageMode.View:
        {
          break;
        }
    }
  }

  //endregion

  //region Layout
  @override
  Widget build(BuildContext context) {
    return PageContent(
      formKey: form1,
      showModalProgress: showModalProgress,
      toolbar: ToolbarBox(
        controller: tlbPage,
        toolbarBoxMode: ToolbarBoxMode.Master,
        onNew: tlbNew_Click,
        onSave: tlbSave_Click,
        listEntity: "APNO-01",
        listTitle: "List of Application",
        listOnSelected: (map) {
          edtZAAPNO.text = map["Code"] ?? "";
          getData();
        },
        isBackVisible: true,
        isBackEnable: true,
      ),
      builder: (context, constraints) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Row(
              children: [
                const LabelText(
                  labelText: "APNO",
                  isMandatory: true,
                ),
                EditText(
                  controller: edtZAAPNO,
                  textMode: TextInputType.text,
                  isMandatory: true,
                  onLostFocus: edtZAAPNO_LostFocus,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "APNA",
                  isMandatory: true,
                ),
                EditText(
                  controller: edtZAAPNA,
                  isMandatory: true,
                  textMode: TextInputType.text,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "AURL",
                ),
                EditText(
                    controller: edtZAAURL,
                    textMode: TextInputType.text,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "IURL",
                ),
                EditText(
                  controller: edtZAIURL,
                  textMode: TextInputType.text,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "REMA",
                ),
                EditText(
                  controller: edtZAREMA,
                  textMode: TextInputType.text,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Record Status",
                ),
                CheckboxExtender(
                  controller: chbZARCST,
                  label: "Active",
                  isChecked: true,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Upload File",
                ),
                UploadBox(
                  controller: upbFile,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Lookup",
                ),
                Lookup(
                  controller: lupApno,
                  entity: "APNO-01",
                  title: "List of Application",
                  onLostFocus: (item) {
                    //edtZAAPNO.text = item["Code"] ?? "";
                    //getData();
                  },
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "SearchBox",
                ),
                SearchBoxFormField(
                  controller: scbApno,
                  entity: "APNO-01",
                  title: "List of Application",
                  isMandatory: true,
                  onLostFocus: (item) {
                    //edtZAAPNO.text = item["Code"] ?? "";
                    //getData();
                  },
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Enable/Disable Toolbar",
                ),
                ButtonExtender(
                  buttonText: "Toolbar Switch",
                  onPressed: (){
                    setState(() {
                      // if(cbxComboTest.value == "AGLiS"){
                      //   // cbxComboTest.value = "CPNL";
                      //   cbxComboTest.selectedIndex = 2;
                      //   lupApno.text = "CPNL";
                      // }else{
                      //   // cbxComboTest.value = "AGLiS";
                      //   cbxComboTest.selectedIndex = 1;
                      //   lupApno.text = "AGLiS";
                      // }
                      // lupApno.text = "10001";
                      // cbxComboTest.selectedIndex = 1;
                      // edtZAREMA.text = cbxComboTest1.value;
                      // cbxComboTest1.isEnable = !cbxComboTest1.isEnable;
                      // tlbPage.isNewEnable = !(tlbPage.isNewEnable ?? true);
                      // tlbPage.isSaveEnable = !(tlbPage.isSaveEnable ?? true);
                      // tlbPage.isListEnable = !(tlbPage.isListEnable ?? true);

                      isTab1Visible = false;
                    });
                  },
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Date Test",
                ),
                DateBox(
                  controller: dtbDateTest,
                  // onSelected: (selectedDate) {
                  //   print(selectedDate);
                  // },
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "ComboBox Test 1",
                ),
                ComboBox(
                  controller: cbxComboTest1,
                  entity: "IWHS-01",
                  // comboBoxType: ComboBoxType.Year,
                  // showEmpty: false,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "ComboBox Test 2",
                ),
                ComboBox(
                  controller: cbxComboTest2,
                  entity: "ILOC-01",
                  isEnable: false,
                  // comboBoxType: ComboBoxType.Year,
                  // showEmpty: false,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Multiselect Test 1",
                ),
                Multiselect(
                  controller: mtsMultiselectTest1,
                  entity: "IWHS-01",
                  // comboBoxType: ComboBoxType.Year,
                  // showEmpty: false,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Multiselect Test 2",
                ),
                Multiselect(
                  controller: mtsMultiselectTest2,
                  entity: "ILOC-01",
                  // comboBoxType: ComboBoxType.Year,
                  // showEmpty: false,
                ),
              ],
            ),
            const Row(
              children: [
                LabelText(
                  labelText: "Tooltip Button",
                ),
                TooltipButton(
                  child: Text("Tooltip message", style: const TextStyle(fontSize: 16.0, fontWeight: FontWeight.normal, color: Colors.black),),
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "TimeBox",
                ),
                TimeBoxFormField(
                  controller: tmbTime,
                  isMandatory: true,
                ),
              ],
            ),
            TabbarExtender(
                children: tabItems
            ),
          ],
        );
      },
    );
  }

  Widget builderTab1(){
    return StatefulBuilder(builder: (context, setState) {
      return Column(
        children: [
          Text("Content 1"),
          Text(t1.toString()),
          ButtonExtender(
            buttonText: "+1",
            onPressed: () {
              setState(() {
                t1 += 1;
              });
            },
          ),
        ],
      );
    },);
  }

  Widget builderTab2(){
    return Column(
      children: [
        Text("Content 2"),
        Text(t2.toString()),
        ButtonExtender(
          buttonText: "+1",
          onPressed: () {
            setState(() {
              t2 += 1;
            });
          },
        ),
      ],
    );
  }
  //endregion
}
