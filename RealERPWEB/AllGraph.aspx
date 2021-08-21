<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AllGraph.aspx.cs" Inherits="RealERPWEB.AllGraph" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Scripts/highcharts.js"></script>

    <script src="Scripts/highchartexporting.js"></script>


    <script language="javascript" type="text/javascript">
        function GetData() {

            //console.log("GetData Method Called..");

            $.ajax({
                type: "POST",
                url: "AllGraph.aspx/GetAllData",
                data: '{dates: "' + $('#<%=this.txtDate.ClientID%>').val() + '" }',
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (response) {
        //console.log(JSON.parse(response.d));
        var data = JSON.parse(response.d);
        // alert(data['sales'][0]['collamt']);
        funMonthlyGraph(data)
    },
    failure: function (response) {
        //  alert(response);
        alert("f");
    }
            });
}
$(document).ready(function () {


    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);



});
function pageLoaded() {

    GetData();

}


function addplug(plug) {
    // alert(plug);


    $('#<%=this.txtflag.ClientID %>').val(plug);

            switch (plug) {

                case "Procurement":
                case "Construction":
                case "Accounts":
                    $("#OpDate").show();
                    break;
                default:
                    $("#OpDate").hide();
                    break;
            }

        }

        function activetab() {

            var plug = $('#<%=this.txtflag.ClientID %>').val();
            // alert(plug);
            switch (plug) {
                case "Procurement":
                    $('.nav-tabs a[href="#tab1primary"]').tab('show');



                    break;
                case "Sales":



                    $('.nav-tabs a[href="#tab0primary"]').tab('show');
                    break;
                case "Construction":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab2primary"]').tab('show');
                    break;
                case "Accounts":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab3primary"]').tab('show');
                    break;
                case "Collection":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab4primary"]').tab('show');
                    break;
            }



        }
    </script>
    <style>
        .flowMenu ul {
            margin: 0;
        }

            .flowMenu ul li {
                list-style: none;
                padding: 5px 0;
                /*border-bottom: 1px solid #e9e9e9;*/
            }

                .flowMenu ul li a {
                    padding-bottom: 8px;
                    color: #000;
                    font-size: 14px;
                    font-weight: normal;
                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                    font-family: 'Times New Roman';
                }

        .flowMenu h3 {
            background: #046971;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            color: #fff;
            font-family: AR CENA;
            font-size: 18px;
            /*font-weight: bold;*/
            line-height: 40px;
            margin: 5px 0 0;
            padding: 0 0;
            text-decoration: none;
            text-align: center;
        }



        ul.sidebarMenu li {
            display: block;
            list-style: none;
            border: 1px solid #00444C;
            padding: 0;
            /* border-bottom: 0; */
        }

            ul.sidebarMenu li a {
                text-align: left;
                display: block;
                cursor: pointer;
                /* background: #32CD32; */
                background: #046971;
                border-radius: 5px;
                color: #fff;
                text-align: left;
                padding: 0 5px;
                border-bottom: 1px;
                line-height: 30px;
                color: #fff;
                font-size: 13px;
                font-weight: normal;
                text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
            }

                ul.sidebarMenu li a:hover {
                    background: #43b643;
                    color: #fff;
                }

        .AllGraph .nav-tabs {
            border-bottom: 0;
        }

        .sidebarMenu li h5 {
            background: #43b643;
            color: #fff;
            font-size: 15px;
            margin: 0;
            padding: 0;
            line-height: 35px;
            text-align: center;
        }






        #demo {
            margin-top: 30px;
            position: absolute;
            z-index: 200;
            margin-left: 10px;
        }


          
      
         #demo1{
            margin-top:30px;
            position:absolute;
            z-index:200;
            margin-left:1050px;
        }

    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="10000">
            </asp:Timer>

            <triggers> 
                  <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>--%>

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
            <asp:Panel runat="server" ID="plnGrph">


                <div class="container moduleItemWrpper">
                    <div class="contentPart">
                        <div class="row">
                            <div class="col-md-2 ">

<%--                                <button type="button" class="btn btn-info btn-xs" data-toggle="collapse" data-target="#demo"><span class="glyphicon glyphicon-th"></span></button>--%>
                                <div class="pull-right">
                                    <asp:Label ID="lbldatefrm" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>

                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClientClick="GetData()" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>
                                    </div>
                                    <asp:TextBox ID="txtflag" Style="display: none;" runat="server"></asp:TextBox>

                                </div>
                              



                            </div>


                          <%-- <div class="col-md-10">

                                <button type="button" class="btn btn-info btn-xs pull-right" data-toggle="collapse" data-target="#demo1"><span class="glyphicon glyphicon-th"></span></button>



                            </div>--%>

