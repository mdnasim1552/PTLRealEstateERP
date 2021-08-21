<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptReceivedList.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptReceivedList" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    
    

    
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .style12
        {
            width: 102px;
        }
        .style18
        {
            width: 19px;
            height: 7px;
        }
        .style19
        {
            color: #FFFFFF;
        }
        .style21
        {
            width: 216px;
        }
        .style53
        {
            height: 7px;
            }
        .style54
        {
            height: 7px;
        }
        .style102
    {
        width: 5px;
    }
        .style100
    {
        width: 105px;
    }
            
        .style72
        {
            width: 114px;
        }
        .style105
        {
            width: 97px;
        }
        .StyleCheckBoxList
        {
            text-transform: capitalize;
            margin-bottom: 0px;
        }
        
        .style107
        {
            height: 7px;
            }
        .style111
        {
            height: 7px;
            width: 13px;
        }
        .style112
        {
            width: 622px;
            height: 7px;
        }
        .style113
        {
            width: 36px;
            height: 7px;
        }
        .style114
        {
            width: 506px;
            height: 7px;
        }
        </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 98%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px" 
                    ForeColor="Yellow" Text="RECEIVED LIST INFORMATION" Width="600px"
                   STYLE="border-bottom:1px solid blue;border-top:1px solid blue;" ></asp:Label>
            </td>
            <td class="style21">
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
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" 
                    onclick="lbtnPrint_Click" CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
                
                
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                        <table style="width:100%;">
                            <tr>
                                <td colspan="11">
                                
                                    <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                        BorderWidth="1px">
                                     
                                        <table style="width:100%;">
                                            <tr>
                                                <td class="style53" align="right" colspan="30">
                                                    <asp:Panel ID="Panel11" runat="server">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td class="style102">
                                                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="14px" 
                                                                        ForeColor="Yellow" 
                                                                        style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;" 
                                                                        Text="Project Name:" Width="100px"></asp:Label>
                                                                </td>
                                                                <td class="style100">
                                                                    <asp:CheckBox ID="chkDeselectAll" runat="server" AutoPostBack="True" 
                                                                        BackColor="#000066" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" 
                                                                        Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                                        oncheckedchanged="chkDeselectAll_CheckedChanged" Text="Deslect All" 
                                                                        Width="90px" />
                                                                </td>
                                                                <td class="style72">
                                                                    &nbsp;</td>
                                                                <td class="style105">
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
                                            <tr>
                                                <td class="style107" colspan="25">
                                                    <asp:CheckBoxList ID="cblProject" runat="server" BorderColor="#FFCC00" 
                                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="0" CellSpacing="0" 
                                                        CssClass="StyleCheckBoxList" Font-Bold="True" Font-Size="12px" 
                                                        ForeColor="White" Height="12px" RepeatColumns="6" RepeatDirection="Vertical" 
                                                        Width="1100px">
                                                        <asp:ListItem>aa</asp:ListItem>
                                                        <asp:ListItem>bb</asp:ListItem>
                                                        <asp:ListItem>cc</asp:ListItem>
                                                        <asp:ListItem>dd</asp:ListItem>
                                                        <asp:ListItem>ee</asp:ListItem>
                                                        <asp:ListItem>ff</asp:ListItem>
                                                        <asp:ListItem>gg</asp:ListItem>
                                                        <asp:ListItem>hh</asp:ListItem>
                                                        <asp:ListItem>mm</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                   
                                                </td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="style107">
                                                    <asp:Label ID="Label5" runat="server" CssClass="style19" Font-Bold="True" 
                                                        Font-Size="12px" style="text-align: right" Text="Date:" Width="100px"></asp:Label>
                                                </td>
                                                <td class="style111">
                                                    <asp:TextBox ID="txtfromdate" runat="server" BorderStyle="None" 
                                                        CssClass="txtboxformat" Font-Bold="True" Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" 
                                                        TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="style18">
                                                    <asp:Label ID="lblPage" runat="server" CssClass="style18" Font-Bold="True" 
                                                        Font-Size="12px" ForeColor="White" Height="16px" style="text-align: right" 
                                                        Text="Size:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style53">
                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                        onselectedindexchanged="ddlpagesize_SelectedIndexChanged" 
                                                        Width="100px">
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
                                                <td class="style114">
                                                    <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" 
                                                        onclick="lnkOk_Click" Width="78px">Ok</asp:LinkButton>
                                                </td>
                                                <td class="style112">
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" 
                                                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                                ForeColor="Yellow" style="text-align: left" Text="Please wait . . . . . . ." 
                                                                Width="120px"></asp:Label>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                                <td class="style112">
                                                    &nbsp;</td>
                                                <td class="style112">
                                                    &nbsp;</td>
                                                <td class="style112">
                                                    &nbsp;</td>
                                                <td class="style112">
                                                    &nbsp;</td>
                                                <td class="style112">
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
                                                <td class="style18">
                                                    &nbsp;</td>
                                                <td class="style54" valign="top">
                                                    &nbsp;</td>
                                                <td class="style54" valign="top">
                                                    &nbsp;</td>
                                                <td class="style54" valign="top">
                                                    &nbsp;</td>
                                                <td class="style54" valign="top">
                                                    &nbsp;</td>
                                                <td class="style54" valign="top">
                                                    &nbsp;</td>
                                                <td class="style54" valign="top">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                                <td class="style54">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                          
                                    </asp:Panel>
                                </td>
                            </tr>
                            
                                <tr>
                                    <td class="style22">
                                        <asp:GridView ID="dgvAccRec" runat="server" AutoGenerateColumns="False" 
                                            ShowFooter="True" style="text-align: left" Width="654px" 
                                            AllowPaging="True" onpageindexchanging="dgvAccRec_PageIndexChanging">
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" 
                                                            style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Pr.Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAccCod" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>' 
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgactdesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Cutomer Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgacuname" runat="server" 
                                                            Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>' 
                                                            Width="220px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgudesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sales Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgudesc" runat="server" 
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "salsdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact Person">
                                                    <FooterTemplate>
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="White" 
                                                            Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgCper" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cteam")) %>' 
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Amt">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvAcAmt" runat="server" Font-Bold="True" Font-Size="12px" style="text-align: right" 
                                                            ForeColor="White"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvAAmt" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Received Amt" >
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvRecAmt" runat="server" Font-Bold="True" Font-Size="12px" style="text-align: right" 
                                                            ForeColor="White"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvRamt" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Balance Amount">
                                                     <FooterTemplate>
                                                         <asp:Label ID="lgvBalAmt" runat="server" Font-Bold="True" Font-Size="12px" style="text-align: right" 
                                                             ForeColor="White"></asp:Label>
                                                     </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBamt" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Inst.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvTIns" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toinstall")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="25px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To be Paid Inst.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvTBPaid" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topay")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="55px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Paid Inst.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvPaid" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidins")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dues Inst.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDues" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueins")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dues Amt">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFDueAmt" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDamt" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px" style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#333333" HorizontalAlign="Right" />
                                            <PagerStyle HorizontalAlign="left" />
                                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style34">
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style198">
                                        <asp:Panel ID="PnlRmrks" runat="server" BorderColor="Maroon" 
                                            BorderStyle="Solid" BorderWidth="1px" Visible="False">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style210">
                                                        &nbsp;</td>
                                                    <td class="style214">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td class="style212">
                                                        &nbsp;</td>
                                                    <td class="style213">
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
                                                    <td class="style210">
                                                        &nbsp;</td>
                                                    <td align="left" class="style211" colspan="4">
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
                      
                            
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
            
</asp:Content>



