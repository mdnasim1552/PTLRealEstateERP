<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table style="width: 97%;">
    <tr>
        <td class="style13">
            <asp:Label ID="lblTitle" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" 
                Text="STANDARD ANALYSIS INFORMATION INPUT/EDIT" Width="443px"></asp:Label>
        </td>
        <td class="style121">
            <asp:Label ID="lbljavascript" runat="server"></asp:Label>
        </td>
        <td class="style125">
            <asp:Label ID="lblFloor1" runat="server" CssClass="newStyle3" Font-Size="12px" 
                style="font-weight: 700; text-align: right; color: #FFFFFF;" 
                Text="Floor:" Width="55px"></asp:Label>
        </td>
        <td class="style12">
            <asp:DropDownList ID="ddlFloor1" runat="server" Font-Bold="True" 
                Font-Size="11px" Height="20px" 
                style="border: 1px solid #99CCFF;text-transform:capitalize" Width="126px">
            </asp:DropDownList>
        </td>
        <td class="style123">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
        </td>
        <td class="style124">
            <asp:LinkButton ID="lbtnPrintAnaLysis" runat="server" Font-Bold="True" 
                Font-Size="12px" onclick="lbtnPrintAnaLysis_Click" Width="60px" 
                CssClass="button" style="text-align: center">PRINT</asp:LinkButton>
        </td>
        <td class="style11">
            &nbsp;</td>
    </tr>
</table>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:TreeView ID="MyTreeView" Runat="server" Font-Bold="True" Font-Size="12px" 
                            ForeColor="White" onselectednodechanged="MyTreeView_SelectedNodeChanged">
                            <HoverNodeStyle BackColor="#000066" BorderStyle="Solid" BorderWidth="1px" 
                                Font-Bold="True" Font-Size="12px" ForeColor="Yellow" />
                            <SelectedNodeStyle BackColor="#000066" ForeColor="Yellow" />
                        </asp:TreeView>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="button" Font-Bold="True" 
                            Font-Size="12px" onclick="lbtnOk_Click" style="text-align: center" Width="60px">Ok</asp:LinkButton>
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/Image/arrow_html.jpg"  Target="_blank"
                            NavigateUrl="~/Image/ComName01.jpg">HyperLink</asp:HyperLink>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

