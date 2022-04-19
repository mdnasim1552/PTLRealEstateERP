<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccDTransaction.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccDTransaction" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script src="../Scripts/waypoints.min.js"></script>
    <script src="../Scripts/jquery.counterup.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });



        function pageLoaded() {

            try {

                $('.counter').counterUp({
                    delay: 10,
                    time: 1000
                });


                $("input, select")
                    .bind("keydown",
                        function (event) {
                            var k1 = new KeyPress();
                            k1.textBoxHandler(event);
                        });
                $('.chzn-select').chosen({ search_contains: true });

                $('#dgPdc').gridviewScroll({
                    width: 1160,
                    height: 420,
                    arrowsize: 30,
                    railsize: 16,
                    barsize: 8,
                    varrowtopimg: "../Image/arrowvt.png",
                    varrowbottomimg: "../Image/arrowvb.png",
                    harrowleftimg: "../Image/arrowhl.png",
                    harrowrightimg: "../Image/arrowhr.png",
                    freezesize: 4


                });

                $('#<%=this.gvcashbook.ClientID%>').tblScrollable();
                $('#<%=this.gvcashbookp.ClientID%>').tblScrollable();




            }


            catch (e) {
                alert(e);
            }

        };



    </script>



    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_A" runat="server" id="mainfiledset">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12 pading5px ">
                                <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" CssClass="btn btn-primary primaryBtn">
                                    <asp:ListItem Selected="True">Cash/Bank Book</asp:ListItem>
                                    <asp:ListItem>Daily Transaction</asp:ListItem>
                                    <asp:ListItem>Daily Proposal</asp:ListItem>
                                    <asp:ListItem>Deleted Transaction</asp:ListItem>
                                    <asp:ListItem>Receipts &amp; Payment</asp:ListItem>
                                    <asp:ListItem Enabled="False">Fund Flow</asp:ListItem>
                                    <asp:ListItem>Issued Vs. Collection</asp:ListItem>
                                    <asp:ListItem>Project Transaction</asp:ListItem>
                                    <asp:ListItem>Day Book</asp:ListItem>
                                    <asp:ListItem>Receipt Payment</asp:ListItem>
                                    <asp:ListItem>Receipt Payment2</asp:ListItem>
                                    <asp:ListItem>Receipt Payment3</asp:ListItem>
                                    <asp:ListItem>Receipt Payment(Customized)</asp:ListItem>
                                </asp:RadioButtonList>

                            </div>
                        </div>

                        <div class="form-group" runat="server" id="datefield">
                            <div class="col-md-4 pading5px asitCol4">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate" Enabled="true"></cc1:CalendarExtender>


                                <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                            </div>

                            <div class="col-md-1">

                                <div class="colMdbtn">
                                    <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShow_Click">Show</asp:LinkButton>

                                </div>

                            </div>
                            <div class="col-md-3 pading5px pull-right">

                                <div class="msgHandSt">


                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                        <ProgressTemplate>
                                            <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar"
                                                Text="Please Wait.........."></asp:Label>

                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                </div>


                            </div>

                        </div>
                    </div>
                </fieldset>

            </div>

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="VCashBook" runat="server">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblVoucherCash" runat="server" CssClass="lblTxt lblName">Voucher Type</asp:Label>

                                            <asp:DropDownList ID="ddlVoucharCash" runat="server" CssClass=" ddlistPull inputTxt">
                                                <asp:ListItem Value="C">Cash Voucher</asp:ListItem>
                                                <asp:ListItem Value="B">Bank Voucher</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="ALL Voucher">ALL Voucher</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2 pading5px asitCol2">
                                            <asp:TextBox ID="lblGroup" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 pading5px">

                                            <asp:RadioButtonList ID="rbtnGroup" runat="server"
                                                RepeatColumns="6" RepeatDirection="Horizontal"
                                                Width="200px">

                                                <asp:ListItem>Receipt</asp:ListItem>
                                                <asp:ListItem>Payment</asp:ListItem>
                                                <asp:ListItem Selected="True">Both</asp:ListItem>

                                            </asp:RadioButtonList>


                                        </div>

                                        <div class="col-md-3 pading5px">

                                            <asp:CheckBox ID="chkwitransfer" runat="server" Text="Without Contra" CssClass="btn btn-primary checkBox" />


                                        </div>
                                    </div>
                                    <div class="form-group">

                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblAmount0" runat="server" CssClass="lblTxt lblName">Amount</asp:Label>

                                            <asp:DropDownList ID="ddlSrchCash" runat="server" CssClass=" ddlistPull inputTxt"
                                                OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                                <asp:ListItem Value="between">Between</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:TextBox ID="txtAmountC1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                            <asp:Label ID="lblToCash" runat="server" Visible="False" CssClass=" smLbl_to">To</asp:Label>


                                            <asp:Label ID="txtAmountC2" runat="server" Visible="False" CssClass="lblTxt lblName"></asp:Label>
                                            <asp:LinkButton ID="imgbtnSearchVoucherCash" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" Visible="false" OnClick="imgbtnSearchVoucherCash_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>






                                    </div>


                                </div>
                            </asp:Panel>
                        </fieldset>
                        <fieldset class="scheduler-border fieldset_A">
                            <asp:Label ID="lblReceiptCash" runat="server" Font-Bold="True"
                                Text="Receipts" Width="162px" Visible="False"></asp:Label>
                        </fieldset>

                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvcashbook" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="931px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash/Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="125px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="125px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <table style="width: 7%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvCashAmttext" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px" Text="Total"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvnetTotaltext" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px" Text="Grand Total"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="125px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCashAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 7%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvCashAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvnetTotal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvBankAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 7%;">
                                            <tr>
                                                <td>

                                                    <asp:Label ID="lgvFBankAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>

                                                    <asp:Label ID="lgvFBankAmtgap" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>


                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pay To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Narration">
                                    <HeaderTemplate>
                                        <table style="width: 45%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label13" runat="server" Font-Bold="True"
                                                        Text="Narration" Width="110px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCBdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="80px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                            Width="190px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:Label ID="lblPaymentCash" runat="server"
                                Text="Payment" Width="669px" Visible="False"></asp:Label>
                        </div>
                    </div>

                    <div class="row table-responsive">
                        <asp:GridView ID="gvcashbookp" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="931px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDatepay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnumpay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash/Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="125px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="125px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvCashAmt1text" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px" Text="Total"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvnetCashaBankAmttext" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px" Text="Grand Total"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="125px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCashAmtpay" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvCashAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvnetCashaBankAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvBankAmt0" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>

                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFBankAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lgvFBankAmt1gap" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>


                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpayto" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="110px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Narration">
                                    <HeaderTemplate>
                                        <table style="width: 45%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="True"
                                                        Text="Narration" Width="110px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCBPdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="80px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNarrationp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                            Width="190px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                    <div class="row" style="margin-top: 20px;">
                        <div class="col-xs-12">
                            <asp:Label ID="lblDetailsCash" runat="server"
                                Text="Details of Cash &amp; Bank Balance" Width="669px" CssClass="btn btn-success primaryBtn"
                                Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvcashbookDB" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="973px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="500px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvOpening" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvClAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFClAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>



                </asp:View>
                <asp:View ID="VDailytransaction" runat="server">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <h4>
                                        <asp:Label ID="Label10" runat="server" Text="Transaction Listing"></asp:Label></h4>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblVoucher" runat="server" CssClass="lblTxt lblName">Voucher Type</asp:Label>

                                        <asp:DropDownList ID="ddlVouchar" runat="server" CssClass=" ddlistPull inputTxt">
                                            <asp:ListItem>BC</asp:ListItem>
                                            <asp:ListItem>BD</asp:ListItem>
                                            <asp:ListItem>CC</asp:ListItem>
                                            <asp:ListItem>CD</asp:ListItem>
                                            <asp:ListItem>CT</asp:ListItem>
                                            <asp:ListItem>JV</asp:ListItem>
                                            <asp:ListItem Selected="True">ALL Voucher</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblAmount" runat="server" CssClass="lblTxt lblName">Amount</asp:Label>

                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass=" ddlistPull inputTxt"
                                            OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                            <asp:ListItem Value="between">Between</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:TextBox ID="txtAmount1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:Label ID="lblTo" runat="server" Visible="False" CssClass=" smLbl_to">To</asp:Label>


                                        <asp:Label ID="txtAmount2" runat="server" Visible="False" CssClass="lblTxt lblName"></asp:Label>
                                        <asp:LinkButton ID="imgbtnSearchVoucher" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" Visible="false" OnClick="imgbtnSearchVoucher_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                </div>


                            </div>
                        </fieldset>


                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvtranlsit" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True"
                            Width="931px" OnRowDataBound="gvtranlsit_RowDataBound"
                            PageSize="15">
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle HorizontalAlign="Left" ForeColor="White" VerticalAlign="Top" />

                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnum1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAcRsCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Description" Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="#fff" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAcRsDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Res. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inneram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDram" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDram" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvCram" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtgvFCram" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRefnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOthRefnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srinfo")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvParyname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpostusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbydesc")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Paneltovoucherno" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-xs-3 col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblCashVoucher" runat="server" CssClass="lblTxt lblName">Cash Voucher</asp:Label>
                                            <asp:Label ID="lbltoCashVoucher" runat="server" CssClass=" smLbl_to"></asp:Label>
                                        </div>
                                        <div class="col-xs-3 col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblBankVoucher" runat="server" CssClass="lblTxt lblName">Bank Voucher</asp:Label>
                                            <asp:Label ID="lbltoBankVoucher" runat="server" CssClass=" smLbl_to"></asp:Label>
                                        </div>
                                        <div class=" col-xs-3 col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblContraVoucher" runat="server" CssClass="lblTxt lblName">Contra Voucher</asp:Label>
                                            <asp:Label ID="lbltoContraVoucher" runat="server" CssClass="smLbl_to"></asp:Label>
                                        </div>
                                        <div class=" col-xs-3 col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblJournalVoucher" runat="server" CssClass="lblTxt lblName">Journal Voucher</asp:Label>
                                            <asp:Label ID="lbltoJournalVoucher" runat="server" CssClass="smLbl_to"></asp:Label>
                                        </div>
                                    </div>


                                </div>
                            </fieldset>


                        </asp:Panel>

                    </div>


                </asp:View>

                <asp:View ID="DailPayment" runat="server">
                    <div class="row table-responsive">
                        <asp:GridView ID="gvDailPayPro" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDailPayPro_RowDataBound"
                            ShowFooter="True" Width="931px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnum2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" FooterText="Grand Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAcRsCodePPro" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" Wrap="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAcRsDescPPro" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Res. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInAmtPPro" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inneram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDramPPro" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDramPPro" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="DeleteTransaction" runat="server">
                    <div class="row col-sm-12">
                        <asp:Label ID="Label21" runat="server" CssClass="btn btn-success primaryBtn" Text="Deleted Transaction List" Width="669px"></asp:Label>
                    </div>
                    <div class=" clearfix"></div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvdtranlsit" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvdtranlsit_RowDataBound"
                            ShowFooter="True" Width="931px" AllowPaging="false" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvdtranlsit_PageIndexChanging">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdvnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdAcRsCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdAcRsDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Res. Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdInAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inneram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdDram" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvdFDram" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvdCram" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtgvdFCram" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdRefnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delbydesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="VRecAndPayment" runat="server">

                    <div class="row">

                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblGroupRP" runat="server" CssClass=" lblTxt lblName " Text="Group"></asp:Label>

                                        <asp:RadioButtonList ID="rbtnGroupRP" runat="server" CssClass="smDropDown inputTxt" TabIndex="6" RepeatColumns="6" RepeatDirection="Horizontal">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Bank</asp:ListItem>
                                            <asp:ListItem Selected="True">Both</asp:ListItem>
                                        </asp:RadioButtonList>


                                        <asp:CheckBox ID="chknet" runat="server" TabIndex="10" Text="Net" CssClass="btn btn-primary checkBox" />
                                    </div>
                                </div>


                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblproj" runat="server" CssClass="lblTxt lblName" Text="Get Acc. Heads" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtproj" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1" Visible="false"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="btnproj" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="IbtnSearchAcc_Click" TabIndex="2" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlproj" runat="server" CssClass="form-control chzn-select" TabIndex="3" Visible="false" Width="300px">
                                        </asp:DropDownList>

                                    </div>




                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row table-responsive">

                        <asp:GridView ID="gvrecandpay" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="973px" OnRowDataBound="gvrecandpay_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo5" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpcode" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head Accounts Head">
                                    <FooterTemplate>
                                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black" Style="text-align: right;" Width="300px">Net Increase/(Decrease) in Cash</asp:Label>

                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Head Accounts Head" Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnRcvPayCdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Excel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRecDesc" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "recpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="300px" Font-Underline="False" Style="color: Black"
                                            OnClick="btnRecDesc_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:HyperLink ID="lgvFNetBalance" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Blue" Target="_blank"
                                            Style="text-align: right" Width="100px"></asp:HyperLink>

                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaycode" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPayDesc" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "paydesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="300px" Font-Underline="False"
                                            Style="color: Black" OnClick="btnPayDesc_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Text="" Width="100px"></asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayam1" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpayam1" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="100px"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>
                    <asp:Panel ID="PanelNote" runat="server" Visible="false">
                        <div class="row table-responsive">
                            <asp:Label ID="lblBankstatus" runat="server" CssClass=" lblTxt" Text="Bank Status"></asp:Label>
                            <div class="clearfix"></div>
                            <asp:GridView ID="gvbankbal" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvbankbal_RowDataBound" ShowFooter="True" Width="258px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcActDescbb" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc"))
                                                                        
                                                                         
                                                                    %>'
                                                Width="280px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclosambb" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvopnambb" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Change">
                                        <ItemTemplate>


                                            <asp:HyperLink ID="hlnkgvbalambb" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px">
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>

                        </div>
                    </asp:Panel>
                </asp:View>
                <asp:View ID="IssuedVsCollection" runat="server">
                    <div class="row table-responsive">
                        <asp:GridView ID="gvarecandpay" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="973px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpcodeac" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head Accounts Head">
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Text="Total Receipts:"
                                                        Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Text="Net Balance" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label26" runat="server" Font-Bold="True"
                                                        Text="Head Accounts Head" Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnacRcvPayCdataExel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="#fff" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRecDescac" runat="server" Font-Bold="True"
                                            Font-Underline="False" OnClick="btnRecDesc_Click" Style="color: Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                            Width="300px"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpamac" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblgvFrecpamac" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lgvFNetBalanceac" runat="server" Font-Bold="True"
                                                        Font-Size="12px" ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaycodeac" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPayDescac" runat="server" Font-Bold="True"
                                            Font-Underline="False" OnClick="btnPayDesc_Click" Style="color: Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                            Width="300px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Text="Total Payments:"
                                                        Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayamac" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="width: 12%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lgvFpayamac" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewProjectTransaction" runat="server">
                    <asp:Panel ID="Panel4" runat="server">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Get Acc. Heads"></asp:Label>
                                            <asp:TextBox ID="txtAccSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="IbtnSearchAcc" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="IbtnSearchAcc_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-5 pading5px ">
                                            <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="form-control chzn-select" TabIndex="3">
                                            </asp:DropDownList>

                                        </div>




                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </asp:Panel>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvPtotranlsit" runat="server" AutoGenerateColumns="False"
                            PageSize="15" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="931px" OnRowDataBound="gvPtotranlsit_RowDataBound">
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNop" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDatep" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnump1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvVounum1" runat="server" _
                                            Font-Size="12px" Font-Underline="False" Target="_blank"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <%-- <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnump" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Description">
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label30" runat="server" Font-Bold="True" Text="Description"
                                                        Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnbtbCdataExelp" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAcRsDescp" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))%>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Debit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDramp" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDramp" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Credit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvCramp" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFCramp" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRefnump" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOthRefnump" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srinfo")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvParynamep" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" User Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvuser" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewDayBook" runat="server">
                </asp:View>
                <asp:View runat="server" ID="vwRecptPayment">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                        <asp:DropDownList ID="ddlproject" runat="server" CssClass="form-control chzn-select" TabIndex="3" Width="220px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfrmdat" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdat" Enabled="true"></cc1:CalendarExtender>

                                        <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodat" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodat" Enabled="true"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-1" style="margin-left: -75px;">

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnshw" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShow_Click">Show</asp:LinkButton>

                                        </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass=" lblTxt lblName " Text="Group"></asp:Label>

                                        <asp:RadioButtonList ID="rbtncashbank" runat="server" CssClass="smDropDown inputTxt" TabIndex="6" RepeatColumns="6" RepeatDirection="Horizontal">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Bank</asp:ListItem>
                                            <asp:ListItem Selected="True">Both</asp:ListItem>
                                        </asp:RadioButtonList>


                                        <asp:CheckBox ID="cknet" runat="server" TabIndex="10" Text="Net" Checked="true" CssClass="btn btn-primary checkBox" />
                                    </div>
                                </div>



                            </div>
                        </fieldset>

                    </div>


                    <div class="row table-responsive">

                        <asp:GridView ID="gvRecptPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="973px" OnRowDataBound="gvRecptPayment_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo9" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpcodep" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head Accounts Head">
                                    <FooterTemplate>
                                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black" Style="text-align: right;" Width="300px">Net Increase/(Decrease) in Cash</asp:Label>

                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Head Accounts Head" Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnRcvPayCdataExelp" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Excel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRecDescp" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "recpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="300px" Font-Underline="False" Style="color: Black"
                                            OnClick="btnRecDesc_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpamp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:HyperLink ID="lgvFNetBalancep" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Blue" Target="_blank"
                                            Style="text-align: right" Width="100px"></asp:HyperLink>

                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaycodep" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPayDescp" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "paydesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="300px" Font-Underline="False"
                                            Style="color: Black" OnClick="btnPayDesc_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Text="" Width="100px"></asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayam1p" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFpayam1p" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Width="100px"></asp:Label>

                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>


                    </div>

                    <asp:Label ID="banksts" runat="server" CssClass=" lblTxt" Text="Bank Status" Visible="False"></asp:Label>
                    <div class="clearfix"></div>
                    <asp:GridView ID="gvbankbal1" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="gvbankbal1_RowDataBound" ShowFooter="True" Width="258px" CssClass="table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lgcActDescbbp" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc"))
                                                                        
                                                                         
                                                                    %>'
                                        Width="280px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Closing">
                                <ItemTemplate>
                                    <asp:Label ID="lgvclosambbp" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opening">
                                <ItemTemplate>
                                    <asp:Label ID="lgvopnambbp" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Change">
                                <ItemTemplate>


                                    <asp:HyperLink ID="hlnkgvbalambbp" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                        Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px">
                                    </asp:HyperLink>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>

                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                    </asp:GridView>
                </asp:View>

                <asp:View ID="ViewRecAndPayment02" runat="server">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblgrouprp02" runat="server" CssClass=" lblTxt lblName" Text="Group"></asp:Label>

                                        <asp:RadioButtonList ID="rbtnlistrp02" runat="server" CssClass="smDropDown inputTxt" TabIndex="6" RepeatColumns="6" RepeatDirection="Horizontal">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Bank</asp:ListItem>
                                            <asp:ListItem Selected="True">Both</asp:ListItem>
                                        </asp:RadioButtonList>


                                    </div>
                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row table-responsive">

                        <asp:GridView ID="gvrecandpay02" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="973px" OnRowDataBound="gvrecandpay02_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo5rp02" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpcoderp02" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head Accounts Head">

                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4rp02" runat="server" Font-Bold="True"
                                                        Text="Head Accounts Head" Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnRcvPayCdataExelrp02" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Excel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRecDescrp02" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "recpdesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")): "") 
                                                                         
                                                                    %>'
                                            Width="395px" Font-Underline="False" Style="color: Black"
                                            OnClick="btnRecDescrp02_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Height="35px" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpamrp02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" Height="35px" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaycoderp02" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPayDescrp02" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "paydesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                      
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")): "") 
                                                                         
                                                                    %>'
                                            Width="300px" Font-Underline="False"
                                            Style="color: Black" OnClick="btnPayDescrp02_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Text="" Width="100px"></asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayamrp02" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>

                </asp:View>


                <asp:View ID="ViewRecAndPayment03" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                        <asp:DropDownList ID="ddlproject2" runat="server" CssClass="form-control chzn-select" TabIndex="3" Width="220px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfrmdat2" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdat2" Enabled="true"></cc1:CalendarExtender>

                                        <asp:Label ID="Label9" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodat2" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodat2" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-1" style="margin-left: -75px;">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnshow2" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnshow2_Click">Show</asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="Label11" runat="server" CssClass=" lblTxt lblName " Text="Group"></asp:Label>
                                        <asp:RadioButtonList ID="rbtncashbank2" runat="server" CssClass="smDropDown inputTxt" TabIndex="6" RepeatColumns="6" RepeatDirection="Horizontal">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Bank</asp:ListItem>
                                            <asp:ListItem Selected="True">Both</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:CheckBox ID="cknet2" runat="server" TabIndex="10" Text="Net" Checked="true" CssClass="btn btn-primary checkBox" />
                                    </div>
                                </div>



                            </div>
                        </fieldset>

                    </div>
                    <div class="row table-responsive">

                        <asp:GridView ID="gvrecandpay03" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="973px" OnRowDataBound="gvrecandpay03_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrp2sl" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrp2recpcode" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head Accounts Head">
                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4gvrp2" runat="server" Font-Bold="True"
                                                        Text="Head Accounts Head" Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="btngvrp2ept2excel" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Excel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="btngvrp2recpdesc" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "recpdesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")): "") 
                                                                         
                                                                    %>'
                                            Width="395px" Font-Underline="False" Style="color: Black"
                                            OnClick="btngvrp2recpdesc_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Height="35px" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrp2recpam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" Height="35px" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrp2paycode" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btngvrp2paydesc" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "paydesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                      
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")): "") 
                                                                         
                                                                    %>'
                                            Width="300px" Font-Underline="False"
                                            Style="color: Black" OnClick="btngvrp2paydesc_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFNetBal03" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Text="" Width="100px"></asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrp2payam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                    </div>

                </asp:View>

                  <asp:View ID="ViewRecAndPayCustomized" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblGroup03" runat="server" CssClass=" lblTxt lblName" Text="Group"></asp:Label>

                                        <asp:RadioButtonList ID="rbtnListCustomized" runat="server" CssClass="smDropDown inputTxt" TabIndex="6" RepeatColumns="6" RepeatDirection="Horizontal">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Bank</asp:ListItem>
                                            <asp:ListItem Selected="True">Both</asp:ListItem>
                                        </asp:RadioButtonList>


                                    </div>
                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvRecPayCustomized" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            Width="973px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo5rp03" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RecCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpcoderp03" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpcode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head Accounts Head">

                                    <HeaderTemplate>
                                        <table style="width: 47%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4rp03" runat="server" Font-Bold="True"
                                                        Text="Head Accounts Head" Width="180px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnRcvPayCdataExelrp03" runat="server" BackColor="#000066"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center" Width="90px">Export Excel</asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRecDescrp03" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "recpdesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grprpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")): "") 
                                                                         
                                                                    %>'
                                            Width="395px" Font-Underline="False" Style="color: Black"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Height="35px" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receipt Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrecpamrp03" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recpam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" Height="35px" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PayCode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpaycoderp03" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paycode")) %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head of Accounts">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnPayDescrp03" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "paydesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grppaydesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                      
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")): "") 
                                                                         
                                                                    %>'
                                            Width="300px" Font-Underline="False"
                                            Style="color: Black"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Text="" Width="100px"></asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amt.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayamrp03" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </asp:View>


            </asp:MultiView>


        </div>
        <!-- End of contentpart-->
    </div>
    <!-- End of Container-->




    <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
</asp:Content>
