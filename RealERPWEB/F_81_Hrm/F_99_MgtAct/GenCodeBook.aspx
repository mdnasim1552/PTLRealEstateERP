<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GenCodeBook.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_99_MgtAct.GenCodeBook" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style15
        {
            color: #FFFFFF;
            text-align: center;
        }
        .style16
        {
            width: 119px;
        }
        .style18
        {
            width: 522px;
            text-align: right;
        }
        .style20
        {
            width: 151px;
        }
        .style21
        {
            width: 263px;
        }
        .style22
        {
            width: 30px;
        }
    </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 912px">
        <tr>
            <td>
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="REPORTING CODE BOOK INFORMATION" Width="500px"
                   
                    STYLE="border-bottom:1px solid white;border-top:1px solid white; text-align: left;" ></asp:Label>
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
    <%-- 
    --%>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

         <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                           BorderWidth="1px">
           <table style="width:100%;">
            
                
                <tr>
                    <td class="style22">
                        <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="12px" ForeColor="#003366" Height="12px" 
                            style="FONT-SIZE: 12px; TEXT-ALIGN: right; color: #FFFFFF;" Text="SELECT CODE BOOK :" 
                            Width="135px" CssClass="style15"></asp:Label>
                    </td>
                    <td class="style21">
                        <asp:DropDownList ID="ddlOthersBook" runat="server" BackColor="#68AED0" 
                        Font-Bold="True" Font-Size="12px" style="margin-left: 0px" Width="300px">
                        </asp:DropDownList>
                        <asp:Label ID="lbalterofddl" runat="server" BackColor="#68AED0" 
                        BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                        Font-Size="12px" style="margin-bottom:1px" Visible="False" Width="300px"></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" 
                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                            style="text-align: center; height: 17px;" Width="50px">Ok</asp:LinkButton>
                    </td>
                    <td class="style50">
                        &nbsp;</td>
                    
                </tr>
                <tr>
                    <td class="style22">
                        <asp:Label ID="lblPage" runat="server" CssClass="style18" Font-Bold="True" 
                            Font-Size="12px" ForeColor="White" Text="Size:" Width="135px"></asp:Label>
                    </td>
                    <td class="style21">
                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                            onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Width="85px">
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                            <asp:ListItem Value="150">150</asp:ListItem>
                            <asp:ListItem Value="200">200</asp:ListItem>
                            <asp:ListItem Value="300">300</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style20">
                        <asp:Label ID="ConfirmMessage" runat="server" BackColor="Red" 
                            Font-Italic="False" Font-Size="12px" ForeColor="White"></asp:Label>
                    </td>
                    <td class="style50">
                    </td>
                    
                </tr>
                </table>
                </asp:Panel>

                <table style="width:100%;">
                <tr>
                    <td colspan="6">
                        <%--<asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" ShowFooter="True">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" /> 
                            
                            
                            <Columns>--%>
                            
                          <%--  <cc1:ToolkitScriptManager ID="ScriptManager2" runat="server">
                            </cc1:ToolkitScriptManager>--%>
                            
                                <asp:GridView ID="gvmiscodeBook" runat="server" AutoGenerateColumns="False" 
                                    ShowFooter="True" onpageindexchanging="gvmiscodeBook_PageIndexChanging"
                                    onrowcancelingedit="gvmiscodeBook_RowCancelingEdit" onrowediting="gvmiscodeBook_RowEditing" 
                                        onrowupdating="gvmiscodeBook_RowUpdating" AllowPaging="True">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                     <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                    <asp:ImageButton ID="imgbtn" ImageUrl="~/Image/Edit.jpg" runat="server" Width="25" Height="25" onclick="imgbtn_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" 
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode2"))+"-" %>' 
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px" 
                                            MaxLength="9" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode3")) %>' 
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode3")) %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")) %>' 
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" 
                                            style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")) %>' 
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvUnit" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvUnit" runat="server" Font-Size="12px" 
                                            style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gencode")) %>' 
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                 </Columns>
                                    <RowStyle BackColor="#CAD8B1" Height="15px" />
                                    <EditRowStyle BackColor="LightSkyBlue" Font-Bold="True" Font-Size="12px" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle Font-Bold="True" Font-Size="12px" 
                                        ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#66CCFF" />
                        </asp:GridView>
                        <asp:Label ID="lblresult" runat="server"/>
<asp:Button ID="btnShowPopup" runat="server" style="display:none" />
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="180px" Width="550px" style="display:none">
<table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%" cellpadding="0" cellspacing="0">
    <tr style="background-color:#D55500">
        <td colspan="2" style=" height:10%; color:White; font-weight:bold; font-size:larger" align="center">Reporting Code Details</td>
    </tr>
<%--<tr>
<td align="right" style=" width:45%">
Sl No:
</td>
<td>
<asp:Label ID="lblID" runat="server"></asp:Label>
</td>
</tr>
--%>

    <tr>
        <td align="right">
            GRoup Code : 
        </td>
        <td>
            <asp:Label ID="lblGrpCode" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right">
            Code:
        </td>
        <td>
            <asp:TextBox ID="txtCode2" runat="server" MaxLength="6"/>
        </td>
    </tr>
    <tr>
        <td align="right">
            Description of Code : 
        </td>
        <td>
            <asp:TextBox ID="txtDesc" runat="server"/>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" onclick="btnUpdate_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </td>
    </tr>
</table>
</asp:Panel>
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



