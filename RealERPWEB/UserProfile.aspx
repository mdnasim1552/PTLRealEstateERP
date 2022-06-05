﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="RealERPWEB.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <!-- Latest compiled and minified CSS -->


    <script>

        $(document).keyup(function (e) {
            if (e.keyCode == 44) return false;
        });

        $(document).ready(function () {
            ExcuteEmpStatus();
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

    </script>
    <style>
        #payslipmodal{
                  z-index:9999;
        }
#{

          z-index:1;
}
        .dbx {
            height: 60px;
            width:100%;
          display: flex;
  justify-content: center;
  text-align:center;
  align-items: center;

        }
        .dbx a{
            text-decoration:none;
        }
        .dbx:hover{
            background:crimson;
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
    <div class="page ">
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
                <div class="col-4">
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
                <div class="col-4">
                    <asp:Panel runat="server" ID="pnlUpcmEdison">
                                       <section class="card card-fluid mb-0" style="width: 100%; height: 268px;">
                        <header class="card-header border-0 pb-0">
                            <div class="d-flex align-items-center">
                                <span class="mr-auto">Upcoming Holidays </span>
                            </div>
                        </header>
                        <div class="card-body" style="max-height: 200px; overflow-y: scroll" id="Div1" runat="server">



                            <div class="row">
                        

                                <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border">    <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/MyLeave?Type=MGT")%>" target="_blank">Apply Leave</a></div></div>
                                                                  <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border">    <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/AttnOutOfOffice")%>" target="_blank">Online Attendance</a></div></div>

                                  <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border">
                                                 <asp:HyperLink ID="hlnkattreport" runat="server" Target="_blank"  >
                                                     Attendance Report
                                                        </asp:HyperLink>
                                                        </div></div>
                            </div>
                             <div class="row mt-2">
                                   <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border"><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HREmpMonthlyAtten")%>" target="_blank">Manual Attendance</a></div></div>
                                   <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border"><a href="<%=this.ResolveUrl("~/UserProfile")%>" target="_blank">Profile</a></div></div>

                                  <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border"><a data-toggle="modal" data-target="#payslipmodal">Pay Slip</a></div></div>
                            </div>
                            
                                          <div class="row mt-2">
                          <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border"><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/AllEmpList?Type=Report&comcod=")%>" target="_blank">Employee Directory</a></div></div>
                                  <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border">Business Card</div></div>
                                  <div class="col-lg-4"><div class="dbx shadow-sm bg-white rounded border">KPI</div></div>
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
                <div class="col-4">
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

            <nav class="page-navs" id="page-navs">
                <!-- .nav-scroller -->
                <div class="card-body m-0 p-0">

                    <ul class="nav nav-pills topMenu">
                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home">Activities</a> </li>
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

                        <li class="nav-item">
                            <a href="#" class="nav-link smooth-scrol" data-toggle="modal" data-target="#followingModal">Change Profile Photo</a></li>
                        <li class="nav-item d-none"><a href="#" class="nav-link smooth-scroll" data-toggle="modal" data-target="#fdollowingModal">Change Pasword</a></li>
                        <li class="nav-item">
                            <asp:LinkButton ID="hyplPreviewCv" CssClass=" btn btn-success btn-sm d-none" runat="server" OnClick="hyplPreviewCv_Click1"> View Profile <i class="fa fa-print "></i> </asp:LinkButton></li>
                        <li class="nav-item"><a href="MyShortCutLink.aspx?Module=" class="btn btn-light d-none">My Shortcut</a></li>
                           <li class="nav-item" runat="server" id="modalPayslipBti"><a data-toggle="modal" data-target="#payslipmodal" class="btn btn-light">Pay Slip</a></li>




                    </ul>



                </div>
                <!-- /.nav-scroller -->
            </nav>

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
                                        <span class="mr-auto">LEAVE HISTORY</span>
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
                        </div>
                    </div>


                    <div class="row" hidden="hidden">
                        <!-- grid column -->
                        <div class="col-xl-6">
                            <div class="card card-fluid">
                                <div class="card-header border-0">
                                    <!-- .d-flex -->
                                    <div class="d-flex align-items-center">
                                        <span class="mr-auto">JOB RESPONSIBILITIES</span>
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
                        <div class="col-xl-6">
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
                                            <span class="mr-auto">Upcoming Notice </span>

                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Notification/GetNotification?Id=All&RefId=&notiytype=&ntype=" Target="_blank"
                                                CssClass="btn btn-sm btn-info pull-right" Text="View all"></asp:HyperLink>
                                        </div>

                                    </div>


                                    <div class="table table-responsive card-body pt-0 pb-0">
                                        <asp:GridView ID="gvAllNotice" runat="server" CssClass="table-striped table-hover table-bordered"
                                            AutoGenerateColumns="False"
                                            ShowFooter="false" AllowPaging="true" PageSize="5">
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
                                                                <span class="mr-auto"><%#Convert.ToString(DataBinder.Eval(Container.DataItem, "eventitle").ToString())  %> </span>
                                                            </div>
                                                        </header>

                                                        <p class="m-0"><%#Convert.ToString(DataBinder.Eval(Container.DataItem, "ndetails").ToString())  %></p>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Published date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "publdate")) %>'></asp:Label>
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
                            <div class="card " style="max-height: 550px; overflow-y: scroll">
                                <div class="card-header">
                                    <span class="mr-auto">Upcoming BirthDay </span>

                                    <asp:LinkButton ID="birthday" runat="server" OnClick="birthday_print_click"
                                        CssClass="btn btn-primary float-right"> <i class="fa fa-print"></i></asp:LinkButton>
                                </div>
                                <div class="card-body row" id="EventBirthday" runat="server">
                                </div>
                            </div>
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

                                    <div class="set">
                                        <a href="#" class="active">Leave Policy 
      <i class="fa fa-plus"></i>
                                        </a>
                                        <div class="content" style="display: block;">

                                            <p>
                                                We are enjoying leave facility in all formats as bti treated us as second VIP. To
