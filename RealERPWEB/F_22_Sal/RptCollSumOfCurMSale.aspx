<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptCollSumOfCurMSale.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptCollSumOfCurMSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);




        });
        function pageLoaded() {

            $('#tblrpcashflow').gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",< a href = "RptCollSumOfCurMSale.aspx" > RptCollSumOfCurMSale.aspx</a >
            varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 6
                          

            });
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
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



                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="control-label" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfrmDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lbltodate" runat="server" CssClass="control-label" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSrcProject" runat="server" TabIndex="3" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindProject" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="4" CssClass="btn btn-sm">Project Name</asp:LinkButton>
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" TabIndex="5">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card card-fluid">
                        <div class="card-body" style="min-height:480px;">
                            <div class="row table-responsive">
                                <asp:Repeater ID="rpcsummary" runat="server" OnItemDataBound="rpcsummary_ItemDataBound">
                                    <HeaderTemplate>
                                        <table id="tblrpcsummary" class="table-striped table-hover table-bordered grvContentarea">
                                            <tr>
                                                <th>SL</th>
                                                <th style="width: 120px;">Project Name</th>
                                                <th style="width: 120px;">Sales Team</th>
                                                <th style="width: 120px;">Customer</th>
                                                <th style="width: 80px;">Unit</th>
                                                <th style="width: 130px;">Collection From Booking Money</th>
                                                <th style="width: 130px;">Collection From Installation</th>
                                                <th style="width: 130px;">Total Collection</th>
                                                <th style="width: 130px;">Bank Clearance</th>

                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))  %>' Width="130px"></asp:Label>


                                            </td>

                                            <td>
                                                <asp:Label ID="lblrpSalesTeam" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "salesteam"))  %>' Width="130px"></asp:Label>


                                            </td>




                                            <td style="text-align: left">
                                                <asp:Label ID="lblrpCustomer" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>' Width="70px"></asp:Label>
                                            </td>

                                            <td style="text-align: left">
                                                <asp:Label ID="lblrpSunit" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpbMoney" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpColIns" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpTCollection" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblrpBclearance" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bclam")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr>
                                            <th></th>
                                            <th></th>
                                            <th>Total </th>
                                            <th></th>
                                            <th></th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblrpFbMoney" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblFcinstall" runat="server" Width="80px"></asp:Label>
                                            </th>

                                            <th style="text-align: right">
                                                <asp:Label ID="lblFtotal" runat="server" Width="80px"></asp:Label>
                                            </th>
                                            <th style="text-align: right">
                                                <asp:Label ID="lblFbclearace" runat="server" Width="80px"></asp:Label>
                                            </th>

                                        </tr>
                                        </table>
                                    </FooterTemplate>


                                </asp:Repeater>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


