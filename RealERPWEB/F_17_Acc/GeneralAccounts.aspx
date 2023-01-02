<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GeneralAccounts.aspx.cs" Inherits="RealERPWEB.F_17_Acc.GeneralAccounts" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            margin: 0px 0 0 0px;
            font-size: 11px;
            font-weight: normal;
            border: solid 1px #006699;
            background-color: White;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
    </style>

    <script type="text/javascript">


        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('#<%=this.ddlvoucher.ClientID %>').focus();

            $("input, select ").not($(":submit, :button")).keypress(function (evt) {
                evt.preventDefault();
                alert(evt);
                if (evt.keyCode == 13) {
                    var next = $('[tabindex="' + (this.tabIndex + 1) + '"]');
                    if (next.length)
                        next.focus()
                    else
                        $('[tabindex="1"]').focus();
                }
            });

        });
        function pageLoaded() {

            <%--$("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

                var dgv1 = $('#<%=this.dgv1.ClientID %>');

                $.keynavigation(dgv1);

            });--%>

            $('.chzn-select').chosen({
                search_contains: true,
                enable_escape_special_char: false
            });


        };


        function searchKeyPress(e) {

            e = e || window.event;

            if (e.keyCode == 13) {

                document.getElementById("<%=lnkOk0.ClientID %>").click();

                return false;
            }
            return true;
        }

        function PrintRpt(printype) {

            window.open('../RptViewer.aspx?PrintOpt=' + printype + '', '_blank');
        }


        function PrintRptCheque(vouanptype) {
            window.open('AccPrint.aspx?' + vouanptype + '', '_blank');

        }


        function PrintRptVoucher(vouanptype) {
            window.open('AccPrint.aspx?' + vouanptype + '', '_blank');

        }

        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode != 43 && charCode != 45 && charCode != 42 && charCode != 47 && charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
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
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-4 pading5px  asitCol4">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName"> Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>
                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblPrivVou" runat="server" CssClass=" smLbl_to">Previous</asp:Label>
                                        <asp:TextBox ID="txtScrchPre" runat="server" CssClass=" inputBox50px"></asp:TextBox>
                                        <asp:DropDownList ID="ddlvoucher" runat="server" TabIndex="1" CssClass=" ddlPage" Style="width: 110px;" AutoPostBack="True" OnSelectedIndexChanged="ddlvoucher_SelectedIndexChanged">
                                            <asp:ListItem Value="BD">Bank Payment</asp:ListItem>
                                            <asp:ListItem Value="CD">Cash Payment</asp:ListItem>
                                            <asp:ListItem Value="BC">Bank Deposit</asp:ListItem>
                                            <asp:ListItem Value="CC">Cash Deposit</asp:ListItem>
                                            <asp:ListItem Value="CT">Contra Voucher</asp:ListItem>
                                            <asp:ListItem Value="JV">Journal Voucher</asp:ListItem>
                                            <asp:ListItem Value="PV" Enabled="false">Post Dated Voucher</asp:ListItem>
                                        </asp:DropDownList>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindPrv_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" CssClass=" ddlistPull chzn-select" Style="width: 197px;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Control Code"></asp:Label>
                                        <asp:TextBox ID="txtScrchConCode" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindConCode" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindConCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-6 pading5px ">
                                        <asp:DropDownList ID="ddlConAccHead" TabIndex="2" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lnkOk" TabIndex="3" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                        </div>
                                        <asp:Label ID="lblbalamt" runat="server" ForeColor="Red" CssClass=" smLbl_to margin5px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <asp:Panel ID="pnlondated" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-5 pading5px">
                                <div class="row">
                                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                                        <fieldset class="scheduler-border fieldset_B">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-md-3">

                                                        <asp:Label ID="lblactcode" runat="server" CssClass="lblTxt lblName">Head of A/C</asp:Label>
                                                        <asp:TextBox ID="txtserceacc" runat="server" CssClass="hidden inputtextbox"></asp:TextBox>


                                                        <div class="colMdbtn ">
                                                            <asp:LinkButton ID="lnkAcccode" runat="server" CssClass=" btn btn-primary srearchBtn" OnClick="lnkAcccode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </div>

                                                    </div>
                                                    <div class="col-md-7 pading5px">
                                                        <asp:DropDownList ID="ddlacccode" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlacccode_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="col-md-1 pading5px">
                                                        <asp:LinkButton ID="lnkOk0" runat="server" CssClass="btn btn-primary  okBtn" OnClick="lnkOk0_Click" TabIndex="7">Add</asp:LinkButton>
                                                    </div>





                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3">

                                                        <asp:Label ID="lblrescode" runat="server" CssClass="lblTxt lblName">Sub of Account</asp:Label>
                                                        <asp:TextBox ID="txtserchReCode" runat="server" CssClass=" inputtextbox hidden" ng-model="txtasrchrescode"></asp:TextBox>
                                                        <div class="colMdbtn">
                                                            <asp:LinkButton ID="lnkRescode" runat="server" CssClass="btn btn-primary srearchBtn " OnClick="lnkRescode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </div>

                                                    </div>

                                                    <div class="col-md-7 pading5px ">


                                                        <asp:DropDownList ID="ddlresuorcecode" runat="server" AutoPostBack="True" TabIndex="5" OnSelectedIndexChanged="ddlresuorcecode_SelectedIndexChanged" CssClass=" form-control inputTxt chzn-select">
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-2 pading5px ">
                                                        <asp:Label ID="lblBalance" runat="server" ForeColor="Red" Style="padding: 5px 0;" CssClass=" smLbl_to"></asp:Label>
                                                    </div>







                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3 ">
                                                        <asp:Label ID="lblspecification" runat="server" CssClass="lblTxt lblName" Visible="false">Specification</asp:Label>
                                                        <asp:TextBox ID="txtSearchSpeci" runat="server" CssClass=" hidden" Visible="false"></asp:TextBox>


                                                        <div class="colMdbtn">
                                                            <asp:LinkButton ID="lnkSpecification" runat="server" CssClass=" btn btn-primary srearchBtn" OnClick="lnkSpecification_Click" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </div>

                                                    </div>


                                                    <div class="col-md-6 pading5px">
                                                        <asp:DropDownList ID="ddlSpclinf" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSpclinf_SelectedIndexChanged"
                                                            Visible="False" TabIndex="19">
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="col-md-2 pading5px" style="display: none">
                                                        <asp:Label ID="lblrate" runat="server" CssClass=" lblVal" Visible="false">Rate</asp:Label>
                                                        <asp:TextBox ID="txtrate" runat="server" CssClass="inputtextbox" TabIndex="29" Style="text-align: right;" Visible="false"></asp:TextBox>

                                                    </div>

                                                    <div class="col-md-3 pading5px asitCol3" style="display: none">
                                                        <asp:Label ID="lblqty" runat="server" CssClass="lblTxt lblName" Visible="false">Quantity</asp:Label>
                                                        <asp:TextBox ID="txtqty" runat="server" CssClass="inputtextbox" TabIndex="30" Visible="false" Style="text-align: right;"></asp:TextBox>

                                                    </div>

                                                </div>

                                                <div class="form-group">

                                                    <div class="col-md-3">
                                                        <asp:Label ID="lblbillno" runat="server" CssClass="lblTxt lblName" Visible="false">Bill no</asp:Label>
                                                        <asp:TextBox ID="txtserchBill" runat="server" CssClass="inputtextbox  hidden" TabIndex="26" Visible="false"></asp:TextBox>


                                                        <div class="colMdbtn">
                                                            <asp:LinkButton ID="lnkBillNo" runat="server" CssClass="btn btn-primary srearchBtn hidden" OnClick="lnkBillNo_Click" TabIndex="27" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </div>

                                                    </div>

                                                    <div class="col-md-6 pading5px">
                                                        <asp:DropDownList ID="ddlBillList" runat="server" CssClass="form-control inputTxt"
                                                            Visible="False" TabIndex="19">
                                                        </asp:DropDownList>

                                                    </div>


                                                    <div class="col-md-2  pading5px" style="display: none">
                                                        <asp:Label ID="lblremarks" runat="server" CssClass="lblVal">Remarsk</asp:Label>
                                                        <asp:TextBox ID="txtremarks" runat="server" CssClass="inputtextbox" TabIndex="24"></asp:TextBox>




                                                    </div>

                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3 ">
                                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Cost Center</asp:Label>
                                                        <asp:TextBox ID="txtCostcenter" runat="server" CssClass=" hidden" TabIndex="17" Visible="false"></asp:TextBox>


                                                        <div class="colMdbtn hidden">
                                                            <asp:LinkButton ID="lnkCostCenter" runat="server" CssClass="hidden btn btn-primary srearchBtn" OnClick="lnkCostCenter_Click" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </div>

                                                    </div>
                                                    <div class="col-md-6 pading5px">
                                                        <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control inputTxt chzn-select"
                                                            TabIndex="19">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>

                                                <div class="form-group">

                                                    <div class="col-md-3 ">

                                                        <asp:Label ID="lblDramt" runat="server" CssClass="lblTxt lblName">Dr. Amount</asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtDrAmt" runat="server" CssClass="form-control inputTxt" TabIndex="6" Style="text-align: right; margin-left: -14px;" onkeypress="return searchKeyPress(event);"></asp:TextBox>


                                                    </div>

                                                    <div class="col-md-2 ">
                                                        <asp:Label ID="lblCramt" runat="server" Style="margin-left: -20px;" CssClass="lblTxt">Cr. Amount</asp:Label>
                                                    </div>
                                                    <div class="col-md-3 ">
                                                        <asp:TextBox ID="txtCrAmt" runat="server" CssClass="form-control" TabIndex="22" Style="text-align: right; margin-left: -20px;" onkeypress="return searchKeyPress(event);"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:CheckBox ID="chkCopy" runat="server" TabIndex="10" Text="Copy" CssClass="btn checkBox" AutoPostBack="True" Style="margin-left: -20px;" OnCheckedChanged="chkCopy_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                </div>


                                                <asp:Panel ID="PnlCopy" runat="server" Visible="False">

                                                    <fieldset class="scheduler-border fieldset_Nar">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">

                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">From Voucher</asp:Label>
                                                                    <asp:TextBox ID="txtScrchcopyvoucher" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                                                    <asp:LinkButton ID="ibtnCopyVoucher" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnCopyVoucher_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                                </div>

                                                                <div class="col-md-7 pading5px">
                                                                    <asp:DropDownList ID="ddlcopyvoucher" runat="server" CssClass=" ddlPage" Style="width: 200px;">
                                                                    </asp:DropDownList>
                                                                    <asp:LinkButton ID="lbtnCopyVoucher" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnCopyVoucher_Click">Copy</asp:LinkButton>

                                                                </div>



                                                            </div>
                                                        </div>
                                                    </fieldset>

                                                </asp:Panel>



                                            </div>


                                        </fieldset>

                                    </asp:Panel>
                                    <div class="spacing"></div>


                                    <asp:Panel ID="pnlNarration" runat="server" Visible="False">
                                        <fieldset class="scheduler-border fieldset_Nar">
                                            <div class="form-horizontal">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-5">
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblchequedate" runat="server" CssClass=" lblTxt lblName" Text=" Cheque Date"></asp:Label>
                                                                        <asp:TextBox ID="txtchequedate" runat="server" CssClass="inputtextbox" Width="100px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtchequedate_CalendarExtender" runat="server"
                                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtchequedate"></cc1:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-7">
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblRecived" runat="server" CssClass=" lblTxt lblName" Text="Received Bank"></asp:Label>
                                                                        <asp:TextBox ID="txtBankNam" runat="server" CssClass="inputtextbox" Width="130px"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-5">
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Cheque No"></asp:Label>
                                                                        <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox" Width="100px"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-7">
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblSrInfo" runat="server" CssClass=" lblTxt lblName" Text=" Referecne No"></asp:Label>
                                                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox" Width="130px"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-5">
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblchelist" runat="server" CssClass="lblTxt lblName" Text="Cheque List"></asp:Label>
                                                                        <asp:DropDownList ID="ddlcheque" runat="server" CssClass="chzn-select" OnSelectedIndexChanged="ddlcheque_SelectedIndexChanged" AutoPostBack="true" Style="width: 100px;">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>



                                                                <div class="col-md-7">
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblPayto" runat="server" CssClass="lblTxt lblName" Text="Pay To"></asp:Label>
                                                                        <asp:TextBox ID="txtPayto" runat="server" CssClass="inputtextbox" Width="130"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="txtPayto_AutoCompleteExtender"
                                                                            runat="server" CompletionListCssClass="AutoExtender"
                                                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                                            CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                                            DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                                            MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                                                            ServicePath="~/AutoCompleted.asmx" TargetControlID="txtPayto">
                                                                        </cc1:AutoCompleteExtender>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="form-group">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-5">
                                                                    <div class="form-group row">
                                                                        <asp:Label ID="lblpayeetype" runat="server" CssClass="lblTxt lblName" Text="Payee Type"></asp:Label>
                                                                        <asp:DropDownList ID="ddlpayeelist" runat="server" CssClass="chzn-select"  OnSelectedIndexChanged="ddlpayeelist_SelectedIndexChanged"   AutoPostBack="true" Style="width: 100px;">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>



                                                                
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-md-12 pading5px">
                                                            <div class="input-group">
                                                                <span class="input-group-addon glypingraddon">
                                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                                </span>
                                                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" Width="345" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </fieldset>

                                        <div class="row">

                                            <div class=" col-md-offset-1 col-md-11   pading5px">

                                                <asp:CheckBox ID="checkpb" runat="server" Visible="false" CssClass="btn btn-primary checkBox" Text="Pubali Bank Cheque Print" />
                                                <asp:CheckBox ID="withoutchqdate" runat="server" Text="Without Chq Date" Visible="false" CssClass="btn btn-primary checkBox" AutoPostBack="true" />
                                                <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>
                                                <asp:CheckBox ID="chkpost" runat="server" TabIndex="10" Text="post" CssClass="btn btn-primary checkBox" Visible="false" />
                                            </div>
                                        </div>




                                    </asp:Panel>

                                </div>
                            </div>

                            <div class="col-md-7 ">

                                <div class="table-responsive" style="min-height: 360px">

                                    <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea "
                                        ShowFooter="True" Width="650px" OnRowDataBound="dgv1_RowDataBound" OnRowDeleting="dgv1_RowDeleting" OnRowEditing="dgv1_RowEditing" OnRowUpdating="dgv1_RowUpdating" OnRowCancelingEdit="dgv1_RowCancelingEdit">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid" runat="server"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                            </asp:TemplateField>

                                            <asp:CommandField ShowDeleteButton="True" ButtonType="Link" ControlStyle-Width="30px">
                                                <ControlStyle Width="30px" />
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle Width="30px" />
                                            </asp:CommandField>

                                            <asp:CommandField ShowEditButton="True" ControlStyle-Width="25px">
                                                <ControlStyle Width="25px" />
                                                <HeaderStyle Width="25px" />
                                                <ItemStyle Width="25px" />
                                            </asp:CommandField>



                                            <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpclCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Head of Accounts" Width="200px"></asp:Label>

                                                    <asp:HyperLink ID="hlbtntbGrdExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                                    </asp:HyperLink>
                                                </HeaderTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True"
                                                        OnClick="lnkTotal_Click" CssClass="btn btn-primary primarygrdBtn pull-right">Total :</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>



                                                    <asp:HyperLink ID="hlnkAccdesc1" runat="server" Target="_blank" Font-Size="10px"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") +                                                                           
                                                                        " &nbsp;&nbsp;&nbsp;&nbsp;"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                                        Width="250px" Font-Names="Verdana"></asp:HyperLink>

                                                    <asp:Label ID="lblAccdesc" runat="server"
                                                        Font-Size="11px" Visible="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="50px" Font-Names="Verdana"></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                    <fieldset class="scheduler-border fieldset_A">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName">Accounts Head</asp:Label>
                                                                    <div class="col-md-3 pading5px">
                                                                        <asp:DropDownList ID="ddlgrdacccode" runat="server" CssClass="form-control chzn-select"
                                                                            TabIndex="28" Style="width: 130px;" AutoPostBack="True" OnSelectedIndexChanged="ddlgrdacccode_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <%-- <asp:TextBox ID="txtgrdserceacc" runat="server" CssClass="inputtextbox" TabIndex="26"></asp:TextBox>--%>
                                                                    <%--<div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtngrdFindAccCode" runat="server" CssClass="btn btn-primary srearchBtn"  TabIndex="27"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>--%>
                                            <
                                           <%-- <div class="col-md-3 pading5px">
                                            </div--%>>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="lblgvreshead" runat="server" CssClass="lblTxt lblName">Details Head</asp:Label>
                                                                    <div class="col-md-3 pading5px">
                                                                        <asp:DropDownList ID="ddlrgrdesuorcecode" runat="server" CssClass="chzn-select"
                                                                            TabIndex="28" Style="width: 130px;">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <%--<asp:TextBox ID="txtgrdserresource" runat="server" CssClass="inputtextbox" TabIndex="29"></asp:TextBox>--%>
                                                                    <%--<div class="colMdbtn">
                                                    <asp:LinkButton ID="ibtngrdFindResource" runat="server" CssClass="btn btn-primary srearchBtn"  TabIndex="30"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </EditItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" Width="325px" />
                                                <HeaderStyle HorizontalAlign="Left" Width="325px" />
                                                <ItemStyle Width="325px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Details Description" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResdesc" runat="server"
                                                        Font-Size="10px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                                        Width="300px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpcldesc" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'
                                                        Width="80px" TabIndex="78"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgvunit" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="30px" TabIndex="78"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Qty">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Visible="False" Font-Size="12px" Style="text-align: right"
                                                        ReadOnly="True"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None"
                                                        onkeypress="return isNumberKey(this, event);" 
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="79"></asp:TextBox>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Visible="False" Width="60px" Font-Size="12px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px" Font-Size="12px" ForeColor="Black"
                                                        onkeypress="return isNumberKey(this, event);"
                                                        Style="text-align: right" TabIndex="80"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr.Amount" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="0px"
                                                        onkeypress="return isNumberKey(this, event);"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="81"></asp:TextBox>

                                                    <asp:HiddenField ID="hntrndram" runat="server" />

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ForeColor="Black"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                                        Width="70px" Style="text-align: right"></asp:TextBox>
                                                </FooterTemplate>
                                                
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr.Amount" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        onkeypress="return isNumberKey(this, event);"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="82"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" ForeColor="Black"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="12px" ReadOnly="True"
                                                        Width="70px" Style="text-align: right"></asp:TextBox>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle Width="70px" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                        Width="80px" ForeColor="Black" TabIndex="83"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reconcilation" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrecndat" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                        Width="80px" TabIndex="78"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="RpCode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrpcode" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                                        Width="80px" TabIndex="60"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill/Chalan No" Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblgvBillno" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridTextboxL" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                        Width="100px" ForeColor="Black" TabIndex="99"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cost Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcostcenter" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectcode")) %>'
                                                        Width="80px" TabIndex="60"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvssbalamt" runat="server"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ssbalamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" TabIndex="60"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp: ID="HiddenField1" runat="server" Value='<%# Bind("ProductId") %>' />--%>

                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <asp:Label ID="lblInword" runat="server" CssClass="lblTxt lblName" Style="width: 640px; color: blue; text-align: left;"></asp:Label>
                            </div>



                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pnlpostdated" runat="server" Visible="false">
                    </asp:Panel>



                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->




        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        //function matchCustom(params, data) {
        //    alert("d");
        //    // If there are no search terms, return all of the data
        //    if ($.trim(params.term) === '') {
        //        return data;
        //    }

        //    // Do not display the item if there is no 'text' property
        //    if (typeof data.text === 'undefined') {
        //        return null;
        //    }

        //    // `params.term` should be the term that is used for searching
        //    // `data.text` is the text that is displayed for the data object
        //    if (data.text.indexOf(params.term) > -1) {
        //        var modifiedData = $.extend({}, data, true);
        //        modifiedData.text += ' (matched)';

        //        // You can return modified objects from here
        //        // This includes matching the `children` how you want in nested data sets
        //        return modifiedData;
        //    }

        //    // Return `null` if the term should not be displayed
        //    return null;
        //}

    </script>
</asp:Content>