enhance this facility for the betterment of our employee following option is included
at our leave policy which is agree by all HOD at standing meeting this month.
                                            </p>
                                            <p>
                                                i) In case of 4 to 5 days continue leave, the first 3 days will be considered as
casual leave. Rest 1 or 2 days leave may be considered as CL or EL which
will be depends on the applicants respective HOD.
                                            </p>
                                            <p>
                                                ii) If the enjoyed leave duration is more than 05 days then it shall be treated as
Earn Leave.
                                            </p>

                                            <p>This is for all of your information</p>
                                        </div>
                                    </div>
                                    <div class="set">
                                        <a href="#">SOP for Late Attendance 
      <i class="fa fa-plus"></i>
                                        </a>
                                        <div class="content">
                                            <h6 class="text-left"><i class="fas fa-arrow-circle-right"></i>Purpose:</h6>

                                            <p>
                                                To ensure the maximum attendance of all employee of bti.

                                            </p>
                                            <h6 class="text-left"><i class="fas fa-arrow-circle-right"></i>Late Attendance Procedure:</h6>

                                            <p>
                                                i)  The office hour at Head office and branch offices of bti is from 9:00am to 5:30pm for Saturday to Thursday.
ii) Presence of an employee after scheduled office hour will be marked as late within 10:00am at Head office and branch offices.
iii) In case of continuous late of any employee which is 3 or more than 3 days will be adjusted from his/ her Casual Leave (CL) and Earn Leave (EL) as per the
following schedule:
a) 3 Days Late = 1 CL
b) 4 Days Late = 1 CL & half day leave
c) 5 Days Late = 2 CL & half day leave
d) 6 Days Late = 3 CL & half day leave
 Each attendance after 10:00am will be marked as absent for the day.
                                            </p>
                                            <h6 class="text-left">Note: </h6>

                                            <p>
                                                i. For each late after 6 days late additional 1 EL will be deducted.

                                            </p>

                                            <p>ii. If CL/ EL is not available than salary will be deducted from monthly salary.</p>
                                        </div>
                                    </div>
                                    <div class="set">
                                        <a href="#">Late Present Policy 
      <i class="fa fa-plus"></i>
                                        </a>
                                        <div class="content">


                                            <p>
                                                i)  The office hour at Head office and branch offices of bti is from 9:00am to 5:30pm for Saturday to Thursday.
                                            </p>
                                            <p>
                                                ii) Presence of an employee after scheduled office hour will be marked as late within 10:00am at Head office and branch offices.
                                            </p>

                                        </div>
                                    </div>
                                    <div class="set">
                                        <a href="#">Half day leave Policy 
      <i class="fa fa-plus"></i>
                                        </a>
                                        <div class="content">

                                            <p><i class="fas fa-arrow-circle-right"></i>Half day  leave  is  divided into  below 2 time  slots:</p>
                                            <p>i.  	9:00  am to  1:00  pm</p>
                                            <p>ii.	1:00  pm to  5:30  pm.</p>
                                            <p><i class="fas fa-arrow-circle-right"></i>3 Casual Leave (CL) can be enjoyed 6 times as half day leave.</p>
                                            <p><i class="fas fa-arrow-circle-right"></i>2 Sick Leave (SL) can be enjoyed 4 times as half day leave.</p>
                                            <p><i class="fas fa-arrow-circle-right"></i>Half day leave can be enjoyed maximum 10 times in a year.</p>
                                            <p><i class="fas fa-arrow-circle-right"></i>2 half day leave will be considered as 1 day CL/ SL.</p>
                                            <p><i class="fas fa-arrow-circle-right"></i>Before  taking  half  day leave  facility  each  employee  must  get  approval from  their  respective  HOD and submit approval  copy  to HR.</p>
                                            <p><i class="fas fa-arrow-circle-right"></i>Any employee taking half day leave in the morning will inform to respective concern person of his/ her department before 9:00am.</p>
                                            <p><i class="fas fa-arrow-circle-right"></i>No half day leave information will be accepted after 9:00am. </p>
                                            <p><i class="fas fa-arrow-circle-right"></i>All other condition regarding leave will remain unchanged.</p>

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
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
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
                    <div class="modal-footer">

                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

                    </div>
                </div>
            </div>
        </div>



<!-- Modal -->
<div class="modal fade" id="payslipmodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Pay Slip Last 3 Month</h5>
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
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaySlipSlNo" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
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
                                                        <asp:HyperLink ID="HyplnkModal" runat="server" data-dismiss="modal" CssClass="btn btn-xs btn-success" ForeColor="White" data-toggle="modal" data-target="#exampleModal" ToolTip="Print Pay Slip"><span class=" fa fa-print"> View</span>
                                                        </asp:HyperLink>
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
                } else {
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

        function PrintRpt(printype) {
            window.open('../../RDLCViewerWin.aspx?PrintOpt=' + printype + '', '_blank');
        }

    </script>
</asp:Content>
