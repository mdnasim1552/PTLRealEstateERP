<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptSalesFunnel.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptSalesFunnel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .grvHeader {
            font-family: 'Century Gothic' !important;
            font-size: 16px !important;
        }

        table tr td {
            font-family: 'Century Gothic' !important;
        }

        .chzn-container-single {
            width: 210px !important;
            height: 34px !important;
        }



            .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }

        /*  .project-slect  .chzn-container-single{
         width: 100px !important;
            height: 34px !important;
        
        }*/
        .profession-slect .chzn-container-single {
            width: 180px !important;
            height: 34px !important;
        }

        .prcntbox {
            display: block !important;
            color: #ff6a00 !important;
            font-size: 14px !important;
        }

        .grvContentarea {
        }

        .srDiv .chzn-container-single {
            width: 155px !important;
        }
    </style>



    <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    <script src="<%=this.ResolveUrl("~/Scripts/highchartexporting.js")%>"></script>




    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            document.getElementById('<%= lbtnOk.ClientID %>').click();

        });
        $('.chzn-container').css('width', '250px');


        function pageLoaded() {
            //$('.datepicker').datepicker({
            //    format: 'mm/dd/yyyy',
            //});

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });


            $('.chzn-select').chosen({ search_contains: true });



            var gvSummary = $('#<%=this.gvSaleFunnel.ClientID %>');
            gvSummary.Scrollable();


        };

        //$('#chkcondate').change(function () {

        //    alert("ok");

        //    if ($('#chkcondate').is(":checked")) {

        //        alert("checked");
        //    }








        //});
        function openModaldis() {

            $('#mdiscussion').modal('toggle');
            //  $('#lbtntfollowup').click();
        };


        // Create the chart
        function ExecuteGraph(data, data1, data2, data3, data4, data5, data6, data8, data9, data10, data11, gtype, data12) {

            try {


                // alert(gtype); 

                //var rbtn = $("input[name='ctl00$ContentPlaceHolder1$rbtnlst']:checked").val();;



                var saldata = JSON.parse(data);
                var empleadst = JSON.parse(data1);
                var empleadstdets = JSON.parse(data2);// employee wise leads deatails
                var prjLead = JSON.parse(data3);// Project wise leads deatails
                var prjLeadTeam = JSON.parse(data4);// Project wise leads deatails

                var prjLeadProfess = JSON.parse(data5);// Project wise leads deatails
                var prjLeadProfssTeam = JSON.parse(data6);// Project wise leads deatails


                var srcleads = JSON.parse(data8);// Source wise leads deatails
                var srcleadsTeam = JSON.parse(data9);// Source TEam wise leads deatails
                var teamlead = JSON.parse(data10);// Source TEam wise leads deatails
                var leadlist = JSON.parse(data12);// Source TEam wise leads deatails



                var rbtn = $("#<%=this.rbtnlst.ClientID %> input[type='radio']:checked").val();
                var leadstatus = $('#<%=this.ddlleadstatus.ClientID%>').val();

                if (rbtn == "Stand By") {

                    if ($.trim(leadstatus).length == 7) {


                        Highcharts.chart('salchartG', {
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
                                pointFormat: '<span style="color:{point.color}">{point.name}</span><br/>'
                            },

                            series: [
                                {
                                    name: "Sales Funnel",
                                    colorByPoint: true,
                                    data: [
                                        {
                                            name: leadlist[0].la,
                                            y: parseFloat(saldata[0].query)
                                        },
                                        {
                                            name: leadlist[0].lb,
                                            y: parseFloat(saldata[0].lead)
                                        },
                                        {
                                            name: leadlist[0].lc,
                                            y: parseFloat(saldata[0].qualiflead)
                                        },
                                        {
                                            name: leadlist[0].ld,
                                            y: parseFloat(saldata[0].nego)
                                        },
                                        {
                                            name: leadlist[0].ld,
                                            y: parseFloat(saldata[0].finalnego)
                                        },
                                        {
                                            name: leadlist[0].lf,
                                            y: parseFloat(saldata[0].win)
                                        }


                                    ]
                                }
                            ]

                        });

                    }

                    else {


                        Highcharts.chart('salchartG', {
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
                                pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                            },

                            series: [
                                {
                                    name: "Sales Funnel",
                                    colorByPoint: true,
                                    data: [
                                        {
                                            name: leadlist[0].la,

                                            y: parseFloat(saldata[0].query)
                                        },
                                        {
                                            name: leadlist[0].lb,
                                            y: parseFloat(saldata[0].lead)
                                        },
                                        {
                                            name: leadlist[0].lc,

                                            y: parseFloat(saldata[0].qualiflead)
                                        },
                                        {
                                            name: leadlist[0].ld,

                                            y: parseFloat(saldata[0].nego)
                                        },
                                        {
                                            name: leadlist[0].le,

                                            y: parseFloat(saldata[0].finalnego)
                                        },


                                        {
                                            name: leadlist[0].lf,
                                            y: parseFloat(saldata[0].win)
                                        }
                                        ,
                                        {
                                            name: "Total",
                                            y: parseFloat(saldata[0].total)
                                        }

                                    ]
                                }
                            ]

                        });

                    }

                    //$('#salchartG').show();
                    // $('#salchartCon').hide();

                }
                else {
                    // $('#salchartG').hide();
                    // $('#salchartCon').show();
                    var leadpcnt = "";
                    var qulpcnt = "";
                    var negpcnt = "";
                    var fgpecnt = "";
                    var winpcnt = "";
                    var qurypcnt = "";

                    var rbtn = $("#<%=this.rbtnlst.ClientID %> input[type='radio']:checked").val();
                    if (rbtn == "Conversion") {
                        qurypcnt = "100 %";




                        leadpcnt = Math.round(saldata[0].query != 0 ? saldata[0].lead * 100 / parseFloat(saldata[0].query) : 0);
                        leadpcnt = leadpcnt + "%";

                        qulpcnt = Math.round(saldata[0].lead != 0 ? saldata[0].qualiflead * 100 / parseFloat(saldata[0].lead) : 0);
                        qulpcnt = qulpcnt + "%";

                        negpcnt = Math.round(saldata[0].qualiflead != 0 ? saldata[0].nego * 100 / parseFloat(saldata[0].qualiflead) : 0);
                        negpcnt = negpcnt + "%";

                        fgpecnt = Math.round(saldata[0].nego != 0 ? saldata[0].finalnego * 100 / parseFloat(saldata[0].nego) : 0);
                        fgpecnt = fgpecnt + "%";

                        winpcnt = Math.round(saldata[0].finalnego != 0 ? saldata[0].win * 100 / parseFloat(saldata[0].finalnego) : 0);
                        winpcnt = winpcnt + "%";


                    }


                    Highcharts.chart('salchartG', {
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
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                        },

                        series: [
                            {
                                name: "Sales Funnel",
                                colorByPoint: true,

                                data: [
                                    {

                                        name: "Query <span class='prcntbox text-'>" + qurypcnt + " </span>",
                                        y: parseFloat(saldata[0].query)
                                    },
                                    {
                                        name: "Lead  <span class='prcntbox text-'>" + leadpcnt + " </span>",
                                        y: parseFloat(saldata[0].lead)
                                    },
                                    {
                                        name: "Qualified Lead <span class='prcntbox'>" + qulpcnt + " </span>",
                                        y: parseFloat(saldata[0].qualiflead)
                                    },
                                    {
                                        name: "Negotiation <span class='prcntbox'>" + negpcnt + " </span>",
                                        y: parseFloat(saldata[0].nego)
                                    },
                                    {
                                        name: "Final Negotiation <span class='prcntbox'>" + fgpecnt + " </span>",
                                        y: parseFloat(saldata[0].finalnego)
                                    },


                                    {
                                        name: "Win <span class='prcntbox'>" + winpcnt + " </span>",
                                        y: parseFloat(saldata[0].win)
                                    }

                                ]
                            }
                        ]

                    });



                }














                //team members graph
                var sumlead = 0;
                var allempdata = [];
                for (var i = 0; i < empleadst.length; i++) {
                    allempdata.push({ "name": empleadst[i].usrname, "y": parseFloat(empleadst[i].total) })
                    sumlead += parseFloat(empleadst[i].total);
                }
                var rbtn = $("#<%=this.rbtnlst.ClientID %> input[type='radio']:checked").val();
                var typea = (rbtn == "Conversion") ? " 100%" : "";

                //console.log(sumlead);
                //console.log("NAhid");

                var empttlead = Highcharts.chart('Allempchart', {
                    chart: {
                        type: gtype
                    },
                    title: {
                        text: 'Sales Funnel Total Query:-  ' + sumlead + typea
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
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                    },

                    series: [
                        {
                            name: "Sales Funnel",
                            colorByPoint: true,
                            data: allempdata
                        }
                    ]

                });

                var leadpcnt = "";
                var qulpcnt = "";
                var negpcnt = "";
                var fgpecnt = "";
                var winpcnt = "";
                var qurypcnt = "";
                //indiviual team graph bar
                for (var i = 0; i < empleadstdets.length; i++) {

                    $('#indEmpStatusBar').append('<div id="r' + empleadstdets[i].teamcode + '" class="col-md-4"></div>')

                    if (rbtn = "Conversion") {
                        qurypcnt = "100 %";


                        leadpcnt = Math.round(parseFloat(empleadstdets[i].query) != 0 ? empleadstdets[i].lead * 100 / parseFloat(empleadstdets[i].query) : 0);
                        leadpcnt = leadpcnt + "%";

                        qulpcnt = Math.round(parseFloat(empleadstdets[i].lead) != 0 ? empleadstdets[i].qualiflead * 100 / parseFloat(empleadstdets[i].lead) : 0);
                        qulpcnt = qulpcnt + "%";

                        negpcnt = Math.round(parseFloat(empleadstdets[i].qualiflead) != 0 ? empleadstdets[i].nego * 100 / parseFloat(empleadstdets[i].qualiflead) : 0);
                        negpcnt = negpcnt + "%";

                        fgpecnt = Math.round(parseFloat(empleadstdets[i].nego) != 0 ? empleadstdets[i].finalnego * 100 / parseFloat(empleadstdets[i].nego) : 0);
                        fgpecnt = fgpecnt + "%";

                        winpcnt = Math.round(parseFloat(empleadstdets[i].finalnego) != 0 ? empleadstdets[i].win * 100 / parseFloat(empleadstdets[i].finalnego) : 0);


                        winpcnt = winpcnt + "%";




                        Highcharts.chart('r' + empleadstdets[i].teamcode, {
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: 'Sales Funnel: ' + empleadstdets[i].usrname + '<img src="../images/userImg.png" alt=ddd>'
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
                                pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                            },

                            series: [
                                {
                                    name: "Sales Funnel",
                                    colorByPoint: true,
                                    data: [
                                        {
                                            name: "Query <span class='prcntbox'>" + qurypcnt + " </span>",
                                            y: parseFloat(empleadstdets[i].query),
                                            drilldown: "Query"
                                        },
                                        {
                                            name: "Lead <span class='prcntbox'>" + leadpcnt + " </span>",
                                            y: parseFloat(empleadstdets[i].lead),
                                            drilldown: "Lead"
                                        },
                                        {
                                            name: "Qualified Lead <span class='prcntbox'>" + qulpcnt + " </span>",
                                            y: parseFloat(empleadstdets[i].qualiflead),
                                            drilldown: "QualifiedLead"
                                        },
                                        {
                                            name: "Negotiation <span class='prcntbox'>" + negpcnt + " </span>",
                                            y: parseFloat(empleadstdets[i].nego),
                                            drilldown: "Negotiation"
                                        },
                                        {
                                            name: "Final Negotiation <span class='prcntbox'>" + fgpecnt + " </span>",
                                            y: parseFloat(empleadstdets[i].finalnego),
                                            drilldown: "Final Negotiation"
                                        },
                                        {
                                            name: "Win <span class='prcntbox'>" + winpcnt + " </span>",
                                            y: parseFloat(empleadstdets[i].win),
                                            drilldown: null
                                        }

                                    ]
                                }
                            ]

                        });
                    }
                    else {
                        Highcharts.chart('r' + empleadstdets[i].teamcode, {
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: 'Sales Funnel: ' + empleadstdets[i].usrname + '<img src="../images/userImg.png" alt=ddd>'
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
                                pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                            },

                            series: [
                                {
                                    name: "Sales Funnel",
                                    colorByPoint: true,
                                    data: [
                                        {
                                            name: "Query",
                                            y: parseFloat(empleadstdets[i].query),
                                            drilldown: "Query"
                                        },
                                        {
                                            name: "Lead",
                                            y: parseFloat(empleadstdets[i].lead),
                                            drilldown: "Lead"
                                        },
                                        {
                                            name: "Qualified Lead",
                                            y: parseFloat(empleadstdets[i].qualiflead),
                                            drilldown: "QualifiedLead"
                                        },
                                        {
                                            name: "Negotiation",
                                            y: parseFloat(empleadstdets[i].nego),
                                            drilldown: "Negotiation"
                                        },
                                        {
                                            name: "Final Negotiation",
                                            y: parseFloat(empleadstdets[i].finalnego),
                                            drilldown: "Final Negotiation"
                                        },
                                        {
                                            name: "Win",
                                            y: parseFloat(empleadstdets[i].win),
                                            drilldown: null
                                        }
                                        ,
                                        {
                                            name: "Total",
                                            y: parseFloat(empleadstdets[i].total)
                                        }
                                    ]
                                }
                            ]

                        });
                    }



                }


                //indiviual team graph pie
                for (var i = 0; i < empleadstdets.length; i++) {

                    $('#indEmpStatusPie').append('<div id="p' + empleadstdets[i].teamcode + '" class="col-md-4"></div>')
                    Highcharts.chart('p' + empleadstdets[i].teamcode, {
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        title: {
                            text: 'Sales Funnel :' + empleadstdets[i].usrname
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.y}</b>'
                        },
                        accessibility: {
                            point: {
                                valueSuffix: ''
                            }
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
                            name: 'Stages',
                            colorByPoint: true,
                            data: [
                                {
                                    name: "Query",
                                    y: parseFloat(empleadstdets[i].query),
                                    sliced: true,
                                    selected: true
                                },
                                {
                                    name: "Lead",
                                    y: parseFloat(empleadstdets[i].lead)

                                },
                                {
                                    name: "Qualified Lead",
                                    y: parseFloat(empleadstdets[i].qualiflead)

                                },
                                {
                                    name: "Negotiation",
                                    y: parseFloat(empleadstdets[i].nego)

                                },
                                {
                                    name: "Final Negotiation",
                                    y: parseFloat(empleadstdets[i].finalnego)
                                },
                                {
                                    name: "Win",
                                    y: parseFloat(empleadstdets[i].win)
                                }
                                ,
                                {
                                    name: "Total",
                                    y: parseFloat(empleadstdets[i].total)
                                }
                            ]
                        }]
                    });
                }


                //project wise
                var sumplead = 0;
                var allprj = [];
                for (var i = 0; i < prjLead.length; i++) {
                    allprj.push({ "name": prjLead[i].prjname, "y": parseFloat(prjLead[i].total) })
                    sumplead += parseFloat(prjLead[i].total);

                }
                //console.log(allempdata);

                var prjlead = Highcharts.chart('kpiProjects', {
                    chart: {
                        type: gtype
                    },
                    title: {
                        text: 'Projects Wise Sales Funnel, Total Query:-  ' + sumplead
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
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                    },

                    series: [
                        {
                            name: "Sales Funnel",
                            colorByPoint: true,
                            data: allprj
                        }
                    ]

                });

                //prjWiseEmpbar
                for (var i = 0; i < empleadstdets.length; i++) {

                    var empid = empleadstdets[i].teamcode;

                    var allprjTeam = [];
                    for (var j = 0; j < prjLeadTeam.length; j++) {
                        var prjteam = prjLeadTeam[j].teamcode
                        if (prjteam == empid) {
                            allprjTeam.push({ "name": prjLeadTeam[j].prjname, "y": parseFloat(prjLeadTeam[j].total) })

                        }
                    }




                    $('#prjWiseEmpbar').append('<div id="prj' + empleadstdets[i].teamcode + '" class="col-md-4"></div>')

                    Highcharts.chart('prj' + empleadstdets[i].teamcode, {
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'Sales Funnel: ' + empleadstdets[i].usrname
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
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                        },

                        series: [
                            {
                                name: "Sales Funnel",
                                colorByPoint: true,
                                data: allprjTeam
                            }
                        ]

                    });

                }

                //prjWiseEmpPie

                for (var i = 0; i < empleadstdets.length; i++) {

                    var empid = empleadstdets[i].teamcode;

                    var allprjTeam = [];
                    for (var j = 0; j < prjLeadTeam.length; j++) {
                        var prjteam = prjLeadTeam[j].teamcode
                        if (prjteam == empid) {
                            allprjTeam.push({ "name": prjLeadTeam[j].prjname, "y": parseFloat(prjLeadTeam[j].total) })

                        }
                    }




                    $('#prjWiseEmpPie').append('<div id="prjpie' + empleadstdets[i].teamcode + '" class="col-md-4"></div>')


                    Highcharts.chart('prjpie' + empleadstdets[i].teamcode, {
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        title: {
                            text: 'Sales Funnel :' + empleadstdets[i].usrname
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.y}</b>'
                        },
                        accessibility: {
                            point: {
                                valueSuffix: ''
                            }
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
                            name: 'Stages',
                            colorByPoint: true,
                            data: allprjTeam
                        }]
                    });

                }


                //Profession wise
                var sumproflead = 0;
                var allProf = [];
                for (var i = 0; i < prjLeadProfess.length; i++) {
                    allProf.push({ "name": prjLeadProfess[i].prjname, "y": parseFloat(prjLeadProfess[i].total) })
                    sumproflead += parseFloat(prjLeadProfess[i].total);

                }
                //console.log(allempdata);

                var proflead = Highcharts.chart('kpiProfesson', {
                    chart: {
                        type: gtype
                    },
                    title: {
                        text: 'Profession Wise Sales Funnel, Total:-  ' + sumproflead
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
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                    },

                    series: [
                        {
                            name: "Sales Funnel",
                            colorByPoint: true,
                            data: allProf
                        }
                    ]

                });

                //ProfessionWiseEmpbar
                for (var i = 0; i < empleadstdets.length; i++) {

                    var empid = empleadstdets[i].teamcode;

                    var allprofTeam = [];
                    for (var j = 0; j < prjLeadProfssTeam.length; j++) {
                        var prjteam = prjLeadProfssTeam[j].teamcode
                        if (prjteam == empid) {
                            allprofTeam.push({ "name": prjLeadProfssTeam[j].prjname, "y": parseFloat(prjLeadProfssTeam[j].total) })

                        }
                    }

                    //bar


                    $('#ProfessionEMPbar').append('<div id="prof' + empleadstdets[i].teamcode + '" class="col-md-4"></div>')

                    Highcharts.chart('prof' + empleadstdets[i].teamcode, {
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'Sales Funnel: ' + empleadstdets[i].usrname
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
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                        },

                        series: [
                            {
                                name: "Sales Funnel",
                                colorByPoint: true,
                                data: allprofTeam
                            }
                        ]

                    });

                    //// pie
                    $('#ProfessionEMPpIE').append('<div id="profpie' + empleadstdets[i].teamcode + '" class="col-md-4"></div>')


                    Highcharts.chart('profpie' + empleadstdets[i].teamcode, {
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        title: {
                            text: 'Sales Funnel :' + empleadstdets[i].usrname
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.y}</b>'
                        },
                        accessibility: {
                            point: {
                                valueSuffix: ''
                            }
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
                            name: 'Stages',
                            colorByPoint: true,
                            data: allprofTeam
                        }]
                    });


                }


                //
                //Source Wise PAnel 
                var sumsrc = 0;
                var allSrc = [];
                for (var i = 0; i < srcleads.length; i++) {
                    allSrc.push({ "name": srcleads[i].prjname, "y": parseFloat(srcleads[i].total) })
                    sumsrc += parseFloat(srcleads[i].total);

                }
                var allSrcGrph = Highcharts.chart('kpiSourch', {
                    chart: {
                        type: gtype
                    },
                    title: {
                        text: 'Source Wise Sales Funnel, Total: ' + sumsrc
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
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                    },

                    series: [
                        {
                            name: "Source Wise Sales Funnel",
                            colorByPoint: true,
                            data: allSrc
                        }
                    ]

                });


                //Source emp wise
                for (var i = 0; i < empleadstdets.length; i++) {

                    var empid = empleadstdets[i].teamcode;

                    var allsrcTeam = [];
                    for (var j = 0; j < srcleadsTeam.length; j++) {
                        var prjteam = srcleadsTeam[j].teamcode
                        if (prjteam == empid) {
                            allsrcTeam.push({ "name": srcleadsTeam[j].prjname, "y": parseFloat(srcleadsTeam[j].total) })

                        }
                    }

                    //bar


                    $('#SourceEMPbar').append('<div id="src' + empleadstdets[i].teamcode + '" class="col-md-4"></div>')

                    Highcharts.chart('src' + empleadstdets[i].teamcode, {
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'Sales Funnel: ' + empleadstdets[i].usrname
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
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                        },

                        series: [
                            {
                                name: "Sales Funnel",
                                colorByPoint: true,
                                data: allsrcTeam
                            }
                        ]

                    });

                    //// pie
                    $('#SourceEMPbarPie').append('<div id="srcpie' + empleadstdets[i].teamcode + '" class="col-md-4"></div>')


                    Highcharts.chart('srcpie' + empleadstdets[i].teamcode, {
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        title: {
                            text: 'Sales Funnel :' + empleadstdets[i].usrname
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.y}</b>'
                        },
                        accessibility: {
                            point: {
                                valueSuffix: ''
                            }
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
                            name: 'Stages',
                            colorByPoint: true,
                            data: allsrcTeam
                        }]
                    });


                }




                let w = $(".graph-main").width();
                let h = 300;
                // chartsal.setSize(w, h);
                empttlead.setSize(w, h);
                prjlead.setSize(w, h);
                proflead.setSize(w, h);
                allSrcGrph.setSize(w, h);



                // Lead Team


                var ateamlead = [];
                for (var i = 0; i < teamlead.length; i++) {
                    ateamlead.push({ "name": teamlead[i].usrname, "y": parseFloat(teamlead[i].total) });

                }

                Highcharts.chart('kpitmleader', {
                    chart: {
                        type: gtype
                    },
                    title: {
                        text: 'Sales Funnel (Team Wise) '
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
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>'
                    },

                    series: [
                        {
                            name: "Sales Funnel",
                            colorByPoint: true,
                            data: ateamlead
                        }
                    ]

                });




                // console.log(ateamlead);



                //const elem = $(".graph-main")[0];             
                //resizeObserver.observe(elem);






                //var empttlead = Highcharts.chart('Allempchart', {
                //    chart: {
                //        type: gtype
                //    },
                //    title: {
                //        text: 'Sales Funnel Total Lead:-  ' + sumlead
                //    },
                //    subtitle: {
                //        text: ''
                //    },
                //    accessibility: {
                //        announceNewData: {
                //            enabled: true
                //        }
                //    },
                //    xAxis: {
                //        type: 'category'
                //    },
                //    yAxis: {
                //        title: {
                //            text: 'Total Sales Funnel Stages'
                //        }
                //    },
                //    legend: {
                //        enabled: false
                //    },
                //    plotOptions: {
                //        series: {
                //            borderWidth: 0,
                //            dataLabels: {
                //                enabled: true,
                //                format: '{point.y}'
                //            }
                //        }
                //    },

                //    tooltip: {
                //        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                //        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b> of total<br/>'
                //    },

                //    series: [
                //        {
                //            name: "Sales Funnel",
                //            colorByPoint: true,
                //            data: allempdata
                //        }
                //    ]

                //});





            }


            catch (e) {
                alert(e.message);

            }

        }
        function printFunc() {
            ////$("#divFilter").hide();
            //var name = $("#txtfodate").val();
            //var name = $("#txtfodate").val();

            //var divContents = document.getElementById("printarea").innerHTML;
            //var a = window.open('', '', 'height=3300px, width=2400px');
            //a.document.write('<html>');
            //a.document.write('<head>');
            //a.document.write('<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" />');

            //a.document.write('<style></style>');

            //a.document.write('</head>');
            //a.document.write('<body>');
            //a.document.write(divContents);


            //a.document.write('</body></html>');
            //a.document.close();
            //a.print();
        }


    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid container-data mt-5" id='printarea'>
                <div class="card-body">
                    <div class="row">
                        <a href="#" target="_blank" id='btn' class="d-none" onclick='printFunc();'>Print</a>
                        <div class="form-check form-check-inline">
                            <asp:RadioButtonList ID="rbtnlst" runat="server" AutoPostBack="True" CssClass="form-check-label" OnSelectedIndexChanged="rbtnlst_SelectedIndexChanged"
                                RepeatColumns="7" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Stand By">Stand By</asp:ListItem>
                                <asp:ListItem Value="Conversion">Conversion</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row mb-2" id="divFilter">
                        <div class="col-md-4 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtfodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfodate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" id="lblToDate" runat="server" type="button">To</button>
                                </div>
                                <asp:TextBox ID="txttodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="Cal3" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" clientidmode="Static" id="lblcondate" runat="server">Con Date</label>
                                    <%--<asp:CheckBox  runat="server" ID="chkcondate" Text=" Con Date" CssClass="btn btn-secondary" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkcondate_CheckedChanged" />--%>
                                </div>
                                <asp:TextBox ID="txtcondate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtcondate" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtcondate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Team Lead</button>
                                </div>
                                <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Project</button>
                                </div>
                                <asp:DropDownList ID="ddlProject" ClientIDMode="Static" data-placeholder="Choose Projects.." runat="server" CssClass="custom-select chzn-select " AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-md-2 p-0">
                            <div class="input-group input-group-alt srDiv">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary pl-0 pr-0" type="button">Source</button>
                                </div>
                                <asp:DropDownList ID="ddlSource" data-placeholder="Choose Source.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 p-0 mt-2">
                            <div class="input-group input-group-alt profession-slect srDiv">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary  pl-0 pr-0" type="button">Lead Status</button>
                                </div>
                                <asp:DropDownList ID="ddlleadstatus" data-placeholder="Choose Lead Status.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlleadstatus_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 p-0 mt-2">
                            <div class="input-group input-group-alt srDiv">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary pl-0 pr-0" type="button">Pref. Loc.</button>
                                </div>
                                <asp:DropDownList ID="ddlPrefLocation" data-placeholder="Choose Pref. Location.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlPrefLocation_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 p-0 mt-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Apartment Size</button>
                                </div>
                                <asp:DropDownList ID="ddlAptSize" data-placeholder="Choose Apt. Size.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlAptSize_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 p-0 mt-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Budget</button>
                                </div>
                                <asp:DropDownList ID="ddlBudget" data-placeholder="Choose Budget.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlBudget_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 10px;">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 p-0 mt-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Page</button>
                                </div>
                                <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>400</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 p-0" style="display: none">
                            <div class="input-group input-group-alt profession-slect">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Profession</button>
                                </div>
                                <asp:DropDownList ID="ddlProfession" data-placeholder="Choose Profession.." runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProfession_SelectedIndexChanged">
                                </asp:DropDownList>
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <div class="card-body">
                                <div id="dvTab">
                                    <ul class="nav nav-tabs" id="myTab" role="tablist">
                                        <li class="nav-item">

                                            <a class="nav-link btn active" data-toggle="tab" href="#tab1" role="tab" aria-controls="tab1" aria-selected="false">Sales Funnel Graph</a>
                                        </li>
                                        <li class="nav-item ml-1">
                                            <a class="nav-link btn" data-toggle="tab" href="#tab2" role="tab" aria-controls="tab2" aria-selected="false">Team Members Graph</a>
                                        </li>
                                        <li class="nav-item ml-1">
                                            <a class="nav-link btn" data-toggle="tab" href="#tab4" role="tab" aria-controls="tab2" aria-selected="false">Projects Graph</a>
                                        </li>
                                        <li class="nav-item ml-1">
                                            <a class="nav-link btn" data-toggle="tab" href="#tab3" role="tab" aria-controls="tab3" aria-selected="false">Profession Graph</a>
                                        </li>

                                        <li class="nav-item ml-1">
                                            <a class="nav-link btn" data-toggle="tab" href="#tab5" role="tab" aria-controls="tab3" aria-selected="false">Sourch Wise Graph</a>
                                        </li>

                                        <li class="nav-item ml-1">
                                            <a class="nav-link btn" data-toggle="tab" href="#tab6" role="tab" aria-controls="tab3" aria-selected="false">Team Leader</a>
                                        </li>
                                    </ul>

                                </div>
                            </div>
                        </div>
                        <div class="col-2 col-sm-2 pading5px">
                            <div class="card-body">
                                <div class="input-group input-group-alt">
                                    <div class="input-group-prepend">
                                        <button class="btn btn-secondary" type="button">Graph Type</button>
                                    </div>
                                    <asp:DropDownList ID="ddlgrpType" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlgrpType_SelectedIndexChanged">
                                        <asp:ListItem Value="column">column</asp:ListItem>
                                        <asp:ListItem Value="line">line</asp:ListItem>
                                        <asp:ListItem Value="pie">pie</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Tab panes -->
                <div class="tab-content p-3">
                    <div class="tab-pane fade active show" id="tab1" role="tabpanel" aria-labelledby="tab1">

                        <div class="col-md-12 text-center graph-main" id="grpBox" runat="server" visible="false">


                            <div id="salchartG" style="height: 325px; width: 550px; margin: 0 auto"></div>
                            <%--<div id="salchartCon" style="height: 325px; width: 450px; margin: 0 auto"></div>--%>
                        </div>
                        <div class="row">

                            <div class="col-md-12 table-responsive">
                                <asp:GridView ID="gvSaleFunnel" runat="server" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvSaleFunnel_PageIndexChanging"
                                    PageSize="15" AllowPaging="true"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvSaleFunnel_RowDataBound" HeaderStyle-Font-Size="11px" Width="800px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField
                                            HeaderText="Project Name">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                    Text="Project Name" Width="180px"></asp:Label>
                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                    CssClass="btn  btn-success  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblgvItmCode" runat="server"
                                                    Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projname")) %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Associate Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvassocname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"associatename"))%>'
                                                    Width="150px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempid" runat="server" Width="320px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode")) %>'></asp:Label>
                                                <asp:Label ID="lsircode" runat="server" Width="350px" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'></asp:Label>
                                                <asp:LinkButton ID="lnkEditfollowup" ForeColor="Chocolate" ClientIDMode="Static" runat="server" Width="250px" OnClick="lnkEditfollowup_Click"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clientname")) %>'> </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Profession Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvProfession" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"profession")) %>'
                                                    Width="150px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Preferred Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPrefLoc" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"prflocation")) %>'
                                                    Width="80px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Apt. Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAptSize" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"aptsize")) %>'
                                                    Width="70px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Budget Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBgdAmt" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"bgdtamt")) %>'
                                                    Width="70px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Source">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSource" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sourtxt")) %>'
                                                    Width="70px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Query">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvquery" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "query")) %>'
                                                    Width="60px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlead" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lead")) %>'
                                                    Width="80px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qualified Lead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqualiflead" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qualiflead")) %>'
                                                    Width="120px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Negotiation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNegotiation" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nego")) %>'
                                                    Width="100px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Final Negotiation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvfNegotiation" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "finalnego")) %>'
                                                    Width="120px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Win">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvwin" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "win")) %>'
                                                    Width="40px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                    <div class="tab-pane fade" id="tab2" role="tabpanel" aria-labelledby="tab2">
                        <div class="row">
                            <div class="col-md-12 graph-main">
                                <div id="Allempchart" style="width: 100%; height: 300px;"></div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 mt-5">
                                <header class="card-header" style="background: #F6F7F9">



                                    <div class="d-flex align-items-center">
                                        <div class="mr-auto">
                                            Individual Employee wise sales Funnel  

                                        </div>
                                        <div class="dropdown">

                                            <div class="form-group">

                                                <ul class="nav nav-tabs" id="myTab2" role="tablist">
                                                    <li class="nav-item">

                                                        <a class="nav-link btn active" data-toggle="tab" href="#ntab1" role="tab" aria-controls="tab1" aria-selected="false">Column Graph</a>
                                                    </li>
                                                    <li class="nav-item ml-1">
                                                        <a class="nav-link btn" data-toggle="tab" href="#ntab2" role="tab" aria-controls="tab2" aria-selected="false">Pie Graph</a>
                                                    </li>


                                                </ul>


                                            </div>



                                        </div>
                                    </div>
                                </header>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="tab-content p-3">
                                <div class="tab-pane fade active show" id="ntab1" role="tabpanel" aria-labelledby="tab1">
                                    <div class="row" id="indEmpStatusBar"></div>

                                </div>
                                <div class="tab-pane fade" id="ntab2" role="tabpanel" aria-labelledby="tab1">
                                    <div class="row" id="indEmpStatusPie"></div>



                                </div>
                            </div>


                        </div>

                    </div>

                    <div class="tab-pane fade" id="tab3" role="tabpanel" aria-labelledby="tab3">
                        <div id="kpiProfesson" style="width: 100%; height: 300px;"></div>


                        <div class="row">
                            <div class="col-md-12">
                                <header class="card-header" style="background: #F6F7F9">



                                    <div class="d-flex align-items-center">
                                        <div class="mr-auto">
                                            Individual Employee Project wise sales Funnel  

                                        </div>
                                        <div class="dropdown">

                                            <div class="form-group">

                                                <ul class="nav nav-tabs" id="myTabkprofess" role="tablist">
                                                    <li class="nav-item">

                                                        <a class="nav-link btn active" data-toggle="tab" href="#prtab1" role="tab" aria-controls="prtab1" aria-selected="false">Column Graph</a>
                                                    </li>
                                                    <li class="nav-item ml-1">
                                                        <a class="nav-link btn" data-toggle="tab" href="#prtab2" role="tab" aria-controls="prtab2" aria-selected="false">Pie Graph</a>
                                                    </li>


                                                </ul>


                                            </div>



                                        </div>
                                    </div>
                                </header>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="tab-content p-3">
                                <div class="tab-pane fade active show" id="prtab1" role="tabpanel" aria-labelledby="prtab1">
                                    <div class="row" id="ProfessionEMPbar"></div>

                                </div>
                                <div class="tab-pane fade" id="prtab2" role="tabpanel" aria-labelledby="prtab1">
                                    <div class="row" id="ProfessionEMPpIE"></div>



                                </div>
                            </div>


                        </div>
                    </div>



                    <div class="tab-pane fade" id="tab4" role="tabpanel" aria-labelledby="tab3">

                        <div class="row">
                            <div class="col-md-12">
                                <div id="kpiProjects" style="width: 100%; height: 300px;"></div>

                            </div>
                        </div>

                        <div class="row mt-5">
                            <div class="col-md-12">
                                <header class="card-header pt-0 pb-0" style="background: #F6F7F9">



                                    <div class="d-flex align-items-center">
                                        <div class="mr-auto">
                                            Individual Employee Project wise sales Funnel  

                                        </div>
                                        <div class="dropdown">

                                            <div class="form-group">

                                                <ul class="nav nav-tabs" id="myTabkpproj" role="tablist">
                                                    <li class="nav-item">

                                                        <a class="nav-link btn active" data-toggle="tab" href="#ptab1" role="tab" aria-controls="tab1" aria-selected="false">Column Graph</a>
                                                    </li>
                                                    <li class="nav-item ml-1">
                                                        <a class="nav-link btn" data-toggle="tab" href="#ptab2" role="tab" aria-controls="tab2" aria-selected="false">Pie Graph</a>
                                                    </li>


                                                </ul>


                                            </div>



                                        </div>
                                    </div>
                                </header>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="tab-content p-3">
                                <div class="tab-pane fade active show" id="ptab1" role="tabpanel" aria-labelledby="ptab1">
                                    <div class="row" id="prjWiseEmpbar">
                                    </div>

                                </div>
                                <div class="tab-pane fade" id="ptab2" role="tabpanel" aria-labelledby="ptab1">
                                    <div class="row" id="prjWiseEmpPie">
                                    </div>


                                </div>
                            </div>


                        </div>

                    </div>

                    <div class="tab-pane fade" id="tab5" role="tabpanel" aria-labelledby="tab5">

                        <div class="row">
                            <div class="col-md-12">
                                <div id="kpiSourch" style="width: 100%; height: 300px;"></div>

                            </div>
                        </div>

                        <div class="row mt-5">
                            <div class="col-md-12">
                                <header class="card-header pt-0 pb-0" style="background: #F6F7F9">



                                    <div class="d-flex align-items-center">
                                        <div class="mr-auto">
                                            Individual Employee Source wise sales Funnel  

                                        </div>
                                        <div class="dropdown">

                                            <div class="form-group">

                                                <ul class="nav nav-tabs" id="mySrc" role="tablist">
                                                    <li class="nav-item">

                                                        <a class="nav-link btn active" data-toggle="tab" href="#tabsrc1" role="tab" aria-controls="tabsrc1" aria-selected="false">Column Graph</a>
                                                    </li>
                                                    <li class="nav-item ml-1">
                                                        <a class="nav-link btn" data-toggle="tab" href="#tabsrc2" role="tab" aria-controls="tabsrc2" aria-selected="false">Pie Graph</a>
                                                    </li>


                                                </ul>


                                            </div>



                                        </div>
                                    </div>
                                </header>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="tab-content p-3">
                                <div class="tab-pane fade active show" id="tabsrc1" role="tabpanel" aria-labelledby="tabsrc1">
                                    <div class="row" id="SourceEMPbar">
                                    </div>

                                </div>
                                <div class="tab-pane fade" id="tabsrc2" role="tabpanel" aria-labelledby="tabsrc2">
                                    <div class="row" id="SourceEMPbarPie">
                                    </div>


                                </div>
                            </div>


                        </div>

                    </div>


                    <div class="tab-pane fade" id="tab6" role="tabpanel" aria-labelledby="tab6">

                        <div class="row">
                            <div class="col-md-12">
                                <div id="kpitmleader" style="width: 100%; height: 300px;"></div>

                            </div>
                        </div>

                        <div class="row mt-5">
                            <div class="col-md-12">
                                <header class="card-header pt-0 pb-0" style="background: #F6F7F9">



                                    <div class="d-flex align-items-center">
                                        <div class="mr-auto">
                                            Individual Employee Source wise sales Funnel  

                                        </div>
                                        <div class="dropdown">

                                            <div class="form-group">

                                                <ul class="nav nav-tabs" id="myteamlead" role="tablist">
                                                    <li class="nav-item">

                                                        <a class="nav-link btn active" data-toggle="tab" href="#tabtmleader1" role="tab" aria-controls="tabsrc1" aria-selected="false">Column Graph</a>
                                                    </li>
                                                    <li class="nav-item ml-1">
                                                        <a class="nav-link btn" data-toggle="tab" href="#tabtmleader2" role="tab" aria-controls="tabsrc2" aria-selected="false">Pie Graph</a>
                                                    </li>


                                                </ul>


                                            </div>



                                        </div>
                                    </div>
                                </header>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="tab-content p-3">
                                <div class="tab-pane fade active show" id="tabtmleader1" role="tabpanel" aria-labelledby="tabsrc1">
                                    <div class="row" id="tmleaderbar">
                                    </div>

                                </div>
                                <div class="tab-pane fade" id="tabtmleader2" role="tabpanel" aria-labelledby="tabsrc2">
                                    <div class="row" id="tmleaderPie">
                                    </div>


                                </div>
                            </div>


                        </div>

                    </div>




                </div>
            </div>
            <div id="mdiscussion" class="modal fade animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-full-width modal-lg ">
                    <div class="modal-content modal-content-full-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>
                                Discussion </h4>

                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>


                        </div>
                        <div class="modal-body ">



                            <div class="row">

                                <div class="col-xs-9 col-sm-9 col-md-9">

                                    <p>
                                        <strong><span id="lblprosname" runat="server"></span></strong>
                                        <br>
                                        <strong>Primary : </strong><span id="lblprosphone" runat="server"></span>
                                        <br>
                                        <strong>Home Address: </strong><span id="lblprosaddress" runat="server"></span>
                                        <br>

                                        <strong>Notes: </strong><span id="lblnotes" runat="server"></span>
                                        <br>
                                    </p>

                                    <p>

                                        <strong>Prefered Area: </strong><span id="lblpreferloc" runat="server"></span>
                                        <br>
                                        <strong>Appartment Size: </strong><span id="lblaptsize" runat="server"></span>

                                        <asp:HiddenField ID="lblproscod" runat="server" />
                                        <asp:HiddenField ID="lbleditempid" runat="server" />
                                    </p>
                                </div>



                            </div>



                            <div class="row">


                                <div class="col-md-12 col-lg-12">
                                    <div class="row">
                                        <asp:Repeater ID="rpclientinfo" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>


                                                <div class="col-md-12  col-lg-12">
                                                    <div class="well">

                                                        <div class="col-sm-12 panel">

                                                            <div class=" col-sm-12">

                                                                <p>
                                                                    <strong>
                                                                        <%# DataBinder.Eval(Container, "DataItem.prosdesc")%> </strong>

                                                                    <%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString() %>  on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>
                                                                    <br>




                                                                    <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString() %><br>


                                                                    <strong>Summary:</strong><span class="textwrap"><%# DataBinder.Eval(Container, "DataItem.discus").ToString() %></span><br>



                                                                    <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString() %> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                                    <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString() %>





                                                                    <br>
                                                                </p>







                                                            </div>





                                                            <div class="col-md-12 collapse dcomments" id="divreschedule<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>">



                                                                <asp:TextBox ID="txtdate" runat="server" ClientIDMode="Static" CssClass=""></asp:TextBox>
                                                                <cc1:CalendarExtender ID="Cal2" runat="server"
                                                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>





                                                                Subject:
                                                    <textarea name="lblsubjects" id="lblsubjects" style="width: 300px"></textarea>
                                                                Reason:
                                                    <textarea name="lblreason" id="lblreason" style="width: 300px"></textarea>

                                                                <%--<button type="button" class="btn  btn-success btn-xs" onclick="funReschedule('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>')">Post</button>--%>
                                                                <button type="button" class="lbtnschedule">Post</button>

                                                                <input type="hidden" id="lblcdate" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>" />


                                                            </div>



                                                            <%--<asp:LinkButton ID="lbtnComments" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnComments_Click"    data-toggle="collapse" data-target="#dcomments">Comments</asp:LinkButton>--%>



                                                            <div class="col-md-12 collapse dcomments" id="dcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>">

                                                                <textarea name="lblcomments" id="lblcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>" style="width: 300px"></textarea>
                                                                <br>
                                                                <input type="text" name="txtcomdate" class="datepicker" id="txtcomdate<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("MM/dd/yyyy") %>" style="width: 300px"></input>

                                                                <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments" onclick="funPost('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>')">Post</button>



                                                            </div>

                                                            <%--  <button type="button" class="btn btn-primary btn-xs" runat="server" id="Button1" data-toggle="collapse" data-target="#dcomments" >Comments</button>

                                    <div class="col-md-12 collapse "  id="dcomments">

                                      <input type="text"  name="lblcomments" id="lblcomments" />
                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments"  >Post</button>
                                      


                                    </div>--%>
                                                        </div>
                                                    </div>
                                                </div>



                                            </ItemTemplate>

                                        </asp:Repeater>



                                    </div>
                                </div>


                            </div>









                        </div>




                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

