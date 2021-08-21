<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccInterComVoucher.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccInterComVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


   

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          

        });
        function pageLoaded() {
       
            $('.chzn-select').chosen({ search_contains: true });

        };


      








    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="Transfer From"></asp:Label>
                                        <asp:Label ID="lblFromCmpName" runat="server" CssClass="form-control inputTxt" Width="218px"></asp:Label>


                                    </div>
                                    <div class="col-md-2 pading5px asitCol2">
                                        <asp:RadioButtonList ID="rbtnList1" BorderColor="BlueViolet" runat="server" AutoPostBack="True" CssClass="rbtnList1 chkBoxControl form-control" RepeatColumns="5">
                                            <asp:ListItem>Payment</asp:ListItem>
                                            <asp:ListItem>Received</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                        
                                         <asp:Label ID="lblfVoucherNo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to">Voucher Date</asp:Label>
                                        <asp:TextBox ID="txtfdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfdate">
                                        </cc1:CalendarExtender>

                                    </div>

                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-1 pading5px asitCol1">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName">Control Accounts</asp:Label>
                                       
                                        <asp:TextBox ID="txtsercacc" runat="server" CssClass="inputTxt inputName inpPixedWidth"   Visible="false"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindCAccount" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" Visible="false" OnClick="imgbtnFindCAccount_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 pading5px asitCol1">
                                        <asp:Label ID="lblAccountHead" runat="server" CssClass="lblTxt lblName">Accounts Head</asp:Label>
                                        <asp:TextBox ID="txtserheacc" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="display:none;"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindAccount" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindAccount_Click" Style="display:none;"> <span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:Label ID="lblDramt0" runat="server" CssClass="lblTxt lblName">Dr. Amount</asp:Label>
                                        <asp:TextBox ID="txtDrAmt" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="text-align: right;"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>

                        </fieldset>
                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_Nar">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-2 pading5px asitCol2 ">
                                        <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                        <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                    </div>
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                        <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>




                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblRefNum0" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnRefresh_Click">Referesh</asp:LinkButton>

                                    </div>




                                </div>
                            </div>

                        </fieldset>

                    </div>




                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Transfer To"></asp:Label>
                                        <asp:DropDownList ID="ddlToCompany" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlToCompany_SelectedIndexChanged" Width="218px">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Voucher No</asp:Label>
                                        <asp:Label ID="lbltVoucherNo" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:Label>

                                        <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Voucher Date</asp:Label>
                                        <asp:TextBox ID="txttdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " ReadOnly="true"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfdate_CalendarExtender3" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfdate">
                                        </cc1:CalendarExtender>

                                    </div>

                                </div>
                            </div>
                        </fieldset>

                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-1 pading5px asitCol1 ">
                                        <asp:Label ID="lblcontrolAccHead0" runat="server" CssClass="lblTxt lblName">Control Accounts</asp:Label>
                                        <asp:TextBox ID="txtsetrcacc" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="display:none;"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindtCAccount" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindtCAccount_Click" Style="display:none;"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlContAccHead" runat="server" CssClass=" chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlContAccHead_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-1 pading5px asitCol1 ">
                                        <asp:Label ID="lblAccountHead0" runat="server" CssClass="lblTxt lblName">Head Of Accounts</asp:Label>
                                        <asp:TextBox ID="txtsertoheacc" runat="server" CssClass="inputTxt inputName inpPixedWidth" Style="display:none;"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindtoAccount" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindtoAccount_Click" Style="display:none;"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlAcctoHead" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:Label ID="lblDramt1" runat="server" CssClass="lblTxt lblName">Cr. Amount</asp:Label>
                                        <asp:Label ID="lbltcramt" runat="server" CssClass="inputTxt inputName inpPixedWidth"  Style="text-align: right;"></asp:Label>

                                    </div>

                                </div>
                            </div>

                        </fieldset>
                    </div>
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_Nar">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-2 pading5px asitCol2 ">
                                        <asp:Label ID="lblRefNum1" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                        <asp:Label ID="lbltRefNum" runat="server" CssClass="inputtextbox"></asp:Label>

                                    </div>
                                    <div class="col-md-4 pading5px">

                                        <asp:Label ID="lblSrInfo0" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                        <asp:TextBox ID="txttSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>




                                    </div>
                                    <div>
                                        <asp:Label ID="lblComAdd" runat="server" Visible="False"></asp:Label>
                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <div class="input-group">
                                            <span class="input-group-addon glypingraddon">
                                                <asp:Label ID="lblRefNum2" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                            </span>
                                            <asp:TextBox ID="txttNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>

                                    </div>




                                </div>
                            </div>

                        </fieldset>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>







