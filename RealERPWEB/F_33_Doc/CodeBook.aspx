<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CodeBook.aspx.cs" Inherits="RealERPWEB.F_33_Doc.CodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style15
        {
            color: #FFFFFF;
        }
        .style16
        {
            width: 119px;
        }
        .style17
        {
            width: 605px;
        }
        .style18
        {
            width: 522px;
        }
    </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 912px">
        <tr>
            <td class="style18">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="CODE BOOK INFORMATION VIEW/EDIT" Width="618px"
                   STYLE="border-bottom:1px solid white;border-top:1px solid white;" ></asp:Label>
            </td>
            <td class="style47">
                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td class="style16">
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
                    Font-Italic="True" Font-Underline="True" 
                    onclick="lnkPrint_Click" 
                    
                    style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; color: #FFFFFF;" 
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

         <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                           BorderWidth="1px">
            <table class="style17">
                
                <tr>
                    <td class="style49">
                        <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="#003366" Height="12px" 
                            style="FONT-SIZE: 12px; TEXT-ALIGN: right; color: #FFFFFF;" Text="SELECT CODE BOOK :" 
                            Width="135px" CssClass="style15"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOthersBook" runat="server" 
                            Font-Bold="True" Font-Size="12px" style="margin-left: 0px" Width="350px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOthersBookSegment" runat="server" 
                            Font-Bold="True" Font-Size="12px" style="margin-left: 0px" Width="129px">
                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                            <asp:ListItem Value="5">Sub Code-2</asp:ListItem>    
                            <asp:ListItem Selected="True" Value="7">Details Code</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style50">
                        <asp:LinkButton ID="lnkok" runat="server" Font-Bold="True" Font-Size="12px" 
                            Height="16px" onclick="lnkok_Click" Width="43px" CssClass="button">OK</asp:LinkButton>

                    </td>
                    <td class="style48">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style49">
                        &nbsp;</td>
                    <td>
                        <asp:Label ID="ConfirmMessage" runat="server" BackColor="Red" 
                            Font-Italic="False" Font-Size="12px" ForeColor="White"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="style50">
                    </td>
                    <td class="style48">
                    </td>
                    <td>
                    </td>
                </tr>
                </table>
                </asp:Panel>

                 <table class="style17">
                <tr>
                    <td colspan="6">
                       <%-- <asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" ShowFooter="True">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" /> 
                            --%>


                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" PageSize="15" Width="284px" 
                            ShowFooter="True">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
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
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>' 
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px" 
                                            MaxLength="6" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>' 
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True" 
                                            Font-Bold="True" Font-Size="14px" 
                                            onselectedindexchanged="ddlPageNo_SelectedIndexChanged" 
                                            style="BORDER-RIGHT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-BOTTOM: navy 1px solid" 
                                            Width="150px">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" 
                                            style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>' 
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               

                                


                            </Columns>

                             <RowStyle BackColor="#CAD8B1" Height="15px" />
                            <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#999999" Font-Bold="True" Font-Size="16px" 
                                ForeColor="White" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#66CCFF" />
                        </asp:GridView>
                            <%--<RowStyle BackColor="#92AF5F" Height="15px" />
                            <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#999999" Font-Bold="True" Font-Size="16px" 
                                ForeColor="White" HorizontalAlign="Right" />
                            <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#BBC684" />
                        </asp:GridView>--%>
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style50">
                        &nbsp;</td>
                    <td class="style48">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

