<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccOnlinePaymnt.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.AccOnlinePaymnt" %>

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


        function Confirmation() {
            if (confirm('Are you sure you want to save?')) {
                return;
            } else {
                return false;
            }
        }



    </script>
    <style>
        .grvHeader th {
            text-align: center;
        }
        .chzn-single {
            border-radius: 3px !important;
            height: 31px !important;
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
            <div class="card card-fluid mb-1 mt-2">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Panel ID="PnlBill" runat="server">
                                <div class="row">

                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Receive Date"></asp:Label>
                                            <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy"
                                                TargetControlID="txtReceiveDate"></cc1:CalendarExtender>

                                        </div>
                                    </div>



                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" Text="Resource Head"></asp:Label>

                                            <asp:DropDownList ID="ddlResourceHead" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlResourceHead_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>




                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Bill No"></asp:Label>
                                            <asp:TextBox ID="txtsrchBillno" runat="server" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>

                                            <asp:LinkButton ID="ibtnBillNo" CssClass="btn btn-sm btn-primary" runat="server" Visible="false" OnClick="ibtnBillNo_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                            <asp:DropDownList ID="ddlBillList" runat="server" CssClass="chzn-select form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-1 Pr-0" style="margin-top:22px;">
                                        <div class="form-group">

                                            <asp:RadioButtonList runat="server" ID="rblpaytype" CssClass="rbtnList1 chkBoxControl pull-right" RepeatDirection="Horizontal" Visible="false">

                                                <%--<asp:ListItem Value="Date Wise">Date Wise</asp:ListItem>--%>
                                                <asp:ListItem Value="Resource" Selected="True">Resource</asp:ListItem>
                                            </asp:RadioButtonList>

                                            <asp:CheckBox ID="chkSinglIssue" runat="server" Text="Multi Bill" Checked="false" Style="margin-left: 30px" CssClass="margin5px chkBoxControl" AutoPostBack="true" OnCheckedChanged="chkSinglIssue_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div class="col-md-1 pr-0"  style="margin-top:22px;">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Visible="false" Text="Total Payment"></asp:Label>
                                        <asp:TextBox ID="txtrecordno" runat="server" AutoCompleteType="Disabled" Visible="false" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                        <asp:LinkButton ID="lbtnAddTable" runat="server" CssClass="btn btn-sm btn-primary"
                                            OnClick="lbtnAddTable_Click">Add Bill</asp:LinkButton>
                                        </div>
                                        </div>
                                    <div class="col-md-1 Pr-0"  style="margin-top:22px;">
                                        <div class="form-group">
                                        <asp:LinkButton ID="btnAllBill" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btnAllBill_Click">Add All Bill</asp:LinkButton>
                                            </div>
                                        </div>
                                     <div class="col-md-1"  style="margin-top:22px;">
                                        <div class="form-group">
                                        <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="btn btn-sm btn-primary" Style="margin-left: 30px"
                                            OnClick="lbtnRefresh_Click">Reset</asp:LinkButton>


                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                        <asp:Label ID="lblmslnum" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="lblslnum" runat="server" Visible="False"></asp:Label>
                                            </div>
                                    </div>





                                </div>
                            </asp:Panel>
                        </div>


                    </div>
                    <div class="row">
                        <div class="table table-responsive">
                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                Style="margin-top: 0px" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnRowCancelingEdit="gvPayment_RowCancelingEdit"
                                OnRowEditing="gvPayment_RowEditing" OnRowUpdating="gvPayment_RowUpdating"
                                OnRowDeleting="gvPayment_RowDeleting" OnRowDataBound="gvPayment_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="15px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvmslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Payment Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>


                                    <asp:CommandField CancelText="Can" ShowEditButton="False" UpdateText="Up" />


                                    <asp:TemplateField HeaderText="Value Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvValdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                Width="60px"></asp:Label>


                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Received Date">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvrcvdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; font-size: 11px;" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyy") %>'
                                                Width="80px"></asp:Label>
                                            <%-- <cc1:CalendarExtender ID="txtgvrcvdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy"
                                                TargetControlID="txtgvrcvdate"></cc1:CalendarExtender>--%>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Head of Accounts">

                                        <EditItemTemplate>
                                            <table style="width: 500px;">
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PnlBill" runat="server" Width="442px">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Receive Date:"></asp:Label>
                                                                <asp:TextBox ID="txteditReceiveDate" runat="server" AutoCompleteType="Disabled"
                                                                    AutoPostBack="True" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                    Enabled="True" Format="dd.MM.yyyy"
                                                                    TargetControlID="txteditReceiveDate"></cc1:CalendarExtender>

                                                                <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to" Text="Bill Ref. No"></asp:Label>
                                                                <asp:TextBox ID="txteditRefno" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                                <div class="clearfix"></div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Head of Accounts"></asp:Label>
                                                                    <asp:TextBox ID="txtsrchProject" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>

                                                                    <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" Visible="false" OnClick="ibtnFindProject_Click" TabIndex="6"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                                                    <div class="clearfix"></div>
                                                                </div>

                                                                <div class="col-md-4 pading5px asitCol3">
                                                                    <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" CssClass="inputTxt chzn-select" Width="100%"
                                                                        OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="7">
                                                                    </asp:DropDownList>
                                                                </div>

                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="lblDetHead" runat="server" CssClass="lblTxt lblName" Text="Details Head:"></asp:Label>
                                                                    <asp:TextBox ID="txtsrchRes" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>

                                                                    <asp:LinkButton ID="ibtnRes" CssClass="btn btn-primary srearchBtn" runat="server" Visible="false" OnClick="ibtnRes_Click" TabIndex="6"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                                                    <div class="clearfix"></div>
                                                                </div>
                                                                <div class="col-md-4 pading5px asitCol3">
                                                                    <asp:DropDownList ID="ddlRescode" runat="server" AutoPostBack="True" CssClass="inputTxt chzn-select" Width="100%" TabIndex="7">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Appr. Amount"></asp:Label>
                                                                    <asp:TextBox ID="txteditBillAmount" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                                    <asp:Label ID="Label8" runat="server" CssClass=" smLbl_to" Text="Adv. Amount" Visible="false"></asp:Label>
                                                                    <asp:TextBox ID="txteditAdvAmt" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>



                                                                </div>
                                                                <div class="clearfix"></div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName" Text="Value Date"></asp:Label>
                                                                    <asp:Label ID="lbleditValDate" runat="server" AutoPostBack="true" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:Label>



                                                                    <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="Pay Date"></asp:Label>
                                                                    <asp:TextBox ID="txteditpaymentdate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"
                                                                        AutoPostBack="True"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txteditpaymentdate_CalendarExtender" runat="server"
                                                                        Enabled="True" Format="dd.MM.yyyy"
                                                                        TargetControlID="txteditpaymentdate"></cc1:CalendarExtender>


                                                                </div>
                                                                <div class="clearfix"></div>
                                                            </div>

                                                            <div class="form-group" style="display: none;">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName" Text="Pay To"></asp:Label>
                                                                    <asp:TextBox ID="txtSrhParty" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                                    <asp:LinkButton ID="ibtnFindParty" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindParty_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                                                </div>
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:DropDownList ID="ddlPartyName" runat="server" CssClass=" ddlistPull" Width="100%">
                                                                    </asp:DropDownList>

                                                                </div>
                                                                <div class="clearfix"></div>
                                                            </div>
                                                            <div class="form-group" style="display: none;">
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Bill Nature"></asp:Label>
                                                                    <asp:TextBox ID="txtsrchnature" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                                    <asp:LinkButton ID="ibtnnature" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnnature_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                                                </div>
                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:DropDownList ID="ddlBillNature" runat="server" CssClass=" ddlistPull" Width="100%">
                                                                    </asp:DropDownList>

                                                                </div>
                                                                <div class="clearfix"></div>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:LinkButton ID="lbtnGrdUpdate" runat="server"
                                                                    OnClick="lbtnGrdUpdate_Click"
                                                                    CssClass="btn btn-danger primaryBtn"
                                                                    TabIndex="14">Add</asp:LinkButton>
                                                            </div>

                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Details Head">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click" OnClientClick="return Confirmation();">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgvResdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="170px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt1")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbok" runat="server" CommandArgument="lbok"
                                                OnClick="Add_Click" Width="30px">Add</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Payment Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apppaydate")) %>'
                                                Width="60px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtgvpaymentdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy"
                                                TargetControlID="txtgvpaymentdate"></cc1:CalendarExtender>


                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>


                                    <%--   <asp:TemplateField HeaderText="Cheque Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvchequedate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqdate")) %>'
                                                Width="65px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtgvchequedate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy"
                                                TargetControlID="txtgvchequedate"></cc1:CalendarExtender>


                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>--%>


                                    <asp:TemplateField HeaderText="Payment Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFtopayamt" runat="server" ForeColor="#000"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvpayamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Adv. Amt." Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="txtFAdvTotal" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAdvamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Net Amt." Visible="false">
                                        <FooterTemplate>
                                            <asp:Label ID="txtFNetTotal" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvNetamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bill Nature" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lbgvbillnaturedsc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Name" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref. No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>





                                    <asp:CommandField ShowDeleteButton="True" />

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
            </div>




            <%--<table style="width: 100%;">
                <tr>
                    <td colspan="12">
                        <asp:Panel ID="xPnlBill" runat="server">
                            <table style="width: 100%; background: #cccc99">

                                <tr>
                                    <td class="style43">
                                        <asp:Label ID="Label42" runat="server" CssClass="lbltextColor" Text="Receive Date:"
                                            Width="110px"></asp:Label>
                                    </td>
                                    <td class="style66">
                                        <asp:TextBox ID="txtReceiveDate" runat="server" AutoCompleteType="Disabled"
                                            AutoPostBack="True" BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px"
                                            OnTextChanged="txtReceiveDate_TextChanged" Width="80px"></asp:TextBox>

                                    </td>
                                    <td class="style43">
                                        <asp:Image ID="imgrecdate" runat="server" Height="16"
                                            ImageUrl="~/Image/calender.png" Width="16" />
                                    </td>
                                    <td class="style83"></td>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style76">&nbsp;</td>
                                    <td class="style101">&nbsp;</td>
                                    <td class="style75">&nbsp;</td>
                                    <td class="style75">&nbsp;</td>
                                    <td class="style75">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style43">
                                        <asp:Label ID="Label38" runat="server" CssClass="lbltextColor" Text="Bill No:"
                                            Width="110px"></asp:Label>
                                    </td>
                                    <td class="style66">
                                        <asp:TextBox ID="txtsrchBillno" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" TabIndex="11"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style43">
                                        <asp:ImageButton ID="ibtnBillNo" runat="server" Height="18px"
                                            ImageUrl="~/Image/find_images.jpg" OnClick="ibtnBillNo_Click" TabIndex="12" />
                                    </td>
                                    <td class="style83"></td>
                                    <td class="style43">
                                        <asp:Label ID="Label48" runat="server" CssClass="lbltextColor"
                                            Text="Total Record:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style43">
                                        <asp:TextBox ID="txtrecordno" runat="server" AutoCompleteType="Disabled"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" TabIndex="14"
                                            Width="30px"></asp:TextBox>
                                    </td>
                                    <td class="style43">
                                        <asp:LinkButton ID="lbtnAddTable" runat="server" BackColor="#003366"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black"
                                            OnClick="lbtnAddTable_Click" Style="font-size: small; height: 17px;"
                                            TabIndex="15" Width="60px">Add Table</asp:LinkButton>
                                    </td>
                                    <td class="style76">&nbsp;</td>
                                    <td class="style101">
                                        <asp:Label ID="lmsg" runat="server" BackColor="Red" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" Style="color: #FFFFFF"></asp:Label>
                                    </td>
                                    <td class="style75"></td>
                                    <td class="style75"></td>
                                    <td class="style75">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style66">&nbsp;</td>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style43">&nbsp;</td>
                                    <td class="style76">&nbsp;</td>
                                    <td class="style101">&nbsp;</td>
                                    <td class="style75">&nbsp;</td>
                                    <td class="style75">&nbsp;</td>
                                    <td class="style75">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="12"></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
