import "package:universal_html/html.dart" as html;
import 'package:flutter/foundation.dart';
import 'package:flutter/cupertino.dart';
import '../Dto/Base/ModalPopupResult.dart';
import '../UserControls/MessageBox.dart';

enum ModalMode { Add, Edit, View }

enum ModalPlatform { web, phone, desktop }

abstract class ModalBase<T extends StatefulWidget> extends State<T>{
  late Map<String, String> args;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    // if(ModalRoute.of(context)!.settings.arguments != null) {
    //   args = ModalRoute
    //       .of(context)!
    //       .settings
    //       .arguments as Map<String, String>;
    // }
    // baseInit();
    // appInit();
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
      await Future.delayed(const Duration(milliseconds: 10));
      args = {};
      if(ModalRoute.of(context)!.settings.arguments != null) {
        args = ModalRoute
            .of(context)!
            .settings
            .arguments as Map<String, String>;
      }
      baseInit();
      await Future.delayed(const Duration(milliseconds: 10));
      double screenWidth = MediaQuery.sizeOf(context).width;
      double screenHeight = MediaQuery.sizeOf(context).height;

      ModalPlatform modalPlatform = ModalPlatform.web;

      if(screenWidth < 600){
        modalPlatform = ModalPlatform.phone;
      }

      appInit(modalPlatform);
    });
  }

  void baseInit() async {

  }

  void appInit(ModalPlatform modalPlatform);

  void modalBehaviour(ModalMode modalMode);

  void initBuild(BuildContext context){
    if(kIsWeb){
      html.document.addEventListener('keydown', (event) {
        if (event.type == 'tab') {
          event.preventDefault();
        }
      });
    }
  }

  void closeModalPopup(DialogResult? dlgResult, { Map<String, dynamic>? map }){
    ModalPopupResult objResult = ModalPopupResult();
    objResult.dialogResult = dlgResult;
    objResult.map = map;

    Navigator.of(context, rootNavigator: true).pop(objResult);
  }
}