<%--                            <div id="demo" class="collapse">
                                <div class="flowMenu" style="margin-top: 20px;">
                                    <ul class="dashCir block sidebarMenu">


                                        <li>
                                            <h5>Analysis</h5>
                                        </li>

                                        <li><a href="<%=this.ResolveUrl("~/F_22_Sal/SalesInformation.aspx")%>" target="_blank">Sales</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation.aspx")%>" target="_blank">Procurement</a></li>
                                        <li><a href="<%=this.ResolveUrl("F_08_PPlan/ConstructionInfo.aspx")%>" target="_blank">Construction</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_18_MAcc/AccDashBoard.aspx")%>" target="_blank">Accounts</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan")%>" target="_blank">Project Report</a></li>
                                        <%-- <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptManProjectSum.aspx")%>" target="_blank">Project Report 2</a></li>--%>

                                        <%--<li><a href="<%=this.ResolveUrl("~/F_46_GrMgtInter/RptGrpDailyReportJq.aspx")%>" target="_blank">Overall</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccFincStatmnt.aspx")%>" target="_blank">Financial Statement</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_32_Mis/ProjectSummary.aspx")%>" target="_blank">Income Statement Project</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptProjectStatus.aspx?Type=Prjwiseres")%>" target="_blank">Project Resource</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptPrjCostPerSFT.aspx?Type=RemainingCost&prjcode=")%>" target="_blank">Inflation</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptRatioAnalisiys.aspx")%>" target="_blank">Ratio Analysis</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccSpLedger.aspx?Type=ASupConPayment")%>" target="_blank">Payable</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_23_CR/RptReceivedList04.aspx?Type=AllProDuesCollect")%>" target="_blank">Receivable</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_32_Mis/ProjectAnalysis.aspx")%>" target="_blank">Project Analysis</a></li>
                                         

                                    </ul>--%>
                               <%-- </div>
                            </div>--%>
                            
                             <%-- <div id="demo1" class="collapse">
                                    <div class="flowMenu" style="margin-top: 20px; width:120px">
                                    <ul class="dashCir block sidebarMenu">


                                        <li>
                                            <h5>Dashboard</h5>  
                                        </li>

                                        <li><a href="<%=this.ResolveUrl("~/F_32_Mis/ProjectAnalysis.aspx")%>" target="_blank">Project Analysis</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/CompanyOverAllReport.aspx")%>" target="_blank">Messenger</a></li> 
                                        
                                   

                                    </ul>
                                </div>
                                    </div>--%>


                        </div>

                        <!------main panel----------->
                        <div class="row">
                            <div class="col-md-6">
                                <div id="SalesChart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                            </div>
                            <div class="col-md-6">
                                <div id="CollChart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                            </div>
                            <div class="col-md-6">
                                <div id="purchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                            </div>
                            <div class="col-md-6">
                                <div id="prodchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                            </div>
                            <div class="col-md-6">
                                <div id="accchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                            </div>
                            <div class="col-md-6">
                                <div id="balsheetchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                            </div>

                        </div>

                    </div>
                </div>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">

        /////--------------------------Month Graph-------------------------

        function funMonthlyGraph(data) {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
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
                    text: 'Procurement-TK (Crore) ',
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

                    //pointFormat: "{point.y:, .2f} Crore <br>"
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
                    data: [data['pur'][0]['ttlsalamtcore'], data['pur'][1]['ttlsalamtcore'], data['pur'][2]['ttlsalamtcore'], data['pur'][3]['ttlsalamtcore'], data['pur'][4]['ttlsalamtcore'], data['pur'][5]['ttlsalamtcore'], data['pur'][6]['ttlsalamtcore'], data['pur'][7]['ttlsalamtcore'], data['pur'][8]['ttlsalamtcore'], data['pur'][9]['ttlsalamtcore'], data['pur'][10]['ttlsalamtcore'], data['pur'][11]['ttlsalamtcore']],
                    color: '#f4429e'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [data['pur'][0]['tpayamtcore'], data['pur'][1]['tpayamtcore'], data['pur'][2]['tpayamtcore'], data['pur'][3]['tpayamtcore'], data['pur'][4]['tpayamtcore'], data['pur'][5]['tpayamtcore'], data['pur'][6]['tpayamtcore'], data['pur'][7]['tpayamtcore'], data['pur'][8]['tpayamtcore'], data['pur'][9]['tpayamtcore'], data['pur'][10]['tpayamtcore'], data['pur'][11]['tpayamtcore']],
                    color: '#b24942'
                }]
            });






            $('#SalesChart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''

                },

                subtitle: {
                    text: 'Sales-TK(Crore)',
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
                    useHTML: true,

                    //   pointFormat: "{point.y:, .2f} Crore <br>"
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
                    data: [data['sales'][0]['targtsaleamtcore'], data['sales'][1]['targtsaleamtcore'], data['sales'][2]['targtsaleamtcore'], data['sales'][3]['targtsaleamtcore'], data['sales'][4]['targtsaleamtcore'], data['sales'][5]['targtsaleamtcore'], data['sales'][6]['targtsaleamtcore'], data['sales'][7]['targtsaleamtcore'], data['sales'][8]['targtsaleamtcore'], data['sales'][9]['targtsaleamtcore'], data['sales'][10]['targtsaleamtcore'], data['sales'][11]['targtsaleamtcore']],
                    color: '#1581C1'

                }, {

                    name: 'Actual',
                    //color:red,
                    data: [data['sales'][0]['ttlsalamtcore'], data['sales'][1]['ttlsalamtcore'], data['sales'][2]['ttlsalamtcore'], data['sales'][3]['ttlsalamtcore'], data['sales'][4]['ttlsalamtcore'], data['sales'][5]['ttlsalamtcore'], data['sales'][6]['ttlsalamtcore'], data['sales'][7]['ttlsalamtcore'], data['sales'][8]['ttlsalamtcore'], data['sales'][9]['ttlsalamtcore'], data['sales'][10]['ttlsalamtcore'], data['sales'][11]['ttlsalamtcore']],
                    color: '#CA6621'
                }]
            });
            $('#prodchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Construction-TK (Crore) ',
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
                    name: 'Target',
                    data: [data['prod'][0]['taramtcore'], data['prod'][1]['taramtcore'], data['prod'][2]['taramtcore'], data['prod'][3]['taramtcore'], data['prod'][4]['taramtcore'], data['prod'][5]['taramtcore'], data['prod'][6]['taramtcore'], data['prod'][7]['taramtcore'], data['prod'][8]['taramtcore'], data['prod'][9]['taramtcore'], data['prod'][10]['taramtcore'], data['prod'][11]['taramtcore']],
                    color: '#96780a'

                }, {

                    name: 'Excution',
                    //color:red,
                    data: [data['prod'][0]['examtcore'], data['prod'][1]['examtcore'], data['prod'][2]['examtcore'], data['prod'][3]['examtcore'], data['prod'][4]['examtcore'], data['prod'][5]['examtcore'], data['prod'][6]['examtcore'], data['prod'][7]['examtcore'], data['prod'][8]['examtcore'], data['prod'][9]['examtcore'], data['prod'][10]['examtcore'], data['prod'][11]['examtcore']],
                    color: '#990c4b'
                }]
            });

            $('#accchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Accounts-TK (Crore)',
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
                    data: [data['acc'][0]['cramcore'], data['acc'][1]['cramcore'], data['acc'][2]['cramcore'], data['acc'][3]['cramcore'], data['acc'][4]['cramcore'], data['acc'][5]['cramcore'], data['acc'][6]['cramcore'], data['acc'][7]['cramcore'], data['acc'][8]['cramcore'], data['acc'][9]['cramcore'], data['acc'][10]['cramcore'], data['acc'][11]['cramcore']],
                    color: '#138225'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [data['acc'][0]['dramcore'], data['acc'][1]['dramcore'], data['acc'][2]['dramcore'], data['acc'][3]['dramcore'], data['acc'][4]['dramcore'], data['acc'][5]['dramcore'], data['acc'][6]['dramcore'], data['acc'][7]['dramcore'], data['acc'][8]['dramcore'], data['acc'][9]['dramcore'], data['acc'][10]['dramcore'], data['acc'][11]['dramcore']],
                    color: '#aa1811'
                }]
            });

            ////Collection Bar chart 
            $('#CollChart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Collection-TK (Crore)',
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
                    name: 'Target',
                    data: [data['sales'][0]['tarcollamtcore'], data['sales'][1]['tarcollamtcore'], data['sales'][2]['tarcollamtcore'], data['sales'][3]['tarcollamtcore'], data['sales'][4]['tarcollamtcore'], data['sales'][5]['tarcollamtcore'], data['sales'][6]['tarcollamtcore'], data['sales'][7]['tarcollamtcore'], data['sales'][8]['tarcollamtcore'], data['sales'][9]['tarcollamtcore'], data['sales'][10]['tarcollamtcore'], data['sales'][11]['tarcollamtcore']],
                    color: '#42f47a'

                }, {

                    name: 'Actual',
                    //color:red,
                    data: [data['sales'][0]['collamtcrore'], data['sales'][1]['collamtcrore'], data['sales'][2]['collamtcrore'], data['sales'][3]['collamtcrore'], data['sales'][4]['collamtcrore'], data['sales'][5]['collamtcrore'], data['sales'][6]['collamtcrore'], data['sales'][7]['collamtcrore'], data['sales'][8]['collamtcrore'], data['sales'][9]['collamtcrore'], data['sales'][10]['collamtcrore'], data['sales'][11]['collamtcrore']],
                    color: '#454289'
                }]
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
                            format: '{point.y:.1f}%'
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
                                "y": data['balshet'][0]['noncuram'],

                            },
                            {
                                "name": "Current Asset",
                                "y": data['balshet'][0]['curam'],

                            },
                            {
                                "name": "Equity",
                                "y": data['balshet'][0]['equityam'],

                            },
                            {
                                "name": "Non-Current Liabilities",
                                "y": data['balshet'][0]['noncurlia'],

                            },
                            {
                                "name": "Current Liabilities",
                                "y": data['balshet'][0]['curlia'],

                            }
                        ]
                    }
                ]
            });
        }



    </script>
</asp:Content>


