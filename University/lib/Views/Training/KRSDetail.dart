import 'package:flutter/material.dart';
import '../../Common/ModalBase.dart';
import '../../Dao/Training/KRSHeaderDao.dart';
import '../../Dto/Training/KRSDetailDto.dart';
import '../../Dto/Training/KRSHeaderDto.dart';
import '../../UserControls/ButtonExtender.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/ModalContent.dart';
import '../../UserControls/SearchBox.dart';

class KRSDetail extends StatefulWidget {
  static const String route = "/Training/KRSDetail";

  final KRSHeaderDto KRSHeader;
  final Function(KRSDetailDto obj)? callback;

  KRSDetail({
    required this.KRSHeader,
    this.callback,
  });

  @override
  createState() => KRSDetailState();
}

class KRSDetailState extends ModalBase<KRSDetail> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  SearchBoxController scbMatakuliah = SearchBoxController();
  EditTextController edtSKS = EditTextController();

  //endregion

  //region init
  @override
  void appInit(ModalPlatform modalPlatform) {
    scbMatakuliah.filter =
        "kode_fakultas = '${widget.KRSHeader.kode_fakultas}' AND kode_jurusan = '${widget.KRSHeader.kode_jurusan}'";
  }
  //endregion

  //region events
  void scbMatakuliah_onChanged(Map<String, dynamic> item) {
    edtSKS.numericValue = double.tryParse(item["SKS"]?.toString() ?? "0") ?? 0;
  }

  void btnOK_Click() async {
    if (form1.currentState!.validate()) {
      String strResult = "";
      showModalProgress.value = true;

      try {
        KRSDetailDto objDetail = collectionInfo();

        KRSHeaderDto dto = KRSHeaderDto();
        dto.nim = widget.KRSHeader.nim;
        dto.semester = widget.KRSHeader.semester;
        dto.kode_fakultas = widget.KRSHeader.kode_fakultas;
        dto.kode_jurusan = widget.KRSHeader.kode_jurusan;
        dto.objLine = objDetail;

        double currentTotal =
            (widget.KRSHeader.total_sks + objDetail.sks).toDouble();
        if (currentTotal > 24) {
          strResult =
              "Maaf, SKS matakuliah yang di pilih melebihi maksimum SKS pada semester ini!";
        } else {
          KRSHeaderDao dao = KRSHeaderDao();
          strResult = await dao.Save(dto);
        }
      } catch (ex) {
        strResult = ex.toString();
      }

      showModalProgress.value = false;

      if (strResult.isEmpty) {
        await MessageBox.show(
          context: context,
          message: "Simpan berhasil",
          title: "Simpan Sukses",
          dialogButton: DialogButton.OK,
        );

        closeModalPopup(DialogResult.OK);
      } else {
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
  //endregion

  //region Methods
  KRSDetailDto collectionInfo() {
    KRSDetailDto objInfo = KRSDetailDto();
    objInfo.nim = widget.KRSHeader.nim;
    objInfo.semester = widget.KRSHeader.semester;
    objInfo.kode_matakuliah = scbMatakuliah.text;
    objInfo.sks = edtSKS.numericValue;
    return objInfo;
  }

  @override
  void modalBehaviour(ModalMode modalMode) {
    switch (modalMode) {
      case ModalMode.Add:
        {
          setState(() {
            scbMatakuliah.isEnable = true;
            edtSKS.isEnable = false;
          });
          break;
        }
      case ModalMode.Edit:
        {
          setState(() {
            scbMatakuliah.isEnable = false;
            edtSKS.isEnable = false;
          });
          break;
        }
      case ModalMode.View:
        {
          setState(() {
            scbMatakuliah.isEnable = false;
            edtSKS.isEnable = false;
          });
          break;
        }
    }
  }
  //endregion

  //region Layout
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
                SearchBoxFormField(
                  controller: scbMatakuliah,
                  entity: "MTKL-02",
                  title: "List of Mata Kuliah",
                  isMandatory: true,
                  onLostFocus: scbMatakuliah_onChanged,
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
                  textMode: TextInputType.number,
                  maxLength: 3,
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
//endregion
}
