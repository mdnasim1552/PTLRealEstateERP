<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBgdStdAna.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptBgdStdAna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style12
        {
            width: 133px;
        }
        .style11
        {
            width: 174px;
        }
        .style13
    {
        width: 457px;
    }
        .style15
        {
        }
        .style17
        {
            width: 12px;
        }
        .style20
        {
            width: 2px;
        }
        .newStyle1
        {
            border: 1px solid #99CCFF;
        }
        .newStyle2
        {
            float: none;
        }
        .style22
        {
        }
        .style25
        {
        }
        .style28
        {
            width: 33px;
        }
        .newStyle3
        {
            border-style: none;
            border-width: 1px;
        }
        .style33
        {
        }
        .style34
        {
            width: 18px;
        }
        .style36
        {
            width: 70px;
        }
        .style37
        {
            width: 281px;
        }
        .newStyle4
        {
            background-color: #D2D2BD;
        }
        .style43
        {
            width: 27px;
        }
                
        .style107
        {
            width: 61px;
        }
        .style111
        {
            width: 120px;
        }
        .style113
        {
            width: 99px;
        }
        .style114
        {
            width: 7px;
        }
        .style115
        {
            width: 8px;
        }
        .style116
        {
            width: 16px;
        }
        
        .style118
        {
        }
        
                 .style101
        {
            BACKGROUND-COLOR: transparent;
            BORDER-TOP-STYLE: none; 
            BORDER-RIGHT-STYLE: none; 
            BORDER-LEFT-STYLE: none; 
            TEXT-ALIGN: right; 
            BORDER-BOTTOM-STYLE: none;
            font-size:11px
        }
         .style119
        {
            width: 20px;
        }
        .style120
        {
            width: 227px;
        }
        .style121
        {
            width: 311px;
        }
        .style123
        {
            width: 78px;
        }
        .style124
        {
            width: 272px;
        }
        .style125
        {
            width: 37px;
        }
        .style127
        {
            width: 107px;
        }
        .style129
        {
            width: 361px;
        }
        .style130
        {
            width: 171px;
        }
        .style131
        {
            width: 193px;
        }
         .HeaderStyle
        {
            text-transform:capitalize;
            
            }
        </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
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
                Font-Underline="True" style="font-weight: 700; text-align: right; color: #FFFFFF;" 
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
            <asp:Label ID="lblRptType" runat="server" Visible="False" Width="99px"></asp:Label></td>
    </tr>
