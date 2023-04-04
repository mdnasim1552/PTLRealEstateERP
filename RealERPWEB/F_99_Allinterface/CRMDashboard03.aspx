<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CRMDashboard03.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.CRMDashboard03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/crm-new-dashboard.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            //var date = new Date();

            //var day = ("0" + date.getDate()).slice(-2);
            //var month = ("0" + (date.getMonth() + 1)).slice(-2);

            //var today = date.getFullYear() + "-" + (month) + "-" + (day);

            //$('#TxtFdate').val(today);
            //$('#TxtTdate').val(today);
            $(document).on("change", "#DdlDateType", function () {
                // $("#DdlDateType").change(function () {
                var status = this.value;
                // alert(status);
                if (status == "7") {
                    $("#exampleModalSm").modal("toggle");
                }

            });
            $('.po-markup > .po-link').popover({
                sanitize: false,
                html: true,
                trigger: 'click',
                // must have if HTML is contained in popover

                // get the title and conent
                title: function () {
                    return $(this).parent().find('.po-title').html();
                },
                content: function () {
                    return $(this).parent().find('.po-body').html();
                },

                container: 'body',
                placement: 'bottom'

            });
        });
        function pageLoaded() {
            try {

                $("#btnInterface").click(function () {
                    //specify your URL here..
                    window.location.href = '../F_21_MKT/CrmClientInfo02?Type=Entry';
                });
                $("#btnSalesFunnel").click(function () {
                    window.location.href = '../F_21_Mkt/RptSalesFunnel';
                });

            }
            catch (e) {

            }
        };
    </script>
    <style>
        .popover {
            min-width: 27%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="wrapper">
                <div class="page mt-4">
                    <div class="page">
                        <div class="row">
                            <div class="col-md-7">
                                <div class="row">
                                    <div class="col-4">




                                        <div class="form-group mb-0 mw-170px  po-markup">
                                            <label for="form-date-range" class="form-label">
                                                Date
                                            </label>

                                            <asp:DropDownList ID="DdlDateType" runat="server" ClientIDMode="Static" class="form-select">
                                                <asp:ListItem Value="0">Today</asp:ListItem>
                                                <asp:ListItem Value="1">Yesterday</asp:ListItem>
                                                <asp:ListItem Value="2">Last 7 Days</asp:ListItem>
                                                <asp:ListItem Value="3">This Month</asp:ListItem>
                                                <asp:ListItem Value="4">Last Month</asp:ListItem>
                                                <asp:ListItem Value="5">This Year</asp:ListItem>
                                                <asp:ListItem Value="6">last Year</asp:ListItem>
                                                <asp:ListItem Value="7">Custom</asp:ListItem>

                                            </asp:DropDownList>

                                            <a href="#" class="po-link pull-right"></a>
                                            <div class="po-content d-none">
                                                <div class="po-title">
                                                    Select Date 
                   
                                                </div>
                                                <!-- ./po-title -->

                                                <div class="po-body">
                                                    <div class="form-group row">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label1" runat="server">From</asp:Label>
                                                            <input type="date" id="TxtFdate" runat="server" class="form-control">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label2" runat="server">To</asp:Label>
                                                            <input type="date" id="TxtTdate" runat="server" class="form-control">
                                                        </div>

                                                        <div class="col-md-12">
                                                            <br />
                                                            <button type="button" onclick='$(".po-link").trigger("click")' style="float: right;" class="btn btn-primary btn-sm">SET</button>
                                                        </div>

                                                    </div>
                                                </div>
                                                <!-- ./po-body -->
                                            </div>
                                            <!-- ./po-content -->

                                            <%-- <select class="form-select" id="form-date-range">
                                                <option>Today</option>
                                                <option>Yesterday</option>
                                                <option>Last 7 Days</option>
                                                <option>This Month</option>
                                                <option>Last Month</option>
                                                <option>This Year</option>
                                                <option>last Year</option>
                                                <option>Custom</option>
                                            </select>--%>
                                        </div>
                                    </div>
                                    <div class="col-8">
                                        <label for="form-name" class="form-label">
                                            Username</label>
                                        <div class="form-select-group">
                                            <select class="form-select" id="form-name">
                                                <option>Select</option>
                                                <option>Nayem</option>
                                                <option>Forid</option>
                                            </select>
                                            <asp:LinkButton ID="LbtnOk" OnClick="LbtnOk_Click" runat="server" CssClass="form-select-group-prepend btn btn-primary">
                                                Apply
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5 align-self-end">
                                <div class="d-flex justify-content-end">
                                    <button class="mmbd-btn mmbd-btn-primary" id="btnInterface">
                                        <img
                                            src="../assets/new-ui/images/equalizer.svg"
                                            alt="CRM Interface"
                                            class="img-responsive" />
                                        <strong>All Leads</strong>
                                        <span>(Interface)</span>
                                    </button>
                                    <button class="mmbd-btn mmbd-btn-primary-outline ml-3" id="btnSalesFunnel">
                                        Sales Funnel
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- END HEAD -->
                        <div class="statistic mt-3">
                            <div class="row">
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-warning">
                                            <div class="card-title" id="Widget_Query" runat="server"></div>
                                            <div class="card-subtitle">Query</div>
                                            <ul class="p-0 mb-0">
                                                <li class="d-flex">
                                                    <span>Query to Lead(20%)</span>
                                                    <span>-</span>
                                                    <span>10</span>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-purple">
                                            <div class="card-title" id="Widget_Lead" runat="server"></div>
                                            <div class="card-subtitle">Lead</div>
                                            <ul class="p-0 mb-0">
                                                <li class="d-flex">
                                                    <span>Query to Close(15%)</span>
                                                    <span>-</span>
                                                    <span>10</span>
                                                </li>
                                                <li class="d-flex">
                                                    <span>Query to Hold(25%)</span>
                                                    <span>-</span>
                                                    <span>50</span>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-primary">
                                            <div class="card-title" id="Widget_QLead" runat="server"></div>
                                            <div class="card-subtitle">Qualified Lead</div>
                                            <ul class="p-0 mb-0">
                                                <li class="d-flex">
                                                    <span>Query to Close(15%)</span>
                                                    <span>-</span>
                                                    <span>10</span>
                                                </li>
                                                <li class="d-flex">
                                                    <span>Query to Hold(25%)</span>
                                                    <span>-</span>
                                                    <span>50</span>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-success">
                                            <div class="card-title" id="Widget_Nego" runat="server"></div>
                                            <div class="card-subtitle">Negotiation</div>
                                            <ul class="p-0 mb-0">
                                                <li class="d-flex">
                                                    <span>Query to Close(15%)</span>
                                                    <span>-</span>
                                                    <span>10</span>
                                                </li>
                                                <li class="d-flex">
                                                    <span>Query to Hold(25%)</span>
                                                    <span>-</span>
                                                    <span>50</span>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-success">
                                            <div class="card-title" id="Widget_Sold" runat="server"></div>
                                            <div class="card-subtitle">Sold</div>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-primary">
                                            <div class="card-title" id="Widget_Hold" runat="server"></div>
                                            <div class="card-subtitle">Hold</div>
                                            <ul class="p-0 mb-0">
                                                <li class="d-flex">
                                                    <span>Query to Close(15%)</span>
                                                    <span>-</span>
                                                    <span>10</span>
                                                </li>
                                                <li class="d-flex">
                                                    <span>Query to Hold(25%)</span>
                                                    <span>-</span>
                                                    <span>50</span>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-purple">
                                            <div class="card-title" id="Widget_close" runat="server"></div>
                                            <div class="card-subtitle">Close</div>
                                            <ul class="p-0 mb-0">
                                                <li class="d-flex">
                                                    <span>Query to Close(15%)</span>
                                                    <span>-</span>
                                                    <span>10</span>
                                                </li>
                                                <li class="d-flex">
                                                    <span>Query to Hold(25%)</span>
                                                    <span>-</span>
                                                    <span>50</span>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-danger">
                                            <div class="card-title" id="Widget_lost" runat="server">10</div>
                                            <div class="card-subtitle">Lost</div>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                            </div>
                        </div>
                        <!-- END STATICS -->

                        <div class="my-4">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-flash">
                                        <div class="card-header">
                                            <div class="card-title">To-Do List</div>
                                        </div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoScheduleWOrk" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Schedule Work</div>
                                                            <div class="list-para">Updated in 2 Days</div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoDailyWorkReport" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">
                                                                Daily Work Report
                                                            </div>
                                                            <div class="list-para">Updated in 2 Days</div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoTodayVisit" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Today’s Visit</div>
                                                            <div class="list-para">Updated in 2 Days</div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoMissedCall" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Missed Call</div>
                                                            <div class="list-para danger">
                                                                Updated in 2 Days
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoTodayTask" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Today Task</div>
                                                            <div class="list-para">Updated in 2 Days</div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoTodayWorkLog" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Work Log</div>
                                                            <div class="list-para">Today’s 10</div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoMissedMeetExt" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">
                                                                Missed Meeting External
                                                            </div>
                                                            <div class="list-para danger">
                                                                Number of meetings 10
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoMissedVisit" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Missed Visit</div>
                                                            <div class="list-para danger">
                                                                Number of visits 10
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoTodayCall" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Today’s Call</div>
                                                            <div class="list-para">
                                                                Number of Calls 10
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoTodayMeeting" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Today’s Meeting</div>
                                                            <div class="list-para">
                                                                Number of meetings 10
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoMissedMeetInt" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">
                                                                Missed Meeting Internal
                                                            </div>
                                                            <div class="list-para danger">
                                                                Number of meetings 10
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-lg-3">
                                                    <div class="list list1 d-flex align-items-center">
                                                        <div class="line"></div>
                                                        <a
                                                            href="#"
                                                            class="symbol symbol-60px symbol-light">
                                                            <div class="symbol-label" id="TodoTotalMissedFlowup" runat="server"></div>
                                                        </a>
                                                        <a href="#" class="list-body">
                                                            <div class="list-title">Missed Followup</div>
                                                            <div class="list-para danger">
                                                                Number of followup 10
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END -->
                        <div class="mb-4">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="card-flash h-100 px-4 pt-3 pb-4">
                                        <div class="card-header">
                                            <div class="card-title">
                                                Source Wise Summary (Marketing)
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="dropdown-panel">
                                                <div class="form-group-1">
                                                    <div class="form-select-group">
                                                        <select class="form-select" id="form-name">
                                                            <option>Cold Call to MKT</option>
                                                            <option>Nayem</option>
                                                            <option>Forid</option>
                                                        </select>
                                                        <button
                                                            class="form-select-group-prepend btn btn-primary">
                                                            Apply
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="card-flash card-light">
                                                <div class="card-header">
                                                    <div class="card-title">Cold Call To MKT</div>
                                                </div>
                                                <div class="card-body">
                                                    <div class="progress-1">
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Query</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-warning">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 35%">
                                                                                <p class="progress-percent">35%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">100</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Lost</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 35%">
                                                                                <p class="progress-percent">35%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">100</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Lead</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-purple">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 20%">
                                                                                <p class="progress-percent">20%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">40</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Lead to Close
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 20%">
                                                                                <p class="progress-percent">20%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">40</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Q.Lead</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-primary">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Q.Lead to Close
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Negotiation
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-cheyenne">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Nego. to Close
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Total Sold</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Total Close
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Total Hold</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="card-flash h-100 px-4 pt-3 pb-4">
                                        <div class="card-header">
                                            <div class="card-title">
                                                Source Wise Summary (Sales)
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="dropdown-panel"></div>
                                            <div class="card-flash card-light">
                                                <div class="card-header">
                                                    <div class="card-title">Sales Reference</div>
                                                </div>
                                                <div class="card-body">
                                                    <div class="progress-1">
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Query</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-warning">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 35%">
                                                                                <p class="progress-percent">35%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">100</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Lost</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 35%">
                                                                                <p class="progress-percent">35%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">100</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Lead</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-purple">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 20%">
                                                                                <p class="progress-percent">20%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">40</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Lead to Close
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 20%">
                                                                                <p class="progress-percent">20%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">40</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Q.Lead</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-primary">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Q.Lead to Close
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Negotiation
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-cheyenne">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Nego. to Close
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Total Sold</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">
                                                                    Total Close
                                                                </div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                        <div class="row align-items-center">
                                                            <div class="col-4 py-0">
                                                                <div class="progress-label">Total Hold</div>
                                                            </div>
                                                            <div class="col-8 py-0">
                                                                <div class="d-flex align-items-center py-2">
                                                                    <div class="flex-grow-1">
                                                                        <div class="progress progress-light">
                                                                            <div
                                                                                class="progress-bar"
                                                                                style="width: 10%">
                                                                                <p class="progress-percent">10%</p>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="progress-end">10</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- END -->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="mb-4">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-flash h-100 px-4 pt-3 pb-4">
                                        <div class="card-header">
                                            <div class="card-title">Project Wise Summary</div>
                                            <div class="card-toolbar">
                                                <div class="filter">
                                                    <select class="form-control">
                                                        <option>All User</option>
                                                        <option>Nayem</option>
                                                        <option>Forid</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body pb-0">
                                            <div class="table-responsive">
                                                <table class="table table-dashed d-none">
                                                    <thead>
                                                        <tr>
                                                            <th scope="col" class="text-left">Project Name
                                                            </th>
                                                            <th scope="col" class="text-center bg-light">Inventory
                                                            </th>
                                                            <th scope="col" class="text-center bg-light">Unsold
                                                            </th>
                                                            <th scope="col" class="text-center">Query</th>
                                                            <th scope="col" class="text-center">Lead</th>
                                                            <th scope="col" class="text-center">Q.Lead
                                                            </th>
                                                            <th scope="col" class="text-center">Negotiation
                                                            </th>
                                                            <th scope="col" class="text-center">Hold</th>
                                                            <th scope="col" class="text-center">Close</th>
                                                            <th scope="col" class="text-center">Lost</th>
                                                            <th scope="col" class="text-right w-70px">ACTION
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div
                                                                        class="symbol symbol-circle symbol-50px overflow-hidden mr-3">
                                                                        <a href="#">
                                                                            <div class="symbol-label">
                                                                                <img
                                                                                    src="../assets/new-ui/images/office-building.png"
                                                                                    alt=""
                                                                                    class="img-responsive" />
                                                                            </div>
                                                                        </a>
                                                                    </div>
                                                                    <div class="d-flex flex-column">
                                                                        <a class="fs-3" href="#">Edison Amour, Japan Street
                                          Bashundhara 154</a>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center bg-light">100</td>
                                                            <td class="text-center bg-light">30</td>
                                                            <td class="text-center">15</td>
                                                            <td class="text-center">23</td>
                                                            <td class="text-center">21</td>
                                                            <td class="text-center">24</td>
                                                            <td class="text-center">33</td>
                                                            <td class="text-center">66</td>
                                                            <td class="text-center">10</td>
                                                            <td class="text-right">
                                                                <button class="btn btn-light">View</button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div
                                                                        class="symbol symbol-circle symbol-50px overflow-hidden mr-3">
                                                                        <a href="#">
                                                                            <div class="symbol-label">
                                                                                <img
                                                                                    src="../assets/new-ui/images/office-building.png"
                                                                                    alt=""
                                                                                    class="img-responsive" />
                                                                            </div>
                                                                        </a>
                                                                    </div>
                                                                    <div class="d-flex flex-column">
                                                                        <a class="fs-3" href="#">Edison Amour, Japan Street
                                          Bashundhara 154</a>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center bg-light">100</td>
                                                            <td class="text-center bg-light">30</td>
                                                            <td class="text-center">15</td>
                                                            <td class="text-center">23</td>
                                                            <td class="text-center">21</td>
                                                            <td class="text-center">24</td>
                                                            <td class="text-center">33</td>
                                                            <td class="text-center">66</td>
                                                            <td class="text-center">10</td>
                                                            <td class="text-right">
                                                                <button class="btn btn-light">View</button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div
                                                                        class="symbol symbol-circle symbol-50px overflow-hidden mr-3">
                                                                        <a href="#">
                                                                            <div class="symbol-label">
                                                                                <img
                                                                                    src="../assets/new-ui/images/office-building.png"
                                                                                    alt=""
                                                                                    class="img-responsive" />
                                                                            </div>
                                                                        </a>
                                                                    </div>
                                                                    <div class="d-flex flex-column">
                                                                        <a class="fs-3" href="#">Edison Amour, Japan Street
                                          Bashundhara 154</a>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center bg-light">100</td>
                                                            <td class="text-center bg-light">30</td>
                                                            <td class="text-center">15</td>
                                                            <td class="text-center">23</td>
                                                            <td class="text-center">21</td>
                                                            <td class="text-center">24</td>
                                                            <td class="text-center">33</td>
                                                            <td class="text-center">66</td>
                                                            <td class="text-center">10</td>
                                                            <td class="text-right">
                                                                <button class="btn btn-light">View</button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div
                                                                        class="symbol symbol-circle symbol-50px overflow-hidden mr-3">
                                                                        <a href="#">
                                                                            <div class="symbol-label">
                                                                                <img
                                                                                    src="../assets/new-ui/images/office-building.png"
                                                                                    alt=""
                                                                                    class="img-responsive" />
                                                                            </div>
                                                                        </a>
                                                                    </div>
                                                                    <div class="d-flex flex-column">
                                                                        <a class="fs-3" href="#">Edison Amour, Japan Street
                                          Bashundhara 154</a>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center bg-light">100</td>
                                                            <td class="text-center bg-light">30</td>
                                                            <td class="text-center">15</td>
                                                            <td class="text-center">23</td>
                                                            <td class="text-center">21</td>
                                                            <td class="text-center">24</td>
                                                            <td class="text-center">33</td>
                                                            <td class="text-center">66</td>
                                                            <td class="text-center">10</td>
                                                            <td class="text-right">
                                                                <button class="btn btn-light">View</button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="d-flex align-items-center">
                                                                    <div
                                                                        class="symbol symbol-circle symbol-50px overflow-hidden mr-3">
                                                                        <a href="#">
                                                                            <div class="symbol-label">
                                                                                <img
                                                                                    src="../assets/new-ui/images/office-building.png"
                                                                                    alt=""
                                                                                    class="img-responsive" />
                                                                            </div>
                                                                        </a>
                                                                    </div>
                                                                    <div class="d-flex flex-column">
                                                                        <a class="fs-3" href="#">Edison Amour, Japan Street
                                          Bashundhara 154</a>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td class="text-center bg-light">100</td>
                                                            <td class="text-center bg-light">30</td>
                                                            <td class="text-center">15</td>
                                                            <td class="text-center">23</td>
                                                            <td class="text-center">21</td>
                                                            <td class="text-center">24</td>
                                                            <td class="text-center">33</td>
                                                            <td class="text-center">66</td>
                                                            <td class="text-center">10</td>
                                                            <td class="text-right">
                                                                <button class="btn btn-light">View</button>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                                <asp:GridView BorderWidth="0" ID="GvPrjsum" AutoGenerateColumns="False"
                                                    CssClass="table table-dashed" runat="server" AllowPaging="true" PageSize="5" OnPageIndexChanging="GvPrjsum_PageIndexChanging">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-CssClass="text-left" HeaderText="Project Name">
                                                            <ItemTemplate>
                                                                <div class="d-flex align-items-center">
                                                                    <div
                                                                        class="symbol symbol-circle symbol-50px overflow-hidden mr-3">
                                                                        <a href="#">
                                                                            <div class="symbol-label">
                                                                                <img
                                                                                    src="../assets/new-ui/images/office-building.png"
                                                                                    alt=""
                                                                                    class="img-responsive" />
                                                                            </div>
                                                                        </a>
                                                                    </div>
                                                                    <div class="d-flex flex-column">
                                                                         <a class="fs-3" href="#"><%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjdesc")) %></a>
                                                                    </div>
                                                                </div>

                                                             
                                                            </ItemTemplate>
                                                             
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ControlStyle-CssClass="text-center bg-light" HeaderText="Inventory">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjinv" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invtry")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                              <ItemStyle  CssClass="text-center bg-light" />
                                                              <HeaderStyle CssClass="text-center bg-light" />

                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-center bg-light" HeaderText="Unsold">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjunsold"  runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unsold")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                              <ItemStyle CssClass="text-center bg-light" />
                                                              <HeaderStyle CssClass="text-center bg-light" />

                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-center" HeaderText="Query">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjquery" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "query")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                              <ItemStyle CssClass="text-center" />
                                                              <HeaderStyle CssClass="text-center" />

                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-center " HeaderText="Lead">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjLead" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lead")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                                <ItemStyle CssClass="text-center" />
                                                              <HeaderStyle CssClass="text-center" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-center" HeaderText="Q.Lead">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjQLead" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qualiflead")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                                <ItemStyle CssClass="text-center" />
                                                              <HeaderStyle CssClass="text-center" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-center" HeaderText="Negotiation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjNego" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nego")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                                <ItemStyle CssClass="text-center" />
                                                              <HeaderStyle CssClass="text-center" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-center" HeaderText="Hold">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjHold" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hold")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                                <ItemStyle CssClass="text-center" />
                                                              <HeaderStyle CssClass="text-center" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-center" HeaderText="Close">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjClose" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lclose")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                                <ItemStyle CssClass="text-center" />
                                                              <HeaderStyle CssClass="text-center" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-center" HeaderText="Lost">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvprjLost" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lost")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                                <ItemStyle CssClass="text-center" />
                                                              <HeaderStyle CssClass="text-center" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField ControlStyle-CssClass="text-right " HeaderText="Action">
                                                            <ItemTemplate>
                                                               <button class="btn btn-light">View</button>
                                                            </ItemTemplate>
                                                                <ItemStyle CssClass="text-right" />
                                                              <HeaderStyle CssClass="text-right" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="table-pagination" />
                                                </asp:GridView>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4">
                                                    <div class="d-flex align-items-center">
                                                        <div class="length">
                                                            <select
                                                                class="form-control form-control-sm mb-0">
                                                                <option value="5">5</option>
                                                                <option value="25">25</option>
                                                                <option value="50">50</option>
                                                                <option value="100">100</option>
                                                            </select>
                                                        </div>
                                                        <div class="info ml-2">
                                                            Showing 1 to 19 of 19 records
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <nav class="table-pagination">
                                                        <ul class="pagination">
                                                            <li class="page-item mr-3">
                                                                <a class="page-link previews" href="#"><i class="fas fa-arrow-left"></i>
                                                                    Previous</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link active" href="#">1</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">2</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">3</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link">...</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">8</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">9</a>
                                                            </li>
                                                            <li class="page-item">
                                                                <a class="page-link" href="#">10</a>
                                                            </li>
                                                            <li class="page-item ml-3">
                                                                <a class="page-link next" href="#">Next<i class="fas fa-arrow-right"></i></a>
                                                            </li>
                                                        </ul>
                                                    </nav>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END -->
                        <div class="mb-4">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-flash px-4 pt-3 pb-4">
                                        <div class="card-header">
                                            <div class="d-flex card-icon">
                                                <div
                                                    class="symbol symbol-80px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label">
                                                            <img
                                                                src="../assets/new-ui/images/key-icon.png"
                                                                alt=""
                                                                class="img-responsive" />
                                                        </div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <p class="title mb-0">
                                                        Key Performance Indicator
                                                    </p>
                                                    <p class="subtitle mb-0">Today: 26 Feb 2023</p>
                                                    <p class="text-muted mb-0">Due: 27 Apr 2023</p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body pb-0">
                                            <div class="mb-3">
                                                <h4>Progress: 80%</h4>
                                                <div class="progress-1">
                                                    <div class="progress progress-cheyenne">
                                                        <div class="progress-bar" style="width: 80%">
                                                            <!-- <p class="progress-percent">10%</p> -->
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-6 col-md-4 col-xl-3 mb-3">
                                                    <h4 class="ml-4 card-text-title">Revenue</h4>
                                                    <div class="d-flex">
                                                        <div class="progress-1 progress-vertical mr-3">
                                                            <div class="progress progress-danger">
                                                                <div
                                                                    class="progress-bar"
                                                                    style="height: 10%">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-text card-sm flex-grow-1">
                                                            <div class="card-body card-text-danger">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-between">
                                                                    <div class="card-title mb-0">100</div>
                                                                    <div class="d-flex align-items-center">
                                                                        <div>
                                                                            <img
                                                                                src="assets/new-ui/images/transfer.png"
                                                                                alt="" />
                                                                        </div>
                                                                        <p class="progress-percent mb-0 ml-2">
                                                                            10%
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                                <div class="card-subtitle mb-2">
                                                                    Achievement
                                                                </div>
                                                                <p class="mb-0">Target-1,00,000</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-xl-3 mb-3">
                                                    <h4 class="card-text-title ml-4">No. Of Appt.</h4>
                                                    <div class="d-flex">
                                                        <div class="progress-1 progress-vertical mr-3">
                                                            <div class="progress progress-cheyenne">
                                                                <div
                                                                    class="progress-bar"
                                                                    style="height: 50%">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-text card-sm flex-grow-1">
                                                            <div class="card-body card-text-cheyenne">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-between">
                                                                    <div class="card-title mb-0">50,000</div>
                                                                    <div class="d-flex align-items-center">
                                                                        <div>
                                                                            <img
                                                                                src="assets/new-ui/images/transfer.png"
                                                                                alt="" />
                                                                        </div>
                                                                        <p class="progress-percent mb-0 ml-2">
                                                                            50%
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                                <div class="card-subtitle mb-2">
                                                                    Achievement
                                                                </div>
                                                                <p class="mb-0">Target-1,00,000</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-xl-3 mb-3">
                                                    <h4 class="card-text-title ml-4">Lead to Q.Lead Ratio
                                                    </h4>
                                                    <div class="d-flex">
                                                        <div class="progress-1 progress-vertical mr-3">
                                                            <div class="progress progress-cheyenne">
                                                                <div
                                                                    class="progress-bar"
                                                                    style="height: 90%">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-text card-sm flex-grow-1">
                                                            <div class="card-body card-text-cheyenne">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-between">
                                                                    <div class="card-title mb-0">90,000</div>
                                                                    <div class="d-flex align-items-center">
                                                                        <div>
                                                                            <img
                                                                                src="assets/new-ui/images/transfer.png"
                                                                                alt="" />
                                                                        </div>
                                                                        <p class="progress-percent mb-0 ml-2">
                                                                            90%
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                                <div class="card-subtitle mb-2">
                                                                    Achievement
                                                                </div>
                                                                <p class="mb-0">Target-1,00,000</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-xl-3 mb-3">
                                                    <h4 class="card-text-title ml-4">No. Of new Customer Visit
                                                    </h4>
                                                    <div class="d-flex">
                                                        <div class="progress-1 progress-vertical mr-3">
                                                            <div class="progress progress-cheyenne">
                                                                <div
                                                                    class="progress-bar"
                                                                    style="height: 77%">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-text card-sm flex-grow-1">
                                                            <div class="card-body card-text-cheyenne">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-between">
                                                                    <div class="card-title mb-0">77,000</div>
                                                                    <div class="d-flex align-items-center">
                                                                        <div>
                                                                            <img
                                                                                src="assets/new-ui/images/transfer.png"
                                                                                alt="" />
                                                                        </div>
                                                                        <p class="progress-percent mb-0 ml-2">
                                                                            77%
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                                <div class="card-subtitle mb-2">
                                                                    Achievement
                                                                </div>
                                                                <p class="mb-0">Target-1,00,000</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-xl-3 mb-3">
                                                    <h4 class="card-text-title ml-4">Revenue</h4>
                                                    <div class="d-flex">
                                                        <div class="progress-1 progress-vertical mr-3">
                                                            <div class="progress progress-cheyenne">
                                                                <div
                                                                    class="progress-bar"
                                                                    style="height: 50%">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-text card-sm flex-grow-1">
                                                            <div class="card-body card-text-cheyenne">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-between">
                                                                    <div class="card-title mb-0">50,000</div>
                                                                    <div class="d-flex align-items-center">
                                                                        <div>
                                                                            <img
                                                                                src="assets/new-ui/images/transfer.png"
                                                                                alt="" />
                                                                        </div>
                                                                        <p class="progress-percent mb-0 ml-2">
                                                                            50%
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                                <div class="card-subtitle mb-2">
                                                                    Achievement
                                                                </div>
                                                                <p class="mb-0">Target-1,00,000</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-xl-3 mb-3">
                                                    <h4 class="card-text-title ml-4">No. Of Appt.</h4>
                                                    <div class="d-flex">
                                                        <div class="progress-1 progress-vertical mr-3">
                                                            <div class="progress progress-warning">
                                                                <div
                                                                    class="progress-bar"
                                                                    style="height: 30%">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-text card-sm flex-grow-1">
                                                            <div class="card-body card-text-warning">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-between">
                                                                    <div class="card-title mb-0">30,000</div>
                                                                    <div class="d-flex align-items-center">
                                                                        <div>
                                                                            <img
                                                                                src="assets/new-ui/images/transfer.png"
                                                                                alt="" />
                                                                        </div>
                                                                        <p class="progress-percent mb-0 ml-2">
                                                                            30%
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                                <div class="card-subtitle mb-2">
                                                                    Achievement
                                                                </div>
                                                                <p class="mb-0">Target-1,00,000</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-xl-3 mb-3">
                                                    <h4 class="card-text-title ml-4">Lead to Q.Lead Ratio
                                                    </h4>
                                                    <div class="d-flex">
                                                        <div class="progress-1 progress-vertical mr-3">
                                                            <div class="progress progress-warning">
                                                                <div
                                                                    class="progress-bar"
                                                                    style="height: 5%">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-text card-sm flex-grow-1">
                                                            <div class="card-body card-text-warning">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-between">
                                                                    <div class="card-title mb-0">5,000</div>
                                                                    <div class="d-flex align-items-center">
                                                                        <div>
                                                                            <img
                                                                                src="assets/new-ui/images/transfer.png"
                                                                                alt="" />
                                                                        </div>
                                                                        <p class="progress-percent mb-0 ml-2">
                                                                            5%
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                                <div class="card-subtitle mb-2">
                                                                    Achievement
                                                                </div>
                                                                <p class="mb-0">Target-1,00,000</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                                <div class="col-6 col-md-4 col-xl-3 mb-3">
                                                    <h4 class="card-text-title ml-4">No. Of new Customer Visit
                                                    </h4>
                                                    <div class="d-flex">
                                                        <div class="progress-1 progress-vertical mr-3">
                                                            <div class="progress progress-cheyenne">
                                                                <div
                                                                    class="progress-bar"
                                                                    style="height: 52%">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-text card-sm flex-grow-1">
                                                            <div class="card-body card-text-cheyenne">
                                                                <div
                                                                    class="d-flex align-items-center justify-content-between">
                                                                    <div class="card-title mb-0">52,000</div>
                                                                    <div class="d-flex align-items-center">
                                                                        <div>
                                                                            <img
                                                                                src="assets/new-ui/images/transfer.png"
                                                                                alt="" />
                                                                        </div>
                                                                        <p class="progress-percent mb-0 ml-2">
                                                                            52%
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                                <div class="card-subtitle mb-2">
                                                                    Achievement
                                                                </div>
                                                                <p class="mb-0">Target-1,00,000</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.wrapper -->
            </div>
            <div id="exampleModalSm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-sm" role="document">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header">
                            <h5 class="modal-title">Chose Date Range </h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server">From</asp:Label>
                                        <asp:TextBox ID="txtfrmdate" autocomplete="off" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server">To</asp:Label>
                                        <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-warning" data-dismiss="modal">Set & Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
