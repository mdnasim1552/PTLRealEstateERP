﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccSubCodeBook.aspx.cs" Inherits="RealERPWEB.F_01_LPA.AccSubCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style17
        {
            height: 41px;
            margin-bottom: 0px;
    }
        .style22
        {
            color: #FFFFFF;
        }
        .style24
        {
            width: 105px;
        }
        .style25
        {
            width: 108px;
        }
        .style26
        {
            width: 6px;
        }
        .style28
        {
            width: 393px;
        }
        .style29
        {
            width: 115px;
        }
        .style30
        {
            width: 29px;
        }
        .style31
        {
            width: 63px;
        }
    </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 910px; height: 30px;">
        <tr>
            <td class="style43">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False" 
                    Font-Size="25px" ForeColor="Navy" Height="20px" Text="SCHEDULE &amp; OTHERS CODE BOOK INFORMATION INPUT/EDIT SCREEN" 
                    Width="639px" BackColor="#9999FF" style="font-size: 18px"></asp:Label>
            </td>
            <td class="style47">
                &nbsp;</td>
            <td class="style44">
                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                    </td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Size="12px" Font-Underline="True" Height="17px" 
                    onclick="lnkPrint_Click" 
                    
                    style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; font-size: 12px;" 
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Panel ID="PanelHeader" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                BorderWidth="1px">
            <table class="style17">
               
                <tr>
                    <td colspan="6">
                        <asp:Panel ID="Panel2" runat="server" Width="760px">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                    <td>
                                        <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#003366" Height="16px" 
                                            style="TEXT-ALIGN: right; color: #FFFFFF;" 
                                            Text="Select Code Book:" Width="150px"></asp:Label>
                                    </td>
                                    <td class="style28">
                                        <asp:DropDownList ID="ddlOthersBook" runat="server" BackColor="#68AED0" 
                                            Font-Bold="True" Font-Size="16px" style="margin-left: 0px" Width="400px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl" runat="server" BackColor="#68AED0" 
                                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="14px" style="margin-bottom:1px" Visible="False" Width="400px"></asp:Label>
                                    </td>
                                    <td class="style29">
                                        <asp:DropDownList ID="ddlOthersBookSegment" runat="server" BackColor="#68AED0" 
                                            Font-Bold="True" Font-Size="16px" style="margin-left: 0px" Width="130px" 
                                         >
                                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                            <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl0" runat="server" BackColor="#68AED0" 
                                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="14px" style="margin-bottom:1px" Visible="False" Width="130px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkok" runat="server" CssClass="style22" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" onclick="lnkok_Click" Width="43px">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                    <td>
                                        &nbsp;&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style26">
                                        &nbsp;</td>
                                    <td class="style24">
                                        <asp:Label ID="LblBookName2" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#003366" Height="16px" 
                                            style="TEXT-ALIGN: right; color: #FFFFFF;" 
                                            Text="Search Option:" Width="150px"></asp:Label>
                                    </td>
                                    <td class="style25">
                                        <asp:TextBox ID="txtsrch" runat="server" BorderColor="Yellow" 
                                            BorderStyle="Solid" BorderWidth="1px" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style30">
                                        <asp:ImageButton ID="ibtnSrch" runat="server" Height="16px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnSrch_Click" Width="16px" 
                                            Visible="False" />
                                    </td>
                                    <td class="style31">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="White" Style="text-align: right;" Text="Page Size :" Visible="False" 
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" 
                                            Width="80px">
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        
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
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
          <%--  </fieldset>--%>
             </asp:Panel>
            <table >

                <tr>
                    <td>
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" PageSize="15" Width="950px" 
                            onpageindexchanging="grvacc_PageIndexChanging">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" 
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>' 
                                            Width="20px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>' 
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px" 
                                            MaxLength="13" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>' 
                                            Width="90px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>' 
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>' 
                                            Width="300px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="150px"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>' 
                                            Width="325px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsirunit" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>' 
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblunit" runat="server" Font-Size="12px" style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>' 
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Std.Rate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsirval" runat="server" Font-Size="12px" 
                                            style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="16px" HorizontalAlign="Center" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgridsirtype" runat="server" Font-Size="12px" MaxLength="10" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>' 
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltype" runat="server" Font-Size="12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px" MaxLength="20" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>' 
                                            Width="150px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px" 
                                            style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>' 
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode3")) %>' 
                                            Visible="False"></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entry User Name" Visible="false">
                                    <EditItemTemplate>
                                        <asp:Label ID="tlblgvUsr" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>' 
                                            Width="90px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>' 
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#CAD8B1" Height="15px" />
                            <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerSettings Position="Top" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#66CCFF" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


