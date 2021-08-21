<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProjectDashBoard.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.ProjectDashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .tblh {
            background: #DFF0D8;
            height: 30px;
            text-align: center;
        }

        .th1 {
            min-width: 70px;
            text-align: center;
        }

        .th2 {
            min-width: 250px;
            text-align: center;
        }

        .th3 {
            min-width: 40px;
            text-align: center;
        }


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

        .bgdhead {
            text-align: center;
            background-color: #8A2BE2;
            color: white;
        }

        .salhead {
            text-align: center;
            background-color: #808080;
            color: white;
        }

        .consthead {
            text-align: center;
            background-color: #993366;
            color: white;
        }

        .acchead {
            text-align: center;
            background-color: #009999;
            color: white;
        }

        .fundhead {
            text-align: center;
            background-color: #5CB85C;
            color: white;
        }

        .panel {
    margin-bottom: 20px;
    background-color: #fff;
    border: 1px solid transparent;
    border-radius: 4px;
    -webkit-box-shadow: 0 1px 1px rgba(0,0,0,.05);
    box-shadow: 0 1px 1px rgba(0,0,0,.05);
}
.panel-default {
    border-color: #ddd;
}
.panel-default>.panel-heading {
    color: #333;
    background-color: #f5f5f5;
    border-color: #ddd;
}
.panel-heading {
    padding: 10px 15px;
    border-bottom: 1px solid transparent;
    border-top-left-radius: 3px;
    border-top-right-radius: 3px;
}

.panel-body {
    padding: 15px;
}

      .prjinfo .table td,  .ragstatus .table td {
            padding:0px !important;
            border: none !important;
        }

    </style>
     <script src="<%=this.ResolveUrl("~/Scripts/highchartwithmap.js")%>"></script>
    <script src="<%=this.ResolveUrl("~/Scripts/highchartexporting.js")%>"></script>
