<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptOnlinePayment.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.RptOnlinePayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style12
        {
            width: 499px;
        }
        .style15
        {
            width: 12px;
        }
        .style20
        {
            width: 268px;
        }
        .style27
        {
            width: 102px;
        }
        .style29
        {
            width: 72px;
        }
        .style30
        {
            color: #FFFFFF;
        }
        .style31
        {
            width: 214px;
        }
        .style57
        {
            width: 268px;
            height: 11px;
        }
        .style58
        {
            width: 210px;
            height: 11px;
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
        .style71
        {
            width: 98px;
        }
        .style72
        {
            width: 75px;
        }
        .style73
        {
            width: 10px;
        }
        .style93
        {
            width: 32px;
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

            var gvChqSign = $('#<%=this.gvChqSign.ClientID %>');


            gvChqSign.Scrollable();


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
            <table style="width: 100%;">
                <tr>
                    <td colspan="11">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style29" width="100px">
                                        <asp:Label ID="lblFdate" runat="server" CssClass="style30" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: left" Text="From :" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtfromdate" runat="server" BorderColor="#660033" BorderStyle="Solid"
                                            BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy " TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td class="style65">
                                        <asp:Label ID="lblTdate" runat="server" CssClass="style30" Font-Bold="True" Font-Size="12px"
                                            Text="To:"></asp:Label>
                                    </td>
                                    <td align="left" class="style64">
                                        <asp:TextBox ID="txttodate" runat="server" BorderColor="#660033" BorderStyle="Solid"
                                            BorderWidth="1px" CssClass="txtboxformat" Font-Bold="True" Width="100px" TabIndex="1"></asp:TextBox>
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
                                            Style="margin-left: 0px" Width="85px" TabIndex="2">
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
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="White"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" OnClick="lbtnOk_Click"
                                            Style="color: #FFFFFF; text-align: center;" TabIndex="5" Font-Underline="False"
                                            Height="16px" Width="29px">Ok</asp:LinkButton>
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
                                        <asp:Label ID="lblChqNo" runat="server" BorderStyle="None" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Style="text-align: left" Text=" Cheque No:" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style27">
                                        <asp:TextBox ID="txtSrchChequeno" runat="server" BorderStyle="None" TabIndex="3"
                                            Width="100px" CssClass="txtboxformat"></asp:TextBox>
                                    </td>
                                    <td class="style65">
                                        <asp:ImageButton ID="imgSearchCheque" runat="server" Height="16px" ImageUrl="~/Image/find_images.jpg"
                                            OnClick="imgSearchCheque_Click" Style="margin-left: 0px" TabIndex="4" Width="16px" />
                                    </td>
                                    <td align="left" class="style64">
                                        &nbsp;</td>
                                    <td align="left" class="style93">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="style66">
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
                   <asp:View ID="ChqSignSheet" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="11">
                                <asp:Panel ID="Panel3" runat="server" BorderColor="Maroon" BorderStyle="Solid" BorderWidth="1px">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style71">
                                                <asp:Label ID="lblBankName" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                    Text="Bank Name :" Width="75px"></asp:Label>
                                            </td>
                                            <td class="style72">
                                                <asp:TextBox ID="txtSerchBank" runat="server" BorderStyle="None" TabIndex="12" Width="100px"></asp:TextBox>
                                            </td>
                                            <td class="style73">
                                                <asp:ImageButton ID="ibtnSrchBank" runat="server" Height="16px" ImageUrl="~/Image/find_images.jpg"
                                                    OnClick="ibtnSrchBank_Click" TabIndex="13" Width="16px" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBankName" runat="server" TabIndex="14" Width="300px">
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
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11">
                                <asp:GridView ID="gvChqSign" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Style="text-align: left" Width="546px" AllowPaging="True">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNor" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher #">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvNar" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vouchar Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvVouDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSign" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() : "")
                                                                         
                                                                    %>' Width="150px"> </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbankdesc" runat="server" Font-Bold="true" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc"))    %>'
                                                    Width="150px">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Approved Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblFaTotal" runat="server" ForeColor="White"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblFTotal" runat="server" ForeColor="White"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcashamtdep" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvuChq" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmrDatedep" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrmrks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Record No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvIsu" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Narration">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvNar" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vernar"))%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay to">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto"))%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style57">
                            </td>
                            <td class="style15">
                            </td>
                            <td class="style20">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td class="style58">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:View>

                   <asp:View ID="ViewPayApp" runat="server">
                    <table style="width: 100%;">
                        
                        <tr>
                            <td colspan="11">
                                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Style="margin-top: 0px" Width="831px" AllowPaging="True" 
                                    onpageindexchanging="gvPayment_PageIndexChanging">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue #">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="User">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvUsrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%#  "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ref #">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="lblFTotal" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="lblFBalAmt" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblFApamt" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAppramt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Appx. payment Date">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyyy")  %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                
                              
                                <asp:TemplateField HeaderText="No of Cheque">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvchq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nochq")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style57">
                            </td>
                            <td class="style15">
                            </td>
                            <td class="style20">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td class="style58">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                   
                   <asp:View ID="ViewPayRec" runat="server">
                    <table style="width: 100%;">
                        
                        <tr>
                            <td colspan="11">
                                <asp:GridView ID="gvPayRec" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Style="margin-top: 0px" Width="831px">
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Record No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrcvdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details Head">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvResdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adv. Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFAdvTotal" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAdvamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFNetTotal" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNetamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvValdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Appx. payment Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apppaydate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill Nature">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnature" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            </td>
                            <td class="style57">
                            </td>
                            <td class="style15">
                            </td>
                            <td class="style20">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td class="style58">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                    
                    <asp:View ID="ViewPayRecApp" runat="server">
                    <table style="width: 100%;">
                        
                        <tr>
                            <td colspan="11">
                                <asp:GridView ID="gvPayapp" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Style="margin-top: 0px" Width="831px" 
                                   >
                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoidPayApp" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Record No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvslnumPayApp" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Recived Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvrcvdatePayApp" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apdate")).ToString("dd-MMM-yyyy")%>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvactdescPayApp" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details Head">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvResdescPayApp" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Adv. Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFAdvTotal2" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAdvamt2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amt">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ForeColor="White"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillamtPayApp" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Ref. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrefPayApp" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Appx. payment Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaymentdatePayApp" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apppaydate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Bill No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvbillnoPayApp" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                
                                
                            </Columns>
                            <FooterStyle BackColor="#333333" />
                            <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" ForeColor="#FFFFCC"
                                Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                        </asp:GridView>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            </td>
                            <td class="style57">
                            </td>
                            <td class="style15">
                            </td>
                            <td class="style20">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td class="style58">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
