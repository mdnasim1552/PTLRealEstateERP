<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptChequestatus.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptChequestatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style12
        {
            width: 499px;
        }
        .style29
        {
        }
        .style30
        {
            color: #FFFFFF;
        }
        .style31
        {
            width: 214px;
        }
        .style64
        {
            width: 18px;
        }
        .txtboxformat
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 12px;
            font-weight: normal;
            margin-right: 0px;
            text-align: left;
            margin-left: 0px;
        }
        .style65
        {
            width: 14px;
        }
        .style66
        {
            width: 54px;
        }
        .style93
        {
            width: 32px;
        }
        .style94
        {
            width: 83px;
            margin-left: 40px;
        }
    </style>
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <table style="width: 98%;">
        <tr>
            <td class="style12">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" ForeColor="Yellow"
                    Text="TRANSACTION STATEMENT  INFORMATION VIEW/EDIT" Width="500px" Style="border-bottom: 1px solid blue;
                    border-top: 1px solid blue;"></asp:Label>
            </td>
            <td class="style31">
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" Style="font-size: 11px"
                    Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" OnClick="lbtnPrint_Click"
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
            <td>
                &nbsp;
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
            <td colspan="11">
                <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                    <table style="width: 100%;">
                        <tr>
                            <td class="style29" width="100px">
                                <asp:Label ID="lblSupplier" runat="server" CssClass="style30" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: left" Text="Supplier :" Width="100px"></asp:Label>
                            </td>
                            <td class="style29">
                                <asp:TextBox ID="txtSrchSupplier" runat="server" BorderColor="#660033" BorderStyle="None"
                                    BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" Width="80px" 
                                    TabIndex="1"></asp:TextBox>
                            </td>
                            <td class="style29">
                                <asp:ImageButton ID="ibtnFindSupplier" runat="server" Height="18px" ImageUrl="~/Image/find_images.jpg"
                                    OnClick="ibtnFindSupplier_Click" TabIndex="2" />
                            </td>
                            <td class="style29" colspan="3">
                                <asp:DropDownList ID="ddlSupplier" runat="server" Font-Bold="True" Font-Size="12px"
                                    TabIndex="3" Width="250px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnOk0" runat="server" BackColor="#003366" BorderColor="White"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" Font-Underline="False"
                                    Height="16px" OnClick="lbtnOk_Click" Style="color: #FFFFFF; text-align: center;"
                                    TabIndex="7" Width="29px">Ok</asp:LinkButton>
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
                        <tr>
                            <td class="style29" width="100px">
                                <asp:Label ID="lblFdate" runat="server" CssClass="style30" Font-Bold="True" Font-Size="12px"
                                    Style="text-align: left" Text="From :" Width="100px"></asp:Label>
                            </td>
                            <td class="style94">
                                <asp:TextBox ID="txtfromdate" runat="server" BorderColor="#660033" BorderStyle="None"
                                    BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" Width="80px" 
                                    TabIndex="4"></asp:TextBox>
                                <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtfromdate">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="style65">
                                <asp:Label ID="lblTdate" runat="server" CssClass="style30" Font-Bold="True" Font-Size="12px"
                                    Text="To:"></asp:Label>
                            </td>
                            <td align="left" class="style64">
                                <asp:TextBox ID="txttodate" runat="server" BorderColor="#660033" BorderStyle="None"
                                    BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" TabIndex="5" 
                                    Width="80px"></asp:TextBox>
                                <cc1:CalendarExtender ID="cetdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="left" class="style93">
                                <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                    Style="color: #FFFFFF; text-align: right;" Text="Page Size:" Width="70px"></asp:Label>
                            </td>
                            <td align="left" class="style66">
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" BackColor="#CCFFCC"
                                    Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Style="margin-left: 0px" TabIndex="6" Width="85px">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
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
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="ViewChequeStatus" runat="server">
            <asp:GridView ID="dgvChequeStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                Style="text-align: left" Width="1052px" OnRowDataBound="dgvChequeStatus_RowDataBound">
                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNop" runat="server" Font-Bold="True" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Voucher #">
                        <ItemTemplate>
                            <asp:Label ID="lgvvounum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1"))      %>'
                                Width="70px"> </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Voucher Date">
                        <ItemTemplate>
                            <asp:Label ID="lgvvoudate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1"))%>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Name">
                        <ItemTemplate>
                            <asp:Label ID="lgvpartyname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                Width="130px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Name">
                        <ItemTemplate>
                            <asp:Label ID="lgvprojectname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                Width="150px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Name">
                        <ItemTemplate>
                            <asp:Label ID="lgvbankname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                Width="150px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:Label ID="lgvchequeno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque Date">
                        <ItemTemplate>
                            <asp:Label ID="lgvchequedate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lgvCheam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chequeam")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFChequeam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                         <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:Label ID="lgvclchqno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clchequeno"))%>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Clearing Date">
                        <ItemTemplate>
                            <asp:Label ID="lgvchequecldate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cldate")) %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="lgvcchequeam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clchequeam")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lgvFclChequeam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                Style="text-align: right" Width="80px"></asp:Label>
                        </FooterTemplate>
                         <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#333333" />
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle Font-Size="12px" ForeColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                    Height="20px" HorizontalAlign="Center" />
                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
            </asp:GridView>
        </asp:View>
    </asp:MultiView>
      </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>
