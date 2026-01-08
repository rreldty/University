import 'package:flutter/material.dart';
import '../../Common/GlobalStyle.dart';
import '../../Dto/Base/ModalPopupResult.dart';

import 'ButtonExtender.dart';

class ModalDialog {
  static Future<ModalPopupResult?> show(
      {
        required BuildContext context,
        String title = "",
        required Widget child,
        double modalWidth = 0,
        double modalHeight = 0,
      }) {

    modalWidth = modalWidth == 0 ? MediaQuery.of(context).size.width.floor() / 2 + 200 : modalWidth;
    modalHeight = modalHeight == 0 ? MediaQuery.of(context).size.height.floor() - 20 : modalHeight;

    return showDialog<ModalPopupResult>(
        context: context,
        barrierDismissible: false, // user must tap button for close dialog!
        builder: (BuildContext context) {
          return StatefulBuilder(
            builder: (context, setState) {
              return SimpleDialog(
                  backgroundColor: Colors.transparent,
                  insetPadding: const EdgeInsets.all(0),
                  contentPadding: const EdgeInsets.all(0),
                  children: [
                    Container(
                      width: modalWidth,
                      height: modalHeight,
                      padding: const EdgeInsets.all(0),
                      decoration: BoxDecoration(
                        color: Colors.white,
                        borderRadius: BorderRadius.circular(5),
                      ),
                      child:Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisSize: MainAxisSize.max,
                        children: [
                          Container(
                            width: double.infinity,
                            padding: const EdgeInsets.fromLTRB(20, 5, 0, 5),
                            decoration: BoxDecoration(
                              color: GlobalStyle.toolbarBackgroundColor,
                              borderRadius: const BorderRadius.only(topLeft: Radius.circular(5), topRight: Radius.circular(5)),
                              // border: Border(
                              //   bottom: BorderSide(color: GlobalStyle.toolbarBorderColor, width: 2),
                              // ),
                            ),
                            child: Text(title, style: TextStyle(fontSize: 18.0, fontWeight: FontWeight.normal, color: GlobalStyle.toolbarColor,)),
                          ),
                          Expanded(
                            child: child,
                          ),
                        ],
                      ),
                    ),
                  ]
              );
            },
          );
        });
  }
}
