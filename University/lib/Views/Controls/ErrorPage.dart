import 'package:flutter/material.dart';
import 'package:flutter/cupertino.dart';
import '../../Common/PageBase.dart';

import 'Login.dart';

class ErrorPage extends StatefulWidget {
  static const String route = "/Controls/ErrorPage";

  final String errorTitle;
  final String errorMessage;

  const ErrorPage({
    super.key,
    this.errorTitle = "Page Not Found",
    this.errorMessage = "We couldn't find the page you're looking for"
  });

  @override
  createState() => ErrorPageState();
}

class ErrorPageState extends PageBase<ErrorPage> {
  //region Variables
  String strTitle = "";
  String strMessage = "";
  //endregion

  //region Init
  @override
  void appInit(PagePlatform pagePlatform) {
    // TODO: implement appInit
    setState(() {
      strTitle = widget.errorTitle;
      strMessage = widget.errorMessage;

      if(args.containsKey("t")){
        strTitle = args["t"];
      }

      if(args.containsKey("m")){
        strMessage = args["m"];
      }
    });
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
      body: Container(
        padding: EdgeInsets.all(20),
        alignment: Alignment.center,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Text(strTitle, style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),),
            Text(strMessage)
          ],
        ),
      ),
    );
  }
//endregion

}