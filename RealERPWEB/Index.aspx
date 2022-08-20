<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="RealERPWEB.Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        .tblh {
            background: #DFF0D8;
            height: 30px;
            text-align: center;
        }

        .th1 {
            min-width: 100px;
            text-align: center;
        }

        .th2 {
            min-width: 200px;
            text-align: center;
        }

        .th3 {
            min-width: 40px;
            text-align: center;
        }

        a:hover {
            background-color: #D8E7D1;
        }

        .saldeshtml {
            /*background-image: -webkit-linear-gradient(270deg, #334466 100%, #346CB0 100%);*/
        }

        .purdeshtml {
            /*background-image: -webkit-linear-gradient(270deg, #334466 100%, #346CB0 100%);*/
            /*background-image: -webkit-linear-gradient(270deg, #11998e 0%, #38ef7d 100%);*/
        }

        .accdeshtml {
            /*background-image: -webkit-linear-gradient(270deg, #334466 100%, #346CB0 100%);*/
            /*background-image: -webkit-linear-gradient(90deg, #ee0979 0%, #ff6a00 100%);*/
        }

        .consdeshtml {
            /*background-image: -webkit-linear-gradient(270deg, #334466 100%, #346CB0 100%);*/
            /*background-image: -webkit-linear-gradient(270deg, #45b649 0%, #dce35b 100%);*/
        }

        .crmdeshtml {
            /*background-image: -webkit-linear-gradient(270deg, #334466 100%, #346CB0 100%);*/
            /*background: linear-gradient(to right, #01a9ac, #01dbdf);*/
        }

        .list-group-divider .list-group-item-body {
            padding: 0;
        }

        .has-badge > .tile:last-child {
            line-height: 2.05;
            border-radius: 15px;
        }

        .metric-row a {
            cursor: pointer;
            text-decoration: none;
        }

            .metric-row a:hover {
                cursor: pointer;
                text-decoration: none;
            }

        .textfont16 {
            font-size: 15px;
        }

        .metric_cus {
            padding: 10px;
            margin: 0 0 10px 0 !important;
        }

            .metric_cus .tile-lg {
                font-size: 14px;
                background-color: gray;
                color: #fff;
                width: 2rem;
                height: 2rem;
            }

        .userGraph .card-header {
            padding: 5px 15px;
        }

        .userGraphNav .active > .userGraphNav .nav-link, .userGraphNav .nav-link.active {
            background-color: #D8E7D1 !important;
        }

        .bestemp {
            position: absolute;
            top: -20px;
            left: 42%;
            width: 30px;
            margin: 0 auto;
            color: gold;
        }

        .table td, .table th {
            padding: 10px 5px;
            border: 1px solid #dddddd !important;
        }

            .table td span {
                float: right;
            }

        .table th {
            font-weight: 500 !important;
        }

        .grpattn {
            width: 100%;
        }

        .grMoreMenu .btn {
            outline: 0;
            box-shadow: 0 0 0 1px #346cb0;
        }

        .grMoreMenu ul {
            top: 30px !important;
            left: -40px !important;
        }

            .grMoreMenu ul li {
                padding: 5px;
            }

                .grMoreMenu ul li a {
                    display: block;
                    padding: 0 12px;
                    line-height: 18px;
                    color: #363642;
                }

                    .grMoreMenu ul li a:hover {
                        color: #346cb0 !important;
                        background: none;
                    }
    </style>

    <%--<script src="Scripts/jquery-3.1.1.js"></script>--%>
    <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    <script src="<%=this.ResolveUrl("~/Scripts/highchartexporting.js")%>"></script>

    <script type="text/javascript">



        $(document).ready(function () {

            var url = $('#<%=this.ParentDir.ClientID %>').val();
            GetData();


            document.getElementById('<%= lnkbtnOk.ClientID %>').click();



        });

        function createItem(selyear) {
            localStorage.setItem("year", selyear);
        }
        function GetData() {
            try {

                comcod = <%=this.GetCompCode()%>;
                var temp = comcod.toString();
                var com = temp.slice(0, 1);
                if (com == "1") {
                    $("#dpSales").hide();
                    $("#dpCRM").hide();
                }
            }
            catch (e) {
                alert(e);
            }

        };

        function ExecuteGraph(data, data1, data2, data3, data4, gtype, crm, leadname, emplead, hrm, deptwise, last7days) {
            gtype = (gtype == "" ? "column" : gtype);
            // ExcuteEmpStatus();
            var saldata = JSON.parse(data);
            var purdata = JSON.parse(data1);
            var accdata = JSON.parse(data2);
            var consdata = JSON.parse(data3);
            var sucondata = JSON.parse(data4);
            var cmrData = JSON.parse(crm);

            var leadlist = JSON.parse(leadname);
            var emplead = JSON.parse(emplead);

            var hrmData = JSON.parse(hrm);

            var deptwise = JSON.parse(deptwise);
            var last7days = JSON.parse(last7days);

            var chartsal = Highcharts.chart('salchart', {
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
                        text: 'Amount Core Tk '
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
                        text: 'Amount Core TK'
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
                        text: 'Amount Core TK'
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
                        text: 'Amount Core TK'
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
                        text: 'Amount Core TK'
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

            //console.log(saldata[0].lead);
            var chartcmrData = Highcharts.chart('crmChart', {


                chart: {
                    type: gtype
                },
                title: {
                    text: 'Sales Funnel'
                },
                subtitle: {
                    text: ''
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total Sales Funnel Stages'
                    }

                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b> of total ' + parseFloat(cmrData[0].query) + '<br/>'
                },

                series: [
                    {
                        name: "Sales Funnel",
                        colorByPoint: true,
                        data: [
                            {
                                name: leadlist[0].la,
                                y: parseFloat(cmrData[0].query)
                            },
                            {
                                name: leadlist[0].lb,
                                y: parseFloat(cmrData[0].lead)
                            },
                            {
                                name: leadlist[0].lc,
                                y: parseFloat(cmrData[0].qualiflead)
                            },
                            {
                                name: leadlist[0].ld,
                                y: parseFloat(cmrData[0].nego)
                            },
                            {
                                name: leadlist[0].le,
                                y: parseFloat(cmrData[0].finalnego)
                            },
                            {
                                name: leadlist[0].lf,
                                y: parseFloat(cmrData[0].win)
                            }


                        ]
                    }
                ]

            });

            var chartHrmData = Highcharts.chart('piechartEMPStatus', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'//gtype
                },
                title: {
                    text: 'Today Attendance'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y}</b>'
                },

                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.y}'
                        }
                    }
                },
                series: [{
                    name: 'Today Attendance Status',
                    colorByPoint: true,
                    data: [{
                        name: 'Present',
                        y: hrmData[0].ttlprsnt,
                        sliced: true,
                        selected: true
                    }, {
                        name: 'Absent',
                        y: hrmData[0].ttlabs,
                    }, {
                        name: 'Leave',
                        y: hrmData[0].ttlleave,
                    }, {
                        name: 'Late',
                        y: hrmData[0].ttllate,
                    }, {
                        name: 'Early Leave',
                        y: hrmData[0].ttlearlv,
                    }]
                }]

            });



            ///Khalil 

            /// Start Lead Info Emplyee wise 

            var sumleademp = 0;
            var allleademp = [];
            for (var i = 0; i < emplead.length; i++) {
                allleademp.push({ "name": emplead[i].usrname, "y": parseFloat(emplead[i].total) })
                sumleademp += parseFloat(emplead[i].total);

            }
            //console.log(allempdata);

            var empwiselead = Highcharts.chart('leadempwise', {
                chart: {
                    type: gtype
                },
                title: {
                    text: 'Employee Wise Lead, Total:-  ' + sumleademp
                },
                subtitle: {
                    text: ''
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total Lead'
                    }
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                },

                series: [
                    {
                        name: "Total Lead",
                        colorByPoint: true,
                        data: allleademp
                    }
                ]

            });

            //End Lead Info Emloyee wise


            /// Department wise employee
            var sumdeptemp = 0;
            var alldeptemp = [];
            for (var i = 0; i < deptwise.length; i++) {
                alldeptemp.push({ "name": deptwise[i].deptname, "y": parseFloat(deptwise[i].total) })
                sumdeptemp += parseFloat(deptwise[i].total);

            }
            //console.log(allempdata);

            var deptemp = Highcharts.chart('deptWisEmp', {
                chart: {
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total Employee ' + sumdeptemp
                    }
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                },

                series: [
                    {
                        name: "Total Employee",
                        colorByPoint: true,
                        data: alldeptemp
                    }
                ]

            });

            //End Department wise employee


            /// Last Seven days 
            //sales
            var dayseries = [];
            var dayprsnt = [];
            var dayabs = [];
            var dayleav = [];
            $.each(last7days, function (i, item) {
                dayseries.push(item.ymonddesc);
                dayprsnt.push(item.present);
                dayabs.push(item.absnt);
                dayleav.push(item.onleave);
                total = item.staff;
            });
            var levAtt7days = Highcharts.chart('lst7daysatt', {

                //   $('#MonthlySales').highcharts({
                chart: {
                    // type: 'line'
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: '',
                    //style: {
                    //    color: '#44994a',
                    //    fontWeight: 'bold'
                    //}
                },
                xAxis: {
                    categories:
                        dayseries,
                    //  minTickInterval: 60 * 1000,
                    //  tickMarkPlacement: 'on',

                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Last 07 Days '
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y}</b></td></tr>',
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

                    name: 'Present',
                    data: dayprsnt, //[daysalcol[2]['totalamt'], daysalcol[3]['totalamt'], daysalcol[4]['totalamt'], daysalcol[5]['totalamt'], daysalcol[6]['totalamt'], daysalcol[7]['totalamt'], daysalcol[8]['totalamt']],
                    color: '#008000'

                },
                {

                    name: 'Absent',
                    //color:red,
                    data: dayabs, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#FF0000'
                }
                    ,
                {

                    name: 'Leave',
                    //color:red,
                    data: dayleav, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#A52A2A'
                }

                ]
            });

            ///End Last seven days 

            ///End Khalil




            let w = $(".graph-main").width();
            let h = 325;
            chartsal.setSize(w, h);
            chartpur.setSize(w, h);
            chartacc.setSize(w, h);
            chartcons.setSize(w, h);
            chartsubcon.setSize(w, h);

            chartcmrData.setSize(500, 325);
           // chartHrmData.setSize(600,325);

            /* empwiselead.setSize(500, 325);*/
     /*       deptemp.setSize(500, 325);*/
            //levAtt7days.setSize(400, 325);


            const elem = $(".graph-main")[0];

            let resizeObserver = new ResizeObserver(function () {
                chartsal.setSize(w, h);
                chartpur.setSize(w, h);
                chartacc.setSize(w, h);
                chartcons.setSize(w, h);
                chartsubcon.setSize(w, h);
                chartcmrData.setSize(500, 325);
                //chartHrmData.setSize(600, 325);

                /* empwiselead.setSize(500, 325);*/
            /*    deptemp.setSize(500, 325);*/
                //levAtt7days.setSize(400, 325);

                w = $(".graph-main").width();
            });
            resizeObserver.observe(elem);

        };

        function ExecuteUserdata(data1) {
            //userdata

            console.log(JSON.parse(data1));
            var userdata = JSON.parse(data1);
            var descdata = [];
            var cdata = [];
            for (var i = 0; i < userdata.length; i++) {
                descdata[i] = [userdata[i].grpdesc];
                cdata[i] = [userdata[i].tcount]
                $('#userdata').append(
                    '<tr class="grvRows"><th scope="row">' + (i + 1) + '</th><td>' + userdata[i].grpdesc
                    + '</td><td align="right">' + userdata[i].tcount + '</td>'
                    + '</tr>'
                );
            }


            //Highcharts.chart('empdata', {
            //    chart: {
            //        type: 'bar'
            //    },
            //    title: {
            //        text: ''
            //    },
            //    xAxis: {
            //        categories: descdata
            //    },
            //    yAxis: {
            //        min: 0,
            //        title: {
            //            text: ''
            //        }
            //    },
            //    legend: {
            //        reversed: true
            //    },
            //    plotOptions: {
            //        series: {
            //            stacking: 'normal'
            //        }
            //    },
            //    series: [{
            //        name: 'Count',
            //        data: cdata
            //    }]
            //});



        };


        function ExecuteMotnhsGraph(data, purdata, dataacc, datacons, datasubcons, gtype, crm, leadname, emplead, hrm, deptwise, last7days) {
            var today = new Date(),
                day = 1000 * 60 * 60 * 24;
            //console.log(day);
            // ExcuteEmpStatus();
            // Set to 00:00:00:000 today
            today.setUTCHours(0);
            today.setUTCMinutes(0);
            today.setUTCSeconds(0);
            today.setUTCMilliseconds(0);
            today = today.getTime();


            var sdata1 = JSON.parse(data);
            var purdata1 = JSON.parse(purdata);
            var dataacc = JSON.parse(dataacc);
            var datacons = JSON.parse(datacons);
            var datasubcons = JSON.parse(datasubcons);
            var cmrData = JSON.parse(crm);


            var leadlist = JSON.parse(leadname);
            var empleadmon = JSON.parse(emplead);


            var hrmData = JSON.parse(hrm);

            var deptwise = JSON.parse(deptwise);
            var last7days = JSON.parse(last7days);

            var total = 0;
            //for (var i = 0; i < sdata1.length; i++) {
            //    total += sdata1[i].ttlsalamtcore;
            //}

            //console.log(total);
            //console.log(gtype);
            //console.log("Nahid");
            //sales
            var monthseries = [];
            var monsaleamt = [];
            var moncolamt = [];
            $.each(sdata1, function (i, item) {
                monthseries.push(item.yearmon1);
                monsaleamt.push(item.targtsaleamtcore);
                moncolamt.push(item.ttlsalamtcore);
                total += item.ttlsalamtcore;
            });
            // console.log(monthseries);
            var MonthlySalesline = Highcharts.chart('MonthlySales', {

                //   $('#MonthlySales').highcharts({
                chart: {
                    // type: 'line'
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Current Month',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories:
                        monthseries,
                    //  minTickInterval: 60 * 1000,
                    //  tickMarkPlacement: 'on',

                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount Core TK'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f} Core TK</b></td></tr>',
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
                    data: monsaleamt, //[daysalcol[2]['totalamt'], daysalcol[3]['totalamt'], daysalcol[4]['totalamt'], daysalcol[5]['totalamt'], daysalcol[6]['totalamt'], daysalcol[7]['totalamt'], daysalcol[8]['totalamt']],
                    color: '#1581C1'

                }, {

                    name: 'Actual',
                    //color:red,
                    data: moncolamt, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#CA6621'
                }]
            });




            //purchase
            var monthseriesp_ur = [];
            var monPuramt = [];
            var monPurcolamt = [];
            $.each(purdata1, function (i, item) {
                monthseriesp_ur.push(item.yearmon1);
                monPuramt.push(item.ttlsalamtcore);
                monPurcolamt.push(item.tpayamtcore);
            });
            // console.log(monthseries);
            var MonthlyPurchase = Highcharts.chart('MonthlyPurchase', {

                //$('#MonthlyPurchase').highcharts({
                chart: {
                    // type: 'line'
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Current Month',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories:
                        monthseriesp_ur
                    ,
                    currentDateIndicator: true,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount Core TK'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f} Core TK</b></td></tr>',
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
                    name: 'Purchase',
                    data: monPuramt, //[daysalcol[2]['totalamt'], daysalcol[3]['totalamt'], daysalcol[4]['totalamt'], daysalcol[5]['totalamt'], daysalcol[6]['totalamt'], daysalcol[7]['totalamt'], daysalcol[8]['totalamt']],
                    color: '#1581C1'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: monPurcolamt, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#CA6621'
                }]
            });

            //MonthlyAccounts

            var monthseries_acc = [];
            var monamt_acc = [];
            var moncolamt_acc = [];
            $.each(dataacc, function (i, item) {
                monthseries_acc.push(item.yearmon1);
                monamt_acc.push(item.ttlsalamtcore);
                moncolamt_acc.push(item.tpayamtcore);
            });
            // console.log(monthseries);
            var MonthlyAccounts = Highcharts.chart('MonthlyAccounts', {

                // $('#MonthlyAccounts').highcharts({
                chart: {
                    // type: 'line'
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Current Month',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories:
                        monthseries_acc
                    ,
                    currentDateIndicator: true,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount Core TK'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f} Core TK</b></td></tr>',
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
                    name: 'Receipt',
                    data: monamt_acc, //[daysalcol[2]['totalamt'], daysalcol[3]['totalamt'], daysalcol[4]['totalamt'], daysalcol[5]['totalamt'], daysalcol[6]['totalamt'], daysalcol[7]['totalamt'], daysalcol[8]['totalamt']],
                    color: '#1581C1'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: moncolamt_acc, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#CA6621'
                }]
            });


            //Monthlyconschart
            var monthseries_cons = [];
            var monamt_cons = [];
            var moncolamt_cons = [];
            $.each(datacons, function (i, item) {
                monthseries_cons.push(item.yearmon1);
                monamt_cons.push(item.taramtcore);
                moncolamt_cons.push(item.examtcore);
            });
            var Monthlyconschart = Highcharts.chart('Monthlyconschart', {

                // console.log(monthseries);

                // $('#Monthlyconschart').highcharts({
                chart: {
                    // type: 'line'
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Current Month',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories:
                        monthseries_cons
                    ,
                    currentDateIndicator: true,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount Core TK'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f} Core TK</b></td></tr>',
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
                    data: monamt_cons, //[daysalcol[2]['totalamt'], daysalcol[3]['totalamt'], daysalcol[4]['totalamt'], daysalcol[5]['totalamt'], daysalcol[6]['totalamt'], daysalcol[7]['totalamt'], daysalcol[8]['totalamt']],
                    color: '#1581C1'

                }, {

                    name: 'Actual',
                    //color:red,
                    data: moncolamt_cons, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#CA6621'
                }]
            });


            //MonthlySUBconschart
            var monthseries_scons = [];
            var monamt_scons = [];
            var moncolamt_scons = [];
            $.each(datasubcons, function (i, item) {
                monthseries_scons.push(item.yearmon1);
                monamt_scons.push(item.tcbamtcore);
                moncolamt_scons.push(item.tcbpayamtcore);
            });
            // console.log(monthseries);
            var Monthlysubconchart = Highcharts.chart('Monthlysubconchart', {

                // $('#Monthlysubconchart').highcharts({
                chart: {
                    // type: 'line'
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Current Month',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories:
                        monthseries_scons
                    ,
                    currentDateIndicator: true,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount Core TK'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f} Core TK</b></td></tr>',
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
                    data: monamt_scons, //[daysalcol[2]['totalamt'], daysalcol[3]['totalamt'], daysalcol[4]['totalamt'], daysalcol[5]['totalamt'], daysalcol[6]['totalamt'], daysalcol[7]['totalamt'], daysalcol[8]['totalamt']],
                    color: '#1581C1'

                }, {

                    name: 'Actual',
                    //color:red,
                    data: moncolamt_scons, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#CA6621'
                }]
            });



            var chartcmrData = Highcharts.chart('crmChart', {


                chart: {
                    type: gtype
                },
                title: {
                    text: 'Sales Funnel'
                },
                subtitle: {
                    text: ''
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total Sales Funnel Stages'
                    }

                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b> of total ' + parseFloat(cmrData[0].query) + '<br/>'
                },

                series: [
                    {
                        name: "Sales Funnel",
                        colorByPoint: true,
                        data: [
                            {
                                name: leadlist[0].la,
                                y: parseFloat(cmrData[0].query)
                            },
                            {
                                name: leadlist[0].lb,
                                y: parseFloat(cmrData[0].lead)
                            },
                            {
                                name: leadlist[0].lc,
                                y: parseFloat(cmrData[0].qualiflead)
                            },
                            {
                                name: leadlist[0].ld,
                                y: parseFloat(cmrData[0].nego)
                            },
                            {
                                name: leadlist[0].le,
                                y: parseFloat(cmrData[0].finalnego)
                            },
                            {
                                name: leadlist[0].lf,
                                y: parseFloat(cmrData[0].win)
                            }


                        ]
                    }
                ]


            });

            var chartHrmData = Highcharts.chart('piechartEMPStatus', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'//gtype
                },
                title: {
                    text: 'Today Attendance'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y}</b>'
                },

                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.y}'
                        }
                    }
                },
                series: [{
                    name: 'Today Attendance Status',
                    colorByPoint: true,
                    data: [{
                        name: 'Present',
                        y: hrmData[0].ttlprsnt,
                        sliced: true,
                        selected: true
                    }, {
                        name: 'Absent',
                        y: hrmData[0].ttlabs,
                    }, {
                        name: 'Leave',
                        y: hrmData[0].ttlleave,
                    }, {
                        name: 'Late',
                        y: hrmData[0].ttllate,
                    }, {
                        name: 'Early Leave',
                        y: hrmData[0].ttlearlv,
                    }]
                }]

            });



            ///Khalil 
            /// Start Lead Info Emplyee wise 

            var sumleadempmonth = 0;
            var allleadempmonth = [];
            for (var i = 0; i < empleadmon.length; i++) {
                allleadempmonth.push({ "name": empleadmon[i].usrname, "y": parseFloat(empleadmon[i].total) })
                sumleadempmonth += parseFloat(empleadmon[i].total);

            }
            //console.log(allempdata);

            var empwiselead = Highcharts.chart('leadempwise', {
                chart: {
                    type: gtype
                },
                title: {
                    text: 'Employee Wise Lead, Total:-  ' + sumleadempmonth
                },
                subtitle: {
                    text: ''
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total Lead'
                    }
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                },

                series: [
                    {
                        name: "Total Lead",
                        colorByPoint: true,
                        data: allleadempmonth
                    }
                ]

            });

            //End Lead Info Emloyee wise


            /// Department wise employee
            var sumdeptemp = 0;
            var alldeptemp = [];
            for (var i = 0; i < deptwise.length; i++) {
                alldeptemp.push({ "name": deptwise[i].deptname, "y": parseFloat(deptwise[i].total) })
                sumdeptemp += parseFloat(deptwise[i].total);

            }
            //console.log(allempdata);

            var deptemp = Highcharts.chart('deptWisEmp', {
                chart: {
                    type: gtype
                },
                title: {
                    text: 'Department Wise Employee, Total:-  ' + sumdeptemp
                },
                subtitle: {
                    text: ''
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total Employee'
                    }
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                },

                series: [
                    {
                        name: "Total Employee",
                        colorByPoint: true,
                        data: alldeptemp
                    }
                ]

            });

            //End Department wise employee


            /// Last Seven days 
            //sales
            var dayseries = [];
            var dayprsnt = [];
            var dayabs = [];
            var dayleav = [];
            $.each(last7days, function (i, item) {
                dayseries.push(item.ymonddesc);
                dayprsnt.push(item.present);
                dayabs.push(item.absnt);
                dayleav.push(item.onleave);
                total = item.staff;
            });
            var lst7leavatt = Highcharts.chart('lst7daysatt', {

                //   $('#MonthlySales').highcharts({
                chart: {
                    // type: 'line'
                    type: gtype
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Last 07 Days',
                    //style: {
                    //    color: '#44994a',
                    //    fontWeight: 'bold'
                    //}
                },
                xAxis: {
                    categories:
                        dayseries,
                    //  minTickInterval: 60 * 1000,
                    //  tickMarkPlacement: 'on',

                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total Employee'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y}</b></td></tr>',
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

                    name: 'Present',
                    data: dayprsnt, //[daysalcol[2]['totalamt'], daysalcol[3]['totalamt'], daysalcol[4]['totalamt'], daysalcol[5]['totalamt'], daysalcol[6]['totalamt'], daysalcol[7]['totalamt'], daysalcol[8]['totalamt']],
                    color: '#008000'

                },
                {

                    name: 'Absent',
                    //color:red,
                    data: dayabs, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#FF0000'
                }
                    ,
                {

                    name: 'Leave',
                    //color:red,
                    data: dayleav, //[daysalcol[2]['colamt'], daysalcol[3]['colamt'], daysalcol[4]['colamt'], daysalcol[5]['colamt'], daysalcol[6]['colamt'], daysalcol[7]['colamt'], daysalcol[8]['colamt']],
                    color: '#A52A2A'
                }

                ]
            });

            ///End Last seven days 


            ///End Khalil



            /// this part for Width set part 

            let w = $(".graph-main").width();
            let h = 325;
            MonthlySalesline.setSize(w, h);
            MonthlyPurchase.setSize(w, h);
            MonthlyAccounts.setSize(w, h);
            Monthlyconschart.setSize(w, h);
            Monthlysubconchart.setSize(w, h);
