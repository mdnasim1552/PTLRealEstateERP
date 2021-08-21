
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RecAccCodeBook.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.RecAccCodeBook" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style17
        {
            width: 101%;
        height: 28px;
    }
        .style43
        {
            width: 398px;
        }
        .style44
        {
            width: 149px;
        }
        .style47
        {
            width: 116px;
        }
        .style49
        {
            width: 147px;
        }
        .style55
        {
            width: 8px;
        }
        .style56
        {
            width: 87px;
        }
        .style57
        {
            width: 68px;
        }
        .style58
        {
            width: 63px;
        }
        .style59
        {
            width: 89px;
        }
        .style61
        {
            width: 104px;
        }
        .style63
        {
            width: 635px;
        }
        .style64
        {
            width: 57px;
        }
        .style65
        {
            width: 124px;
        }
    </style>
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width: 882px; height: 20px;">
            <tr>
                <td class="style43" >
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="False"  
                        Text="COMPANY CODE BOOK INFORMATION INPUT/EDIT SCREEN" Width="539px" 
                        style="font-size: 18px" CssClass="HeaderTitle"></asp:Label>
                </td>
                <td class="style47" >
                                    <asp:Label ID="lblprintstk" runat="server"></asp:Label>
                </td>
                <td class="style44" >
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td >
                    <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" 
                        Font-Italic="False" Font-Size="12px" Font-Underline="True" Height="16px" 
                        style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; font-size: 12px; color: #FFFFFF;" 
                         onclick="lnkPrint_Click" CssClass="button">PRINT</asp:LinkButton>
                </td>
                
            </tr>
        </table>
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style17">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td class="style49">
                        &nbsp;</td>
                    <td class="style65" >
                        <asp:Label ID="ConfirmMessage" runat="server" 
                            Font-Size="14px" Font-Italic="False" ForeColor="White" 
                            Height="16px" BackColor="Red" Font-Bold="True" ></asp:Label>
                    </td>
                    <td class="style57">
                        &nbsp;</td>
                    <td class="style55">
                        &nbsp;</td>
                    <td class="style64">
                        &nbsp;</td>
                    <td class="style59">
                        &nbsp;</td>
                    <td class="style56">
                        &nbsp;</td>
                    <td class="style58">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="style49">
                        <asp:Label ID="LblBookName1" runat="server" BorderStyle="None" Font-Bold="True" 
                            Font-Size="16px" ForeColor="#003366" Height="16px" 
                            style="FONT-SIZE: 18px; TEXT-ALIGN: right; color: #FFFFFF;" Text="Select Code Book:" 
                            Width="150px"></asp:Label>
                    </td>
                    <td class="style65" >
                        <asp:DropDownList ID="ddlCodeBook" runat="server" BackColor="#68AED0" 
                            Font-Bold="True" Font-Size="16px" style="margin-left: 0px" Width="380px">
                        </asp:DropDownList>
                        <cc1:ListSearchExtender ID="ddlCodeBook_ListSearchExtender" runat="server" 
                            Enabled="True" QueryPattern="Contains" TargetControlID="ddlCodeBook">
                        </cc1:ListSearchExtender>
                        <asp:Label ID="lbalterofddl" runat="server" BackColor="#68AED0" 
                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="14px" style="margin-bottom:1px" Visible="False" Width="380px"></asp:Label>
                    </td>
                    <td class="style57">
                        <asp:DropDownList ID="ddlCodeBookSegment" runat="server" BackColor="#68AED0" 
                            Font-Bold="True" Font-Size="16px" style="margin-left: 0px" Width="129px">
                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                            <asp:ListItem Value="8">Sub Code-2</asp:ListItem>
                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lbalterofddl0" runat="server" BackColor="#68AED0" 
                            BorderColor="#666633" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="14px" style="margin-bottom:1px; color: #000000;" 
                            Visible="False" Width="124px"></asp:Label>
                    </td>
                    <td class="style55">
                        <asp:LinkButton ID="lnkok" runat="server" Font-Bold="True" Font-Size="16px" 
                            Height="16px" onclick="lnkok_Click" Width="43px" style="color: #FFFFFF">OK</asp:LinkButton>
                        <asp:LinkButton ID="lnkcancel" runat="server" Font-Bold="True" Font-Size="16px" 
                            onclick="lnkcancel_Click" Visible="False" Width="50px" 
                            style="color: #FFFFFF">Change</asp:LinkButton>
                    </td>
                    <td class="style64">
                        &nbsp;</td>
                    <td class="style59">
                        &nbsp;</td>
                    <td class="style56">
                        &nbsp;</td>
                    <td class="style58">
                        &nbsp;</td>
                    <td>
                        <br />
                    </td>
                </tr>
            </table>
            
            <table class="style17">
                <tr>
                    <td>
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid" 
                            BorderWidth="2px" CellPadding="4" Font-Bold="False" Font-Size="12px" 
                            onrowcancelingedit="grvacc_RowCancelingEdit" onrowediting="grvacc_RowEditing" 
                            onrowupdating="grvacc_RowUpdating" Width="726px" PageSize="15">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" 
                                Visible="False" />
                            <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" style="text-align: right" 
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
                                        <asp:Label ID="lbgrcode" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Height="16px" MaxLength="12" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>' 
                                            Width="90px"></asp:TextBox>
                                        <asp:Label ID="lbgrcod1" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode3")) %>' 
                                            Visible="False"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>' 
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Accounts">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="350px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 44%;">
                                            <tr>
                                                <td class="style63">
                                                    <asp:Label ID="Label8" runat="server" Text="Head of Accounts" Width="160px"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style61">
                                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True" 
                                                        Font-Bold="True" Font-Size="14px" 
                                                        onselectedindexchanged="ddlPageNo_SelectedIndexChanged" 
                                                        style="BORDER-RIGHT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-BOTTOM: navy 1px solid" 
                                                        Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Level">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgridlevel" runat="server" MaxLength="10" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>' 
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Font-Size="12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>' 
                                            Width="40px" style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTypeCode" runat="server" Font-Size="12px" MaxLength="20" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>' 
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>' 
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTypeDesc" runat="server" Font-Size="12px" MaxLength="100" 
                                            style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>' 
                                            Width="200px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" style="FONT-SIZE: 12px" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>' 
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
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

