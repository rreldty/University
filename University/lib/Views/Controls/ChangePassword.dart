import 'package:university/Dto/Base/GlobalDto.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../../Common/ModalBase.dart';
import '../../../UserControls/ButtonExtender.dart';
import '../../../UserControls/ComboBox.dart';
import '../../../UserControls/EditText.dart';
import '../../../UserControls/LabelText.dart';
import '../../../UserControls/Lookup.dart';
import '../../../UserControls/MessageBox.dart';
import '../../../UserControls/ModalContent.dart';
import '../../Common/Encryption.dart';
import '../../Dao/Zystem/ZUSRDao.dart';
import '../../Dto/Zystem/ZUSRDto.dart';

class ChangePassword extends StatefulWidget {
  static const String route = "/Views/Controls/ChangePassword";


  ChangePassword({
    super.key
  });

  @override
  createState() => ChangePasswordState();
}

class ChangePasswordState extends ModalBase<ChangePassword> {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  var edtZUUSNO = EditTextController();
  var edtZUPSWD = EditTextController();
  var edtZUPSNW = EditTextController();
  var edtZUPSRT = EditTextController();
  bool edtZUUSNOenable = false;
  Map obj = new Map();

  // Map args = new Map();

  String SQSRDN = "";
  bool Edit = false;
  ZUSRDto dtoHeader = new ZUSRDto();

  //List<ZAUMDto> lstZAUM = [];
  //region Init

  //App Init permulaan
  @override
  void appInit(ModalPlatform modalPlatform) {
    modalBehaviour(ModalMode.Add);
  }

  //endregion
  //region Events
  void btnSave_Click() async {
    String strResult = "";
    if (form1.currentState!.validate()) {
      showModalProgress.value = true;

      if (edtZUPSNW.text != edtZUPSRT.text) {
        strResult = "New Password and Re-type Does not Match";
      }

      if (strResult.isEmpty) {
        try {
          ZUSRDao dao = ZUSRDao();
          ZUSRDto dto = ZUSRDto();
          dto.ZUCONO = GlobalDto.CONO;
          dto.ZUBRNO = GlobalDto.BRNO;
          dto.ZUUSNO = GlobalDto.USNO;
          dto.ZUPSWD = edtZUPSWD.text;
          dto.NewPassword = edtZUPSNW.text;
          strResult = await dao.ChangePassword(dto);
        } catch (ex) {
          strResult = ex.toString();
        }
      }

      showModalProgress.value = false;

      if (strResult.isEmpty) {
        closeModalPopup(DialogResult.OK,
            map: {"ZUUSNO": dtoHeader.ZUUSNO}); //kirim
      } else {
        await MessageBox.show(
            context: context,
            message: strResult,
            title: "Save Failed",
            dialogButton: DialogButton.OK);
      }
    }
  }

  void btnNew_Click() {
    modalBehaviour(ModalMode.Add);
  }

  void btnBack_Click() {
    closeModalPopup(DialogResult.Cancel);
    //closeModalPopup( DialogResult.OK, map: obj);
  }

  //endregion

  //region Methods
  void getLine() async {
    String strResult = "";

    showModalProgress.value = true;

    try {
      setState(() {
        // lupSRITNO.text = dtoLine.SRITNO;
        // edtSRITPR.numericValue = dtoLine.SRITPR;
        // edtSRITQT.numericValue = dtoLine.SRITQT;
        // edtSRGSAM.numericValue = dtoLine.SRGSAM;
      });
    } catch (ex) {
      strResult = ex.toString();
    }

    showModalProgress.value = false;

    if (strResult.isNotEmpty) {
      await MessageBox.show(
          context: context, message: strResult, title: "Get Data");
    }
  }

  @override
  void modalBehaviour(modalMode) {
    switch (modalMode) {
      case ModalMode.Add:
        {
          setState(() {
            edtZUUSNO.text = GlobalDto.USNO;
            edtZUPSWD.text = "";
            edtZUPSNW.text = "";
            edtZUPSRT.text = "";
          });

          break;
        }
      case ModalMode.Edit:
        {
          setState(() {});
          break;
        }
      case ModalMode.View: //setting ketika mode view, true/false
        {
          setState(() {});
          break;
        }
    }
  }

//endregion

//region Layout
  @override
  Widget build(BuildContext context) {
    return ModalContent(
        //pop up pakai ModalContent, page -> PageContent
        formKey: form1,
        showModalProgress: showModalProgress,
        builder: (context, constraints) {
          return Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            mainAxisAlignment: MainAxisAlignment.start,
            mainAxisSize: MainAxisSize.max,
            children: <Widget>[
              Row(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Container(
                    child: LabelText(
                      labelText: "User ID",
                      isMandatory: true,
                      width: 165,
                    ),
                  ),
                  Container(
                    child: EditText(
                      controller: edtZUUSNO,
                      isEnable: false,
                      isMandatory: true,
                    ),
                  ),
                ],
              ),
              Row(
                children: [
                  Container(
                    child: const LabelText(
                      labelText: "Password",
                      isMandatory: true,
                    ),
                  ),
                  Container(
                    child: EditText(
                      controller: edtZUPSWD,
                      isPassword: true,
                      isEnforcePasswordComplexity: true,
                      // minLength: 8,
                      isMandatory: true,
                      textMode: TextInputType.text,
                      maxLength: 500,
                      textInputAction: TextInputAction.next,
                    ),
                  ),
                ],
              ),
              Row(
                children: [
                  Container(
                    child: const LabelText(
                      labelText: "New Password",
                      isMandatory: true,
                    ),
                  ),
                  Container(
                    child: EditText(
                      controller: edtZUPSNW,
                      isPassword: true,
                      isEnforcePasswordComplexity: true,
                      minLength: 8,
                      isMandatory: true,
                      textMode: TextInputType.text,
                      maxLength: 500,
                      textInputAction: TextInputAction.next,
                    ),
                  ),
                ],
              ),
              Row(
                children: [
                  Container(
                    child: const LabelText(
                      labelText: "Re-type Password",
                      isMandatory: true,
                    ),
                  ),
                  Container(
                    child: EditText(
                      controller: edtZUPSRT,
                      isPassword: true,
                      isEnforcePasswordComplexity: true,
                      minLength: 8,
                      isMandatory: true,
                      textMode: TextInputType.text,
                      maxLength: 500,
                      textInputAction: TextInputAction.next,
                    ),
                  ),
                ],
              ),
              Container(
                width: constraints.maxWidth - 45,
                padding: EdgeInsets.only(top: 10),
                child: Row(
                  mainAxisSize: MainAxisSize.max,
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    Container(
                      child: ButtonExtender(
                        buttonText: "Ok",
                        onPressed: btnSave_Click,
                      ),
                    ),
                    Container(
                      margin: EdgeInsets.only(left: 20),
                      child: ButtonExtender(
                        buttonText: "Cancel",
                        onPressed: btnBack_Click,
                      ),
                    ),
                  ],
                ),
              ),
            ],
          );
        });
  }
}
