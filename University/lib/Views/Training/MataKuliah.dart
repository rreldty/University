import 'package:flutter/cupertino.dart';
import 'package:university/Dao/Training/MataKuliahDao.dart';
import 'package:university/Dto/Training/MataKuliahDto.dart';

import '../../Common/PageBase.dart';
import '../../UserControls/CheckboxExtender.dart';
import '../../UserControls/ComboBox.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/PageContent.dart';
import '../../UserControls/ReportViewer.dart';
import '../../UserControls/ToolbarBox.dart';

class MataKuliah extends StatefulWidget {
  static const String route = "/Training/MataKuliah";

  @override
  createState() => MataKuliahState();
}

class MataKuliahState extends PageBase<MataKuliah> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);

  ComboBoxController cbxKode_Fakultas = ComboBoxController();
  ComboBoxController cbxKode_Jurusan = ComboBoxController();
  EditTextController edtKode_MataKuliah = EditTextController();
  EditTextController edtNama_MataKuliah = EditTextController();
  EditTextController edtSKS = EditTextController();
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
        MataKuliahDao dao = MataKuliahDao();
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

  void tlbPrint_Click() {
    Map<String, String> param = Map();
    param["fakultas"] = cbxKode_Fakultas.value.trim();
    param["jurusan"] = cbxKode_Jurusan.value.trim();

    ReportViewer.show(
        context: context,
        title: "Mata Kuliah",
        entity: "PM030A",
        param: param
    );
  }

  void cbxKode_Fakultas_onChanged() {
    // Filter jurusan berdasarkan fakultas yang dipilih
    setState(() {
      debugPrint("[MataKuliah] Fakultas changed: ${cbxKode_Fakultas.value}");
      cbxKode_Jurusan.filter = "kode_fakultas = '${cbxKode_Fakultas.value}'";
      cbxKode_Jurusan.isRefresh = true;
    });
    
    _tryGetData();
  }

  void cbxKode_Jurusan_onChanged() {
    _tryGetData();
  }

  void edtKode_MataKuliah_onLostFocus() {
    _tryGetData();
  }

  void _tryGetData() {
    if (cbxKode_Fakultas.value.isNotEmpty && 
        cbxKode_Jurusan.value.isNotEmpty && 
        edtKode_MataKuliah.text.isNotEmpty) {
      getData();
    }
  }
  //endregion

  //region Methods
  void getData() async {
    try {
      MataKuliahDao dao = MataKuliahDao();
      MataKuliahDto obj = await dao.oneData(collectionInfo());

      if (obj!=null) {
        setState(() {
          cbxKode_Fakultas.value = obj.kode_fakultas;
          cbxKode_Fakultas.isRefresh = true;
          
          // Set filter jurusan berdasarkan fakultas
          cbxKode_Jurusan.filter = "kode_fakultas = '${obj.kode_fakultas}'";
        });
        
        // Delay untuk memastikan fakultas dan filter jurusan sudah terupdate
        await Future.delayed(const Duration(milliseconds: 100));
        
        setState(() {
          cbxKode_Jurusan.value = obj.kode_jurusan;
          cbxKode_Jurusan.isRefresh = true;
        });
        
        edtNama_MataKuliah.text = obj.nama_matakuliah;
        edtSKS.numericValue = obj.sks;
        chbRecordStatus.isChecked = (obj.record_status == 1 ? true : false);
        pageBehaviour(PageMode.Edit);
      }
    } catch (e) {
      debugPrint("[MataKuliah] Error getData: $e");
    }
  }

  MataKuliahDto collectionInfo() {
    MataKuliahDto objInfo = MataKuliahDto();
    objInfo.kode_fakultas = cbxKode_Fakultas.value;
    objInfo.kode_jurusan = cbxKode_Jurusan.value;
    objInfo.kode_matakuliah = edtKode_MataKuliah.text;
    objInfo.nama_matakuliah = edtNama_MataKuliah.text;
    objInfo.sks = double.tryParse(edtSKS.text) ?? 0;
    objInfo.record_status = (chbRecordStatus.isChecked ? 1 : 0);
    return objInfo;
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    switch (pageMode) {
      case PageMode.Add:
        {
          setState(() {
            form1.currentState?.reset();
            cbxKode_Fakultas.value = "";
            cbxKode_Jurusan.value = "";
            edtKode_MataKuliah.text = "";
            edtNama_MataKuliah.text = "";
            edtSKS.text = "";
            chbRecordStatus.isChecked = true;

            cbxKode_Fakultas.isEnable = true;
            cbxKode_Jurusan.isEnable = true;
            edtKode_MataKuliah.isEnable = true;
            edtNama_MataKuliah.isEnable = true;
            edtSKS.isEnable = true;
            chbRecordStatus.isEnable = true;
          });
          break;
        }
      case PageMode.Edit:
        {
          setState(() {
            cbxKode_Fakultas.isEnable = false;
            cbxKode_Jurusan.isEnable = false;
            edtKode_MataKuliah.isEnable = false;
            edtNama_MataKuliah.isEnable = true;
            edtSKS.isEnable = true;
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
          cbxKode_Jurusan.isEnable = false;
          edtKode_MataKuliah.isEnable = false;
          edtNama_MataKuliah.isEnable = false;
          edtSKS.isEnable = false;
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
        listEntity: "MTKL-01",
        listTitle: "List of Mata Kuliah",
        listOnSelected: (map) async {
          String kodeFakultas = map["Kode Fakultas"] ?? "";
          String kodeJurusan = map["Kode Jurusan"] ?? "";
          String kodeMatakuliah = map["Kode Matakuliah"] ?? "";
          
          setState(() {
            // Set fakultas dulu
            cbxKode_Fakultas.value = kodeFakultas;
            cbxKode_Fakultas.isRefresh = true;
            
            // Set filter jurusan berdasarkan fakultas
            cbxKode_Jurusan.filter = "kode_fakultas = '$kodeFakultas'";
          });
          
          // Delay untuk memastikan fakultas sudah terupdate
          await Future.delayed(const Duration(milliseconds: 100));
          
          setState(() {
            cbxKode_Jurusan.value = kodeJurusan;
            cbxKode_Jurusan.isRefresh = true;
          });
          
          edtKode_MataKuliah.text = kodeMatakuliah;

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
                  labelText: "Fakultas",
                  isMandatory: true,
                ),
                ComboBox(
                  controller: cbxKode_Fakultas,
                  entity: "FKLT-01",
                  isMandatory: true,
                  onChanged: (obj) => cbxKode_Fakultas_onChanged(),
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Jurusan",
                  isMandatory: true,
                ),
                ComboBox(
                  controller: cbxKode_Jurusan,
                  entity: "JRSN-01",
                  isMandatory: true,
                  onChanged: (obj) => cbxKode_Jurusan_onChanged(),
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Kode Mata Kuliah",
                  isMandatory: true,
                ),
                EditText(
                  controller: edtKode_MataKuliah,
                  isMandatory: true,
                  textMode: TextInputType.text,
                  maxLength: 10,
                  onLostFocus: edtKode_MataKuliah_onLostFocus,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Nama Matakuliah",
                ),
                EditText(
                  controller: edtNama_MataKuliah,
                  textMode: TextInputType.text,
                  maxLength: 100,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "SKS",
                ),
                EditText(
                  controller: edtSKS,
                  textMode: TextInputType.number,
                  maxLength: 5,
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
