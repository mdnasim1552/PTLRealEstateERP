<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="InvResourceRequirement.aspx.cs" Inherits="RealERPWEB.F_09_PImp.InvResourceRequirement" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

.ddl
{
	border: 1px solid #7393B1;
	padding: 1px;
	font-size: 11px;
	font-style: normal;
	margin-left: 7px;
	background-color: #FFFFFF;
            text-align: left;
        }
        
        .style12
        {
            width: 101px;
        }
        .style17
        {
            width: 304px;
        }
        .style18
        {
            width: 303px;
        }
        .style19
        {
            width: 74px;
        }
        .style20
        {
            width: 219px;
        }
        .style21
        {
            width: 206px;
        }
        .style23
        {
            width: 99px;
        }
        .style24
        {
            width: 27px;
        }
        .style25
        {
            width: 10px;
        }
        
        .style26
        {
            width: 98px;
            height: 9px;
        }
        .style27
        {
            color: #FFFFFF;
        }
        
        .style28
        {
            width: 18px;
        }
        
        .style29
        {
            width: 594px;
        }
        .style31
        {
            width: 37px;
        }
        .style32
        {
            width: 2px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 922px; border-bottom: #d2f4c0 2px outset; height: 0px;">
        <tr>
            <td class="style57">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="18px"
                                
                    
                    
                    
                    Style="border: 2px inset #ffcc99; color: maroon; background-color: #fffbf1; " Text="Resource Requirement Information "
                                Width="446px" BorderStyle="Inset" BackColor="Transparent" 
                    BorderWidth="2px"></asp:Label>
            </td>
            <td>
                                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                                    </td>
            <td class="style58">
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td class="style59">
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" Font-Italic="True"
                                Font-Size="18px" 
                    Style="background-color: #fffbf1; text-align: center" Width="69px" 
                    BorderStyle="Inset" OnClick="lnkPrint_Click" BorderColor="#FFC080" 
                    BorderWidth="2px">Print</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
             <table style="width:77%;">
                <tr>
                    <td class="style17" colspan="15">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                    BorderWidth="1px" Width="844px">
                            <table style="width:100%;">
                                <tr>
                                    <td class="style26">
                                        &nbsp;</td>
                                    <td class="style12">
                                        &nbsp;</td>
                                    <td class="style32">
                                        &nbsp;</td>
                                    <td colspan="25">
                                        <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="True" 
                                            BackColor="#BBBB99" BorderColor="#FFCC00" BorderStyle="Solid" BorderWidth="1px" 
                                            Font-Bold="True" Font-Size="14px" RepeatColumns="6" 
                                            RepeatDirection="Horizontal" style="text-align: left" Width="300px">
                                            <asp:ListItem>Individual Item</asp:ListItem>
                                            <asp:ListItem>All Project</asp:ListItem>
                                        </asp:RadioButtonList>
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
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style26">
                                        <asp:Label ID="lbldate" runat="server" 
                                            style="text-align: left; font-weight: 700;" Text="From:" Width="110px" 
                                            CssClass="style27" Font-Bold="True" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style12">
                                        <asp:TextBox ID="txtfrmDate" runat="server" BorderStyle="None" Height="18px" 
                                            Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style32">
                                        <asp:Label ID="lbldate0" runat="server" 
                                            style="text-align: right; font-weight: 700;" Text="To:" CssClass="style27"></asp:Label>
                                    </td>
                                    <td colspan="25">
                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Height="18px" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="caltodate" runat="server" Format="dd-MMM-yyyy " 
                                            TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
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
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style26">
                                        <asp:Label ID="lbldate1" runat="server" 
                                            style="text-align: left; font-weight: 700;" Text="Implementation No:" 
                                            Width="110px" CssClass="style27" Font-Bold="True" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style12">
                                        <asp:TextBox ID="txtProjectSearch" runat="server" BorderStyle="None" 
                                            Height="18px" Width="100px"></asp:TextBox>
                                    </td>
                                    <td class="style32">
                                        <asp:ImageButton ID="ImgbtnFindImpNo" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindImpNo_Click" 
                                            Width="16px" />
                                    </td>
                                    <td colspan="25">
                                        <asp:DropDownList ID="ddlImpNO" runat="server" CssClass="newStyle1" 
                                            Font-Bold="True" Font-Size="11px" Height="21px" Width="300px" 
                                            AutoPostBack="True" onselectedindexchanged="ddlImpNO_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                    Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                    style="text-align: center" Width="60px">Ok</asp:LinkButton>
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
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style26">
                                        <asp:Label ID="lblItem10" runat="server" CssClass="style27" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" Height="16px" 
                                            style="font-weight: 700; text-align:left" Text="Floor :" Width="110px"></asp:Label>
                                    </td>
                                    <td class="style12">
                                        <asp:DropDownList ID="ddlFloorListRpt" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Height="21px" style="text-transform: capitalize" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style32">
                                        &nbsp;</td>
                                    <td class="style31">
                                        <asp:Label ID="lblRptGroup" runat="server" CssClass="style27" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" 
                                            style="font-weight: 700; text-align:right" Text="Group :" Width="50px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True" 
                                            Font-Size="12px" Height="21px" style="text-transform: capitalize" Width="100px">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
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
             <table style="width:99%;">
                 
                 <tr>
                     <td colspan="18" class="style18">
                         <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" 
                             BackColor="#FFCCFF" ShowFooter="True" Width="847px">
                             <Columns>
                                 <asp:TemplateField HeaderText="Floor">
                                     <ItemTemplate>
                                         <asp:Label ID="lblgvRptFlr1" runat="server" Font-Bold="False" Font-Size="12px" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>' 
                                             Width="120px"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Sl.No.">
                                     <ItemTemplate>
                                         <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" 
                                             style="text-align: right" 
                                             Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField FooterText="Total" HeaderText="Resource Description">
                                     <ItemTemplate>
                                         <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>' 
                                             Width="300px"></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Unit">
                                     <ItemTemplate>
                                         <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px" 
                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>' 
                                             Width="30px"></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Quantity">
                                     <ItemTemplate>
                                         <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px" 
                                             style="text-align: right" 
                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00);-") %>' 
                                             Width="80px"></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <ItemStyle HorizontalAlign="Right" />
                                     <FooterStyle HorizontalAlign="Right" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Rate">
                                     <ItemTemplate>
                                         <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px" 
                                             style="text-align: right" 
                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00);-") %>' 
                                             Width="80px"></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Center" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Amount">
                                     <ItemTemplate>
                                         <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px" 
                                             style="text-align: right" 
                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00);-") %>' 
                                             Width="80px"></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle HorizontalAlign="Center" />
                                     <FooterStyle Font-Bold="true" Font-Size="14px" HorizontalAlign="Right" />
                                 </asp:TemplateField>
                             </Columns>
                             <FooterStyle BackColor="#99CCFF" BorderColor="#660033" BorderStyle="Solid" 
                                 BorderWidth="1px" />
                             <HeaderStyle BackColor="#FFFFCC" BorderColor="#660033" BorderStyle="Solid" 
                                 Font-Size="12px" ForeColor="#660033" />
                             <AlternatingRowStyle BackColor="#CFCFB8" />
                         </asp:GridView>
                     </td>
                 </tr>
                 <tr>
                     <td class="style18">
                         &nbsp;</td>
                     <td class="style43">
                         &nbsp;</td>
                     <td class="style21">
                         &nbsp;</td>
                     <td class="style20">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td class="style136">
                         &nbsp;</td>
                     <td class="style152">
                         &nbsp;</td>
                     <td class="style153">
                         &nbsp;</td>
                     <td class="style125">
                         &nbsp;</td>
                     <td class="style29">
                         &nbsp;</td>
                     <td class="style123">
                         &nbsp;</td>
                     <td class="style119">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td class="style43">
                         &nbsp;</td>
                     <td class="style112">
                         &nbsp;</td>
                     <td class="style111">
                         &nbsp;</td>
                 </tr>
             </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

