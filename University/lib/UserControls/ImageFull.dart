import '../../Common/GlobalStyle.dart';
import 'package:flutter/material.dart';

class ImageFull extends StatefulWidget {
  final String imageNetworkUrl;
  final String imageTitle;

  const ImageFull({
    this.imageNetworkUrl = "",
    this.imageTitle = "",
  });

  @override
  createState() => ImageFullState();
}

class ImageFullState extends State<ImageFull> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        resizeToAvoidBottomInset: false,
        backgroundColor: Colors.white,
        appBar: PreferredSize(
          preferredSize: const Size.fromHeight(50.0), // here the desired height
          child: AppBar(
            backgroundColor: GlobalStyle.primaryColor,
            leading: IconButton(
                icon: const Icon(Icons.arrow_back),
                onPressed: (){
                  Navigator.of(context, rootNavigator: true).pop();
                }
            ),
            title: Text(widget.imageTitle),
          ),
        ),
        body:Container(
          decoration: BoxDecoration(
            image: DecorationImage(
                image: NetworkImage(Uri.encodeFull(widget.imageNetworkUrl)),
                fit: BoxFit.contain
            ),
          ),
        ),
    );
  }

  /*
  Container(
  decoration: BoxDecoration(
  image: DecorationImage(
  image: NetworkImage(widget.imageUrl ?? ""),
  fit: BoxFit.fill
  ) ,
  ),
  ),*/
}