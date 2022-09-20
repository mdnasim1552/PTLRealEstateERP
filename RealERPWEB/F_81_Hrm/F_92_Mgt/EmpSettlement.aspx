<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpSettlement.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmpSettlement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .tblEMPinfo tr td {
            padding: 0 5px;
        }

        .gvWidth {
            width: 272px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
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
            <div class="card mt-3">
                <div class="card-header mb-0 p-0">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="lblRefNo" runat="server">Ref No.</asp:Label>
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2">
                            <div class="form-group">
                                <asp:Label ID="lblDate" runat="server">Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblEmpList" runat="server">Employee List</asp:Label>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="col-md-12">
                                <asp:RadioButtonList ID="rbtnstatement" runat="server" AutoPostBack="True" Visible="false"
                                    BackColor="#DFF0D8" BorderColor="#000" CssClass="rbtnList1 margin5px"
                                    Font-Bold="True" Font-Size="11px" ForeColor="Black" OnSelectedIndexChanged="rbtnstatement_OnSelectedIndexChanged"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">English</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body pb-5">
                    <asp:Panel ID="ViewDataPanel" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-12 col-lg-12 col-sm-12" runat="server" id="engst" visible="True">
                                <h5>Employee Information</h5>
                                <table class="table table-striped table-hober tblEMPinfo">
                                    <tr>
                                        <td class="table-info">Name</td>
                                        <td>
                                            <asp:Label ID="lblname" runat="server"></asp:Label>
                                        </td>
                                        <td class="table-info">&nbsp;</td>
                                        <td></td>
                                        <td class="table-info">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="table-info">Designation</td>
                                        <td>
                                            <asp:Label ID="lbldesig" runat="server"></asp:Label>
                                        </td>
                                        <td class="table-info">Joining Date</td>
                                        <td>
                                            <asp:Label ID="lbljoin" runat="server"></asp:Label>
                                        </td>
                                        <td class="table-info">Seperation Date</td>
                                        <td>
                                            <asp:Label ID="lblsep" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table-info">Id Card No</td>
                                        <td>
                                            <asp:Label ID="lblidcard" runat="server"></asp:Label>
                                        </td>
                                        <td class="table-info">Section/Department</td>
                                        <td>
                                            <asp:Label ID="lblsection" runat="server"></asp:Label>
                                        </td>
                                        <td class="table-info">Present Salary</td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server">0000</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table-info">Job Seperation Type</td>
                                        <td>
                                            <asp:Label ID="lblseptype" runat="server"></asp:Label>
                                        </td>
                                        <td class="table-info">Last Date of Office</td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </td>
                                        <td class="table-info">Service Period</td>
                                        <td>
                                            <asp:Label ID="lblservlen" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>


                                </table>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-md-6 col-lg-6 col-sm-12">

                                <h5 class="text-center">Salary Information</h5>
                                <%--<asp:LinkButton ID="copy" runat="server" Text="Copy" OnClick="copy_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>--%>

                                <asp:GridView ID="gvsettlemntcredit" OnRowDataBound="gvsettlemntcredit_RowDataBound" runat="server" AutoGenerateColumns="False"
                                    CssClass="table-striped table-hover table-bordered grvContentarea mb-3"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Information">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcreditinfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                    Width="150px"></asp:Label>
                                                <asp:Label ID="lblhrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfrmdat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmdat")) %>' Width="75px"></asp:Label>

                                                <asp:TextBox ID="txtfrmdat" runat="server" CssClass="form-control form-control-sm" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmdat")) %>'></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtfrmdat_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdat"></cc1:CalendarExtender>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="To">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltodat" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "todat")) %>' Width="75px"></asp:Label>
                                                <asp:TextBox ID="txttodat" runat="server" CssClass="form-control form-control-sm" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "todat")) %>'></asp:TextBox>
                                                <cc1:CalendarExtender ID="txttodat_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodat"></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="D/Y">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnumday" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "numofday")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" Width="50px" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Gross">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnumofday" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:TextBox>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Calculation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcalculation" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "calculation")) %>' BackColor="Yellow" ForeColor="Black"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Net Payable">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TtlAmout" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblfttlamt" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <%--<asp:ButtonField Text="Select" CommandName="Select" ButtonType="Button" />--%>
                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                </asp:GridView>

                            </div>


                            <div class="col-md-6 col-lg-6 col-sm-12">
                                <h5 class="text-center">Deduction Information</h5>
                                <asp:GridView ID="gvsttlededuct" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deduct Information">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcreditinfo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                                    Width="250px"></asp:Label>
                                                <asp:Label ID="lblhrgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="To">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="D/Y">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnmday" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "numofday")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" Width="50px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Gross">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnumofday" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:TextBox>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Calculation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcalculation" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "calculation")) %>' BackColor="Yellow" ForeColor="Black"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Receivable">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TtlAmout" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfdedttlamt" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                </asp:GridView>

                            </div>
                        </div>
                        <table class="table-striped table-hover table-bordered">
                            <tr>
                                <td class="text-right" style="width: 620px; color: black; font-weight: bolder; font-size: 13px;">Net Payable Amount</td>
                                <td style="width: 130px" class="text-right">
                                    <asp:Label ID="NetAmount" runat="server" Font-Bold="true" Style="color: black; font-weight: bolder; font-size: 13px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


