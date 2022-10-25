<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccPayment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <style type="text/css">
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
   
    
     <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=this.txtScrchConCode.ClientID %>').focus();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

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


            $('.chzn-select').chosen({
                search_contains: true,
                enable_escape_special_char: false
            });

            var gridview = $('#<%=this.dgv1.ClientID %>');
            $.keynavigation(gridview);



        }


         function PrintRptCheque(vouanptype) {
             window.open('AccPrint.aspx?' + vouanptype + '', '_blank');

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
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName">Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ReadOnly="True"></asp:TextBox>


                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>



                                    </div>
                                    <div class="col-md-6 pading5px">
                                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="btn btn-primary  primaryBtn"
                                             >Prev.Voucher</asp:LinkButton>
                                        <asp:TextBox ID="txtScrchPre" runat="server"   CssClass="smltxtBox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindPrv_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" CssClass="ddlistPull chzn-select"  Style="width: 193px;">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3" style="padding-right: 0;">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblName lblTxt" Text="Control Accounts"></asp:Label>
                                        <asp:TextBox ID="txtScrchConCode" runat="server"  CssClass="inputtextbox hidden"></asp:TextBox>

                                        <asp:LinkButton ID="ibtnFindConCode" runat="server" CssClass="btn btn-primary srearchBtn hidden"  OnClick="ibtnFindConCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server"   
                                            OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                      <%--  <div class="colMdbtn">--%>
                                        <div>
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" TabIndex="1" OnClick="lnkOk_Click" >Ok</asp:LinkButton>
                                             <asp:CheckBox ID="chkgcactcode" runat="server" Text="Same Bank"  AutoPostBack="True"  CssClass="btn btn-primary checkBox" />
                                        </div>

                                    </div>
                                    <div class="col-md-3 pull-right">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblissueno" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" CssClass="lblName lblTxt"></asp:Label>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-primary srearchBtn" ><span class="glyphicon glyphicon-search asitGlyp"> </span>Search &nbsp;</asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="col-md-1 pading5px asitCol1">
                                        <asp:CheckBox ID="chkPettyCash" runat="server" AutoPostBack="True" OnCheckedChanged="chkPrint_CheckedChanged" Style="margin-left: 0;" Text="Single Issue" CssClass="btn btn-primary primaryBtn chkBoxControl " />
                                    </div>
                                    <div class="col-md-7 pading5px asitCol7">
                                        <asp:Panel ID="PanelChk" runat="server" Visible="False">
                                            <table>
                                                <tr>
                                                    <td class="style25">
                                                        <asp:CheckBox ID="chkPrint" runat="server" AutoPostBack="True"
                                                            CssClass="btn btn-primary primaryBtn chkBoxControl"  OnCheckedChanged="chkPrint_CheckedChanged"
                                                            Text="Cheque Print" />
                                                        <asp:CheckBox ID="ChboxPayee" runat="server" Text="A/C Payee" CssClass="btn btn-primary checkBox" />
                                                        <asp:CheckBox ID="ChqWithoutdat" runat="server" Text="Without Cheque Date" CssClass="btn btn-primary checkBox" />


                                                    </td>


                                                    <td class="style29">
                                                        <asp:DropDownList ID="ddlChqList" runat="server"  CssClass="form-control inputTxt"
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
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblName lblTxt" Text="Head of Account"></asp:Label>

                                            <asp:TextBox ID="txtserceacc" runat="server"  CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="lnkAcccode" runat="server" OnClick="lnkAcccode_Click" CssClass="btn btn-primary srearchBtn "><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:DropDownList ID="ddlacccode" runat="server"  AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlacccode_SelectedIndexChanged"
                                                CssClass="form-control inputTxt chzn-select" >
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label2" runat="server" CssClass="lblName lblTxt" Text="Sub of Account"></asp:Label>

                                            <asp:TextBox ID="txtserchReCode" runat="server" CssClass="inputtextbox " ></asp:TextBox>
                                            <asp:LinkButton ID="lnkRescode" runat="server" OnClick="lnkRescode_Click" CssClass="btn btn-primary  srearchBtn "><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:DropDownList ID="ddlresuorcecode" runat="server" TabIndex="3"  AutoPostBack="True" OnSelectedIndexChanged="ddlresuorcecode_SelectedIndexChanged"
                                                CssClass="form-control inputTxt chzn-select">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblspecification" runat="server" CssClass="lblTxt lblName" >Specification</asp:Label>
                                            <asp:TextBox ID="txtSearchSpeci" runat="server" CssClass="inputtextbox " ></asp:TextBox>
                                            <asp:LinkButton ID="lnkSpecification" runat="server" CssClass="btn btn-primary  srearchBtn"   OnClick="lnkSpecification_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:DropDownList ID="ddlSpclinf"  runat="server" CssClass="form-control inputTxt chzn-select"
                                                  >
                                            </asp:DropDownList>

                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblDramt" runat="server" CssClass="lblName lblTxt">Amount</asp:Label>

                                            <asp:TextBox ID="txtDrAmt" runat="server" TabIndex="4"  CssClass=" inputtextbox"></asp:TextBox>

                                        </div>
                                        <div class="col-md-9 pading5px">

                                            <asp:Label ID="lblChequeNo" runat="server" CssClass="smLbl_to">Cheque No.</asp:Label>

                                            <asp:TextBox ID="txtChequeNo" runat="server" TabIndex="5" CssClass=" inputtextbox"  ></asp:TextBox>







                                            <asp:Label ID="lblChequeDate" runat="server" CssClass=" smLbl_to">Date</asp:Label>

                                            <asp:TextBox ID="txtChequeDate" runat="server" OnTextChanged="txtChequeDate_TextChanged" CssClass=" inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtChequeDate_CalendarExtender" runat="server"
                                                Enabled="True" TargetControlID="txtChequeDate" PopupButtonID="Image3"
                                                Format="dd.MM.yyyy"></cc1:CalendarExtender>

                                            <asp:Label ID="lblremarks" runat="server" CssClass="smLbl_to" >Remarks</asp:Label>

                                            <asp:TextBox ID="txtremarks" runat="server" CssClass=" inputtextbox" style="width:80px;" ></asp:TextBox>
                                            <asp:LinkButton ID="lbtnAdd" runat="server" TabIndex="5" CssClass="btn btn-primary primaryBtn" OnClick="lbtnAdd_Click" Style="margin-left:5px;">Add Table</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label3" runat="server" CssClass=" lblName lblTxt">Bill No</asp:Label>

                                            <asp:TextBox ID="txtserchBill" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="lnkBillNo" runat="server" OnClick="lnkBillNo_Click" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        </div>
                                        <div class="col-md-2 pading5px asitCol2">

                                            <asp:DropDownList ID="ddlBillList" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblPayto" runat="server" CssClass="smLbl_to">Pay To</asp:Label>

                                            <asp:TextBox ID="txtRecAndPayto" runat="server" CssClass=" inputtextbox" Width="215"></asp:TextBox>

                                            <cc1:AutoCompleteExtender ID="txtRecAndPayto_AutoCompleteExtender"
                                                runat="server" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                                ServicePath="~/AutoCompleted.asmx" TargetControlID="txtRecAndPayto">
                                            </cc1:AutoCompleteExtender>

                                        </div>

                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>
                    <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True"
                        OnRowDeleting="dgv1_RowDeleting" CssClass="table-striped table-hover table-bordered grvContentarea"
                        OnRowCancelingEdit="dgv1_RowCancelingEdit" OnRowEditing="dgv1_RowEditing"
                        OnRowUpdating="dgv1_RowUpdating">
                        <PagerSettings Position="Top" />
                        <RowStyle />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:CommandField ShowEditButton="True" />
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
                                <EditItemTemplate>

                                    <fieldset class="scheduler-border fieldset_A">

                                        <div class="form-horizontal">

                                            <div class="form-group">

                                                <div class="col-md-4 pading5px">
                                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName">Acc. Head

                                                        <asp:LinkButton ID="ibtngrdFindAccCode" runat="server"   OnClick="ibtngrdFindAccCode_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                    </asp:Label>
                                                    <asp:TextBox ID="txtgrdserceacc" runat="server" CssClass="inputtextbox hidden"></asp:TextBox> 
                                                    
                                                         

                                                </div>

                                                <div class="col-md-8 pading5px">
                                                    <asp:DropDownList ID="ddlgrdacccode" runat="server" CssClass="form-control inputTxt chzn-select"
                                                        OnSelectedIndexChanged="ddlgrdacccode_SelectedIndexChanged" Style="width: 213px;" AutoPostBack="True">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="form-group">

                                                <div class="col-md-4 pading5px">
                                                    <asp:Label ID="lblcontrolAccHead0" runat="server" CssClass="lblTxt lblName">Details Head
                                                        <asp:LinkButton ID="ibtngrdFindResource" runat="server"  OnClick="ibtngrdFindResource_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                    </asp:Label>
                                                    <asp:TextBox ID="txtgrdserresource" runat="server" CssClass="inputtextbox hidden" ></asp:TextBox>
                                                     
                                                </div>

                                                <div class="col-md-8 pading5px">
                                                    <asp:DropDownList ID="ddlrgrdesuorcecode" runat="server" CssClass="form-control inputTxt chzn-select"
                                                         Style="width: 213px;">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True"
                                        OnClick="lnkTotal_Click" Style="color: #000" Font-Underline="False">Total :</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAccdesc1" runat="server"
                                        Font-Size="11px"
                                       
                                         Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") +                                                                           
                                                                        " &nbsp;&nbsp;&nbsp;&nbsp;"+Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'





                                        Width="350"></asp:Label>

                                </ItemTemplate>

                                <FooterStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="Bank/Cash">
                                  <EditItemTemplate>
                                    <asp:DropDownList ID="ddlBanklist" runat="server" CssClass="form-control chzn-select">
                                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvbocash" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                        Width="180px" Font-Size="12px" ForeColor="Black"  ></asp:Label>
                                </ItemTemplate>
                                  <HeaderStyle Width="180px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Issue No">

                                <ItemTemplate>
                                    <asp:Label ID="lblisuno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                        Width="60px" Font-Size="12px" ForeColor="Black"  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>




                            <asp:TemplateField HeaderText="Cheque No">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvChequeno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                        Width="100px" Font-Size="12px" ForeColor="Black"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cheque Date">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvChequeDate" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedate")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px" Font-Size="12px" ForeColor="Black"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtgvChequeDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvChequeDate"></cc1:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="bill balance" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblbillbalance" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                         style="text-align:right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0;(#,##0.); ") %>'
                                        Width="80px" Font-Size="12px" ForeColor="Black" ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lblFbillbalance" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px"
                                        Width="80px" ForeColor="#000"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle ForeColor="White" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                         style="text-align:right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0;(#,##0.); ") %>'
                                        Width="80px" Font-Size="12px" ForeColor="Black" ></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterTemplate>
                                    <asp:Label ID="lblTgvDrAmt" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px"
                                        Width="80px" ForeColor="#000"></asp:Label>
                                </FooterTemplate>
                                <FooterStyle ForeColor="White" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                        Width="80px" ForeColor="Black" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Pay To">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvPayto" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                        Width="140px" ForeColor="Black" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acvounum" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvacvounm" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acvounum")) %>'
                                        Width="50px" ForeColor="Black"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill No">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvBillno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                        Width="90px" ForeColor="Black" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Instade Of Issue">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvinsissueno" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        CssClass="GridTextboxL" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insofissue")) %>'
                                        Width="80px" ForeColor="Black" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                    <div class="row">

                        <asp:Panel ID="Panel4" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-2 pading5px asitCol2">
                                            <asp:Label ID="lblNaration" runat="server" CssClass="lblName lblTxt" Text="Narration"></asp:Label>
                                        </div>
                                        <div class="col-md-7 pading5px">
                                            <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"
                                                CssClass="form-control" Rows="5" Height="40" TextMode="MultiLine"
                                                ></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                                OnClick="lnkFinalUpdate_Click"
                                                >Final Update</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


