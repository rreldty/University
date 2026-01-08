import 'package:auto_size_text/auto_size_text.dart';
import 'package:cached_network_image/cached_network_image.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class CarouselWithIndicator extends StatefulWidget {
  final List<CarouselItem> carouselItems;
  final String carouselItemNoImage;
  final String carouselNoItemImageUrl;

  const CarouselWithIndicator({
    super.key,
    required this.carouselItems,
    this.carouselItemNoImage = "",
    this.carouselNoItemImageUrl = ""
  });

  @override
  CarouselWithIndicatorState createState() => CarouselWithIndicatorState();
}

class CarouselWithIndicatorState extends State<CarouselWithIndicator> {
  int _current = 0;

  List<T> map<T>(List list, Function handler) {
    List<T> result = [];
    for (var i = 0; i < list.length; i++) {
      result.add(handler(i, list[i]));
    }

    return result;
  }

  @override
  Widget build(BuildContext context) {
    if (widget.carouselItems.isNotEmpty) {
      List<Widget> child = widget.carouselItems.map((item) => InkWell(
        onTap: item.itemOnTap,
        child: Container(
          margin: const EdgeInsets.all(5.0),
          child: ClipRRect(
            borderRadius: const BorderRadius.all(Radius.circular(5.0)),
            child: Stack(
                alignment: Alignment.bottomCenter,
                children: <Widget>[
                  buildItemImage(item.itemImageUrl),
                  if (item.itemCaption != null)
                    FractionallySizedBox(
                      widthFactor: 1,
                      child: Container(
                        decoration: const BoxDecoration(
                          color: Color.fromRGBO(0, 0, 0, 0.5),
                        ),
                        padding: const EdgeInsets.only(
                            top: 3, left: 5, right: 5, bottom: 5),
                        child: AutoSizeText(
                          item.itemCaption,
                          style: const TextStyle(
                              fontSize: 12, color: Colors.white),
                          maxLines: 3,
                        ),
                      ),
                    )
                ]),
          ),
        ),
      )).toList();

      return Column(children: [
        CarouselSlider(
          items: child,
          options: CarouselOptions(
            autoPlay: true,
            enlargeCenterPage: true,
            aspectRatio: 2,
            height: MediaQuery.of(context).size.height / 3,
            onPageChanged: (index, reason) {
              setState(() {
                _current = index;
              });
            },
          ),
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: map<Widget>(
            widget.carouselItems,
            (index, url) {
              return Container(
                width: 8.0,
                height: 8.0,
                margin: const EdgeInsets.symmetric(vertical: 10.0, horizontal: 2.0),
                decoration: BoxDecoration(
                    shape: BoxShape.circle,
                    color: _current == index
                        ? const Color.fromRGBO(0, 0, 0, 0.9)
                        : const Color.fromRGBO(0, 0, 0, 0.4)),
              );
            },
          ),
        ),
      ]);
    } else {
      if (widget.carouselNoItemImageUrl.isNotEmpty) {
        return SizedBox(
          height: MediaQuery.of(context).size.height / 3,
          child: CachedNetworkImage(
            imageUrl: widget.carouselNoItemImageUrl,
            imageBuilder: (context, imageProvider) => Image.network(
              widget.carouselNoItemImageUrl,
              fit: BoxFit.contain,
            ),
            errorWidget: (context, url, error) => Image.asset(
              widget.carouselItemNoImage,
              fit: BoxFit.contain,
            ),
          ),
        );
      } else {
        return Container(
          height: MediaQuery.of(context).size.height / 3,
        );
      }
    }
  }

  Widget buildItemImage(String imageUrl) {
    if (imageUrl.isNotEmpty) {
      return CachedNetworkImage(
        imageUrl: Uri.encodeFull(imageUrl),
        imageBuilder: (context, imageProvider) => Container(
          decoration: BoxDecoration(
            image: DecorationImage(
                alignment: const Alignment(-.2, 0),
                image: imageProvider,
                fit: BoxFit.contain),
          ),
        ),
        errorWidget: (context, url, error) => Container(
          decoration: BoxDecoration(
            image: DecorationImage(
                alignment: const Alignment(-.2, 0),
                image: AssetImage(widget.carouselItemNoImage),
                fit: BoxFit.contain),
          ),
        ),
      );
    } else {
      return Container(
        decoration: BoxDecoration(
          image: DecorationImage(
              alignment: const Alignment(-.2, 0),
              image: AssetImage(widget.carouselItemNoImage),
              fit: BoxFit.contain),
        ),
      );
    }
  }
}

class CarouselItem {
  String itemImageUrl;
  String itemCaption;
  Function()? itemOnTap;

  CarouselItem({
    this.itemImageUrl = "",
    this.itemCaption = "",
    this.itemOnTap,
  });
}
