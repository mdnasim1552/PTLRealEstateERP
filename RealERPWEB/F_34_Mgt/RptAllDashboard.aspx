<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptAllDashboard.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.RptAllDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script src="../Scripts/highchartwithmap.js"></script>
    <script src="../Scripts/map.js"></script>
    <script src="http://github.highcharts.com/modules/exporting.js"></script>

    <script src="../Scripts/drilldown.js"></script>

    <script src="../Scripts/bd-all.js"></script>



    <script type="text/javascript"> 
        function ExecuteSalesGraph(toplist, weeksales, monsales, singldata) {

            try
            {
                console.log(JSON.parse(toplist));
                console.log(JSON.parse(weeksales));
                console.log(JSON.parse(monsales));
                console.log(JSON.parse(singldata));

                var tdata = JSON.parse(toplist);
                var wdata = JSON.parse(weeksales);
                var mdata = JSON.parse(monsales);
                var sdata = JSON.parse(singldata);


                console.log(sdata);
                console.log(sdata);
                console.log(sdata);
                console.log(sdata);
                console.log(sdata);



                if (sdata[5] === 0) {
                    $(".progress-bar").html('0');
                    $(".progress-bar").css('color', 'black');
                    $(".progress-bar").css('width', sdata[5] + '%');
                }
                else {
                    if (sdata[5] > 100) {
                        $(".progress-bar").html("Target Achieved:   " + parseFloat(sdata[5]).toFixed(2) + " %");
                        $(".progress-bar").css('width', '100%');
                    }
                    else {
                        $(".progress-bar").html("Target Completed:  " + parseFloat(sdata[5]).toFixed(2) + " %");
                        $(".progress-bar").css('width', sdata[5] + '%');
                    }
                }



                $('#MonthlySales').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Monthly Sales Collection',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }

                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'Amount in Crore'
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
                                format: '{point.y:.2f}'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f} crore</b><br/>'
                    },

                    "series": [
                        {
                            "name": "Balance Sheet",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": "Target Sales",
                                    "y": sdata[9] / 10000000,

                                },
                                {
                                    "name": "Actual Sales",
                                    "y": sdata[1] / 10000000,

                                },
                                {
                                    "name": "Target Collection",
                                    "y": sdata[10] / 10000000,

                                },
                                {
                                    "name": "Actual Collection",
                                    "y": sdata[2] / 10000000,

                                }
                            ]
                        }
                    ]
                });

                $('#TodaySales').highcharts({


                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Today Sales Realization',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'Amount in Taka(Lacs)'
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
                                format: '{point.y:.1f}'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f} Lacs</b><br/>'
                    },

                    "series": [
                        {
                            "name": "Today Sales Collection",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": "Sales",
                                    "y": sdata[3] / 100000,
                                    "color": "#f45342",


                                },
                                {
                                    "name": "Collection",
                                    "y": sdata[4] / 100000,
                                    "color": "#448e33",

                                }
                            ]
                        }
                    ]
                });

                $('#weekchart').highcharts({


                    chart: {
                        // type: 'line'
                        type: 'area'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Last 7 Days ',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        categories: [
                            wdata[0]['ymon'],
                            wdata[1]['ymon'],
                            wdata[2]['ymon'],
                            wdata[3]['ymon'],
                            wdata[4]['ymon'],
                            wdata[5]['ymon'],
                            wdata[6]['ymon']

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
                            '<td style="padding:0"><b>{point.y:0f} Lacs</b></td></tr>',
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
                        name: 'Sales',
                        data: [wdata[0]['ttlsalamt'] / 100000,
                        wdata[1]['ttlsalamt'] / 100000,
                        wdata[2]['ttlsalamt'] / 100000,
                        wdata[3]['ttlsalamt'] / 100000,
                        wdata[4]['ttlsalamt'] / 100000,
                        wdata[5]['ttlsalamt'] / 100000,
                        wdata[6]['ttlsalamt'] / 100000],
                        color: '#1581C1'

                    }, {

                        name: 'Collection',
                        //color:red,
                        data: [wdata[0]['collamt'] / 100000,
                        wdata[1]['collamt'] / 100000,
                        wdata[2]['collamt'] / 100000,
                        wdata[3]['collamt'] / 100000,
                        wdata[4]['collamt'] / 100000,
                        wdata[5]['collamt'] / 100000,
                        wdata[6]['collamt'] / 100000],
                        color: '#CA6621'
                    }]
                });

                $('#piechart').highcharts({


                    chart: {
                        type: 'pie'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Yearly (Receivable)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        categories: [
                            'Sales',
                            'Collection',
                            'Receivable'

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
                            '<td style="padding:0"><b>{point.y:.2f}</b> Crore</td></tr>',
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
                        name: "Amount",
                        colorByPoint: true,
                        data: [{
                            name: 'Sales',
                            y: sdata[6] / 10000000,
                            color: "#2E9ADA"
                            //drilldown: 'Microsoft Internet Explorer'
                        }, {
                            name: 'Collection',
                            y: sdata[7] / 10000000,
                            color: '#E37F3A'
                            //drilldown: null
                        },
                        {
                            name: 'Dues',
                            y: sdata[8] / 10000000,
                            color: '#8C8453'
                            //drilldown: null
                        }]
                    }],



                });

                $('#SalesChart').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''

                    },

                    subtitle: {
                        text: 'Sales',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
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
                            text: 'Amount in Lacs'
                        }
                        //,
                        //labels: {
                        //    format: '{value} crore'
                        //}

                    },


                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.1,
                            borderWidth: 0


                        }
                    },
                    series: [{
                        name: 'Target',
                        data: [mdata[0]['targtsaleamt'] / 100000, mdata[1]['targtsaleamt'] / 100000,
                        mdata[2]['targtsaleamt'] / 100000, mdata[3]['targtsaleamt'] / 100000,
                        mdata[4]['targtsaleamt'] / 100000, mdata[5]['targtsaleamt'] / 100000,
                        mdata[6]['targtsaleamt'] / 100000, mdata[7]['targtsaleamt'] / 100000,
                        mdata[8]['targtsaleamt'] / 100000, mdata[9]['targtsaleamt'] / 100000,
                        mdata[10]['targtsaleamt'] / 100000, mdata[11]['targtsaleamt'] / 100000],
                        color: '#1581C1'

                    }, {

                        name: 'Actual',
                        //color:red,
                        data: [mdata[0]['ttlsalamt'] / 100000, mdata[1]['ttlsalamt'] / 100000,
                        mdata[2]['ttlsalamt'] / 100000, mdata[3]['ttlsalamt'] / 100000,
                        mdata[4]['ttlsalamt'] / 100000, mdata[5]['ttlsalamt'] / 100000,
                        mdata[6]['ttlsalamt'] / 100000, mdata[7]['ttlsalamt'] / 100000,
                        mdata[8]['ttlsalamt'] / 100000, mdata[9]['ttlsalamt'] / 100000,
                        mdata[10]['ttlsalamt'] / 100000, mdata[11]['ttlsalamt'] / 100000],
                        color: '#CA6621'
                    }]
                });
                $('#CollChart').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Collection',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
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
                            text: 'Amount in Lacs'
                        }
                    },


                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y:.2f} Lacs</b></td></tr>',
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
                        data: [mdata[0]['tarcollamt'] / 100000, mdata[1]['tarcollamt'] / 100000,
                        mdata[2]['tarcollamt'] / 100000, mdata[3]['tarcollamt'] / 100000,
                        mdata[4]['tarcollamt'] / 100000, mdata[5]['tarcollamt'] / 100000,
                        mdata[6]['tarcollamt'] / 100000, mdata[7]['tarcollamt'] / 100000,
                        mdata[8]['tarcollamt'] / 100000, mdata[9]['tarcollamt'] / 100000,
                        mdata[10]['tarcollamt'] / 100000, mdata[11]['tarcollamt'] / 100000],
                        color: '#42f47a'

                    }, {

                        name: 'Actual',
                        //color:red,
                        data: [mdata[0]['collamt'] / 100000, mdata[1]['collamt'] / 100000,
                        mdata[2]['collamt'] / 100000, mdata[3]['collamt'] / 100000,
                        mdata[4]['collamt'] / 100000, mdata[5]['collamt'] / 100000,
                        mdata[6]['collamt'] / 100000, mdata[7]['collamt'] / 100000,
                        mdata[8]['collamt'] / 100000, mdata[9]['collamt'] / 100000,
                        mdata[10]['collamt'] / 100000, mdata[11]['collamt'] / 100000],
                        color: '#454289'
                    }]

                });


                $('#Top5Customer').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Top 5 Customer(Month)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }

                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'Amount in Lacs'
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
                                format: '{point.y:.2f}'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f} Lacs</b><br/>'
                    },

                    "series": [
                        {
                            "name": "Balance Sheet",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": tdata[0]['sirdesc'],
                                    "y": tdata[0]['suamt'] / 100000,

                                },
                                {
                                    "name": tdata[1]['sirdesc'],
                                    "y": tdata[1]['suamt'] / 100000,

                                },
                                {
                                    "name": tdata[2]['sirdesc'],
                                    "y": tdata[2]['suamt'] / 100000,
                                },
                                {
                                    "name": tdata[3]['sirdesc'],
                                    "y": tdata[3]['suamt'] / 100000,

                                },
                                {
                                    "name": tdata[4]['sirdesc'],
                                    "y": tdata[4]['suamt'] / 100000,

                                }
                            ]
                        }
                    ]
                });
                $('#Top5Item').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Top 5 Unit(Month)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }

                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'Quantity'
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
                                format: '{point.y:0f}'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:0f} Unit</b><br/>'
                    },

                    "series": [
                        {
                            "name": "Unit",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": tdata[5]['sirdesc'],
                                    "y": tdata[5]['suamt'],

                                },
                                {
                                    "name": tdata[6]['sirdesc'],
                                    "y": tdata[6]['suamt'],

                                },
                                {
                                    "name": tdata[7]['sirdesc'],
                                    "y": tdata[7]['suamt'],
                                },
                                {
                                    "name": tdata[8]['sirdesc'],
                                    "y": tdata[8]['suamt'],

                                },
                                {
                                    "name": tdata[9]['sirdesc'],
                                    "y": tdata[9]['suamt'],

                                }
                            ]
                        }
                    ]
                });
                $('#Top5Team').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Top 5 SalesTeam(Month)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }

                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'Amount in Lacs'
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
                                format: '{point.y:.2f}'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f} Lacs</b><br/>'
                    },

                    "series": [
                        {
                            "name": "Sales Team",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": tdata[10]['sirdesc'],
                                    "y": tdata[10]['suamt'] / 100000,

                                },
                                {
                                    "name": tdata[11]['sirdesc'],
                                    "y": tdata[11]['suamt'] / 100000,

                                },
                                {
                                    "name": tdata[12]['sirdesc'],
                                    "y": tdata[12]['suamt'] / 100000,
                                },
                                {
                                    "name": tdata[13]['sirdesc'],
                                    "y": tdata[13]['suamt'] / 100000,

                                },
                                {
                                    "name": tdata[14]['sirdesc'],
                                    "y": tdata[14]['suamt'] / 100000,

                                }
                            ]
                        }
                    ]
                });
            }



            catch (e) {
                alert(e.message)
            }


        }


        function ExecutePurchaseGraph(purdata, topdata) {

            try {

                var purdata = JSON.parse(purdata);
                //console.log(purdata);
                //console.log(topdata);

                var topdata = JSON.parse(topdata)
                //var topdata = topdata;






                Highcharts.setOptions({
                    lang: {
                        decimalPoint: '.',
                        thousandsSep: ' '
                    }
                });

                $('#purchart').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Procurement TK(Lakh)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
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
                        data: [purdata[0]['ttlsalamtcore'], purdata[1]['ttlsalamtcore'], purdata[2]['ttlsalamtcore'], purdata[3]['ttlsalamtcore'], purdata[4]['ttlsalamtcore'], purdata[5]['ttlsalamtcore'], purdata[6]['ttlsalamtcore'], purdata[7]['ttlsalamtcore'], purdata[8]['ttlsalamtcore'], purdata[9]['ttlsalamtcore'], purdata[10]['ttlsalamtcore'], purdata[11]['ttlsalamtcore']],
                        color: '#4286f4'

                    }, {

                        name: 'Payment',
                        //color:red,
                        data: [purdata[0]['tpayamtcore'], purdata[1]['tpayamtcore'], purdata[2]['tpayamtcore'], purdata[3]['tpayamtcore'], purdata[4]['tpayamtcore'], purdata[5]['tpayamtcore'], purdata[6]['tpayamtcore'], purdata[7]['tpayamtcore'], purdata[8]['tpayamtcore'], purdata[9]['tpayamtcore'], purdata[10]['tpayamtcore'], purdata[11]['tpayamtcore']],
                        color: '#f44941'
                    }]
                });
                $('#monthpurchase').highcharts({
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Monthly Purchase Payment (Lakh)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'Amount in Lakh'
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
                                format: '{point.y:.2f}'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "Monthly Purchase Payment",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": "Purchase",
                                    "y": purdata[12]['ttlsalamtcore'],
                                    "color": "#f45342",


                                },
                                {
                                    "name": "Payment",
                                    "y": purdata[12]['tpayamtcore'],
                                    "color": "#448e33",

                                }
                            ]
                        }
                    ]
                });
                $('#weekpurchase').highcharts({


                    chart: {
                        // type: 'line'
                        type: 'area'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Last 7 Days Purchase ',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        categories: [
                            purdata[13]['yearmon'],
                            purdata[14]['yearmon'],
                            purdata[15]['yearmon'],
                            purdata[16]['yearmon'],
                            purdata[17]['yearmon'],
                            purdata[18]['yearmon'],
                            purdata[19]['yearmon']

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
                        data: [purdata[13]['ttlsalamt'], purdata[14]['ttlsalamt'], purdata[15]['ttlsalamt'], purdata[16]['ttlsalamt'], purdata[17]['ttlsalamt'], purdata[18]['ttlsalamt'], purdata[19]['ttlsalamt']],
                        color: '#1581C1'

                    }, {

                        name: 'Payment',
                        //color:red,
                        data: [purdata[13]['tpayamt'], purdata[14]['tpayamt'], purdata[15]['tpayamt'], purdata[16]['tpayamt'], purdata[17]['tpayamt'], purdata[18]['tpayamt'], purdata[19]['tpayamt']],
                        color: '#CA6621'
                    }]
                });
                //console.log(topdata[0]['sirdesc']);
                $('#Top5Sup').highcharts({
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Top 5 Supplier (In Month)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'In Amount'
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
                                format: '{point.y:.1f}'
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "Top Supplier",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": topdata[0]['sirdesc'],
                                    "y": topdata[0]['itmamt'],

                                },
                                {
                                    "name": topdata[1]['sirdesc'],
                                    "y": topdata[1]['itmamt'],

                                },
                                {
                                    "name": topdata[2]['sirdesc'],
                                    "y": topdata[2]['itmamt'],

                                },
                                {
                                    "name": topdata[3]['sirdesc'],
                                    "y": topdata[3]['itmamt'],

                                },
                                {
                                    "name": topdata[4]['sirdesc'],
                                    "y": topdata[4]['itmamt'],

                                }
                            ]
                        }
                    ]
                });

                $('#top5mat').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Top 5 Materials (In Month)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'In Quantity'
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
                                format: '{point.y:.0f}'
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> <br/>'
                    },

                    "series": [
                        {
                            "name": "Top Materials",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": topdata[5]['sirdesc'],
                                    "y": topdata[5]['itmamt'],

                                },
                                {
                                    "name": topdata[6]['sirdesc'],
                                    "y": topdata[6]['itmamt'],

                                },
                                {
                                    "name": topdata[7]['sirdesc'],
                                    "y": topdata[7]['itmamt'],

                                },
                                {
                                    "name": topdata[8]['sirdesc'],
                                    "y": topdata[8]['itmamt'],

                                },
                                {
                                    "name": topdata[9]['sirdesc'],
                                    "y": topdata[9]['itmamt'],

                                }
                            ]
                        }
                    ]
                });
                $('#top5supout').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Top 5 Supplier Outstanding (In Month)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'In Amount'
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
                                format: '{point.y:.0f}'
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> <br/>'
                    },

                    "series": [
                        {
                            "name": "Top Supplier Outstanding",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": topdata[10]['sirdesc'],
                                    "y": topdata[10]['itmamt'],

                                },
                                {
                                    "name": topdata[11]['sirdesc'],
                                    "y": topdata[11]['itmamt'],

                                },
                                {
                                    "name": topdata[12]['sirdesc'],
                                    "y": topdata[12]['itmamt'],

                                },
                                {
                                    "name": topdata[13]['sirdesc'],
                                    "y": topdata[13]['itmamt'],

                                },
                                {
                                    "name": topdata[14]['sirdesc'],
                                    "y": topdata[14]['itmamt'],

                                }
                            ]
                        }
                    ]
                });
                $('#top5suppay').highcharts({


                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Top 5 Supplier Payment (In Month)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'In Amount'
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
                                format: '{point.y:.0f}'
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> <br/>'
                    },

                    "series": [
                        {
                            "name": "Top Supplier Payment",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": topdata[15]['sirdesc'],
                                    "y": topdata[15]['itmamt'],

                                },
                                {
                                    "name": topdata[16]['sirdesc'],
                                    "y": topdata[16]['itmamt'],

                                },
                                {
                                    "name": topdata[17]['sirdesc'],
                                    "y": topdata[17]['itmamt'],

                                },
                                {
                                    "name": topdata[18]['sirdesc'],
                                    "y": topdata[18]['itmamt'],

                                },
                                {
                                    "name": topdata[19]['sirdesc'],
                                    "y": topdata[19]['itmamt'],

                                }
                            ]
                        }
                    ]
                });
                $('#purchasebal').highcharts({
                    chart: {
                        type: 'pie'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: 'Yearly Purchase (Balance)',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },
                    xAxis: {
                        categories: [
                            'Purchase',
                            'Payment',
                            'Balance'

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
                    //series: [{
                    //    name: 'Export',
                    //    data: [ys1],
                    //    color: '#1581C1'

                    //},
                    //{

                    //    name: 'Realization',
                    //    //color:red,
                    //    data: [yc1],
                    //    color: '#CA6621'
                    //    }
                    //]


                    series: [{
                        name: "Amount",
                        colorByPoint: true,
                        data: [{
                            name: 'Purchase',
                            y: purdata[0]['ttlsalamtcore'] + purdata[1]['ttlsalamtcore'] + purdata[2]['ttlsalamtcore'] + purdata[3]['ttlsalamtcore'] + purdata[4]['ttlsalamtcore'] + purdata[5]['ttlsalamtcore'] + purdata[6]['ttlsalamtcore'] + purdata[7]['ttlsalamtcore'] + purdata[8]['ttlsalamtcore'] + purdata[9]['ttlsalamtcore'] + purdata[10]['ttlsalamtcore'] + purdata[11]['ttlsalamtcore'],
                            color: "#2E9ADA"
                            //drilldown: 'Microsoft Internet Explorer'
                        }, {
                            name: 'Payment',
                            y: purdata[0]['tpayamtcore'] + purdata[1]['tpayamtcore'] + purdata[2]['tpayamtcore'] + purdata[3]['tpayamtcore'] + purdata[4]['tpayamtcore'] + purdata[5]['tpayamtcore'] + purdata[6]['tpayamtcore'] + purdata[7]['tpayamtcore'] + purdata[8]['tpayamtcore'] + purdata[9]['tpayamtcore'] + purdata[10]['tpayamtcore'] + purdata[11]['tpayamtcore'],
                            color: '#E37F3A'
                            //drilldown: null
                        },
                        {
                            name: 'Balance',
                            y: ((purdata[0]['ttlsalamtcore'] + purdata[1]['ttlsalamtcore'] + purdata[2]['ttlsalamtcore'] + purdata[3]['ttlsalamtcore'] + purdata[4]['ttlsalamtcore'] + purdata[5]['ttlsalamtcore'] + purdata[6]['ttlsalamtcore'] + purdata[7]['ttlsalamtcore'] + purdata[8]['ttlsalamtcore'] + purdata[9]['ttlsalamtcore'] + purdata[10]['ttlsalamtcore'] + purdata[11]['ttlsalamtcore']) - (purdata[0]['tpayamtcore'] + purdata[1]['tpayamtcore'] + purdata[2]['tpayamtcore'] + purdata[3]['tpayamtcore'] + purdata[4]['tpayamtcore'] + purdata[5]['tpayamtcore'] + purdata[6]['tpayamtcore'] + purdata[7]['tpayamtcore'] + purdata[8]['tpayamtcore'] + purdata[9]['tpayamtcore'] + purdata[10]['tpayamtcore'] + purdata[11]['tpayamtcore'])),
                            color: '#8C8453'
                            //drilldown: null
                        }]
                    }],



                });
            }
            catch (e) {


                alert(e.message);
            }
        }
        function ExecuteAccGraph(balshet, monthacc, curbal1, todayrecv, todaypay, monthrec, monthpay) {
            console.log(JSON.parse(todaypay));
            var balshet_date = JSON.parse(balshet);
            var monthacc = JSON.parse(monthacc);
            var curbal = JSON.parse(curbal1);
            var todayrecv = JSON.parse(todayrecv);
            var todaypay = JSON.parse(todaypay);
            var monthrec = JSON.parse(monthrec);
            var monthpay = JSON.parse(monthpay);
            //    console.log(monthpay);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });
            $('#balsheetchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Balance Sheet (%)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Parcentage'
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
                            format: '{point.y:.2f}%'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Balance Sheet",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Non-Current Asset",
                                "y": balshet_date[0]['noncuram'],

                            },
                            {
                                "name": "Current Asset",
                                "y": balshet_date[0]['curam'],

                            },
                            {
                                "name": "Equity",
                                "y": balshet_date[0]['equityam'],

                            },
                            {
                                "name": "Non-Current Liabilities",
                                "y": balshet_date[0]['noncurlia'],

                            },
                            {
                                "name": "Current Liabilities",
                                "y": balshet_date[0]['curlia'],

                            }
                        ]
                    }
                ]
            });
            $('#accchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Accounts TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
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
                    name: 'Recipt',
                    data: [monthacc[0]['cramcore'], monthacc[1]['cramcore'], monthacc[2]['cramcore'], monthacc[3]['cramcore'], monthacc[4]['cramcore'], monthacc[5]['cramcore'], monthacc[6]['cramcore'], monthacc[7]['cramcore'], monthacc[8]['cramcore'], monthacc[9]['cramcore'], monthacc[10]['cramcore'], monthacc[11]['cramcore']],
                    color: '#138225'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [monthacc[0]['dramcore'], monthacc[1]['dramcore'], monthacc[2]['dramcore'], monthacc[3]['dramcore'], monthacc[4]['dramcore'], monthacc[5]['dramcore'], monthacc[6]['dramcore'], monthacc[7]['dramcore'], monthacc[8]['dramcore'], monthacc[9]['dramcore'], monthacc[10]['dramcore'], monthacc[11]['dramcore']],
                    color: '#aa1811'
                }]
            });

            $('#CurmonAcc').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Current Month Account',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Tk (Lakh)'
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
                            format: '{point.y:.2f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Current Month Account",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Receipt",
                                "y": monthacc[12]['cramcore'],
                                "color": "#ba8639",


                            },
                            {
                                "name": "Payment",
                                "y": monthacc[12]['dramcore'],
                                "color": "#72bc60",

                            }
                        ]
                    }
                ]
            });

            $('#CurBal').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Overall Balance',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Overall Balance",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": curbal[0]['cactdesc'],
                                "y": curbal[0]['trnam'],

                            },
                            {
                                "name": curbal[1]['cactdesc'],
                                "y": curbal[1]['trnam'],

                            },
                            {
                                "name": curbal[2]['cactdesc'],
                                "y": curbal[2]['trnam'],

                            },
                            {
                                "name": curbal[3]['cactdesc'],
                                "y": curbal[3]['trnam'],

                            },
                            {
                                "name": curbal[4]['cactdesc'],
                                "y": curbal[4]['trnam'],

                            }
                        ]
                    }
                ]
            });
            $('#TodayReceive').highcharts({
                chart: {
                    styledMode: true
                },

                title: {
                    text: 'Today Received'
                },

                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    keys: ['name', 'y', 'selected', 'sliced'],
                    data: (function () {
                        // generate an array of random data
                        var data = [],
                            time = (new Date()).getTime(),
                            i;

                        for (var key in todayrecv) {
                            if (todayrecv.hasOwnProperty(key)) {
                                data.push([todayrecv[key].cactdesc,
                                todayrecv[key].trnam, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true
                }]
            });
            $('#TodayPayment').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                title: {
                    text: 'Today <br>Payment in %<br>',
                    align: 'center',
                    verticalAlign: 'middle',
                    y: 40
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}</b>'
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            distance: -50,
                            format: '{point.y:.2f}',




                            style: {
                                fontWeight: 'bold',
                                color: 'white'
                            }
                        },
                        startAngle: -90,
                        endAngle: 90,
                        center: ['50%', '75%'],
                        size: '160%'
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Today Payment',
                    innerSize: '50%',
                    data: (function () {
                        // generate an array of random data
                        var data = [];

                        for (var key in todaypay) {
                            if (todaypay.hasOwnProperty(key)) {
                                data.push([todaypay[key].cactdesc,
                                todaypay[key].trnam
                                ]);
                            }
                        }
                        return data;
                    }()),
                }]
            });
            $('#MonthRec').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Top Receive Head',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Monthly Top Receive Head",
                        "colorByPoint": true,
                        "data": (function () {
                            // generate an array of random data
                            var data = [];

                            for (var key in monthrec) {
                                if (monthrec.hasOwnProperty(key)) {
                                    data.push({
                                        "name": monthrec[key].cactdesc,
                                        "y": monthrec[key].trnam,
                                    });
                                }
                            }
                            return data;
                        }())
                    }
                ]
            });
            $('#MonthPay').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Top Payment Head',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Monthly Top Payment Head",
                        "colorByPoint": true,
                        "data": (function () {
                            // generate an array of random data
                            var data = [];

                            for (var key in monthpay) {
                                if (monthpay.hasOwnProperty(key)) {
                                    data.push({
                                        "name": monthpay[key].cactdesc,
                                        "y": monthpay[key].trnam,
                                    });
                                }
                            }
                            return data;

                        }())

                    }
                ]

            });
        }
        function ExecuteProductionGrpah(projA, projB, mon, singldata) {
            var proj1 = JSON.parse(projA);
            var proj2 = JSON.parse(projB);
            var mdata = JSON.parse(mon);
            var singl = JSON.parse(singldata);
            console.log(proj1)
            console.log(proj2)
            console.log(mdata)
            console.log(singl)

            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            var aisirdesc = [];
            var budget = [];
            var budget2 = [];
            var actual = [];
            var actual2 = [];
            var drildata = [];

            for (var key in proj1) {
                if (proj1.hasOwnProperty(key)) {
                    aisirdesc.push(proj1[key].pactdesc);
                    budget.push(proj1[key].taramt);
                    budget2.push(
                        { name: proj1[key].pactdesc, y: proj1[key].taramt / 100000, drilldown: "B" + proj1[key].pactcode });
                    actual.push(proj1[key].exeamt);
                    actual2.push(
                        { name: proj1[key].pactdesc, y: proj1[key].exeamt / 100000, drilldown: "E" + proj1[key].pactcode });
                    var bud = [];
                    var exec = [];
                    for (var keyy in proj2) {
                        if (proj2.hasOwnProperty(keyy)) {
                            if (proj1[key].pactcode == proj2[keyy].pactcode) {
                                bud.push([proj2[keyy].isirdesc, proj2[keyy].taramt / 100000]);
                                exec.push([proj2[keyy].isirdesc, proj2[keyy].exeamt / 100000]);
                            }


                        }
                    }
                    //   console.log(bud);
                    drildata.push({
                        id: 'B' + proj1[key].pactcode,
                        data: bud,
                        name: 'Budget',
                        color: '#b2e8a0',

                    }, {
                        id: 'E' + proj1[key].pactcode,
                        data: exec,
                        name: 'Execution',
                        color: 'maroon',

                    });
                }
            }

            console.log(budget2)
            console.log(actual2)
            console.log(drildata)
            $('#MostItmProd').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: 'Target VS Execution(Project)-Monthly'


                },

                xAxis: {
                    type: 'category',
                    labels: {
                        formatter: function () {
                            return '<span style="fill: black;">' + this.value + '</span>';
                        },
                        style: {
                            color: '#000',

                        }
                    }
                },

                plotOptions: {
                    series: {
                        stacking: 'normal',
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.2f}'
                        }
                    }
                },

                series: [{
                    name: 'Target',
                    color: '#b2e8a0',

                    data: budget2,
                }, {
                    name: 'Execution',
                    color: 'maroon',

                    data: actual2,

                }],
                drilldown: {
                    series: drildata,

                }
            });

            $('#Curmonprod').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Target Vs Execution (Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Taka'
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
                            format: '{point.y:.2f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Monthly Purchase Payment",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Target",
                                "y": singl[0] / 100000,
                                "color": "#f45342",


                            },
                            {
                                "name": "Execution",
                                "y": singl[1] / 100000,
                                "color": "#448e33",

                            }
                        ]
                    }
                ]
            });

            $('#CurmonprodA').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Yearly Construction Progress',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Target',
                        'Execution',
                        'Remaining'

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
                        '<td style="padding:0"><b>{point.y:.2f}</b> Crore</td></tr>',
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
                    name: "Amount",
                    colorByPoint: true,
                    data: [{
                        name: 'Target',
                        y: (((mdata[0].taramt) +
                            (mdata[1].taramt) +
                            (mdata[2].taramt) +
                            (mdata[3].taramt) +
                            (mdata[4].taramt) +
                            (mdata[5].taramt) +
                            (mdata[6].taramt) +
                            (mdata[7].taramt) +
                            (mdata[8].taramt) +
                            (mdata[9].taramt) +
                            (mdata[10].taramt) +
                            (mdata[11].taramt))
                            / 10000000),
                        color: "#2E9ADA"
                        //drilldown: 'Microsoft Internet Explorer'
                    }, {
                        name: 'Execution',
                        y: (((mdata[0].examt) +
                            (mdata[1].examt) +
                            (mdata[2].examt) +
                            (mdata[3].examt) +
                            (mdata[4].examt) +
                            (mdata[5].examt) +
                            (mdata[6].examt) +
                            (mdata[7].examt) +
                            (mdata[8].examt) +
                            (mdata[9].examt) +
                            (mdata[10].examt) +
                            (mdata[11].examt))
                            / 10000000),
                        color: '#E37F3A'
                        //drilldown: null
                    },
                    {
                        name: 'Dues',
                        y: ((((mdata[0].taramt) +
                            (mdata[1].taramt) +
                            (mdata[2].taramt) +
                            (mdata[3].taramt) +
                            (mdata[4].taramt) +
                            (mdata[5].taramt) +
                            (mdata[6].taramt) +
                            (mdata[7].taramt) +
                            (mdata[8].taramt) +
                            (mdata[9].taramt) +
                            (mdata[10].taramt) +
                            (mdata[11].taramt)) -
                            ((mdata[0].examt) +
                                (mdata[1].examt) +
                                (mdata[2].examt) +
                                (mdata[3].examt) +
                                (mdata[4].examt) +
                                (mdata[5].examt) +
                                (mdata[6].examt) +
                                (mdata[7].examt) +
                                (mdata[8].examt) +
                                (mdata[9].examt) +
                                (mdata[10].examt) +
                                (mdata[11].examt)))
                            / 10000000),
                        color: '#8C8453'
                        //drilldown: null
                    }]
                }],



            });

        }




        function ExecuteBillGraph(billdata) {

            var billdata = JSON.parse(billdata);
            console.log(billdata);


            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            //Bill and Payment(Monthly)

            $('#monthconbill').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Bill & Payment (Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Lakh'
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
                            format: '{point.y:.2f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Monthly Bill Payment",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Bill",
                                "y": billdata[12]['ttlsalamtcore'],
                                "color": "#f45342",


                            },
                            {
                                "name": "Payment",
                                "y": billdata[12]['tpayamtcore'],
                                "color": "#448e33",

                            }
                        ]
                    }
                ]
            });


            //Bill and Payment(Last 7 days)

            $('#weekconbill').highcharts({


                chart: {
                    // type: 'line'
                    type: 'area'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Last 7 Days Bill ',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        billdata[13]['yearmon'],
                        billdata[14]['yearmon'],
                        billdata[15]['yearmon'],
                        billdata[16]['yearmon'],
                        billdata[17]['yearmon'],
                        billdata[18]['yearmon'],
                        billdata[19]['yearmon']

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
                    name: 'Bill',
                    data: [billdata[13]['ttlsalamt'], billdata[14]['ttlsalamt'], billdata[15]['ttlsalamt'], billdata[16]['ttlsalamt'], billdata[17]['ttlsalamt'], billdata[18]['ttlsalamt'], billdata[19]['ttlsalamt']],
                    color: '#1581C1'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [billdata[13]['tpayamt'], billdata[14]['tpayamt'], billdata[15]['tpayamt'], billdata[16]['tpayamt'], billdata[17]['tpayamt'], billdata[18]['tpayamt'], billdata[19]['tpayamt']],
                    color: '#CA6621'
                }]
            });



            //Bill and Payment(Monthly)


            $('#billmonwisechart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Bill TK(Lakh) Month Wise',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
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
                    name: 'Bill',
                    data: [billdata[0]['ttlsalamtcore'], billdata[1]['ttlsalamtcore'], billdata[2]['ttlsalamtcore'], billdata[3]['ttlsalamtcore'], billdata[4]['ttlsalamtcore'], billdata[5]['ttlsalamtcore'], billdata[6]['ttlsalamtcore'], billdata[7]['ttlsalamtcore'], billdata[8]['ttlsalamtcore'], billdata[9]['ttlsalamtcore'], billdata[10]['ttlsalamtcore'], billdata[11]['ttlsalamtcore']],
                    color: '#4286f4'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [billdata[0]['tpayamtcore'], billdata[1]['tpayamtcore'], billdata[2]['tpayamtcore'], billdata[3]['tpayamtcore'], billdata[4]['tpayamtcore'], billdata[5]['tpayamtcore'], billdata[6]['tpayamtcore'], billdata[7]['tpayamtcore'], billdata[8]['tpayamtcore'], billdata[9]['tpayamtcore'], billdata[10]['tpayamtcore'], billdata[11]['tpayamtcore']],
                    color: '#f44941'
                }]
            });







            // Bill, Payment and Balance

            $('#conbillpaybal').highcharts({
                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Yearly Contractor Bill (Balance)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Bill',
                        'Payment',
                        'Balance'

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
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.2f}'
                        }
                    }
                },


                //plotOptions: {
                //    column: {
                //        pointPadding: 0.1,
                //        borderWidth: 0


                //    }
                //},


                series: [{
                    name: "Amount",
                    colorByPoint: true,
                    data: [{
                        name: 'Bill',
                        y: billdata[0]['ttlsalamtcore'] + billdata[1]['ttlsalamtcore'] + billdata[2]['ttlsalamtcore'] + billdata[3]['ttlsalamtcore'] + billdata[4]['ttlsalamtcore'] + billdata[5]['ttlsalamtcore'] + billdata[6]['ttlsalamtcore'] + billdata[7]['ttlsalamtcore'] + billdata[8]['ttlsalamtcore'] + billdata[9]['ttlsalamtcore'] + billdata[10]['ttlsalamtcore'] + billdata[11]['ttlsalamtcore'],
                        color: "#2E9ADA"
                        //drilldown: 'Microsoft Internet Explorer'
                    }, {
                        name: 'Payment',
                        y: billdata[0]['tpayamtcore'] + billdata[1]['tpayamtcore'] + billdata[2]['tpayamtcore'] + billdata[3]['tpayamtcore'] + billdata[4]['tpayamtcore'] + billdata[5]['tpayamtcore'] + billdata[6]['tpayamtcore'] + billdata[7]['tpayamtcore'] + billdata[8]['tpayamtcore'] + billdata[9]['tpayamtcore'] + billdata[10]['tpayamtcore'] + billdata[11]['tpayamtcore'],
                        color: '#E37F3A'
                        //drilldown: null
                    },
                    {
                        name: 'Balance',
                        y: ((billdata[0]['ttlsalamtcore'] + billdata[1]['ttlsalamtcore'] + billdata[2]['ttlsalamtcore'] + billdata[3]['ttlsalamtcore'] + billdata[4]['ttlsalamtcore'] + billdata[5]['ttlsalamtcore'] + billdata[6]['ttlsalamtcore'] + billdata[7]['ttlsalamtcore'] + billdata[8]['ttlsalamtcore'] + billdata[9]['ttlsalamtcore'] + billdata[10]['ttlsalamtcore'] + billdata[11]['ttlsalamtcore']) - (billdata[0]['tpayamtcore'] + billdata[1]['tpayamtcore'] + billdata[2]['tpayamtcore'] + billdata[3]['tpayamtcore'] + billdata[4]['tpayamtcore'] + billdata[5]['tpayamtcore'] + billdata[6]['tpayamtcore'] + billdata[7]['tpayamtcore'] + billdata[8]['tpayamtcore'] + billdata[9]['tpayamtcore'] + billdata[10]['tpayamtcore'] + billdata[11]['tpayamtcore'])),
                        color: '#8C8453'
                        //drilldown: null
                    }]
                }],



            });



        }

        // A $( document ).ready() block.
        $(document).ready(function () {

            const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            let current_datetime = new Date()
            let formatted_date = current_datetime.getDate() + "-" + months[current_datetime.getMonth()] + "-" + current_datetime.getFullYear()


            var year = localStorage.getItem("year");
            var tdate = new Date();
            var dd = tdate.getDate(); //yields day
            var MM = tdate.getMonth(); //yields month
            var yyyy = tdate.getFullYear(); //yields year
            var currentDate;
            if (localStorage.getItem("year") === null) {
                // currentDate = dd + "-" + (MM + 1) + "-" + yyyy;
                currentDate = current_datetime.getDate() + "-" + months[current_datetime.getMonth()] + "-" + current_datetime.getFullYear()
            }
            else {
                // currentDate = dd + "-" + (MM + 1) + "-" + year;
                currentDate = current_datetime.getDate() + "-" + months[current_datetime.getMonth()] + "-" + year
            }



            $('#<%=txtCurTransDate.ClientID%>').val(currentDate);


            jQuery(function () {

                jQuery('#<%=OkBtn.ClientID%>').click();
                });


        });

    </script>


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





            <div class=" card card-fluid">
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="FromDate" id="lblDate" runat="server">Date</label>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">

                                <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control flatpickr-input"></asp:TextBox>

                                <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>

                            </div>

                        </div>
                        <div class="col-md-2">
                            <asp:RadioButtonList ID="rbtList" runat="server" CssClass="form-check-inline" RepeatDirection="Horizontal"> 
                                <asp:ListItem Value="0">Actual</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">Reconcile</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:Button ID="OkBtn" OnClick="OkBtn_Click" CssClass="btn btn-sm btn-primary" runat="server" Text="Ok" />
                            </div>


                        </div>





                    </div>


                    <div class="row-fluid">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="sales" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <span style="color: #44994a; font-weight: bold; font-size: 12px">Sales Progress Monthly</span>
                                        <div class="progress">
                                            <div class="progress-bar progress-bar-striped active" role="progressbar"
                                                aria-valuenow="40" aria-valuemin="0" aria-valuemax="100">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="MonthlySales" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="TodaySales" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>



                                </div>

                                <div class="row">

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="weekchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="piechart" style="width: 580px; height: 250px; margin: 0 auto; float: left;"></div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="SalesChart" style="width: 580px; height: 250px; margin: 0 auto"></div>

                                        <a href="../F_22_Sal/SalesInformation.aspx?Type=Report" target="_blank" class="btn btn-primary" id="lnkmonthlysales">Details</a>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="CollChart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">

                                        <div id="Top5Customer" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">

                                        <div id="Top5Item" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">

                                        <div id="Top5Team" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">

                                        <div id="Top5Teamweek" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>

                            </asp:View>
                            <asp:View ID="purchase" runat="server">
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="monthpurchase" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="weekpurchase" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="purchart" style="width: 580px; height: 250px; margin: 0 auto"></div>

                                        <a href="../F_14_Pro/PurInformation.aspx?Type=Report" target="_blank" class="btn btn-primary">Details</a>


                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="Top5Sup" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="top5mat" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="top5supout" style="width: 580px; height: 250px; margin: 0 auto"></div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="top5suppay" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="purchasebal" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>

                                </div>
                            </asp:View>
                            <asp:View ID="Accounts" runat="server">
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="TodayReceive" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="TodayPayment" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="MonthRec" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="MonthPay" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="CurmonAcc" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="CurBal" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="balsheetchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="accchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                        <a href="../F_18_MAcc/AccDashBoard.aspx?Type=Report" target="_blank" class="btn btn-primary">Details</a>


                                    </div>
                                </div>

                            </asp:View>
                            <asp:View ID="Construction" runat="server">
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="MostItmProd" style="width: 580px; height: 500px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6">

                                        <div style="border: 1px solid #D8D8D8">
                                            <div id="Curmonprod" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                        </div>

                                        <div style="border: 1px solid #D8D8D8">
                                            <div id="CurmonprodA" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                        </div>

                                    </div>
                                </div>

                            </asp:View>


                            <asp:View ID="ViewSuncon" runat="server">
                                <div class="row">
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="monthconbill" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="weekconbill" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>

                                </div>
                                <div class="row">


                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="billmonwisechart" style="width: 580px; height: 250px; margin: 0 auto"></div>

                                        <a href="../F_08_PPlan/SubConInformation.aspx?Type=Report" target="_blank" class="btn btn-primary">Details</a>
                                    </div>

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="conbillpaybal" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>

                                </div>


                            </asp:View>

                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

