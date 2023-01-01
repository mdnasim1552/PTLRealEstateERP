<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktMoneyReceipt.aspx.cs" Inherits="RealERPWEB.F_23_CR.MktMoneyReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

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
    <style>
        .inputtextbox_s {
            width: 120px;
            height: 24px;
        }

        input[type="checkbox"], input[type="radio"] {
            margin: 0 3px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
    <div class="card mt-2">
        <div class="card-header">
            <div class="row mt-4">



                <div class=" col-md-3">

                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Project Name:"></asp:Label>
                    <asp:TextBox ID="txtSrcPro" Visible="false" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                    <asp:LinkButton ID="ibtnFindProject" Visible="false" runat="server" CssClass="btn btn btn-primary" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select  form-control form-control-sm" TabIndex="3" AutoPostBack="true">
                    </asp:DropDownList>

                    <asp:Label ID="lblProjectdesc" CssClass="form-control form-control-sm" Visible="False" runat="server" Text="Project Name:"></asp:Label>

                </div>
                <div class="col-md-1 ml-2">

                    <asp:Label ID="lblSearch" CssClass="form-label" runat="server" Text="Unit Name"></asp:Label>
                    <asp:TextBox ID="txtsrchunit" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>


                </div>
                <div class="col-md-0" style="margin-top: 22px;">
                    <asp:LinkButton ID="lbtnsrchunit" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnsrchunit_Click" TabIndex="12"><span class="fa fa-search"> </span></asp:LinkButton>

                </div>
                <div class="col-md-1 ml-2" style="margin-top: 22px;">
                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>
                </div>
                <div class="col-md-2 pull-right" style="margin-top: 22px;">
                    <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="True" BackColor="#CDDBC8" CssClass="form-control form-control-sm" RepeatColumns="6" RepeatDirection="Horizontal">
                        <asp:ListItem>Money Receipt</asp:ListItem>
                        <asp:ListItem>Payment Status</asp:ListItem>

                    </asp:RadioButtonList>
                </div>





            </div>
            <div class="row mt-3">
                <div class="col-md-10">
                    <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        Width="900">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvItmCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Item">
                                <ItemTemplate>
                                    <asp:Label ID="lgcResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                        Width="300px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Size">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize" OnClick="lbtnusize_Click"
                                        Style="text-align: right; height: 14px;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Name">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvCustName" runat="server" BackColor="Transparent" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Present Address">
                                <ItemTemplate>
                                    <asp:Label ID="lgvPreAdd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preadd")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcustphn" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custphn")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Customer Id">
                                <ItemTemplate>
                                    <asp:Label ID="lgvfilecode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fcode")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Car Parking">
                                <ItemTemplate>
                                    <asp:Label ID="lgvcparking" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cparking")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Customer Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvCustid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custid")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>

                <asp:Label  ID="lmsg" runat="server" CssClass="btn btn-danger d-none"></asp:Label>


            </div>

            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">

                    <div class="row">

                        <div class="col-md-9" style="margin-top: -25px;text-align:right;">
                            <asp:Label ID="lblCode" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblPhone" runat="server" Visible="False"></asp:Label>
                            <asp:CheckBox ID="chkAllSchedul" runat="server" CssClass=" " Text="Multiple Cheque No" />
                            <asp:LinkButton ID="lbtnBack" runat="server" OnClick="lbtnBack_Click" CssClass=" btn btn-sm btn-danger pull-right">Back</asp:LinkButton>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Panel ID="Panel2" runat="server">
                                <asp:Panel ID="Panel3" runat="server"
                                    Visible="False">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="Label15" runat="server" CssClass="form-label" Text="Previous MR:"></asp:Label>
                                        </div>

                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlPreMrr" runat="server" CssClass="form-control form-control-sm" TabIndex="13">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lbtnShowPreMrr" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnShowPreMrr_Click">Show</asp:LinkButton>

                                        </div>

                                    </div>



                                </asp:Panel>



                                <div class="row mt-2">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label5" runat="server" CssClass="form-label" Text="Pay type"></asp:Label>
                                        <asp:DropDownList ID="ddlpaytype" runat="server" Font-Bold="True"
                                            AutoPostBack="True" CssClass="form-control form-control-sm"
                                            OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2" style="margin-top: 22px;">
                                        <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Receive No"></asp:Label>
                                        <asp:Label ID="lblReceiveNo" runat="server" CssClass="form-label"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName" Text="Cheque No:"></asp:Label>

                                        <asp:TextBox ID="txtchqno" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>

                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="lbldiscount" runat="server" CssClass="form-label" Text="Dis./Rebate Amt"></asp:Label>
                                        <asp:TextBox ID="txtdiscountamt" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" Style="text-align: right;"></asp:TextBox>


                                    </div>

                                </div>




                                <div class="row mt-2">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label19" runat="server" CssClass="form-label" Text="Ins. Type:"></asp:Label>
                                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" TabIndex="5" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Receive Date:"></asp:Label>

                                        <asp:TextBox ID="txtReceiveDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"
                                            AutoPostBack="True" OnTextChanged="txtReceiveDate_TextChanged"></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblbankname" runat="server" CssClass="form-label" Text="Bank Name"></asp:Label>

                                        <asp:DropDownList ID="ddlbank" runat="server" Font-Bold="True"
                                            CssClass=" chzn-select form-control form-control-sm">
                                        </asp:DropDownList>

                                        <%-- <asp:TextBox ID="txtBName" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox_s inputtextbox"></asp:TextBox>--%>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label23" runat="server" CssClass="form-label" Text="MR No(Manual)"></asp:Label>

                                        <asp:TextBox ID="txtrefid" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>




                                    </div>
                                    <div class="col-md-1" style="margin-top:22px;">
                                        <asp:LinkButton ID="lbtRefreshMrr" runat="server" CssClass="btn btn-sm btn-danger" OnClick="lbtRefreshMrr_Click">Refresh</asp:LinkButton>

                                    </div>

                                    <asp:CheckBox ID="chkPrevious" runat="server" AutoPostBack="True"
                                        CssClass="btn btn-sm chkBoxControl primaryBtn" OnCheckedChanged="chkPrevious_CheckedChanged"
                                        Text="Previous Mrr" />

                                    <asp:Label ID="lblSchCode" runat="server" Visible="False"></asp:Label>
                                    <asp:CheckBox ID="chkOrginal" runat="server" Text="Orginal " Visible="False" />

                                </div>

                                <div class="row mt-2">

                                    <div class="col-md-2">
                                        <asp:Label ID="lblRecType" runat="server" CssClass="form-label" Text="Receive Type:"></asp:Label>

                                        <asp:DropDownList ID="ddlRecType" runat="server" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-2">

                                        <asp:Label ID="Label21" runat="server" CssClass="form-label" Text="Receipt  Amount"></asp:Label>
                                        <asp:TextBox ID="txtPaidamt" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" Style="text-align: right;"></asp:TextBox>

                                    </div>
                                    <div class="col-md-2">

                                        <asp:Label ID="lblbranch" runat="server" CssClass="form-label" Text="Branch Name"></asp:Label>
                                        <asp:TextBox ID="txtBranchName" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>

                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label24" runat="server" CssClass="lblTxt lblName" Text="Remarks"></asp:Label>
                                        <asp:TextBox ID="txtremarks" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>


                                    </div>
                                    <div>
                                        <%-- <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Text="Pay type"></asp:Label>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" Font-Bold="True" Width="95px"
                                                        AutoPostBack="True" CssClass="ddlistPull"
                                                        OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                                    </asp:DropDownList>--%>

                                        <%--                                                    <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Cheque No:" ></asp:Label>

                                                    <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"
                                                        AutoPostBack="True"></asp:TextBox>--%>
                                    </div>

                                </div>


                                <div class="row mt-2">
                                    <div class="col-md-2">

                                        <asp:Label ID="Label11" runat="server" CssClass="form-label" Text="CR  Member"></asp:Label>
                                        <asp:TextBox ID="txtSrchCollfrm" runat="server" Visible="false" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnCollfrm" runat="server" Visible="false" CssClass="btn btn-sm btn-primary" OnClick="ibtnCollfrm_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                        <asp:DropDownList ID="ddlCollType" runat="server" CssClass="form-control form-control-sm chzn-single">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblpaydate" runat="server" CssClass="form-label"></asp:Label>
                                        <asp:TextBox ID="txtpaydate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtpaydate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtpaydate"></cc1:CalendarExtender>



                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Replace Chq No"></asp:Label>
                                        <asp:TextBox ID="txtRpChqNo" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>


                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Book No"></asp:Label>
                                        <asp:TextBox ID="txtbookno" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>

                                    </div>



                                    <div class="col-md-4 pull-right" style="margin-top:22px;">



                                        <asp:LinkButton ID="lblAddToTable" runat="server" OnClick="lblAddToTable_Click" Style="padding: 2px 6px; margin-left: 35px" CssClass="btn btn-sm btn-success">Add To Table</asp:LinkButton>

                                    </div>
                                </div>

                                <div class="row mt-2">
                                    <div class="col-md-12">
                                        <asp:Panel ID="panelexcel" runat="server">
                                        <div class="row mt-2">
                                            
                                                <asp:Panel ID="pnlxcel" runat="server">
                         
                                                    <div class="col-sm-3 col-md-3 col-lg-3 ml-3">
                                                    <asp:Label ID="lblExel" runat="server" CssClass="form-label" Text="Exele :"></asp:Label>
                                                    <div class="uploadFile">
                                                        <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                                    </div>
                                                    </div>
                                                </asp:Panel>
                                            
                                              <div class="col-sm-1 col-md-1 col-lg-1 mt-2" style="margin-right:-50px;">
                                                <asp:LinkButton ID="btnexcuplosd" OnClientClick="javascript: return FunConfirmSave();" runat="server" OnClick="btnexcuplosd_OnClick" CssClass=" btn btn-danger primarygrdBtn" Text="Upload Exel"></asp:LinkButton>
                                               </div>

                                           
                                        </div>



                                    </asp:Panel>
                                    </div>


                                    



                                </div>





                            </asp:Panel>
                        </div>
                    </div>


                    <div class="col-md-11">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDeleting="grvacc_RowDeleting" ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField HeaderText="Pay type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCheckno" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvbankna" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name">
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvBrance" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "branchname")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pay Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaydate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                            Width="70px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvpaydate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvpaydate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref ID">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbTotal" runat="server" OnClick="lbTotal_Click" CssClass="btn btn-primary primarygrdBtn"> Total </asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrefid" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refid")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid Amt." Visible="True">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaidamount" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamount")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Discount" Visible="True">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFdisamt" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdisamt" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;-#,##0; ") %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Replace Chq No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrRpChq" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "repchqno")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvremarks" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cllection From" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvColl" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "collfrm")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive  Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvRecType" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recType")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>

                    </div>
                    <div class="col-md-1">

                        <div class="form-group">
                            <asp:LinkButton ID="lbtnUpdate" CssClass="btn btn-success primaryBtn pull-right" runat="server" OnClick="lbtnUpdate_Click" Style="margin-top: 50px;"
                                Visible="False">Update</asp:LinkButton>
                            <div class=" clearfix"></div>
                        </div>
                    </div>







                    <div class="form-group">
                        <asp:Label ID="lPays" runat="server" CssClass="btn btn-success primaryBtn" Width="200px"
                            Text="Payment Shedule"></asp:Label>
                        <asp:CheckBox ID="chkConsolidate" runat="server" TabIndex="10" Text="Consolidate" Visible="true" AutoPostBack="true" OnCheckedChanged="chkConsolidate_CheckedChanged" CssClass="btn btn-primary checkBox" />


                        <div class="clearfix"></div>
                    </div>
                    <div>

                        <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode3" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Boook No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbookno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bookno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" FooterText="Total">
                                    <HeaderTemplate>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description of Item" Width="200px"></asp:Label>

                                        <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel "></i>
                                        </asp:HyperLink>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" Font-Size="12px" ForeColor="#000" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Schedule Date ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Schedule Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvschamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lfAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MR  No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="65px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="MR No (Manual)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrmno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="75px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvChequeNo" runat="server" ForeColor="Black" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpaydate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paiddate")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvfpayamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <%--  <asp:TemplateField HeaderText="Exess Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvexessamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exessamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lfexessAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Balance Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbalamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="100px" BorderStyle="None"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFbalanceAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="100px"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>


                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>

                    <div class="form-group">

                        <asp:Label ID="lblchqdishonour" runat="server" CssClass="btn btn-success primaryBtn" Width="350px"
                            Text="List of Dishonour Cheque :" Visible="False"></asp:Label>
                        <div class="clearfix"></div>
                    </div>


                    <asp:GridView ID="gvCDHonour" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Width="600px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mr. No">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMrrno" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cheque No">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvChequeNo" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDueDate" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paiddate")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvamount" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "schamt")).ToString("#,##0;(#,##0); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dishonour Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDisDate" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bank Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvBName" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Branch Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvBBName" runat="server" ForeColor="Black" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                        </Columns>


                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>


                </asp:View>
            </asp:MultiView>
        </div>
    </div>






    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
