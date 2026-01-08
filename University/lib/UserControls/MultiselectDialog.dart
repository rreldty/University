import 'package:auto_size_text/auto_size_text.dart';
import 'package:flutter/material.dart';
import '../../Common/GlobalStyle.dart';
import '../Dto/Base/MultiselectDto.dart';

/// A dialog containing either a classic checkbox style list, or a chip style list.
class MultiselectDialog<V> extends StatefulWidget {
  /// List of items to select from.
  final List<MultiselectDto<V>> items;

  /// The list of selected values before interaction.
  final List<V>? initialValue;

  /// Fires when the an item is selected / unselected.
  final void Function(List<V>)? onSelectionChanged;

  /// Fires when confirm is tapped.
  final void Function(List<V>)? onConfirm;

  /// Sets the color of the checkbox or chip when it's selected.
  final Color? selectedColor;

  /// Sets a fixed height on the dialog.
  final double? height;

  /// A function that sets the color of selected items based on their value.
  /// It will either set the chip color, or the checkbox color depending on the list type.
  final Color Function(V)? colorator;

  /// The background color of the dialog.
  final Color? backgroundColor;

  /// The color of the chip body or checkbox border while not selected.
  final Color? unselectedColor;

  /// Style the text on the chips or list tiles.
  final TextStyle? itemsTextStyle;

  /// Style the text on the selected chips or list tiles.
  final TextStyle? selectedItemsTextStyle;

  /// Set the color of the check in the checkbox
  final Color? checkColor;

  MultiselectDialog({
    required this.items,
    this.initialValue,
    this.onSelectionChanged,
    this.onConfirm,
    this.selectedColor,
    this.height,
    this.colorator,
    this.backgroundColor,
    this.unselectedColor,
    this.itemsTextStyle,
    this.selectedItemsTextStyle,
    this.checkColor,
  });

  @override
  State<StatefulWidget> createState() => _MultiselectDialogState<V>();

  List<V> onItemCheckedChange(
      List<V> selectedValues, V itemValue, bool checked) {
    if (checked) {
      selectedValues.add(itemValue);
    } else {
      selectedValues.remove(itemValue);
    }
    return selectedValues;
  }

  /// Pops the dialog from the navigation stack and returns the initially selected values.
  void onCancelTap(BuildContext ctx, List<V> initiallySelectedValues) {
    Navigator.pop(ctx, initiallySelectedValues);
  }

  /// Pops the dialog from the navigation stack and returns the selected values.
  /// Calls the onConfirm function if one was provided.
  void onConfirmTap(
      BuildContext ctx, List<V> selectedValues, Function(List<V>)? onConfirm) {
    Navigator.pop(ctx, selectedValues);
    if (onConfirm != null) {
      onConfirm(selectedValues);
    }
  }

  /// Accepts the search query, and the original list of items.
  /// If the search query is valid, return a filtered list, otherwise return the original list.
  List<MultiselectDto<V>> updateSearchQuery(
      String? val, List<MultiselectDto<V>> allItems) {
    if (val != null && val.trim().isNotEmpty) {
      List<MultiselectDto<V>> filteredItems = [];
      for (var item in allItems) {
        if (item.label.toLowerCase().contains(val.toLowerCase())) {
          filteredItems.add(item);
        }
      }
      return filteredItems;
    } else {
      return allItems;
    }
  }

  /// Toggles the search field.
  bool onSearchTap(bool showSearch) {
    return !showSearch;
  }
}

class _MultiselectDialogState<V> extends State<MultiselectDialog<V>> {
  List<V> _selectedValues = [];
  late List<MultiselectDto<V>> _items;

  //_MultiselectDialogState(this._items);

  void initState() {
    super.initState();

    _items = widget.items;

    if (widget.initialValue != null) {
      _selectedValues.addAll(widget.initialValue!);
    }
  }

  /// Returns a CheckboxListTile
  Widget _buildListItem(MultiselectDto<V> item) {
    return Theme(
      data: ThemeData(
        unselectedWidgetColor: widget.unselectedColor ?? Colors.black54,
        colorScheme: ColorScheme.fromSwatch().copyWith(secondary: widget.selectedColor ?? Theme.of(context).primaryColor),
      ),
      child: CheckboxListTile(
        dense: true,
        visualDensity: const VisualDensity(horizontal: -4, vertical: -4),
        checkColor: widget.checkColor,
        value: _selectedValues.contains(item.value),
        activeColor: widget.colorator != null
            ? widget.colorator!(item.value) : widget.selectedColor,
        title: Tooltip(
          message: item.label,
          child: AutoSizeText(
            item.label,
            overflow: TextOverflow.ellipsis,
            maxFontSize: 12,
            maxLines: 1,
            style: _selectedValues.contains(item.value)
                ? widget.selectedItemsTextStyle
                : widget.itemsTextStyle,
          ),
        ),
        controlAffinity: ListTileControlAffinity.leading,
        onChanged: (checked) {
          setState(() {
            _selectedValues = widget.onItemCheckedChange(
                _selectedValues, item.value, checked!);
          });
          if (widget.onSelectionChanged != null) {
            widget.onSelectionChanged!(_selectedValues);
          }
        },
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      backgroundColor: widget.backgroundColor,
      title: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: <Widget>[
          Expanded(
            child: Container(
              padding: const EdgeInsets.only(left: 10),
              child: TextField(
                style: const TextStyle(fontSize: 14),
                decoration: InputDecoration(
                  isDense: true,
                  contentPadding: const EdgeInsets.all(5.0),
                  hintText: "Search",
                  focusedBorder: UnderlineInputBorder(
                    borderSide: BorderSide(
                      color: widget.selectedColor ??
                          Theme.of(context).primaryColor,
                    ),
                  ),
                ),
                onChanged: (val) {
                  setState(() {
                    _items = widget.updateSearchQuery(
                        val, widget.items);
                  });
                },
              ),
            ),
          ),
          IconButton(
              onPressed: (){
                setState(() {
                  _selectedValues = [];
                });
              },
              icon: Icon(
                Icons.delete_sweep,
                color: GlobalStyle.primaryColor,
              )
          ),
        ],
      ),
      contentPadding: const EdgeInsets.all(0),
      content: SizedBox(
        height: widget.height,
        width: 300,  //MediaQuery.of(context).size.width * 0.72,
        child: ListView.builder(
          itemCount: _items.length,
          itemBuilder: (context, index) {
            return _buildListItem(_items[index]);
          },
        )
      ),
      actions: <Widget>[
        TextButton(
          child: Text(
            "Cancel",
            style: TextStyle(
              color: (widget.selectedColor != null &&
                  widget.selectedColor != Colors.transparent)
                  ? widget.selectedColor!.withOpacity(1)
                  : Theme.of(context).primaryColor,
            ),
          ),
          onPressed: () {
            widget.onCancelTap(context, widget.initialValue!);
          },
        ),
        TextButton(
          child: Text(
            "OK",
            style: TextStyle(
              color: (widget.selectedColor != null &&
                  widget.selectedColor != Colors.transparent)
                  ? widget.selectedColor!.withOpacity(1)
                  : Theme.of(context).primaryColor,
            ),
          ),
          onPressed: () {
            widget.onConfirmTap(context, _selectedValues, widget.onConfirm);
          },
        )
      ],
    );
  }
}
