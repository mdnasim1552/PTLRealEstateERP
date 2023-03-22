<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CRMDashboard03.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.CRMDashboard03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Content/crm-new-dashboard.css" rel="stylesheet" />--%>
    <script>
        function submitform() {
            document.getElementById("form1").submit();
        }
        var skin = localStorage.getItem("skin") || "default";
        var unusedLink = document.querySelector(
            'link[data-skin]:not([data-skin="' + skin + '"])'
        );
        unusedLink.setAttribute("rel", "");
        unusedLink.setAttribute("disabled", true);

        // setTimeout(function () { manipulate(); }, 3000);
        //  manipulate();
        function manipulate() {
            var itemname = localStorage.getItem("itemname");
            // alert(itemname);
            $("." + itemname).addClass("has-open");
            if (itemname == "") {
                return;
            }
        }
        function SetItemStorage(itemname) {
            var olitemname = localStorage.getItem("itemname");
            console.log(olitemname);
            if (olitemname == itemname) {
                itemname = "";
            }
            localStorage.removeItem("itemname");
            localStorage.setItem("itemname", itemname);
        }
        function openNav() {
            document.getElementById("mySidenav").style.width = "250px";
            document.getElementById("main").style.marginLeft = "250px";
            $(".menu-text").show();
            $(".closebtn").show();
            $("#the-basics").attr("placeholder", "Search");
            //$(".stacked-menu-has-collapsible .has-child>.menu-link:after").addClass("addDownArrow");
        }

        function closeNav() {
            document.getElementById("mySidenav").style.width = "75px";
            document.getElementById("main").style.marginLeft = "75px";
            console.log(document.querySelectorAll(".menu-item.has-child"));
            var menulist = document.querySelectorAll(".menu-item.has-child");
            menulist.forEach(function (item, index) {
                if (item.classList.contains("has-open")) {
                    var openclass = document.querySelector(".has-open");
                    openclass.classList.remove("has-open");
                }
            });

            $(".menu-text").css("display", "none");
            console.log($(".menu-text"));
            //$(".stacked-menu-has-collapsible .has-child>.menu-link:after").removeClass("removeDownArrow");

            $("#the-basics").attr("placeholder", "");
            //  $("li").removeClass("has-open");
        }

        function IsNumberWithOneDecimal(txt, evt) {
            var charCode = evt.which ? evt.which : event.keyCode;
            if (
                charCode > 31 &&
                (charCode < 48 || charCode > 57) &&
                !(charCode == 46 || charCode == 8)
            ) {
                return false;
            } else {
                var len = txt.value.length;
                var index = txt.value.indexOf(".");
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    if (len + 1 - index > 3) {
                        return false;
                    }
                }
            }
            return true;
        }
        function GetEmployeeform() {
            $("#EmployeeEntry").modal("toggle");
        }
        function CloseModal() {
            $("#EmployeeEntry").modal("hide");
        }
    </script>
    <!-- END THEME STYLES -->
    <style>
        .ajax__calendar_day {
            font-weight: 100 !important;
        }

        .ajax__calendar_dayname {
            font-weight: 100 !important;
        }

        .checkbox-menu li label {
            display: block;
            padding: 3px 10px;
            clear: both;
            font-weight: normal;
            line-height: 1.42857143;
            color: #333;
            white-space: nowrap;
            margin: 0;
            transition: background-color 0.4s ease;
        }

        .checkbox-menu li input {
            margin: 0px 5px;
            top: 2px;
            position: relative;
        }

        .checkbox-menu li.active label {
            background-color: #cbcbff;
            font-weight: bold;
        }

        .checkbox-menu li label:hover,
        .checkbox-menu li label:focus {
            background-color: #f5f5f5;
        }

        .checkbox-menu li.active label:hover,
        .checkbox-menu li.active label:focus {
            background-color: #b8b8ff;
        }

        .sidenav {
            height: 100%;
            width: 75px;
            position: fixed;
            z-index: 1;
            top: 0;
            left: 0;
            overflow-x: hidden;
            transition: 0.5s;
            padding-top: 60px;
        }

        #nav-bar {
            z-index: 1000;
        }

        #nav-bar-open {
            display: flex;
            justify-content: flex-start;
            align-items: center;
        }

        #main {
            transition: margin-left 0.5s;
            padding: 16px;
        }

        @media screen and (max-height: 450px) {
            .sidenav {
                padding-top: 15px;
            }

                .sidenav a {
                    font-size: 18px;
                }
        }

        .ajax__calendar_dayname {
            padding: 0 !important;
        }

        .app-header-dark .btn-account.focus,
        .app-header-dark .btn-account.show,
        .app-header-dark .btn-account:active,
        .app-header-dark .btn-account:focus,
        .app-header-dark .top-bar-brand {
            background-color: none;
        }

        .ps {
            overflow-x: hidden !important;
        }

        .top-bar-brand {
            margin-top: 8px !important;
        }

        .app-header-dark .top-bar-brand {
            background: none;
        }

        .chzn-container-single .chzn-single {
            height: 35px;
            line-height: 35px;
        }

        .GridViewScrollHeader TH,
        .GridViewScrollHeader TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #f4f4f4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }

        .GridViewScrollItem TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #ffffff;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #fafafa;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #f4f4f4;
            color: #444444;
        }

        #pnlTopMenu .nav-link {
            color: #fff !important;
            font-size: 18px;
        }

        .card-header {
            font-weight: normal !important;
        }

        .ajax__calendar {
            font-weight: normal !important;
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
                                        <div class="form-group mb-0 mw-170px">
                                            <label for="form-date-range" class="form-label">
                                                Date</label>
                                            <select class="form-select" id="form-date-range">
                                                <option>Today</option>
                                                <option>Yesterday</option>
                                                <option>Last 7 Days</option>
                                                <option>This Month</option>
                                                <option>Last Month</option>
                                                <option>This Year</option>
                                                <option>last Year</option>
                                                <option>Custom</option>
                                            </select>
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
                                            <button
                                                class="form-select-group-prepend btn btn-primary">
                                                Apply
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5 align-self-end">
                                <div class="d-flex justify-content-end">
                                    <button class="mmbd-btn mmbd-btn-primary">
                                        <img
                                            src="../assets/new-ui/images/equalizer.svg"
                                            alt=""
                                            class="img-responsive" />
                                        <strong>All Leads</strong>
                                        <span>(Interface)</span>
                                    </button>
                                    <button class="mmbd-btn mmbd-btn-primary-outline ml-3">
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
                                            <div class="card-title">100</div>
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
                                            <div class="card-title">50</div>
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
                                            <div class="card-title">40</div>
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
                                            <div class="card-title">30</div>
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
                                            <div class="card-title">30</div>
                                            <div class="card-subtitle">Sold</div>
                                        </div>
                                    </div>
                                </div>
                                <!-- END -->
                                <div class="col-6 col-lg-3 mb-3">
                                    <div class="card-color h-100">
                                        <div class="card-body card-light-primary">
                                            <div class="card-title">20</div>
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
                                            <div class="card-title">20</div>
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
                                            <div class="card-title">10</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">100</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                            <div class="symbol-label">30</div>
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
                                                <table class="table table-dashed">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
