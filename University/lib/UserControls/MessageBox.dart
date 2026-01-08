import 'package:flutter/material.dart';
import '../../Common/GlobalStyle.dart';

enum DialogResult { Cancel, OK, Retry }
enum DialogButton { OK, OkCancel, YesNo, RetryCancel, RetryOkCancel }

class MessageBox{
  static Future<DialogResult?> show({
    required BuildContext context,
    String title = "",
    String message = "",
    DialogButton dialogButton = DialogButton.OK
  }){
    List<Widget> lst = [];

    TextButton btnOK = TextButton(
      child: Text("OK", style: TextStyle(color: GlobalStyle.primaryColor),),
      onPressed: () {
        Navigator.of(context, rootNavigator: true).pop(DialogResult.OK);
      },
    );

    TextButton btnCancel = TextButton(
      child: Text("Cancel", style: TextStyle(color: GlobalStyle.primaryColor)),
      onPressed: () {
        Navigator.of(context, rootNavigator: true).pop(DialogResult.Cancel);
      },
    );

    TextButton btnYes = TextButton(
      child: Text("Yes", style: TextStyle(color: GlobalStyle.primaryColor),),
      onPressed: () {
        Navigator.of(context, rootNavigator: true).pop(DialogResult.OK);
      },
    );

    TextButton btnNo = TextButton(
      child: Text("No", style: TextStyle(color: GlobalStyle.primaryColor)),
      onPressed: () {
        Navigator.of(context, rootNavigator: true).pop(DialogResult.Cancel);
      },
    );

    TextButton btnRetry = TextButton(
      child: Text("Retry", style: TextStyle(color: GlobalStyle.primaryColor)),
      onPressed: () {
        Navigator.of(context, rootNavigator: true).pop(DialogResult.Retry);
      },
    );

    switch(dialogButton){
      case DialogButton.OK:{
        lst.add(btnOK);
        break;
      }
      case DialogButton.OkCancel:{
        lst.add(btnOK);
        lst.add(btnCancel);
        break;
      }
      case DialogButton.YesNo:{
        lst.add(btnYes);
        lst.add(btnNo);
        break;
      }
      case DialogButton.RetryCancel:{
        lst.add(btnRetry);
        lst.add(btnCancel);
        break;
      }
      case DialogButton.RetryOkCancel:{
        lst.add(btnRetry);
        lst.add(btnOK);
        lst.add(btnCancel);
        break;
      }
      default:{
        lst.add(btnOK);
        break;
      }
    }

    return showDialog<DialogResult>(
      context: context,
      barrierDismissible: false, // user must tap button for close dialog!
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(title, style: TextStyle(fontSize: 18.0, fontWeight: FontWeight.normal, color: GlobalStyle.primaryColor,)),
          content: Text(message, style: const TextStyle(fontSize: 16.0, fontWeight: FontWeight.normal, color: Colors.black),),
          actions: lst,
        );
      },
    );
  }
}