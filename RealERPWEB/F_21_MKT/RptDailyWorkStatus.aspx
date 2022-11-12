<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptDailyWorkStatus.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptDailyWorkStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mt20 {
            margin-top: 20px !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblEmployee" runat="server">Employee</asp:Label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control chzn-select form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblDate" runat="server">Date</asp:Label>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalenderExtender_txtDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Ok" OnClick="lnkbtnShow_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body" style="min-height: 500px;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDailyWorkStatus" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID Card">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvidno" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idno")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblheader" runat="server" Text="Employee Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempid" runat="server" Visible="false" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="80px"></asp:Label>
                                        <asp:Label ID="lblgvempname" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Settlement </br>Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbilldate" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesignation" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "designation")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="gvlblFText" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='Total'></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdeptname" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joining </br>Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjoindat" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindat")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Seperation</br> Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvretdat" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "retdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payable </br>Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttlamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="gvlblFTotal" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='Total'></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Service Length">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvservleng" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "servleng")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvLink" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvstatus")).ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
