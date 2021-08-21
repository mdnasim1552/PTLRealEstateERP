<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="Default03.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.Default03" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style8
        {
            width: 28px;
        }
    </style>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
  
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            // Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            alert("pr");
           
            $('#<%=this.txtkeycode.ClientID%>').keyup(function (e) {

                var key = e.keyCode;
                if (key == 120) {
                    alert("you enter"+key.toString());

                }
                else {
                    alert("you enter" + key.toString());

                }

            });

        });


       
     
    </script>

    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td class="style6">
                        <asp:LinkButton ID="lnkObjToXML" runat="server" BackColor="#000066" 
                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" onclick="lnkObjToXML_Click" 
                            TabIndex="3">OBJ TO XML</asp:LinkButton>
                    </td>
                    <td class="style8">
                        <asp:LinkButton ID="lbtnExcelData" runat="server" OnClick="lbtnExcelData_Click">LinkButton</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:TextBox ID="TextBox1" runat="server" TabIndex="1"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="TextBox1" PopupButtonID="Image2">
                        </asp:CalendarExtender>
                    </td>
                    <td class="style8">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td>
                        <asp:Image ID="Image2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:TextBox ID="txtkeycode" runat="server" Text="e" TabIndex="2"></asp:TextBox>
                    </td>
                    <td class="style8">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="lblvalue" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


