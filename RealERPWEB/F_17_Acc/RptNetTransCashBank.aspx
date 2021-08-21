<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptNetTransCashBank.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptNetTransCashBank" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style33
        {
            width: 51px;
        }
        .style34
        {
            width: 43px;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 11px;
            font-weight: normal;
            margin-right: 0px;
        }
        .style32
        {
            width: 12px;
        }
        .style35
        {
            width: 848px;
        }
        .style36
        {
            width: 215px;
        }
        .style38
        {
            width: 106px;
        }
        .style39
        {
            width: 36px;
        }
        .style40
        {
            color: #FFFFFF;
            }
        .style58
        {
            width: 75px;
        }
        .style60
        {
            width: 17px;
        }
        .style61
        {
        }
        .style62
        {
            width: 38px;
        }
        .style64
        {
        }
        .style69
        {
            width: 39px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
     <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
     <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

           
            
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>
    <table style="width: 91%;">
        <tr>
            <td class="style35">
                <asp:Label ID="LblTitle" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="Cheque Clearance Report" Width="500px" Style="border-bottom: 1px solid WHITE;
                    border-top: 1px solid WHITE;"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    Style="color: #FFFFFF" CssClass="button">PRINT</asp:LinkButton>
            </td>
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
            <table style="width: 100%;">
                <tr>
                    <td colspan="8">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                    BorderWidth="1px">
                            <table style="width: 100%;">
                               
                                <tr>
                                    <td class="style62">
                                        <asp:Label ID="Label22" runat="server" CssClass="style40" Font-Bold="True" 
                                            Style="text-align: left" Text="From:" Width="50px" Height="12px"></asp:Label>
                                    </td>
                                    <td class="style58">
                                        <asp:TextBox ID="txtfromdate" runat="server" 
                                            Font-Bold="True" Width="85px" BorderStyle="None" Height="16px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style60">
                                        <asp:Label ID="Label23" runat="server" CssClass="style40" Font-Bold="True" 
                                            Style="text-align: right" Text="To:" Height="12px"></asp:Label>
                                    </td>
                                    <td class="style61">
                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Font-Bold="True" 
                                            Width="87px" Height="16px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat="">
                                        </cc1:CalendarExtender>
                                     
                                    </td>
                                    <td class="style69">
                                        <asp:LinkButton ID="lbtnShow" runat="server" BackColor="#003366" 
                                            BorderColor="White" BorderStyle="Solid" Font-Bold="True" 
                                            Font-Size="12px" Font-Underline="False" OnClick="lbtnShow_Click" 
                                            Width="40px" BorderWidth="1px" ForeColor="White" 
                                            style="text-align: center; height: 17px;">Show</asp:LinkButton>
                                    </td>
                                    <td class="style62">
                                        <asp:Label ID="lblGroup" runat="server" Font-Size="12px" ForeColor="White" 
                                            style="font-weight: 700; text-align: right" Text="Group:" Width="60px"></asp:Label>
                                    </td>
                                    <td class="style36">
                                        <asp:RadioButtonList ID="rbtnGroup" runat="server" BackColor="#BBBB99" 
                                            BorderColor="#FFCC00" BorderStyle="None" Font-Bold="True" Font-Size="14px" 
                                            RepeatColumns="6" RepeatDirection="Horizontal" Style="text-align: center" 
                                            Width="235px">
                                            <asp:ListItem>Deposit</asp:ListItem>
                                            <asp:ListItem Selected="True">Withdraw</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td class="style36">
                                        &nbsp;</td>
                                    <td align="left" class="style33">
                                        &nbsp;</td>
                                    <td class="style34">
                                        &nbsp;</td>
                                    <td class="style32">
                                        &nbsp;</td>
                                    <td class="style39">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                               
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                                <table style="width: 100%;">
                                     <tr>
                                        <td class="style64">
                                            <asp:Label ID="lblReceiptCash" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="Yellow"
                                                Text="Deposit" Width="162px" Visible="False"></asp:Label>
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
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvcashbook" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="931px" onrowdatabound="gvcashbook_RowDataBound">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Issue Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvchequeDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Voucher Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Clearing Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt1")) %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    
                                                    <asp:TemplateField HeaderText="Issue #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvisunum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Voucher #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvvnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                                        <HeaderTemplate>
                                                            <table style="width:23%;">
                                                                <tr>
                                                                    <td class="style58">
                                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Cash/Bank Name" 
                                                                            Width="108px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" BackColor="#000066" 
                                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                            ForeColor="White" style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    
                                                    <asp:TemplateField HeaderText="Pay To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                Width="130px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Narration">
                                                    
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                         <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Cash/Bank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvDpAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srcham")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:Label ID="lblDetailsCash" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="Yellow"
                                                Text="Details of Cash &amp; Bank Balance" Width="669px" Height="16px" 
                                                Visible="False" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvcashbookDB" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash/Bank Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="300px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Deposit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrecam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Withdraw">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpayam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                                    Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

