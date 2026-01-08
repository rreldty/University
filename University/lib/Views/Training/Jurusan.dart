import 'package:flutter/cupertino.dart';
import 'package:university/Dao/Training/JurusanDao.dart';
import 'package:university/Dto/Training/JurusanDto.dart';

import '../../Common/PageBase.dart';
import '../../UserControls/CheckboxExtender.dart';
import '../../UserControls/ComboBox.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/PageContent.dart';
import '../../UserControls/ToolbarBox.dart';

class Jurusan extends StatefulWidget {
  static const String route = "/Training/Jurusan";

  @override
  createState() => JurusanState();
}

class JurusanState extends PageBase<Jurusan> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);

  ComboBoxController cbxKode_Fakultas = ComboBoxController();
  EditTextController edtKode_Jurusan = EditTextController();
  EditTextController edtNama_Jurusan = EditTextController();
  CheckboxExtenderController chbRecordStatus = CheckboxExtenderController();

  //endregion

  //region Load
  @override
  void appInit(PagePlatform pagePlatform) {
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

      try {
        JurusanDao dao = JurusanDao();
        strResult = await dao.Save(collectionInfo());
      } catch (e) {
        strResult = e.toString();
      }

      if (strResult.isEmpty) {
        await MessageBox.show(
            context: context,
            message: "Save successfully",
            title: "Save Success",
            dialogButton: DialogButton.OK);

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

  void tlbBack_Click() {
    Navigator.pop(context);
  }

  void cbxKode_Fakultas_onLostFocus() {
    if (cbxKode_Fakultas.value.isNotEmpty && edtKode_Jurusan.text.isNotEmpty) {
      getData();
    }
  }

  void edtKode_Jurusan_onLostFocus() {
    if (cbxKode_Fakultas.value.isNotEmpty && edtKode_Jurusan.text.isNotEmpty) {
      getData();
    }
  }
  //endregion

  //region Methods
  void getData() async {
    debugPrint("[Jurusan] getData called");
    debugPrint("[Jurusan] kode_fakultas: ${cbxKode_Fakultas.value}");
    debugPrint("[Jurusan] kode_jurusan: ${edtKode_Jurusan.text}");
    
    try {
      JurusanDao dao = JurusanDao();
      JurusanDto obj = await dao.oneData(collectionInfo());

      debugPrint("[Jurusan] nama_jurusan from API: ${obj.nama_jurusan}");
      
      if(obj != null){
  edtNama_Jurusan.text = obj.nama_jurusan;
  chbRecordStatus.isChecked = (obj.record_status == 1 ? true : false);

  pageBehaviour(PageMode.Edit);
}
      
    } catch (e) {
      debugPrint("[Jurusan] Error: $e");
    }
  }

  JurusanDto collectionInfo() {
    JurusanDto objInfo = JurusanDto();
    objInfo.kode_fakultas = cbxKode_Fakultas.value;
    objInfo.kode_jurusan = edtKode_Jurusan.text;
    objInfo.nama_jurusan = edtNama_Jurusan.text;
    objInfo.record_status = (chbRecordStatus.isChecked ? 1 : 0);
    return objInfo;
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    switch (pageMode) {
      case PageMode.Add:
        {
          setState(() {
            form1.currentState!.reset();
            cbxKode_Fakultas.value = "";
            edtKode_Jurusan.text = "";
            edtNama_Jurusan.text = "";
            chbRecordStatus.isChecked = true;

            cbxKode_Fakultas.isEnable = true;
            edtKode_Jurusan.isEnable = true;
            edtNama_Jurusan.isEnable = true;
            chbRecordStatus.isEnable = true;
          });
          break;
        }
      case PageMode.Edit:
        {
          setState(() {
            cbxKode_Fakultas.isEnable = false;
            edtKode_Jurusan.isEnable = false;
            edtNama_Jurusan.isEnable = true;
            chbRecordStatus.isEnable = true;
          });
          break;
        }
      case PageMode.Copy:
        {
          break;
        }
      case PageMode.View:
        {
          cbxKode_Fakultas.isEnable = false;
          edtKode_Jurusan.isEnable = false;
          edtNama_Jurusan.isEnable = false;
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
        listEntity: "JRSN-01",
        listTitle: "List of Jurusan",
        listOnSelected: (map) {
          cbxKode_Fakultas.value = map["Kode Fakultas"] ?? "";
          edtKode_Jurusan.text = map["Kode Jurusan"] ?? "";
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
                  labelText: "Kode Fakultas",
                  isMandatory: true,
                ),
                ComboBox(
                  controller: cbxKode_Fakultas,
                  entity: "FKLT-01",
                  isMandatory: true,
                  onChanged: (obj) => cbxKode_Fakultas_onLostFocus(),
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Kode Jurusan",
                  isMandatory: true,
                ),
                EditText(
                  controller: edtKode_Jurusan,
                  isMandatory: true,
                  textMode: TextInputType.text,
                  maxLength: 100,
                  onLostFocus: edtKode_Jurusan_onLostFocus,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Nama Jurusan",
                ),
                EditText(
                  controller: edtNama_Jurusan,
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
