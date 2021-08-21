<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MisFinNoteCodeBook.aspx.cs" Inherits="RealERPWEB.F_32_Mis.MisFinNoteCodeBook" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css">
        .style18
        {
            width: 6%;
        }
        .style19
        {
            width: 29%;
        }
        .style20
        {
            width: 4px;
        }
        .style21
        {
            width: 12%;
        }
        </style>
    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 912px">
            <tr>
                <td class="style43" >
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="FINANCIAL NOTE BOOK" Width="500px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
                </td>
                <td class="style47" >
                                    <asp:Label ID="lblprintstk" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style44" >
                    <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" 
                        Font-Italic="True" Font-Underline="True" 
                        
                        style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; color: #FFFFFF;" 
                        CssClass="button" onclick="lnkPrint_Click" >PRINT</asp:LinkButton>
                </td>
                <td >
                    &nbsp;</td>
                
            </tr>
        </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <table style="width:100%;">
                           
                            <tr>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style19">
                                    <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                        Font-Size="12px" ForeColor="Maroon" style="color: #FFFFFF"></asp:Label>
                                </td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style20">
                                    &nbsp;</td>
                                <td class="style21">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                            </tr>
                           
                            <tr>
                                <td colspan="12">
                                   
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvnoteinfo" runat="server" AllowPaging="True" 
                                                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                                                            BorderWidth="2px" CellPadding="4" Font-Size="12px" 
                                                            onrowcancelingedit="gvnoteinfo_RowCancelingEdit" 
                                                            onrowediting="gvnoteinfo_RowEditing" 
                                                            onrowupdating="gvnoteinfo_RowUpdating" PageSize="15" ShowFooter="True" 
                                                            Width="915px" onpageindexchanging="gvnoteinfo_PageIndexChanging">
                                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                                                Mode="NumericFirstLast" />
                                                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl.No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="serialnoid0" runat="server" ForeColor="Black" 
                                                                            style="text-align: right" 
                                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                </asp:TemplateField>
                                                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" 
                                                                    SelectText="" ShowEditButton="True">
                                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                                                </asp:CommandField>
                                                                <asp:TemplateField HeaderText=" ">
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lbgvcode" runat="server" ForeColor="Black" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notecode2"))+"-" %>' 
                                                                            Width="20px"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" ForeColor="Black" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notecode2"))+"-" %>' 
                                                                            Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtgvcode3" runat="server" Height="16px" MaxLength="6" 
                                                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notecode3")) %>' 
                                                                            Width="40px"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvcode3" runat="server" ForeColor="Black" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notecode3")) %>' 
                                                                            Width="40px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description of Code">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtgvCodeDesc" runat="server" Height="280px" 
                                                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notedesc")) %>' 
                                                                            TextMode="MultiLine" width="750px"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvgvCodeDesc" runat="server" ForeColor="Black" 
                                                                            style="FONT-SIZE: 12px" 
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notedesc")) %>' 
                                                                            width="750px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle BackColor="#CAD8B1" Height="15px" />
                                                            <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <PagerStyle BackColor="#999999" Font-Bold="True" Font-Size="16px" 
                                                                ForeColor="White" HorizontalAlign="Left" />
                                                            <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="#66CCFF" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                   
                                </td>
                              </tr>
             
                            <tr>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style19">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style20">
                                    &nbsp;</td>
                                <td class="style21">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                                <td class="style18">
                                    &nbsp;</td>
                            </tr>
                        </table>
                           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



