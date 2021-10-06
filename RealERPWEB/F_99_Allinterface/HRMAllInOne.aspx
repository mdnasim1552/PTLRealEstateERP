<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HRMAllInOne.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.HRMAllInOne" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .footer {
    background-color: #2e3639;
    /*position: relative;*/
    z-index: 1;
}

    .footer .splitter {
        background-color: #ac0;
        background-image: linear-gradient(45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
        background-size: 50px 50px;
        box-shadow: 1px 1px 8px gray;
        height: 10px;
    }

    .splitterh {
        background-color: #ac0;
        background-image: linear-gradient(45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
        background-size: 50px 50px;
        box-shadow: 1px 1px 4px gray;
        height: 5px;
    }


    .footer .bar {
        background-color: #1e2629;
        padding: 11px 0 0;
    }

.quickLink h4 {
    color: #ffffff;
}

ul.Menulinks {
    margin: 0;
    padding: 0;
}

    ul.Menulinks li {
        list-style: none;
    }

        ul.Menulinks li a {
            display: block;
            color: #ffffff;
            padding: 2px 5px 2px 0;
        }

.Menulinks li a:hover {
    color: #ed4e6e;
    text-decoration: none;
}

.Menulinks .glyphicon {
    padding-right: 3px;
}

.quickLink p {
    margin: 0;
}

.quickLink a:hover {
    color: #0989c6;
}

.clTestimonial img {
    margin: 0 auto;
}

.clTestimonialTxt {
    text-align: right;
    color: #b3b9bf;
}

.clTestimonial h5 {
    color: #0989c6;
    font-size: 14px;
    font-weight: bold;
}

.clTestimonial h6 {
    font-size: 18px;
    color: #fff;
}

.clTestimonial a {
    color: #ffffff;
}

.quickLink {
    color: #fff;
}

.MainMenu a {
    background: #f9f9f9;
    color: #000;
}

    .MainMenu a:hover {
        color: #fff;
        /*//background: #336dbb;*/
    }

.quickLink fieldset {
    color: #fff;
    font-size: 14px;
    line-height: 18px;
}

.copyright p {
    color: #ffffff;
}

.nAsitModel p {
    font-size: 18px;
    line-height: 22px;
    color: #000;
}

.nAsitModel a span.serialNumb {
    border-right: 1px solid #ccc;
    float: left;
    margin-right: 5px;
    padding: 0 5px;
    text-align: left;
}

.tblh {
      background: #DFF0D8;
     height: 30px; 
     text-align: center
}
.th1 {
    width: 100px; 
    text-align: center
}
.th2 {
    width: 70px;
    text-align: center
}
.th3 {
     width: 40px;
     text-align: center
}

    </style>

    <script src="../Scripts/highchart2.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>

    <script language="javascript" type="text/javascript">

        var comcod, Date1, Date2;
        $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
          
             GetData();
        }



        function GetData() {
            try {
                comcod = <%=this.GetCompCode()%>;
                Date1 = $('#txtDateFrom').val();
                Date2 = $('#txtDateto').val();

                $("#hlnkatt").attr("href","F_81_Hrm/F_99_MgtAct/LinkLateElLeaveAAbs.aspx?Type=LELLAndAbsent&comcod=" +
                                  comcod + "&Date=" + Date2); 
                $.ajax({
                    type: "POST",
                    url: "HRMAllInOne.aspx/GetAllData",
                    data: '{date1: "' + $('#<%=this.txtDateFrom.ClientID%>').val() + '" , date2: "' + $('#<%=this.txtDateto.ClientID%>').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        //console.log(JSON.parse(response.d));
                        var data = response.d;
                        
                        //console.log(data['account']);
                        ExecuteGraph(data);
                    },
                    failure: function (response) {
                        //  alert(response);
                        alert("f");
                    }
                });
            }
            catch(e) {
                alert(e);
            }
           
        }

        function ExecuteGraph(bgd) {
            try {

                Highcharts.setOptions({
                    lang: {
                        decimalPoint: '.',
                        thousandsSep: ' '
                    }
                });

                var bgddata = JSON.parse(bgd);
                console.log(bgddata);

                //Leave Legend
                var leavedata = bgddata['leave'];
               
                var armainhead = [];
                for (var i = 0; i < leavedata.length; i++) {
                    armainhead[i] = leavedata[i]["head"];
                }

                //console.log(leavedata);
                //var ar1 = '';
                //var ar2 = '';
                var row = '';
            <%-- comcod = <%=this.GetCompCode()%>;
            Date1 = $('#txtDateFrom').val();
            Date2 = $('#txtDateto').val();--%>

                $.each(leavedata,
                    function(i, item) {
                        //ar1 = (item.grp == "A")? '<a target=_blank href=' + encodeURI('F_17_Acc/LinkRptReciptPayment.aspx?Type=receipt&comcod=' + comcod + '&Date1=' +Date1 + '&Date2=' + Date2) + '>'
                        //    : item.grp == "B"? '<a target=_blank href=' + encodeURI('F_17_Acc/LinkRptReciptPayment.aspx?Type=payment&comcod=' + comcod + '&Date1=' +Date1 + '&Date2=' + Date2) + '>'
                        //    : '';
                        //ar2 = (item.grp == "A") || (item.grp == "B") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" +  item.head  + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#leave tbody").html(row);
                    });


                //Sales Legend
                var memberdata = bgddata['member'];
                
                var saleshead = [];
                for (var i = 0; i < memberdata.length; i++) {
                    saleshead[i] = memberdata[i]["head"];
                }

                //var ar1 = '';
                //var ar2 = '';
                var row = '';
                $.each(memberdata,
                    function(i, item) {
                        //ar1 = (item.gcod == "01001")
                        //    ? '<a target=_blank href=' + encodeURI('F_22_Sal/RptSaleMonYear.aspx') + '>'
                        //    : item.gcod == "01002"
                        //    ? '<a target=_blank href=' + encodeURI('F_22_Sal/RptCollMonYear.aspx') + '>'
                        //    : '';
                        //ar2 = (item.gcod == "01001") || (item.gcod == "01002") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + item.head +"</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                            "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#member tbody").html(row);
                    });

                //Purchase Legend
                var attendata = bgddata['attendance'];
                console.log(attendata);
                var purchasehead = [];
                for (var i = 0; i < attendata.length; i++) {
                    purchasehead[i] = attendata[i]["head"];
                }


                //var ar1 = '';
                //var ar2 = '';
                var row = '';
                $.each(attendata,
                    function(i, item) {
                        //ar1 = (item.gcod == "01001")
                        //    ? '<a target=_blank href=' + encodeURI('F_14_Pro/PurSumMatWise.aspx') + '>'
                        //    : '';
                        //ar2 = (item.gcod == "01001") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                            "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#atten tbody").html(row);
                    });


                //Salary Legend
                var salarydata = bgddata['salary'];
                var conshead = [];
                for (var i = 0; i < salarydata.length; i++) {
                    conshead[i] = salarydata[i]["head"];
                }
                //var ar1 = '';
                //var ar2 = '';
                var row = '';
                $.each(salarydata,
                    function(i, item) {
                        //ar1 = (item.gcod == "01001")
                        //    ? '<a target=_blank href=' + encodeURI('F_32_Mis/RptConstruProgressSum.aspx') + '>'
                        //    : item.gcod == "01002"
                        //    ? '<a target=_blank href=' +
                        //    encodeURI('F_09_PImp/RptImpExeStatus.aspx?Type=DayWiseExecution&prjcode=&Date1=&Date2=') +
                        //    '>'
                        //    : '';
                        //ar2 = (item.gcod == "01001") || (item.gcod == "01002") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                            "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#salary tbody").html(row);
                    });


                ////Confirmation Legend
                var confirmdata = bgddata['confirm'];
                var bankhead = [];
                for (var i = 0; i < confirmdata.length; i++) {
                    bankhead[i] = confirmdata[i]["head"];
                }

                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(confirmdata,
                    function(i, item) {
                        ar1 = (item.gcod == "01002")
                            ? '<a target=_blank href=' + encodeURI('/F_81_Hrm/F_92_Mgt/HREmpConfirmation.aspx') + '>'
                            : item.gcod == "01003"
                            ? '<a target=_blank href=' +
                            encodeURI('/F_81_Hrm/F_92_Mgt/HREmpConfirmation.aspx') +
                            '>'
                            : '';
                        ar2 = (item.gcod == "01002") || (item.gcod == "01003") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>"+ar1+item.head+ar2+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#confirm tbody").html(row);
                    });

                //Loan Legend
                var loandata = bgddata['loan'];
                var stockhead = [];
                for (var i = 0; i < loandata.length; i++) {
                    stockhead[i] = loandata[i]["head"];
                }

                var row = '';
                $.each(loandata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#loan tbody").html(row);
                    });



                ////PF Account
                var pffunddata = bgddata['pffund'];
                var dueshead = [];
                for (var i = 0; i < pffunddata.length; i++) {
                    dueshead[i] = pffunddata[i]["head"];
                }

                var row = '';
                $.each(pffunddata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#pffund tbody").html(row);
                    });



                ////Separation Legend
                var sepdata = bgddata['separation'];
                var penbilhead = [];
                for (var i = 0; i < sepdata.length; i++) {
                    penbilhead[i] = sepdata[i]["head"];
                }
                var row = '';
                $.each(sepdata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#sep tbody").html(row);
                    });


                //////Employee Joining
                var empjoindata = bgddata['empjoining'];
                var dueshead = [];
                for (var i = 0; i < empjoindata.length; i++) {
                    dueshead[i] = empjoindata[i]["head"];
                }

                var row = '';
                $.each(empjoindata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#empjoin tbody").html(row);
                    });






                ////Future Fund
                //var ffunddata = bgddata['ffund'];
                //var ffundhead = [];
                //for (var i = 0; i < ffunddata.length; i++) {
                //    ffundhead[i] = ffunddata[i]["head"];
                //}

                

                //var row = '';
                //$.each(ffunddata,
                //    function(i, item) {
                        
                //        row += "<tr>";
                //        row += "<td>"+item.head+ "</td>";
                //        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                //        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                //        row += "</tr>";
                //        $("#futuretable tbody").html(row);
                //    });

                ////Construction Progress
                //var conprodata = bgddata['conprogress'];
                //var conprohead = [];
                //for (var i = 0; i < conprodata.length; i++) {
                //    conprohead[i] = conprodata[i]["head"];
                //}
                
                //var row = '';
                //$.each(conprodata,
                //    function(i, item) {
                        
                //        row += "<tr>";
                //        row += "<td>"+item.head+ "</td>";
                //        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                //        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                //        row += "</tr>";
                //        $("#prjrpttable tbody").html(row);
                //    });
                

                ////Future Cost 
                //var fcostdata = bgddata['fcost'];
                //var fcosthead = [];
                //for (var i = 0; i < fcostdata.length; i++) {
                //    fcosthead[i] = fcostdata[i]["head"];
                //}
                //var row = '';
                //$.each(fcostdata,
                //    function(i, item) {
                        
                //        row += "<tr>";
                //        row += "<td>"+item.head+ "</td>";
                //        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                //        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                //        row += "</tr>";
                //        $("#costtable tbody").html(row);
                //    });
                
                ////Future Fund Vs Cost 
                //var fvscostdata = bgddata['fundcost'];
                //var fvscosthead = [];
                //for (var i = 0; i < fvscostdata.length; i++) {
                //    fvscosthead[i] = fvscostdata[i]["head"];
                //}
                //var row = '';
                //$.each(fvscostdata,
                //    function(i, item) {
                        
                //        row += "<tr>";
                //        row += "<td>"+item.head+ "</td>";
                //        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                //        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                //        row += "</tr>";
                //        $("#funvscosttable tbody").html(row);
                //    });
                
                //Leave
                Highcharts.chart('leavedt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, armainhead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in leavedata) {
                                    if (leavedata.hasOwnProperty(key)) {
                                        data.push([
                                            leavedata[key].head,
                                            leavedata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                //Member
                Highcharts.chart('memberdt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, saleshead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in memberdata) {
                                    if (memberdata.hasOwnProperty(key)) {
                                        data.push([
                                            memberdata[key].head,
                                            memberdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });
                //Attendance
                Highcharts.chart('attendt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, purchasehead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in attendata) {
                                    if (attendata.hasOwnProperty(key)) {
                                        data.push([
                                            attendata[key].head,
                                            attendata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                ////Salary
                Highcharts.chart('salarydt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, conshead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in salarydata) {
                                    if (salarydata.hasOwnProperty(key)) {
                                        data.push([
                                            salarydata[key].head,
                                            salarydata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                ////Confirmation
                Highcharts.chart('confirmdt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, bankhead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in confirmdata) {
                                    if (confirmdata.hasOwnProperty(key)) {
                                        data.push([
                                            confirmdata[key].head,
                                            confirmdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                ////Loan
                Highcharts.chart('loandt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, stockhead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in loandata) {
                                    if (loandata.hasOwnProperty(key)) {
                                        data.push([
                                            loandata[key].head,
                                            loandata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                ////Dues Over Dues
                //Highcharts.chart('duesdt',
                //{
                //    chart: {
                //        type: 'bar'
                //    },
                //    title: {
                //        text: ''
                //    },
                //    subtitle: {
                //        text: '',
                //        style: {
                //            color: '#44994a',
                //            fontWeight: 'bold'
                //        }
                //    },


                //    xAxis: {
                //        type: 'category',
                //        labels:
                //        {
                //            formatter: function() {
                //                if ($.inArray(this.value, dueshead) !== -1) {
                //                    return '<span style="fill: maroon;">' + this.value + '</span>';
                //                } else {
                //                    return this.value;
                //                }
                //            },
                //            style: {
                //                color: '#000',

                //            }
                //        }
                //    },
                //    yAxis: {
                //        title: {
                //            text: ''
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
                //                format: '{point.y:.2f}'
                //            }
                //        }
                //    },
                //    tooltip: {
                //        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                //        pointFormat:
                //            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                //    },

                //    "series": [
                //        {
                //            "name": "",
                //            "colorByPoint": true,
                //            "data":
                //            (function() {
                //                // generate an array of random data
                //                var data = [],

                //                    i;

                //                for (var key in duesdata) {
                //                    if (duesdata.hasOwnProperty(key)) {
                //                        data.push([
                //                            duesdata[key].head,
                //                            duesdata[key].amount, false
                //                        ]);
                //                    }
                //                }
                //                return data;
                //            }())
                //        }
                //    ]
                //});

                ////Separation
                Highcharts.chart('sepdt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, penbilhead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in sepdata) {
                                    if (sepdata.hasOwnProperty(key)) {
                                        data.push([
                                            sepdata[key].head,
                                            sepdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                ////Man Power
                Highcharts.chart('pffunddt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, dueshead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in pffunddata) {
                                    if (pffunddata.hasOwnProperty(key)) {
                                        data.push([
                                            pffunddata[key].head,
                                            pffunddata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });


                ////Employee Joining
                Highcharts.chart('joindt',
                {
                    chart: {
                        type: 'bar'
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
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, dueshead) !== -1) {
                                    return '<span style="fill: maroon;">' + this.value + '</span>';
                                } else {
                                    return this.value;
                                }
                            },
                            style: {
                                color: '#000',

                            }
                        }
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
                                format: '{point.y:.2f}'
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
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in empjoindata) {
                                    if (empjoindata.hasOwnProperty(key)) {
                                        data.push([
                                            empjoindata[key].head,
                                            empjoindata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });


                ////Construction Progress
                //Highcharts.chart('conprodt',
                //{
                //    chart: {
                //        type: 'bar'
                //    },
                //    title: {
                //        text: ''
                //    },
                //    subtitle: {
                //        text: '',
                //        style: {
                //            color: '#44994a',
                //            fontWeight: 'bold'
                //        }
                //    },


                //    xAxis: {
                //        type: 'category',
                //        labels:
                //        {
                //            formatter: function() {
                //                if ($.inArray(this.value, conprohead) !== -1) {
                //                    return '<span style="fill: maroon;">' + this.value + '</span>';
                //                } else {
                //                    return this.value;
                //                }
                //            },
                //            style: {
                //                color: '#000',

                //            }
                //        }
                //    },
                //    yAxis: {
                //        title: {
                //            text: ''
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
                //                format: '{point.y:.2f}'
                //            }
                //        }
                //    },
                //    tooltip: {
                //        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                //        pointFormat:
                //            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                //    },

                //    "series": [
                //        {
                //            "name": "",
                //            "colorByPoint": true,
                //            "data":
                //            (function() {
                //                // generate an array of random data
                //                var data = [],

                //                    i;

                //                for (var key in conprodata) {
                //                    if (conprodata.hasOwnProperty(key)) {
                //                        data.push([
                //                            conprodata[key].head,
                //                            conprodata[key].amount, false
                //                        ]);
                //                    }
                //                }
                //                return data;
                //            }())
                //        }
                //    ]
                //});
                /////Future Cost
                //Highcharts.chart('fcostdt',
                //{
                //    chart: {
                //        type: 'bar'
                //    },
                //    title: {
                //        text: ''
                //    },
                //    subtitle: {
                //        text: '',
                //        style: {
                //            color: '#44994a',
                //            fontWeight: 'bold'
                //        }
                //    },


                //    xAxis: {
                //        type: 'category',
                //        labels:
                //        {
                //            formatter: function() {
                //                if ($.inArray(this.value, fcosthead) !== -1) {
                //                    return '<span style="fill: maroon;">' + this.value + '</span>';
                //                } else {
                //                    return this.value;
                //                }
                //            },
                //            style: {
                //                color: '#000',

                //            }
                //        }
                //    },
                //    yAxis: {
                //        title: {
                //            text: ''
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
                //                format: '{point.y:.2f}'
                //            }
                //        }
                //    },
                //    tooltip: {
                //        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                //        pointFormat:
                //            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                //    },

                //    "series": [
                //        {
                //            "name": "",
                //            "colorByPoint": true,
                //            "data":
                //            (function() {
                //                // generate an array of random data
                //                var data = [],

                //                    i;

                //                for (var key in fcostdata) {
                //                    if (fcostdata.hasOwnProperty(key)) {
                //                        data.push([
                //                            fcostdata[key].head,
                //                            fcostdata[key].amount, false
                //                        ]);
                //                    }
                //                }
                //                return data;
                //            }())
                //        }
                //    ]
                //});

                ////Fund Vs Cost
                //Highcharts.chart('fundcstdt',
                //{
                //    chart: {
                //        type: 'bar'
                //    },
                //    title: {
                //        text: ''
                //    },
                //    subtitle: {
                //        text: '',
                //        style: {
                //            color: '#44994a',
                //            fontWeight: 'bold'
                //        }
                //    },


                //    xAxis: {
                //        type: 'category',
                //        labels:
                //        {
                //            formatter: function() {
                //                if ($.inArray(this.value, fvscosthead) !== -1) {
                //                    return '<span style="fill: maroon;">' + this.value + '</span>';
                //                } else {
                //                    return this.value;
                //                }
                //            },
                //            style: {
                //                color: '#000',

                //            }
                //        }
                //    },
                //    yAxis: {
                //        title: {
                //            text: ''
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
                //                format: '{point.y:.2f}'
                //            }
                //        }
                //    },
                //    tooltip: {
                //        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                //        pointFormat:
                //            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                //    },

                //    "series": [
                //        {
                //            "name": "",
                //            "colorByPoint": true,
                //            "data":
                //            (function() {
                //                // generate an array of random data
                //                var data = [],

                //                    i;

                //                for (var key in fvscostdata) {
                //                    if (fvscostdata.hasOwnProperty(key)) {
                //                        data.push([
                //                            fvscostdata[key].head,
                //                            fvscostdata[key].amount, false
                //                        ]);
                //                    }
                //                }
                //                return data;
                //            }())
                //        }
                //    ]
                //});

            } catch (e) {

                alert(e);
            }


        }

    </script>

     <style>
         ul.footerMenu li {
             display: block;
             list-style: none;
             padding: 0;
             /* border-bottom: 0; */
         }

         ul.footerMenu li a {
             text-align: left;
             display: block;
             cursor: pointer;
             /* background: #32CD32; */
             background: #046971;
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

         ul.footerMenu li a:hover {
             background: red;
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
         #demo1 {
             margin-top: 30px;
             position: absolute;
             z-index: 200;
             margin-left: 1050px;
         }
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <div class="row" style="margin-bottom: 30px;">
                           
                                    <div class="col-md-4">
                                         
                                        <div>
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" 
                                             ClientIDMode="Static" ></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" ClientIDMode="Static" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>

                                        <div class="colMdbtn">
                                          
                                            <asp:LinkButton ID="btnok" runat="server" CssClass="btn btn-primary okBtn" OnClientClick="GetData();return false;" >OK</asp:LinkButton>
                                         
                                        </div>
                                            </div>
                                    </div>
                               <div class="col-md-8">
                                   <div class="col-md-3 pull-left">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblProgressBar" Text="Please wait . . . . . . ."
                                                    Width="218px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                   <asp:Label runat="server" CssClass="col-md-offset-4" style="font-size: 16px;font-family:sans-serif">Taka In Lac</asp:Label>
                               
                            </div>
                        
                            <%--
                             --%>
                            </div>
                        <div class="row">
                             <div class="splitterh">
                                </div>
                            <div style="background: #2E3639">
                            <div class="row" style=" padding-top: 7px; margin-bottom:10px;">
                               
                                <asp:Label runat="server" style="color: #67D19A; margin-left: 40px; font-size: 24px; font-weight: bold;">Pending Task</asp:Label>
                            </div>
                            <div class="row" style="padding-bottom: 10px; ">
                                <div class="col-sm-3 col-md-3 col-lg-3" style="margin-left: 40px;">
                                     
                                   
                                       
                                </div>
                                <div class="col-sm-3 col-md-3 col-lg-3" style="margin-left: 20px">
                                    
                                </div>
                                <div class="col-sm-2 col-md-2 col-lg-2" style="margin-left: -70px">
                                    <asp:HyperLink runat="server" NavigateUrl="~/F_81_Hrm/F_92_Mgt/InterfaceHR.aspx" Target="_blank"  style="color: #ffffff;font-size: 16px; margin-left: -15px;font-weight: bold;" id="HyperLink3">HR Interface</asp:HyperLink>
                                </div>
                                 <div class="col-sm-3 col-md-3 col-lg-3">
                               
                                 </div>
                                 <div class="col-sm-2 col-md-2 col-lg-2" >
                                     
                                 </div>
                            </div>
                        </div>
                            </div>
                        <div class="row">
                            <div class="col-md-6" style="border: 1px solid #EDEDED; border-right:3px solid #EDEDED">
                                <div class="col-md-6">
                                     
                                    <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" Visible="False" NavigateUrl="~/F_81_Hrm/F_92_Mgt/AllEmpList.aspx?Type=Report" target="_blank"  id="lblsales" style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;">Member</asp:HyperLink>
                                    </div>
                                    <table id="member" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Number</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>


                                     <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>


                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px;margin-top: 25px">
                                        <div id="memberdt" style="width:295px;height: 220px; margin: 0 auto"></div>
                                    </div>
                            </div>
                            <div class="col-md-6" style="border: 1px solid #EDEDED">
                                <div class="col-md-6">
                                    <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                     <asp:HyperLink  runat="server" target="_blank" Visible="False"  style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" id="hlnkatt" 
                                         ClientIDMode="Static"  >Attendance</asp:HyperLink>
                                        </div>
                                        <table id="atten" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Number</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    </div>
                                     
                                <div class="col-md-6" style="margin-left: -50px; margin-top: 25px">
                                        <div id="attendt" style="width: 290px;height: 220px; margin: 0 auto"></div>
                                    </div>
                                </div>
                         </div>
                        
                        <div class="row">
                            <div class="col-md-6" style="border: 1px solid #EDEDED; border-right:3px solid #EDEDED">
                                <div class="col-md-6">
                                    <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                    <asp:HyperLink runat="server" NavigateUrl="~/F_81_Hrm/F_97_Mis/RptMgtInterface.aspx" target="_blank" Visible="False" style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" id="lblcons">Salary</asp:HyperLink>
                                        </div>
                                    <table id="salary" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>    
                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px;margin-top: 25px">
                                        <div id="salarydt" style="width:295px;height: 220px; margin: 0 auto"></div>
                                    </div>
                            </div>
                             <div class="col-md-6" style="border: 1px solid #EDEDED">
                                  <div class="col-md-6">
                                    <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" Visible="False" NavigateUrl="~/F_81_Hrm/F_84_Lea/EmpLeaveStatus.aspx" target="_blank" style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" id="lblaccount">Leave</asp:HyperLink>
                                        </div>
                                        
                                      <table id="leave" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Number</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>  
                                      

                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px; margin-top: 25px">
                                        <div id="leavedt" style="width: 290px;height: 220px; margin: 0 auto"></div>
                                    </div>
                                </div>
                         </div>
                        
                        <div class="row">
                            <div class="col-md-6" style="border: 1px solid #EDEDED; border-right:3px solid #EDEDED">
                                <div class="col-md-6">
                                     
                                    <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                    <asp:HyperLink runat="server" NavigateUrl="~/F_81_Hrm/F_92_Mgt/HREmpConfirmation.aspx" Target="_blank" Visible="False" style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" id="lblbbalance">Confirmation</asp:HyperLink>
                                        </div>
                                    
                                     <table id="confirm" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Number</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>  
                                      
                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px;margin-top: 25px">
                                        <div id="confirmdt" style="width: 290px;height: 220px; margin: 0 auto"></div>
                                    </div>
                                </div>
                            <div class="col-md-6" style="border: 1px solid #EDEDED">
                                <div class="col-md-6">
                                     <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                    <asp:HyperLink runat="server" NavigateUrl="~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType" target="_blank" Visible="False" style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" id="lblbill">Separation</asp:HyperLink>
                                         </div>
                                        
                                     <table id="sep" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Number</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>  

                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px; margin-top: 25px">
                                        <div id="sepdt" style="width:295px;height: 220px; margin: 0 auto"></div>
                                    </div>
                            </div>

                            
                         </div>
                        
                        <div class="row">
                            <div class="col-md-6" style="border: 1px solid #EDEDED; border-right:3px solid #EDEDED">
                                <div class="col-md-6">
                                     
                                    <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                     <asp:HyperLink runat="server" NavigateUrl="~/F_81_Hrm/F_85_Lon/EmpLoanStatusOpn.aspx" target="_blank" Visible="False" style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" id="lblstock">Loan</asp:HyperLink>
                                        </div>
                                    
                                     <table id="loan" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>  
                                        
                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px;margin-top: 25px">
                                        <div id="loandt" style="width:295px;height: 220px; margin: 0 auto"></div>
                                    </div>
                            </div>
                            <div class="col-md-6" style="border: 1px solid #EDEDED">
                                <div class="col-md-6">
                                     <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                      <asp:HyperLink runat="server" Visible="False" Target="_blank" style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" id="lbldues">PF Accounts</asp:HyperLink>
                                      </div>
                                        <table id="pffund" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table> 
                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px; margin-top: 25px">
                                        <div id="pffunddt" style="width: 290px;height: 220px; margin: 0 auto"></div>
                                    </div>
                                </div>

                            <div class="col-md-6" style="border: 1px solid #EDEDED">
                                <div class="col-md-6">
                                     <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                      <asp:HyperLink runat="server" Target="_blank" style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" id="HyperLink1">Employee Joining</asp:HyperLink>
                                      </div>
                                        <table id="empjoin" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Number</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table> 
                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px; margin-top: 25px">
                                        <div id="joindt" style="width: 290px;height: 220px; margin: 0 auto"></div>
                                    </div>
                                </div>
                            
                         </div>
                        
                          <div class="footer row">
                                <div class="splitter">
                                </div>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div style="margin-left: 25px" class="footerCol quickLink">
                                                <h4 style="color: #67D19A">Appointment</h4>
                                                <ul class="Menulinks">
                                         
                                                  <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpAllInfo")%>" target="_blank">Employee Information </a></li>
                                                   
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div style="margin-left: 25px" class="footerCol quickLink">
                                                <h4 style="color  :#67D19A"> Attendance System </h4>
                                                <ul class="Menulinks">
                                                    
                                                     <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptAttendenceSheet.aspx")%>" target="_blank">Attendance Information</a></li>
                                                    
                                                    
                                                </ul>
                                            </div>
                                        </div>
                                        
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div style="margin-left: 25px" class="footerCol quickLink">
                                                <h4 style="color : #67D19A">Leave Monitoring</h4>
                                                <ul class="Menulinks">
                                                    <li><a href="<%=this.ResolveUrl("~//F_81_Hrm/F_84_Lea/RptHREmpLeave.aspx?Type=EmpLeaveSt")%>" target="_blank">Employee Leave Status</a></li>
                                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02.aspx?Type=EmpLeaveStatus")%>" target="_blank">Leave Status-Company Wise</a></li>
                                                     
                                                    
                                                </ul>
                                            </div>
                                        </div>

                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div style="margin-left: 15px" class="footerCol quickLink">
                                                <h4 style="color : #67D19A">Annual Increment</h4>
                                                <ul class="Menulinks">
                                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_93_AnnInc/RptIncrement.aspx")%>" target="_blank">Increment Report</a></li>
                                                   
                                                    
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                     
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div style="margin-left: 25px" class="footerCol quickLink">
                                                <h4 style="color: #67D19A">Payroll Syatem</h4>
                                                <ul class="Menulinks">
                                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>" target="_blank">Actual Salary Sheet</a></li>
                                                     <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=CashSalary")%>" target="_blank">Salary Statement-Cash</a></li>
                                                    
                                                     <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/EmpBankSalary.aspx?Type=Entry")%>" target="_blank">Salary Transfer statemeent</a></li>
                                                     <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>" target="_blank">Employee Separation Report</a></li>
                                                   
                                                    
                                                     <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpHold")%>" target="_blank">Employee Hold List</a></li>
                                                     <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum")%>" target="_blank">Salary Summary</a></li>
                                                   
                                                   
                                                    
                                                </ul>
                                            </div>
                                        </div>
                                     
                                        

                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div style="margin-left: 25px;" class="footerCol quickLink">
                                                <h4 style="color: #67D19A">ACR</h4>
                                                <ul class="Menulinks">
                                                    <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_91_ACR/RptPerAppraisal.aspx")%>" target="_blank">Employee Performance Apprisal</a></li>
                                                   
                                                    
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
        
                          </div>
                        </div>
                    </div>
                
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

