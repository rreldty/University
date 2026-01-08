import 'package:auto_size_text/auto_size_text.dart';
import 'package:file_picker/file_picker.dart';
import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';
import 'ButtonExtender.dart';
import 'EditText.dart';

class UploadBox extends StatefulWidget {
  final UploadBoxController? controller;
  final String buttonText;
  final double width;
  final bool isEnable;
  final String? mandatoryText;

  final bool isMandatory;
  final bool isVisible;
  final List<String>? allowedExtensions;
  final Function(FilePickerResult? fileResult)? onPressed;

  const UploadBox({
    super.key,
    this.controller,
    this.buttonText = "",
    this.mandatoryText = "",
    this.width = 120,
    this.isMandatory = false,
    this.isEnable = true,
    this.isVisible = true,
    this.allowedExtensions,
    this.onPressed,
  });

  @override
  createState() => UploadBoxState();
}

class UploadBoxState extends State<UploadBox> {
  ButtonController btnUpload = ButtonController();
  // bool _isEnable = true;
  // List<String> lstFilePath = [];
  // String strFilePath = "";

  @override
  void initState() {
    super.initState();
    if(widget.controller != null){
      // _isEnable = widget.isEnable;
      widget.controller!.isEnable = widget.isEnable;
    }
  }

  // void btnUpload_Click() async{
  //   FilePickerResult? result = await FilePicker.platform.pickFiles();
  //
  //   if (result != null) {
  //     lstFilePath.add(result.files.single.path ?? "");
  //   }
  // }

  // String? validateText(String? value) {
  //   String strResult = "";
  //   String strValue = value ?? "";
  //   String strMandatory = widget.mandatoryText ?? "";
  //
  //   if(widget.isMandatory && strValue.isEmpty){
  //     if(strMandatory.isNotEmpty){
  //       strResult = strMandatory;
  //     }else{
  //       strResult = "Please fill this field";
  //     }
  //   }
  //
  //
  //   if(strResult.isNotEmpty) {
  //     return strResult;
  //   }
  //
  //   return null;
  // }

  void btnUpload_Click() async{
    FilePickerResult? fileResult = await FilePicker.platform.pickFiles(
      type: widget.allowedExtensions != null ? FileType.custom : FileType.any,
      allowedExtensions: widget.allowedExtensions,
    );

    // if (fileResult != null) {
    //   widget.controller!.text = fileResult.files.single.name;
    //   // setState(() {
    //   //
    //   // });
    // }

    if(widget.onPressed != null){
      widget.onPressed!(fileResult);
    }
  }

  @override
  Widget build(BuildContext context) {

    if(widget.isVisible) {
      return ButtonExtender(
        controller: btnUpload,
        width: 140,
        buttonText: "Choose File",
        onPressed: btnUpload_Click,
        isEnable: widget.controller!.isEnable,
        icon: Icons.folder_copy,
      );
    }

    return Container();

    // if(widget.isVisible){
    //   return SizedBox(
    //     width: 450,
    //     child: Stack(
    //       // mainAxisSize: MainAxisSize.max,
    //       children: [
    //         Container(
    //           margin: const EdgeInsets.only(top: 5),
    //           width: 250,
    //           child: TextFormField(
    //               textInputAction: TextInputAction.go,
    //               textAlign: TextAlign.start,
    //               obscureText: false,
    //               keyboardType: TextInputType.text,
    //               enabled: false,
    //               controller: widget.controller?._textController,
    //               onFieldSubmitted: (val) => debugPrint("onFieldSubmitted"),
    //               onEditingComplete: () => debugPrint("onEditingComplete"),
    //               onSaved: (val) => debugPrint("onSaved"),
    //               validator:  validateText,
    //               style: TextStyle(fontSize: GlobalStyle.fontSize),
    //               decoration: InputDecoration(
    //                 counterText: "",
    //                 errorStyle: const TextStyle(fontWeight: FontWeight.bold, color:  Colors.red),
    //                 prefixIconConstraints: const BoxConstraints(maxHeight: 24, maxWidth: 24),
    //                 suffixIconConstraints: const BoxConstraints(maxHeight: 24, maxWidth: 24),
    //                 hoverColor: Colors.transparent,
    //                 isDense: true,
    //                 contentPadding: const EdgeInsets.all(10),
    //                 filled: true,
    //                 hintStyle: TextStyle(fontSize: GlobalStyle.fontSize),
    //                 labelStyle: TextStyle(fontSize: GlobalStyle.fontSize),
    //                 helperStyle: TextStyle(fontSize: GlobalStyle.fontSize),
    //                 fillColor: GlobalStyle.disableColor,
    //                 border: OutlineInputBorder(
    //                   borderRadius: BorderRadius.circular(5.0),
    //                   borderSide: const BorderSide(
    //                     color: Color(0xffbababa),
    //                     width: 1.0,
    //                   ),
    //                 ),
    //                 errorBorder: OutlineInputBorder(
    //                   borderRadius: BorderRadius.circular(5.0),
    //                   borderSide: BorderSide(
    //                     color: GlobalStyle.errorColor,
    //                     width: 1.0,
    //                   ),
    //                 ),
    //                 focusedBorder: OutlineInputBorder(
    //                   borderRadius: BorderRadius.circular(5.0),
    //                   borderSide: BorderSide(
    //                     color: GlobalStyle.primaryColor,
    //                     width: 1.0,
    //                   ),
    //                 ),
    //               )
    //           ),
    //         ),
    //
    //         // Expanded(
    //         //     child: Tooltip(
    //         //       message: widget.controller!.text,
    //         //       child: Container(
    //         //         width: double.infinity,
    //         //         height: 30,
    //         //         margin: const EdgeInsets.only(top: 5),
    //         //         padding: const EdgeInsets.fromLTRB(5, 4, 5, 0),
    //         //         decoration: BoxDecoration(
    //         //           color: GlobalStyle.disableColor,
    //         //           border: Border.all(color: Colors.black26, width: 1),
    //         //           borderRadius: BorderRadius.circular(5),
    //         //         ),
    //         //         child: AutoSizeText(
    //         //           widget.controller!.text,
    //         //           maxLines: 1,
    //         //           overflow: TextOverflow.ellipsis,
    //         //         ),
    //         //       ),
    //         //     ),
    //         // ),
    //         // const SizedBox(
    //         //   width: 5,
    //         // ),
    //         Positioned(
    //           left: 250,
    //           child: ButtonExtender(
    //             controller: btnUpload,
    //             width: 100,
    //             buttonText: "Choose File",
    //             onPressed: btnUpload_Click,
    //             isEnable: widget.controller!.isEnable,
    //           ),
    //         ),
    //         // TextButton(
    //         //   style: ButtonStyle(
    //         //     backgroundColor: MaterialStateProperty.all<Color>(_isEnable ? GlobalStyle.primaryColor : GlobalStyle.disableColor),
    //         //   ) ,
    //         //   onPressed: (){
    //         //     if(_isEnable){
    //         //       btnUpload_Click();
    //         //     }
    //         //   },
    //         //   child: Text(
    //         //     "Upload",
    //         //     style: TextStyle(fontSize: GlobalStyle.fontSize, color: Colors.white),
    //         //   ),
    //         // )
    //       ],
    //     ),
    //   );
    // }else{
    //   return Container();
    // }

  }

}

class UploadBoxController{
  bool isEnable = true;
  bool isClear = false;
  String text = "";

  // final EditTextController _textController = EditTextController();
  //
  // String get text{
  //   return _textController.text;
  // }
  //
  // set text(String value) {
  //   _textController.text = value;
  // }
}