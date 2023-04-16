<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="JobAnalytics.aspx.cs" Inherits="RealERPWEB.F_38_AI.JobAnalytics" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/drilldown.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/accessibility.js"></script>
     <script src="../Scripts/highcharts.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        var comcod, projcode, data;
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            GetData();
        };

        function GetData() {
            try {
                comcod = <%=this.GetComdCode()%>;
                projcode = '<%=this.Request.QueryString["PID"].ToString()%>';
                var temp = comcod.toString();
                var com = temp.slice(0, 1);
                $.ajax({
                    type: "POST",
                    url: "JobAnalytics.aspx/GetAllData",
                    contentType: "application/json; charset=utf-8",
                    data: '{"comcodi":"' + comcod + '" , "projcode": "' + projcode + '"}',
                    dataType: "json",

                    success: function (response) {
                        var data = JSON.parse(response.d);
                        var data1 = data.GrphicalShow;
                        console.log('success', data1);

                        //data1[1].pmargin
                        //var marcolor = data1[1].pmargin > 0 ? "#009999" : "#FF0000";
                        //var cashflowcolor = data1[1].cashflow > 0 ? "#009999" : "#FF0000";

                        Highcharts.chart('container', {
                            chart: {
                                type: 'pie',
                                styledMode: true
                            },
                            title: {
                                text: 'AI Project Wise Report',
                                align: 'center'
                            },
                            subtitle: {
                                text: '',
                                align: ''
                            },

                            accessibility: {
                                announceNewData: {
                                    enabled: true
                                },
                                point: {
                                    valueSuffix: '%'
                                }
                            },

                            plotOptions: {
                                series: {
                                    dataLabels: {
                                        enabled: true,
                                        format: '{point.name}: {point.y:.1f}'
                                    }
                                }
                            },
                            legend: {
                                enabled: true,
                                floating: true,
                                borderWidth: 0,
                                align: 'right',
                                layout: 'vertical',
                                verticalAlign: 'right',
                                labelFormatter: function () {
                                    return '<span style="color:{point.color}">' + this.name + ': </span>' + this.y + '<br/>';
                                }
                            },
                            tooltip: {
                                headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                                pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b><br/>'
                            },

                            series: [
                                {
                                    name: '',
                                    colorByPoint: true,
                                    data: [
                                        {
                                            name: 'Total',
                                            y: data1[0].total,
                                            "color": '#8A2BE2'
                                        },
                                        {
                                            name: 'Annotor',
                                            y: data1[0].qa1work,
                                            "color": '#808080'
                                        },
                                        {
                                            name: 'QA1',
                                            y: data1[0].qa2work,
                                            "color": '#C5D930'
                                        },
                                        {
                                            name: 'QA2',
                                            y: data1[0].qa3work,
                                            "color": '#96D5DF'
                                        }

                                    ],
                                    size: '90%',
                                    innerSize: '55%',
                                    showInLegend: true,
                                    dataLabels: {
                                        enabled: true
                                    }
                                }
                            ],
                        });
                    },
                    failure: function (response) {
                        //  alert(response);
                        console.log('failure', response);
                        alert("f");
                    }

                });


            } catch (e) {
                alert(e);
            }

        }
    </script>
    <style>
        .highcharts-figure,
        .highcharts-data-table table {
            min-width: 320px;
            max-width: 660px;
            margin: 1em auto;
        }

        .highcharts-data-table table {
            font-family: Verdana, sans-serif;
            border-collapse: collapse;
            border: 1px solid #ebebeb;
            margin: 10px auto;
            text-align: center;
            width: 100%;
            max-width: 500px;
        }

        .highcharts-data-table caption {
            padding: 1em 0;
            font-size: 1.2em;
            color: #555;
        }

        .highcharts-data-table th {
            font-weight: 600;
            padding: 0.5em;
        }

        .highcharts-data-table td,
        .highcharts-data-table th,
        .highcharts-data-table caption {
            padding: 0.5em;
        }

        .highcharts-data-table thead tr,
        .highcharts-data-table tr:nth-child(even) {
            background: #f8f8f8;
        }

        .highcharts-data-table tr:hover {
            background: #f1f7ff;
        }
    </style>

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

            <div class="card mt-4">
                <div class="row">
                    <div class="col-md-2">
                        <div class="shadow-lg p-1 mb-5 bg-body rounded">
                            <div class="text-center">
                                <h6>Total Batch</h6>
                            </div>
                            <h3 class="text-center">
                                <asp:Label runat="server" ID="lbltotalbatch"></asp:Label></h3>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-1 mb-5 bg-body rounded">
                            <div class="text-center">
                                <h6>Total QA1</h6>
                            </div>
                            <h3 class="text-center" id="H1" runat="server">
                                <asp:Label runat="server" ID="lbltotalqa1"></asp:Label></h3>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-1 mb-5 bg-body rounded">
                            <div class="text-center">
                                <h6>Total QA2</h6>
                            </div>
                            <h3 class="text-center" id="H2" runat="server">
                                <asp:Label runat="server" ID="lbltotalqa2"></asp:Label></h3>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-1 mb-5 bg-body rounded">
                            <div class="text-center">
                                <h6>Total QA3</h6>
                            </div>
                            <h3 class="text-center" id="H3" runat="server">
                                <asp:Label runat="server" ID="lbltotalqa3"></asp:Label></h3>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-1 mb-5 bg-body rounded">
                            <div class="text-center">
                                <h6>Total Work</h6>
                            </div>
                            <h3 class="text-center" id="H4" runat="server">
                                <asp:Label runat="server" ID="lbltotaltask"></asp:Label></h3>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="shadow-lg p-1 mb-5 bg-body rounded">
                            <div class="text-center">
                                <h6>Complete</h6>
                            </div>
                            <h3 class="text-center" id="H5" runat="server">
                                <asp:Label runat="server" ID="lblcomplete"></asp:Label></h3>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-lg-6 text-center">
                        <div id="container" style="height: 360px; width: 600px;"></div>
                    </div>
                    <div class="col-lg-6" style="width: 100%">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center" runat="server" id="doninstnace">25K</h2>
                                    <div class="text-center">
                                        <p>Total Number of Complate class insatance</p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="attinstance">0</h2>
                                    <div class="text-center">
                                        <p>Number of Complete attribute insatance</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center" runat="server" id="qaspent">0.1<small>hrs</small></h2>
                                    <div class="text-center">
                                        <p>
                                            Out of 770.4hrs 
                                    <br />
                                            <b class="text-primary">QA hours spent</b>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="annotspent">410.8<small>hrs</small></h2>
                                    <div class="text-center">
                                        <p>
                                            Out of 770.4hrs
                                    <br />
                                            <b class="text-primary">Annot, hours spent</b>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="adminspnt">359.4<small>hrs</small></h2>
                                    <div class="text-center">
                                        <p>
                                            Out of 770.4hrs 
                                    <br />
                                            <b class="text-primary">Admin hours spent</b>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="ttlskip">0.00</h2>
                                    <div class="text-center">
                                        <p>
                                            Out of 770.4hrs 
                                    <br />
                                            <b class="text-primary">Total Number of Skip</b>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />


            </div>
            <div class="card">

                <div class="card-header bg-light"><span class="font-weight-bold text-muted">Users</span></div>
                <div class="table-responsive">
                    <table class="table">
                        <!-- thead -->
                        <thead>
                            <tr>

                                <th>Users </th>
                                <th>Role Type </th>
                                <th>Work Hours </th>
                                <th></th>

                            </tr>
                        </thead>
                        <!-- /thead -->
                        <!-- tbody -->
                        <tbody>
                            <!-- tr -->
                            <tr>

                                <td>
                                    <a href="#" class="tile tile-img mr-1">
                                        <img class="img-fluid" src="../Upload/UserImages/3101001.png" alt="Card image cap"></a> <a href="#">NAhid</a>
                                </td>
                                <td class="align-center">Annotor </td>
                                <td class="align-center">4 Hurs </td>

                                <td class="align-center text-right">
                                    <a href="#" class="btn btn-sm btn-icon btn-secondary"><i class="fa fa-eye"></i><span class="sr-only">View</span></a>

                                </td>
                            </tr>
                            <!-- /tr -->
                            <!-- tr -->
                            <tr>

                                <td>
                                    <a href="#" class="tile tile-img mr-1">
                                        <img class="img-fluid" src="../Upload/UserImages/3101001.png" alt="Card image cap"></a> <a href="#">Robi</a>
                                </td>
                                <td class="align-middle">Annotor </td>
                                <td class="align-middle">2 Hurs </td>

                                <td class="align-middle text-right">
                                    <a href="#" class="btn btn-sm btn-icon btn-secondary"><i class="fa fa-eye"></i><span class="sr-only">View</span></a>

                                </td>
                            </tr>
                            <!-- /tr -->

                        </tbody>
                        <tfoot>
                            <tr>
                                <td>Total</td>
                                <td></td>
                                <td>6 hours</td>
                                <td></td>
                            </tr>
                        </tfoot>
                        <!-- /tbody -->
                    </table>

                    <asp:GridView ID="gv_UserAnalytic" runat="server" AutoGenerateColumns="False"
                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15" Width="100%">
                        <Columns>

                            <asp:TemplateField HeaderText="SL # ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right; font-size: 12px;"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                        ForeColor="Black"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="lbluser" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="500px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role">
                                <ItemTemplate>
                                    <asp:Label ID="lblbatchid" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Hours">
                                <ItemTemplate>
                                    <asp:Label ID="lblworkour" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle CssClass="gvPagination" />

                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
