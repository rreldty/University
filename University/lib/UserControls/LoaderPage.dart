import 'package:flutter/material.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import '../../Common/GlobalStyle.dart';
import '../../Common/PageBase.dart';

class LoaderPage extends StatefulWidget {
  static const String route = "/UserControls/LoaderPage";

  const LoaderPage({super.key});

  @override
  createState() => LoaderPageState();
}

class LoaderPageState extends PageBase<LoaderPage> {
  //region Variables
  String strLoader = "";
  //endregion

  //region Init
  @override
  void appInit(PagePlatform pagePlatform) {
    strLoader = args["loader"] ?? "";
    debugPrint("LoaderPage-Init-$strLoader");
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
      backgroundColor: Colors.white,
      body: Column(
        mainAxisSize: MainAxisSize.max,
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          SpinKitFadingCube(
            color: GlobalStyle.modalProgressColor,
            size: 80.0,
          ),
          const SizedBox(height: 20,),
          Text("Loading",
            style: TextStyle(
              decoration: TextDecoration.none,
              fontSize: 20,
              color: GlobalStyle.modalProgressTextColor,
              fontFamily: GlobalStyle.fontFamily,
            ),
          )
        ],
      ),
    );
  }

//endregion

}