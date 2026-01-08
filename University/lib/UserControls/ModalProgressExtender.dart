import 'package:flutter/material.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';
import '../../Common/CommonMethod.dart';

import '../Common/GlobalStyle.dart';

class ModalProgressExtender extends StatefulWidget {
  final Widget child;
  final ValueNotifier<bool> inAsyncCall;

  const ModalProgressExtender({
    super.key,
    required this.child,
    required this.inAsyncCall,
  });

  @override
  createState() => ModalProgressExtenderState();
}

class ModalProgressExtenderState extends State<ModalProgressExtender> {
  Widget customProgressIndicator() {
    return Column(
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
    );
  }

  @override
  Widget build(BuildContext context) {
    return ValueListenableBuilder(
      valueListenable: widget.inAsyncCall,
      builder: (context, value, child) {
        double opacity = 0.3;
        Offset? offset;

        List<Widget> widgetList = [];
        widgetList.add(widget.child);

        if (value) {
          Widget layOutProgressIndicator = customProgressIndicator();

          // if (offset == null) {
          //   debugPrint("layOutProgressIndicator - Center");
          //   layOutProgressIndicator = Center(child: customProgressIndicator());
          // } else {
          //   debugPrint("layOutProgressIndicator - Positioned");
          //   layOutProgressIndicator = Positioned(
          //     left: offset.dx,
          //     top: offset.dy,
          //     child: customProgressIndicator(),
          //   );
          // }

          List<Widget> modal = [
            Opacity(
              opacity: opacity,
              child: const ModalBarrier(dismissible: false, color: Colors.transparent),
            ),
            layOutProgressIndicator
          ];

          widgetList += modal;
        }

        return Stack(
          children: widgetList,
        );
    },
    );
  }

}