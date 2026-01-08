<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PBIEmbed.aspx.cs" Inherits="University.Api.Controls.PBIEmbed" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="screen" href="<%=ResolveUrl("~/styles/vjx-loader.css") %>"/>
    <link rel="stylesheet" type="text/css" media="screen" href="<%=ResolveUrl("~/styles/vjx-powerbi.css") %>"/>

    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/jquery.min.js") %>"></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/scripts/powerbi.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/scripts/vjx.powerbi.js")%>'></script>
    <script type="text/javascript" >
        $(document).ready(function () {
            $('.main').hide();
            $('.loader').fadeOut(500, function () {
                $('.main').fadeIn(250);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loader"></div>
        <div id="reportContainer" class="main"></div>
    </form>
</body>
</html>
