<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpLoanInfo.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_85_Lon.EmpLoanInfo1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });

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

            <div class="card card-fluid container-data" style="min-height: 500px;">
                <div class="card-header mt-3 mb-0 pb-0">
                    <div class="row mb-0 pb-0">

                        <asp:Label ID="lbldate" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Loan Date</asp:Label>

                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm col-1 "></asp:TextBox>
                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                        <asp:Label ID="loanNo" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Loan No:</asp:Label>
                        <asp:Label ID="lblCurNo1" runat="server" CssClass="btn btn-sm btn-secsondary "></asp:Label>
                        <asp:Label ID="lblCurNo2" runat="server" CssClass="btn btn-sm btn-secsondary "></asp:Label>
                        <asp:LinkButton ID="lbtnPrevLoanList" runat="server" OnClick="lbtnPrevLoanList_Click" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Prev. Loan List:</asp:LinkButton>
                        <asp:DropDownList ID="ddlPrevLoanList" data-placeholder="Choose Employee.." runat="server"
                            CssClass="chzn-select form-control col-5" AutoPostBack="true">
                        </asp:DropDownList>

                    </div>

                    <div class="row mb-0 pb-0 pt-2">

                        <asp:Label ID="lblEmplist" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Employee List</asp:Label>

                        <asp:DropDownList ID="ddlEmpList" data-placeholder="Choose loan.." runat="server"
                            CssClass="chzn-select form-control col-3" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:Label ID="lblEmpName" runat="server" Visible="false" CssClass="form-control form-control-sm  col-3"></asp:Label>

                        <asp:Label ID="Label5" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Loan Type :</asp:Label>

                        <asp:DropDownList ID="ddlLoantype" data-placeholder="Choose loan.." runat="server"
                            CssClass="chzn-select form-control col-3" AutoPostBack="true">
                        </asp:DropDownList>

                        <asp:Label ID="Label6" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Upto Paid</asp:Label>
                        <asp:TextBox ID="txtPaidAmt" runat="server" CssClass="form-control form-control-sm  col-1" Style="text-align: right"></asp:TextBox>
                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-success btn-sm ml-1 col-1">Ok</asp:LinkButton>

                    </div>

                    <div class="row mb-0 pb-0 pt-2" runat="server" id="pnlloan" visible="false">

                        <%--  <asp:Panel ID="pnlloan" runat="server">
                            <div class="form-horizontal">
                                <div class="form-group">--%>
                        <asp:Label ID="Label1" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Total Amount</asp:Label>
                        <asp:TextBox ID="txtToamt" runat="server" CssClass="form-control form-control-sm col-1"></asp:TextBox>

                        <asp:Label ID="Label2" runat="server" CssClass=" btn btn-sm btn-secsondary mr-2 col-1">Ins. Amount</asp:Label>
                        <asp:TextBox ID="txtinsamt" runat="server" CssClass="form-control form-control-sm col-1"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" CssClass=" btn btn-sm btn-secsondary mr-2 col-1">Start Date</asp:Label>
                        <asp:TextBox ID="txtstrdate" runat="server" CssClass="form-control form-control-sm col-1"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtstrdate_CalendarExtender" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtstrdate"></cc1:CalendarExtender>
                        <asp:Label ID="Label4" runat="server" CssClass=" btn btn-sm btn-secsondary mr-2 col-1">Duration</asp:Label>
                        <asp:DropDownList ID="ddlMonth" runat="server" ata-placeholder="Choose Month.." CssClass="chzn-select form-control col-2">
                            <asp:ListItem Value="1">1 Month</asp:ListItem>
                            <asp:ListItem Value="2">2 Month</asp:ListItem>
                            <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                            <asp:ListItem Value="4">4 Month</asp:ListItem>
                            <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                            <asp:ListItem Value="6">6 Month</asp:ListItem>
                            <asp:ListItem Value="7">7  Month</asp:ListItem>
                            <asp:ListItem Value="8">8  Month</asp:ListItem>
                            <asp:ListItem Value="9">9  Month</asp:ListItem>
                            <asp:ListItem Value="10">10  Month</asp:ListItem>
                            <asp:ListItem Value="11">11  Month</asp:ListItem>
                        </asp:DropDownList>

                        <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-info btn-sm ml-1 col-1" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>
                        <%--    </div>
                            </div>
                        </asp:Panel>--%>
                    </div>
                    <div class="row mb-0 pb-0 pt-2">
                        <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True" CssClass="btn btn-info btn-sm ml-1 col-2 "
                            OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment"
                            Visible="False" />
                        <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                            Text="Add.Installment" CssClass=" btn btn-info btn-sm ml-1 col-1  chkBoxControl"
                            Visible="False" OnCheckedChanged="chkAddIns_CheckedChanged" />
                        <asp:LinkButton ID="lbtnAddInstallment" runat="server" OnClick="lbtnAddInstallment_Click"
                            Visible="False" CssClass="btn btn-info btn-sm ml-1 col-1">Add</asp:LinkButton>
                    </div>
                </div>

                <div class="card-body">

                    <div class="row table table-responsive">
                        <asp:GridView ID="gvloan" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvloan_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." FooterText="Total ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-info btn-sm" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Installment Date.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvinstdate" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lndate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvinstdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvinstdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True"
                                            CssClass="btn btn-success btn-sm ml-1" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Installment Amt.">
                                    <HeaderTemplate>
                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                            ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="gvlFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="120px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="120px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "paidamt")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDel" runat="server" CssClass="btn btn-xs btn-danger" OnClientClick="return confirm('Are you sure to delete this item?');" OnClick="lnkDel_Click" Text="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
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
