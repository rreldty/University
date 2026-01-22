import 'package:flutter/material.dart';
import 'package:university/Views/Training/MataKuliah.dart';
import '../../Common/ModalBase.dart';
import '../../Dao/Training/NilaiHeaderDao.dart';
import '../../Dto/Training/NilaiDetailDto.dart';
import '../../Dto/Training/NilaiHeaderDto.dart';
import '../../Dao/Training/MataKuliahDao.dart';
import '../../Dto/Training/MataKuliahDto.dart';
import '../../UserControls/ButtonExtender.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/Lookup.dart';
import '../../UserControls/ModalContent.dart';
import '../../UserControls/SearchBox.dart';

class NilaiDetail extends StatefulWidget {
  static const String route = "/Training/NilaiDetail";

  final NilaiHeaderDto NilaiHeader;
  final Function(NilaiDetailDto obj)? callback;

  const NilaiDetail({
    super.key,
    required this.NilaiHeader,
    this.callback,
  });

  @override
  createState() => NilaiDetailState();
}

class NilaiDetailState extends ModalBase<NilaiDetail> {
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  LookupController lupMatakuliah = LookupController();
  EditTextController edtSKS = EditTextController();
  EditTextController edtSkor = EditTextController();
  String namaMatakuliah = "";

  @override
  void appInit(ModalPlatform modalPlatform) {
    // Use MTKL-03 entity which gets mata kuliah from KRS based on NIM and semester
    //lupMatakuliah.filter =
    //  "A.nim = '${widget.NilaiHeader.nim}' AND A.semester = ${widget.NilaiHeader.semester}";
  }

  void lupMatakuliah_onChanged(Map<String, dynamic> item) {
    // Try multiple key variations for SKS
    edtSKS.numericValue = double.tryParse(
            item["sks"]?.toString() ?? item["SKS"]?.toString() ?? "0") ??
        0;

    // Capture nama_matakuliah
    namaMatakuliah = item["nama_matakuliah"]?.toString() ??
        item["Nama_MataKuliah"]?.toString() ??
        "";
  }

  void btnOK_Click() async {
    if (form1.currentState!.validate()) {
      String strResult = "";
      showModalProgress.value = true;

      try {
        NilaiDetailDto objDetail = collectionInfo();

        // Validate max skor 100
        if (objDetail.skor > 100) {
          strResult = "Skor tidak boleh lebih dari 100";
        } else if (objDetail.skor < 0) {
          strResult = "Skor tidak boleh kurang dari 0";
        } else {
          NilaiHeaderDto dto = NilaiHeaderDto();
          dto.nim = widget.NilaiHeader.nim;
          dto.semester = widget.NilaiHeader.semester;
          dto.kode_fakultas = widget.NilaiHeader.kode_fakultas;
          dto.kode_jurusan = widget.NilaiHeader.kode_jurusan;
          dto.objLine = objDetail;

          NilaiHeaderDao dao = NilaiHeaderDao();
          strResult = await dao.Save(dto);
        }
      } catch (ex) {
        strResult = ex.toString();
      }

      showModalProgress.value = false;

      if (strResult.isEmpty) {
        if (!mounted) return;
        await MessageBox.show(
          context: context,
          message: "Simpan berhasil",
          title: "Simpan Sukses",
          dialogButton: DialogButton.OK,
        );

        closeModalPopup(DialogResult.OK);
      } else {
        if (!mounted) return;
        await MessageBox.show(
          context: context,
          message: strResult,
          title: "Simpan Gagal",
          dialogButton: DialogButton.OK,
        );
      }
    }
  }

  void btnCancel_Click() {
    closeModalPopup(DialogResult.Cancel);
  }

  void getData() async {
    String strResult = "";

    showModalProgress.value = true;

    try {
      MataKuliahDto objInfo = MataKuliahDto();
      objInfo.kode_fakultas = widget.NilaiHeader.kode_fakultas.trim();
      objInfo.kode_jurusan = widget.NilaiHeader.kode_jurusan.trim();
      objInfo.kode_matakuliah = lupMatakuliah.text;
      MataKuliahDao dao = MataKuliahDao();
      MataKuliahDto? obj = await dao.oneData(objInfo);

      if (obj != null) {
        setState(() {
          edtSKS.numericValue = obj.sks;
        });
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

  NilaiDetailDto collectionInfo() {
    NilaiDetailDto objInfo = NilaiDetailDto();
    objInfo.nim = widget.NilaiHeader.nim;
    objInfo.semester = widget.NilaiHeader.semester;
    objInfo.kode_matakuliah = lupMatakuliah.text;
    objInfo.sks = edtSKS.numericValue;
    objInfo.skor = edtSkor.numericValue;
    return objInfo;
  }

  @override
  void modalBehaviour(ModalMode modalMode) {
    switch (modalMode) {
      case ModalMode.Add:
        {
          setState(() {
            lupMatakuliah.isEnable = true;
            edtSKS.isEnable = false;
            edtSkor.isEnable = true;
          });
          break;
        }
      case ModalMode.Edit:
        {
          setState(() {
            lupMatakuliah.isEnable = false;
            edtSKS.isEnable = false;
            edtSkor.isEnable = false;
          });
          break;
        }
      case ModalMode.View:
        {
          setState(() {
            lupMatakuliah.isEnable = false;
            edtSKS.isEnable = false;
            edtSkor.isEnable = false;
          });
          break;
        }
    }
  }

  @override
  Widget build(BuildContext context) {
    return ModalContent(
      formKey: form1,
      showModalProgress: showModalProgress,
      builder: (context, constraint) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const LabelText(
                  labelText: "Mata Kuliah",
                  isMandatory: true,
                ),
                Lookup(
                  controller: lupMatakuliah,
                  entity: "MTKL-03",
                  filter:
                      "A.nim = '${widget.NilaiHeader.nim}' AND A.semester = ${widget.NilaiHeader.semester}",
                  title: "List of Mata Kuliah",
                  isMandatory: true,
                  onLostFocus: lupMatakuliah_onChanged,
                ),
              ],
            ),
            Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const LabelText(
                  labelText: "SKS",
                ),
                EditText(
                  controller: edtSKS,
                  isEnable: false,
                  textMode: TextInputType.number,
                  maxLength: 3,
                ),
              ],
            ),
            Row(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const LabelText(
                  labelText: "Skor",
                  isMandatory: true,
                ),
                EditText(
                  controller: edtSkor,
                  textMode: TextInputType.number,
                  maxLength: 3,
                  isMandatory: true,
                ),
              ],
            ),
            const SizedBox(height: 20),
            Row(
              crossAxisAlignment: CrossAxisAlignment.end,
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                ButtonExtender(
                  buttonText: "OK",
                  onPressed: btnOK_Click,
                ),
                Container(
                  margin: const EdgeInsets.only(left: 10),
                  child: ButtonExtender(
                    buttonText: "Cancel",
                    onPressed: btnCancel_Click,
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
