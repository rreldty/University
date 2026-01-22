import 'package:flutter/material.dart';
import 'package:university/Dao/Training/NilaiDetailDao.dart';
import 'package:university/Dao/Training/NilaiHeaderDao.dart';
import 'package:university/Dto/Training/NilaiDetailDto.dart';
import 'package:university/Dto/Training/NilaiHeaderDto.dart';

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
import 'NilaiDetail.dart';

class NilaiHeader extends StatefulWidget {
  static const String route = "/Training/NilaiHeader";

  @override
  createState() => NilaiHeaderState();
}

class NilaiHeaderState extends PageBase<NilaiHeader> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);

  LookupController lupNim = LookupController();
  EditTextController edtSemester = EditTextController();
  ComboBoxController cbxFakultas = ComboBoxController();
  ComboBoxController cbxJurusan = ComboBoxController();
  EditTextController edtTotal_SKS = EditTextController();
  EditTextController edtNilaiAkhir = EditTextController();

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
        title: "Print Nilai",
        dialogButton: DialogButton.OK,
      );
      return;
    }

    Map<String, String> param = Map();
    param["nim"] = lupNim.text.trim();
    param["semester"] = edtSemester.text.trim();

    ReportViewer.show(
      context: context,
      title: "Report Nilai",
      entity: "Nilai",
      param: param,
    );
  }
  //endregion

  //region Button Events
  Future<void> btnTambahData() async {
    if (form1.currentState!.validate()) {
      ModalPopupResult? popupResult = await ModalDialog.show(
        context: context,
        title: "Input Nilai",
        child: NilaiDetail(
          NilaiHeader: collectionInfo(),
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
          List<NilaiDetailDto> lst = [];
          List<NilaiDetailDto> lstGrid =
              dge.gridItem.map((e) => NilaiDetailDto.fromJson(e)).toList();

          if (lstGrid.isNotEmpty) {
            for (int i = 0; i < lstGrid.length; i++) {
              NilaiDetailDto obj = lstGrid[i];
              if (obj.isSelected) {
                lst.add(obj);
              }
            }
          }

          if (lst.isNotEmpty) {
            NilaiHeaderDto dto = collectionInfo();
            dto.Details = lst;

            NilaiHeaderDao dao = NilaiHeaderDao();
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
  void lupNim_onLostFocus(Map<String, dynamic> map) {
    String kodeFakultas = map["kode_fakultas"]?.toString() ?? "";
    String kodeJurusan = map["kode_jurusan"]?.toString() ?? "";

    setState(() {
      cbxFakultas.value = kodeFakultas;
      cbxFakultas.isRefresh = true;
      cbxJurusan.filter = "kode_fakultas = '$kodeFakultas'";
      cbxJurusan.isRefresh = true;
    });

    //Future.delayed(const Duration(milliseconds: 100), () {
    // setState(() {
    // cbxJurusan.value = kodeJurusan;
    //cbxJurusan.isRefresh = true;
    //});
    //});

    getData();
  }

  void edtSemester_onLostFocus() {
    if (lupNim.text.isNotEmpty && edtSemester.text.isNotEmpty) {
      getData();
    }
  }

  //endregion

  //region Methods
  void getData() async {
    String strResult = "";

    NilaiHeaderDao dao = NilaiHeaderDao();
    NilaiHeaderDto? obj = await dao.oneData(collectionInfo());

    print("Response dari backend: $obj"); // Debug: cek isi response

    //if (obj != null) {
    setState(() {
      cbxFakultas.value = obj.kode_fakultas;
      cbxJurusan.value = obj.kode_jurusan;
      edtNilaiAkhir.numericValue = obj.nilai;

      cbxFakultas.isRefresh = true;
      cbxJurusan.filter = "kode_fakultas = '${obj.kode_fakultas}'";
      cbxJurusan.isRefresh = true;

      dge.isRefresh = true;
    });
    pageBehaviour(PageMode.Edit);
    //}
  }

  Future<dynamic> getListDetail(
      intPageNumber, intPageSize, strSqlFilter, strSqlSort) async {
    NilaiDetailDto objInfo = NilaiDetailDto(
      nim: lupNim.text,
      semester: edtSemester.text,
      PageNumber: intPageNumber,
      PageSize: intPageSize,
    );

    NilaiDetailDao dao = NilaiDetailDao();
    List<NilaiDetailDto> lst = await dao.listPaging(objInfo);
    return lst;
  }

  NilaiHeaderDto collectionInfo() {
    NilaiHeaderDto objInfo = NilaiHeaderDto();
    objInfo.nim = lupNim.text;
    objInfo.semester = edtSemester.text;
    objInfo.kode_fakultas = cbxFakultas.value;
    objInfo.kode_jurusan = cbxJurusan.value;
    objInfo.nilai = edtNilaiAkhir.numericValue;
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
            edtNilaiAkhir.text = "";

            lupNim.isEnable = true;
            edtSemester.isEnable = true;
            cbxFakultas.isEnable = false;
            cbxJurusan.isEnable = false;
            edtNilaiAkhir.isEnable = false;

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
            edtNilaiAkhir.isEnable = false;

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
          edtNilaiAkhir.isEnable = false;
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
        listTitle: "List of NilaiHeader",
        listOnSelected: (map) {
          lupNim.text = map["nim"] ?? "";
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
          children: <Widget>[
            Row(children: [
              const LabelText(
                labelText: "Nomer Induk Mahasiswa",
                isMandatory: true,
              ),
              Lookup(
                controller: lupNim,
                entity: "MSHW-02",
                title: "List of Mahasiswa",
                isMandatory: true,
                onLostFocus: lupNim_onLostFocus,
              ),
            ]),
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
            Row(children: [
              const LabelText(labelText: "Fakultas"),
              ComboBox(
                controller: cbxFakultas,
                entity: "FKLT-01",
              ),
            ]),
            Row(children: [
              const LabelText(labelText: "Jurusan"),
              ComboBox(
                controller: cbxJurusan,
                entity: "JRSN-01",
              ),
            ]),
            Row(children: [
              const LabelText(labelText: "Nilai Akhir"),
              EditText(
                controller: edtNilaiAkhir,
                isEnable: false,
                textMode: TextInputType.number,
              ),
            ]),
            const SizedBox(height: 10),
            Row(children: [
              ButtonExtender(
                buttonText: "+ Tambah Data",
                onPressed: btnTambahData,
              ),
              const SizedBox(width: 20),
              ButtonExtender(
                buttonText: "- Hapus Data",
                onPressed: btnHapusData,
              ),
            ]),
            const SizedBox(height: 10),
            DataGridExtender(
              controller: dge,
              futureData: getListDetail,
              deColumns: [
                DEColumn(
                  columnName: "isSelected",
                  columnType: DEColumnType.DECheckbox,
                ),
                DEColumn(
                  columnName: "kode_matakuliah",
                  headerText: "Kode Mata Kuliah",
                  columnType: DEColumnType.String,
                  columnWidth: 150,
                ),
                DEColumn(
                  columnName: "nama_matakuliah",
                  headerText: "Nama Mata Kuliah",
                  columnType: DEColumnType.String,
                  columnWidth: 250,
                ),
                DEColumn(
                  columnName: "sks",
                  headerText: "SKS",
                  columnType: DEColumnType.Numeric,
                  columnWidth: 80,
                ),
                DEColumn(
                  columnName: "skor",
                  headerText: "Skor",
                  columnType: DEColumnType.Numeric,
                  columnFormat: DENumericFormat.Amount,
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
