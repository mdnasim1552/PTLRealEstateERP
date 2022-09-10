
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LinkLateElLeaveAAbs.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_99_MgtAct.LinkLateElLeaveAAbs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #tblrplellaabsemp tr td{
            font-size:12px !important;
        }
    </style>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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

    <div class="page-inner">
        <!-- .page-title-bar -->
        <header class="page-title-bar">
            <!-- page title stuff goes here -->
        </header>
        <!-- /.page-title-bar -->
        <!-- .page-section -->
        <div class="page-section">
            <div class="section-deck">
                <!-- .card -->
                <section class="card card-fluid">
                    <header class="card-header">Particular Person </header>
                    <div class="card-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-10">
                                    <asp:Label ID="lbldate" runat="server" CssClass=" smLbl_to">Date</asp:Label>
                                    <asp:Label ID="lblvaldate" runat="server" CssClass="   inputDateBox">Date</asp:Label>

                                    <asp:Label ID="lbltoStaff" runat="server" CssClass=" smLbl_to">Total Staff</asp:Label>
                                    <asp:Label ID="lblvaltoStaff" runat="server" CssClass=" inputDateBox"></asp:Label>




                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- .card-body -->
                    <div class="card-body">
                        <table id="tblleavesummery" class="table-striped table-hover table-bordered grvContentarea">
                            <tr>
                                <th>SL</th>
                                <th>Particular</th>
                                <th>Person</th>
                            </tr>


                            <tr>
                                <td>1</td>
                                <td>Intime</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lbltointime" runat="server"
                                        Width="80px"></asp:Label></td>
                            </tr>



                            <tr>
                                <td>2</td>
                                <td>Total Late</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lbltolate" runat="server"
                                        Width="80px"></asp:Label></td>
                            </tr>


                            <tr>
                                <th>3</th>
                                <td>Total Early Leave</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lbltoeleave" runat="server"
                                        Width="80px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>Total On Leave</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lbltooleave" runat="server"
                                        Width="80px"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>5</th>
                                <td>Total Absent</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lbltoabsent" runat="server"
                                        Width="80px"></asp:Label></td>
                            </tr>

                        </table>
                    </div>

                </section>
                <!-- /.card -->
                <!-- .card -->
                <section class="card card-fluid">
                    <header class="card-header">Graph</header>
                    <!-- .card-body -->
                    <div class="card-body">
                        <div id="donutchart" class="col-sm-12 col-md-12 col-lg-12" style="height: 250px"></div>

                    </div>

                </section>
                <!-- /.card -->
            </div>
             
            <section class="card card-fluid">
                 
                  <div class="card-body">
                   
                   
                  <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="viewCashFlow" runat="server">



                    <div class="table-responsive">

                        <asp:Repeater ID="rplellaabsemp" runat="server">
                            <HeaderTemplate>
                                <table id="tblrplellaabsemp" class="table-striped table-hover table-bordered grvContentarea">
                                    <tr>
                                        <th rowspan="2">SL</th>

                                        <th colspan="3" style="background-color: #DC3912; font-size: 14px; color: #fff;">
                                            <asp:Label ID="Label1" runat="server" Text="In-Time"></asp:Label></th>

                                        <th colspan="2" style="background-color: #DC3912; font-size: 14px; color: #fff;">
                                            <asp:Label ID="lblrphamt01" runat="server" Text="Late"></asp:Label></th>
                                        <%-- <th colspan="2" style=" background-color:#DC3912; font-size:14px;color:#fff;">
                                                <asp:Label ID="lblrphamt02" runat="server" Text="Last Day"></asp:Label></th>
                                        --%>


                                        <th colspan="2" style="background-color: #FF9900; font-size: 14px; color: #fff;">


                                            <asp:Label ID="lblrphamt03" runat="server" Text="Early Leave"></asp:Label></th>
                                        <th colspan="2" style="background-color: #109618; font-size: 14px; color: #fff;">
                                            <asp:Label ID="lblrphamt04" runat="server" Text="On Leave"></asp:Label></th>
                                        <th colspan="2" style="background-color: #990099; font-size: 14px; color: #fff;">
                                            <asp:Label ID="lblrphamt05" runat="server" Text="Absent"></asp:Label></th>



                                    </tr>

                                    <tr>

                                        <th style="">Name</th>
                                        <th style="width: 60px;">In-Time</th>
                                        <th style="width: 60px;">Out Time</th>

                                        <th style="">Name</th>
                                        <th style="width: 60px;">Time</th>
                                        <%--<th style="width: 100px;">Out Time</th>
                                            <th style="width: 60px;">Reason</th>--%>
                                        <%-- <th style="width: 130px;">Name</th>
                                            <th style="width: 60px;">Time</th>--%>
                                        <th style="">Name</th>
                                        <th style="width: 60px;">Time</th>
                                        <th style="">Name</th>
                                        <th style="width: 60px;">Comments</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>

                                    <td>

                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right" Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </td>
                                    <td>

                                       
                                        <asp:Label ID="Label3" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "inempname"))  %>'
                                            ></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "incomm"))  %>'
                                            Width="60px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "outcomm"))  %>'
                                            Width="60px"></asp:Label>
                                    </td>



                                    <td>
                                      
                                        <asp:Label ID="lrplempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lempname"))  %>'
                                            ></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lrplcomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lcomm"))  %>'
                                            Width="60px"></asp:Label>
                                    </td>


                                    <%--   <td>
                                            <asp:Label ID="lrpllastdayst" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "llastdayst"))  %>'
                                                Width="80px"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lrplresaon" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lresaon"))  %>'
                                                Width="80px"></asp:Label>
                                        </td>--%>


                                    <td>
                                        <asp:Label ID="lrpelempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "elempname"))  %>'
                                          ></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lrpelcomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "elcomm"))  %>'
                                            Width="60px"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lrpolempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "olempname"))  %>'
                                          ></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lrpolcomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "olcomm"))  %>'
                                            Width="60px"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lrpaempname" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "aempname"))  %>'
                                           ></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lrpacomm" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "acomm"))  %>'
                                            Width="60px"></asp:Label>
                                    </td>




                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>


                        </asp:Repeater>


                    </div>


                </asp:View>

                <asp:View ID="Viewcrlim02" runat="server">

                    <div class="row">
                    </div>
                </asp:View>


            </asp:MultiView>
                    
                  </div>
                 
                </section>
        </div>
        <!-- /.page-section -->
    </div>



     
    <script src="../../Scripts/GoogleChart.js"></script>
    <script src="../../Scripts/jsapi.js"></script>
    <script src="../../Scripts/uds_api_contents.js"></script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var p = parseInt(<%=this.lbltointime.Text.ToString() %>);
            var l = parseInt(<%=this.lbltolate.Text.ToString() %>);
            var el = parseInt(<%=this.lbltoeleave.Text.ToString() %>);
            var ol = parseInt(<%=this.lbltooleave.Text.ToString() %>);
            var ab = parseInt(<%=this.lbltoabsent.Text.ToString() %>);


            //alert(p+','+l+','+el+','+ol+','+ab+',');

            var data = google.visualization.arrayToDataTable([

             ['Task', 'Present per Day'],
              //['TotalStaf', 52],
              ['Present', p],
              ['Late', l],
              ['Early Leave', el],
              ['On Leave', ol],
              ['Absent', ab]
            ]);

            var options = {
                title: "",
                pieHole: 0.4,
            };


            var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
            chart.draw(data, options);
        }
    </script>

    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
