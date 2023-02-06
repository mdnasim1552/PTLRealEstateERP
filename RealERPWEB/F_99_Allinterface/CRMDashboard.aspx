<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CRMDashboard.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.CRMDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {

            document.getElementById('<%= lnkbtnOk.ClientID %>').click();

        });

        function ExecuteGraph(data, data1, data2, gtype) {
            var lead_m = JSON.parse(data);
            var lead_w = JSON.parse(data1);
            var lead_d = JSON.parse(data2);

            $('#chartleadMonths').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Monthly Lead status'
                },
                accessibility: {
                    announceNewData: {
                        enabled: true
                    }
                },
                xAxis: {
                    categories: ['Call', 'Ext.Meeting', 'Int. Meeting', 'Visit', 'Proposal', 'Close'],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total Monthly Lead status '
                    }
                },
                legend: {
                    enabled: false
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y}'
                        }
                    }
                },

                series: [
                    {
                        name: "Target",
                        color: "#A50200",
                        data: [lead_m[0].tcall, lead_m[0].textmeeting, lead_m[0].tintmeeting, lead_m[0].tvisit, lead_m[0].tproposal, lead_m[0].tclose]


                    }, {
                        name: "Achive",
                        color: "#00A28A",
                        data: [lead_m[0].call, lead_m[0].extmeeting, lead_m[0].intmeeting, lead_m[0].visit, lead_m[0].proposal, lead_m[0].close]
                    }
                ],
            });

            var chartlead_w = Highcharts.chart('chartleadweek', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Weekly Lead status'
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
                        text: 'Total Weekly Lead status'
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
                        name: "Weekly Lead Status",
                        colorByPoint: true,
                        data: [
                            {
                                name: "Call",
                                y: parseFloat(lead_w[0].call)
                            },
                            {
                                name: "Ext.Meet",
                                y: parseFloat(lead_w[0].extmeeting)
                            },
                            {
                                name: "Int.Meet",
                                y: parseFloat(lead_w[0].intmeeting)
                            },
                            {
                                name: "visit",
                                y: parseFloat(lead_w[0].visit)
                            },
                            {
                                name: "Proposal",
                                y: parseFloat(lead_w[0].proposal)
                            },

                            {
                                name: "Close",
                                y: parseFloat(lead_w[0].close)
                            }
                        ]
                    }
                ]
            });
            var chartlead_d = Highcharts.chart('chartleadDaily', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Daily Lead status'
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
                        text: 'Total Daily Lead status'
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
                        name: "Daily Lead Status",
                        colorByPoint: true,
                        data: [
                            {
                                name: "Call",
                                y: parseFloat(lead_d[0].call)
                            },
                            {
                                name: "Ext.Meet",
                                y: parseFloat(lead_d[0].extmeeting)
                            },
                            {
                                name: "Int.Meet",
                                y: parseFloat(lead_d[0].intmeeting)
                            },
                            {
                                name: "Visit",
                                y: parseFloat(lead_d[0].visit)
                            },
                            {
                                name: "Proposal",
                                y: parseFloat(lead_d[0].proposal)
                            },

                            {
                                name: "Close",
                                y: parseFloat(lead_d[0].close)
                            }
                        ]
                    }
                ]
            });

            let w = $(".graph-main").width();
            let h = 350;
            chartlead_w.setSize(w, h);
            chartlead_d.setSize(w, h);
            const elem = $(".graph-main")[0];
            let resizeObserver = new ResizeObserver(function () {
                chartlead_w.setSize(w, h);
                chartlead_d.setSize(w, h);
                w = $(".graph-main").width();
            });
            resizeObserver.observe(elem);


        };

    </script>
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



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="card card-fluid container-data mt-5">
        <div class="card-body">
            <div class="row mb-2">
                <div class="col-md-2">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend ">
                            <button class="btn btn-secondary" type="button">From</button>
                        </div>
                        <asp:TextBox ID="txtfodate" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtfodate"></cc1:CalendarExtender>
                    </div>
                </div>

                <div class="col-md-5">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary ml-1" type="button">Team Lead</button>
                        </div>
                        <asp:DropDownList ID="ddlEmpid" ClientIDMode="Static" data-placeholder="Choose Employee.." runat="server"
                            CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="lnkbtnOk" OnClick="lnkbtnOk_Click" Class="btn btn-sm btn-primary" runat="server">Ok</asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <a href="../F_21_MKT/CrmClientInfo?Type=Entry" target="_blank" class="btn btn-sm btn-primary float-right">Interface</a>


                </div>
                <div class="col-md-2">

                    <asp:HyperLink ID="HyperLink2" class="btn btn-sm btn-success float-left" Target="_blank" NavigateUrl="~/F_21_Mkt/RptSalesFunnel" runat="server">Sales Funnel Reports</asp:HyperLink>

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
                                        <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-purple text-white counterup" id="lblrating" runat="server" data-plugin="counterup" >0</span></h4>
                                        <p class="mb-0 mt-1 text-truncate">Valuable Prospect</p>
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
                                        <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-warning text-white counterup" id="lbldws" runat="server" data-plugin="counterup">0</span></h4>
                                        <p class="mb-0 mt-1 text-truncate">Todays Follow-up</p>
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
                                        <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-success text-white counterup" id="lbldwr" runat="server" data-plugin="counterup">0</span></h4>
                                        <p class="mb-0 mt-1 text-truncate">Today's Work Done</p>
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
                                <div class="avatar-md bg-danger rounded-circle mr-2">
                                    <i class="fas fa-times-circle avatar-title font-26 text-white"></i>

                                </div>
                                <div class="media-body align-self-center">
                                    <div class="text-center">
                                        <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-danger text-white counterup" id="lblFreez" runat="server" data-plugin="counterup">0</span></h4>
                                        <p class="mb-0 mt-1 text-truncate">Close/Hold</p>
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
                <div class="col-md-2 col-xl-2">
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





                <div class="col-md-3 col-xl-2">
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
                <div class="col-md-2 col-xl-2">
                    <div class="card  mb-1 ">
                        <div class="card-body mb-2">
                            <asp:HyperLink ID="hyplnkOccasion" runat="server" Target="_blank" class="media">
                                <div class="avatar-md xbg-info rounded-circle mr-2">
                                    <i class="fas fa-birthday-cake avatar-title font-26 text-red"></i>
                                </div>
                                <div class="media-body align-self-center">
                                    <div class="text-center">
                                        <h4 class="font-20 my-0 font-weight-bold"><span class="tile-circle bg-red text-white counterup" id="lblOccasion" runat="server" data-plugin="counterup">0
                                        </span></h4>
                                        <p class="mb-0 mt-1 text-truncate">Celebrate</p>
                                    </div>
                                </div>
                            </asp:HyperLink>


                        </div>
                        <!-- end card-box-->
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="row mb-2  mb-5" style="max-height: 400px;">
        <div class="col-md-3 col-sm-3 col-lg-3">
            <div class="card mb-0 card-fluid graph-main" style="width: 100%; height: 350px;">

                <div id="chartleadDaily" style="max-height: 350px;"></div>

            </div>
        </div>
        <div class="col-md-3 col-sm-3 col-lg-3">
            <div class="card  mb-0 graph-main card-fluid" style="width: 100%; height: 350px;">

                <div id="chartleadweek" style="max-height: 350px;"></div>

            </div>
        </div>
        <div class="col-md-6 col-sm-6 col-lg-6">
            <div class="card mb-0 graph-main card-fluid" style="width: 100%; height: 350px;">

                <div id="chartleadMonths" style="width: 90%; height: 350px;"></div>

            </div>
        </div>
    </div>


 

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
