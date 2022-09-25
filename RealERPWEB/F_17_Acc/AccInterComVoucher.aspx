<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccInterComVoucher.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccInterComVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                <div class="card-body" style="margin-bottom: 25px">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblDate" runat="server" CssClass="smLbl_to" Text="Transfer From"></asp:Label>
                            <asp:Label ID="lblFromCmpName" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" CssClass="smLbl_to">Voucher No</asp:Label>
                            <asp:Label ID="lblfVoucherNo" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblCurDate" runat="server" Text="Voucher Date" CssClass="smLbl_to"></asp:Label>
                                <asp:TextBox ID="txtfdate" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblSrInfo" runat="server" CssClass="smLbl_to" Text="Ref No"></asp:Label>
                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="Label5" CssClass="lblTxt lblName" runat="server">Voucher Type</asp:Label>
                            <asp:RadioButtonList runat="server" ID="rbtnList1" CssClass="form-control chkBoxControl" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Payment">Payment</asp:ListItem>
                                <asp:ListItem Value="Received">Received</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Control Accounts"></asp:Label>
                            <asp:DropDownList ID="ddlConAccHead" runat="server" Font-Bold="True" AutoPostBack="true" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged" TabIndex="3">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblAccountHead" runat="server" CssClass="lblTxt lblName" Text="Accounts Head"></asp:Label>
                            <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="chzn-select form-control inputTxt" Font-Bold="True" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblRefNum" runat="server" CssClass="smLbl_to" Text="Cheque No"></asp:Label>
                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblDramt0" runat="server" CssClass="smLbl_to" Text="Dr. Amount"></asp:Label>
                            <asp:Label ID="txtDrAmt" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtDrAmt2" runat="server" CssClass="form-control" TabIndex="2" type="number" min="1"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblRefNum0" runat="server" CssClass="smLbl_to" Text="Narration:"></asp:Label>
                            <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="1" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnRefresh_Click" Style="margin-top: 23px" >Select</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="margin-bottom: 30px">

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Transfer To"></asp:Label>
                            <asp:DropDownList ID="ddlToCompany" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label11" runat="server" CssClass="smLbl_to">Voucher No</asp:Label>
                            <asp:Label ID="lbltVoucherNo" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Voucher Date</asp:Label>
                            <asp:TextBox ID="txttdate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfdate"></cc1:CalendarExtender>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblcontrolAccHead0" runat="server" CssClass="lblTxt lblName" Text="Control Accounts"></asp:Label>
                            <asp:DropDownList ID="ddlContAccHead" runat="server" Font-Bold="True" AutoPostBack="true" CssClass=" chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlContAccHead_SelectedIndexChanged" TabIndex="3">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblAccountHead0" runat="server" CssClass="lblTxt lblName" Text="Accounts Head"></asp:Label>
                            <asp:DropDownList ID="ddlAcctoHead" runat="server" CssClass="chzn-select form-control inputTxt" Font-Bold="True" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblRefNum1" runat="server" CssClass="smLbl_to" Text="Cheque No"></asp:Label>
                            <asp:Label ID="lbltRefNum" runat="server" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblDramt1" runat="server" CssClass="lblTxt lblName" Text="Cr. Amount">Cr. Amount</asp:Label>
                            <asp:Label ID="lbltcramt" runat="server" CssClass="form-control" TabIndex="2"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblRefNum2" runat="server" CssClass="smLbl_to" Text="Narration:"></asp:Label>
                            <asp:TextBox ID="txttNarration" runat="server" class="form-control" Rows="1" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lbtnUpdate_Click" Style="margin-top: 23px">Update</asp:LinkButton>

                        </div>
                    </div>

                    <div class="row">
                        <asp:Label ID="lblComAdd" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>







