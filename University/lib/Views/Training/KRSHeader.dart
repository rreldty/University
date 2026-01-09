import 'package:flutter/material.dart';
import 'package:university/Dao/Training/KRSDetailDao.dart';
import 'package:university/Dao/Training/KRSHeaderDao.dart';
import 'package:university/Dto/Training/KRSDetailDto.dart';
import 'package:university/Dto/Training/KRSHeaderDto.dart';

import '../../Common/PageBase.dart';
import '../../Dto/Base/ModalPopupResult.dart';
import '../../UserControls/ButtonExtender.dart';
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
import 'KRSDetail.dart';

class KRSHeader extends StatefulWidget {
  static const String route = "/Training/KRSHeader";

  @override
  createState() => KRSHeaderState();
}

class KRSHeaderState extends PageBase<KRSHeader> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);

  LookupController lupNim = LookupController();
  EditTextController edtSemester = EditTextController();
  ComboBoxController cbxFakultas = ComboBoxController();
  ComboBoxController cbxJurusan = ComboBoxController();
  EditTextController edtTotal_SKS = EditTextController();
  DataGridExtenderController dge = DataGridExtenderController();

  //endregion

  //region Init
  @override
  void appInit(PagePlatform pagePlatform) {
    pageBehaviour(PageMode.Add);
  }

  //endregion

  //region Toolbar Events
  void tlbNew_Click() {
    pageBehaviour(PageMode.Add);
  }

  void tlbBack_Click() {
    Navigator.pop(context);
  }

  void tlbPrint_Click() {
    if (lupNim.text.isEmpty || edtSemester.text.isEmpty) {
      MessageBox.show(
        context: context,
        message: "Pilih Mahasiswa dan Semester terlebih dahulu",
        title: "Print KRS",
        dialogButton: DialogButton.OK,
      );
      return;
    }

    Map<String, String> param = Map();
    param["nim"] = lupNim.text.trim();
    param["semester"] = edtSemester.text.trim();

    ReportViewer.show(
      context: context,
      title: "Report KRS",
      entity: "KRS",
      param: param,
    );
  }
  //endregion

  //region Button Events
  Future<void> btnTambahData() async {
    if (form1.currentState!.validate()) {
      ModalPopupResult? popupResult = await ModalDialog.show(
        context: context,
        title: "Tambah Mata Kuliah",
        child: KRSDetail(
          KRSHeader: collectionInfo(),
        ),
      );

      if (popupResult!.dialogResult == DialogResult.OK) {
        showModalProgress.value = true;

        setState(() {
          dge.isRefresh = true;
        });

        showModalProgress.value = false;
      }
    }
  }

  void btnHapusData() async {
    if (form1.currentState!.validate()) {
      DialogResult? dlgResult = await MessageBox.show(
        context: context,
        title: "Hapus Mata Kuliah",
        message: "Apakah anda yakin ingin menghapus mata kuliah yang dipilih?",
        dialogButton: DialogButton.OkCancel,
      );

      if (dlgResult == DialogResult.OK) {
        String strResult = "";
        showModalProgress.value = true;

        try {
          List<KRSDetailDto> lst = [];
          List<KRSDetailDto> lstGrid =
              dge.gridItem.map((e) => KRSDetailDto.fromJson(e)).toList();

          if (lstGrid.isNotEmpty) {
            for (int i = 0; i < lstGrid.length; i++) {
              KRSDetailDto obj = lstGrid[i];
              if (obj.isSelected) {
                lst.add(obj);
              }
            }
          }

          if (lst.isNotEmpty) {
            KRSHeaderDto dto = collectionInfo();
            dto.Details = lst;

            KRSHeaderDao dao = KRSHeaderDao();
            strResult = await dao.Update(dto);
          } else {
            strResult = "Pilih mata kuliah yang akan dihapus";
          }
        } catch (ex) {
          strResult = ex.toString();
        }

        showModalProgress.value = false;

        if (strResult.isEmpty) {
          await MessageBox.show(
            context: context,
            message: "Hapus mata kuliah berhasil",
            title: "Hapus Berhasil",
            dialogButton: DialogButton.OK,
          );

          getData();
        } else {
          await MessageBox.show(
            context: context,
            message: strResult,
            title: "Hapus Gagal",
            dialogButton: DialogButton.OK,
          );
        }
      }
    }
  }
  //endregion

  //region Lookup Events
  void lupNim_onChanged(Map<String, dynamic> map) {
    String kodeFakultas = map["Kode_Fakultas"]?.toString() ?? "";
    String kodeJurusan = map["Kode_Jurusan"]?.toString() ?? "";

    setState(() {
      cbxFakultas.value = kodeFakultas;
      cbxFakultas.isRefresh = true;
      cbxJurusan.filter = "kode_fakultas = '$kodeFakultas'";
      cbxJurusan.isRefresh = true;
    });

    Future.delayed(const Duration(milliseconds: 100), () {
      setState(() {
        cbxJurusan.value = kodeJurusan;
        cbxJurusan.isRefresh = true;
      });
    });

    _tryGetData();
  }

  void edtSemester_onLostFocus() {
    _tryGetData();
  }

  void _tryGetData() {
    if (lupNim.text.isNotEmpty && edtSemester.text.isNotEmpty) {
      getData();
    }
  }
  //endregion

  //region Methods
  void getData() async {
    String strResult = "";

    KRSHeaderDao dao = KRSHeaderDao();
    KRSHeaderDto? obj = await dao.oneData(collectionInfo());

    if (obj != null) {
      setState(() {
        cbxFakultas.value = obj.kode_fakultas;
        cbxJurusan.value = obj.kode_jurusan;
        edtTotal_SKS.numericValue = obj.total_sks;

        cbxFakultas.isRefresh = true;
        cbxJurusan.filter = "kode_fakultas = '${obj.kode_fakultas}'";
        cbxJurusan.isRefresh = true;

        dge.isRefresh = true;
      });
      pageBehaviour(PageMode.Edit);
    }
  }

  Future<dynamic> getListDetail(
      intPageNumber, intPageSize, strSqlFilter, strSqlSort) async {
    KRSDetailDto objInfo = KRSDetailDto(
      nim: lupNim.text,
      semester: edtSemester.text,
      PageNumber: intPageNumber.toInt(),
      PageSize: intPageSize.toInt(),
    );

    KRSDetailDao dao = KRSDetailDao();
    List<KRSDetailDto> lst = await dao.listPaging(objInfo);
    return lst;
  }

  KRSHeaderDto collectionInfo() {
    KRSHeaderDto objInfo = KRSHeaderDto();
    objInfo.nim = lupNim.text;
    objInfo.semester = edtSemester.text;
    objInfo.kode_fakultas = cbxFakultas.value;
    objInfo.kode_jurusan = cbxJurusan.value;
    objInfo.total_sks = edtTotal_SKS.numericValue;
    objInfo.record_status = 1;
    return objInfo;
  }

  @override
  void pageBehaviour(PageMode pageMode) {
    switch (pageMode) {
      case PageMode.Add:
        {
          setState(() {
            form1.currentState?.reset();
            lupNim.text = "";
            edtSemester.text = "";
            cbxFakultas.value = "";
            cbxJurusan.value = "";
            edtTotal_SKS.text = "";

            lupNim.isEnable = true;
            edtSemester.isEnable = true;
            cbxFakultas.isEnable = false;
            cbxJurusan.isEnable = false;
            edtTotal_SKS.isEnable = false;

            dge.isRefresh = true;
          });
          break;
        }
      case PageMode.Edit:
        {
          setState(() {
            lupNim.isEnable = true;
            edtSemester.isEnable = true;
            cbxFakultas.isEnable = false;
            cbxJurusan.isEnable = false;
            edtTotal_SKS.isEnable = false;

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
          lupNim.isEnable = false;
          edtSemester.isEnable = false;
          cbxFakultas.isEnable = false;
          cbxJurusan.isEnable = false;
          edtTotal_SKS.isEnable = false;
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
        toolbarBoxMode: ToolbarBoxMode.New,
        onNew: tlbNew_Click,
        onBack: tlbBack_Click,
        onPrint: tlbPrint_Click,
        listEntity: "FKLS-01",
        listTitle: "List of KRSHeader",
        listOnSelected: (map) {
          lupNim.text = map["Kode KRSHeader"] ?? "";
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
                  labelText: "Mahasiswa",
                  isMandatory: true,
                ),
                Lookup(
                  controller: lupNim,
                  entity: "MSHW-03",
                  title: "List of Mahasiswa",
                  isMandatory: true,
                  onLostFocus: lupNim_onChanged,
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
                  maxLength: 3,
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
                  controller: cbxFakultas,
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
                  entity: "JRSN-01",
                ),
              ],
            ),
            Row(
              children: [
                const LabelText(
                  labelText: "Total SKS",
                ),
                EditText(
                  controller: edtTotal_SKS,
                  textMode: TextInputType.number,
                  maxLength: 3,
                ),
              ],
            ),
            const SizedBox(height: 10),
            Row(
              children: [
                ButtonExtender(
                  buttonText: "+ Tambah Data",
                  onPressed: btnTambahData,
                ),
                const SizedBox(width: 10),
                ButtonExtender(
                  buttonText: "- Hapus Data",
                  onPressed: btnHapusData,
                ),
              ],
            ),
            const SizedBox(height: 10),
            DataGridExtender(
              controller: dge,
              futureData: getListDetail,
              width: constraints.maxWidth,
              deColumns: [
                DEColumn(
                  columnName: "isSelected",
                  columnType: DEColumnType.DECheckbox,
                ),
                DEColumn(
                  columnName: "kode_matakuliah",
                  columnType: DEColumnType.String,
                  columnWidth: 150,
                ),
                DEColumn(
                  columnName: "nama_matakuliah",
                  columnType: DEColumnType.String,
                  columnWidth: 250,
                ),
                DEColumn(
                  columnName: "sks",
                  columnType: DEColumnType.Numeric,
                  columnWidth: 80,
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
