import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import '../../Common/GlobalStyle.dart';

import '../Dto/Base/MultiselectDto.dart';
import 'MultiselectDialog.dart';

/// A customizable InkWell widget that opens the MultiSelectDialog
// ignore: must_be_immutable
class MultiselectField<V> extends FormField<List<V>> {
  /// Style the Container that makes up the field.
  final BoxDecoration? decoration;

  /// Set text that is displayed on the button.
  final Text? buttonText;

  /// Specify the button icon.
  final Icon? buttonIcon;

  /// The text at the top of the dialog.
  final Widget? title;

  /// List of items to select from.
  final List<MultiselectDto<V>> items;

  /// Fires when the an item is selected / unselected.
  final void Function(List<V>)? onSelectionChanged;

  /// The list of selected values before interaction.
  final List<V>? initialValue;

  /// Fires when confirm is tapped.
  final void Function(List<V>)? onConfirm;

  /// Toggles search functionality.
  final bool searchable;

  /// Text on the confirm button.
  final Text? confirmText;

  /// Text on the cancel button.
  final Text? cancelText;

  /// Set the color of the space outside the BottomSheet.
  final Color? barrierColor;

  /// Sets the color of the checkbox or chip when it's selected.
  final Color? selectedColor;

  /// Give the dialog a fixed height
  final double? height;

  /// A function that sets the color of selected items based on their value.
  /// It will either set the chip color, or the checkbox color depending on the list type.
  final Color Function(V)? colorator;

  /// Set the background color of the dialog.
  final Color? backgroundColor;

  /// Color of the chip body or checkbox border while not selected.
  final Color? unselectedColor;

  /// Style the text on the chips or list tiles.
  final TextStyle? itemsTextStyle;

  /// Style the text on the selected chips or list tiles.
  final TextStyle? selectedItemsTextStyle;

  /// Set the color of the check in the checkbox
  final Color? checkColor;

  final AutovalidateMode autovalidateMode;
  final FormFieldValidator<List<V>>? validator;
  final FormFieldSetter<List<V>>? onSaved;
  final GlobalKey<FormFieldState>? key;
  
  late FormFieldState<List<V>> state;

  MultiselectField({
    required this.items,
    this.onConfirm,
    this.title,
    this.buttonText,
    this.buttonIcon,
    this.decoration,
    this.onSelectionChanged,
    this.searchable = true,
    this.confirmText,
    this.cancelText,
    this.barrierColor,
    this.selectedColor,
    this.height,
    this.colorator,
    this.backgroundColor,
    this.unselectedColor,
    this.itemsTextStyle,
    this.selectedItemsTextStyle,
    this.checkColor,
    this.onSaved,
    this.validator,
    this.initialValue,
    this.autovalidateMode = AutovalidateMode.disabled,
    this.key,
  }) : super(
      key: key,
      onSaved: onSaved,
      validator: validator,
      autovalidateMode: autovalidateMode,
      initialValue: initialValue ?? [],
      builder: (FormFieldState<List<V>> state) {
        _MultiselectFieldView field =
        _MultiselectFieldView<V>(
          title: title,
          items: items,
          buttonText: buttonText,
          buttonIcon: buttonIcon,
          decoration: decoration,
          onConfirm: onConfirm,
          onSelectionChanged: onSelectionChanged,
          initialValue: initialValue,
          searchable: searchable,
          confirmText: confirmText,
          cancelText: cancelText,
          barrierColor: barrierColor,
          selectedColor: selectedColor,
          height: height,
          colorator: colorator,
          backgroundColor: backgroundColor,
          unselectedColor: unselectedColor,
          itemsTextStyle: itemsTextStyle,
          selectedItemsTextStyle: selectedItemsTextStyle,
          checkColor: checkColor,
        );
        return _MultiselectFieldView<V>._withState(field as _MultiselectFieldView<V>, state);
      });
}

// ignore: must_be_immutable
class _MultiselectFieldView<V> extends StatefulWidget {
  final BoxDecoration? decoration;
  final Text? buttonText;
  final Icon? buttonIcon;
  final Widget? title;
  final List<MultiselectDto<V>> items;
  final void Function(List<V>)? onSelectionChanged;
  final List<V>? initialValue;
  final void Function(List<V>)? onConfirm;
  final bool? searchable;
  final Text? confirmText;
  final Text? cancelText;
  final Color? barrierColor;
  final Color? selectedColor;
  final double? height;
  final String searchHint;
  final Color Function(V)? colorator;
  final Color? backgroundColor;
  final Color? unselectedColor;
  final Icon? searchIcon;
  final Icon? closeSearchIcon;
  final TextStyle? itemsTextStyle;
  final TextStyle? selectedItemsTextStyle;
  final TextStyle? searchTextStyle;
  final TextStyle? searchHintStyle;
  final Color? checkColor;
  late FormFieldState<List<V>> state;