</table>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                            BorderWidth="1px">
            <table style="width:60%;">
                <tr>
                    <td class="style20">
                        <asp:Label ID="lblItemList" runat="server" CssClass="style17" 
                            Font-Bold="True" Font-Size="12px" ForeColor="White" style="text-align: left;" 
                            Text="Item Code:" Width="100px"></asp:Label>
                    </td>
                    <td class="style15" colspan="2">
                        <asp:Label ID="lbItemCode" runat="server" BackColor="White" 
                            CssClass="newStyle1" Font-Bold="True" Font-Size="12px" ForeColor="Blue" 
                            Height="18px" style="font-weight: 700" Width="400px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblItem3" runat="server" CssClass="newStyle3" Font-Size="12px" 
                Font-Underline="True" style="font-weight: 700; text-align: right; color: #FFFFFF;" 
                Text="Std Qty" Width="80px"></asp:Label>
                    </td>
                    <td class="style28">
                        <asp:Label ID="lblItem8" runat="server" CssClass="newStyle3" Font-Size="12px" 
                Font-Underline="True" style="font-weight: 700; text-align: left; color: #FFFFFF;" Text="Unit" 
                Width="45px"></asp:Label>
                    </td>
                    <td class="style130">
                        &nbsp;</td>
                    <td class="style129">
                        &nbsp;</td>
                    <td class="style120">
                        &nbsp;</td>
                    <td class="style127">
                        &nbsp;</td>
                    <td class="style111">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style20">
                        <asp:Label ID="lblItemDes" runat="server" CssClass="style17" 
                            Font-Bold="True" Font-Size="12px" ForeColor="White" style="text-align: left;" 
                            Text="Item Description:" Width="100px"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblItemDesc" runat="server" BackColor="White" 
                            CssClass="newStyle1" Font-Bold="True" Font-Size="12px" ForeColor="Blue" 
                            Height="18px" style="font-weight: 700" Width="400px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblStdQtyF" runat="server" BackColor="White" 
                CssClass="newStyle3" Font-Size="12px" Height="18px" 
                style="font-weight: 700; text-align: right" Text=" " Width="80px"></asp:Label>
                    </td>
                    <td class="style28">
                        <asp:Label ID="lblUnitFPS" runat="server" BackColor="White" 
                CssClass="newStyle3" Font-Size="12px" Height="18px" style="font-weight: 700; " 
                Text=" " Width="63px"></asp:Label>
                    </td>
                    <td class="style130">
                        &nbsp;</td>
                    <td class="style129">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="50">
                            <ProgressTemplate>
                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" 
                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="18px" 
                                    ForeColor="Yellow" style="text-align:center" Text="Please wait . . . . . . ." 
                                    Width="218px"></asp:Label>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    <td class="style120">
                        &nbsp;</td>
                    <td class="style127">
                        &nbsp;</td>
                    <td class="style111">
                        &nbsp;</td>
                </tr>
                </table>
                </asp:Panel>
             <table style="width:65%;">
                <tr>
                    <td class="style22" colspan="8">
                        <asp:Panel ID="PnlAnalysis" runat="server" BackColor="#CECEB5" Visible="False" 
                            Width="891px">
                            <table style="width: 924px; margin-right: 0px">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style33">
                                        &nbsp;</td>
                                    <td class="style34">
                                        &nbsp;</td>
                                    <td class="style37">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style107">
                                        <asp:Label ID="lblItem6" runat="server" CssClass="newStyle3" Font-Size="12px" 
                                            Font-Underline="False" style="font-weight: 700" Text="Current Column Group:" 
                                            Width="131px"></asp:Label>
                                    </td>
                                    <td class="style119">
                                        <asp:Label ID="lblColGroup" runat="server" BackColor="#660033" Font-Size="25px" 
                                            ForeColor="White" style="text-align: center; font-weight: 700" Width="24px">1</asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:LinkButton ID="lbtngvP1" runat="server" Font-Bold="True" Font-Size="12px" 
                                            Height="16px" onclick="lbtngvP_Click" style="text-align: center" Width="17px">1</asp:LinkButton>
                                    </td>
                                    <td class="style17">
                                        <asp:LinkButton ID="lbtngvP2" runat="server" Font-Bold="True" Font-Size="12px" 
                                            onclick="lbtngvP_Click" style="text-align: center" Width="17px">2</asp:LinkButton>
                                    </td>
                                    <td class="style34">
                                        <asp:LinkButton ID="lbtngvP3" runat="server" Font-Bold="True" Font-Size="12px" 
                                            onclick="lbtngvP_Click" style="text-align: center" Width="17px">3</asp:LinkButton>
                                    </td>
                                    <td class="style114">
                                        <asp:LinkButton ID="lbtngvP4" runat="server" Font-Bold="True" Font-Size="12px" 
                                            onclick="lbtngvP_Click" style="text-align: center" Width="17px">4</asp:LinkButton>
                                    </td>
                                    <td class="style115">
                                        <asp:LinkButton ID="lbtngvP5" runat="server" Font-Bold="True" Font-Size="12px" 
                                            onclick="lbtngvP_Click" style="text-align: center" Width="17px">5</asp:LinkButton>
                                    </td>
                                    <td class="style116">
                                        <asp:LinkButton ID="lbtngvP6" runat="server" Font-Bold="True" Font-Size="12px" 
                                            onclick="lbtngvP_Click" style="text-align: center" Width="17px">6</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtngvP7" runat="server" Font-Bold="True" Font-Size="12px" 
                                            onclick="lbtngvP_Click" style="text-align: center" Width="17px">7</asp:LinkButton>
                                    </td>
                                    <td class="style113">
                                        <asp:LinkButton ID="lbtngvP8" runat="server" Font-Bold="True" Font-Size="12px" 
                                            onclick="lbtngvP_Click" style="text-align: center" Width="17px">8</asp:LinkButton>
                                    </td>
                                    <td class="style113">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style33" colspan="16">
                                        <asp:GridView ID="gvAnalysis" runat="server" AutoGenerateColumns="False" 
                                            HeaderStyle-CssClass="HeaderStyle" Width="16px">
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtngvSlNo" runat="server" Font-Bold="True" 
                                                            Font-Size="12px" onclick="lbtngvSlNo_Click" style="text-align: center" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Res. Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResCod1" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>' 
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Res. Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>' 
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Resources">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResDesc" runat="server" Font-Bold="True" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>' 
                                                            Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnit" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>' 
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Init.Work">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty001" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty001")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobiliz.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty002" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty002")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SubStruc.">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty003" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty003")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Base-1">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty004" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty004")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Base-2">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty005" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty005")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Base-3">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty006" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty006")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Base-4">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty007" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty007")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Base-5">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty008" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty008")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gr. Floor">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty009" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty009")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="1st Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty010" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty010")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="2nd Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty011" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty011")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="3rd Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty012" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty012")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="4th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty013" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty013")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="5th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty014" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty014")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="6th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty015" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty015")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="7th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty016" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty016")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="8th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty017" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty017")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="9th floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty018" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty018")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="10th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty019" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty019")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="11th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty020" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty020")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="12th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty021" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty021")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="13th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty022" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty022")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="14th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty023" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty023")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="15th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty024" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty024")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="16th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty025" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty025")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="17th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty026" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty026")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="18th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty027" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty027")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="19th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty028" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty028")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="20th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty029" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty029")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="21st Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty030" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty030")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="22ndFloor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty031" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty031")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="23rd Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty032" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty032")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="24th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty033" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty033")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="25th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty034" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty034")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="26th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty035" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty035")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="27th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty036" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty036")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="28th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty037" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty037")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="29th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty038" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty038")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="30th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty039" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty039")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="31st Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty040" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty040")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="32nd Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty041" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty041")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="33nd Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty042" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty042")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="34th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty043" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty043")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="35th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty044" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty044")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="36nd Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty045" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty045")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="37th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty046" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty046")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="38th Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty047" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty047")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Roof Top" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty048" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty048")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mezz.Floor" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty049" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty049")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comm.Work" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty050" runat="server" CssClass="style101" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty050")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                            Width="55px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#333333" />
                                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style33">
                                        &nbsp;</td>
                                    <td class="style34">
                                        &nbsp;</td>
                                    <td class="style37">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style107">
                                        &nbsp;</td>
                                    <td class="style119">
                                        &nbsp;</td>
                                    <td class="style43">
                                        &nbsp;</td>
                                    <td class="style17">
                                        &nbsp;</td>
                                    <td class="style34">
                                        &nbsp;</td>
                                    <td class="style114">
                                        &nbsp;</td>
                                    <td class="style115">
                                        &nbsp;</td>
                                    <td class="style116">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style113">
                                        &nbsp;</td>
                                    <td class="style113">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td class="style127">
                        &nbsp;</td>
                    <td class="style111">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style20">
                        &nbsp;</td>
                    <td class="style118" colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style28">
                        &nbsp;</td>
                    <td class="style130">
                        &nbsp;</td>
                    <td class="style129">
                        &nbsp;</td>
                    <td class="style120">
                        &nbsp;</td>
                    <td class="style127">
                        &nbsp;</td>
                    <td class="style111">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style20">
                        &nbsp;</td>
                    <td class="style118">
                        &nbsp;</td>
                    <td class="style131">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style28">
                        &nbsp;</td>
                    <td class="style130">
                        &nbsp;</td>
                    <td class="style129">
                        &nbsp;</td>
                    <td class="style120">
                        &nbsp;</td>
                    <td class="style127">
                        &nbsp;</td>
                    <td class="style111">
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

