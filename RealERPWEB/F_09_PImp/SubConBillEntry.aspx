
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SubConBillEntry.aspx.cs" Inherits="RealERPWEB.F_09_PImp.SubConBillEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .panlPayStyle .lblName {
            width: 115px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt"  style="width:336px" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblconList" runat="server" CssClass="lblTxt lblName" Text="Contractor List"></asp:Label>

                                        <asp:TextBox ID="txtSrcSub" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnFindSubConName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindSubConName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSubName" runat="server" CssClass="chzn-select form-control  inputTxt" style="width:336px" TabIndex="3"></asp:DropDownList>
                                        <asp:Label ID="lblSubDesc" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>


                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PnlPay" runat="server" Visible="False">
                            <div class="row">
                                <div class="col-md-12 panlPayStyle">
                                    <div class="form-group">

                                        <div class="col-md-6 pading5px">
                                            <asp:Label ID="Label21" runat="server" Text="Payable:" CssClass=" lblName btn btn-success primaryBtn"></asp:Label>
                                            <div class="clearfix"></div>
                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:Label ID="lblLabour" runat="server" CssClass="lblTxt lblName" Text="Bill (Ref. Execution):" Font-Size="11px"></asp:Label>
                                                <asp:TextBox ID="txtSubConAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 pading5px asitCol4">
                                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Above(%):"></asp:Label>
                                                <asp:TextBox ID="txtperAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                <asp:LinkButton ID="lbtnAmt" runat="server" OnClick="lbtnAmt_Click" CssClass="btn btn-primary primaryBtn">Amt</asp:LinkButton>
                                                <asp:TextBox ID="txtAbvAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 pading5px asitCol4">
                                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Completed in Schedule Time"></asp:Label>
                                                <asp:TextBox ID="txtCstbamt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 pading5px asitCol4">
                                                <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Completed Before Schedule Time"></asp:Label>
                                                <asp:TextBox ID="txtCstbbat" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 pading5px asitCol4">
                                                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Others"></asp:Label>
                                                <asp:TextBox ID="txtDedOtAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                <div class=" clearfix"></div>
                                            </div>
                                            <div class="col-md-3 pading5px asitCol4">
                                                <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName" Text="Total"></asp:Label>
                                                <asp:TextBox ID="lbltDedAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                            </div>



                                        </div>
                                        <div class="col-md-6 pading5px">

                                            <asp:Panel ID="PnlDed" runat="server" Visible="False">
                                                <asp:Label ID="Label2" runat="server" Text="Deduction:" CssClass="btn btn-success primaryBtn"></asp:Label>

                                                   <div class="clearfix"></div>
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Security(%)"></asp:Label>
                                                    <asp:TextBox ID="txtSperAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                                    <asp:LinkButton ID="lbtnSAmt" runat="server" OnClick="lbtnSAmt_Click" CssClass="btn btn-primary primaryBtn">Amt</asp:LinkButton>
                                                    <asp:TextBox ID="txtSecAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName" Text="Penalty"></asp:Label>
                                                    <asp:TextBox ID="txtPanalAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Material Supply"></asp:Label>
                                                    <asp:TextBox ID="txtMatamt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="Others"></asp:Label>
                                                    <asp:TextBox ID="txtPayOtAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3 pading5px asitCol4">
                                                    <asp:Label ID="Label14" runat="server" CssClass="lblTxt lblName" Text="Total"></asp:Label>
                                                    <asp:TextBox ID="lbltPayAmt" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                </div>


                                                
                                            </asp:Panel>


                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <hr class="hrline" />
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>
                                        </div>
                                        <div class="col-md-6 pading5px">
                                            <asp:LinkButton ID="lnkbtnTotalCal" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lnkbtnTotalCal_Click" Visible="False">Total Calculation</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </asp:Panel>
                    </div>
                    <div class="row">
                        <div class="panlPayStyle">
                            <asp:Panel ID="PnlNet" runat="server" Visible="False">
                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                           
                                                <div class="col-md-8 pading5px asitCol8">
                                                    <asp:Label ID="Label8" runat="server" CssClass="lblTxt lblName" Text="Payable"></asp:Label>

                                                    <asp:TextBox ID="lbltPayAfDed" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                               
                                                    <asp:Label ID="Label9" runat="server" CssClass=" smLbl_to" Text="Advanced(As per Accounts)"></asp:Label>

                                                    <asp:TextBox ID="lblAccAmt" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                              
                                                    <asp:Label ID="Label15" runat="server" CssClass="smLbl_to" Text="Net Payble"></asp:Label>

                                                    <asp:TextBox ID="lbltNetPayAmt" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                                
                                                    <asp:LinkButton ID="lnlbtnfUpdate" runat="server" OnClick="lnlbtnfUpdate_Click" CssClass="btn btn-danger primaryBtn margin5px">Final Update</asp:LinkButton>
                                                </div>
                                           
                                        </div>
                                    </div>
                                </fieldset>
                                <%--<table style="width: 100%;">
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Size="16px"
                                            ForeColor="#FFFF99" Style="text-align: right" Text="Payable:"
                                            Width="191px" CssClass="style27"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="lbltPayAfDed" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right; color: #FFFFFF;" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style23">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Size="16px"
                                            ForeColor="#FFFF99" Style="text-align: right" Text="Advanced(As per Accounts):"
                                            Width="191px" CssClass="style27"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="lblAccAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right; color: #FFFFFF;" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style23">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Size="16px"
                                            ForeColor="#FFFF99" Style="text-align: right" Text="Net Payble:"
                                            Width="191px" CssClass="style27"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        <asp:Label ID="lbltNetPayAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right; color: #FFFFFF;" Width="100px"></asp:Label>
                                    </td>
                                    <td class="style23">&nbsp;</td>
                                    <td>
                                        <asp:LinkButton ID="lnlbtnfUpdate" runat="server" Font-Bold="True"
                                            Font-Size="12px" OnClick="lnlbtnfUpdate_Click"
                                            Style="text-align: center; background-color: #CCFF66" Width="100px">Final Update</asp:LinkButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>--%>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <table style="width: 100%;">
                <%--<tr>
                                <td colspan="12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table style="width:100%;">
                                            <tr>
                                                <td class="style18">
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" 
                                                        style="text-align: right" Text="Date:" Width="122px" CssClass="style26" 
                                                        Font-Size="12px" Height="17px"></asp:Label>
                                                </td>
                                                <td class="style17">
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                                        Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="style19">
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
                                                <td class="style18">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                                                        style="text-align: right" Text="Project Name:" Width="122px" 
                                                        CssClass="style26" Font-Size="12px" Height="17px"></asp:Label>
                                                </td>
                                                <td class="style17">
                                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="txtboxformat" 
                                                        Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style19">
                                                    <asp:ImageButton ID="ibtnFindProject" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindProject_Click" />
                                                </td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlProjectName" runat="server" 
                                                        Font-Bold="True" Font-Size="12px" Width="300px" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlProjectName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblProjectdesc" runat="server" BackColor="White" 
                                                        Font-Size="12px" ForeColor="Blue" Height="16px" Visible="False" Width="300px"></asp:Label>
                                                   
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
                                            </tr>
                                            <tr>
                                                <td class="style18">
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" 
                                                        style="text-align: right" Text="Contractor Name:" Width="122px" 
                                                        CssClass="style26" Font-Size="12px" Height="17px"></asp:Label>
                                                </td>
                                                <td class="style17">
                                                    <asp:TextBox ID="txtSrcSub" runat="server" CssClass="txtboxformat" 
                                                        Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style19">
                                                    <asp:ImageButton ID="ibtnFindSubConName" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindSubConName_Click" />
                                                </td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="ddlSubName" runat="server" 
                                                        Font-Bold="True" Font-Size="12px" Width="300px">
                                                    </asp:DropDownList>
                                                    <cc1:ListSearchExtender ID="ddlSubName_ListSearchExtender" runat="server" 
                                                        QueryPattern="Contains" TargetControlID="ddlSubName">
                                                    </cc1:ListSearchExtender>
                                                    <asp:Label ID="lblSubDesc" runat="server" BackColor="White" Font-Size="12px" 
                                                        ForeColor="Blue" Height="16px" Visible="False" Width="300px"></asp:Label>
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
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>--%>

                <tr>
                    <td colspan="6"></td>
                    <td class="style22" colspan="6">
                        <%--<asp:Panel ID="PnlDed" runat="server" BorderColor="#660033" BorderStyle="Solid"
                            BorderWidth="1px" Visible="False" Width="423px">
                            <table style="width: 104%;">
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="16pt"
                                            ForeColor="#FFFF99" Style="text-align: right" Text="Deduction:"
                                            Width="182px" CssClass="style27"></asp:Label>
                                    </td>
                                    <td class="style24">&nbsp;</td>
                                    <td class="style25">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label23" runat="server" CssClass="style28" Font-Bold="True"
                                            Style="text-align: right" Text="Security(%):" Width="182px"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        <asp:TextBox ID="txtSperAmt" runat="server" CssClass="txtboxformat"
                                            MaxLength="2" Width="29px"></asp:TextBox>
                                    </td>
                                    <td class="style25">
                                        <asp:LinkButton ID="lbtnSAmt" runat="server" OnClick="lbtnSAmt_Click"
                                            CssClass="style26">Amt</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSecAmt" runat="server" CssClass="txtboxformatl"
                                            Style="text-align: right;" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label24" runat="server" CssClass="style28" Font-Bold="True"
                                            Style="text-align: right" Text="Penalty:" Width="182px"></asp:Label>
                                    </td>
                                    <td class="style24">&nbsp;</td>
                                    <td class="style25">&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtPanalAmt" runat="server" CssClass="txtboxformatl"
                                            Style="text-align: right;" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label25" runat="server" CssClass="style28" Font-Bold="True"
                                            Style="text-align: right" Text="Material Supply:" Width="182px"></asp:Label>
                                    </td>
                                    <td class="style24">&nbsp;</td>
                                    <td class="style25">&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtMatamt" runat="server" CssClass="txtboxformatl"
                                            Style="text-align: right;" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label26" runat="server" CssClass="style28" Font-Bold="True"
                                            Style="text-align: right" Text="Others:" Width="182px"></asp:Label>
                                    </td>
                                    <td class="style24">&nbsp;</td>
                                    <td class="style25">&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtDedOtAmt" runat="server" CssClass="txtboxformatl"
                                            Style="text-align: right;" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style21">&nbsp;</td>
                                    <td class="style24">&nbsp;</td>
                                    <td class="style25">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style21">
                                        <asp:Label ID="Label27" runat="server" CssClass="style28" Font-Bold="True"
                                            Style="text-align: right" Text="Total:" Width="182px"></asp:Label>
                                    </td>
                                    <td class="style24">&nbsp;</td>
                                    <td class="style25">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="lbltDedAmt" runat="server" Font-Bold="True" Font-Size="12pt"
                                            Style="text-align: right; color: #FFFFFF;" Width="100px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="6"></td>
                    <td class="style22" colspan="6"></td>
                </tr>
                <tr>
                    <td colspan="12">
                        <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server"
                            QueryPattern="Contains" TargetControlID="ddlProjectName">
                        </cc1:ListSearchExtender>

                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="style51"></td>
                    <td class="style15"></td>
                    <td class="style20"></td>
                    <td></td>
                    <td colspan="2"></td>
                    <td class="style52"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



