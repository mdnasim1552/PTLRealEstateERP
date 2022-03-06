<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RDLCViewer.aspx.cs" Inherits="RealERPWEB.RDLCViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A" Namespace="Microsoft.Reporting.WinForms" TagPrefix="rsweb" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Preview1</title>
      <link rel="icon" type="image/png" sizes="32x32" href="Image/favicon-32x32.png"/>
    <link rel="icon" type="image/png" sizes="16x16" href="Image/favicon-16x16.png"/>
    <script language="javascript" type="text/javascript">
        function WStartUpSize() {
            self.resizeTo(400, 400);
            self.moveTo(screen.width / 2 - 200, screen.height / 2 - 200);
        }
        function WResizeTo() {
            self.moveTo(0, 0);
            self.resizeTo(screen.width, screen.height);
        }
    </script>
    <style type="text/css">
        .style1 {
            width: 85px;
        }

        .style2 {
            height: 21px;
            width: 85px;
        }

        .style3 {
            width: 21px;
        }

        .style4 {
            height: 21px;
            width: 21px;
        }
    </style>
</head>

<body onload="WStartUpSize()" bgcolor="#F0FEF0">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>
          <%--  <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote"></rsweb:ReportViewer>--%>
        </div>
    </form>
</body>
</html>

