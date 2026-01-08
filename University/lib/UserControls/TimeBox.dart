import 'package:flutter/cupertino.dart';
import 'package:intl/intl.dart';
import '../../Common/CommonMethod.dart';
import 'package:flutter/material.dart';

import '../Common/GlobalStyle.dart';
import 'EditText.dart';

class TimeBoxFormField extends FormField<double> {
  TimeBoxFormField({
    super.key,
    super.autovalidateMode,
    required TimeController controller,
    Function(double value)? onSelected,
    bool isMandatory = false,
    bool? isEnable,
    bool isVisible = true,
  }):super(
    initialValue: controller.value,
    builder: (state){
      void onSelectedHandler(double value) {
        state.didChange(value);
        if (onSelected != null) {
          onSelected(value);
        }
      }

      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          TimeBox(
            controller: controller,
            isEnable: isEnable,
            isVisible: isVisible,
            colorBorder: state.hasError ? Colors.red : null,
            onSelected: onSelectedHandler,
          ),
          if (state.hasError) ...[
            Container(
              padding: const EdgeInsets.only(top: 8, left: 8),
              child: Text(
                state.errorText!,
                style: GlobalStyle.errorStyle,
              ),
            ),
          ],
        ],
      );
    },
    validator: (value) {
      if (isMandatory && value == 0) return "Please fill this field";

      return null;
    },

  );

}

class TimeBox extends StatefulWidget {
  final TimeController controller;
  final bool? isEnable;
  final bool isVisible;
  final Function(double value)? onSelected;
  final Color? colorBorder;

  const TimeBox({
    super.key,
    required this.controller,
    this.isEnable,
    this.isVisible = true,
    this.onSelected,
    this.colorBorder,
  });

  @override
  createState() => TimeBoxState();
}

class TimeBoxState extends State<TimeBox> {
  String strTime = "";
  DateTime changedTime = DateTime.now();

  @override
  void initState() {
    if (widget.isEnable != null) {
      widget.controller.isEnable = widget.isEnable ?? true;
    }

    if(widget.controller.value == 0){
      widget.controller._text = "00:00";
    }else{
      widget.controller._text = CommonMethod.NumericToTimeString(widget.controller.value);
    }

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    if (widget.isVisible) {
      return OutlinedButton.icon(
        style: OutlinedButton.styleFrom(
          side: widget.colorBorder != null ? BorderSide(color: widget.colorBorder!) : null,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(5.0),
          ),
        ),
        onPressed: (){
          showDialog(
            context: context,
            barrierDismissible: false,
            builder: (BuildContext context) {
              // debugPrint("onShow - ${widget.controller.value}");

              DateTime initialTime = DateTime.now();

              if(widget.controller.value > 0){
                String sTime = widget.controller.value.toString();
                sTime = sTime.length > 4 ? sTime.substring(0,4) : sTime;
                sTime = (10000 + double.parse(sTime)).toString().substring(1);
                int h = int.parse(sTime.substring(0, 2));
                int m = int.parse(sTime.substring(2, 4));
                // debugPrint(sTime);
                // debugPrint(sTime.substring(0, 2));
                // debugPrint(sTime.substring(2, 4));

                initialTime = DateTime(DateTime.now().year, DateTime.now().month, DateTime.now().day, h, m);
                // debugPrint(initialTime.toString());
              }

              return AlertDialog(
                content: SizedBox(
                    width: 300,
                    height: 200,
                    child: CupertinoDatePicker(
                      use24hFormat: true,
                      initialDateTime: initialTime,
                      mode: CupertinoDatePickerMode.time,
                      onDateTimeChanged: (value) {
                        changedTime = value;
                      },
                    )
                ),
                actions: [
                  TextButton(
                    child: Text("OK", style: TextStyle(color: GlobalStyle.primaryColor),),
                    onPressed: () {
                      double dblTime = double.parse(DateFormat("HHmm", "en_US").format(changedTime));
                      setState(() {
                        widget.controller._text = DateFormat("HH:mm", "en_US").format(changedTime);
                        widget.controller.value = dblTime;
                        // debugPrint("onPressed - ${widget.controller.value}");
                      });
                      Navigator.of(context, rootNavigator: true).pop();
                      if(widget.onSelected != null){
                        widget.onSelected!(dblTime);
                      }
                    },
                  ),
                  TextButton(
                    child: Text("Cancel", style: TextStyle(color: GlobalStyle.primaryColor),),
                    onPressed: () {
                      Navigator.of(context, rootNavigator: true).pop();
                    },
                  ),
                ],
              );
            },
          );
        },
        icon: Icon(
          Icons.watch_later_outlined,
          size: 20,
          color: GlobalStyle.primaryColor,
        ),
        label: Text(
          widget.controller._text,
          style: const TextStyle(fontSize: 14, color: Colors.black),
        ),
      );
    } else {
      return Container();
    }
  }

}

class TimeController{
  bool isEnable = true;
  double value = 0;
  String _text = "";
}