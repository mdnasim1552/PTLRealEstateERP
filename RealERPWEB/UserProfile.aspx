<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="RealERPWEB.UserProfile" %>

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
    </script>
    <div class="page">
        <!-- .page-cover -->
        <header class="page-cover">
            <div class="text-center">
                <a href="UserProfile.aspx" class="user-avatar user-avatar-xl">
                    <asp:Image ID="userimg" runat="server" ImageUrl="~/image/profile_img.png" />
                </a>
                <h2 class="h4 mt-2 mb-0" id="UserName" runat="server">Beni Arisandi </h2>

                <p class="text-muted" id="UDesignation" runat="server">Project Manager @CreativeDivision </p>

            </div>
            <!-- .cover-controls -->
            <div class="cover-controls cover-controls-bottom">
                <asp:HyperLink ID="HyperLink4" class="btn btn-light" Target="_blank" NavigateUrl="~/F_81_Hrm/F_84_Lea/MyLeave?Type=User" runat="server">Apply Leave</asp:HyperLink>

                 
                <a href="MyShortCutLink.aspx?Module=" class="btn btn-light">My Shortcut</a>
                <a href="#" class="btn btn-light" data-toggle="modal" data-target="#followingModal">Change Profile Photo</a>
                <a href="#" class="btn btn-light" data-toggle="modal" data-target="#fdollowingModal">Change Pasword</a>
            </div>
            <!-- /.cover-controls -->
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
            <!-- .section-block -->
            <div class="row">
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
                    <div class="card card-fluid">

                        <div class="card-body">
                            <div class="d-flex align-items-center mb-3">
                                <h3 class="card-title mr-auto">Graph</h3>

                            </div>

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






            <!-- /grid row -->
            <!-- grid row -->
            <div class="row">
                <!-- grid column -->
                <div class="col-xl-6">
                    <!-- .card -->
                    <div class="card card-fluid">
                        <!-- .card-header -->
                        <div class="card-header border-0">
                            <!-- .d-flex -->
                            <div class="d-flex align-items-center">
                                <span class="mr-auto">LEAVE HISTORY</span>
                                <!-- .card-header-control -->

                                <div class="form-group dropdown">
                                    <asp:HyperLink ID="hlnkbtnNext" runat="server" NavigateUrl="#" Target="_blank" CssClass="btn btn-info primaryBtn pull-right" Text="Next">

                                    </asp:HyperLink>
                                </div>


                            </div>
                            <!-- /.d-flex -->
                        </div>
                        <!-- /.card-header -->
                        <!-- .table-responsive -->

                        <div class="table-responsive card-body">
                            <!-- .table -->
                            <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered">
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
                        <div class="table-responsive">
                            <!-- .table -->
                            <asp:GridView ID="gvempservices" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="678px">
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
                                                Width="80px"></asp:Label>
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
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvComp" runat="server" Font-Size="11PX"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
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
            </div>

            <!-- /grid row -->
        </div>
        <!-- /.page-section -->

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
