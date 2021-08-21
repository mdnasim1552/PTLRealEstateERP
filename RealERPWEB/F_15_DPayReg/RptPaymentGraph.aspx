<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptPaymentGraph.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.RptPaymentGraph" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .highcharts-axis-labels highcharts-xaxis-labels {
            color: maroon !important;
            fill: maroon !important;
            font-weight: normal !important;
            text-decoration: none !important;
        }
    </style>
    <script src="../Scripts/highchart2.js"></script>
    
    <script src="../Scripts/HighChartExportData.js"></script>

    <script language="javascript" type="text/javascript">





        $(document).ready(function () {
            var comcod = <%=this.GetCompCode()%>;
            $("#hlnkpostdatedcheque").attr("href", "../F_17_Acc/PostDatedChq.aspx?comcod=" + comcod);
            $("#hlnkPendingbill").attr("href", "../F_17_Acc/AccPurNotUpdated.aspx?Type=Report&comcod=" + comcod);
            $("#hlnkbillreg").attr("href", "BillRegInterface.aspx?Type=Report&comcod=" + comcod);
            $("#hlnkapprovbil").attr("href", "RptBillStatusInf.aspx?Type=Report&comcod=" + comcod);
        });

        // F_15_DPayReg/

        function funcFundRequirement(ddaily, dweekly, dmonthly, lstcatwise, lstacthead, lstpaywithpro) {

            var ddaily = JSON.parse(ddaily);
            var dweekly = JSON.parse(dweekly);
            var dmonthly = JSON.parse(dmonthly);
            var lstcatwise = JSON.parse(lstcatwise);
            var lstacthead = JSON.parse(lstacthead);
            var lstpaywithpro = JSON.parse(lstpaywithpro);
            var aracthead = [];
            var arrmonth = [];
            var arpmonth = [];
            var arbpmonth = [];
            var arppmonth = [];
            var artmonth = [];

            var arrweek = [];
            var arpweek = [];
            var arbpweek = [];
            var arppweek = [];
            var artweek = [];

            var arrday = [];
            var arpday = [];
            var arbpday = [];
            var arppday = [];
            var artday = [];


            var arbdet = [];
            var categories = []


            for (var key in lstacthead) {

                aracthead.push({ name: lstacthead[key].actdesc, y: lstacthead[key].amt });

            }



            // Details

            for (var key in lstpaywithpro) {

                arbdet.push(
                    { name: lstpaywithpro[key].grpdesc, y: lstpaywithpro[key].billam });




            }




            // Monthly 
            for (var key in dmonthly) {

                categories.push(dmonthly[key].grpdesc);

                if (dmonthly[key].grp == "A") {
                    arrmonth.push(dmonthly[key].month1);
                    arrmonth.push(dmonthly[key].month2);
                    arrmonth.push(dmonthly[key].month3);
                    arrmonth.push(dmonthly[key].monthab3);
                    arrmonth.push(dmonthly[key].tobill);

                }

                else if (dmonthly[key].grp == "B") {

                    arpmonth.push(dmonthly[key].month1);
                    arpmonth.push(dmonthly[key].month2);
                    arpmonth.push(dmonthly[key].month3);
                    arpmonth.push(dmonthly[key].monthab3);
                    arpmonth.push(dmonthly[key].tobill);




                }
                else if (dmonthly[key].grp == "C") {


                    arbpmonth.push(dmonthly[key].month1);
                    arbpmonth.push(dmonthly[key].month2);
                    arbpmonth.push(dmonthly[key].month3);
                    arbpmonth.push(dmonthly[key].monthab3);
                    arbpmonth.push(dmonthly[key].tobill);

                }

                else if (dmonthly[key].grp == "D") {


                    arppmonth.push(dmonthly[key].month1);
                    arppmonth.push(dmonthly[key].month2);
                    arppmonth.push(dmonthly[key].month3);
                    arppmonth.push(dmonthly[key].monthab3);
                    arppmonth.push(dmonthly[key].tobill);




                }

                else {


                    artmonth.push(dmonthly[key].month1);
                    artmonth.push(dmonthly[key].month2);
                    artmonth.push(dmonthly[key].month3);
                    artmonth.push(dmonthly[key].monthab3);
                    artmonth.push(dmonthly[key].tobill);




                }


            }


            console.log(arrmonth);
            console.log(arpmonth);
            console.log(artmonth);
            console.log(categories);

            // Weekly 
            for (var key in dweekly) {

                if (dweekly[key].grp == "A") {
                    arrweek.push(dweekly[key].week1);
                    arrweek.push(dweekly[key].week2);
                    arrweek.push(dweekly[key].week3);
                    arrweek.push(dweekly[key].week4);
                    arrweek.push(dweekly[key].tobill);
                }

                else if (dweekly[key].grp == "B") {
                    arpweek.push(dweekly[key].week1);
                    arpweek.push(dweekly[key].week2);
                    arpweek.push(dweekly[key].week3);
                    arpweek.push(dweekly[key].week4);
                    arpweek.push(dweekly[key].tobill);
                }
                else if (dweekly[key].grp == "C") {
                    arbpweek.push(dweekly[key].week1);
                    arbpweek.push(dweekly[key].week2);
                    arbpweek.push(dweekly[key].week3);
                    arbpweek.push(dweekly[key].week4);
                    arbpweek.push(dweekly[key].tobill);
                }

                else if (dweekly[key].grp == "D") {
                    arppweek.push(dweekly[key].week1);
                    arppweek.push(dweekly[key].week2);
                    arppweek.push(dweekly[key].week3);
                    arppweek.push(dweekly[key].week4);
                    arppweek.push(dweekly[key].tobill);
                }

                else {

                    artweek.push(dweekly[key].week1);
                    artweek.push(dweekly[key].week2);
                    artweek.push(dweekly[key].week3);
                    artweek.push(dweekly[key].week4);
                    artweek.push(dweekly[key].tobill);
                }


            }


            // Daily 
            for (var key in ddaily) {

                if (ddaily[key].grp == "A") {
                    arrday.push(ddaily[key].day1);
                    arrday.push(ddaily[key].day2);
                    arrday.push(ddaily[key].day3);
                    arrday.push(ddaily[key].day4);
                    arrday.push(ddaily[key].day5);
                    arrday.push(ddaily[key].day6);
                    arrday.push(ddaily[key].day7);
                    arrday.push(ddaily[key].tobill);

                }

                else if (dweekly[key].grp == "B") {
                    arpday.push(ddaily[key].day1);
                    arpday.push(ddaily[key].day2);
                    arpday.push(ddaily[key].day3);
                    arpday.push(ddaily[key].day4);
                    arpday.push(ddaily[key].day5);
                    arpday.push(ddaily[key].day6);
                    arpday.push(ddaily[key].day7);
                    arpday.push(ddaily[key].tobill);
                }
                else if (dweekly[key].grp == "C") {
                    arbpday.push(ddaily[key].day1);
                    arbpday.push(ddaily[key].day2);
                    arbpday.push(ddaily[key].day3);
                    arbpday.push(ddaily[key].day4);
                    arbpday.push(ddaily[key].day5);
                    arbpday.push(ddaily[key].day6);
                    arbpday.push(ddaily[key].day7);
                    arbpday.push(ddaily[key].tobill);

                }
                else if (dweekly[key].grp == "D") {
                    arppday.push(ddaily[key].day1);
                    arppday.push(ddaily[key].day2);
                    arppday.push(ddaily[key].day3);
                    arppday.push(ddaily[key].day4);
                    arppday.push(ddaily[key].day5);
                    arppday.push(ddaily[key].day6);
                    arppday.push(ddaily[key].day7);
                    arppday.push(ddaily[key].tobill);
                }

                else {

                    artday.push(ddaily[key].day1);
                    artday.push(ddaily[key].day2);
                    artday.push(ddaily[key].day3);
                    artday.push(ddaily[key].day4);
                    artday.push(ddaily[key].day5);
                    artday.push(ddaily[key].day6);
                    artday.push(ddaily[key].day7);
                    artday.push(ddaily[key].tobill);
                }


            }




            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });



            $('#chartpaywpro').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Bill Details',
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
                        text: 'Amount in Lac'
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
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>',
                    enabled: false
                },







                series: [{
                    name: '',
                    "colorByPoint": true,
                    data: arbdet,
                }



                ],



            });



            $('#chartdayily').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },


                subtitle: {
                    text: 'Payment Due - Daily',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }

                },
                xAxis: {
                    categories: ['Day1', 'Day2', 'Day3', 'Day4', 'Day5', 'Day6', 'Day7', 'Total']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: ''
                    }
                },
                legend: {
                    enabled: false
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

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}',
                    enabled: true
                },






                series: [




                    {
                        name: 'Total',
                        data: artday
                    },


                    {
                        name: 'Bill in Process',
                        data: arbpday
                    },


                    {
                        name: 'Approved Bills',
                        data: arrday
                    },


                    {
                        name: 'Cheque in Process',
                        data: arppday
                    },


                    {
                        name: 'PDC',
                        data: arpday
                    },


                ]
            });


            $('#chartweekly').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },


                subtitle: {
                    text: 'Payment Due - Weekly',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }

                },
                xAxis: {
                    categories: ['Week 1', 'Week 2', 'Week 3', 'Week 4', 'Total']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: ''
                    }
                },
                legend: {
                    enabled: false
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

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}',
                    enabled: true
                },





                series: [




                    {
                        name: 'Total',
                        data: artweek
                    },


                    {
                        name: 'Bill in Process',
                        data: arbpweek
                    },


                    {
                        name: 'Approved Bills',
                        data: arrweek
                    },
                    {
                        name: 'Cheque in Process',
                        data: arppweek
                    },


                    {
                        name: 'PDC',
                        data: arpweek
                    },


                ]
            });



            $('#chartmontyly').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },


                subtitle: {
                    text: 'Payment Due - Monthly',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }

                },
                xAxis: {
                    categories: ['Month1', 'Month2', 'Month3', 'Above Month3', 'Total']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: ''
                    }
                },
                legend: {
                    enabled: false
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

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}',
                    enabled: true
                },




                series: [




                    {
                        name: 'Total',
                        data: artmonth
                    },


                    {
                        name: 'Bill in Process',
                        data: arbpmonth
                    },



                    {
                        name: 'Approved Bills',
                        data: arrmonth
                    },

                    {
                        name: 'Cheque in Process',
                        data: arppmonth
                    },

                    {
                        name: 'PDC',
                        data: arpmonth
                    },


                ]
            });















            $('#charbillcat').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Bills (Catagrory Wise) ',
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
                        text: 'Amount in Lac'
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
                            format: '{point.y:.1f}Tk'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}'
                },

                "series": [
                    {
                        "name": "Bills (Catagrory Wise)",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Supplier",
                                "y": lstcatwise[0]["supam"],

                            },
                            {
                                "name": "Sub-Contractor",
                                "y": lstcatwise[0]["conam"],

                            },
                            {
                                "name": "General",
                                "y": lstcatwise[0]["genam"],

                            }



                        ]
                    }
                ]
            });




            $('#chartacchead').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Bills (Group Wise)',
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
                        text: 'Amount in Lac'
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
                            format: '{point.y:.2f}Tk'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>',
                    enabled: false
                },

                series: [{
                    name: 'Budget',
                    color: '#b2e8a0',
                    //data: [{
                    //    name: 'Republican',
                    //    y: 5,
                    //    drilldown: 'republican-2010'
                    //}, {
                    //    name: 'Democrats',
                    //    y: 2,
                    //    drilldown: 'democrats-2010'
                    //}, {
                    //    name: 'Other',
                    //    y: 4,
                    //    drilldown: 'other-2010'
                    //}]
                    data: aracthead,
                }

                ],


            });












        }




    </script>
    <style>
      
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


            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lbldatefrm" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" AutoCompleteType="Disabled" CssClass="form-control"
                                    ClientIDMode="Static"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate" Enabled="true"></cc1:CalendarExtender>

                            </div>

                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="Bank Balance"></asp:Label>
                                <asp:HyperLink ID="Hyplnkbal" runat="server" ToolTip="Click Details bank position" CssClass="btn btn-danger" NavigateUrl="~/F_17_Acc/AccTrialBalance.aspx?Type=BankPosition&comcod=&Date2=&Date1=" Target="_blank"></asp:HyperLink>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="form-group">

                                <asp:HyperLink ID="hlnkpostdatedcheque" runat="server" Target="_blank" Style="margin-top: 20px;" CssClass="btn btn-warning" Font-Size="11px" ForeColor="Black" Font-Underline="false" ClientIDMode="Static">Post Dated Cheque</asp:HyperLink>
                                <asp:HyperLink ID="hlnkapprovbil" runat="server" Target="_blank" Style="margin-top: 20px;" CssClass="btn btn-warning" Font-Size="11px" ForeColor="Black" Font-Underline="false" ClientIDMode="Static">Approve Bills</asp:HyperLink>
                                <asp:HyperLink ID="hlnkPendingbill" runat="server" Target="_blank" Style="margin-top: 20px;" CssClass="btn btn-warning" Font-Size="11px" ForeColor="Black" Font-Underline="false" ClientIDMode="Static">Bill(In Process)</asp:HyperLink>

                                <asp:HyperLink ID="hlnkbillreg" runat="server" Target="_blank" Style="margin-top: 20px;" CssClass="btn btn-warning" Font-Size="11px" ForeColor="Black" Font-Underline="false" ClientIDMode="Static">Bill Register</asp:HyperLink>
                                <asp:Label ID="lbltk" runat="server" CssClass="lblTxt lblName pull-right" Style="font-size: 16px;">Taka in Lac </asp:Label>

                            </div>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <ul class="nav nav-tabs card-header-tabs" style="margin-top: 20px;">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#billdet">Bill Details</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show " data-toggle="tab" href="#payddy">Payment Due-Daily</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#paydwk">Payment Due-Weekly</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#paydmon">Payment Due-Monthly</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#billtab">Bill</a>
                                    </li>

                                </ul>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="card card-fluid" style="min-height: 350px;">
                        <div class="card-body">
                            <div id="myTabContent" class="tab-content">
                                <div class="tab-pane fade active show" id="billdet">



                                    <div class="col-md-8">


                                        <div id="chartpaywpro" style="width: 550px; height: 350px;"></div>

                                    </div>
                                </div>
                                <div class="tab-pane fade" id="payddy">
                                    <div class="col-md-8">
                                        <div id="chartdayily" style="width: 550px; height: 350px;"></div>

                                    </div>

                                </div>
                                <div class="tab-pane fade" id="paydwk">
                                    <div class="col-md-8">

                                        <div id="chartweekly" style="width: 550px; height: 300px;"></div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="paydmon">
                                    <div class="col-md-8">
                                        <div id="chartmontyly" style="width: 550px; height: 300px;"></div>

                                    </div>
                                </div>
                                 <div class="tab-pane fade" id="billtab">
                                <div class="row">

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8; margin-left: 0px;">


                                        <div id="charbillcat" style="width: 550px; height: 250px;"></div>

                                    </div>

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">


                                        <div id="chartacchead" style="width: 550px; height: 250px;"></div>
                                    </div>

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


