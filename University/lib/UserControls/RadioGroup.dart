import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';

import '../Common/GlobalStyle.dart';
import '../Dto/Base/DDLDto.dart';
import '../Dto/Base/EntityDto.dart';

enum RadioGroupType {
  Entity,
  ListItem,
}

enum Direction{
  Vertical,
  Horizontal
}

enum ChildPosition{
  Right,
  Bottom
}

class RadioListChild {
  final String name;
  final String value;
  final Widget? child;
  final ChildPosition childPosition;
  final bool isEnable;
  final bool isVisible;
  bool isSelected;
  
  RadioListChild({
    this.name = "",
    this.value = "",
    this.child,
    this.childPosition = ChildPosition.Right,
    this.isEnable = true,
    this.isVisible = true,
    this.isSelected = false,
  });
}

class RadioGroup extends StatefulWidget {
  final RadioGroupController controller;
  final RadioGroupType? radioGroupType;
  final List<RadioListChild> radioGroupList;
  final Function(String value)? onChange;
  final Direction direction;
  final String? initialValue;
  final bool? isEnable;
  final bool? isVisible;

  const RadioGroup({
    super.key,
    required this.controller,
    required this.radioGroupList,
    this.radioGroupType = RadioGroupType.Entity,
    this.onChange,
    this.isEnable,
    this.isVisible,
    this.direction = Direction.Vertical,
    this.initialValue,
  });

  @override
  createState() => RadioGroupState();
}

class RadioGroupState extends State<RadioGroup> {
  @override
  void initState() {
    super.initState();

    widget.controller.isEnable = widget.isEnable ?? true;
    widget.controller.isVisible = widget.isVisible ?? true;

    WidgetsBinding.instance.addPostFrameCallback((timeStamp) {
      if(widget.controller.selectedValue != null && widget.onChange != null) {
        widget.onChange!(widget.controller.selectedValue!);
      }
    });

  }

  @override
  void didChangeDependencies() {
    if(widget.initialValue != null && widget.radioGroupList.isNotEmpty) {
      RadioListChild objChild = widget.radioGroupList.firstWhere((obj) => obj.value == widget.initialValue, orElse: () => widget.radioGroupList[0],);
      widget.controller.selectedName = objChild.name;
      widget.controller.selectedValue = objChild.value;
    }

    super.didChangeDependencies();
  }

  @override
  void dispose() {
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    if(widget.controller.isVisible) {
      switch (widget.direction) {
        case Direction.Vertical:
          return Column(
            children: widget.radioGroupList.map((data) {
              return radioItemBuilder(data);
            }).toList(),
          );
        case Direction.Horizontal:
          return Row(
            mainAxisAlignment: MainAxisAlignment.start,
            mainAxisSize: MainAxisSize.max,
            children: widget.radioGroupList.map((data) {
              return radioItemBuilder(data);
            }).toList(),
          );
      }
    }

    return Container();
  }

  Widget radioItemBuilder(RadioListChild radioChild){
    List<Widget> lstRadioChild = [
      Radio(
        fillColor: WidgetStateProperty.all(GlobalStyle.primaryColor),
        value: radioChild.value,
        groupValue: widget.controller.selectedValue,
        onChanged: widget.controller.isEnable ? (value) {
          setState(() {
            widget.controller.selectedName = radioChild.name ;
            widget.controller.selectedValue = radioChild.value;
          });
          if(widget.onChange != null) {
            widget.onChange!(widget.controller.selectedValue!);
          }
        } : null,
      ),
      Container(
        constraints: const BoxConstraints(minWidth: 60),
        child: Text(
          radioChild.name,
          style: TextStyle(
            color: GlobalStyle.labelColor,
            fontSize: GlobalStyle.labelFontSize,
            fontWeight: FontWeight.normal,
          ),
        ),
      ),
    ];

    if(radioChild.child != null && radioChild.childPosition == ChildPosition.Bottom){
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          Row(
            children: lstRadioChild,
          ),
          Container(
              padding: const EdgeInsets.only(left: 25),
              child: radioGrandchildrenBuilder(radioChild.child)
          )

        ],
      );
    }

    if(radioChild.child != null && radioChild.childPosition == ChildPosition.Right){
      lstRadioChild.add(radioGrandchildrenBuilder(radioChild.child));
    }

    return Row(
      children: lstRadioChild,
    );
  }

  Widget radioGrandchildrenBuilder(Widget? child){
    if(child != null){
      return Container(
        padding: const EdgeInsets.only(left: 5),
        child: child,
      );
    }
    return Container();
  }
}

class RadioGroupController{
  bool isEnable = true;
  bool isVisible = true;
  String? selectedName;
  String? selectedValue;
}