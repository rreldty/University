import '../../UserControls/MessageBox.dart';

class ModalPopupResult {
  DialogResult? dialogResult;
  Map? map;

  ModalPopupResult({
    this.dialogResult = DialogResult.Cancel,
    this.map,
  });
}