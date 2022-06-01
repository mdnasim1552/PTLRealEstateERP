<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpOfficeTimeSetup.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpOfficeTimeSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div#ContentPlaceHolder1_ddlCompany_chzn {
            width: 100% !important;
        }

        div#ContentPlaceHolder1_ddlProjectName_chzn {
            width: 100% !important;
        }


        div#ContentPlaceHolder1_ddlSection_chzn {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .w100 {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }
        .chzn-container{
            width:100%!important;
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

    <%--    <script language="javascript" type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">--%>


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>
    <style>
    </style>
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


    <div class="card mt-5">
        <div class="card-header">
            <div class="row">

                <div class="col-lg-3 col-md-3 col-sm-4">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server">Company</asp:Label>
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-4">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server">Department</asp:Label>
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-lg-3  col-md-3 col-sm-4">
                    <div class="form-group">
                        <asp:Label ID="lblSection" runat="server">Section</asp:Label>
                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" TabIndex="7">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-1">
                    <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                </div>
            </div>






        </div>
        <div class="card-body">
            <asp:Panel ID="pnlOfftime" runat="server" Visible="False">
                <div class="row">
    <%--                <div class="col-lg-3">
                        <div class="form-group">
                            <asp:Label ID="lblfrmdate" runat="server" Visible="false">Date</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                        </div>
                    </div>

                    <div class="col-lg-3">
                        <div class="form-group">
                            <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt lblName" Visible="false">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" Visible="false" Style="width: 85px;"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                        </div>
                    </div>--%>
                    <div class="col-lg-2">
                        <div class="form-group">
                            <asp:Label ID="lbltOfftime1" runat="server" CssClass="lblTxt lblName">Office InTime</asp:Label>
                            <asp:DropDownList ID="ddlOffintimedw" runat="server"  CssClass="form-control form-control-sm" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-2">
                        <div class="form-group">
                            <asp:Label ID="lbltLantime1" runat="server" CssClass="lblTxt lblName">Launch InTime</asp:Label>
                            <asp:DropDownList ID="ddlLanintimedw" runat="server"  CssClass="form-control form-control-sm" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                    </div>


                    
                    <div class="col-lg-2">
                        <div class="form-group">
                            <asp:Label ID="lbltOfftime2" runat="server" CssClass="lblTxt lblName">Office OutTime</asp:Label>
                            <asp:DropDownList ID="ddlOffouttimedw" runat="server"  CssClass="form-control form-control-sm" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-2">
                        <div class="form-group">
                            <asp:Label ID="lbltLantime2" runat="server" CssClass="lblTxt lblName">Launch OutTime</asp:Label>
                            <asp:DropDownList ID="ddlLanouttimedw" runat="server"  CssClass="form-control form-control-sm" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-lg-1">
                        <div class="form-group">
                            <asp:LinkButton ID="lnkbtnUpdateOfftime" runat="server" CssClass="btn btn-success btn-sm mt20"
                                OnClick="lnkbtnUpdateOfftime_Click"
                             >Update</asp:LinkButton>
                        </div>
                    </div>


                    </div>


                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>

</asp:Content>



