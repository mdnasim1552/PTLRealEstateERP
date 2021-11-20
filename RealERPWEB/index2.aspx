<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="index2.aspx.cs" Inherits="RealERPWEB.index2" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    <script src="<%=this.ResolveUrl("~/Scripts/highchartexporting.js")%>"></script>

    <script type="text/javascript">
         
        $(document).ready(function () {           
            var url = $('#<%=this.ParentDir.ClientID %>').val();
            GetData();  
        });

        
        function createItem(selyear) {
            localStorage.setItem("year", selyear);
        };
        function GetData() {
            try {
                comcod = <%=this.GetCompCode()%>;
                var temp = comcod.toString();
                var com = temp.slice(0, 1);
                if (com == "1") {
                  //  $("#dpSales").hide();
                  //  $("#dpCRM").hide();
                }
            }
            catch (e) {
                alert(e);
            }

        };

        function ExecuteGraph(data, data1, data2, data3, data4, gtype) {

            var saldata = JSON.parse(data);
            var purdata = JSON.parse(data1);
            var accdata = JSON.parse(data2);
            var consdata = JSON.parse(data3);
            var sucondata = JSON.parse(data4);

            var chartsal = Highcharts.chart('salchartG', {
                chart: {
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0
                    }
                },
                series: [{
                    name: 'Target',
                    data: [saldata[0].targtsaleamtcore,
                    saldata[1].targtsaleamtcore,
                    saldata[2].targtsaleamtcore,
                    saldata[3].targtsaleamtcore,
                    saldata[4].targtsaleamtcore,
                    saldata[5].targtsaleamtcore,
                    saldata[6].targtsaleamtcore,
                    saldata[7].targtsaleamtcore,
                    saldata[8].targtsaleamtcore,
                    saldata[9].targtsaleamtcore,
                    saldata[10].targtsaleamtcore,
                    saldata[11].targtsaleamtcore
                    ],
                    color: '#759ABE'

                },
                {
                    name: 'Actual',
                    //color:red,
                    data: [saldata[0].ttlsalamtcore,
                    saldata[1].ttlsalamtcore,
                    saldata[2].ttlsalamtcore,
                    saldata[3].ttlsalamtcore,
                    saldata[4].ttlsalamtcore,
                    saldata[5].ttlsalamtcore,
                    saldata[6].ttlsalamtcore,
                    saldata[7].ttlsalamtcore,
                    saldata[8].ttlsalamtcore,
                    saldata[9].ttlsalamtcore,
                    saldata[10].ttlsalamtcore,
                    saldata[11].ttlsalamtcore
                    ],
                    color: 'black'
                }],
                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: 500
                        },
                        chartOptions: {
                            chart: {
                                height: 300
                            },
                            subtitle: {
                                text: null
                            },
                            navigator: {
                                enabled: false
                            }
                        }
                    }]
                }
            });




            //End of Sales Chart
            //Start of Sales Chart

            var chartpur = Highcharts.chart('purchart', {


                chart: {
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''

                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Purchase',
                    data: [purdata[0].ttlsalamtcore,
                    purdata[1].ttlsalamtcore,
                    purdata[2].ttlsalamtcore,
                    purdata[3].ttlsalamtcore,
                    purdata[4].ttlsalamtcore,
                    purdata[5].ttlsalamtcore,
                    purdata[6].ttlsalamtcore,
                    purdata[7].ttlsalamtcore,
                    purdata[8].ttlsalamtcore,
                    purdata[9].ttlsalamtcore,
                    purdata[10].ttlsalamtcore,
                    purdata[11].ttlsalamtcore
                    ],
                    color: '#759ABE'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [purdata[0].tpayamtcore,
                    purdata[1].tpayamtcore,
                    purdata[2].tpayamtcore,
                    purdata[3].tpayamtcore,
                    purdata[4].tpayamtcore,
                    purdata[5].tpayamtcore,
                    purdata[6].tpayamtcore,
                    purdata[7].tpayamtcore,
                    purdata[8].tpayamtcore,
                    purdata[9].tpayamtcore,
                    purdata[10].tpayamtcore,
                    purdata[11].tpayamtcore
                    ],
                    color: 'black'
                }]
            });
            //End of Purchase
            //Start of Accounts Chart
            var chartacc = Highcharts.chart('accchart', {
                chart: {
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''

                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Receipt',
                    data: [accdata[0].cramcore,
                    accdata[1].cramcore,
                    accdata[2].cramcore,
                    accdata[3].cramcore,
                    accdata[4].cramcore,
                    accdata[5].cramcore,
                    accdata[6].cramcore,
                    accdata[7].cramcore,
                    accdata[8].cramcore,
                    accdata[9].cramcore,
                    accdata[10].cramcore,
                    accdata[11].cramcore


                    ],
                    color: '#759ABE'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [accdata[0].dramcore,
                    accdata[1].dramcore,
                    accdata[2].dramcore,
                    accdata[3].dramcore,
                    accdata[4].dramcore,
                    accdata[5].dramcore,
                    accdata[6].dramcore,
                    accdata[7].dramcore,
                    accdata[8].dramcore,
                    accdata[9].dramcore,
                    accdata[10].dramcore,
                    accdata[11].dramcore
                    ],
                    color: 'black'
                }]
            });


            //Start of Construction Chart
            var chartcons = Highcharts.chart('conschart', {


                chart: {
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''

                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Target',
                    data: [consdata[0].taramtcore,
                    consdata[1].taramtcore,
                    consdata[2].taramtcore,
                    consdata[3].taramtcore,
                    consdata[4].taramtcore,
                    consdata[5].taramtcore,
                    consdata[6].taramtcore,
                    consdata[7].taramtcore,
                    consdata[8].taramtcore,
                    consdata[9].taramtcore,
                    consdata[10].taramtcore,
                    consdata[11].taramtcore
                    ],
                    color: '#759ABE'

                }, {

                    name: 'Actual',
                    //color:red,
                    data: [consdata[0].examtcore,
                    consdata[1].examtcore,
                    consdata[2].examtcore,
                    consdata[3].examtcore,
                    consdata[4].examtcore,
                    consdata[5].examtcore,
                    consdata[6].examtcore,
                    consdata[7].examtcore,
                    consdata[8].examtcore,
                    consdata[9].examtcore,
                    consdata[10].examtcore,
                    consdata[11].examtcore
                    ],
                    color: 'Black'
                }]
            });
            //Start of Construction Chart
            var chartsubcon = Highcharts.chart('subconchart', {
                chart: {
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''

                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Bill',
                    data: [sucondata[0].tcbamtcore,
                    sucondata[1].tcbamtcore,
                    sucondata[2].tcbamtcore,
                    sucondata[3].tcbamtcore,
                    sucondata[4].tcbamtcore,
                    sucondata[5].tcbamtcore,
                    sucondata[6].tcbamtcore,
                    sucondata[7].tcbamtcore,
                    sucondata[8].tcbamtcore,
                    sucondata[9].tcbamtcore,
                    sucondata[10].tcbamtcore,
                    sucondata[11].tcbamtcore
                    ],
                    color: '#759ABE'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [sucondata[0].tcbpayamtcore,
                    sucondata[1].tcbpayamtcore,
                    sucondata[2].tcbpayamtcore,
                    sucondata[3].tcbpayamtcore,
                    sucondata[4].tcbpayamtcore,
                    sucondata[5].tcbpayamtcore,
                    sucondata[6].tcbpayamtcore,
                    sucondata[7].tcbpayamtcore,
                    sucondata[8].tcbpayamtcore,
                    sucondata[9].tcbpayamtcore,
                    sucondata[10].tcbpayamtcore,
                    sucondata[11].tcbpayamtcore
                    ],
                    color: 'Black'
                }]
            });

            let w = $(".graph-main").width();
            let h = 325;
            chartsal.setSize(w, h);
            chartpur.setSize(w, h);
            chartacc.setSize(w, h);
            chartcons.setSize(w, h);
            chartsubcon.setSize(w, h);
            const elem = $(".graph-main")[0];

            let resizeObserver = new ResizeObserver(function () {
                chartsal.setSize(w, h);
                chartpur.setSize(w, h);
                chartacc.setSize(w, h);
                chartcons.setSize(w, h);
                chartsubcon.setSize(w, h);
                w = $(".graph-main").width();
            });
            resizeObserver.observe(elem);
        }
    </script>
    
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

            <div class="page">
                <div class="page-inner">
                    <div style="display: none;">
                        <asp:TextBox ID="ParentDir" runat="server" CssClass="hide"></asp:TextBox>
                        
                    </div>

                </div>
                <div class="page-section">
                    <div class="section-block mt-0 mb-0">
                        <!-- <h1 class="section-title">Overview</h1> -->
                        <!-- metric row -->
                        <div class="metric-row mt-0 mb-0">
                            <div class="col-12">
                                <section class="card pt-0 pb-0 mb-1 card-fluid">

                                    <div id="divgsraph" class="userGraph" runat="server">
                                        <!-- .card-header -->
                                        <header class="card-header pt-0 pb-0">
                                            <div class="d-flex align-items-center">
                                                <div class="mr-auto">
                                                    <ul class="nav userGraphNav" id="userGraph" runat="server">
                                                    </ul>
                                                </div>

                                                <div class="dropdown">

                                                    <div class="form-group mb-0">
                                                        <label class="control-label" for="ddlUserName">Year</label>





                                                        <asp:DropDownList ID="ddlyearSale" runat="server" OnSelectedIndexChanged="ddlyearSale_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                                            <asp:ListItem Value="2021" Selected="True">2021</asp:ListItem>

                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlMonths" runat="server" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                            <asp:ListItem Selected Value="00">All Months</asp:ListItem>
                                                            <asp:ListItem Value="Jan">Jan</asp:ListItem>
                                                            <asp:ListItem Value="Feb">Feb</asp:ListItem>
                                                            <asp:ListItem Value="Mar">Mar</asp:ListItem>
                                                            <asp:ListItem Value="Apr">Apr</asp:ListItem>
                                                            <asp:ListItem Value="May">May</asp:ListItem>
                                                            <asp:ListItem Value="Jun">Jun</asp:ListItem>
                                                            <asp:ListItem  Value="Jul">Jul</asp:ListItem>
                                                            <asp:ListItem Value="Aug">Aug</asp:ListItem>
                                                            <asp:ListItem Value="Sep">Sep</asp:ListItem>
                                                            <asp:ListItem Value="Oct">Oct</asp:ListItem>
                                                            <asp:ListItem Value="Nov">Nov</asp:ListItem>
                                                            <asp:ListItem Value="Dec">Dec</asp:ListItem>


                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddlGraphtype" runat="server" OnSelectedIndexChanged="ddlGraphtype_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                            <asp:ListItem Value="line">Line</asp:ListItem>
                                                            <asp:ListItem Value="column" Selected="True">Column</asp:ListItem>
                                                            <asp:ListItem Value="area">Area</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>



                                                </div>
                                            </div>
                                            <!-- .nav-tabs -->

                                            <!-- /.nav-tabs -->
                                        </header>
                                        <!-- /.card-header -->
                                        <!-- .card-body -->
                                        <div class="card-body pt-0 pb-0">
                                            <!-- .tab-content -->
                                            <div id="myTabContent" class="tab-content graph-main" style="width: 100%; height: 375px;">


                                                <div class="tab-pane fade show" id="tab_1231" runat="server">
                                                    <div class="row ">

                                                        <div class="col-md-12 xtext-righ pt-1 pb-1">
                                                            <h5><small class="float-right">


                                                                <div class="btn-group show-on-hover grMoreMenu">
                                                                    <button type="button" class="btn btn-sm btn-default dropdown-toggle" data-toggle="dropdown">
                                                                        More Reports 
                                                                    </button>
                                                                    <ul class="dropdown-menu" role="menu">
                                                                        <li><a href="F_34_Mgt/RptAllDashboard.aspx?Type=Sales">Sales All Graph</a></li>
                                                                        <li>
                                                                            <asp:HyperLink runat="server" Target="_blank" ID="Hypersales" ClientIDMode="Static">Sales Details</asp:HyperLink>


                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </small></h5>

                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="pnlMonthlySales" runat="server">
                                                        <div id="MonthlySales" style="height: 280px; margin: 0 auto"></div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="pnlsalchart" runat="server">
                                                        <div id="salchart" style="width: 90%; max-height: 280px;"></div>
                                                    </asp:Panel>


                                                </div>
                                                <div class="tab-pane fade show" id="tab_1232" runat="server">

                                                    <div class="row ">

                                                        <div class="col-md-12 xtext-right pt-1 pb-1">
                                                            <h5><small class="float-right">
                                                                <div class="btn-group show-on-hover grMoreMenu">
                                                                    <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown">
                                                                        More Reports 
                                                                    </button>
                                                                    <ul class="dropdown-menu" role="menu">
                                                                        <li><a href="F_34_Mgt/RptAllDashboard.aspx?Type=Purchase">Procurement All Graph</a></li>
                                                                        <li>

                                                                            <asp:HyperLink runat="server" Target="_blank" ID="HyperProcurement" ClientIDMode="Static">Procurement Details</asp:HyperLink>

                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </small></h5>

                                                        </div>

                                                    </div>
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <div id="MonthlyPurchase" style="height: 280px; margin: 0 auto"></div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <div id="purchart" style="width: 90%; max-height: 280px;"></div>
                                                    </asp:Panel>





                                                </div>
                                                <div class="tab-pane fade show" id="tab_1233" runat="server">
                                                    <div class="row ">
                                                        <div class="col-md-12 xtext-right pt-1 pb-1">
                                                            <h5><small class="float-right">
                                                                <div class="btn-group show-on-hover grMoreMenu">
                                                                    <button type="button" class="btn btn-sm btn-default dropdown-toggle" data-toggle="dropdown">
                                                                        More Reports 
                                                                    </button>
                                                                    <ul class="dropdown-menu" role="menu">
                                                                        <li><a href="F_34_Mgt/RptAllDashboard.aspx?Type=Accounts">Accounts All Graph</a></li>
                                                                        <li>

                                                                            <asp:HyperLink runat="server" Target="_blank" ID="HypAccounts" ClientIDMode="Static">Accounts Details</asp:HyperLink>

                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </small></h5>

                                                        </div>


                                                    </div>

                                                    <asp:Panel ID="Panel3" runat="server">
                                                        <div id="MonthlyAccounts" style="height: 280px; margin: 0 auto"></div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="Panel4" runat="server">
                                                        <div id="accchart" style="width: 90%; max-height: 280px;"></div>
                                                    </asp:Panel>




                                                </div>
                                                <div class="tab-pane fade show" id="tab_1234" runat="server">


                                                    <div class="row ">
                                                        <div class="col-md-12 xtext-right pt-1 pb-1">
                                                            <h5><small class="float-right">
                                                                <div class="btn-group show-on-hover grMoreMenu">
                                                                    <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown">
                                                                        More Reports 
                                                                    </button>
                                                                    <ul class="dropdown-menu" role="menu">
                                                                        <li><a href="F_34_Mgt/RptAllDashboard.aspx?Type=Construction">Construction All Graph</a></li>
                                                                        <li>

                                                                            <asp:HyperLink runat="server" Target="_blank" ID="hypConstruction" ClientIDMode="Static">Construction Details</asp:HyperLink>


                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </small></h5>

                                                        </div>


                                                    </div>
                                                    <asp:Panel ID="Panel5" runat="server">
                                                        <div id="Monthlyconschart" style="height: 280px; margin: 0 auto"></div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="Panel6" runat="server">
                                                        <div id="conschart" style="width: 90%; max-height: 280px;"></div>
                                                    </asp:Panel>





                                                </div>
                                                <div class="tab-pane fade show" id="tab_1235" runat="server">
                                                    <div class="col-md-12 xtext-right pt-1 pb-1">
                                                        <h5>

                                                            <small class="float-right">
                                                                <div class="btn-group show-on-hover grMoreMenu">
                                                                    <button type="button" class="btn btn-sm btn-default dropdown-toggle" data-toggle="dropdown">
                                                                        More Reports 
                                                                    </button>
                                                                    <ul class="dropdown-menu" role="menu">

                                                                        <li>
                                                                            <asp:HyperLink runat="server" Target="_blank" ID="lblSubContractor" ClientIDMode="Static">Sub-Contractor Details</asp:HyperLink>


                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </small>

                                                        </h5>

                                                    </div>


                                                    <asp:Panel ID="Panel7" runat="server">
                                                        <div id="Monthlysubconchart" style="height: 280px; margin: 0 auto"></div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="Panel8" runat="server">
                                                        <div id="subconchart" style="width: 90%; max-height: 280px;"></div>
                                                    </asp:Panel>



                                                </div>


                                                <div class="tab-pane fade show" id="tab_1236" runat="server">
                                                    <div class="col-md-12 xtext-right pt-1 pb-1">
                                                        <h5>

                                                            <small class="float-right">
                                                                <div class="btn-group show-on-hover grMoreMenu">
                                                                    <button type="button" class="btn btn-sm btn-default dropdown-toggle" data-toggle="dropdown">
                                                                        More Reports 
                                                                    </button>
                                                                    <ul class="dropdown-menu" role="menu">

                                                                        <li>
                                                                            <asp:HyperLink runat="server" Target="_blank" ID="HyperLink1" ClientIDMode="Static">Sub-Contractor Details</asp:HyperLink>


                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </small>

                                                        </h5>

                                                    </div>


                                                    <div id="memberdt" style="width: 295px; height: 220px; margin: 0 auto"></div>
                                                    <div id="attendt" style="width: 290px; height: 220px; margin: 0 auto"></div>




                                                </div>

                                                <div class="tab-pane fade show" id="tab_1343" runat="server">
                                                    <div class="col-md-12 xtext-right pt-1 pb-1">
                                                        <h5>

                                                            <small class="float-right">
                                                                <div class="btn-group show-on-hover grMoreMenu">
                                                                    <button type="button" class="btn btn-sm btn-default dropdown-toggle" data-toggle="dropdown">
                                                                        More Reports 
                                                                    </button>
                                                                    <ul class="dropdown-menu" role="menu">

                                                                        <li>
                                                                            <asp:HyperLink runat="server" Target="_blank" ID="hypCrmDetails" ClientIDMode="Static">CRM Details</asp:HyperLink>


                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </small>

                                                        </h5>

                                                    </div>


                                                    <asp:Panel ID="Panel9" runat="server" Width="500" Style="margin: 0 auto;">
                                                        <div id="crmChart" style="height: 280px; width: 450px; margin: 0 auto"></div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="Panel10" runat="server">
                                                        <div id="crmChartMonthly" style="height: 280px; margin: 0 auto"></div>
                                                    </asp:Panel>



                                                </div>


                                            </div>
                                            <!-- /.tab-content -->
                                        </div>
                                        <!-- /.card-body -->
                                    </div>
                                </section>
                            </div>


                        </div>

                    </div>
                </div>
            </div>



        
</asp:Content>
