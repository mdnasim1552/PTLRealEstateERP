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
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Ok" OnClick="lnkbtnShow_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="lblPageSize" runat="server">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>1000</asp:ListItem>
                                <asp:ListItem>2000</asp:ListItem>
                                <asp:ListItem>3000</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="card-body" style="min-height: 500px;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvDailyWorkStatus" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvDailyWorkStatus_PageIndexChanging"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvDailyWorkStatus_RowDataBound"
                            ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGrp" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grp")) %>'></asp:Label>
                                        <asp:Label ID="lblgvWorkStatus" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPID" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblheader" runat="server" Text="Prospect Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export to Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSircode" runat="server" Visible="false" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                            Width="80px"></asp:Label>
                                        <asp:Label ID="lblgvProsName" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Generated">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvFollowupCreateDate" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "nfollowupcdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Followup Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvFollowupDate" runat="server"
                                            BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "nfollowupdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Followup">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvFollowup" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followupdesc")).ToString() %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle CssClass="gvPagination" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
