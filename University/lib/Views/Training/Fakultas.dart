import 'package:flutter/cupertino.dart';
import 'package:university/Dao/Training/FakultasDao.dart';
import 'package:university/Dto/Training/FakultasDto.dart';

import '../../Common/PageBase.dart';
import '../../UserControls/CheckboxExtender.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/PageContent.dart';
import '../../UserControls/ReportViewer.dart';
import '../../UserControls/ToolbarBox.dart';

class Fakultas extends StatefulWidget {
  static const String route = "/Training/Fakultas";

  @override
  createState() => FakultasState();
}

class FakultasState extends PageBase<Fakultas> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);

  EditTextController edtKode_Fakultas = EditTextController();
  EditTextController edtNama_Fakultas = EditTextController();
  CheckboxExtenderController chbRecordStatus = CheckboxExtenderController();

  //endregion

  //region Load
  @override
  void appInit(PagePlatform pagePlatform) {
    // TODO: implement appInit
    pageBehaviour(PageMode.Add);
  }

  //endregion

  //region Events
  void tlbNew_Click() {
      pageBehaviour(PageMode.Add);
  }

  void tlbSave_Click() async {
    if (form1.currentState!.validate()) {
      String strResult = "";

      try{
        FakultasDao dao = FakultasDao();
        strResult = await dao.Save(collectionInfo());
      }
      catch(e) {
        strResult = e.toString();
      }

      if (strResult.isEmpty) {
        await MessageBox.show(
            context: context,
            message: "Save successfully",
            title: "Save Success",
            dialogButton: DialogButton.OK);

        // setState(() {
        //   edtKode_Fakultas.isEnable = false;
        // });
        pageBehaviour(PageMode.Edit);

      } else {
        await MessageBox.show(
            context: context,
            message: strResult,
            title: "Save Failed",
            dialogButton: DialogButton.OK);
      }

    }
  }

  void tlbBack_Click(){
    Navigator.pop(context);
  }

  void tlbPrint_Click() {
    Map<String, String> param = Map();
    param["Kode_Fakultas"] = edtKode_Fakultas.text.trim();
    param["Nama_Fakultas"] = edtNama_Fakultas.text.trim();
    param["User"] = "Cyntya";

    ReportViewer.show(
        context: context,
        title: "Fakultas",
        entity: "Fakultas",
        param: param
    );
  }

  void edtKode_Fakultas_onLostFocus(){
    getData();
  }
  //endregion

  //region Methods
  void getData() async {
    String strResult = "";

    FakultasDao dao = FakultasDao();
    FakultasDto obj= await dao.oneData(collectionInfo());

if(obj != null){
  edtNama_Fakultas.text = obj.nama_fakultas;
  chbRecordStatus.isChecked = (obj.record_status == 1 ? true : false);

  pageBehaviour(PageMode.Edit);
}
    }

  FakultasDto collectionInfo() {
    FakultasDto objInfo = FakultasDto();
    objInfo.kode_fakultas = edtKode_Fakultas.text;
    objInfo.nama_fakultas = edtNama_Fakultas.text;
    objInfo.record_status = (chbRecordStatus.isChecked ? 1 : 0);
    return objInfo;
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    // TODO: implement pageBehaviour
    switch (pageMode) {
      case PageMode.Add:
        {
          setState(() {
            form1.currentState!.reset();
            edtKode_Fakultas.text = "";
            edtNama_Fakultas.text = "";
            chbRecordStatus.isChecked = true;

            edtKode_Fakultas.isEnable = true;
          });

          break;
        }
      case PageMode.Edit:
        {
          setState(() {

            edtKode_Fakultas.isEnable = false;
          });

          break;
        }
      case PageMode.Copy:
        {
          break;
        }
      case PageMode.View:
        {
          edtKode_Fakultas.isEnable = false;
          edtNama_Fakultas.isEnable = false;
          chbRecordStatus.isEnable = false;
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
        toolbarBoxMode: ToolbarBoxMode.Master,
        onNew: tlbNew_Click,
        onSave: tlbSave_Click,
        onBack: tlbBack_Click,
        onPrint: tlbPrint_Click,
        listEntity: "FKLS-01",
        listTitle: "List of Fakultas",
        listOnSelected: (map) {
          edtKode_Fakultas.text = map["Kode Fakultas"] ?? "";
          getData();
        },
        isBackVisible: true,
        isBackEnable: true,
        isPrintVisible: true,
        isPrintEnable: true,
      ),
      builder: (context, constraints) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Row(
              children: [
                const LabelText(
                  labelText: "Kode Fakultas",
                  isMandatory: true,
                ),
                EditText(
                  controller: edtKode_Fakultas,
                  isMandatory: true,
                  textMode: TextInputType.text,
                  onLostFocus: edtKode_Fakultas_onLostFocus,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Nama Fakultas",
                ),
                EditText(
                  controller: edtNama_Fakultas,
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
                  controller: chbRecordStatus,
                  label: "Active",
                  isChecked: true,
                ),
              ],
            ),

          ],
        );
      },
    );
  }

//endregion
}
