<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSalesEvaulation.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptSalesEvaulation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .mt30 {
            margin-top: 30px;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 29px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });
        });
        function pageLoaded() {
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });
            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>

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
        <div class="card-body mt-1 mb-0">
            <div class="row">
                <div class="col-sm-1.5 col-md-1.5  col-lg-1.5">
                    <div class="form-group">
                        <asp:Label ID="lblFdate" runat="server">From </asp:Label>
                        <asp:TextBox ID="txtfromdate" runat="server" autocomplete="off" CssClass="form-control form-control-sm"></asp:TextBox>
                        <cc1:CalendarExtender ID="csefdate" runat="server"
                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-sm-1.5  col-md-1.5  col-lg-1.5 ml-3">
                    <div class="form-group">
                        <asp:Label ID="lblTdate" runat="server">To </asp:Label>
                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        <cc1:CalendarExtender ID="cetdate" runat="server"
                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-3 col-sm-3 col-lg-3">
                    <div class="form-group">
                        <asp:Label ID="lblEmpName" runat="server" CssClass="control-label" Text="Employee "></asp:Label>
                        <asp:LinkButton ID="ibtnFindEmp" CssClass="srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><i class="fas fa-search"></i></asp:LinkButton>
                        <asp:DropDownList ID="ddlEmp" runat="server" CssClass="chzn-select form-control form-control-sm chzn-select" Style="width: 290px;" TabIndex="3">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-1 col-md-1 col-lg-1">
                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                </div>
                <div class="col-sm-2 col-md-2 col-lg-2">
                    <%--<div class="form-group">
                        <asp:Label ID="lblPage" runat="server">Page Size</asp:Label>
                        <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                            <asp:ListItem>150</asp:ListItem>
                            <asp:ListItem>200</asp:ListItem>
                            <asp:ListItem>300</asp:ListItem>
                            <asp:ListItem>500</asp:ListItem>>
                        </asp:DropDownList>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="card card-fluid ">
        <div class="card-body mt-1 mb-0 pb-0" style="min-height: 500px">
            <div class="row">
            </div>
        </div>
    </div>
</asp:Content>
