<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="University.Api.Controls.ReportViewer" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="screen" href="<%=ResolveUrl("~/styles/main.css") %>"/>
    <script type="text/javascript" src="<%=ResolveUrl("~/scripts/jquery.min.js") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--<div class="header">
            <table  style="border-collapse:collapse; border-spacing:0; border:0; padding:0; margin:0; width:100%">
                <tr>
                    <td style="padding:0; margin:0; width:100px;">
                        <asp:ImageButton ID="imbFirst" runat="server" ImageUrl="~/Images/Toolbar/First.png" CssClass="button" style="padding-left:10px" OnClick="btnNavFirst_Click" />    
                        <asp:ImageButton ID="imbPrevious" runat="server" ImageUrl="~/Images/Toolbar/Previous.png" CssClass="button" OnClick="btnNavPrevious_Click" />
                    </td>
                    <td style="padding:0; margin:0; width:90px;">
                        <asp:TextBox ID="txbPage" runat="server" AutoPostBack="true" Width="40px" Text="1" Font-Size="Smaller" OnTextChanged="txbPage_TextChanged"></asp:TextBox>
                        <span style="padding:0px 3px 0px 3px">of</span>
                        <asp:Label ID="lblTotalPage" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="padding:0; margin:0; width:89px; border-right:1px solid #e8e8e8">
                        <asp:ImageButton ID="imbNext" runat="server" ImageUrl="~/Images/Toolbar/Next.png" CssClass="button" OnClick="btnNavNext_Click" />
                        <asp:ImageButton ID="imbLast" runat="server" ImageUrl="~/Images/Toolbar/Last.png"  CssClass="button" OnClick="btnNavLast_Click" />
                    </td>
                    <td style="padding:0; margin:0; width:90px; padding-left:10px; border-right:1px solid #e8e8e8">
                        <asp:DropDownList ID="ddlZoom" runat="server" AutoPostBack="true" CssClass="zoom" OnSelectedIndexChanged="ddlZoom_SelectedIndexChanged">
                            <asp:ListItem Value="500" Text="500%"></asp:ListItem>
                            <asp:ListItem Value="200" Text="200%"></asp:ListItem>
                            <asp:ListItem Value="100" Text="100%" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="75" Text="75%"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25%"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10%"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="vertical-align:top; padding:0; margin:0; width:30px; border-right:1px solid #e8e8e8">
                        <asp:ImageButton ID="imbRefresh" runat="server" ImageUrl="~/Images/Toolbar/Refresh.png" CssClass="button" OnClick="btnRefresh_Click" />
                    </td>
                    <td style="vertical-align:top; padding:0; margin:0; width:30px">
                        <asp:ImageButton ID="imbPrint" runat="server" ImageUrl="~/Images/Toolbar/Print.png" CssClass="button" OnClick="btnPrint_Click" />
                    </td>
                    <td>

                    </td>
                </tr>
            </table>
        </div>--%>
        <div class="main">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
                Width="100%" 
                AsyncRendering="false" 
                PageCountMode = "Actual"
                SizeToReportContent="true" 
                ShowToolBar="true"
                ShowBackButton="false"
                ShowExportControls="false"
                ShowFindControls="false"
                >

            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