<script src="https://code.highcharts.com/modules/accessibility.js"></script>
    <script language="javascript" type="text/javascript">

        var comcod, projcode, data;
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            //alert("I m In");
            //$("#loader").hide();
            //GetData();      
            $('.chzn-select').chosen({ search_contains: true });
            showhide();
            tab();

        }
        function NewWindow(){
            document.forms[0].target="_blank";
        }
        function tab() {
            $("#topheada").click(function () {
                $("html, body").animate({ scrollTop: 0 }, "slow");
                return false;
            });


            $("#saltab").click(function () {

            });


        }

       
        function showhide() {
            $("#lnkOK").click(function () {
                GetData();

                event.preventDefault();
            });

            $("#btndetinfo").click(function () {
                event.preventDefault();
                alert("I m In");
            });

            //$("#bgdsalanchor").click(function () {
            //    $("#bgdsaldata").scrollIntoView();

            //});//attr("href", "#bgdsaldata");




        }

        function GetData() {
            try {

                comcod = <%=this.GetCompCode()%>;

                var temp = comcod.toString();
                var com = temp.slice(0, 1);


                $.ajax({
                    type: "POST",
                    url: "ProjectDashBoard.aspx/GetAllData",
                    contentType: "application/json; charset=utf-8",
                    data: '{comcodi:"' + comcod + '" , projcode: "' + $('#<%=this.ddlprojname.ClientID%>').val() + '"}',
                    dataType: "json",
                    beforeSend: function () {
                        // Show image container
                        $("#loader").show();
                    },
                    success: function (response) {
                        $('#bgddata2').empty();
                        $('#dataTable').empty();
                        $('#bgddata1').empty();
                        $('#saldata1').empty();
                        $('#accdata1').empty();
                        $('#funddata1').empty();
                        $('#bgddata3').empty();
                        $('#saldata2').empty();
                        $("#saldata3").empty();
                        var data = JSON.parse(response.d);
                        var data1 = data.TBLTAB101;
                        var data2 = data.TBLTAB102;
                        var data3 = data.TBLTAB2;
                        var data4 = data.TBLTAB21;
                        var data5 = data.TBLTAB31;
                        var data6 = data.TBLTAB32;
                        MakeDashboard(data.wrkproggress);
                        BindProjectInfo(data.projectinfo);
                        $("#txtprojname").val(data2[7]);
                        $("#txtstdt").val(data2[0]);
                        $("#txtconstarea").val(data2[1]);
                        $("#txtstoried").val(data2[2]);
                        $("#txtland").val(data2[3]);
                        $("#txtsalable").val(data2[4]);
                        $("#txthandover").val(data2[5]);
                        $("#txtloca").val(data2[6]);
                        //console.log(data2);

                        var bgdgrapgdatsal = [];

                        for (var i = 0; i < data1.length; i++) {

                            //bgdgrapgdat[i] = data1[i];


                            $('#dataTable').append(
                                '<tr class="grvRows"><th scope="row">' + data1[i].grpdesc + '</th><td align="right">' + data1[i].bgdsales + '</td><td align="right">' + data1[i].bgdcost +
                                '</td><td align="right">' + data1[i].bgdmar + '</td>' +


                                '<td align="right"><a target=_blank href=' + '../F_32_Mis/RatioAnaWithGraph?grp=01&comcod=' + comcod + '>' + data1[i].salam + '</a></td>' +
                                '<td align="right">' + data1[i].collam + '</td>' +
                                '<td align="right">' + data1[i].cdueam + '</td>' +
                                '<td align="right">' + data1[i].pdueam + '</td>' +
                                '<td align="right">' + data1[i].cbgdcost + '</td>' +
                                '<td align="right">' + data1[i].cprogress + '</td>' +
                                '<td align="right">' + data1[i].cdelay + '</td>' +
                                '<td align="right">' + data1[i].invamt + '</td>' +
                                '<td align="right">' + data1[i].liaam + '</td>' +
                                '<td align="right">' + data1[i].rinflow + '</td>' +
                                '<td align="right">' + data1[i].routflow + '</td>' +
                                '<td align="right">' + data1[i].fblock + '</td>' +
                                '</tr>'
                            );


                            $('#consdata1').append(
                                '<tr class="grvRows">' +
                                '<th scope="row">' + data1[i].grpdesc + '</th>' +
                                '<td align="right">' + data1[i].cbgdcost + '</td>' +
                                '<td align="right">' + data1[i].cprogress + '</td>' +
                                '<td align="right">' + data1[i].cdelay + '</td>' +
                                '</tr>'
                            );
                            $('#accdata1').append(
                                '<tr class="grvRows">' +
                                '<th scope="row">' + data1[i].grpdesc + '</th>' +
                                '<td align="right">' + data1[i].invamt + '</td>' +
                                '<td align="right">' + data1[i].liaam + '</td>' +
                                '<td align="right">' + data1[i].rinflow + '</td>' +
                                '<td align="right">' + data1[i].routflow + '</td>' +
                                '</tr>'
                            );
                            $('#funddata1').append(
                                '<tr class="grvRows">' +
                                '<th scope="row">' + data1[i].grpdesc + '</th>' +
                                '<td align="right">' + data1[i].fblock + '</td>' +
                                '</tr>'
                            );


                        }
                        $('#saldata1').append(
                            '<tr class="grvRows">' +
                            '<th scope="row">' + data1[1].grpdesc + '</th>' +
                            '<td align="right"><a href="#salcomdata">' + data1[1].salam + '</a></td>' +
                            '<td align="right"><a target=_blank href=' + '../F_23_CR/RptReceivedList02?Type=DuesCollect&prjcode=18' + ($('#<%=this.ddlprojname.ClientID%>').val()).substr(2, 10) + '>' + data1[1].collam + '</a></td>' +
                            '<td align="right"><a href="#salduedata">' + data1[1].cdueam + '</a></td>' +
                            '<td align="right"><a href="#salduedata">' + data1[1].pdueam + '</a></td>' +
                            '</tr>'
                        );

                        var sum = 0.00;
                        var d = new Date();
                        var strDate = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
                        var bgdtopsaldata = [];
                        //console.log(data3);
                        for (var i = 0; i < data3.length; i++) {
                            bgdgrapgdatsal[i] = [data3[i].udesc, (data3[i].tuamt) / 100000000]
                            bgdtopsaldata[i] = [data3[i].udesc, data3[i].tuamt]
                            sum = sum + data3[i].tuamt;

                            if (data3[i].sqty === 0) {

                            }
                            else {
                                $('#bgddata2').append(
                                    '<tr class="grvRows">' +
                                    '<th scope="row">' + (i + 1) + '</th>' +
                                    '<td >' + data3[i].udesc + '</td>' +
                                    '<td align="right">' + data3[i].munit + '</td>' +
                                    '<td align="right">' + data3[i].tqty + '</td>' +
                                    '<td align="right">' + data3[i].tuamt + '</td>' +
                                    '<td align="right">' + data3[i].sqty + '</td>' +
                                    '<td align="right">' + data3[i].susize + '</td>' +
                                    '<td align="right">' + data3[i].srate + '</td>' +
                                    '<td align="right"><a target=_blank href=' + '../F_22_Sal/LinkDuesColl?Type=ClientLedger&comcod=' + comcod + "&pactcode=" +
                                    data3[i].pactcode + "&usircode=" + data3[i].usircode + "&Date1=" + "" +
                                    '>' + data3[i].suamt + '</a></td>' +
                                    '<td align="right">' + data3[i].colamt + '</td>' +
                                    '<td>' + data3[i].schdate + '</td>' +
                                    '</tr>'
                                );
                            }


                        }
                        // console.log(bgdtopsaldata);
                        $('#bgddata2').append(
                            '<tr class="grvRows">' +
                            '<th scope="row"></th>' +
                            '<th >Total:</th>' +
                            '<td ></td>' +
                            '<td align="right"></td>' +
                            '<td align="right"><b>' + sum + '<b></td>' +
                            '<td align="right"></td>' +
                            '<td align="right"></td>' +
                            '<td align="right"></td>' +
                            '<td></td>' +

                            '<td align="right"></td>' +
                            '<td></td>' +
                            '</tr>'
                        );
                        //console.log(data4);
                        var actcode = "";
                        actcode = data4[0].actcode;
                        for (var j = 1; j < data4.length; j++) {
                            //console.log(actcode);
                            if (data4[j].actcode === actcode) {
                                actcode = data4[j].actcode;
                                data4[j].actdesc = "";
                            }

                            else {
                                actcode = data4[j].actcode;
                            }

                        }
                        var k = 0;
                        var r = 0;
                        var listtest = [];
                        var topdata = [];
                        var sumbgdlst = 0;
                        for (var i = 0; i < data4.length; i++) {
                            if ((data4[i].rescode).substr(2, 10) === "AAAAAAAAAA" && (data4[i].rescode).substr(0, 2) !== "21"
                                && (data4[i].rescode).substr(0, 2) !== "05"
                                && (data4[i].grp) !== "A") {
                                var dtdesc = ((data4[i].rescode === "06AAAAAAAAAA") ? "Construction Work" :
                                    (data4[i].rescode === "13AAAAAAAAAA") ? "Mechanical Work" : "Others");
                                if ((data4[i].rescode).substr(0, 2) !== "05" && (data4[i].rescode).substr(0, 2) !== "06"
                                    && (data4[i].rescode).substr(0, 2) !== "13") {
                                    sumbgdlst += data4[i].bgdam;
                                }
                                else {
                                    topdata[r] = [dtdesc, data4[i].bgdam];
                                }
                                r++;
                            }
                            if (data4[i].rescode === "05AAAAAAAAAA") {
                                $('#bgddata3').append(
                                    '<tr class="grvRows">' +
                                    '<th scope="row">' + (i + 1) + '</th>' +
                                    '<td align="right"><b>' + data4[i].resdesc + '</b></td>' +
                                    '<td></td>' +
                                    '<td align="right"><a target=_blank href=' + '../F_04_Bgd/linkBgdPrjAna?InputType=BgdMainRpt&AnaType=1&&prjcode=' + $('#<%=this.ddlprojname.ClientID%>').val() + '>' + data4[i].bgdam + '</a></td>' +
                                    '<td align="right"><b>' + data4[i].devcost + '</b></td>' +
                                    '<td align="right"><b>' + data4[i].salcost + '</b></td>' +
                                    '</tr>'
                                );

                            }
                            else if ((data4[i].rescode).substr(8, 4) === "AAAA") {

                                $('#bgddata3').append(
                                    '<tr class="grvRows">' +
                                    '<th scope="row">' + (i + 1) + '</th>' +
                                    '<td align="right"><b>' + data4[i].resdesc + '</b></td>' +
                                    '<td></td>' +
                                    '<td align="right"><b>' + data4[i].bgdam + '</b></td>' +
                                    '<td align="right"><b>' + data4[i].devcost + '</b></td>' +
                                    '<td align="right"><b>' + data4[i].salcost + '</b></td>' +
                                    '</tr>'
                                );

                            }
                            else {

                                $('#bgddata3').append(

                                    '<tr class="grvRows">' +
                                    '<th scope="row">' + (i + 1) + '</th>' +
                                    '<td >' + ('<b>' + data4[i].actdesc + '</b>' +
                                        (data4[i].resdesc.length > 0 ?
                                            (data4[i].actdesc.length > 0 ? '<br>' : "") +
                                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                            data4[i].resdesc : "")) + '</td>' +
                                    '<td>' + data4[i].resunit + '</td>' +
                                    '<td align="right">' + data4[i].bgdam + '</td>' +
                                    '<td align="right">' + data4[i].devcost + '</td>' +
                                    '<td align="right">' + data4[i].salcost + '</td>' +
                                    '</tr>'
                                );
                                k++;
                            }

                        }
                        topdata[(topdata.length)] = ["Others", sumbgdlst]
                        //console.log(sumbgdlst);
                        //console.log(r);
                        var lstttltopdata = [];
                        if (bgdtopsaldata.length > topdata.length) {
                            var i = 0
                            for (i = 0; i < topdata.length; i++) {
                                lstttltopdata[i] = [bgdtopsaldata[i][0], bgdtopsaldata[i][1], topdata[i][0], topdata[i][1]]
                            }
                            for (var k = i; k < bgdtopsaldata.length; k++) {
                                lstttltopdata[k] = [bgdtopsaldata[k][0], bgdtopsaldata[k][1], '', '0']

                            }
                        }

                        if (bgdtopsaldata.length < topdata.length) {
                            var i = 0
                            for (i = 0; i < bgdtopsaldata.length; i++) {
                                lstttltopdata[i] = [bgdtopsaldata[i][0], bgdtopsaldata[i][1], topdata[i][0], topdata[i][1]]
                            }
                            for (var k = i; k < topdata.length; k++) {
                                lstttltopdata[k] = ['', '0', topdata[k][0], topdata[k][1]]

                            }
                        }
                        if (bgdtopsaldata.length === topdata.length) {
                            for (var i = 0; i < bgdtopsaldata.length; i++) {
                                lstttltopdata[i] = [bgdtopsaldata[i][0], bgdtopsaldata[i][1], topdata[i][0], topdata[i][1]]
                            }

                        }
                        console.log(bgdtopsaldata);
                        console.log(topdata);
                        console.log(lstttltopdata);
                        var ttltopbgdsalsum = 0;
                        var ttltopbgdcostsum = 0;
                        for (var i = 0; i < lstttltopdata.length; i++) {
                            ttltopbgdsalsum += lstttltopdata[i][1];
                            ttltopbgdcostsum += lstttltopdata[i][3];
                            $('#bgddata1').append(
                                '<tr class="grvRows"><th class="th3">' + (i + 1) + '</th>' +
                                '<td>' + lstttltopdata[i][0] + '</td>' +
                                '<td align="right"><a href="#bgdsaldata">' + lstttltopdata[i][1] + '</a></td>' +
                                + '</td>' +
                                '<td>' + lstttltopdata[i][2] + '</td>' +
                                '<td align="right"><a href="#bgdcostdata">' + lstttltopdata[i][3] + '</a></td>' +
                                '</tr>'
                            );
                        }
                        $('#bgddata1').append(
                            '<tr class="grvRows"><th scope="row"></th>' +
                            '<td>' + "Total:" + '</td>' +
                            '<td align="right"><a href="#bgdsaldata"><b>' + ttltopbgdsalsum + '</b></a></td>' +
                            + '</td>' +
                            '<td>' + '</td>' +
                            '<td align="right"><a href="#bgdcostdata"><b>' + ttltopbgdcostsum + '</b></a></td>' +
                            '</tr>'

                        );
                        for (var i = 0; i < data5.length; i++) {
                            $('#saldata2').append(
                                '<tr class="grvRows"><th class="th3">' + (i + 1) + '</th>' +
                                '<td>' + data5[i].custname + '</td>' +
                                '<td>' + data5[i].udesc + '</td>' +
                                '<td align="right">' + data5[i].dueins + '</td>' +
                                '<td align="right">' + data5[i].predues + '</td>' +
                                '<td align="right">' + data5[i].curdues + '</td>' +
                                '<td align="right">' + data5[i].receivable + '</td>' +
                                '<td>' + data5[i].mrno + '</td>' +
                                '<td>' + data5[i].recdate + '</td>' +
                                '<td align="right">' + data5[i].recamt + '</td>' +
                                '<td align="right">' + data5[i].netdues + '</td>' +
                                '</tr>'
                            );
                        }
                        for (var i = 0; i < data6.length; i++) {
                            $("#saldata3").append(
                                '<tr class="grvRows"><th class="th3">' + (i + 1) + '</th>' +
                                '<td>' + data6[i].codedesc + '</td>' +
                                '<td align="right">' + data6[i].unumber + '</td>' +
                                '<td align="right">' + data6[i].usize + '</td>' +
                                '<td align="right">' + data6[i].rate + '</td>' +
                                '<td align="right"><a target=_blank href=' + '../F_23_CR/RptReceivedList02?Type=DuesCollect&prjcode=18' + ($('#<%=this.ddlprojname.ClientID%>').val()).substr(2, 10) + '>' + data6[i].amount + '</a></td>' +
                                '<td align="right">' + data6[i].percnt + '</td>' +
                                '</tr>'
                            );

                        }



                        // console.log(topdata);
                        Highcharts.chart('container', {
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: 'Budgeted'
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
                                    text: "TK(In Crore)"
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
                                pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f} In Crores</b><br/>'
                            },

                            series: [
                                {
                                    name: "TK",
                                    colorByPoint: true,
                                    data: [
                                        {
                                            name: "Sales",
                                            y: ttltopbgdsalsum / 10000000,
                                            drilldown: "Sales"
                                        },
                                        {
                                            name: "Cost",
                                            y: ttltopbgdcostsum / 10000000,
                                            drilldown: "Cost"
                                        }
                                    ]
                                }
                            ],
                            drilldown: {
                                series: [
                                    {
                                        name: "Sales",
                                        id: "Sales",
                                        data: bgdtopsaldata

                                    },
                                    {
                                        name: "Cost",
                                        id: "Cost",
                                        data: topdata
                                    },

                                ]
                            }
                        });



                        Highcharts.chart('salgraph', {
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: 'Sales'
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
                                    text: "TK(In Crore)"
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
                                pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f} In Crores</b><br/>'
                            },

                            series: [
                                {
                                    name: "TK",
                                    colorByPoint: true,
                                    data: [
                                        {
                                            name: "Completed",
                                            y: data1[1].salam,

                                        },
                                        {
                                            name: "Collection",
                                            y: data1[1].collam
                                        },
                                        {
                                            name: "Dues",
                                            y: data1[1].cdueam
                                        },
                                        {
                                            name: "OverDues",
                                            y: data1[1].pdueam
                                        }

                                    ]
                                }
                            ]
                        });



                    },
                    complete: function (data) {
                        $("#loader").hide();
                        $("#myTabContent").show();
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


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row fm1" id="sumdatabgd">
                        <div class="col-md-1">

                            <div class="form-group">
                                <label class="control-label" for="ddlUserName">Project Name:</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlprojname" runat="server" CssClass="custom-select  chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkOK" runat="server" ClientIDMode="Static" CssClass="btn btn-primary">OK</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1 offset-7">
                        <%--    <div class="form-group">--%>
                                <asp:LinkButton ID="btnlink" runat="server" ClientIDMode="Static" CssClass="btn btn-warning pull-right" OnClick="btnlink_Click" PostBackUrl="#" OnClientClick="NewWindow();" >Construction</asp:LinkButton>
                           <%-- </div>--%>
                        </div>

                        <div id="loader" style="display: none;"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <ul class="nav nav-tabs card-header-tabs">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#sumtab" id="sal">Summary</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show " data-toggle="tab" href="#purchasetab" id="bdg">Budgeted</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#saltab">Sale</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#constab">Construction</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#acctab">Account</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#fundtab">Fund</a>
                                    </li>
                                      <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#Dashboard">Dashboard</a>
                                    </li>

                                </ul>
                            </div>
                        </div>
                    </div>

                    <br />
                    <div id="myTabContent" class="tab-content" style="display: none;">

                        <div class="tab-pane fade active show" id="sumtab">


                            <div class="row">

                                <table id="sales" class="table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th colspan="3" class="bgdhead">Budgeted</th>
                                            <th colspan="4" class="salhead">Sales</th>
                                            <th colspan="3" class="consthead">Construction</th>
                                            <th colspan="4" class="acchead">Accounts</th>
                                            <th colspan="1" class="fundhead" style="">Fund</th>
                                        </tr>
                                        <tr class="tblh">
                                            <th class="th3"></th>
                                            <th class="th1">Sales</th>
                                            <th class="th1">Cost</th>
                                            <th class="th1">Margin</th>
                                            <th class="th1">Completed</th>
                                            <th class="th1">Collection</th>
                                            <th class="th1">Dues</th>
                                            <th class="th1">Overdues</th>
                                            <th class="th1">Budget</th>
                                            <th class="th1">Progress</th>
                                            <th class="th1">Delay</th>
                                            <th class="th1">Actual Cost</th>
                                            <th class="th1">Liablities</th>
                                            <th class="th1">R.Inflow</th>
                                            <th class="th1">R.Outflow</th>
                                            <th class="th1">Block/Generated</th>
                                        </tr>
                                    </thead>
                                    <tbody id="dataTable"></tbody>
                                </table>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="purchasetab">
                            <div class="row">
                                <asp:HyperLink runat="server" Target="_blank" ID="lblpurchase" Style="font-size: 16px; color: #70737c; font-weight: bold;" ClientIDMode="Static">Budget Summary</asp:HyperLink>

                            </div>
                            <div class="row">
                                <div class="col-md-5">

                                    <table id="bgd1" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th3">#</th>
                                                <th class="th1" colspan="2">Sales</th>
                                                <th class="th1" colspan="2">Cost</th>

                                        </thead>
                                        <tbody id="bgddata1"></tbody>
                                    </table>
                                </div>
                                <div class="col-md-7">
                                    <div class="row">
                                        <div id="container" style="width: 600px; height: 260px;"></div>


                                    </div>

                                </div>
                            </div>
                            <br />
                            <div id="bgdsaldata">
                                <br />
                                <br />
                                <br />
                                <div class="row">
                                    <a runat="server" href="#sumdatabgd" style="font-size: 16px; color: #70737c; font-weight: bold;" clientidmode="Static">Sales</a>

                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <table id="bgd2" class="table-striped table-hover table-bordered">
                                            <thead>
                                                <tr class="tblh">
                                                    <th class="th3">#</th>

                                                    <th>Description Of Item</th>
                                                    <th class="th1">Unit</th>
                                                    <th class="th1">Budgeted Qty</th>
                                                    <th class="th1">Budgeted Amt</th>
                                                    <th class="th1">Sold Qty</th>
                                                    <th class="th1">Unit Size</th>
                                                    <th class="th1">Rate</th>
                                                    <th class="th1">Sold Amt</th>
                                                    <th class="th1">Collection Amt</th>
                                                    <th class="th1">Sold Date</th>
                                            </thead>
                                            <tbody id="bgddata2"></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div id="bgdcostdata">
                                <br />
                                <br />
                                <br />
                                <div class="row">
                                    <a runat="server" href="#sumdatabgd" style="font-size: 16px; color: #70737c; font-weight: bold;" clientidmode="Static">Cost</a>

                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <table id="bgd3" class="table-striped table-hover table-bordered">
                                            <thead>
                                                <tr class="tblh">
                                                    <th class="th3">#</th>
                                                    <th class="th2">Description</th>
                                                    <th class="th1">Unit</th>
                                                    <th class="th1">Budgeted</th>
                                                    <th class="th1">Construction Cost per SFT</th>
                                                    <th class="th1">Saleable Cost per SFT</th>
                                            </thead>
                                            <tbody id="bgddata3"></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="saltab">
                            <div class="row">
                                <asp:HyperLink runat="server" ID="HyperLink1" Style="font-size: 16px; color: #70737c; font-weight: bold;" ClientIDMode="Static">Sales Summary</asp:HyperLink>

                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <table id="sal1" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th3"></th>
                                                <th class="th1">Completed</th>
                                                <th class="th1">Collection</th>
                                                <th class="th1">Dues</th>
                                                <th class="th1">Over Dues</th>
                                        </thead>
                                        <tbody id="saldata1"></tbody>
                                    </table>
                                </div>
                                <div class="col-md-7">
                                    <div id="salgraph" style="width: 600px; height: 260px;"></div>

                                </div>
                            </div>


                            <div id="salcomdata">
                                <br />
                                <br />
                                <br />
                                <div class="row">
                                    <a runat="server" href="#sumdatabgd" style="font-size: 16px; color: #70737c; font-weight: bold;" clientidmode="Static">DUES COLLECTION STATMENT REPORT</a>

                                </div>


                                <div class="row">
                                    <div class="table table-responsive">
                                        <table id="sal3" class="table-striped table-hover table-bordered">
                                            <thead>
                                                <tr class="tblh">
                                                    <th class="th3">#</th>
                                                    <th class="th1">Description</th>
                                                    <th class="th1">Unit</th>
                                                    <th class="th1">Total Unit Size</th>
                                                    <th class="th1">Rate</th>
                                                    <th class="th1">Amount</th>
                                                    <th class="th1">Percentage</th>
                                            </thead>
                                            <tbody id="saldata3"></tbody>
                                        </table>

                                    </div>

                                </div>

                            </div>

                            <div id="salduedata">
                                <br />
                                <br />
                                <br />
                                <div class="row">
                                    <a runat="server" href="#sumdatabgd" style="font-size: 16px; color: #70737c; font-weight: bold;" clientidmode="Static">Dues And Over Dues</a>

                                </div>

                                <div class="row">
                                    <div class="col-md-8">
                                        <table id="sal2" class="table-striped table-hover table-bordered">
                                            <thead>
                                                <tr class="tblh">
                                                    <th class="th3">#</th>
                                                    <th class="th2">Customer Name</th>
                                                    <th class="th1">Unit Desc.</th>
                                                    <th class="th1">Dues Ins.</th>
                                                    <th class="th1">Prev Dues</th>
                                                    <th class="th1">Cur. Dues</th>
                                                    <th class="th1">Receivable</th>
                                                    <th class="th1">Mr No</th>
                                                    <th class="th1">Mr Date</th>
                                                    <th class="th1">Received</th>
                                                    <th class="th1">Net Dues</th>
                                            </thead>
                                            <tbody id="saldata2"></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="constab">
                            <div class="row">

                                <table id="cons1" class="table-striped table-hover table-bordered">
                                    <thead>
                                        <tr class="tblh">
                                            <th class="th3"></th>
                                            <th class="th1">Budget</th>
                                            <th class="th1">Progress</th>
                                            <th class="th1">Delay</th>
                                    </thead>
                                    <tbody id="consdata1"></tbody>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="acctab">
                            <div class="row">

                                <table id="acc1" class="table-striped table-hover table-bordered">
                                    <thead>
                                        <tr class="tblh">
                                            <th class="th3"></th>
                                            <th class="th1">Actual Cost</th>
                                            <th class="th1">Liabilities</th>
                                            <th class="th1">R. Inflow</th>
                                            <th class="th1">R. Outflow</th>
                                    </thead>
                                    <tbody id="accdata1"></tbody>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="fundtab">
                            <div class="row">

                                <table id="fund1" class="table-striped table-hover table-bordered">
                                    <thead>
                                        <tr class="tblh">
                                            <th class="th3"></th>
                                            <th class="th1">Block/Generated</th>

                                    </thead>
                                    <tbody id="funddata1"></tbody>
                                </table>
                            </div>
                        </div>
                         <div class="tab-pane fade" id="Dashboard">
                         <div class="row">
                             <div class="col-md-12">
                                 <div class="panel panel-default">
                                          <div class="panel-heading"><label id="ProjName"></label></div>
                                          <div class="panel-body prjinfo">
                                              <table class="table table-condensed">
                                                  <tr>
                                                    <td>Location</td>
                                                      <td><label id="Location"></label></td>
                                                       <td>Project Type</td>
                                                      <td><label id="PrjType"></label></td>
                                                  </tr>
                                                   <tr>
                                                    <td>Project Code</td>
                                                      <td><label id="PrjCode"></label></td>
                                                       <td>Project Name</td>
                                                      <td><label id="prjname"></label></td>
                                                  </tr>
                                                   <tr>
                                                    <td>Total Building</td>
                                                      <td><label id="TotalBuilding"></label></td>
                                                       <td>Block</td>
                                                      <td><label id="Block"></label></td>
                                                  </tr>
                                                   <tr>
                                                    <td rowspan="2">Other</td>
                                                      <td rowspan="2"><label id="OtheBuilding"></label></td>
                                                       <td>Project Land Area</td>
                                                      <td><label id="LandArea"></label></td>
                                                  </tr>
                                                  <tr>
                                                   
                                                       <td>Project Duration</td>
                                                      <td><label id="prjduration"></label></td>
                                                  </tr>
                                              </table>
                                             
                                              </div>
                                              </div>
                             </div>
                               <div class="col-md-6">
                                          <div class="panel panel-default">
                                          <div class="panel-heading">RAG Status</div>
                                          <div class="panel-body ragstatus">
                                              <div class="row">
                                                  <div class="col-md-3">

                                                  </div>
                                              <div class="col-md-6 ">
                                              <table class="table table-condensed">
                                                  <tr>
                                                      <td>Schedule</td>
                                                      <td><span style="color:red; font-weight:bold">RED</span></td>
                                                  </tr>
                                                   <tr>
                                                      <td>Supply Chain</td>
                                                      <td><span style="color:greenyellow; font-weight:bold">AMBER</span></td>
                                                  </tr>
                                                   <tr>
                                                      <td>Sales</td>
                                                      <td><span style="color:red; font-weight:bold">RED</span></td>
                                                  </tr>
                                                   <tr>
                                                      <td>Health & Safety</td>
                                                      <td><span style="color:green; font-weight:bold">GREEN</span></td>
                                                  </tr>
                                              </table>
                                           </div>
                                              </div>
                                              </div>
                                              </div>
                                    </div>
                             <div class="col-md-6">
                                     <div class="panel panel-default">
                                          <div class="panel-heading">Sales Meter</div>
                                          <div class="panel-body">
                                              <div class="row">
                                                  <div class="col-md-6 ">
                                                      <h3>Apartments</h3>
                                                      <div class="form-group">                                                        
                                                          <label class="col-md-12" id="totalunit"></label>
                                                            <label class="col-md-12" id="lounit"></label>
                                                            <label class="col-md-12" id="sunit"></label>
                                                            <label class="col-md-12" id="usunit"></label>

                                                      </div>                                            

                                                  </div>
                                                    <div class="col-md-6">
                                                      <h3>Shop</h3>
                                                        <div class="form-group">                                                        
                                                          <label class="col-md-12" id="totalshopunit"></label>
                                                            <label class="col-md-12" id="shoplounit"></label>
                                                            <label class="col-md-12" id="shopsunit"></label>
                                                            <label class="col-md-12" id="shopusunit"></label>

                                                      </div>  
                                              </div>
                                              </div>
                                            
                                              </div>
                                              </div>
                                    </div>
                         
                              
   <div class="col-md-12">
                                <div class="panel panel-default">
  <div class="panel-heading">Construction Progress</div>
  <div class="panel-body">
    
            
    <div class="row">

          <div class="col-md-3">

                <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkMob" runat="server"  CssClass="btn btn-warning" PostBackUrl="#" OnClientClick="NewWindow();" onclick="lnkMob_Click" >Details</asp:LinkButton>
                            </div>
                        </div>
             
               <div id="progresssub" style="max-width:350px; height:300px; margin:0px;" ></div>  
                
          </div>
            <div class="col-md-3">

                  <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkSub" runat="server" ClientIDMode="Static" CssClass="btn btn-warning" OnClick="lnkSub_Click" PostBackUrl="#" OnClientClick="NewWindow();">Details</asp:LinkButton>
                            </div>
                        </div>
              
                 <div id="progresssup" style="max-width:350px; height:300px; margin:0px;"></div> 
                  
          </div>
        
         <div class="col-md-3">

               <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbkSup" runat="server" ClientIDMode="Static" CssClass="btn btn-warning" OnClick="lbkSup_Click" PostBackUrl="#" OnClientClick="NewWindow();" >Details</asp:LinkButton>
                            </div>
                        </div>
             
               <div id="progressfinish" style="max-width:350px; height:300px; margin:0px;" ></div>  
                
          </div>
          <div class="col-md-3">

                <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkOverall" runat="server" ClientIDMode="Static" CssClass="btn btn-warning" OnClick="lnkOverall_Click" PostBackUrl="#" OnClientClick="NewWindow();">Details</asp:LinkButton>
                            </div>
                        </div>
               
                 <div id="progress" style="max-width:350px; height:300px; margin:0px;" ></div> 
                  
          </div>
       


        
  </div>
</div>
                                    </div>
                             
                            </div>
                             </div>
                        </div>

                    </div>


                    <a id="topheada" href="#">Go TOP
                            
                    </a>



                </div>




            </div>

            <script>

                function BindProjectInfo(prjinfo){
                    console.log(prjinfo);
                    if(prjinfo.length==0)
                        return;

                    $("#ProjName").html(prjinfo[0].prjname);
                    $("#prjname").html(prjinfo[0].prjname);
                    $("#Location").html(prjinfo[0].location);
                    $("#PrjType").html(prjinfo[0].prjtype);
                    $("#prjduration").html(prjinfo[0].prjduration);
                    $("#LandArea").html(prjinfo[0].landareasft+" SFT");
                    $("#PrjCode").html(prjinfo[0].prjcode);
                    $("#TotalBuilding").html(prjinfo[0].numofbuilding);
                    $("#OtheBuilding").html(prjinfo[0].other);
                    
                    $("#totalunit").html("Total Units: "+prjinfo[0].aprtmntunit+" Units");
                    $("#lounit").html("LO Units: "+prjinfo[0].aprtmntlo+" Units");
                    $("#sunit").html("Sold Units: "+prjinfo[0].aprtmntsqty+" Units");
                    $("#usunit").html("Unsold Units: "+prjinfo[0].aprtmntusqty+" Units");

                    $("#totalshopunit").html("Total Units: "+prjinfo[0].shopunit+" Units");
                    $("#shoplounit").html("LO Units: "+prjinfo[0].shoplo+" Units");
                    $("#shopsunit").html("Sold Units: "+prjinfo[0].shopsqty+" Units");
                    $("#shopusunit").html("Unsold Units: "+prjinfo[0].shopusqty+" Units");
                }
                function MakeDashboard(graphdata) {
                    console.log(graphdata);
                    //overl all
                    //Highcharts.chart('progress', {
                    //    chart: {
                    //        type: 'pie'
                    //    },
                    //    title: {
                    //        text: graphdata[3].wrkdesc
                    //    },
                    //    subtitle: {
                    //        text: '<div>'+graphdata[3].parcnt.toFixed(2)+'%</div> of Total',
                    //        align: "center",
                    //        verticalAlign: "middle",
                    //        style: {
                    //            "fontSize": "15px",
                    //            "textAlign": "center"
                    //        },
                    //        x: 0,
                    //        y: -2,
                    //        useHTML: true
                    //    },
                    //    plotOptions: {
                    //        pie: {
                    //            shadow: false,
                    //            center: ["50%", "50%"],
                    //            dataLabels: {
                    //                enabled: false
                    //            },
                    //            states: {
                    //                hover: {
                    //                    enabled: false
                    //                }
                    //            },
                    //            size: "45%",
                    //            innerSize: "95%",
                    //            borderColor: null,
                    //            borderWidth: 8
                    //        }

                    //    },
                    //    tooltip: {
                    //        valueSuffix: '%'
                    //    },
                    //    series: [{
                    //        innerSize: '80%',
                    //        data: [{
                    //            name: 'Complete',
                    //            y: graphdata[3].parcnt,
                    //            color: '#e7eaeb'
                    //        }, {
                    //            name: 'In-complete',
                    //            y: 100-graphdata[3].parcnt,
                    //        }]
                    //    }],
                    //});

                    // sub struction
                    Highcharts.chart('progresssub', {
                        title: {
                            text: graphdata[0].wrkdesc
                        },
                        subtitle: {
                            text: "<div style='font-size: 40px'>"+graphdata[0].parcnt.toFixed(2)+"%</div> of Total",
                            align: "center",
                            verticalAlign: "middle",
                            style: {
                                "textAlign": "center"
                            },
                            x: 0,
                            y: -2,
                            useHTML: true
                        },
                        series: [{
                            type: 'pie',
                            enableMouseTracking: false,
                            innerSize: '80%',
                            dataLabels: {
                                enabled: false
                            },
                            data: [{
                                y: graphdata[0].parcnt
                            }, {
                                y: 100-graphdata[0].parcnt,
                                color: '#97e6c5'
                            }]
                        }]
                    });
          

                    Highcharts.chart('progresssup', {
                        title: {
                            text: graphdata[1].wrkdesc
                        },
                        subtitle: {
                            text: "<div style='font-size: 40px'>"+graphdata[1].parcnt.toFixed(2)+"%</div> of Total",
                            align: "center",
                            verticalAlign: "middle",
                            style: {
                                "textAlign": "center"
                            },
                            x: 0,
                            y: -2,
                            useHTML: true
                        },
                        series: [{
                            type: 'pie',
                            enableMouseTracking: false,
                            innerSize: '80%',
                            dataLabels: {
                                enabled: false
                            },
                            data: [{
                                y: graphdata[1].parcnt
                            }, {
                                y: 100-graphdata[1].parcnt,
                                color: '#e3e3e3'
                            }]
                        }]
                    });

                    Highcharts.chart('progress', {
                        title: {
                            text: graphdata[3].wrkdesc
                        },
                        subtitle: {
                            text: "<div style='font-size: 40px'>"+graphdata[3].parcnt.toFixed(2)+"%</div> of Total",
                            align: "center",
                            verticalAlign: "middle",
                            style: {
                                "textAlign": "center"
                            },
                            x: 0,
                            y: -2,
                            useHTML: true
                        },
                        series: [{
                            type: 'pie',
                            enableMouseTracking: false,
                            innerSize: '80%',
                            dataLabels: {
                                enabled: false
                            },
                            data: [{
                                y: graphdata[3].parcnt
                            }, {
                                y: 100-graphdata[3].parcnt,
                                color: '#db7dbd'
                            }]
                        }]
                    });
                    Highcharts.chart('progressfinish', {
                        title: {
                            text: graphdata[2].wrkdesc
                        },
                        subtitle: {
                            text: "<div style='font-size: 40px'>"+graphdata[2].parcnt.toFixed(2)+"%</div> of Total",
                            align: "center",
                            verticalAlign: "middle",
                            style: {
                                "textAlign": "center"
                            },
                            x: 0,
                            y: -2,
                            useHTML: true
                        },
                        series: [{
                            type: 'pie',
                            enableMouseTracking: false,
                            innerSize: '80%',
                            dataLabels: {
                                enabled: false
                            },
                            data: [{
                                y: graphdata[2].parcnt
                            }, {
                                y: 100-graphdata[2].parcnt,
                                color: '#f2aa5c'
                            }]
                        }]
                    });
                }
            </script>
            <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


