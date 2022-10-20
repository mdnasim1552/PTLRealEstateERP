<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSettlementStatus.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RptSettlementStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        div#ContentPlaceHolder1_ddlWstation_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlDept_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlSection_chzn {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>

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
        };

        function SetTarget(type) {
            window.open('../../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
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
            <div class="card mt-3">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server">Company</asp:Label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">Date</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="lblLcName" runat="server">To</asp:Label>
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                                <cc1:CalendarExtender ID="dateto" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lnkbtnSerOk_Click" CssClass="btn btn-primary btn-sm mt20" TabIndex="4"></asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvSettInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" OnRowDataBound="gvSettInfo_RowDataBound">
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ToolTip="Edit And Approve" ID="lnkEdit" Target="_blank" CssClass="btn btn-xs btn-success" runat="server"><i class="fa fa-edit"></i></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Print">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="HypRDDoPrint" runat="server" OnClick="HypRDDoPrint_Click" CssClass="btn btn-xs btn-success"><i class="fa fa-print"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblHeadCost" runat="server" CssClass="btn btn-primary primaryBtn"
                    Text="Cost:" Visible="False">                             
                </asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
