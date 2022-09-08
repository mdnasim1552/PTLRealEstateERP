<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="RealERPWEB.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <!-- Latest compiled and minified CSS -->


    <script>
        
        function openEmpModal() {
            $('#LongTermModal').modal('toggle');
        }

        function OpenBirthDayDetModal() {
            $('#BirthDayDetModal').modal('toggle');
        }


        function openNoticeModal() {
            $('#NoticeModal').modal('toggle');
        }

        $(document).keyup(function (e) {
            if (e.keyCode == 44) return false;
        });

        $(document).ready(function () {
            ExcuteEmpStatus(); p
        });
        function ExcuteEmpStatus() {

            var present = this.parseFloat($("#<%=this.lblpresent.ClientID %>").val());
            var late = this.parseFloat($("#<%=this.lbllate.ClientID %>").val());

            var onleave = this.parseFloat($("#<%=this.lblonleave.ClientID %>").val());
            var abs = this.parseFloat($("#<%=this.lblabs.ClientID %>").val());



            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var data = google.visualization.arrayToDataTable([
                    ['Task', 'This Yearly Attendance status'],
                    ['Present', present],
                    ['Absent', abs],
                    ['Late', late],
                    ['On leave', onleave],


                ]);

                var options = {
                    title: 'Your Yearly Attendance status',
                    is3D: true,
                    pieSliceText: 'value'

                };

                var chart = new google.visualization.PieChart(document.getElementById('piechartEMPStatus'));

                chart.draw(data, options);
            }
        }


        function OpenPayslipModal() {

            $('#exampleModal').modal('toggle');
        }
        function closePayslipModal() {
            $('#exampleModal').modal('hide');
            location.reload();
        }
    </script>
    <style>
        #view_emp10 #view_emp15 #view_emp20 #view_emp25 {
            padding: 0 !important;
        }

        .accordion_two_section {
            background: #f7f7f7;
        }

        .panel-default {
            border-top: 1px solid #dddd !important;
        }

        .ptb-100 {
            padding-top: 100px;
            padding-bottom: 100px;
        }

        .accordionTwo .panel-group {
            margin-bottom: 0;
        }

        .accordionTwo .panel {
            background-color: transparent;
            box-shadow: none;
            border-bottom: 10px solid transparent;
            border-radius: 0;
            margin: 0;
        }

        .accordionTwo .panel-default {
            border: 0;
        }

            .accordionTwo .panel-default > .panel-heading {
                background: #3399cc;
                border-radius: 0px;
                border-color: #4385f5;
            }

        .accordion-wrap .panel-heading {
            padding: 0px;
            border-radius: 0px;
        }

        .panel-title {
            margin-top: 0;
            margin-bottom: 0;
            font-size: 16px;
            color: inherit;
        }

        .accordionTwo .panel .panel-heading a.collapsed {
            color: #999999;
            background-color: #fff;
            display: block;
            padding: 12px 20px;
        }

        .accordionTwo .panel .panel-heading a {
            display: block;
            padding: 12px 20px;
            color: #fff;
        }

        .accordion-wrap .panel .panel-heading a {
            font-size: 14px;
        }


        .accordionTwo .panel-group .panel-heading + .panel-collapse > .panel-body {
            border-top: 0;
            padding-top: 0;
            padding: 20px 20px 20px 30px;
            /*    background: #4385f5;
    color: #fff;*/
            font-size: 14px;
            line-height: 24px;
        }

        .accordionTwo .panel .panel-heading a:after {
            content: "\2212";
            color: #4285f4;
            background: #fff;
        }

        .accordionTwo .panel .panel-heading a:after, .accordionTwo .panel .panel-heading a.collapsed:after {
            font-family: 'FontAwesome';
            font-size: 14px;
            float: right;
            width: 21px;
            display: block;
            height: 21px;
            line-height: 21px;
            text-align: center;
            border-radius: 50%;
            color: #FFF;
        }

        .accordionTwo .panel .panel-heading a.collapsed:after {
            content: "\2b";
            color: #fff;
            background-color: #DADADA;
        }

        .accordionTwo .panel .panel-heading a:after {
            content: "\2212";
            color: #4285f4;
            background: #dadada;
        }

        a:link {
            text-decoration: none
        }

        .dbx {
            height: 60px;
            width: 100%;
            display: flex;
            justify-content: center;
            text-align: center;
            align-items: center;
        }

            .dbx a {
                text-decoration: none;
            }


        .noselect {
            -webkit-touch-callout: none; /* iOS Safari */
            -webkit-user-select: none; /* Safari */
            -moz-user-select: none; /* Old versions of Firefox */
            -ms-user-select: none; /* Internet Explorer/Edge */
            user-select: none; /* Non-prefixed version, currently
                                      supported by Chrome, Edge, Opera and Firefox */
        }

        @media print {

            #exampleModal {
                visibility: hidden;
            }
        }

        .topMenu li .nav-link {
            padding: 10px 10px;
        }

        .marquee {
            margin: 0 auto;
            white-space: nowrap;
            overflow: hidden;
            box-sizing: border-box;
        }

            .marquee > div {
                display: table-row;
                white-space: nowrap;
                padding-left: 100%;
                animation: marquee 30s linear infinite; /* Time must be adjusted based on total width of scrolled elements*/
            }

                .marquee > div p {
                    width: 100%; /* Width of p elements must match the width of marquee "window"*/
                    padding-left: 100%; /* Padding determines space between scrolled elements */
                    display: table-cell;
                    color: crimson;
                    font-size: 14px;
                }

        /* Make it move */
        @keyframes marquee {
            0% {
                transform: translate(0, 0);
            }

            100% {
                transform: translate(-100%, 0);
            }
        }


        .accordion-container {
            position: relative;
            height: auto;
            margin: 10px auto;
        }

            .accordion-container > h2 {
                text-align: center;
                color: #fff;
                padding-bottom: 5px;
                margin-bottom: 20px;
                padding-bottom: 15px;
                border-bottom: 1px solid #ddd;
            }

        .set {
            position: relative;
            width: 100%;
            height: auto;
            background-color: #f5f5f5;
        }

            .set > a {
                display: block;
                padding: 10px 15px;
                text-decoration: none;
                color: #555;
                font-weight: 600;
                border-bottom: 1px solid #ddd;
                -webkit-transition: all 0.2s linear;
                -moz-transition: all 0.2s linear;
                transition: all 0.2s linear;
            }

                .set > a i {
                    float: right;
                    margin-top: 2px;
                }

                .set > a.active {
                    background-color: #3399cc;
                    color: #fff;
                }

        .content {
            background-color: #fff;
            border-bottom: 1px solid #ddd;
            display: none;
        }

            .content p {
                padding: 10px 15px;
                margin: 0;
                color: #333;
            }

        .table td, .table th {
            padding: 3px;
        }
    </style>
    
        <!-- .page-cover -->
        <header class="Xpage-cover mt-4">
            <div class="row">

                <div class="col-12 py-0 pl-0 " id="EventNotice" runat="server" style="border: 1px solid #D6D8E1;">
                    <div class="row">
                        <!--Breaking box-->
                        <div class="col-md-2 col-lg-2 pr-md-0">
                            <div class="p-2 bg-primary text-white text-center breaking-caret"><span class="font-weight-bold">Notice/Events</span></div>
                        </div>

                        <div class="col-md-10 col-lg-10 pl-md-4 py-2">
                            <div class="breaking-box">
                                <div id="carouselbreaking" class="carousel slide" data-ride="carousel">

                                    <div class="marquee">
                                        <div id="EventCaro" runat="server"></div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-3">
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="text-center" style="height: 250px;">
                                <a href="UserProfile.aspx" class="user-avatar user-avatar-xl">
                                    <asp:Image ID="userimg" runat="server" ImageUrl="~/image/profile_img.png" />
                                </a>
                                <h2 class="h4 mt-2 mb-0" id="UserName" runat="server">Beni Arisandi </h2>

                                <p class="text-muted  mb-0" id="UDesignation" runat="server">Project Manager @CreativeDivision </p>
                                <p class="text-muted" id="UDptment" runat="server">Project Manager @UDptment </p>
                                <p class="text-muted" hidden="hidden" id="offiTime" runat="server">Office Time- 09:00-05:00</p>

                                <asp:HyperLink ID="hylnkUserProfileEdit" runat="server" NavigateUrl="~/F_81_Hrm/F_82_App/EmpProfileEdit.aspx" Target="_blank" ToolTip="Edit Your Profile"><i class="fas fa-user-edit">&nbsp;Edit</i></asp:HyperLink>
                            </div>

                        </div>
                    </div>


                </div>
                <div class="col-lg-4 col-md-4 col-sm-3">
                    <asp:Panel runat="server" ID="pnlUpcmEdison" visiable="false">

                        <section class="card card-fluid mb-0" style="width: 100%; height: 268px;">


                            <div class="card-body" id="Div1" runat="server">



                                <div class="row">

                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-green rounded border ">
                                            <a class="text-white" href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/MyLeave?Type=User")%>" target="_blank">Apply Leave</a>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-white rounded border"><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/AttnOutOfOffice")%>" target="_blank">Online Attendance</a></div>
                                    </div>

                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-pink rounded border">
                                            <asp:HyperLink CssClass="text-white" ID="hlnkattreport" class="text-white" runat="server" Target="_blank">
                                                     Attendance Report
                                            </asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-blue rounded border"><a class="text-white" href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HREmpMonthlyAtten")%>" target="_blank">Manual Attendance</a></div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-yeallow rounded border"><a class="text-dark" href="<%=this.ResolveUrl("~/UserProfile")%>" target="_blank">Profile</a></div>
                                    </div>

                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-success rounded border"><a class="text-white" data-toggle="modal" data-target="#payslipmodal">Pay Slip</a></div>
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-success rounded border"><a class="text-white" href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/AllEmpList?Type=Report&comcod=")%>" target="_blank">Employee Directory</a></div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-secondary text-dark rounded border">KPI</div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="dbx shadow-sm bg-blue text-dark rounded border">
                                            <a class="text-white" href='<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/InterfaceLeavApp?Type=Ind")%>' target="_blank">Leave Interface</a>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </section>
                    </asp:Panel>




                    <asp:Panel runat="server" ID="pnlUpcmBti">
                        <section class="card card-fluid mb-0" style="width: 100%; height: 268px;">
                            <header class="card-header border-0 pb-0">
                                <div class="d-flex align-items-center">
                                    <span class="mr-auto">Upcoming Holidays </span>
                                </div>
                            </header>
                            <div class="card-body" style="max-height: 200px; overflow-y: scroll" id="upComingHolidays" runat="server">
                            </div>



                        </section>
                    </asp:Panel>




                </div>
               
                    <div class="col-lg-4 col-md-4 col-sm-3">
                        <div class="card card-fluid">
                            <div class="card-body">

                                <div id="piechartEMPStatus" style="width: 100%; height: 250px;"></div>
                                <div class="d-none">
                                    <asp:TextBox ID="lblpresent" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="lbllate" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="lblonleave" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="lblabs" runat="server"></asp:TextBox>

                                </div>


                            </div>
                        </div>

                    </div>
                
            </div>


            <div class="row ">

                <div class="col-lg-12 col-md-12 col-sm-12">
                    <div class="card ">
                        <nav class="navbar navbar-expand-lg" id="page-navs">
                            <!-- .nav-scroller -->
                            <div class="card-body m-0 p-0">
                                <ul class=" navbar-nav topMenu">
                                    <li class="nav-item"><a class="nav-link active btn btn-primary" data-toggle="tab" href="#home">Activities</a> </li>

                                    <li class="nav-item"><a href="#Notice" class="nav-link smooth-scroll" data-toggle="tab">Notice</a></li>
                                    <li class="nav-item"><a href="#HolidayCalender" class="nav-link smooth-scroll" data-toggle="tab">Holiday Calender</a></li>

                                    <li class="nav-item"><a id="hrpolicy" runat="server" href="#LeavePolicy" class="nav-link smooth-scroll" data-toggle="tab">HR Policy</a></li>
                                    <li class="nav-item">
                                        <asp:HyperLink ID="lnkOrintation" CssClass="nav-link smooth-scrol" NavigateUrl="#" Target="_blank" runat="server">Orintation Link</asp:HyperLink>
                                    </li>

                                    <li class="nav-item" id="winsList" runat="server">
                                        <a href="#winsListData" class="nav-link smooth-scroll" data-toggle="tab">Wins List</a>
                                    </li>
                                    <li class="nav-item">
                                        <asp:HyperLink ID="HyperCodeofConduct" CssClass="nav-link smooth-scrol" Visible="true" data-toggle="tab" href="#CodeofConduct" runat="server">Code of Conduct</asp:HyperLink>

                                    <li class="nav-item">
                                        <asp:HyperLink ID="HypOrganogram" CssClass="nav-link smooth-scrol" Visible="true" NavigateUrl='#Organogram' data-toggle="tab" runat="server">Organogram</asp:HyperLink>

                                    </li>

                                    <li class="nav-item" id="List_EmpDirectory" runat="server">
                                        <asp:HyperLink ID="EmpDirectory" CssClass="nav-link smooth-scrol" NavigateUrl="#" Target="_blank" runat="server">Employee directory</asp:HyperLink>
                                    </li>
                                    <li class="nav-item" runat="server" id="modalPayslipBti">
                                        <a data-toggle="modal" data-target="#payslipmodal" class="nav-link smooth-scrol">Pay Slip</a></li>

                                    <li class="nav-item">
                                        <a href="#" class="nav-link smooth-scrol" data-toggle="modal" data-target="#followingModal">Change Profile Photo</a></li>
                                    <li class="nav-item d-none"><a href="#" class="nav-link smooth-scroll" data-toggle="modal" data-target="#fdollowingModal">Change Pasword</a></li>
                                    <li class="nav-item">
                                        <asp:LinkButton ID="hyplPreviewCv" CssClass=" btn btn-success btn-sm d-none" runat="server" OnClick="hyplPreviewCv_Click1"> View Profile <i class="fa fa-print "></i> </asp:LinkButton></li>


                                    <li class="nav-item"><a href="MyShortCutLink.aspx?Module=" class="btn btn-light">My Shortcut</a></li>

                                </ul>

                            </div>
                            <!-- /.nav-scroller -->
                        </nav>
                    </div>
                </div>
            </div>
        </header>
    <!-- /.page-cover -->

        <!-- .modal -->
        <div class="modal fade" id="followingModal" tabindex="-1" role="dialog" aria-labelledby="followingModalLabel" aria-hidden="true">
            <!-- .modal-dialog -->
            <div class="modal-dialog modal-dialog-scrollable" role="document">
                <!-- .modal-content -->
                <div class="modal-content">
                    <!-- .modal-header -->
                    <div class="modal-header">
                        <h6 id="followingModalLabel" class="modal-title"><span class="fa fa-user-tag"></span>Change Your Profile Picture </h6>
                    </div>
                    <!-- /.modal-header -->
                    <!-- .modal-body -->
                    <div class="modal-body px-0">
                        <div class="card-body">
                            <div id="dropzone" class="fileinput-dropzone">
                                <span>Drop files or click to upload.</span>
                                <!-- The file input field used as target for the file upload widget -->
                                <asp:FileUpload ID="fileuploaddropzone" runat="server"
                                    onchange="submitform();" />
                            </div>
                            <div id="progress" class="progress progress-xs rounded-0 fade">
                                <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal-body -->
                    <!-- .modal-footer -->
                    <div class="modal-footer">
                        <label>New Profile Picture Effective After Logout and login</label>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    <!-- /.modal-footer -->
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>


        <div class="page-section">
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane fade active show" id="home">
                    <!-- .section-block -->
                    <div class="row d-none">
                        <div class="section-block">
                            <div class="list-group-item d-flex justify-content-between align-items-center">
                                <span class="text-dark" id="UserName1" runat="server"></span>
                                <!-- .switcher-control -->

                                <label class="switcher-control switcher-control-lg" id="EventSTatus" runat="server">
                                </label>
                                <!-- /.switcher-control -->
                            </div>
                            <!-- metric row -->
                        </div>
                    </div>
                    <!-- /.section-block -->

                    <div class="row mt-2">
                        <div class="col-md-6">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="d-none " ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                            <div class="card card-fluid">

                                <div class="card-body pb-3">
                                    <div class="d-flex align-items-center mb-3">
                                        <h3 class="card-title mr-auto">ATTENDANCE HISTORY </h3>
                                        <!-- .card-title-control -->

                                        <!-- /.card-title-control -->
                                    </div>
                                    <div class="table-responsive">
                                        <asp:Repeater ID="RptAttHistroy" runat="server" OnItemDataBound="RptAttHistroy_ItemDataBound">
                                            <HeaderTemplate>
                                                <table class="table-striped table-hover table-bordered">
                                                    <tr>
                                                        <th>Month </th>
                                                        <th>Intime</th>
                                                        <th>Late </th>
                                                        <th>Absent </th>
                                                        <th>Leave </th>
                                                        <th>Leave Adjusted </th>
                                                        <th>Late Approval </th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 80px">
                                                        <asp:Label ID="lblymonid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ymonid")).ToString() %>'></asp:Label>

                                                        <asp:HyperLink ID="hlnkbtnadd" runat="server" Target="_blank" Text='<%# Eval("yearmon") %>'></asp:HyperLink>

                                                    </td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblacintimed" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acintime")).ToString("#, ##0;(#, ##0); ") %>'></asp:Label>
                                                    </td>

                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblLate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aclate")).ToString("#,##0.00;(#,##0.00); ")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblAbsent" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblLeave" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leave")).ToString("#,##0.00;(#,##0.00); ")%>'></asp:Label>
                                                    </td>

                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lbllvadj" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lvadj")).ToString("#,##0.00;(#,##0.00); ")%>'></asp:Label>
                                                    </td>


                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblrlateapp" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lateapp")).ToString("#,##0.00;(#,##0.00); ")%>'></asp:Label>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td style="width: 80px">
                                                        <asp:Label ID="ttl" runat="server" CssClass=" smLbl_to" Text="Total"></asp:Label></td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblacintime" runat="server" Style="text-align: center"></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lbltotallate" runat="server" Style="text-align: center"></asp:Label></td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lbltotalabs" runat="server" Style="text-align: center"></asp:Label>
                                                    </td>

                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lbltotalleave" runat="server" Style="text-align: center"></asp:Label></td>

                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lbltolvadj" runat="server" Style="text-align: center"></asp:Label></td>

                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblfrtolateapp" runat="server" Style="text-align: center"></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 80px">
                                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to" Text="In %"></asp:Label></td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblperIntime" runat="server" Style="text-align: center"></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblperLate" runat="server" Style="text-align: center"></asp:Label></td>
                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblPerabs" runat="server" Style="text-align: center"></asp:Label>
                                                    </td>

                                                    <td style="width: 80px; text-align: center !important;">
                                                        <asp:Label ID="lblperleave" runat="server" Style="text-align: center"></asp:Label></td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblfperlvadj" runat="server" Style="text-align: center"></asp:Label></td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblfrperlateapp" runat="server" Style="text-align: center"></asp:Label></td>

                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="col-md-6">
                            <!-- .card -->
                            <div class="card card-fluid">
                                <!-- .card-header -->
                                <div class="card-header border-0 pb-0">
                                    <!-- .d-flex -->
                                    <div class="d-flex align-items-center">
                                        <h3 class="mr-auto card-title">LEAVE HISTORY</h3>
                                        <!-- .card-header-control -->
                                        <asp:LinkButton ID="hlnkbtnNext" runat="server" CssClass="btn btn-sm btn-info primaryBtn pull-right" OnClick="hlnkbtnNext_Click" Text="View all"></asp:LinkButton>
                                    </div>
                                    <!-- /.d-flex -->
                                </div>
                                <!-- /.card-header -->
                                <!-- .table-responsive -->
                                <div class="card-body">
                                    <div class="table-responsive pb-3">
                                        <!-- .table -->
                                        <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="false" CssClass="table-striped table-hover table-bordered">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Desription">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblgvDescr" runat="server" Text='Desription'></asp:Label>

                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                            Width="120px"></asp:Label>


                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Entitlement">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enjoyed">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlentitled01" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last Leave End Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                                            Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Last Leave Day's">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                            </Columns>

                                            <EditRowStyle />
                                            <AlternatingRowStyle />

                                        </asp:GridView>
                                        <!-- /.table -->

                                    </div>
                                </div>
                                <!-- /.card-footer -->
                            </div>
                            <!-- /.card -->


                      <asp:Panel runat="server" ID="pnlApplyLeavBTI" Visible="false">
                          <div class="card">
                              <div class="card-header">
                                      <div class="d-flex align-items-center">
                                        <h3 class="mr-auto card-title">TIME OF LEAVE HISTORY</h3>
                                        <!-- .card-header-control -->
                                        
                                        <asp:HyperLink ID="lnkapplyleave" runat="server" CssClass="btn btn-sm btn-info primaryBtn pull-right" NavigateUrl="~/F_81_Hrm/F_84_Lea/TimeOfLeave?Type=Ind" Text="Apply" Target="_blank"></asp:HyperLink>
                                    </div>
                              </div>
                              <div class="card-body">
                                  
                            <asp:GridView ID="gvLvReqAll" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered"
                                                        ShowFooter="True" >
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2SlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="leaveIdd" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2id" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2empid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                                                                               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>                                                             
                                                           
                                                            <asp:TemplateField HeaderText="Apply Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvapplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Intime Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2intime" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "intime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                   



                                                                </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Out Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvouttime" runat="server" BackColor="Transparent"
                                                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "outtime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                       

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Use Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv2duration" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "USETIME")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotalremtime" runat="server" CssClass="badge bg-danger text-white"></asp:Label>

                                                                   
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remaning Time" ControlStyle-BackColor="#ccffcc">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvremtime" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remaintime")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Reason/Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReason"  ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>' runat="server" 
                                                                        Height="20px" Text='<%# Eval("remarks").ToString().Length>50 ? (Eval("remarks") as string).Substring(0,25)+ "....."
                                                                             : Eval("remarks")  %>'></asp:Label>

                                                                                                                                
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                           
                                     
                                                        </Columns>
                                                        
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        
                                                    </asp:GridView>  
                              </div>
                          </div>

                      </asp:Panel>

                        </div>
                    </div>


                    <div class="row">
                        <!-- grid column -->
                        <div class="col-xl-6">
                            <div class="card card-fluid">
                                <div class="card-header border-0">
                                    <!-- .d-flex -->
                                    <div class="d-flex align-items-center">

                                        <h3 class="mr-auto card-title">JOB RESPONSIBILITIES</h3>
                                        <!-- .card-header-control -->

                                        <!-- /.card-header-control -->
                                    </div>
                                    <!-- /.d-flex -->
                                </div>
                                <div class="table-responsive card-body">
                                    <asp:GridView ID="grvJobRespo" runat="server" AutoGenerateColumns="False"
                                        CssClass="table-striped table-hover table-bordered"
                                        ShowFooter="True" Width="400px">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo42" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode1" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Responsibilities">
                                                <ItemTemplate>

                                                    <asp:Label ID="lgvgval1Job" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobresp")) %>'></asp:Label>

                                                </ItemTemplate>



                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                        <EditRowStyle />
                                        <AlternatingRowStyle />

                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                        <!-- /grid column -->
                        <!-- grid column -->
                        <div class="col-xl-6 d-none">
                            <!-- .card -->
                            <div class="card card-fluid" id="pnlServHis" visible="false" runat="server">
                                <!-- .card-header -->
                                <div class="card-header border-0">
                                    <!-- .d-flex -->
                                    <div class="d-flex align-items-center">
                                        <span class="mr-auto">SERVICE HISTORY</span>
                                        <!-- .card-header-control -->

                                        <!-- /.card-header-control -->
                                    </div>
                                    <!-- /.d-flex -->
                                </div>
                                <!-- /.card-header -->
                                <!-- .table-responsive -->
                                <div class="card-body">

                                    <div class="table-responsive pb-3">
                                        <!-- .table -->
                                        <asp:GridView ID="gvempservices" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="false" CssClass="table-striped table-hover table-bordered">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdescription" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDate" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvComp" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSection" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Increment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIncSalary" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrsal")).ToString("#, ##0;(#, ##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSalary" runat="server"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalary")).ToString("#, ##0;(#, ##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpredesig" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                            <EditRowStyle />
                                            <AlternatingRowStyle />

                                        </asp:GridView>
                                        <!-- /.table -->
                                    </div>
                                    <!-- /.table-responsive -->
                                    <!-- .card-footer -->
                                </div>
                                <!-- /.card-footer -->
                            </div>
                            <!-- /.card -->
                        </div>

                        <!-- /grid column -->
                    </div>



                </div>
                <div class="tab-pane fade" id="Notice">
                    <div class="row">
                        <div class="col-6">
                            <section class="card card-fluid" style="min-height: 550px">
                                <div class="card-body">
                                    <!-- .card-header -->
                                    <div class="card-header border-0 mt-0 pt-0 pb-1">
                                        <!-- .d-flex -->
                                        <div class="d-flex align-items-center">


                                            <h3 class="mr-auto card-title">UPCOMMING NOTICE</h3>

                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Notification/GetNotification?Id=Notice&RefId=&notiytype=&ntype=" Target="_blank"
                                                CssClass="btn btn-sm btn-info pull-right" Text="View all"></asp:HyperLink>
                                        </div>

                                    </div>


                                    <div class="table table-responsive card-body pt-0 pb-0">
                                        <asp:GridView ID="gvAllNotice" runat="server" CssClass="table-striped table-hover table-bordered" OnRowDataBound="gvAllNotice_RowDataBound"
                                            AutoGenerateColumns="False"
                                            ShowFooter="false" AllowPaging="true" PageSize="5">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Published date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpubdate" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "publdate")) %>'></asp:Label>
                                                        <asp:Label ID="lblNoticeDet" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndetails")) %>'></asp:Label>
                                                        <asp:Label ID="lblNoticeTitle" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eventitle")) %>'></asp:Label>
                                                        <asp:Label ID="lblstartdate" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nstartdate")) %>'></asp:Label>

                                                        <asp:Label ID="lblenddate" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nenddate")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <header class="card-header border-0 p-0 m-0" style="width: 200px;">
                                                            <div class="d-flex align-items-center">
                                                                <asp:LinkButton ID="NoticeTitle" OnClick="NoticeTitle_Click" runat="server"><%#Convert.ToString(DataBinder.Eval(Container.DataItem, "eventitle").ToString())  %></asp:LinkButton>

                                                            </div>
                                                        </header>

                                                        <asp:Label ID="NoticeDet" runat="server" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ndetails").ToString())  %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>





                                            </Columns>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                    </div>

                                </div>
                            </section>
                        </div>

                        <div class="col-6">
                            <asp:Panel runat="server" ID="pnlUpcmBD" Visible="false">
                                <div class="row">
                                    <div class="card" style="max-height: 350px; overflow-y: scroll; width: 100%;">
                                        <div class="card-header">
                                            <h3 class="mr-auto card-title">UPCOMMING BIRTHDAY </h3>


                                            <asp:LinkButton ID="birthday" runat="server" OnClick="birthday_print_click"
                                                CssClass="btn btn-primary float-right"> <i class="fa fa-print"></i></asp:LinkButton>
                                        </div>
                                        <div class="card-body row" id="EventBirthday" runat="server">
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnlUpcmBDT" Visible="false">
                                <div class="row">
                                    <div class="card mt-2" style="max-height: 350px; width: 100%;">
                                        <div class="card-header">
                                            <h3 class="card-title">UPCOMMING BIRTHDAY</h3>
                                        </div>
                                        <div class="card-body row" runat="server">
                                            <div class="table table-responsive card-body pt-0 pb-0">
                                                <asp:GridView ID="gvUpcmBDT" runat="server" CssClass="table-striped table-hover table-bordered"
                                                    AutoGenerateColumns="False" OnRowCommand="gvUpcmBDT_RowCommand"
                                                    ShowFooter="false">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Jan">
                                                            <ItemTemplate>

                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jan")) %>' CommandArgument="Jan" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Feb">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "feb")) %>' CommandArgument="Feb" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Mar">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Mar")) %>' CommandArgument="Mar" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Apr">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apr")) %>' CommandArgument="Apr" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="May">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "may")) %>' CommandArgument="May" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Jun">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jun")) %>' CommandArgument="Jun" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Jul">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jul")) %>' CommandArgument="Jul" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Aug">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aug")) %>' CommandArgument="Aug" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sept">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sept")) %>' CommandArgument="Sep" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Oct">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "oct")) %>' CommandArgument="Oct" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Nov">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nov")) %>' CommandArgument="Nov" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dec">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "decm")) %>' CommandArgument="Dec" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        <asp:Panel runat="server" ID="pnlServiceInfoBTI">
                            <div class="row">
                                <div class="card" style="max-height: 450px; overflow-y: scroll; width: 100%!important;">
                                    <div class="card-header">
                                        <strong><span runat="server" id="longTermTitle"></span></strong>
                                    </div>
                                    <div class="card-body row" id="LongTerm" runat="server">
                                        <div class="table table-responsive card-body pt-0 pb-0">
                                            <asp:GridView ID="gvServiceInfo" runat="server" CssClass="table-striped table-hover table-bordered"
                                                AutoGenerateColumns="False" OnRowCommand="gvServiceInfo_RowCommand"
                                                ShowFooter="false">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Month">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblserviceMonth" runat="server" Font-Bold="True" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monname")) %>' Width="80"></asp:Label>

                                                            <asp:Button ID="viewempall" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monname")) %>' CommandName="view_emp_clickall"  CommandArgument="" Width="70px"  />

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                          <ItemStyle Width=" 100px"/>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="10 Years">
                                                        <ItemTemplate>


                                                            <asp:Button ID="view_emp10" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "year10")) %>' CommandName="view_emp_click10" CommandArgument="10" Width="50px"/>





                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                         <ItemStyle Width=" 100px"/>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="15 Years">
                                                        <ItemTemplate>
                                                            <asp:Button ID="view_emp15" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "year15")) %>' CommandName="view_emp_click15" CommandArgument="15" Width="50px"/>

                                                        </ItemTemplate>
                                                         <ItemStyle Width=" 100px"/>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="20 Years">
                                                        <ItemTemplate>
                                                            <asp:Button ID="view_emp20" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "year20")) %>' CommandName="view_emp_click20" CommandArgument="20" Width="50px"/>

                                                        </ItemTemplate>
                                                         <ItemStyle Width=" 100px"/>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="25 Years">
                                                        <ItemTemplate>
                                                            <asp:Button ID="view_emp25" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "year25")) %>' CommandName="view_emp_click25" CommandArgument="25" Width="50px"/>
                                                        </ItemTemplate>
                                                         <ItemStyle Width=" 100px"/>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                           <%-- <asp:Label ID="year30" runat="server" Font-Bold="True"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "total")) %>' Width="80px"></asp:Label>--%>
                                                            <asp:Button ID="btntotal" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "total")) %>' CommandName="view_emp_clickall_total"  CommandArgument="" Width="50px"  />

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                            </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="HolidayCalender">
                    <div class="row">
                        <div class="col-6">
                            <section class="card card-fluid " style="min-height: 550px">
                                <div class="card-body">
                                    <div class="card-header border-0">
                                        <div class="d-flex align-items-center mb-0">
                                            <h3 class="card-title mr-auto mb-0">Goverment Holidys</h3>
                                            <asp:LinkButton ID="gvholidayprint" runat="server" OnClick="gvholidayprint_Click"
                                                CssClass="btn btn-primary"> <i class="fa fa-print"></i></asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="table table-responsive card-body pt-0 pb-0">
                                        <asp:GridView ID="GvHoliday" runat="server" CssClass="table-striped table-hover table-bordered"
                                            AutoGenerateColumns="False"
                                            ShowFooter="false">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Events">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEvents" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reason")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="200" />

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDate" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wkdate1")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdaynam" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "daynam")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No. of Days">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdaynam" runat="server"
                                                            Style="text-align: left"
                                                            Text='01'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                            </Columns>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </section>
                        </div>
                        <div class="col-6">
                            <section class="card card-fluid " style="min-height: 550px">

                                <div class="card-body">
                                    <div class="card-header border-0">
                                        <div class="d-flex align-items-center mb-0">
                                            <h3 class="card-title mr-auto mb-0">Special Holidys</h3>
                                            <asp:LinkButton ID="spholidayprint" runat="server" OnClick="spholidayprint_Click" CssClass="btn btn-primary"> <i class="fa fa-print"></i></asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="table table-responsive card-body pt-0 pb-0">
                                        <asp:GridView ID="gvSpHolidyas" runat="server" CssClass="table-striped table-hover table-bordered"
                                            AutoGenerateColumns="False"
                                            ShowFooter="false">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <header class="card-header border-0 p-0 m-0">
                                                            <div class="d-flex align-items-center">
                                                                <span class="mr-auto"><%#Convert.ToString(DataBinder.Eval(Container.DataItem, "reason").ToString())  %> </span>
                                                            </div>
                                                        </header>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="200" />
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wkdate1")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Day">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "daynam")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                            </Columns>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                    </div>

                                </div>
                            </section>

                        </div>
                    </div>

                </div>

                <div class="tab-pane fade" id="Organogram">

                    <section class="card card-fluid" style="min-height: 550px">
                        <div class="card-header border-0 mb-0 pb-0">
                            <div class="d-flex align-items-center mb-0">
                                <h3 class="card-title mr-auto">Organogram</h3>
                            </div>
                        </div>
                        <div class="card-body mt-0 pt-0">
                            <div class="row">
                                <div class="col-lg-4">
                                    <ul runat="server" id="orgrm1" class="list-group list-group-flush list-group-bordered">
                                    </ul>
                                </div>
                                <div class="col-lg-4">
                                    <ul runat="server" id="orgrm2" class="list-group list-group-flush list-group-bordered">
                                    </ul>
                                </div>
                                <div class="col-lg-4">
                                    <ul runat="server" id="orgrm3" class="list-group list-group-flush list-group-bordered">
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </section>

                </div>

                <div class="tab-pane fade" id="LeavePolicy">
                    <section class="card card-fluid">
                        <div class="card-body" style="min-height: 550px">
                            <div class="col-12">
                                <div class="accordion-container" id="BtiPolicy" runat="server" visible="false">
                                    <div class=" accordionTwo">
                                        <div class="panel-group" id="accordionTwoLeft" clientidmode="Static" runat="server">
                                        </div>
                                    </div>




                                </div>






                                <div class="accordion-container" id="edidisonPolicy" runat="server" visible="false">

                                    <div class="set">
                                        <a href="#" class="active">Leave Policy 
      <i class="fa fa-plus"></i>
                                        </a>
                                        <div class="content" style="display: block;">

                                            <p>Content Comming son..............</p>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </section>
                </div>

                <div class="tab-pane fade" id="winsListData">
                    <section class="card card-fluid">
                        <div class="card-body" style="min-height: 550px">
                            <div class="col-12">

                                <div class="card-body">
                                    <h1 class="text-center">Wins List</h1>

                                    <div class="row">
                                        <div class="col-4">

                                            <ul class="list-group list-group-flush list-group-bordered" id="winUlList" runat="server">
                                            </ul>






                                        </div>

                                    </div>



                                </div>
                            </div>
                        </div>
                    </section>
                </div>

                <div class="tab-pane fade" id="CodeofConduct">
                    <section class="card card-fluid">
                        <div class="card-body" style="min-height: 550px">
                            <div class="col-12">
                                <div class="card-body">
                                    <h1 class="text-center">Code of Conduct</h1>
                                    <div runat="server" class="text-center" id="conductid">
                                    </div>

                                </div>



                            </div>
                        </div>
                    </section>
                </div>


            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade noselect" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static" oncontextmenu="return false;">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel"></h5>

                        <asp:LinkButton ID="payslip_modal_close" runat="server" CssClass="close close_btn" OnClientClick="closePayslipModal();" data-dismiss="modal"> &times; </asp:LinkButton>
                    </div>
                    <div class="modal-body">
                        <div class="slip-container">
                            <div class="header text-center font-weight-bold" style="width: 100%;">

                                <h4>PAYSLIP</h4>
                                <h5 id="RptTitle" runat="server">March-2022 (Month of salary disbursement)</h5>


                            </div>
                            <div class="employee-details mt-3 d-none">
                                <table style="border: 1px solid black; border-collapse: collapse; width: 100%;">
                                    <tr style="border: 1px solid black; border-collapse: collapse;">
                                        <td class="font-weight-bold">Employee Details :</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="font-weight-bold">Employee ID</td>
                                        <td>:<span id="EmployeeId" runat="server"></span></td>
                                        <td class="font-weight-bold">Employee Name</td>
                                        <td>:<span id="EmployeeName" runat="server"></span></td>
                                    </tr>

                                    <tr>
                                        <td class="font-weight-bold">Department</td>
                                        <td>:<span id="Department" runat="server"></span></td>
                                        <td class="font-weight-bold">Designation</td>
                                        <td>:<span id="Designation" runat="server"></span></td>
                                    </tr>

                                    <tr>
                                        <td class="font-weight-bold">Joinning Date</td>
                                        <td>: <span id="JoinDate" runat="server"></span></td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                </table>

                            </div>

                            <div class="salary-details d-none mt-3">

                                <table style="border: 1px solid black; border-collapse: collapse; width: 100%;">
                                    <tr style="border: 1px solid black; border-collapse: collapse;">
                                        <td class="font-weight-bold">Salary Details :</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="font-weight-bold">Days in Month</td>
                                        <td>:<span id="DaysInMonth" runat="server"></span></td>
                                        <td class="font-weight-bold">Gross Salary</td>
                                        <td>:<span id="GrossSal" runat="server"></span></td>
                                    </tr>

                                    <tr>
                                        <td class="font-weight-bold">Working Days</td>
                                        <td>: <span id="WorkingDays" runat="server"></span></td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                </table>


                            </div>
                            <div class="salary-details2 mt-3">
                                <p class="font-weight-bold m-0">Salary Details(Component-wise Breakdown):</p>
                                <table style="border: 1px solid black; border-collapse: collapse; width: 100%;">
                                    <tr class="font-weight-bold" style="border: 1px solid black; border-collapse: collapse;">
                                        <td>Earnings</td>
                                        <td>BDT</td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Deduction</td>
                                        <td>BDT</td>
                                    </tr>
                                    <tr>
                                        <td>House Rent</td>
                                        <td>:<span id="HouseRent" runat="server"></span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Income Tax</td>
                                        <td>:<span id="IncomeTax" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td>Basic</td>
                                        <td>:<span id="Basic" runat="server"></span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>W.F Fund</td>
                                        <td>:<span id="WFfund" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td>Medical</td>
                                        <td>:<span id="Medical" runat="server"></span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Transport</td>
                                        <td>:<span id="Transport" runat="server"></span></td>
                                    </tr>

                                    <tr>
                                        <td>Conveyance</td>
                                        <td>:<span id="Conveyance" runat="server"></span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Absent</td>
                                        <td>:<span id="Absent" runat="server"></span></td>
                                    </tr>


                                    <tr>
                                        <td>Arrear/Others</td>
                                        <td>:<span runat="server" id="ArrearOthers"></span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Gratuity loan</td>
                                        <td>:<span id="Gratuity" runat="server"></span></td>
                                    </tr>

                                    <tr>
                                        <td>Food & Others</td>
                                        <td>:<span id="FoodAndOthrs" runat="server"></span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Car loan</td>
                                        <td>:<span id="CarLoan" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td>Car Allowance</td>
                                        <td>:<span id="CarAllow" runat="server">222</span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Adv./Other</td>
                                        <td>:<span id="AdvOthers" runat="server">222</span></td>
                                    </tr>
                                    <tr>
                                        <td>Earn leave</td>
                                        <td>:<span id="EarnLeave" runat="server">33</span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Others</td>
                                        <td>:<span id="Others" runat="server">400</span></td>
                                    </tr>

                                    <tr class="text-bold">
                                        <td>Total Earnings</td>
                                        <td>:<span id="TotalEarning" runat="server">25223</span></td>
                                        <td style="border-left: 1px solid"></td>
                                        <td>Total Deduction</td>
                                        <td>:<span id="TotalDeduction" runat="server">43000</span></td>
                                    </tr>

                                </table>

                            </div>
                            <div class="net-payment mt-2">
                                <table>
                                    <tr class="font-weight-bold">
                                        <td>Net Payment</td>
                                        <td>:<span id="NetPayment" runat="server"></span></td>

                                    </tr>
                                    <tr>
                                        <td class="font-weight-bold">In words:</td>
                                        <td>:<span id="InWords" runat="server">(Taka Twenty Nine Thousand Two Hundred Twenty Five only)</span></td>

                                    </tr>
                                </table>

                            </div>

                            <div class="nb border border-dark d-none mt-5">
                                <p class="text-center m-0 p-3">
                                    <span class="font-weight-bold">NB: </span>
                                    This payslip is software generated, Any Discrepancy must be notify to HR Department within 7 days, Else it will be deemed that the staff has found this salary statement correct.
                                </p>

                            </div>

                        </div>
                        <p class="text-danger font-weight-bold float-left mt-3"><strong>For print, Please contact with HR/Payroll Department.</strong></p>
                    </div>

                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="NoticeModal" tabindex="-1" role="dialog" aria-labelledby="NoticeModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header order-bottom">
                        <h6 class="modal-title font-weight-bold" id="">Notice</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card">
                            <div class="card-header bg-info ">
                                <h6 class="font-weight-bold text-white" id="modalNoticeTitle" runat="server"></h6>
                            </div>
                            <div class="card-body bg-light">
                                <p class="" runat="server" id="modalNoticeDet"></p>
                                <hr />
                                <p class="font-weight-bold">Publish Date : <span runat="server" class="text-muted" id="publishDate"></span></p>
                                <p class="font-weight-bold">Start Date : <span runat="server" class="text-muted" id="noticeStartDate"></span></p>
                                <p class="font-weight-bold">End Date : <span runat="server" class="text-muted" id="noticeEndDate"></span></p>
                            </div>
                        </div>




                    </div>
                </div>
            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="payslipmodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <p class="modal-title font-weight-bold" id="exampleModalLongTitle">Pay Slip</p>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

                        <div class="card" id="PaySlipPart" runat="server">


                            <div class="card-body" id="payslipdiv">

                                <div class="table-responsive pb-3">
                                    <asp:GridView ID="gvPaySlip" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvPaySlip_RowDataBound"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaySlipSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: center"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpayslipmonth" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthdesc")) %>' Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvmonthid" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid")) %>' Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvempid" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnetamt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkPrintPaySlip" runat="server" Target="_blank" CssClass="btn btn-xs btn-danger" ToolTip="Print Pay Slip"><span class=" fa fa-print"> Print</span>
                                                    </asp:HyperLink>
                                                    <%-- <asp:HyperLink ID="HyplnkModal" runat="server" data-dismiss="modal" CssClass="btn btn-xs btn-success" ForeColor="White" data-toggle="modal" data-target="#exampleModal" ToolTip="Print Pay Slip"><span class=" fa fa-print"> View</span>     </asp:HyperLink>--%>
                                                    <asp:LinkButton ID="HyplnkModal" OnClick="payslip_modal_Click" Style="margin-left: 3px;" runat="server" class="btn btn-primary btn-sm"><i class="fa fa-eye"></i> View</asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>


        <!-- Long Term Service Modal -->
        <div class="modal fade" id="LongTermModal"  role="dialog"  aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h6 class="modal-title font-weight-bold" runat="server" id="empdettitle">EMPLOYEE LONG TERM SERVICE</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card">

                            <div class="card-body bg-light">
                                <div class="table-responsive pb-3">
                                    <asp:GridView ID="gvEmpDet" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Font-Bold="True"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID Card">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center" ID="lblidcard"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Emp. Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                                                      <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "department")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Joining Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "doj")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>


                  

                  




                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>




                        </div>
                    </div>
                </div>
            </div>
        </div>




        <!-- Modal -->
        <div class="modal fade" id="BirthDayDetModal" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header order-bottom">
                        <h6 class="modal-title font-weight-bold" id="empbdtitle" runat="server" >All Employee</h6>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="card">

                            <div class="card-body bg-light">
                                <div class="table-responsive pb-3">
                                    <asp:GridView ID="gvBirthDayDet" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Font-Bold="True"
                                                        Style="text-align: left;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' ></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="40px"/>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="ID Card">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'></asp:Label>
                                                </ItemTemplate>
                                                       <ItemStyle Width="50px"/>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Emp. Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                 <ItemStyle Width="150px"/>
                                            </asp:TemplateField>



                                            
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                   
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "department")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                  <ItemStyle Width="150px"/>
                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="Birth Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bdate")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                  <ItemStyle Width="
                                                                      100px"/>
                                            </asp:TemplateField>

                                                                                      <asp:TemplateField HeaderText="Joining Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "doj")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                  <ItemStyle Width="
                                                                      100px"/>
                                            </asp:TemplateField>




                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>




                        </div>
                    </div>
                </div>
            </div>
        </div>
   
        <script>
            $(document).ready(function () {
                $(".set > a").on("click", function () {
                    if ($(this).hasClass("active")) {
                        $(this).removeClass("active");
                        $(this)
                            .siblings(".content")
                            .slideUp(200);
                        $(".set > a i")
                            .removeClass("fa-minus")
                            .addClass("fa-plus");
                    }
                    else {
                        $(".set > a i")
                            .removeClass("fa-minus")
                            .addClass("fa-plus");
                        $(this)
                            .find("i")
                            .removeClass("fa-plus")
                            .addClass("fa-minus");
                        $(".set > a").removeClass("active");
                        $(this).addClass("active");
                        $(".content").slideUp(200);
                        $(this)
                            .siblings(".content")
                            .slideDown(200);
                    }
                });
            });

            $(function () {
                var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "home";
                $('#Tabs a[href="#' + tabName + '"]').tab('show');
                $("#Tabs a").click(function () {
                    $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                });

            });

            function PrintRpt() {
                window.open('<%= ResolveUrl("RDLCViewerWin.aspx?PrintOpt=PDF") %>', '_blank');
            }
        </script>
</asp:Content>
