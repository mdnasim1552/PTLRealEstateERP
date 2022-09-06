<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustOthMoneyReceipt.aspx.cs" Inherits="RealERPWEB.F_23_CR.CustOthMoneyReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
        };
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
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass=" inputtextbox" TabIndex="0"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass="chzn-select form-control inputTxt" TabIndex="2" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" CssClass="form-control inputTxt" Visible="False" runat="server"></asp:Label>

                                    </div>


                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click" TabIndex="6">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:Label ID="lblBalance" CssClass="lblTxt lblName" runat="server" Text="Balance:" Visible="false"></asp:Label>
                                        <asp:Label ID="lblCustomerFromService" CssClass="lblTxt lblName" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:TextBox ID="txtBalance" runat="server" CssClass="form-control" TabIndex="0" Visible="false" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="lblscustomer" CssClass="lblTxt lblName" runat="server" Text="Customer Name:"></asp:Label>
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass=" inputtextbox" TabIndex="3"></asp:TextBox>

                                        <asp:LinkButton ID="ibtnFindCustomer" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindCustomer_Click" TabIndex="4">
                                        <span class="glyphicon glyphicon-search asitGlyp"> </span>

                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="5" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCustomer" CssClass="form-control inputTxt" Visible="False" runat="server"></asp:Label>
                                    </div>


                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PnlMoneyReceipt" runat="server" Visible="False">

                            <asp:Panel ID="Panel3" runat="server" Visible="False">

                                <fieldset class="scheduler-border fieldset_A">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label30" runat="server" CssClass="lblTxt lblName" Text="Previous MR:"></asp:Label>
                                            </div>

                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlPreMrr" runat="server" CssClass="form-control inputTxt" TabIndex="13">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:LinkButton ID="lbtnShowPreMrr" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShowPreMrr_Click">Show</asp:LinkButton>

                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </fieldset>


                            </asp:Panel>
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">


                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:CheckBox ID="chkOrginal" runat="server" Text="Orginal " Style="margin-left: 20px;" Visible="False" />

                                            <asp:CheckBox ID="chkPrevious" runat="server" AutoPostBack="True" TabIndex="7"
                                                CssClass=" btn chkBoxControl primaryBtn" OnCheckedChanged="chkPrevious_CheckedChanged"
                                                Text="Previous Mrr" />
                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Receive No"></asp:Label>
                                            <asp:Label ID="lblReceiveNo" runat="server" CssClass="smLbl_to"></asp:Label>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Receive Date:"></asp:Label>

                                            <asp:TextBox ID="txtReceiveDate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"
                                                AutoPostBack="True" TabIndex="8"></asp:TextBox>

                                            <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>


                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Replace Chq No"></asp:Label>
                                            <asp:TextBox ID="txtRpChqNo" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox" TabIndex="8"> </asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label21" runat="server" CssClass="lblTxt lblName" Text="Receipt  Amount"></asp:Label>
                                            <asp:TextBox ID="txtPaidamt" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox" TabIndex="9"></asp:TextBox>

                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Pay type"></asp:Label>
                                            <asp:DropDownList ID="ddlpaytype" runat="server" Font-Bold="True" Width="95px" TabIndex="10"
                                                AutoPostBack="True" CssClass="ddlistPull"
                                                OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName" Text="Cheque No:"></asp:Label>

                                            <asp:TextBox ID="txtchqno" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"
                                                AutoPostBack="True" TabIndex="11"></asp:TextBox>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label20" runat="server" CssClass="lblTxt lblName" Text="Bank Name"></asp:Label>
                                            <asp:TextBox ID="txtBName" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox" TabIndex="12"></asp:TextBox>

                                        </div>
                                    </div>



                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label10" runat="server" CssClass="lblTxt lblName" Text="Branch Name "></asp:Label>
                                            <asp:TextBox ID="txtBranchName" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox" TabIndex="13"></asp:TextBox>

                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label22" runat="server" CssClass="lblTxt lblName" Text="Pay Date"></asp:Label>
                                            <asp:TextBox ID="txtpaydate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtpaydate_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtpaydate"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label23" runat="server" CssClass="lblTxt lblName" Text="Ref. ID"></asp:Label>

                                            <asp:TextBox ID="txtrefid" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"
                                                AutoPostBack="True" TabIndex="15"></asp:TextBox>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label24" runat="server" CssClass="lblTxt lblName" Text="Remarks"></asp:Label>
                                            <asp:TextBox ID="txtremarks" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox" TabIndex="16"></asp:TextBox>

                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lblAddToTable" runat="server" OnClick="lblAddToTable_Click" CssClass="btn btn-primary primaryBtn" TabIndex="21">Add To Table</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName" Text="Collection From"></asp:Label>
                                            <asp:TextBox ID="txtSrchCollfrm" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox" TabIndex="17"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnCollfrm" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnCollfrm_Click" TabIndex="18"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:DropDownList ID="ddlCollType" runat="server" CssClass="form-control inputTxt" TabIndex="19">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="Receive Type:"></asp:Label>

                                            <asp:DropDownList ID="ddlRecType" runat="server" CssClass="ddlPage" Width="95px" TabIndex="20">
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblbillno" runat="server" CssClass="lblTxt lblName" Text="Bill No:" Visible="false"></asp:Label>

                                            <asp:DropDownList ID="ddlbilno" runat="server" CssClass="ddlPage" Width="95px" TabIndex="20" Visible="false">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>

                                    </div>

                                </div>
                            </fieldset>



                        </asp:Panel>

                    </div>
                    <asp:GridView ID="gvMoneyreceipt" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        OnRowDeleting="gvMoneyreceipt_RowDeleting" ShowFooter="True" Width="831px">
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
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Replace Cheque NO">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvrepchqno" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "repchqno")) %>'
                                        Width="80px"></asp:TextBox>
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
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" CssClass="btn btn-danger primaryBtn" TabIndex="22">Update</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvpaydate" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydate"))%>'
                                        Width="80px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtgvpaydate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvpaydate"></cc1:CalendarExtender>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref ID">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" OnClick="lbTotal_Click" Style="text-decoration: none;"> Total </asp:LinkButton>
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
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Received Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvrectype" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "RecTyped")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bill No">
                                <ItemTemplate>
                                    <asp:Label ID="lbgvbillnoo" runat="server" BorderColor="#99CCFF"
                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                        Style="text-align: Left; background-color: Transparent"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
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
                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

