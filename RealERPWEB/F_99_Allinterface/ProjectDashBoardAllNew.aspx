
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjectDashBoardAllNew.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.ProjectDashBoardAllNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 40px;
            height: 40px;
            margin: -30px 0 0 -50px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            width: 60px;
            height: 60px;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>

    <script src="../Scripts/highcharts.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>
    <script language="javascript" type="text/javascript">

        var comcod, projcode, data;
        var month_names = ["Jan", "Feb", "Mar",
            "Apr", "May", "Jun",
            "Jul", "Aug", "Sep",
            "Oct", "Nov", "Dec"];
        //var day = this.getDate();
        //var month_index = this.getMonth();
        //var year = this.getFullYear();

        //var strDate = "" + day + "-" + month_names[month_index] + "-" + year;

        var d = new Date();
        var month_index = d.getMonth();

        var strDate = d.getDate() + "-" + month_names[month_index] + "-" + d.getFullYear();
        //alert(strDate);
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
            GetData();
            showhide();
            $('#<%=this.ddlprojname.ClientID%>').change(function () {
                $('#dataTable').empty();
                $(".fm2").hide();
                GetData();
            });



        }




        function showhide() {


            $("#LinkButton1").click(function () {
                event.preventDefault();
                $('#overtimeinfo').modal('toggle');
            });
        }

        function GetData() {
            try {

                comcod = <%=this.GetCompCode()%>;
                projcode = $('#<%=this.ddlprojname.ClientID%>').val();
                var strprjcode = '18' + projcode.substring(2, 12);
                var temp = comcod.toString();
                var com = temp.slice(0, 1);
                var opndate = $('#<%=this.lblopndate.ClientID%>').text();
                
                if(com=="1")
                {
                    $("#dDues").hide();
                    $("#dSales").hide();
                    $("#dCMod").hide();
                    $("#dStd").hide();
                    $("#dReg").hide();


                
                }
                $("#btn101").attr("href", "../F_04_Bgd/RptBgdPrjojectNew?Type=Report&comcod=" + comcod + "&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val());
                $("#btn102").attr("href", "../F_08_PPlan/ProTargetTimeBasis?Type=GrpWise&sircode=&flrcod=&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val());
                $("#btn103").attr("href", "../F_08_PPlan/RptProTarget?Type=RealFlow&comcod=" + comcod + "&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val());
                $("#btn104").attr("href", "../F_09_PImp/RptImpExeStatus?Type=BgdVSEx02&comcod=" + comcod + "&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val() + "&date1=&date2=");
                $("#btn105").attr("href", "../F_32_Mis/LinkConstruProgress?pactcode=" + $('#<%=this.ddlprojname.ClientID%>').val() + "&pactdesc=" + $('#<%=this.ddlprojname.ClientID%> option:selected').text() + "&date=" + strDate);
                $("#btn106").attr("href", "../F_09_PImp/RptConsConBillStatus?Type=ConBillSummary&Date1=" + opndate + "&Date2=" + strDate + "&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val());
                $("#btn107").attr("href", "../F_12_Inv/RptInventoryAll?Type=Report&comcod=" + comcod + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn108").attr("href", "../F_12_Inv/RptInventoryAll?Type=Report&comcod=" + comcod + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn109").attr("href", "../F_14_Pro/PurSumMatWise?Type=Report&comcod=" + comcod + "&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val() + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn110").attr("href", "../F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=DaywPur&comcod=" + comcod + "&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val() + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn111").attr("href", "../F_22_Sal/RptSaleSoldunsoldUnit?Type=soldunsold&comcod=" + comcod + "&prjcode=" + strprjcode + "&Date1=");
                $("#btn112").attr("href", "../F_22_Sal/RptSaleSoldunsoldUnit?Type=soldunsold&comcod=" + comcod + "&prjcode=" + strprjcode + "&Date1=");
                $("#btnmonsales").attr("href", "../F_17_Acc/RptAccCollVsClearance?Type=MonSales&comcod=" + comcod);
                $("#btnmoncoll").attr("href", "../F_17_Acc/RptAccCollVsClearance?Type=MonAR&comcod=" + comcod);
                $("#btncledger").attr("href", "../F_23_CR/RptCustPayStatus?Type=ClLedger&prjcode=" + projcode);
              
               
                

                $("#btn113").attr("href", "../F_23_CR/RptCustomerDues?Type=Report&comcod=" + comcod + "&prjcode=" + strprjcode + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn114").attr("href", "../F_24_CC/RptClientModification?WType=CliModfi&prjcode="+ strprjcode + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn115").attr("href", "../F_23_CR/RptSalesReportGen?Type=All&comcod=&prjcode=" + projcode);
                $("#btn201").attr("href", "../F_32_Mis/ProjTrialBalanc?Type=PrjTrailBal&prjcode="+projcode);


               
                $("#btn202").attr("href", "../F_17_Acc/RptAccDTransaction?Type=Accounts&TrMod=ProTrans&comcod=" + comcod + "&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val() + "&Date1=" + opndate + "&Date2=" + strDate);

                $("#btn203").attr("href", "../F_32_Mis/RptPrjCostPerSFT?Type=RemainingCost&comcod=" + comcod + "&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val());
                <%--$("#btn204").attr("href", "F_22_Sal/SalesInformation?Type=Report&comcod=" + comcod + "&projcode=" + $('#<%=this.ddlprojname.ClientID%>').val());--%>
                $("#btn205").attr("href", "../F_25_Reg/RptRegclearacne?Type=Regiscl&prjcode=" + strprjcode);
                $("#btn206").attr("href", "../F_32_Mis/BalanceSheet?Type=BLS&prjcode=" + $('#<%=this.ddlprojname.ClientID%>').val());
                $("#btn207").attr("href", "../F_17_Acc/AccMultiReport?rpttype=detailsTB&comcod=" + comcod + "&actcode=" + strprjcode + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn208").attr("href", "../F_17_Acc/AccMultiReport?rpttype=detailsTB&comcod=" + comcod + "&actcode=" + '26' + projcode.substring(2, 12) + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn209").attr("href", "../F_17_Acc/AccMultiReport?rpttype=detailsTB&comcod=" + comcod + "&actcode=" + '23' + projcode.substring(2, 12) + "&Date1=" + opndate + "&Date2=" + strDate);
                $("#btn210").attr("href", "../F_23_CR/RptSalesReportGen?Type=All&comcod=&prjcode=" + strprjcode);

                $.ajax({
                    type: "POST",
                    url: "ProjectDashBoardAllNew.aspx/GetAllData",
                    contentType: "application/json; charset=utf-8",
                    data: '{comcodi:"' + comcod + '" , projcode: "' + $('#<%=this.ddlprojname.ClientID%>').val() + '"}',
                    dataType: "json",
                    beforeSend: function () {
                        $("#loader").show();
                    },
                    success: function (response) {
                        var data = JSON.parse(response.d);
                        var data1 = data.TBLTAB101;
                        var data2 = data.TBLTAB102;
                        console.log(data2);
                        $("#<%=this.lblvalstartdate.ClientID%>").html(data2[0]);
                        $("#<%=this.lblvalconsarea.ClientID%>").html(data2[1]);
                        $("#<%=this.lblvalstoried.ClientID%>").html(data2[2]);
                        $("#<%=this.lblvallandarea.ClientID%>").html(data2[3]);
                        $("#<%=this.lblvalsalablearea.ClientID%>").html(data2[4]);
                        $("#<%=this.lblvalhandoverdate.ClientID%>").html(data2[5]);
                        $("#<%=this.txtvallocation.ClientID%>").val(data2[6]);
                        //console.log(data2);


                        $('#container').highcharts({

                            chart: {
                                type: 'column',
                                styledMode: true
                            },
                            title: {
                                text: ''
                            },
                            subtitle: {
                                text: '',
                                style: {
                                    color: '#44994a',
                                    fontWeight: 'bold'
                                }

                            },
                            xAxis: {
                                type: 'category'
                            },
                            yAxis: [{
                                className: 'highcharts-color-0',
                                title: {
                                    text: 'Axis for TK(In Crore)'
                                }
                            }],
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
                                pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b><br/>'

                            },

                            "series": [
                                {
                                    "name": "",
                                    "colorByPoint": true,
                                    "data": [
                                        {
                                            "name": "B.Sales",
                                            "y": data1[1].bgdsales,
                                            "color": '#8A2BE2'

                                        },
                                        {
                                            "name": "B.Cost",
                                            "y": data1[1].bgdcost,
                                            "color": '#8A2BE2'

                                        },
                                        {
                                            "name": "B.Margin",
                                            "y": data1[1].bgdmar,
                                            "color": '#8A2BE2'

                                        },
                                        {
                                            "name": "A.Sales",
                                            "y": data1[1].salam,
                                            "color": '#808080'

                                        },
                                        {
                                            "name": "Collection",
                                            "y": data1[1].collam,
                                            "color": '#808080'

                                        },

                                        {
                                            "name": "Dues",
                                            "y": data1[1].cdueam,
                                            "color": '#808080'

                                        },
                                        {
                                            "name": "Over Dues",
                                            "y": data1[1].pdueam,
                                            "color": '#808080'

                                        },
                                        {
                                            "name": "C.Budget",
                                            "y": data1[1].cbgdcost,
                                            "color": '#993366'

                                        },
                                        {
                                            "name": "Progress",
                                            "y": data1[1].cprogress,
                                            "color": '#993366'

                                        },
                                        {
                                            "name": "Delay",
                                            "y": data1[1].cdelay,
                                            "color": '#993366'

                                        },
                                        {
                                            "name": "Actual Cost",
                                            "y": data1[1].invamt,
                                            "color": '#009999'

                                        },
                                        {
                                            "name": "Liabilities",
                                            "y": data1[1].liaam,
                                            "color": '#009999'

                                        },
                                        {
                                            "name": "R. InFlow",
                                            "y": data1[1].rinflow,
                                            "color": '#009999'

                                        },
                                        {
                                            "name": "R. Outflow",
                                            "y": data1[1].routflow,
                                            "color": '#009999'

                                        },
                                        {
                                            "name": "Block/Gen.",
                                            "y": data1[1].fblock,
                                            "color": '#5CB85C'

                                        }

                                    ]
                                }
                            ]
                        });



                    },
                    complete: function (data) {
                        $("#loader").hide();
                        $(".fm2").show();
                    },
                    failure: function (response) {
                        //  alert(response);
                        alert("f");
                    }
                });




            }
            catch (e) {
                alert(e);
            }

        }

    </script>
    <style>
        .btncssprop {
            background-color: #17A2B8;
            color: white;
            width: 125px;
            font-family: Calibri !important;
            font-size: 11px;
            height: 25px !important;
        }

        .lstbtndropdwn {
            margin-left: 10px;
            line-height: 23px;
        }

        .headertxtstl {
            font-size: 17px;
            font-family: Calibri !important;
            font-weight: bold;
            margin-left: 75px;
            margin-top: 20px;
        }

        .btnprsnlbtn {
            background-color: #5CB85C;
            font-size: 11px;
            width: 125px;
        }

        a:hover {
            background-color: #D8E7D1;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="card card-fluid">
                        <div class="card-header">
                            <div class="row fm1">
                                <fieldset class="">
                                    <div class="form-group">
                                        <div class="col-md-1 pading5px">

                                            <label class="lblTxt lblName" for="ddlUserName">Project Name:</label>

                                        </div>

                                        <div class="col-md-4">
                                            <div id="dp">
                                                <asp:DropDownList ID="ddlprojname" Width="375px" runat="server" CssClass="chzn-select form-control inputTxt">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" CssClass="btn btn-danger okBtn btnprsnlbtn">
                                    Project Information</asp:LinkButton>
                                        </div>
                                        <div class="col-md-2" style="color: white;">
                                            Opening:
                                    <asp:Label runat="server" ID="lblopndate" ClientIDMode="Static"></asp:Label>
                                        </div>

                                    </div>
                                </fieldset>
                            </div>
                        </div>


                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="metric-row metric-flush">
                                        <div class="col">
                                            <a id="btn101" target="_blank" class="metric metric-bordered align-items-center" role="button">
                                                <h2 class="metric-label">Budget</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn102" target="_blank" class="metric metric-bordered align-items-center" role="button">
                                                <h2 class="metric-label">Planning</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn103" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Cash Flow</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <div class="dropdown">
                                                <a class="metric metric-bordered align-items-center dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <h2 class="metric-label">Construction Graph</h2>

                                                </a>
                                                <div class="dropdown-menu">
                                                    <a id="btn104" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Bar Graph</a>
                                                    <br />
                                                    <a id="btn105" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Point Graph</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col">
                                            <a id="btn106" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Sub-Contractor Bill</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <div class="dropdown">
                                                <a class="metric metric-bordered align-items-center dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <h2 class="metric-label">Stock Report</h2>
                                                </a>
                                                <div class="dropdown-menu">
                                                    <a id="btn107" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Issue</a>
                                                    <br />
                                                    <a id="btn108" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Progress</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col">
                                            <div class="dropdown">
                                                <a class="metric metric-bordered align-items-center dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <h2 class="metric-label">Purchase</h2>

                                                </a>
                                                <div class="dropdown-menu">
                                                    <a id="btn109" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Summary</a>
                                                    <br />
                                                    <a id="btn110" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Day-Wise</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col" id="dSales">
                                            <div class="dropdown">
                                                <a class="metric metric-bordered align-items-center dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <h2 class="metric-label">Sale</h2>

                                                </a>
                                                <div class="dropdown-menu">
                                                    <a id="btn111" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Summary</a>
                                                    <br />
                                                    <a id="btn112" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Day-Wise</a>
                                                    <br />
                                                    <a id="btnmonsales" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Month Wise(Sales)</a>
                                                    <br />
                                                    <a id="btnmoncoll" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Month Wise(Collection)</a>
                                                    <br />
                                                    <a id="btncledger" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Client Ledger</a>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col" id="dDues">
                                            <a id="btn113" class="metric metric-bordered align-items-center" target="_blank" role="button">

                                                <h2 class="metric-label">Dues-OverDues</h2>
                                            </a>
                                        </div>
                                        <div class="col" id="dCMod">
                                            <div class="dropdown">
                                                <a class="metric metric-bordered align-items-center dropdown-toggle" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <h2 class="metric-label">Client Modification</h2>
                                                </a>
                                                <div class="dropdown-menu">
                                                    <a id="btn114" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Report 1</a>
                                                    <br />
                                                    <a id="btn115" class="dropdown-item lstbtndropdwn" target="_blank" href="#">Report 2</a>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="metric-row metric-flush">
                                        <div class="col">
                                            <a id="btn201" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Trial Balance</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn202" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Daily Transaction</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn203" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Influation Budget</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn204" class="metric metric-bordered align-items-center" href="#" role="button">
                                                <h2 class="metric-label">Documentation</h2>
                                            </a>
                                        </div>
                                        <div class="col" id="dReg">
                                            <a id="btn205" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Registration</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn206" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Balance Sheet</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn207" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Accounts Receivable</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn208" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Accounts Payable</h2>
                                            </a>
                                        </div>
                                        <div class="col">
                                            <a id="btn209" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Sup.Payable Tax & Vat</h2>
                                            </a>
                                        </div>
                                        <div class="col" id="dStd">
                                            <a id="btn210" class="metric metric-bordered align-items-center" target="_blank" role="button">
                                                <h2 class="metric-label">Customer STD</h2>
                                            </a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card card-fluid">

                        <div class="card-body" style="min-height: 250px;">
                            <div id="loader" style="display: none;"></div>
                            <div class="fm2" style="display: none;">
                                <div class="row">
                                    <fieldset class="scheduler-border fieldset_A">
                                    </fieldset>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="container" style="height: 360px; width: 1250px;"></div>
                                    </div>

                                    <%--<div class="col-md-3" style="padding-left: 50px;">
                                
                                      


                            </div>--%>
                                </div>

                            </div>
                        </div>
                        <div id="overtimeinfo" class="modal col-md-8 col-md-offset-2 animated zoomIn" role="dialog">
                            <div class="modal-dialog modal-sm">
                                <div class="modal-content  ">
                                    <div class="modal-header bg-primary">

                                        <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                        <h4 class="modal-title">
                                            <span class="glyphicon glyphicon-hand-right"></span>
                                            <asp:Label ID="lbmodalheading" runat="server">Project Information</asp:Label>
                                        </h4>
                                    </div>

                                    <div class="modal-body">
                                        <div class="row">
                                            <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Start Date:</asp:Label>
                                            <asp:Label ID="lblvalstartdate" runat="server" CssClass=" inputlblVal" Style="width: 150px;"></asp:Label>

                                            <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName">Const. Area:</asp:Label>
                                            <asp:Label ID="lblvalconsarea" runat="server" CssClass=" inputlblVal" Style="width: 150px;"></asp:Label>

                                            <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName">Storied:</asp:Label>
                                            <asp:Label ID="lblvalstoried" runat="server" CssClass=" inputlblVal" Style="width: 150px;"></asp:Label>

                                            <asp:Label ID="Label14" runat="server" CssClass="lblTxt lblName">Land (Katha):</asp:Label>
                                            <asp:Label ID="lblvallandarea" runat="server" CssClass=" inputlblVal" Style="width: 150px;"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Handover Date:</asp:Label>
                                            <asp:Label ID="lblvalhandoverdate" runat="server" CssClass=" inputlblVal" Style="width: 150px;"></asp:Label>

                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Saleable Area:</asp:Label>
                                            <asp:Label ID="lblvalsalablearea" runat="server" CssClass="inputlblVal" Style="width: 150px;"></asp:Label>


                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Location:</asp:Label>
                                            <asp:TextBox ID="txtvallocation" runat="server" CssClass="inputlblVal" Style="width: 150px;" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

