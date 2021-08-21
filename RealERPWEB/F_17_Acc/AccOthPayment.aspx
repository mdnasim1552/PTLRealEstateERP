<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccOthPayment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccOthPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style5 {
            width: 512px;
        }

        .style6 {
            width: 111px;
        }

        .style7 {
            width: 37px;
        }

        .style8 {
            width: 127px;
        }

        .style10 {
            width: 58px;
        }

        .style14 {
        }

        .style17 {
            width: 5px;
        }

        .style18 {
            width: 94px;
        }

        .style25 {
            width: 132px;
        }

        .style26 {
        }

        .style27 {
            width: 256px;
        }

        .style28 {
        }

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

        .style30 {
            width: 97px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('#<%=this.txtScrchConCode.ClientID %>').focus();



        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            var gridview = $('#<%=this.dgv1.ClientID %>');
            $.keynavigation(gridview);



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
                                    <div class="col-md-5 pading5px">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName">Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>


                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="smLbl_to" Text="Voucher Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEntryDate0_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>

                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="btn btn-primary primaryBtns"
                                            TabIndex="1">Prev.Voucher</asp:LinkButton>
                                        <asp:TextBox ID="txtScrchPre" runat="server" TabIndex="2" CssClass="smltxtBox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindPrv_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" CssClass="form-control inputTxt" TabIndex="4">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3" style="padding-right: 0;">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblName lblTxt" Text="Control Accounts"></asp:Label>
                                        <asp:TextBox ID="txtScrchConCode" runat="server" TabIndex="2" CssClass="inputtextbox"></asp:TextBox>

                                        <asp:LinkButton ID="ibtnFindConCode" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindConCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server"
                                            OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged" CssClass="form-control inputTxt"
                                            TabIndex="7">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">


                                    <%--<asp:Label ID="Label4" runat="server" CssClass="lblName lblTxt"></asp:Label>


                                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span>Search &nbsp;</asp:LinkButton>--%>



                                    <%-- <div class="col-md-1 pading5px asitCol1">
                                        <asp:CheckBox ID="chkPettyCash" runat="server" AutoPostBack="True" OnCheckedChanged="chkPrint_CheckedChanged" Style="margin-left: 0;" Text="Single Issue" CssClass="btn btn-primary primaryBtn chkBoxControl " />
                                    </div>--%>
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblissueno" runat="server" Visible="False"></asp:Label>
                                        <asp:Panel ID="PanelChk" runat="server" Visible="False">
                                            <table>
                                                <tr>
                                                    <td class="style25">
                                                        <asp:CheckBox ID="chkPrint" runat="server" AutoPostBack="True"
                                                            CssClass="btn btn-primary primaryBtn chkBoxControl" OnCheckedChanged="chkPrint_CheckedChanged"
                                                            Text="Cheque Print" />
                                                    </td>
                                                    <td class="style29">
                                                        <asp:DropDownList ID="ddlChqList" runat="server" CssClass="form-control inputTxt"
                                                            Visible="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel2" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblName lblTxt"></asp:Label>

                                            <asp:TextBox ID="txtserchAdvanced" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="lnkothadvance" runat="server" OnClick="lnkothadvance_Click" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span>Advance List</asp:LinkButton>

                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:DropDownList ID="ddlAdvancedList" runat="server" AutoPostBack="True"
                                                CssClass="form-control inputTxt" TabIndex="11">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <%--<div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label2" runat="server" CssClass="lblName lblTxt" Text="Sub of Account"></asp:Label>

                                            <asp:TextBox ID="txtserchReCode" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="lnkRescode" runat="server" OnClick="lnkRescode_Click" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:DropDownList ID="ddlresuorcecode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlresuorcecode_SelectedIndexChanged"
                                                CssClass="form-control inputTxt" TabIndex="11">
                                            </asp:DropDownList>

                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <%-- <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblDramt" runat="server" CssClass="lblName lblTxt">Amount</asp:Label>

                                            <asp:TextBox ID="txtDrAmt" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        </div>--%>
                                        <div class="col-md-9 pading5px">

                                            <asp:Label ID="lblChequeNo" runat="server" CssClass="lblName lblTxt">Cheque No.</asp:Label>

                                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                            <asp:Label ID="lblChequeDate1" runat="server" CssClass=" smLbl_to">Date</asp:Label>

                                            <asp:TextBox ID="txtChequeDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtChequeDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtChequeDate"></cc1:CalendarExtender>
                                            <%-- <asp:Label ID="lblremarks" runat="server" CssClass="smLbl_to" Width="74">Remarks</asp:Label>

                                            <asp:TextBox ID="txtremarks" runat="server" CssClass=" inputtextbox"></asp:TextBox>--%>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <%-- <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label3" runat="server" CssClass=" lblName lblTxt">Bill No</asp:Label>

                                            <asp:TextBox ID="txtserchBill" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="lnkBillNo" runat="server" OnClick="lnkBillNo_Click" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>
                                        <div class="col-md-2 pading5px asitCol2">

                                            <asp:DropDownList ID="ddlBillList" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>--%>

                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblPayto" runat="server" CssClass="lblName lblTxt">Pay To</asp:Label>

                                            <asp:TextBox ID="txtRecAndPayto" runat="server" CssClass=" inputtextbox" Width="230px"></asp:TextBox>

                                            <cc1:AutoCompleteExtender ID="txtRecAndPayto_AutoCompleteExtender"
                                                runat="server" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                                ServicePath="~/AutoCompleted.asmx" TargetControlID="txtRecAndPayto">
                                            </cc1:AutoCompleteExtender>

                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAdd_Click" TabIndex="27">Add Table</asp:LinkButton>

                                        </div>

                                    </div>




                                </div>
                            </fieldset>



                        </asp:Panel>
                    </div>

                    <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="337px">
                        <PagerSettings Position="Top" />
                        <RowStyle />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Head of Accounts">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True"
                                        OnClick="lnkTotal_Click" CssClass="btn btn-danger primaryBtn" >Total :</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAccdesc1" runat="server"
                                        Font-Size="11px"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "          " + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "") %>'
                                        Width="250px" TabIndex="75"></asp:Label>

                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Issue No">

                                <ItemTemplate>
                                    <asp:Label ID="lblisuno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                        Width="60px" Font-Size="11px" ForeColor="Black" TabIndex="76"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Cheque No">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvChequeno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                        Width="100px" Font-Size="11px" ForeColor="Black" TabIndex="76"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cheque Date">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvChequeDate" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedate")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px" Font-Size="12px" ForeColor="Black" TabIndex="77"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtgvChequeDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvChequeDate"></cc1:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvDrAmt" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextbox"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0.); ") %>'
                                        Width="80px" ForeColor="Black" TabIndex="78"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTgvDrAmt" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px"
                                        Width="80px"  ForeColor="#000"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle  ForeColor="#000" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lgvRemarks" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                        Width="80px" ForeColor="Black" TabIndex="79"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Pay To">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvPayto" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                        Width="140px" ForeColor="Black" TabIndex="80"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acvounum" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvacvounm" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acvounum")) %>'
                                        Width="50px" ForeColor="Black" TabIndex="80"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advanced No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvadvno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                        Width="100px" ForeColor="Black" TabIndex="99"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <asp:Panel ID="Panel4" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-2 pading5px asitCol2">
                                            <asp:Label ID="lblNaration" runat="server" CssClass="lblName lblTxt" Text="Narration"></asp:Label>
                                        </div>
                                        <div class="col-md-7 pading5px">
                                            <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"
                                                CssClass="form-control" Rows="5" Height="150" TextMode="MultiLine"
                                                TabIndex="21"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                                OnClick="lnkFinalUpdate_Click"
                                                TabIndex="22">Final Update</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                </div>
            </div>






            <%--                                        <table style="border-color: #99CCFF; width:91%; height: 12px; border-top-style: groove;">
                               
                                <tr>
                                    <td align="left" valign="top" style="text-align: center">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="label2" 
                                            Text="Current Voucher No." Width="120px"></asp:Label>
                                    </td>
                                    <td align="left" style="text-align: center" valign="top">
                                        <asp:TextBox ID="txtcurrentvou" runat="server" BorderStyle="None" 
                                            ReadOnly="True" TabIndex="22" Width="40px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="text-align: center" valign="top">
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" BorderStyle="None" 
                                            Enabled="False" TabIndex="23" ToolTip="You Can Change Voucher Number." 
                                            Width="45px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" class="style206">
                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="label2" Font-Bold="True" 
                                            Font-Size="12px"  ForeColor="#000" Text="Voucher Date" Width="75px"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtEntryDate" runat="server" BorderStyle="None" TabIndex="99" 
                                            Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEntryDate0_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td align="right" valign="top">
                                        &nbsp;</td>
                                    <td align="left" valign="top">
                                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="button" 
                                            Font-Bold="True" Font-Size="11px" Font-Underline="False" Height="16px" 
                                            Width="90px">Prev.Voucher</asp:LinkButton>
                                        <br />
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtScrchPre" runat="server" BorderStyle="None" TabIndex="1" 
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td align="left" class="style8" valign="top">
                                        <asp:ImageButton ID="ibtnFindPrv" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindPrv_Click" TabIndex="2" />
                                    </td>
                                    <td class="style8" align="left" valign="top">
                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" BackColor="Aqua" 
                                            Height="19px" Width="260px" TabIndex="3">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="style191" valign="top">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" 
                                            onclick="lnkOk_Click"   Width="90px" Font-Size="12px" TabIndex="7"  Text="Ok"></asp:LinkButton>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="left" class="style196" valign="top">
                                        &nbsp;</td>
                                    <td align="left" valign="top">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style205">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style206">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="label2" 
                                            Font-Bold="True" Font-Size="12px"  ForeColor="#000" Text="Control Accounts" 
                                            Width="95px"></asp:Label>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:TextBox ID="txtScrchConCode" runat="server" BorderStyle="None" 
                                            TabIndex="4" Width="80px"></asp:TextBox>
                                    </td>
                                    <td align="right" valign="top">
                                        <asp:ImageButton ID="ibtnFindConCode" runat="server" Height="18px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindConCode_Click" 
                                            TabIndex="5" />
                                    </td>
                                    <td align="left" valign="top" colspan="4">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" 
                                            onselectedindexchanged="ddlConAccHead_SelectedIndexChanged" Width="467px" 
                                            TabIndex="6">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style191" align="left" valign="top">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style196" valign="top">
                                       <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" 
                                            Font-Size="12px"  ForeColor="#000"></asp:Label></td>
                                    <td align="left" style="width: 0" valign="top" Width="280px">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style205">
                                        &nbsp;</td>
                                    <td align="left" valign="top" class="style206">
                                      
                                        <asp:Label ID="lblissueno" runat="server" Visible="False"></asp:Label>
                                      
                                    </td>
                                    <td class="style195" align="right" valign="top" colspan="6">
                                        
                                         <asp:Panel ID="PanelChk" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                            BorderWidth="1px" Visible="False">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td class="style25">
                                                        <asp:CheckBox ID="chkPrint" runat="server" AutoPostBack="True" 
                                                            BackColor="#000066" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                            Font-Bold="True"  Font-Size="12px" 
                                                             ForeColor="#000" oncheckedchanged="chkPrint_CheckedChanged" 
                                                            style="text-align: left; margin-left: 0px;" Text="Cheque Print" 
                                                            Width="120px" TabIndex="55" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlChqList" runat="server" Height="19px" TabIndex="56" 
                                                            Visible="False" Width="265px" style="margin-left: 2px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td class="style191">
                                        &nbsp;</td>
                                    <td>
                                        </td>
                                </tr>
                            </table>
            --%>

            <%--<asp:Panel ID="Panel2" runat="server"
                BorderColor="#CC0099" BorderStyle="Ridge"
                BorderWidth="2px" Visible="False" Width="938px">
                <table style="width: 92%; height: 13px;">
                    <tr>
                        <td align="left" class="style10" valign="top">
                            <asp:TextBox ID="txtserchAdvanced" runat="server" BorderStyle="None" TabIndex="21"
                                Width="70px"></asp:TextBox>
                        </td>
                        <td align="left" valign="top" class="style30">
                            <asp:LinkButton ID="lnkothadvance" runat="server" CssClass="button"
                                Font-Size="12px" OnClick="lnkothadvance_Click" TabIndex="22" Width="110px" Text="Advanced List"></asp:LinkButton>
                        </td>
                        <td align="left" class="style119" valign="top" colspan="16">
                            <asp:DropDownList ID="ddlAdvancedList" runat="server" Style="margin-left: 2px"
                                TabIndex="23" Width="500px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" valign="top">&nbsp;</td>
                        <td align="left" class="style129" valign="top">&nbsp;</td>
                        <td class="style200"></td>
                    </tr>
                    <tr>
                        <td class="style10">&nbsp;</td>
                        <td class="style30">
                            <asp:Label ID="lblChequeNo" runat="server" CssClass="label2"  ForeColor="#000"
                                Text="Cheque No.:" Width="70px"></asp:Label>
                        </td>
                        <td class="style28">
                            <asp:TextBox ID="txtChequeNo" runat="server" BorderStyle="None"
                                Style="margin-left: 0px" TabIndex="24" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblChequeDate1" runat="server" CssClass="label2"
                                 ForeColor="#000" Text="Date:"></asp:Label>
                        </td>
                        <td class="style27">
                            <asp:TextBox ID="txtChequeDate" runat="server" BorderStyle="None" TabIndex="25"
                                Width="80px"></asp:TextBox>
                            
                        </td>
                        <td class="style17">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="style26" colspan="4">&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="style119">&nbsp;</td>
                        <td class="style119">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="style129">&nbsp;</td>
                        <td class="style200"></td>
                    </tr>
                    <tr>
                        <td class="style10">&nbsp;</td>
                        <td class="style30">
                            <asp:Label ID="lblPayto" runat="server" CssClass="label2" Text="Pay To:"
                                Width="70px"></asp:Label>
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="txtRecAndPayto" runat="server" BorderStyle="None"
                                TabIndex="26" Width="500px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="txtRecAndPayto_AutoCompleteExtender"
                                runat="server" CompletionListCssClass="AutoExtender"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                ServicePath="~/AutoCompleted.asmx" TargetControlID="txtRecAndPayto">
                            </cc1:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="button" Font-Bold="True"
                                Font-Size="12px" OnClick="lbtnAdd_Click" TabIndex="27" Width="78px">Add Table</asp:LinkButton>
                        </td>
                        <td>&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                        <td class="style133">&nbsp;</td>
                        <td class="style133">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="style135">&nbsp;</td>
                        <td class="style200"></td>
                    </tr>
                </table>
            </asp:Panel>--%>



            <%--<asp:Panel ID="Panel4" runat="server" BorderColor="#996633" BorderStyle="Solid"
                BorderWidth="2px" Visible="False" Width="896px">
                <table style="width: 99%;">
                    <tr>
                        <td valign="top" style="text-align: right" class="style18">
                            <asp:Label ID="lblNaration" runat="server" CssClass="label2" Text="Narration"
                                Width="100px"></asp:Label>
                        </td>
                        <td class="style159" colspan="4">
                            <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"
                                CssClass="ddl" Height="42px" TextMode="MultiLine" Width="600px"
                                TabIndex="28"></asp:TextBox>
                        </td>
                        <td class="style199">&nbsp;</td>
                        <td>
                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="button"
                                Font-Bold="True" Font-Size="12px" OnClick="lnkFinalUpdate_Click"
                                Width="90px" TabIndex="29">Final Update</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="style18">&nbsp;</td>
                        <td class="style161">&nbsp;</td>
                        <td class="style198">&nbsp;</td>
                        <td class="style198">&nbsp;</td>
                        <td class="style117">&nbsp;</td>
                        <td class="style197">&nbsp;</td>
                        <td class="style199">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


