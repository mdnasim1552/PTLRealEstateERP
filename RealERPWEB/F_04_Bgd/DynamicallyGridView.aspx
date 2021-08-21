<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DynamicallyGridView.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.DynamicallyGridView" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 930px;">
        <tr>
            <td class="style137">
                <asp:Label ID="lblTitle" runat="server" BackColor="Blue" Font-Bold="True" 
                ForeColor="Yellow" style="font-weight: 700; color: #FFFF66; text-align: left" 
                Text="GRID VIEW DYNAMIC INFORMATION INPUT/EDIT" Width="567px"></asp:Label>
            </td>
            <td class="style140">
                &nbsp;</td>
            <td class="style12">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style139">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style138">
                <asp:LinkButton ID="lbtnPrintReport" runat="server" Font-Bold="True" 
                    Font-Size="12px" onclick="lbtnPrintReport_Click" CssClass="button" 
                    style="text-align: center; height: 16px;">PRINT</asp:LinkButton>
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
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                            onclick="lbtnOk_Click" style="text-align: center; color: #FFFFFF;">Ok</asp:LinkButton>
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
                </tr>
                <tr>
                    <td colspan="10">
                        <asp:GridView ID="gvDyInfo" runat="server" PageSize="15" ShowFooter="True" 
                            style="margin-right: 0px" Width="918px" AutoGenerateColumns="False" 
                            onrowdatabound="gvDyInfo_RowDataBound">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                 <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" 
                                                                style="text-align: right" 
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerSettings Position="Top" />
                            <PagerStyle Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                    </td>
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
                        <asp:LinkButton ID="lbtnUpdate" runat="server" BackColor="#003366" 
                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                            onclick="lbtnUpdate_Click" style="text-align: center; color: #FFFFFF;">Update</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