  _MultiselectFieldView({
    required this.items,
    this.title,
    this.buttonText,
    this.buttonIcon,
    this.decoration,
    this.onSelectionChanged,
    this.onConfirm,
    this.initialValue,
    this.searchable,
    this.confirmText,
    this.cancelText,
    this.barrierColor,
    this.selectedColor,
    this.searchHint = "",
    this.height,
    this.colorator,
    this.backgroundColor,
    this.unselectedColor,
    this.searchIcon,
    this.closeSearchIcon,
    this.itemsTextStyle,
    this.searchTextStyle,
    this.searchHintStyle,
    this.selectedItemsTextStyle,
    this.checkColor,
  });

  /// This constructor allows a FormFieldState to be passed in. Called by MultiselectField.
  _MultiselectFieldView._withState(
      _MultiselectFieldView<V> field, FormFieldState<List<V>> this.state)
      : items = field.items,
        title = field.title,
        buttonText = field.buttonText,
        buttonIcon = field.buttonIcon,
        decoration = field.decoration,
        onSelectionChanged = field.onSelectionChanged,
        onConfirm = field.onConfirm,
        initialValue = field.initialValue,
        searchable = field.searchable,
        confirmText = field.confirmText,
        cancelText = field.cancelText,
        barrierColor = field.barrierColor,
        selectedColor = field.selectedColor,
        height = field.height,
        searchHint = field.searchHint,
        colorator = field.colorator,
        backgroundColor = field.backgroundColor,
        unselectedColor = field.unselectedColor,
        searchIcon = field.searchIcon,
        closeSearchIcon = field.closeSearchIcon,
        itemsTextStyle = field.itemsTextStyle,
        searchHintStyle = field.searchHintStyle,
        searchTextStyle = field.searchTextStyle,
        selectedItemsTextStyle = field.selectedItemsTextStyle,
        checkColor = field.checkColor;

  @override
  __MultiselectFieldViewState createState() =>
      __MultiselectFieldViewState<V>();
}

class __MultiselectFieldViewState<V>
    extends State<_MultiselectFieldView<V>> {
  List<V> _selectedItems = [];

  @override
  void initState() {
    super.initState();
    if (widget.initialValue != null) {
      _selectedItems.addAll(widget.initialValue!);
    }
  }

  /// Calls showDialog() and renders a MultiSelectDialog.
  _showDialog(BuildContext ctx) async {
    await showDialog(
      barrierColor: widget.barrierColor,
      context: context,
      builder: (ctx) {
        return MultiselectDialog<V>(
          checkColor: widget.checkColor,
          selectedItemsTextStyle: widget.selectedItemsTextStyle,
          itemsTextStyle: widget.itemsTextStyle,
          unselectedColor: widget.unselectedColor,
          backgroundColor: widget.backgroundColor,
          colorator: widget.colorator,
          selectedColor: widget.selectedColor,
          onSelectionChanged: widget.onSelectionChanged,
          height: widget.height,
          items: widget.items,
          initialValue: _selectedItems,
          onConfirm: (selected) {
            // widget.state.didChange(selected);

            _selectedItems = selected;

            if (widget.onConfirm != null) {
              widget.onConfirm!(selected);
            }
          },
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.start,
      children: <Widget>[
        InkWell(
          onTap: () {
            _showDialog(context);
          },
          child: Container(
            decoration: BoxDecoration(
              border: Border.all(color: widget.state != null && widget.state!.hasError ? GlobalStyle.errorColor : Colors.grey, width: 1),
              borderRadius: BorderRadius.circular(5),
            ),
            padding: const EdgeInsets.only(top: 2, bottom: 2, left: 10, right: 10),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Text(_selectedItems.isNotEmpty ? "Some" : "-- All --"),
                const Icon(Icons.arrow_drop_down),
              ],
            ),
          ),
        ),
        widget.state != null && widget.state!.hasError
            ? const SizedBox(height: 5)
            : Container(),
        widget.state != null && widget.state!.hasError
            ? Row(
          children: <Widget>[
            Padding(
              padding: const EdgeInsets.only(left: 4),
              child: Text(
                widget.state!.errorText!,
                style: TextStyle(
                  color: GlobalStyle.errorColor,
                  fontSize: 12.5,
                ),
              ),
            ),
          ],
        )
            : Container(),
      ],
    );
  }
}
