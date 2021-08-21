<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="Defaulthide.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.Defaulthide" %>


<!DOCTYPE html>





<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function() {
        //$("table[id*=rblAttachmentType] input:not(table[id*=rblAttachmentType] input:eq(2))").click(function(){
        //    $("table[id*=rblSource] input,table[id*=rblSource] label").show();
        //});
        $("table[id*=rblAttachmentType] input:eq(2)").click(function(){
            if(this.checked)
                $("table[id*=rblSource] input:first").hide();
                $("table[id*=rblSource] input:first").next().hide();
        })
    });
</script>
</head>
<body>
    <form id="form2" runat="server">
    <table>
        <tr>
            <td class="rightTD">
                <asp:RadioButtonList ID="rblAttachmentType" runat="server" CssClass="radio" RepeatDirection="Horizontal"
                    ToolTip="Select the type of file you want to upload.">
                    <asp:ListItem Selected="True" Value="101">Picture</asp:ListItem>
                    <asp:ListItem Value="102">Floor Plan</asp:ListItem>
                    <asp:ListItem Value="300">Video</asp:ListItem>
                    <asp:ListItem Value="200">Document</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="rightTD">
                <asp:RadioButtonList ID="rblSource" runat="server" RepeatDirection="Horizontal" ToolTip="Select the location of the file. ">
                    <asp:ListItem Text="My Computer" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Website" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
