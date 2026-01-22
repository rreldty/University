
import 'package:flutter/cupertino.dart';
import 'package:university/Common/AppConfig.dart';
import 'package:university/UserControls/Lookup.dart';

import '../../Common/ModalBase.dart';
import '../../Dao/Training/KrsHeaderDao.dart';
import '../../Dto/Training/KrsDetailDto.dart';
import '../../Dto/Training/KrsHeaderDto.dart';
import '../../UserControls/ButtonExtender.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/ModalContent.dart';

class KrsDetail extends StatefulWidget {
  static const String route = "/Training/KrsDetail";

  final KrsHeaderDto objKrsHeader;
  final Function(KrsDetailDto obj)? callback;

  KrsDetail({
    required this.objKrsHeader,
    this.callback,
  });

  @override
  createState() => KrsDetailState();
}

class KrsDetailState extends ModalBase<KrsDetail>  {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  var lupKode_Matakuliah = LookupController();
  var edtSKS = EditTextController();
  KrsHeaderDto dtoHeader = KrsHeaderDto();

  //region init
  @override
  void appInit(ModalPlatform modalPlatform) {
    dtoHeader = widget.objKrsHeader;
    modalBehaviour(ModalMode.Add);
  }
  //endregion

  //region events
  void btnOK_Click() async {
    String strResult = "";

    if (form1.currentState!.validate()) {
      showModalProgress.value = true;


      dtoHeader.objKrsDetail = collectionInfo();

      try {
        KrsHeaderDao dao = KrsHeaderDao();
        strResult = await dao.Save(dtoHeader);
      } catch (ex) {
        strResult = ex.toString();
      }

      showModalProgress.value = false;

      if (strResult.isEmpty) {
        await MessageBox.show(context: context,
            message: "Save successfully",
            title: "Save Success",
            dialogButton: DialogButton.OK);
        closeModalPopup(DialogResult.OK);
      } else {
        await MessageBox.show(context: context,
            message: strResult,
            title: "Save Failed",
            dialogButton: DialogButton.OK);
      }
    }
  }

  void btnCancel_Click() {
    closeModalPopup(DialogResult.Cancel);
  }
  //endregion

  //region Methods

  KrsDetailDto collectionInfo() {
    KrsDetailDto objInfo = KrsDetailDto();
    objInfo.nim = dtoHeader.nim;
    objInfo.semester = dtoHeader.semester;
    objInfo.line_no = 0;
    objInfo.kode_matakuliah = lupKode_Matakuliah.text.trim();
    objInfo.sks = edtSKS.numericValue;
    return objInfo;
  }

  @override
  void modalBehaviour(ModalMode modalMode) {
    switch (modalMode) {
      case ModalMode.Add:
        {
          setState(() {
            lupKode_Matakuliah.filter = "kode_jurusan = '" + dtoHeader.kode_jurusan  + "'";
          });
          break;
        }
      case ModalMode.Edit:
        {
          break;
        }
      case ModalMode.View:
        {
          break;
        }
    }
  }

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
                      width: 150,
                      isMandatory: true,
                    ),
                    Lookup(
                      controller: lupKode_Matakuliah,
                      isMandatory: true,
                      entity: "MTKL-02",
                      onLostFocus: (item) {
                        setState(() {
                          edtSKS.numericValue = item["SKS"];
                        });
                      },

                    ),
                  ],
                ),
                Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const LabelText(
                      labelText: "SKS",
                      width: 150,
                    ),
                    EditText(
                      controller: edtSKS,
                      textMode: TextInputType.number,
                      numericType: NumericType.Unit,
                      isEnable: false,
                    ),
                  ],
                ),
                Row(
                  crossAxisAlignment: CrossAxisAlignment.end,
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
              ]
          );
        }
    );
  }
}