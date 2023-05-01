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
                                            "color": '#1F2F98'
                                        },
                                        {
                                            name: 'Annotor',
                                            y: data1[0].qa1work,
                                            "color": '#7BE495'
                                        },
                                        {
                                            name: 'QA1',
                                            y: data1[0].qa2work,
                                            "color": '#85CBCC'
                                        },
                                        {
                                            name: 'QA2',
                                            y: data1[0].qa3work,
                                            "color": '#4A707A'
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

        .card-counter {
            box-shadow: 2px 2px 10px #DADADA;
            margin: 5px;
            padding: 20px 10px;
            background-color: #fff;
            height: 100px;
            border-radius: 5px;
            transition: .3s linear all;
        }

            .card-counter:hover {
                box-shadow: 4px 4px 20px #DADADA;
                transition: .3s linear all;
            }

            .card-counter.primary {
                background-color: #007bff;
                color: #FFF;
            }

            .card-counter.danger {
                background-color: #ef5350;
                color: #FFF;
            }

            .card-counter.success {
                background-color: #66bb6a;
                color: #FFF;
            }

            .card-counter.info {
                background-color: #26c6da;
                color: #FFF;
            }

            .card-counter i {
                font-size: 5em;
                opacity: 0.2;
            }

            .card-counter .count-numbers {
                position: absolute;
                right: 35px;
                top: 20px;
                font-size: 32px;
                display: block;
            }

            .card-counter .count-name {
                position: absolute;
                right: 35px;
                top: 65px;
                font-style: italic;
                text-transform: capitalize;
                opacity: 0.5;
                display: block;
                font-size: 18px;
            }

        #color {
           background-image: linear-gradient(to right top, #051937, #004d7a, #008793, #00bf72, #a8eb12);
        }
        #colorwork{
            background-image: linear-gradient(to left bottom, #370521, #503868, #3e72ad, #00afdc, #12ebeb);
        }
       #totalcolor{
            background-image: linear-gradient(to left, #7f6c77, #6b6478, #4d5e75, #2b5868, #115151);
        }
        #colorqa1{
           background-image: linear-gradient(to top, #2d2730, #413352, #504279, #5553a4, #4967d3);
        }
        #colorannotor{
            background-image: linear-gradient(to right bottom, #5e6fde, #7e69e1, #9c62e0, #b857db, #d349d3);
        }
         #colorqa2
        {
            background-image: linear-gradient(to left bottom, #69187a, #9c2173, #c13c6a, #db5f63, #ec8561);
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
                <div class="container-fluid well">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="card-counter text-white" id="totalcolor">
                                <i class="fa fa-university"></i>
                                <span class="count-numbers">                                   
                                        <asp:Label runat="server" ID="lbltotalbatch"></asp:Label>
                                </span>
                                <span class="count-name">Total Batch</span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="card-counter text-white" id="colorannotor">
                                <i class="fa fa-certificate"></i>
                                <span class="count-numbers">                                    
                                        <asp:Label runat="server" ID="lbltotalqa1"></asp:Label>
                                </span>
                                <span class="count-name text-white">Total Assign Annotor</span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="card-counter text-white" id="colorqa1">
                                <i class="fa fa-user"></i>
                                <span class="count-numbers">
                                    <asp:Label runat="server" ID="lbltotalqa2"></asp:Label></span>
                                <span class="count-name">Total Assign QA1</span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="card-counter text-white" id="colorqa2">
                                <i class="fa fa-users"></i>
                                <span class="count-numbers">
                                    <asp:Label runat="server" ID="lbltotalqa3"></asp:Label></span>
                                <span class="count-name">Total Assign QA2</span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="card-counter text-white" id="colorwork">
                                <i class="fa fa-star"></i>
                                <span class="count-numbers">
                                    <asp:Label runat="server" ID="lbltotaltask"></asp:Label></span>
                                <span class="count-name">Total Work</span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="card-counter text-white" id="color">
                                <i class="fa fa-database"></i>
                                <span class="count-numbers ">
                                    <asp:Label runat="server" ID="lblcomplete"></asp:Label></span>
                                <span class="count-name">Complete</span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-6 text-center">
                        <div id="container" style="height: 330px; width: 700px;"></div>
                    </div>


                    <div class="col-lg-6" style="width: 100%">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="doninstnace"></h2>
                                    <div class="text-center">
                                        <p>
                                             Out of <asp:Label class="text-center text-primary" runat="server" ID="lblprjquantity"></asp:Label> Quantity
                                            <br />
                                            Total Number of Complate Quantity

                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="projtecthour"></h2>
                                    <div class="text-center">
                                        <p>
                                           <span class="text-center text-primary" >Total Project Quantity</span>
                                            <br />
                                            Total Hour Of Project
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="annotorspent"><small>hrs</small></h2>
                                    <div class="text-center">
                                        <p>
                                          Out of <asp:Label runat="server" ID="Totalhour1"></asp:Label> Hour
                                    <br />
                                            <b class="text-primary">Annotor hours spent</b>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="qa1spent"><small>hrs</small></h2>
                                    <div class="text-center">
                                        <p>
                                             Out of <asp:Label runat="server" ID="lbltotalhour2"></asp:Label> Hour
                                    <br />
                                            <b class="text-primary">QA1, hours spent</b>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="metric metric-bordered align-items-center">
                                    <h2 class="text-center text-primary" runat="server" id="qa2spnt"><small>hrs</small></h2>
                                    <div class="text-center">
                                        <p>
                                            Out of <asp:Label runat="server" ID="lbltotalhour3"></asp:Label> Hour
                                    <br />
                                            <b class="text-primary">QA2 hours spent</b>
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
                                        <img class="img-fluid" src="../Upload/UserImages/3101001.png" alt="Card image cap"></a> <a href="#">Robix</a>
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
