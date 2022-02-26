﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="RealERPWEB.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script>


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
                    ['Task', 'This Months Attendance status'],

                    ['Present', present],
                    ['Absent', abs],
                    ['Late', late],
                    ['On leave', onleave],


                ]);

                var options = {
                    title: 'Attendance status',
                    is3D: true,
                    pieSliceText: 'value'

                };

                var chart = new google.visualization.PieChart(document.getElementById('piechartEMPStatus'));

                chart.draw(data, options);
            }
        }

        function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvPabxInfo.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>
    <div class="page ">
        <!-- .page-cover -->
        <header class="Xpage-cover mt-4">
            <div class="row">
                <div class="col-4">
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="text-center" style="height: 250px;">
                                <a href="UserProfile.aspx" class="user-avatar user-avatar-xl">
                                    <asp:Image ID="userimg" runat="server" ImageUrl="~/image/profile_img.png" />
                                </a>
                                <h2 class="h4 mt-2 mb-0" id="UserName" runat="server">Beni Arisandi </h2>

                                <p class="text-muted" id="UDesignation" runat="server">Project Manager @CreativeDivision </p>

                            </div>

                        </div>
                    </div>


                </div>
                <div class="col-4">


                    <section class="card card-fluid mb-0" style="width: 100%; height: 268px;">

                        <header class="card-header border-0">
                            <div class="d-flex align-items-center">
                                <span class="mr-auto">Upcoming Holidays </span>
                            </div>
                        </header>
                        <div class="card-body" id="upComingHolidays" runat="server" style="height: 268px; overflow-y:scroll;">                           
                        </div>                        
                        <footer class="card-footer">
                            <a href="#" class="card-footer-item"> View All <i class="fa fa-fw fa-angle-right"></i>
                            </a>
                        </footer>
                        <!-- /.card-footer -->
                    </section>



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
            <!-- .cover-controls -->
            <div class="cover-controls cover-controls-bottom col-12 text-right">
            </div>
            <nav class="page-navs">
                <!-- .nav-scroller -->
                <div class="nav-scroller">
                    <!-- .nav -->
                    <div class="nav nav-center nav-tabs">
                        <a class="nav-link active" data-toggle="tab" href="#home">Activities
                            <span class="badge">16</span>
                        </a>
                        <a href="#Notice" class="nav-link smooth-scroll" data-toggle="tab">Notice</a>
                        <a href="#switcher" class="nav-link smooth-scroll">Pabx List</a>

                        <a href="#base-style" class="nav-link smooth-scroll">Holiday Calender</a>
                        <a href="#labels" class="nav-link smooth-scroll">Wins List</a>
                        <a href="#floating-label" class="nav-link smooth-scroll">Orintation Link</a>
                        <a href="#selects" class="nav-link smooth-scroll">Major MR</a>
                        <a href="#checkboxes" class="nav-link smooth-scroll">Code of Conduct </a>
                        <asp:HyperLink ID="HyperLink4" class="nav-link smooth-scroll" Target="_blank" NavigateUrl="~/F_81_Hrm/F_84_Lea/MyLeave?Type=User" runat="server">Apply Leave</asp:HyperLink>

                        <a href="#" class="nav-link smooth-scrol" data-toggle="modal" data-target="#followingModal">Change Profile Photo</a>

                        <a href="#" class="nav-link smooth-scroll" data-toggle="modal" data-target="#fdollowingModal">Change Pasword</a>

                        <asp:LinkButton ID="hyplPreviewCv" CssClass=" btn btn-success btn-sm" runat="server" OnClick="hyplPreviewCv_Click1"> View Profile <i class="fa fa-print "></i> </asp:LinkButton>
                        <a href="MyShortCutLink.aspx?Module=" class="btn btn-light d-none">My Shortcut</a>

                    </div>
                    <!-- /.nav -->
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
        <!-- /.modal -->
        <!-- /Following Modal -->
        <!-- .page-navs -->

        <!-- .page-inner -->

        <!-- .page-section -->
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

                    <div class="row">
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
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblacintimed" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acintime")).ToString("#, ##0;(#, ##0); ") %>'></asp:Label>
                                                    </td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblLate" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aclate")).ToString("#, ##0;(#, ##0); ")%>'></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblAbsent" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#, ##0;(#, ##0); ") %>'></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblLeave" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leave")).ToString("#, ##0;(#, ##0); ")%>'></asp:Label>
                                                    </td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lbllvadj" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lvadj")).ToString("#, ##0;(#, ##0); ")%>'></asp:Label>
                                                    </td>


                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblrlateapp" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lateapp")).ToString("#, ##0;(#, ##0); ")%>'></asp:Label>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td style="width: 80px">
                                                        <asp:Label ID="ttl" runat="server" CssClass=" smLbl_to" Text="Total"></asp:Label></td>
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblacintime" runat="server" Style="text-align: right"></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lbltotallate" runat="server" Style="text-align: right"></asp:Label></td>
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lbltotalabs" runat="server" Style="text-align: right"></asp:Label>
                                                    </td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lbltotalleave" runat="server" Style="text-align: right"></asp:Label></td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lbltolvadj" runat="server" Style="text-align: right"></asp:Label></td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblfrtolateapp" runat="server" Style="text-align: right"></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 80px">
                                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to" Text="In %"></asp:Label></td>
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblperIntime" runat="server" Style="text-align: right"></asp:Label>
                                                    </td>
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblperLate" runat="server" Style="text-align: right"></asp:Label></td>
                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblPerabs" runat="server" Style="text-align: right"></asp:Label>
                                                    </td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblperleave" runat="server" Style="text-align: right"></asp:Label></td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblfperlvadj" runat="server" Style="text-align: right"></asp:Label></td>

                                                    <td style="width: 80px; text-align: right !important;">
                                                        <asp:Label ID="lblfrperlateapp" runat="server" Style="text-align: right"></asp:Label></td>

                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="col-md-6">
                            <div class="card card-fluid" style="min-height: 465px;">
                                <!-- .card-header -->
                                <div class="card-header border-0">
                                    <!-- .d-flex -->

                                    <div class="d-flex align-items-center mb-0">
                                        <h3 class="card-title mr-auto">Pabx List </h3>
                                        <!-- .card-title-control -->
                                        <div class="card-title-control">
                                            <!-- .dropdown -->
                                            <div class="input-group input-group-alt">

                                                <div class="input-group-prepend ">
                                                    <asp:Label ID="Label3" runat="server" CssClass="btn btn-secondary btn-sm">Search</asp:Label>
                                                </div>
                                                <asp:TextBox ID="inputtextbox" Style="height: 29px" runat="server" CssClass="form-control" placeholder="Search here..." onkeyup="Search_Gridview(this)"></asp:TextBox>
                                            </div>
                                            <!-- /.dropdown -->
                                        </div>
                                        <!-- /.card-title-control -->
                                    </div>


                                    <!-- /.d-flex -->
                                </div>
                                <div class="table table-responsive card-body">

                                    <asp:GridView ID="gvPabxInfo" runat="server" CssClass="table-striped table-hover table-bordered"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True">
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

                                            <asp:TemplateField HeaderText="Card #" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcardnoemp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdeptandemployeeemp" runat="server"
                                                        Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname").ToString())  %>'> 
                                              
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdesignationemp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>





                                            <asp:TemplateField HeaderText="Ext#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExtion" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "extention")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmobile" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
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
                        </div>
                    </div>


                    <!-- /grid row -->
                    <!-- grid row -->
                    <div class="row">
                        <!-- grid column -->
                        <div class="col-xl-6">
                            <!-- .card -->
                            <div class="card card-fluid">
                                <!-- .card-header -->
                                <div class="card-header border-0 pb-0">
                                    <!-- .d-flex -->
                                    <div class="d-flex align-items-center">
                                        <span class="mr-auto">LEAVE HISTORY</span>
                                        <!-- .card-header-control -->

                                        <asp:HyperLink ID="hlnkbtnNext" runat="server" NavigateUrl="#" Target="_blank" CssClass="btn btn-sm btn-info primaryBtn pull-right" Text="View all"></asp:HyperLink>



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
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Availed">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlentitled01" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
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
                        <!-- /grid column -->
                        <!-- grid column -->
                        <div class="col-xl-6">
                            <!-- .card -->
                            <div class="card card-fluid">
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
                                            ShowFooter="false" CssClass="table-striped table-hover table-bordered grvContentarea">
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
                                                        <asp:Label ID="lblgvdescription" runat="server" Font-Size="11PX"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDate" runat="server" Font-Size="11PX"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvComp" runat="server" Font-Size="11PX"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSection" runat="server" Font-Size="11PX"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Increment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIncSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrsal")).ToString("#, ##0;(#, ##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSalary" runat="server" Font-Size="11PX"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalary")).ToString("#, ##0;(#, ##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
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

                    <div class="row">
                        <div class="col-md-6">
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
                                    <asp:GridView ID="grvJobRespo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered"
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
                        <div class="col-xl-6">
                            <!-- .card -->

                            <!-- /.card -->
                        </div>
                    </div>


                    <!-- /grid row -->
                </div>
                <div class="tab-pane fade" id="Notice">
                    <div class="row">
                        <div class="col-6">
                            <section class="card card-fluid" >
                                <div class="card-body" style="min-height: 345px">
                                    <div class="list-group list-group-messages list-group-flush list-group-bordered" id="EventCaro" runat="server">
                                        <!-- message item -->
                                        <div class="list-group-item unread">
                                            <div class="list-group-item-body pl-md-2">
                                                <div class="row">
                                                    <div class="col-12 col-lg-7">
                                                        <h4 class="list-group-item-title text-truncate">
                                                            <a href="app-conversations.html">Our trip to Montreal</a>
                                                        </h4>
                                                        <p class="list-group-item-text text-truncate">Hi Guys, minus, aliquam porro repudiandae numquam. Molestias. </p>
                                                    </div>
                                                    <div class="col-12 col-lg-2 text-lg-right">
                                                        <p class="list-group-item-text">16 minutes ago </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </section>
                        </div>

                        <div class="col-6">
                            <section class="card card-fluid" style="min-height: 345px">
                                <div class="card-body row" id="EventBirthday" runat="server">
                                    <div class="col-12 col-sm-6 col-lg-3">
                                        <div class="media align-items-center mb-3">
                                            <a href="#" class="user-avatar user-avatar-lg mr-3">
                                                <img src="https://localhost:44359/Upload/UserImages/3101003.JPG" alt="">
                                            </a>
                                            <div class="media-body">
                                                <h6 class="card-subtitle text-muted">Lets Celebrate Shahadat Hosen Sanon , Supervisor , Birthday on 24 Feb
                                                </h6>
                                            </div>
                                            <a href="#" class="btn btn-reset text-muted" data-toggle="tooltip" title="" data-original-title="Chat with teams">
                                                <i class="oi oi-chat"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </section>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            $(".switcher-input").change(function () {
                $.ajax({
                    type: "POST",
                    url: 'UserProfile.aspx/ChangeEventsStatus',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //   $("#divResult").html("success");
                    },
                    error: function (e) {
                        //  $("#divResult").html("Something Wrong.");
                    }
                });
            });
        });


    </script>
</asp:Content>
