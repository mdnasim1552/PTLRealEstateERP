<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPostNetTrnsCashBank.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptPostNetTrnsCashBank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table-bordered th {
            font-size: 15px;
            font-family: "Century Gothic";
        }

        .table-bordered tr td {
            padding: 6px 5px;
        }

        .table-bordered td {
            font-size: 13px;
            font-family: "Century Gothic";
        }

        .table td, .table th {
            padding: .75rem;
            vertical-align: top;
            border-top: none !important; 
        }
    </style>
    <style type="text/css">
        .style33 {
            width: 51px;
        }

        .style34 {
            width: 43px;
        }

        .txtboxformat {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            font-size: 11px;
            font-weight: normal;
            margin-right: 0px;
        }

        .style32 {
            width: 12px;
        }

        .style35 {
            width: 848px;
        }

        .style36 {
            width: 215px;
        }

        .style38 {
            width: 106px;
        }

        .style39 {
            width: 36px;
        }

        .style40 {
            color: #FFFFFF;
        }

        .style58 {
            width: 75px;
        }

        .style60 {
            width: 17px;
        }

        .style61 {
        }

        .style62 {
            width: 38px;
        }

        .style64 {
        }

        .style69 {
            width: 39px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {



            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            $('.chzn-select').chosen({ search_contains: true });

        }
    </script>


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

            <div class="card card-fluid">
                <div class=" card-body" style="min-height: 100px;">
                    <div class="row">
                        <div class="col-md-2">
                            <div class=" form-group">
                                <label for="Label11" runat="server" class=" control-label  lblmargin-top9px ">From : </label>
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputDateBox" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class=" form-group">
                                <label for="Label12" runat="server" class=" control-label  lblmargin-top9px ">To :</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputDateBox" ToolTip="(dd.mmm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="from-group">
                                <asp:LinkButton ID="lbtnShow" runat="server" Font-Bold="True" Font-Size="14px" CssClass="btn btn-primary" OnClick="lbtnShow_Click" Style="text-align: center;">Show</asp:LinkButton>
                            </div>

                        </div>

                        <div class="col-md-7">
                            <div class="from-group">
                                <div class="row">

                                    <asp:Label ID="lblGroup" runat="server" class=" control-label  lblmargin-top9px ">Group : &nbsp &nbsp</asp:Label>
                                    <asp:RadioButtonList ID="rbtnGroup" runat="server"
                                        BorderColor="#FFCC00" BorderStyle="none" Font-Bold="True" Font-Size="14px" CssClass="control-label lblmargin-top9px "
                                        RepeatColumns="4" RepeatDirection="Horizontal" Style="text-align: center">
                                        <asp:ListItem Value="Deposit">&nbsp Deposit &nbsp</asp:ListItem>
                                        <asp:ListItem Value="Withdraw" Selected="True">&nbsp Withdraw &nbsp</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 100px;">

                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="Viewcashbook" runat="server">
                            <asp:Label ID="lblReceiptCash" runat="server" CssClass="control-label" Font-Bold="True" Font-Size="16px" Text="Deposit" Visible="False"></asp:Label>
                            <div class="table table-responsive">


                                <asp:GridView ID="gvcashbook" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="931px"
                                    OnRowDataBound="gvcashbook_RowDataBound">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvchequeDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
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
                                                <table style="width: 23%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Cash/Bank Name"
                                                                Width="108px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
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
                            </div>
                            <asp:Label ID="lblDetailsCash" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="Black"
                                Text="Details of Cash &amp; Bank Balance" Width="669px" Height="16px"
                                Visible="False"></asp:Label>
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
                        </asp:View>


                    </asp:MultiView>

                    <%--  <table style="width: 100%;">
                        <tr>
                            <td colspan="8">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:Label ID="lblDetailsCash" runat="server" Font-Bold="True" Font-Size="16px" ForeColor="Yellow"
                                                Text="Details of Cash &amp; Bank Balance" Width="669px" Height="16px"
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="12"></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>--%>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
