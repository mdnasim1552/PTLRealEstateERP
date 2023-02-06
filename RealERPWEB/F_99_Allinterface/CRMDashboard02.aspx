<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CRMDashboard02.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.CRMDashboard02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        a:hover {
            text-decoration: none;
            cursor: pointer;
        }

        .metric-label {
            font-weight: bold;
            font-size: 25px;
        }

        .textfont16 {
            padding: 5px 10px;
            border-radius: 10px;
        }

        .font-26 {
            font-size: 45px !important;
        }

        .media-body p {
            font-size: 25px !important;
        }

        .counterup {
            padding: 0px 5px;
        }

        .card-header {
            padding: 5px 12px !important;
            font-size: 15px !important;
        }

        .avatar-md {
            margin-left: 25px;
        }
    </style>
    <script type="text/javascript">


        $(document).ready(function () {

            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });



        function pageLoaded() {

            try {


                $('.chzn-select').chosen({ search_contains: true });


            }

            catch (e) {

                alert(e.message);

            }


        };








        function ExecuteGraph(data1, data2, data3) {
            try {



                
                var lstswise = JSON.parse(data2);
                var lstpwise = JSON.parse(data1);
                var lstleadwise = JSON.parse(data3);
                var sourcehead = [];
                var prohead = [];              
                var leadhead = [];

                for (var i = 0; i < lstswise.length; i++)
                {
                    console.log(lstswise[i]["sourcedesc"]);
                    sourcehead.push(lstswise[i]["sourcedesc"])
                }

                for (var i = 0; i < lstpwise.length; i++) {
                    prohead[i] = lstpwise[i]["prjdesc"];
                }

                for (var i = 0; i < lstleadwise.length; i++) {
                    leadhead.push(lstleadwise[i]["leadst"])
                }

                //Source Wise
                Highcharts.chart('chartswise',
                    {
                        chart: {
                            type: 'bar'
                        },
                        title: {
                            text: 'Source Wise Summary'
                        },
                        subtitle: {
                            text: '',
                            style: {
                                color: '#44994a',
                                fontWeight: 'bold'
                            }
                        },


                        xAxis: {
                            categories: sourcehead,
                            crosshair: true
                        },


                        //xAxis: {
                        //    type: 'category',
                        //    labels:
                        //    {
                        //        formatter: function () {
                        //            if ($.inArray(this.value, prohead) !== -1) {
                        //                return '<span style="fill: maroon;">' + this.value + '</span>';
                        //            } else {
                        //                return this.value;
                        //            }
                        //        },
                        //        style: {
                        //            color: '#000',

                        //        }
                        //    }
                        //},


                        yAxis: {
                            title: {
                                text: ''
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
                            pointFormat:
                                '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                        },

                        "series": [
                            {
                                "name": "",
                                "colorByPoint": true,
                                "data": /*[0, 0, 1, 2, 3, 4]*/
                                    (function () {
                                        // generate an array of random data
                                        var data = [];



                                        for (var key in lstswise) {

                                            if (lstswise.hasOwnProperty(key)) {
                                                data.push([
                                                    lstswise[key].sourcedesc,
                                                    lstswise[key].total, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });


                //Project Wise
                Highcharts.chart('chartprjwise',
                    {
                        chart: {
                            type: 'bar'
                        },
                        title: {
                            text: 'Project Wise Summary'
                        },
                        subtitle: {
                            text: '',
                            style: {
                                color: '#44994a',
                                fontWeight: 'bold'
                            }
                        },


                        xAxis: {
                            categories: prohead,
                            crosshair: true
                        },


                        //xAxis: {
                        //    type: 'category',
                        //    labels:
                        //    {
                        //        formatter: function () {
                        //            if ($.inArray(this.value, prohead) !== -1) {
                        //                return '<span style="fill: maroon;">' + this.value + '</span>';
                        //            } else {
                        //                return this.value;
                        //            }
                        //        },
                        //        style: {
                        //            color: '#000',

                        //        }
                        //    }
                        //},


                        yAxis: {
                            title: {
                                text: ''
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
                            pointFormat:
                                '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                        },

                        "series": [
                            {
                                "name": "",
                                "colorByPoint": true,
                                "data": /*[0, 0, 1, 2, 3, 4]*/
                                    (function () {
                                        // generate an array of random data
                                        var data = [];



                                        for (var key in lstpwise) {
                                          
                                            if (lstpwise.hasOwnProperty(key)) {
                                                data.push([
                                                    lstpwise[key].prjdesc,
                                                    lstpwise[key].total, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });

                //Lead Wise
                Highcharts.chart('chartleadwise',
                    {
                        chart: {
                            type: 'bar'
                        },
                        title: {
                            text: 'Sales Funnel Stage'
                        },
                        subtitle: {
                            text: '',
                            style: {
                                color: '#44994a',
                                fontWeight: 'bold'
                            }
                        },


                        xAxis: {
                            categories: leadhead,
                            crosshair: true
                        },




                        yAxis: {
                            title: {
                                text: ''
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
                            pointFormat:
                                '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                        },

                        "series": [
                            {
                                "name": "",
                                "colorByPoint": true,
                                "data": /*[0, 0, 1, 2, 3, 4]*/
                                    (function () {
                                        // generate an array of random data
                                        var data = [];



                                        for (var key in lstleadwise) {

                                            if (lstleadwise.hasOwnProperty(key)) {
                                                data.push([
                                                    lstleadwise[key].leadst,
                                                    lstleadwise[key].total, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });



            }

            catch (e) {

                alert(e);

            }



            //var chartlead_w = Highcharts.chart('chartleadweek', {
            //    chart: {
            //        type: 'column'
            //    },
            //    title: {
            //        text: 'Weekly Lead status'
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
            //            text: 'Total Weekly Lead status'
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
            //        pointFormat: '<span style="color:{point.color}">{point.name}</span><br/>'
            //    },

            //    series: [
            //        {
            //            name: "Weekly Lead Status",
            //            colorByPoint: true,
            //            data: [
            //                {
            //                    name: "Call",
            //                    y: parseFloat(lead_w[0].call)
            //                },
            //                {
            //                    name: "Ext.Meet",
            //                    y: parseFloat(lead_w[0].extmeeting)
            //                },
            //                {
            //                    name: "Int.Meet",
            //                    y: parseFloat(lead_w[0].intmeeting)
            //                },
            //                {
            //                    name: "visit",
            //                    y: parseFloat(lead_w[0].visit)
            //                },
            //                {
            //                    name: "Proposal",
            //                    y: parseFloat(lead_w[0].proposal)
            //                },

            //                {
            //                    name: "Close",
            //                    y: parseFloat(lead_w[0].close)
            //                }
            //            ]
            //        }
            //    ]
            //});
            //var chartlead_d = Highcharts.chart('chartleadDaily', {
            //    chart: {
            //        type: 'column'
            //    },
            //    title: {
            //        text: 'Daily Lead status'
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
            //            text: 'Total Daily Lead status'
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
            //        pointFormat: '<span style="color:{point.color}">{point.name}</span><br/>'
            //    },

            //    series: [
            //        {
            //            name: "Daily Lead Status",
            //            colorByPoint: true,
            //            data: [
            //                {
            //                    name: "Call",
            //                    y: parseFloat(lead_d[0].call)
            //                },
            //                {
            //                    name: "Ext.Meet",
            //                    y: parseFloat(lead_d[0].extmeeting)
            //                },
            //                {
            //                    name: "Int.Meet",
            //                    y: parseFloat(lead_d[0].intmeeting)
            //                },
            //                {
            //                    name: "Visit",
            //                    y: parseFloat(lead_d[0].visit)
            //                },
            //                {
            //                    name: "Proposal",
            //                    y: parseFloat(lead_d[0].proposal)
            //                },

            //                {
            //                    name: "Close",
            //                    y: parseFloat(lead_d[0].close)
            //                }
            //            ]
            //        }
            //    ]
            //});

            //let w = $(".graph-main").width();
            //let h = 350;
            //chartlead_w.setSize(w, h);
            //chartlead_d.setSize(w, h);
            //const elem = $(".graph-main")[0];
            //let resizeObserver = new ResizeObserver(function () {
            //    chartlead_w.setSize(w, h);
            //    chartlead_d.setSize(w, h);
            //    w = $(".graph-main").width();
            //});
            //resizeObserver.observe(elem);


        };

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <span class="input-group-text">From</span>
                                    <%--<button class="btn btn-secondary" type="button">From</button>--%>
                                </div>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                <div class="input-group-prepend ">
                                    <span class="input-group-text">To</span>
                                    <%--<button class="btn btn-secondary" type="button">To</button>--%>
                                </div>

                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Team Lead</button>
                                </div>
                                <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkbtnOk" OnClick="lnkbtnOk_Click" Class="btn btn-sm btn-primary" runat="server">Ok</asp:LinkButton>
                        </div>
                        <div class="col-md-1">
                            <a href="../F_21_MKT/CrmClientInfo?Type=Entry" target="_blank" class="btn btn-sm btn-primary float-right">Interface</a>


                        </div>
                        <div class="col-md-1">

                            <asp:HyperLink ID="HyperLink2" class="btn btn-sm btn-success float-left" Target="_blank" NavigateUrl="~/F_21_Mkt/RptSalesFunnel" runat="server">Sales Funnel</asp:HyperLink>

                        </div>

                         <div class="col-md-1">

                            <asp:HyperLink ID="hlnkMonthlySales" class="btn btn-sm btn-success float-left" Target="_blank" NavigateUrl="~/F_21_MKT/MonthsWiseSale?Type=CRM" runat="server">Monthly Sales</asp:HyperLink>

                        </div>
                    </div>

                    <div class="row mb-2">
                        <div class="col-md-6 col-xl-3">
                            <div class="card  mb-2 ">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-dark rounded-circle mr-2">
                                            <i class="fas fa-users avatar-title font-26 text-dark"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-center">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-dark text-white counterup" id="lblprospect" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1  text-truncate">Prospect</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">90%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="90" aria-valuemin="0" aria-valuemax="100" style="width: 90%">
                                                <span class="sr-only">90% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card  mb-2 ">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fas fa-gem avatar-title font-26 text-purple"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-center">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-purple text-white counterup" id="lblqualifylead" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Qualified Prospect</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">60%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                <span class="sr-only">60% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card  mb-1 ">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-warning rounded-circle mr-2">
                                            <i class="fas fa-binoculars avatar-title font-26 text-warning"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-center">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-warning text-white counterup" id="lblnego" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Negotiation</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">60%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                <span class="sr-only">60% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>

                        <div class="col-md-6 col-xl-3">
                            <div class="card  mb-1 ">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md bg-success rounded-circle mr-2">
                                            <i class="fas fa-check-circle avatar-title font-26 text-white"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-center">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-success text-white counterup" id="lblfinnego" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Final Negotiation</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase">Target <span class="float-right">60%</span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-info" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                <span class="sr-only">60% Complete</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>

                        <div class="col-md-6 col-xl-3">
                            <div class="card  mb-1 ">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fas fa-cart-plus avatar-title font-26 text-success"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-center">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-success text-white counterup" id="lblcsigned" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Sold</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase"><span class="float-right"></span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-primary" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                                <span class="sr-only"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>
                        <div class="col-md-6 col-xl-3">
                            <div class="card  mb-1 ">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fas fa-door-closed avatar-title font-26 text-danger"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-center">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-danger text-white counterup" id="lbllost" runat="server" data-plugin="counterup">0
                                                </span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Lost</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase"><span class="float-right"></span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-primary" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                                <span class="sr-only"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>





                        <div class="col-md-6 col-xl-3">
                            <div class="card  mb-1 ">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fas fa-database avatar-title font-26 text-white"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-center">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-primary text-white counterup" id="lblDatablank" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Data Bank</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase"><span class="float-right"></span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-primary" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                                <span class="sr-only"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>


                         <div class="col-md-3 col-xl-3">
                            <div class="card  mb-1 ">
                                <div class="card-body mb-2">
                                    <div class="media">
                                        <div class="avatar-md xbg-info rounded-circle mr-2">
                                            <i class="fa fa-handshake avatar-title font-26 text-success"></i>

                                        </div>
                                        <div class="media-body align-self-center">
                                            <div class="text-center">
                                                <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-success text-white counterup" id="lbltosign" runat="server" data-plugin="counterup">0</span></h4>
                                                <p class="mb-0 mt-1 text-truncate">Total Sign</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4 d-none">
                                        <h6 class="text-uppercase"><span class="float-right"></span></h6>
                                        <div class="progress progress-sm m-0">
                                            <div class="progress-bar bg-primary" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                                <span class="sr-only"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-box-->
                            </div>
                        </div>




                    </div>
                </div>
            </div>


            <div class="row mb-2  mb-5" style="max-height: 300px;">

                <div class="col-md-4 col-sm-4 col-lg-4">
                    <div class="card mb-0 card-fluid graph-main" style="width: 100%; height: 350px;">

                        <div id="chartswise" style="max-height: 350px;"></div>

                    </div>
                </div>


                <div class="col-md-4 col-sm-4 col-lg-4">
                    <div class="card  mb-0 graph-main card-fluid" style="width: 100%; height: 350px;">

                        <div id="chartprjwise" style="max-height: 350px;"></div>

                    </div>
                </div>




                <div class="col-md-4 col-sm-4 col-lg-4">
                    <div class="card mb-0 graph-main card-fluid" style="width: 100%; height: 350px;">

                        <div id="chartleadwise" style="width: 90%; height: 350px;"></div>

                    </div>
                </div>


               <%-- <div class="col-md-3 col-sm-3 col-lg-3">
                    <div class="card mb-0 graph-main card-fluid" style="width: 100%; height: 350px;">

                        <div id="chartcallwise" style="width: 90%; height: 350px;"></div>

                    </div>--%>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
