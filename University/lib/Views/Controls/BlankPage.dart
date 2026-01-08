import 'package:flutter/material.dart';
import 'package:flutter/cupertino.dart';
import '../../Common/PageBase.dart';

class BlankPage extends StatefulWidget {
  static const String route = "/Controls/BlankPage";

  const BlankPage({super.key});

  @override
  createState() => BlankPageState();
}

class BlankPageState extends PageBase<BlankPage> {
  //region Variables
  //endregion

  //region Init
  @override
  void appInit(PagePlatform pagePlatform) {
    // TODO: implement appInit
  }
  //endregion

  //region Events
  @override
  void pageBehaviour(pageMode) {
    // TODO: implement pageBehaviour
  }
  //endregion

  //region Layout
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(),
    );
  }
//endregion

}