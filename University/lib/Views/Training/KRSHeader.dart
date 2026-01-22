import 'package:flutter/cupertino.dart';
import 'package:university/Dao/Training/KrsDetailDao.dart';
import 'package:university/Dto/Training/KrsHeaderDto.dart';

import '../../Common/AppConfig.dart';
import '../../Common/PageBase.dart';
import '../../Dao/Training/KrsHeaderDao.dart';
import '../../Dto/Base/ModalPopupResult.dart';
import '../../Dto/Training/KrsDetailDto.dart';
import '../../UserControls/ComboBox.dart';
import '../../UserControls/DataGridExtender.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/Lookup.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/ModalDialog.dart';
import '../../UserControls/PageContent.dart';
import '../../UserControls/ReportViewer.dart';
import '../../UserControls/ToolbarBox.dart';
import 'KrsDetail.dart';

class KrsHeader extends StatefulWidget {
  static const String route = "/Xample/KrsHeader";

  @override
  createState() => KrsHeaderState();
}

class KrsHeaderState extends PageBase<KrsHeader> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  LookupController lupNIM = LookupController();
  EditTextController edtSemester = EditTextController();
  ComboBoxController cbxFakulktas = ComboBoxController();
  ComboBoxController cbxJurusan = ComboBoxController();
  EditTextController edtTotal_SKS = EditTextController();

  DataGridExtenderController dge = DataGridExtenderController();

  //endregion

  //region Init
  @override
  void appInit(PagePlatform pagePlatform) {
    pageBehaviour(PageMode.Add);
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    // TODO: implement pageBehaviour
    switch (pageMode) {
      case PageMode.Add:
        {
          setState(() {
            lupNIM.text = "";
            edtSemester.numericValue = 0;
            cbxFakulktas.value = "";
            cbxJurusan.value = "";
            edtTotal_SKS.numericValue = 0;

            dge = DataGridExtenderController();
          });

          break;
        }
      case PageMode.Edit:
        {
          setState(() {
            dge.isRefresh = true;
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
  void tlbBack_Click() {
    Navigator.pop(context);
  }

  void tlbNew_Click() {
    pageBehaviour(PageMode.Add);
  }

  void tlbPrint_Click() {
    Map<String, String> param = Map();
    param["nim"] = lupNIM.text.trim();
    param["semester"] = edtSemester.numericValue.toString().trim();

    ReportViewer.show(
        context: context, title: "KRS", entity: "KRS", param: param);
  }

  void lupNIM_onLostFocus(map) {
    if (lupNIM.text.isNotEmpty && edtSemester.numericValue > 0) {
      getData();
    }
  }

  void edtSemester_onLostFocus() async {
    if (lupNIM.text.isNotEmpty && edtSemester.numericValue > 0) {
      getData();
    }
  }

  void btnAddLine_Click() async {
    if (form1.currentState!.validate()) {
      KrsDetailDto? objKrsDetail;

      ModalPopupResult? popupResult = await ModalDialog.show(
        context: context,
        title: "Input Nilai",
        modalWidth: 550,
        modalHeight: 200,
        child: KrsDetail(
          objKrsHeader: collectionInfo(),
          // callback: (obj) {
          //   //opsional
          //   objZUG2 = obj;
          // },
        ),
      );

      if (popupResult!.dialogResult == DialogResult.OK) {
        showModalProgress.value = true;

        getData();

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
          List<KrsDetailDto> lstInfo = [];
          List<KrsDetailDto>? lstT =
              dge.gridItem.map((e) => KrsDetailDto.fromJson(e)).toList();

          if (lstT.isNotEmpty) {
            for (int i = 0; i < lstT.length; i++) {
              KrsDetailDto objT = lstT[i];
              if (objT.IsSelected) {
                lstInfo.add(objT);
              }
            }
          }

          KrsHeaderDto obj = collectionInfo();
          obj.listKrsDetail = lstInfo;

          if (lstInfo.isNotEmpty) {
            // KrsHeaderDao dao = KrsHeaderDao();
            KrsDetailDao dao = KrsDetailDao();
            strResult = await dao.SaveHapus(lstInfo);
          } else {
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

          getData();
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

  //endregion

  //region Methods
  void getData() async {
    String strResult = "";

    showModalProgress.value = true;

    try {
      KrsHeaderDao dao = KrsHeaderDao();
      KrsHeaderDto? obj = await dao.oneDataMahasiswa(collectionInfo());

      if (obj != null) {
        setState(() {
          cbxFakulktas.value = obj.kode_fakultas;
          cbxJurusan.value = obj.kode_jurusan;
          edtTotal_SKS.numericValue = obj.total_sks;
        });

        pageBehaviour(PageMode.Edit);
      }
    } catch (ex) {
      strResult = ex.toString();
    }

    showModalProgress.value = false;

    if (strResult.isNotEmpty) {
      await MessageBox.show(
          context: context, message: strResult, title: "Get Data");
    }
  }

  Future<dynamic> getList(
      intPageNumber, intPageSize, strSqlFilter, strSqlSort) async {
    KrsDetailDto objInfo = KrsDetailDto(
      nim: lupNIM.text.trim(),
      semester: edtSemester.numericValue,
      kode_jurusan: cbxJurusan.value,
      PageNumber: intPageNumber,
      PageSize: intPageSize,
    );

    KrsDetailDao dao = KrsDetailDao();
    List<KrsDetailDto> lst = await dao.ListMatakuliah(objInfo);
    return lst;
  }

  KrsHeaderDto collectionInfo() {
    KrsHeaderDto objInfo = KrsHeaderDto();
    objInfo.nim = lupNIM.text.trim();
    objInfo.semester = edtSemester.numericValue;
    objInfo.kode_fakultas = cbxFakulktas.value;
    objInfo.kode_jurusan = cbxJurusan.value;
    objInfo.total_sks = edtTotal_SKS.numericValue;
    return objInfo;
  }

  //endregion

  //region Layout
  @override
  Widget build(BuildContext context) {
    return PageContent(
      formKey: form1,
      showModalProgress: showModalProgress,
      toolbar: ToolbarBox(
        toolbarBoxMode: ToolbarBoxMode.New,
        onNew: tlbNew_Click,
        onBack: tlbBack_Click,
        onPrint: tlbPrint_Click,
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
                  labelText: "Nomer Induk Mahasiswa",
                  isMandatory: true,
                ),
                Lookup(
                  controller: lupNIM,
                  isMandatory: true,
                  title: "List of Mahasiswa",
                  entity: "MSHW-02",
                  onLostFocus: lupNIM_onLostFocus,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Semester",
                  isMandatory: true,
                ),
                EditText(
                  controller: edtSemester,
                  isMandatory: true,
                  textMode: TextInputType.number,
                  numericType: NumericType.Unit,
                  onLostFocus: edtSemester_onLostFocus,
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Fakultas",
                ),
                ComboBox(
                  controller: cbxFakulktas,
                  isEnable: false,
                  entity: "FKLT-01",
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Jurusan",
                ),
                ComboBox(
                  controller: cbxJurusan,
                  isEnable: false,
                  entity: "JRSN-01",
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Total Sks",
                ),
                EditText(
                  controller: edtTotal_SKS,
                  isEnable: false,
                  textMode: TextInputType.number,
                  numericType: NumericType.Unit,
                ),
              ],
            ),
            DataGridExtender(
              controller: dge,
              futureData: getList,
              autoGeneratedColumns: false,
              onAddLine: btnAddLine_Click,
              onDeleteLine: btnDeleteLine_Click,
              isAddLineVisible: true,
              isDeleteLineVisible: true,
              // height: 400,
              width: 550,
              onAfterRefresh: () {
                showModalProgress.value = false;
              },
              deColumns: [
                DEColumn(
                  columnName: "IsSelected",
                  columnType: DEColumnType.DECheckbox,
                ),
                DEColumn(
                  headerText: "Kode Mata Kuliah",
                  columnName: "kode_matakuliah",
                  columnType: DEColumnType.String,
                  columnWidth: 100,
                ),
                DEColumn(
                  headerText: "Nama Mata Kuliah",
                  columnName: "nama_matakuliah",
                  columnType: DEColumnType.String,
                  //columnWidth: 0,
                ),
                DEColumn(
                  headerText: "SKS",
                  columnName: "sks",
                  columnType: DEColumnType.Numeric,
                  columnFormat: DENumericFormat.Unit,
                  columnAlign: TextAlign.end,
                  // headerStyle: TextStyle TextAlign.end,
                  columnWidth: 100,
                ),
              ],
            ),
          ],
        );
      },
    );
  }
}
