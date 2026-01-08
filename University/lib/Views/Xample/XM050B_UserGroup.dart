import 'package:flutter/material.dart';
import '../../Common/CommonMethod.dart';
import '../../Common/ModalBase.dart';
import '../../Dao/Zystem/ZUG2Dao.dart';
import '../../Dao/Zystem/ZUSRDao.dart';
import '../../Dto/Zystem/ZUG1Dto.dart';
import '../../Dto/Zystem/ZUG2Dto.dart';
import '../../Dto/Zystem/ZUSRDto.dart';
import '../../UserControls/ButtonExtender.dart';
import '../../UserControls/DataGridExtender.dart';
import '../../UserControls/ModalContent.dart';
import "../../UserControls/ModalProgressExtender.dart";
import '../../Common/PageBase.dart';
import '../../Dao/Zystem/ZAPPDao.dart';
import '../../Dto/Base/GlobalDto.dart';
import '../../Dto/Zystem/ZAPPDto.dart';
import '../../UserControls/CheckboxExtender.dart';
import '../../UserControls/EditText.dart';
import '../../UserControls/LabelText.dart';
import '../../UserControls/MessageBox.dart';
import '../../UserControls/PageContent.dart';
import '../../UserControls/ToolbarBox.dart';

class XM050B_UserGroup extends StatefulWidget {
  static const String route = "/Xample/XM050B_UserGroup";

  final ZUG1Dto objZUG1;
  final Function(ZUG2Dto obj)? callback;

  XM050B_UserGroup({
    required this.objZUG1,
    this.callback,
  });

  @override
  createState() => XM050B_UserGroupState();
}

class XM050B_UserGroupState extends ModalBase<XM050B_UserGroup>  {
  //region Variables
  final form1 = GlobalKey<FormState>();
  ValueNotifier<bool> showModalProgress = ValueNotifier<bool>(false);
  var edtZUUSNO = EditTextController();
  var edtZUUSNA = EditTextController();
  var edtZUNICK = EditTextController();
  var dgeUser = DataGridExtenderController();

  List<ZUG2Dto> lstZUG2 = [];
  //region init
  @override
  void appInit(ModalPlatform modalPlatform) {

  }
  //endregion

  //region events
  void btnSearch_Click() {
    setState(() {
      dgeUser.isRefresh = true;
    });
  }

  void btnOK_Click() async {
    String strResult = "";

    if (form1.currentState!.validate()) {
      showModalProgress.value = true;

      //Collect grid checked
      ZUG2Dto objLine = collectionInfo();

      try {
        List<ZUSRDto>? lstT = (dgeUser.gridItem as List).map(
                (e) => ZUSRDto.fromJson(e)).toList();
        //objLine.listZUSR = lstT; //yg ditampung dlam grid

        ZUG2Dao dao = ZUG2Dao();
        strResult = await dao.Save(objLine);
      } catch (ex) {
        strResult = ex.toString();
      }

      showModalProgress.value = false;

      if (strResult.isEmpty) {
        await MessageBox.show(context: context,
            message: "Save successfully",
            title: "Save Success",
            dialogButton: DialogButton.OK);

        if(widget.callback != null){
          widget.callback!(objLine);
        }

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

  ZUG2Dto collectionInfo() {
    ZUG2Dto objInfo = ZUG2Dto();
    objInfo.ZHCONO = "";
    objInfo.ZHBRNO = "";
    objInfo.ZHUGNO = widget.objZUG1.ZGUGNO;
    objInfo.ZHCRUS = GlobalDto.USNO;
    objInfo.ZHCHUS = GlobalDto.USNO;
    return objInfo;
  }

  Future<dynamic> getListUser(intPageNumber, intPageSize, strSqlFilter, strSqlSort) async {
    ZUSRDto objInfo = ZUSRDto(
      ZHUGNO: widget.objZUG1.ZGUGNO,
      ZUUSNO: edtZUUSNO.text,
      ZUUSNA: edtZUUSNA.text,
      ZUNICK: edtZUNICK.text,
      PageNumber: intPageNumber,
      PageSize: intPageSize,
    );

    ZUSRDao dao = ZUSRDao();
    List<ZUSRDto> lst = await dao.listPagingNotInUserGroup(objInfo);
    return lst;
  }

  @override
  void modalBehaviour(ModalMode modalMode) {
    switch (modalMode) {
      case ModalMode.Add:
        {
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
                      labelText: "USNO",
                    ),
                    EditText(
                      controller: edtZUUSNO,
                      textMode: TextInputType.text,
                    ),
                  ],
                ),
                Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const LabelText(
                      labelText: "USNA",
                    ),
                    EditText(
                      controller: edtZUUSNA,
                      textMode: TextInputType.text,

                    ),
                  ],
                ),
                Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const LabelText(
                      labelText: "NICK",
                    ),
                    EditText(
                      controller: edtZUNICK,
                      textMode: TextInputType.text,
                    ),
                  ],
                ),
                Row(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    ButtonExtender(
                      buttonText: "Search",
                      onPressed: btnSearch_Click,
                    ),
                  ],
                ),
                DataGridExtender(
                  controller: dgeUser,
                  futureData: getListUser,
                  width: constraint.maxWidth,
                  deColumns: [
                    DEColumn(
                      columnName: "IsSelected",
                      columnType: DEColumnType.DECheckbox,
                    ),
                    DEColumn(
                      columnName: "ZUUSNO",
                      columnType: DEColumnType.String,
                      columnWidth: 150,
                    ),
                    DEColumn(
                      columnName: "ZUUSNA",
                      columnType: DEColumnType.String,
                      //columnWidth: 0,
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