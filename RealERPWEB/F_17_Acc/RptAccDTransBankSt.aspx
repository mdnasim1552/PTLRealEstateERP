<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccDTransBankSt.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccDTransBankSt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            width: 48px;
        }

        .style62 {
            width: 38px;
        }

        .style64 {
        }

        .style65 {
            width: 79px;
        }

        .style66 {
            width: 42px;
        }

        .style67 {
            width: 55px;
        }

        .style68 {
            width: 224px;
        }

        .style71 {
            width: 6px;
        }

        .style77 {
            width: 7px;
        }

        .style78 {
            width: 15px;
        }

        .style79 {
            width: 37px;
        }

        .style80 {
            width: 41px;
        }

        .style81 {
            width: 60px;
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtfromdate" Enabled="true">
                                            </cc1:CalendarExtender>


                                            <asp:Label ID="lblDateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true">
                                            </cc1:CalendarExtender>

                                        </div>
                                        <div class="col-md-1">
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShow_Click">Show</asp:LinkButton>

                                            </div>

                                        </div>


                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>

                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel2" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
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

                                        <div class="col-md-5 pading5px">
                                            <asp:Label ID="lblGroup" runat="server" CssClass="lblTxt lblName">Group:</asp:Label>
                                            <asp:RadioButtonList ID="rbtnGroup" runat="server" CssClass="btn btn-primary checkBox"
                                                RepeatColumns="6" RepeatDirection="Horizontal"
                                                Width="235px">

                                                <asp:ListItem>Deposit</asp:ListItem>
                                                <asp:ListItem>Withdraw</asp:ListItem>
                                                <asp:ListItem Selected="True">Both</asp:ListItem>

                                            </asp:RadioButtonList>



                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblBannk" runat="server" CssClass="lblTxt lblName">Bank Name</asp:Label>

                                            <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnSearchVoucherCash" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnSearchVoucherCash_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">

                                            <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass=" ddlistPull inputTxt">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </fieldset>

                        </asp:Panel>
                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="lblReceiptCash" runat="server" Font-Bold="True" Font-Size="16px" 
                                        Text="Deposit" Width="162px" Visible="False"></asp:Label>

                                </div>
                            </div>
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
                                <asp:TemplateField HeaderText="Voucher Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgchequedate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Bank Clearing Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCDate" runat="server" Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt1")).Year==1900?""  :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt1")).ToString("dd-MMM-yyyy")) %>'
                                            Width="65px"></asp:Label>
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
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque</br>/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accounts Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Pay To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Narration">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvCashAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px" Style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvCashAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label>
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
                                        <asp:Label ID="lgvFBankAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="65px"></asp:Label>
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
                    <div class="row">
                        <div class="col-sm-12"><asp:Label ID="lblPaymentCash" runat="server" Font-Bold="True" Font-Size="16px" 
                                        Text="Withdraw" Width="669px" Visible="False"></asp:Label></div>
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
                                            <asp:TemplateField HeaderText=" Voucher Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDatepay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cheque Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgchequedate1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bank Clearing Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCDate1" runat="server" Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt1")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt1")).ToString("dd-MMM-yyyy")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvnumpay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash/Bank Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque</br>/Ref #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvActDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accounts Head">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pay To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpayto" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Narration">
                                                <HeaderTemplate>
                                                    <table style="width: 47%;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label14" runat="server" Font-Bold="True"
                                                                    Text="Narration" Width="130px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvNarrationp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCashAmtpay" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvCashAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="65px"></asp:Label>
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
                                                    <asp:Label ID="lgvFBankAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="65px"></asp:Label>
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
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblDepUnclr" runat="server" Font-Bold="True" Font-Size="16px"
                                        Text="Deposit Uncleared" Width="162px" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvDepUnclr" runat="server" AutoGenerateColumns="False" ShowFooter="True"
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
                                            <asp:TemplateField HeaderText="Voucher Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cheque Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgchequedate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Bank Clearing Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCDate" runat="server" Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt1")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt1")).ToString("dd-MMM-yyyy")) %>'
                                                        Width="65px"></asp:Label>
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
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque</br>/Ref #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accounts Head">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Pay To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Narration">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCashAmtDV" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCashAmtDV" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="65px"></asp:Label>
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
                                                    <asp:Label ID="lgvFBankAmtDV" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="65px"></asp:Label>
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
                    <div class="row">
                        <div class="col-sm-12">

                            <asp:Label ID="lblWidUnclr" runat="server" Font-Bold="True" Font-Size="16px" 
                                        Text="Withdraw Uncleared" Width="669px" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvWidUnclr" runat="server" AutoGenerateColumns="False" ShowFooter="True"
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
                                            <asp:TemplateField HeaderText=" Voucher Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDatepay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cheque Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgchequedate1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Bank Clearing Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCDate1" runat="server" Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt1")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt1")).ToString("dd-MMM-yyyy")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voucher #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvvnumpay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash/Bank Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cheque</br>/Ref #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvActDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accounts Head">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pay To">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpayto" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Narration">
                                                <HeaderTemplate>
                                                    <table style="width: 47%;">
                                                        <tr>
                                                            <td class="style58">
                                                                <asp:Label ID="Label14" runat="server" Font-Bold="True"
                                                                    Text="Narration" Width="130px"></asp:Label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvNarrationp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cash">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCashAmtpay" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCashAmtpayPV" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvBankAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFBankAmtPV" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="65px"></asp:Label>
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
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label ID="lblDetailsCash" runat="server" Font-Bold="True" Font-Size="16px" 
                                        Text="Details of Cash &amp; Bank Balance" Width="669px" Height="16px"
                                        Visible="False"></asp:Label>

                        </div>
                    </div>
                    <div class="row table-responsive">
                        <asp:GridView ID="gvcashbookDB" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
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
                                                    <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                        Style="text-align: right" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Withdraw">
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

                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                    </asp:GridView>
                    </div>
                </div>
            </div>








            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

