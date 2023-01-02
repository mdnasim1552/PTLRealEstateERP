<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CustOthMoneyReceipt.aspx.cs" Inherits="RealERPWEB.F_23_CR.CustOthMoneyReceipt" %>

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

    <style type="text/css">
          .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>



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

            <div class="card mt-2">
                <div class="card-header">
                    <div class="row mt-4">




                        <div class=" col-md-3 d-none">

                            <asp:Label ID="Label1" CssClass="lblTxt lblName" runat="server" Text="Project Name:"></asp:Label>
                            <asp:TextBox ID="txtSrcPro" runat="server" CssClass=" inputtextbox" TabIndex="0"></asp:TextBox>
                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click" TabIndex="1"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Project Name:"></asp:Label>
                            <asp:DropDownList ID="ddlProjectName" runat="server" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" TabIndex="2" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:Label ID="lblProjectdesc" CssClass="form-control form-control-sm" Visible="False" runat="server"></asp:Label>

                        </div>

                         <div class="col-md-3 ml-2" >
                        <asp:Label ID="lblscustomer" CssClass="form-label" runat="server" Text="Customer Name:"></asp:Label>

                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="chzn-select form-control form-control-sm " TabIndex="5" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Label ID="lblCustomer" CssClass="form-control form-control-sm" Visible="False" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-1" style="margin-top:22px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click" TabIndex="6">Ok</asp:LinkButton>

                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblBalance" CssClass="form-label" runat="server" Text="Balance:" Visible="false"></asp:Label>
                            <asp:Label ID="lblCustomerFromService" CssClass="form-label" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtBalance" runat="server" CssClass="form-control" TabIndex="0" Visible="false" Enabled="false" Style="text-align: right;"></asp:TextBox>
                        </div>
                    

                    <div class=" col-md-3  pading5px asitCol3 d-none">

                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass=" inputtextbox" TabIndex="3"></asp:TextBox>

                        <asp:LinkButton ID="ibtnFindCustomer" runat="server" CssClass="btn btn-sm btn-primary" OnClick="ibtnFindCustomer_Click" TabIndex="4">
                                        <span class="glyphicon glyphicon-search asitGlyp"> </span>

                        </asp:LinkButton>
                    </div>
                   





                </div>
                    </div>
                <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="PnlMoneyReceipt" runat="server" Visible="False">

                        <asp:Panel ID="Panel3" runat="server" Visible="False">

                                    <div class="row">
                                        

                                        <div class="col-md-4">
                                             <asp:Label ID="Label30" runat="server" CssClass="form-label" Text="Previous MR:"></asp:Label>
                                            <asp:DropDownList ID="ddlPreMrr" runat="server" CssClass="form-control inputTxt" TabIndex="13">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton ID="lbtnShowPreMrr" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnShowPreMrr_Click">Show</asp:LinkButton>

                                        </div>
                                       
                                    </div>
                               


                        </asp:Panel>
                  


                                <div class="row mt-2">
                                    <div class="col-md-3" style="margin-top:22px;">
                                        <asp:CheckBox ID="chkOrginal" runat="server" Text="Orginal " Style="margin-left: 20px;" Font-Bold="true" Visible="False" />

                                        <asp:CheckBox ID="chkPrevious" runat="server" AutoPostBack="True" TabIndex="7" Font-Bold="true"
                                            CssClass=" btn btn-sm chkBoxControl primaryBtn" OnCheckedChanged="chkPrevious_CheckedChanged"
                                            Text="Previous Mrr" />
                                    </div>

                                    <div class="col-md-3" style="margin-top:22px;">
                                        <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Receive No" Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblReceiveNo" runat="server" CssClass="form-label" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Receive Date:" Font-Bold="true"></asp:Label>

                                        <asp:TextBox ID="txtReceiveDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"
                                            AutoPostBack="True" TabIndex="8"></asp:TextBox>

                                        <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>


                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="Replace Chq No" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtRpChqNo" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" TabIndex="8"> </asp:TextBox>

                                    </div>
                                </div>

                                <div class="row mt-2">
                                    <div class="col-md-3">

                                        <asp:Label ID="Label21" runat="server" CssClass="form-label" Text="Receipt  Amount" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtPaidamt" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" TabIndex="9"></asp:TextBox>

                                    </div>

                                    <div class="col-md-3">
                                        <asp:Label ID="Label5" runat="server" CssClass="form-label" Text="Pay type" Font-Bold="true"></asp:Label>
                                        <asp:DropDownList ID="ddlpaytype" runat="server" Font-Bold="True"  
                                            AutoPostBack="True" CssClass="form-control form-control-sm"
                                            OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label16" runat="server" CssClass="form-label" Text="Cheque No:" Font-Bold="true"></asp:Label>

                                        <asp:TextBox ID="txtchqno" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"
                                            AutoPostBack="True" TabIndex="11"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label20" runat="server" CssClass="form-label" Text="Bank Name" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtBName" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" TabIndex="12"></asp:TextBox>

                                    </div>
                                </div>



                                <div class="row mt-2">
                                    <div class="col-md-3">

                                        <asp:Label ID="Label10" runat="server" CssClass="form-label" Text="Branch Name " Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtBranchName" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" TabIndex="13"></asp:TextBox>

                                    </div>

                                    <div class="col-md-3">
                                        <asp:Label ID="Label22" runat="server" CssClass="form-label" Text="Pay Date" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtpaydate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" TabIndex="14"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtpaydate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtpaydate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label23" runat="server" CssClass="form-label" Text="Ref. ID" Font-Bold="true"></asp:Label>

                                        <asp:TextBox ID="txtrefid" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"
                                            AutoPostBack="True" TabIndex="15"></asp:TextBox>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label24" runat="server" CssClass="form-label" Text="Remarks" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtremarks" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" TabIndex="16"></asp:TextBox>

                                    </div>
                                   <div class="col-md-1" style="margin-top:20px;">
                                        <asp:LinkButton ID="lblAddToTable" runat="server" OnClick="lblAddToTable_Click" CssClass="btn btn-xm btn-primary" TabIndex="21">Add To Table</asp:LinkButton>
                                    </div>
                                </div>
                        <div class="row mt-2">
                                    
                            </div>
                                <div class="row mt-2 mb-4">
                                    <div class="col-md-2 d-none">

                                       
                                        <asp:TextBox ID="txtSrchCollfrm" runat="server" AutoCompleteType="Disabled" CssClass="form-" TabIndex="17"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnCollfrm" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnCollfrm_Click" TabIndex="18"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>

                                    <div class="col-md-4">
                                         <asp:Label ID="Label11" runat="server" CssClass="form-label" Text="Collection From" Font-Bold="true"></asp:Label>
                                        <asp:DropDownList ID="ddlCollType" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="19">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="Label13" runat="server" CssClass="form-label" Text="Receive Type:" Font-Bold="true"></asp:Label>

                                        <asp:DropDownList ID="ddlRecType" runat="server" CssClass="form-control form-control-sm chzn-select"  TabIndex="20">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="col-md-3">
                                        <asp:Label ID="lblbillno" runat="server" CssClass="form-label" Text="Bill No:" Font-Bold="true" Visible="false"></asp:Label>

                                        <asp:DropDownList ID="ddlbilno" runat="server" CssClass="form-control form-control-sm"  TabIndex="20" Visible="false">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2 d-none">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>

                                </div>




                    </asp:Panel>
                    </div>
                    

                </div>
                    <div class="row mb-4">
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
                                <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" CssClass="btn btn-danger btn-sm" TabIndex="22">Update</asp:LinkButton>
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
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

