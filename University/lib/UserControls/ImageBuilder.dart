import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';
class ImageBuilder extends StatefulWidget {
  final String imageNetworkUrl;
  final Function()? whenOnTap;
  final double width;

  const ImageBuilder({
    this.imageNetworkUrl = "",
    this.whenOnTap,
    this.width = 80
  });

  @override
  createState() => ImageBuilderState();
}

class ImageBuilderState extends State<ImageBuilder> {
  @override
  Widget build(BuildContext context) {
    if(widget.imageNetworkUrl.isNotEmpty){
      return GestureDetector(
        onTap: widget.whenOnTap!,
        child: CachedNetworkImage(
          imageUrl: widget.imageNetworkUrl,
          imageBuilder: (context, imageProvider) => Image.network(
            Uri.encodeFull(widget.imageNetworkUrl),
            fit: BoxFit.cover,
            width: widget.width,
          ),
          errorWidget: (context, url, error) => Image.asset(
            "images/no_image.png",
            fit: BoxFit.cover,
            width: widget.width,
          ),

        ),
      );
    }
    else{
      return Image.asset(
        "images/no_image.png",
        fit: BoxFit.cover,
        width: widget.width,
      );

    }
  }

}