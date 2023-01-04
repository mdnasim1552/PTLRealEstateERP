<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="RealERPWEB.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="Scripts/jquery-3.1.1.js"></script>--%>
    <%-- <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script> 
    <script src="<%=this.ResolveUrl("~/Scripts/highchartexporting.js")%>"></script>
    --%>

    <script type="text/javascript">        
        $(document).ready(function () {
            var url = $('#<%=this.ParentDir.ClientID %>').val();
            GetData();
            $("#btnshowchckdash").click(function () {
                $("#div1").hide();
            });


            $(function () {
                $('#the-basics').autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("~/Service/UserService.asmx/GetSearchUrl") %>',
                            data: '{strkeys: "' + request.term + '" }',
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                //console.log(request.term);
                                console.log(data);
                                response($.map(data.d, function (item) {
                                    //return { value: "<a href=''>"+item.dscrption+"</a>" }
                                    return {
                                        dscrption: item.itemdesc,
                                        urlinf: item.itemurl,
                                        floc: item.fbold

                                    }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                // alert(textStatus);
                            }
                        });
                    }, focus: function (event, ui) {
                        //  $('#the-basics').val(ui.item.dscrption);
                        return false;
                    },
                    select: function (event, ui) {
                        // $('#the-basics').val(ui.item.dscrption);
                        return false;
                    },
                }).data("ui-autocomplete")._renderItem = function (ul, item) {

                    return $("<li>")
                        .append("<a href='" + url + "/" + item.urlinf + "' target='_blank'>" + item.dscrption + "  </a>")
                        .appendTo(ul);
                };
            });

        });
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

        }
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
        function ExecuteUserdata(data1) {

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


            Highcharts.chart('empdata', {
                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                xAxis: {
                    categories: descdata
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: ''
                    }
                },
                legend: {
                    reversed: true
                },
                plotOptions: {
                    series: {
                        stacking: 'normal'
                    }
                },
                series: [{
                    name: 'Count',
                    data: cdata
                }]
            });



        }
        function ExecuteGraph_column(data, data1, data2, data3, data4, data5) {
            var saldata = JSON.parse(data);
            var purdata = JSON.parse(data1);
            var accdata = JSON.parse(data2);
            var consdata = JSON.parse(data3);
            var sucondata = JSON.parse(data4);
            var chartsal = Highcharts.chart('salchart', {
                chart: {
                    type: 'column'
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

                }, {

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
                }]
            });
            var chartpur = Highcharts.chart('purchart', {
                chart: {
                    type: 'column'
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
            var chartacc = Highcharts.chart('accchart', {
                chart: {
                    type: 'column'
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
            var chartcons = Highcharts.chart('conschart', {
                chart: {
                    type: 'column'
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
            var chartsubcon = Highcharts.chart('subconchart', {
                chart: {
                    type: 'column'
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
    </style>


    <div class="page">
        <div class="page-inner">
            <div style="display: none;">
                <asp:TextBox ID="ParentDir" runat="server" CssClass="hide"></asp:TextBox>
            </div>

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
                <div class="row" id="div_admin" runat="server">
                    <div class="col-lg-9">
                        <!-- .section-block -->
                        <div id="div1">
                            <div class="section-block">
                                <!-- metric row -->
                                <div class="metric-row">
                                    <div class="col-lg-12">
                                        <div class="metric-row metric-flush ">
                                            <!-- metric column -->
                                            <div class="col">
                                                <!-- .metric -->
                                                <a href="#" target="_self" runat="server" id="todaywrk" class="metric metric-bordered align-items-center card p-1">
                                                    <h2 class="metric-label">Today's Activities </h2>
                                                    <%--Teams--%>
                                                    <p class="metric-value h3">
                                                        <sub><i class="oi oi-people"></i></sub><span class="value" runat="server" id="todaywrkcount">0</span>
                                                    </p>
                                                </a>
                                                <!-- /.metric -->
                                            </div>
                                            <!-- /metric column -->
                                            <!-- metric column -->
                                            <div class="col">
                                                <!-- .metric -->

                                                <a href="#" class="metric metric-bordered align-items-center card p-1" data-toggle="modal" data-target="#exampleModalDrawerRight">

                                                    <h2 class="metric-label">User Offline </h2>
                                                    <p class="metric-value h3">
                                                        <sub><i class="oi oi-fork"></i></sub><span class="value" id="offlineUserCount" runat="server"></span>
                                                    </p>
                                                </a>
                                                <!-- /.metric -->
                                            </div>
                                            <!-- /metric column -->
                                            <!-- metric column -->
                                            <div class="col">
                                                <!-- .metric -->

                                                <a href="#" target="_self" runat="server" id="noProj" class="metric metric-bordered align-items-center card p-1">
                                                    <h2 class="metric-label">No of Projects </h2>
                                                    <%--Teams--%>
                                                    <p class="metric-value h3">
                                                        <sub><i class="oi oi-people"></i></sub><span class="value" runat="server" id="noProjCount">0</span>
                                                    </p>
                                                </a>



                                                <!-- /.metric -->
                                            </div>


                                            <!-- /metric column -->
                                        </div>
                                    </div>
                                </div>
                                <!-- /metric row -->


                            </div>
                        </div>
                        <!-- .card -->
                        <section class="card card-fluid">
                            <div id="divgraph" runat="server">
                                <!-- .card-header -->
                                <header class="card-header">
                                    <div class="d-flex align-items-center">
                                        <div class="mr-auto">
                                            <ul class="nav ">
                                                <li class="nav-item">
                                                    <a class="nav-link active show" data-toggle="tab" href="#home">Sales</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" data-toggle="tab" href="#profile">Procurement</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" data-toggle="tab" href="#Accounts">Accounts</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" data-toggle="tab" href="#Construction">Construction</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" data-toggle="tab" href="#Contractor">Sub-Contractor</a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="dropdown">


                                            <div class="input-group mb-3">
                                                <div class="input-group-prepend text-white">
                                                    <span class="input-group-text pl-5 pr-5" id="basic-addon1">Year</span>
                                                </div>
                                                <asp:DropDownList ID="ddlyearSale" runat="server" OnSelectedIndexChanged="ddlyearSale_SelectedIndexChanged" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                                    <asp:ListItem Value="2021">2021</asp:ListItem>
                                                    <asp:ListItem Value="2022" >2022</asp:ListItem>
                                                    <asp:ListItem Value="2023" Selected="True">2023</asp:ListItem>

                                                </asp:DropDownList>
                                                 <asp:DropDownList ID="ddlGraphtype" runat="server" OnSelectedIndexChanged="ddlGraphtype_SelectedIndexChanged" AutoPostBack="true" Width="100px" CssClass="custom-select chzn-select">
                                                    <asp:ListItem Value="line">Line</asp:ListItem>
                                                    <asp:ListItem Value="column" Selected="True">Column</asp:ListItem>
                                                    <asp:ListItem Value="area">Area</asp:ListItem>
                                                    <asp:ListItem Value="pie"> </asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                             

                                        </div>
                                    </div>
                                    <!-- .nav-tabs -->

                                    <!-- /.nav-tabs -->
                                </header>
                                <!-- /.card-header -->
                                <!-- .card-body -->
                                <div class="card-body">
                                    <!-- .tab-content -->
                                    <div id="myTabContent" class="tab-content graph-main" style="width: 100%; height: 375px;">


                                        <div class="tab-pane fade active show" id="home">
                                            <div class="row ">
                                                <div class="col-md-12 text-right">
                                                    <a href="F_34_Mgt/RptAllDashboard.aspx?Type=Sales" target="_blank" class="pull-right">View all <i class="fa fa-fw fa-angle-right"></i></a>
                                                </div>
                                            </div>


                                            <div id="salchart" style="width: 90%; max-height: 325px;"></div>

                                        </div>
                                        <div class="tab-pane fade" id="profile">

                                            <div class="row ">
                                                <div class="col-md-12 text-right">
                                                    <a href="F_34_Mgt/RptAllDashboard.aspx?Type=Purchase" target="_blank" class="float-right d-block">View all <i class="fa fa-fw fa-angle-right"></i></a>
                                                </div>
                                            </div>
                                            <div id="purchart" style="width: 90%; max-height: 325px;"></div>
                                        </div>
                                        <div class="tab-pane fade" id="Accounts">
                                            <div class="row ">
                                                <div class="col-md-12 text-right">
                                                    <a href="F_34_Mgt/RptAllDashboard.aspx?Type=Accounts" target="_blank" class="float-right d-block">View all <i class="fa fa-fw fa-angle-right"></i></a>
                                                </div>
                                            </div>
                                            <div id="accchart" style="width: 90%; height: 325px;"></div>
                                        </div>
                                        <div class="tab-pane fade" id="Construction">


                                            <div class="row ">
                                                <div class="col-md-12 text-right">
                                                    <a href="F_34_Mgt/RptAllDashboard.aspx?Type=Construction" target="_blank" class="float-right">View all <i class="fa fa-fw fa-angle-right"></i></a>
                                                </div>
                                            </div>
                                            <div id="conschart" style="width: 90%; height: 325px;"></div>
                                        </div>
                                        <div class="tab-pane fade" id="Contractor">



                                            <div id="subconchart" style="width: 90%; height: 325px;"></div>
                                        </div>


                                    </div>
                                    <!-- /.tab-content -->
                                </div>
                                <!-- /.card-body -->
                            </div>
                        </section>
                        <!-- /.card -->
                    </div>
                    <div class="col-lg-3">
                        <div class="section-block" id="div3">
                            <!-- metric row -->

                            <!-- .card -->
                            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 280px;">
                                    <h3 class="card-title">Top Active User Today</h3>
                                    <div class="list-group list-group-flush list-group-divider" id="TopActivity" runat="server">
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="section-block" id="divs3">
                            <!-- metric row -->

                            <!-- .card -->
                            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 280px;">
                                    <h3 class="card-title">Today's Schedule
                                    </h3>
                                    <div class="list-group list-group-flush list-group-divider" id="Div2" runat="server">
                                    </div>
                                </div>
                            </div>

                        </div>



                    </div>
                </div>




                <div class="row" id="div_groupUSers" runat="server">
                    <div class="col-md-12">
                        <div id="div4" runat="server">
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


                                        <div id="salchartG" style="width: 90%; max-height: 325px;"></div>

                                    </div>
                                    <div class="tab-pane fade" id="Procurementg">

                                        <div class="row ">
                                            <div class="col-md-12 text-right">
                                                <asp:HyperLink ID="Purchaselink" runat="server" class="pull-right"> View all <i class="fa fa-fw fa-angle-right"></i></asp:HyperLink>


                                            </div>
                                        </div>
                                        <div id="purchartG" style="width: 90%; max-height: 325px;"></div>
                                    </div>
                                    <div class="tab-pane fade" id="Accountsg">
                                        <div class="row ">
                                            <div class="col-md-12 text-right">
                                                <asp:HyperLink ID="Accountsglink" runat="server" class="pull-right"> View all <i class="fa fa-fw fa-angle-right"></i></asp:HyperLink>


                                            </div>
                                        </div>
                                        <div id="accchartG" style="width: 90%; height: 325px;"></div>
                                    </div>
                                    <div class="tab-pane fade" id="Constructiong">


                                        <div class="row ">
                                            <div class="col-md-12 text-right">
                                                <asp:HyperLink ID="Constructionglink" runat="server" class="pull-right"> View all <i class="fa fa-fw fa-angle-right"></i></asp:HyperLink>


                                            </div>
                                        </div>
                                        <div id="conschartG" style="width: 90%; height: 325px;"></div>
                                    </div>
                                    <div class="tab-pane fade" id="Sub-Contractorg">



                                        <div id="subconchartG" style="width: 90%; height: 325px;"></div>
                                    </div>


                                </div>
                                <!-- /.tab-content -->
                            </div>
                            <!-- /.card-body -->
                        </div>

                    </div>
                </div>





                <div id="divuser" runat="server">
                    <div class="card-deck-xl">
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

                            <!-- .card-body -->
                            <div class="card-body" style="min-height: 230px;">
                                <div id="empdata" style="width: 480px;"></div>
                            </div>
                            <!-- /.card-body -->
                        </div>
                        <!-- /.card -->
                    </div>
                </div>

                <div class="row" id="divPostDateChecSchdule" runat="server" visible="false">
                    <div class="card-deck-xl">
                        <!-- .card -->
                        <div class="card card-fluid" id="divd8">
                            <div class="card-header">
                                Today's Cheque Payment
                            </div>
                            <!-- .lits-group -->
                            <div class="card-body" style="min-height: 230px;">

                                <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"
                                    CssClass="table-striped table-hover table-bordered">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvchdat" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Acc. Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgactdesc" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvbankname" runat="server"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "cactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")).Trim(): "")  %>'
                                                    Width="180px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvchnono" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <br />
                            </div>

                        </div>

                    </div>


                </div>
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
                        <!-- grid row -->
                        <%--    <div class="row mb-4">
                    <!-- .col -->
                  <%--  <div class="col">
                      <!-- .has-clearable -->
                      <div class="has-clearable">
                        <button type="button" class="close" aria-label="Close"><span aria-hidden="true"><i class="fa fa-times-circle"></i></span></button> <input type="text" class="form-control" placeholder="Search">
                      </div><!-- /.has-clearable -->
                    </div>--%><!-- /.col -->
                        <!-- .col-auto -->
                        <%--<div class="col-auto">
                      <!-- invite members -->
                      <div class="dropdown">
                        <button class="btn btn-primary" data-toggle="dropdown" data-display="static" aria-haspopup="true" aria-expanded="false"><i class="fas fa-user-plus mr-1"></i> Invite</button> <!-- .dropdown-menu -->
                        <div class="dropdown-menu dropdown-menu-right dropdown-menu-rich stop-propagation">
                          <div class="dropdown-arrow"></div>
                          <div class="dropdown-header"> Add members </div>
                          <div class="form-group px-3 py-2 m-0">
                            <input type="text" class="form-control" placeholder="e.g. @bent10" data-toggle="tribute" data-remote="assets/data/tribute.json" data-menu-container="#people-list" data-item-template="true" data-autofocus="true" data-tribute="true"> <small class="form-text text-muted">Search people by username or email address to invite them.</small>
                          </div>
                          <div id="people-list" class="tribute-inline stop-propagation"></div><a href="#" class="dropdown-footer">Invite member by link <i class="far fa-clone"></i></a>
                        </div><!-- /.dropdown-menu -->
                      </div><!-- /invite members -->
                    </div><!-- /.col-auto -->
                  </div>--%><!-- /grid row -->
                        <!-- .card -->
                        <!-- /.card -->
                        <!-- .card -->

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


    <!-- /.page -->
</asp:Content>