/*            deptemp.setSize(w, h);*/
            //chartcmrData.setSize(500, 325);
            //chartHrmData.setSize(400, 325);

            //lst7leavatt.setSize(400, 325);

            const elem = $(".graph-main")[0];

            let resizeObserver = new ResizeObserver(function () {
                MonthlySalesline.setSize(w, h);
                MonthlyPurchase.setSize(w, h);
                MonthlyAccounts.setSize(w, h);
                Monthlyconschart.setSize(w, h);
                Monthlysubconchart.setSize(w, h);

                //chartcmrData.setSize(500, 325);
                //chartHrmData.setSize(400, 325);
                //deptemp.setSize(500, 325);
                //lst7leavatt.setSize(200, 325);



                w = $(".graph-main").width();
            });
            resizeObserver.observe(elem);



        };

        function ExecuteGroupGraph(data, data1, data2, data3, data4, gtype) {

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
                        text: 'Amount Core TK'
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

            var chartpur = Highcharts.chart('purchartG', {


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
                        text: 'Amount Core TK'
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
            var chartacc = Highcharts.chart('accchartG', {
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
                        text: 'Amount Core TK'
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
            var chartcons = Highcharts.chart('conschartG', {


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
                        text: 'Amount Core TK'
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
            var chartsubcon = Highcharts.chart('subconchartG', {
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
                        text: 'Amount Core TK'
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

        };


    </script>


    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

    <asp:LinkButton ID="lnkbtnOk" OnClick="lnkbtnOk_Click" Class="btn btn-sm btn-primary d-none" runat="server">Ok</asp:LinkButton>

    <div class="page">
        <div class="page-inner">
            <div style="display: none;">
                <asp:TextBox ID="ParentDir" runat="server" CssClass="hide"></asp:TextBox>
                <div class="col-md-4">

                    <div>
                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"
                            ClientIDMode="Static"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                        <asp:TextBox ID="txtDateto" ClientIDMode="Static" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>

                        <div class="colMdbtn">

                            <asp:LinkButton ID="btnok" runat="server" CssClass="btn btn-primary okBtn" OnClientClick="GetData();return false;">OK</asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
            <%-- <div class="mb-5" id="EventNotice" runat="server">--%>
            <div class="col-12 py-0 pl-0 " id="EventNotice" runat="server" style="border: 1px solid #D6D8E1;">
                <div class="row">
                    <!--Breaking box-->
                    <div class="col-md-2 col-lg-2 pr-md-0">
                        <div class="p-2 bg-primary text-white text-center breaking-caret"><span class="font-weight-bold">Notice/Events</span></div>
                    </div>
                    <!--end breaking box-->
                    <!--Breaking content-->
                    <div class="col-md-10 col-lg-10 pl-md-4 py-2">
                        <div class="breaking-box">
                            <div id="carouselbreaking" class="carousel slide" data-ride="carousel">
                                <!--breaking news-->
                                <div class="carousel-inner " id="EventCaro" runat="server">
                                </div>
                                <!--end breaking news-->

                                <!--navigation slider-->
                                <div class="navigation-box p-2 d-none d-sm-block">
                                    <!--nav left-->
                                    <a class="carousel-control-prev text-primary" href="#carouselbreaking" role="button" data-slide="prev">
                                        <i class="fa fa-angle-left" aria-hidden="true"></i>
                                        <span class="sr-only">Previous</span>
                                    </a>
                                    <!--nav right-->
                                    <a class="carousel-control-next text-primary" href="#carouselbreaking" role="button" data-slide="next">
                                        <i class="fa fa-angle-right" aria-hidden="true"></i>
                                        <span class="sr-only">Next</span>
                                    </a>
                                </div>
                                <!--end navigation slider-->
                            </div>
                        </div>
                    </div>
                    <!--end breaking content-->
                </div>
            </div>
            <%-- </div>--%>
            <!-- /.page-title-bar -->
            <!-- .page-section -->


            <div class="page-section">
                <asp:Panel ID="pnlActiveUser" Visible="false" runat="server">
                    <div class="section-block mb-0 mt-1">
                        <div class="metric-row mb-0">
                            <div class="col-lg-12">
                                <div class="card pt-0 pb-0 mb-1 card-fluid">
                                    <div class="card-body pb-1 pt-0">
                                        <h3 class="card-title">Top Active User Today</h3>
                                        <div class="row" id="TopActivity" runat="server">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>





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

                                            <div class="dropdown" id="filterData" runat="server">

                                                <div class="form-group mb-0">
                                                    <label class="control-label" for="ddlUserName">Year</label>





                                                    <asp:DropDownList ID="ddlyearSale" runat="server" OnSelectedIndexChanged="ddlyearSale_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                        <asp:ListItem Value="2019">2019</asp:ListItem>
                                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                                        <asp:ListItem Value="2021">2021</asp:ListItem>
                                                        <asp:ListItem Value="2022" Selected="True">2022</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlMonths" runat="server" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                        <asp:ListItem Value="00" Selected>All Months</asp:ListItem>
                                                        <asp:ListItem Value="Jan">Jan</asp:ListItem>
                                                        <asp:ListItem Value="Feb">Feb</asp:ListItem>
                                                        <asp:ListItem Value="Mar">Mar</asp:ListItem>
                                                        <asp:ListItem Value="Apr">Apr</asp:ListItem>
                                                        <asp:ListItem Value="May">May</asp:ListItem>
                                                        <asp:ListItem Value="Jun">Jun</asp:ListItem>
                                                        <asp:ListItem Value="Jul">Jul</asp:ListItem>
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
                                        <div id="myTabContent" class="tab-content " style="width: 100%; height: 375px;">


                                            <div class="tab-pane fade show graph-main" id="tab_1231" runat="server">
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
                                            <div class="tab-pane fade show graph-main" id="tab_1232" runat="server">

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
                                            <div class="tab-pane fade show graph-main" id="tab_1233" runat="server">
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
                                            <div class="tab-pane fade show graph-main" id="tab_1234" runat="server">


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
                                            <div class="tab-pane fade show graph-main" id="tab_1235" runat="server">
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
                                                <div class="row">
                                                    <div class="col-md-5 col-sm-12 col-lg-5">
                                                        <div id="piechartEMPStatus" style="width: 100%; height: 250px;"></div>
                                                        <center>
                                                        <asp:LinkButton ID="ToAtten" runat="server"><a Class="btn btn-primary btn-sm" href="F_81_Hrm/F_83_Att/TodayAttendanceSheet.aspx">Attendance</a></asp:LinkButton>
                                                         </center>
                                                    </div>
                                                    
                                                    <div class="col-md-7 col-sm-12 col-lg-7 ">
                                                        <div id="lst7daysatt" style="width: 100%; height: 200px;"></div>
                                                        <div id="deptWisEmp" style="width: 100%; height: 160px;"></div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="tab-pane fade show graph-main" id="tab_1343" runat="server">
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
                                                <asp:Panel ID="Panel9" runat="server">
                                                    <div class="row">
                                                        <div class="col-md-6 col-sm-6 col-xs-6 col-lg-6">
                                                            <div id="crmChart" style="height: 280px;"></div>

                                                        </div>
                                                        <div class="col-md-6 col-sm-6 col-xs-6 col-lg-6">
                                                            <div id="leadempwise" style="height: 280px;"></div>

                                                        </div>
                                                    </div>




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

                <div id="Div1" class="section-block mt-0 mb-0" runat="server">
                    <!-- .section-block -->
                    <span class="d-none hidden" runat="server" id="offlineUserCount">0</span>
                    <div class="section-block mb-0 mt-1">


                        <div id="Usercompnent" class="metric-row mb-0" runat="server"></div>

                        <!-- /metric row -->
                    </div>
                    <!-- /.section-block -->

                </div>


                <!--  this is for group users -->

                <div class="row" id="div_groupUSers" runat="server">
                    <div class="col-md-12">
                        <div id="div4" class="card card-fluid" runat="server">
                            <!-- .card-header -->
                            <header class="card-header">
                                <div class="d-flex align-items-center">
                                    <div class="mr-auto">
                                        <ul class="nav ">
                                            <li class="nav-item">
                                                <a class="nav-link active show" data-toggle="tab" href="#Salesg">Sales</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#Procurementg">Procurement</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#Accountsg">Accounts</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#Constructiong">Construction</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#Sub-Contractorg">Sub-Contractor</a>
                                            </li>

                                        </ul>
                                    </div>

                                    <div class="dropdown">

                                        <div class="form-group">
                                            <label class="control-label" for="ddlUserName">Company</label>
                                            <asp:DropDownList ID="ddlCompcode" runat="server" OnSelectedIndexChanged="ddlCompcode_SelectedIndexChanged" AutoPostBack="true" Width="300px" CssClass="custom-select chzn-select">
                                            </asp:DropDownList>

                                            <label class="control-label" for="ddlUserName">Year</label>
                                            <asp:DropDownList ID="ddlGropuYear" runat="server" OnSelectedIndexChanged="ddlGropuYear_SelectedIndexChanged" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                <asp:ListItem Value="2019">2019</asp:ListItem>
                                                <asp:ListItem Value="2020">2020</asp:ListItem>
                                                <asp:ListItem Value="2021" Selected="True">2021</asp:ListItem>

                                            </asp:DropDownList>

                                            <asp:DropDownList ID="ddlGrpGraphtype" runat="server" OnSelectedIndexChanged="ddlGrpGraphtype_SelectedIndexChanged" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                <asp:ListItem Value="line">Line</asp:ListItem>
                                                <asp:ListItem Value="column" Selected="True">Column</asp:ListItem>
                                                <asp:ListItem Value="area">Area</asp:ListItem>


                                            </asp:DropDownList>
                                        </div>



                                    </div>
                                </div>
                            </header>
                            <!-- /.card-header -->
                            <!-- .card-body -->
                            <div class="card-body">
                                <!-- .tab-content -->
                                <div id="myTabContentg" class="tab-content graph-main" style="width: 100%; height: 375px;">


                                    <div class="tab-pane fade active show" id="Salesg">
                                        <div class="row ">
                                            <div class="col-md-12 text-right">
                                                <asp:HyperLink ID="salesView" runat="server" class="pull-right"> View all <i class="fa fa-fw fa-angle-right"></i></asp:HyperLink>
                                            </div>
                                        </div>


                                        <div id="salchartG" style="width: 90%; max-height: 280px;"></div>

                                    </div>
                                    <div class="tab-pane fade" id="Procurementg">

                                        <div class="row ">
                                            <div class="col-md-12 text-right">
                                                <asp:HyperLink ID="Purchaselink" runat="server" class="pull-right"> View all <i class="fa fa-fw fa-angle-right"></i></asp:HyperLink>


                                            </div>
                                        </div>
                                        <div id="purchartG" style="width: 90%; max-height: 280px;"></div>
                                    </div>
                                    <div class="tab-pane fade" id="Accountsg">
                                        <div class="row ">
                                            <div class="col-md-12 text-right">
                                                <asp:HyperLink ID="Accountsglink" runat="server" class="pull-right"> View all <i class="fa fa-fw fa-angle-right"></i></asp:HyperLink>


                                            </div>
                                        </div>
                                        <div id="accchartG" style="width: 90%; height: 280px;"></div>
                                    </div>
                                    <div class="tab-pane fade" id="Constructiong">


                                        <div class="row ">
                                            <div class="col-md-12 text-right">
                                                <asp:HyperLink ID="Constructionglink" runat="server" class="pull-right"> View all <i class="fa fa-fw fa-angle-right"></i></asp:HyperLink>


                                            </div>
                                        </div>
                                        <div id="conschartG" style="width: 90%; height: 280px;"></div>
                                    </div>
                                    <div class="tab-pane fade" id="Sub-Contractorg">



                                        <div id="subconchartG" style="width: 90%; height: 280px;"></div>
                                    </div>


                                </div>
                                <!-- /.tab-content -->
                            </div>
                            <!-- /.card-body -->
                        </div>

                    </div>
                </div>








                <asp:Panel ID="pnlwkpresence" Visible="false" runat="server">
                    <div class="section-block mb-0 mt-1">
                        <div class="metric-row">
                            <div class="col-md-6 col-lg-6">
                                <section class="card mb-1 card-fluid p-2">
                                    <!-- .card-header -->
                                    <header class="card-header border-0">
                                        <div class="d-flex align-items-center">
                                            <span class="mr-auto">Today's Presence</span>

                                        </div>
                                    </header>
                                    <!-- /.card-header -->
                                    <!-- .table-responsive -->
                                    <div class="table-responsive">
                                        <!-- .table -->
                                        <asp:GridView ID="gvRptAttn" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            ShowFooter="false" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvRptAttn_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Company Name">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlnkgvcomname" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="none" Font-Size="11px" Font-Underline="false" ForeColor="Black"
                                                            Style="text-align: left; background-color: Transparent" Target="_blank"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam"))%>'></asp:HyperLink>


                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comnam"))%>'></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Staff">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvttStaff" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlstap")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Present">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPresent" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "present")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Late">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvLate" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "late")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Early Leave">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvEarlyLv" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "earlyLev")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="On Leave">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvOnleav" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "onlev")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Absent">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAbst" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                            </Columns>

                                            <EditRowStyle />
                                            <AlternatingRowStyle />


                                        </asp:GridView>
                                        <!-- /.table -->
                                    </div>
                                    <!-- /.table-responsive -->
                                </section>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="card card-fluid p-2 grpattn">
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>








                <asp:Panel ID="pnlTdayWork" runat="server" class="card-deck-xl" Visible="false">
                    <!-- .card -->
                    <div class="card card-fluid" id="div8">
                        <div class="card-header">
                            My Work:
                             <a href="F_34_Mgt/RptUserLogDetails.aspx" class="float-right">View all <i class="fa fa-fw fa-angle-right"></i></a>

                        </div>
                        <!-- .lits-group -->
                        <div class="card-body" style="min-height: 230px;">
                            <table class="table-striped table-hover table-bordered">
                                <thead>
                                    <tr class="tblh">
                                        <th class="th3">#</th>
                                        <th class="th2">Today's Work List</th>
                                        <th class="th1">Count</th>
                                </thead>
                                <tbody id="userdata"></tbody>
                            </table>
                            <br />
                        </div>

                    </div>
                    <!-- /.card -->
                    <!-- .card -->
                    <div class="card card-fluid" id="div9">
                        <div class="card-header">
                            Today's Work
                        </div>
                        <!-- .card-body -->
                        <div class="card-body" style="min-height: 230px;">
                            <div id="empdata" style="width: 480px;"></div>
                        </div>
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->
                </asp:Panel>







            </div>


            <!-- /.page-section -->
        </div>
    </div>
    <!-- /.page-inner -->
    <!----Draw right ---->
    <div class="modal modal-drawer fade has-shown" id="exampleModalDrawerRight" tabindex="-1" role="dialog" aria-labelledby="exampleModalDrawerRightLabel" style="display: none;" aria-hidden="true">
        <!-- .modal-dialog -->
        <div class="modal-dialog modal-drawer-right" role="document">
            <!-- .modal-content -->
            <div class="modal-content">
                <!-- .modal-header -->
                <div class="modal-header modal-body-scrolled">
                    <h5 id="exampleModalDrawerRightLabel" class="modal-title">Offline User List </h5>
                </div>
                <!-- /.modal-header -->
                <!-- .modal-body -->
                <div class="modal-body">
                    <div class="section-block" id="OfflineUsers" runat="server">
                    </div>
                </div>
                <!-- /.modal-body -->
                <!-- .modal-footer -->
                <div class="modal-footer modal-body-scrolled">
                    <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                </div>
                <!-- /.modal-footer -->
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

