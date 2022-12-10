<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLetterOfAlotment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptLetterOfAlotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .chzn-select {
            width: 100%;
        }

        .chzn-container {
            width: 100% !important;
        }

        .gview tr td {
            border: 0;
        }

        .gview .form-control {
            height: 25px;
            line-height: 25px;
            padding: 0 12px;
            border-style: solid !important;
            border-color: #c6c9d5 !important;
        }

        #cardstyle {
            background-color: #E8E3E3;
            padding: 0 !important;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            //<a href="RptSalInterest.aspx">RptSalInterest.aspx</a>


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
                <div class="card-header well">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblprjname">Project Name</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlprjname" AutoPostBack="True" CssClass="chzn-select form-control" OnSelectedIndexChanged="ddlprjname_SelectedIndexChanged" ></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label1">Customer Name</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlcustomerName" AutoPostBack="True" CssClass="chzn-select form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 mt-4">
                            <div class="form-group">
                                <asp:LinkButton ID="btnok" OnClick="btnok_Click" runat="server" CssClass="btn btn-primary btn-sm ">OK</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
