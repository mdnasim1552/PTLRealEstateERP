<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptSaleMonYear.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptSaleMonYear" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
     <script src="../Scripts/highchart2.js"></script>
    
   
     <style type="text/css">
        .tblh {
            background: #DFF0D8;
            height: 30px;
            text-align: center;
        }

        .th1 {
            width: 170px;
            text-align: center;
        }

        .th2 {
            width: 60px;
            text-align: center;
        }

        .th3 {
            width: 60px;
            text-align: center;
        }
         .th4 {
            width: 60px;
            text-align: center;
        }

        .th5 {
            width: 60px;
            text-align: center;
        }
        #monsale {
            margin-left: 3px;
        }
         #yearsale {
             margin-left: 3px;
         }
    </style>
    

    <script language="javascript" type="text/javascript">
        var comcod,date1,date2;
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            GetData();
        }

        function GetData() 
        {
            

            try {
                comcod = <%=this.GetCompCode()%>;
                date1 = $('#<%=this.txtfromdate.ClientID%>').val();
                date2 = $('#<%=this.txttodate.ClientID%>').val();
                $.ajax({
                    type: "POST",
                    url: "RptSaleMonYear.aspx/GetAllData",
                    data: '{comcod:"'+comcod+'", date1:"' +
                        $('#<%=this.txtfromdate.ClientID%>').val() +
                        '",date2:"' +
                        $('#<%=this.txttodate.ClientID%>').val() +
                        '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        //console.log(JSON.parse(response.d));
                        var data = response.d;

                        //console.log(data['account']);
                        ExecuteGraph(data);
                    },
                    failure: function(response) {
                        //  alert(response);
                        alert("f");
                    }
                });
            } catch (e) {
                alert(e);
            };

        }

        function ExecuteGraph(bgd) {

            try {

                var data = JSON.parse(bgd);

                var monsale = data["monsale"];

                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(monsale,
                    function (i, item) {
                        ar1 = (item.team == "Total ( In lac )")
                            ? '<a target=_blank href=' + encodeURI('RptSalSummery.aspx?Type=dSaleVsColl&comcod='+comcod+'&Date1='+date1+'&Date2='+date2) + '>'
                            : '';
                        ar2 = (item.team == "Total ( In lac )")? '</a>' : '';
                        row += (item.team == "Total ( In lac )") ? "<tr style='font-weight:bold;color:maroon;background-color:#C0C0C0;'>" : "<tr>";
                        row += "<td>" +ar1+ item.team +ar2+ "</td>";
                        row += "<td style=text-align:right;>" + ((item.msaleamt == 0) ? '' : (item.msaleamt.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.uasaleamt == 0) ? '' : (item.uasaleamt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.pmonsaleamt == 0) ? '' : (item.pmonsaleamt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.lstymonth == 0) ? '' : (item.lstymonth.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#monsale tbody").html(row);
                    });





                var yearsale = data["yearsale"];
                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(yearsale,
                    function (i, item) {
                        ar1 = (item.team == "Total ( In lac )")
                            ? '<a target=_blank href=' + encodeURI('RptSalSummery.aspx?Type=dSaleVsColl&comcod='+comcod+'&Date1='+date1+'&Date2='+date2) + '>'
                            : '';
                        ar2 = (item.team == "Total ( In lac )") ? '</a>' : '';
                        row += (item.team == "Total ( In lac )") ? "<tr style='font-weight:bold;color:maroon;background-color:#C0C0C0;'>" : "<tr>";
                        row += "<td>" + ar1 + item.team + ar2 + "</td>";
                        row += "<td style=text-align:right;>" + ((item.msaleamt == 0) ? '' : (item.msaleamt.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.actty == 0) ? '' : (item.actty.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.actly == 0) ? '' : (item.actly.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#yearsale tbody").html(row);
                    });

                //Monthly Sales
                $('#monsaldt').highcharts({


                    chart: {
                        type: 'column'
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
                    yAxis: {
                        title: {
                            text: 'Percentage'
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
                                format: '{point.y:.1f} %'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": "ABP",
                                    "y": monsale[0]['msaleamt'],

                                },
                                {
                                    "name": "Actual TM",
                                    "y": monsale[0]['uasaleamt'],

                                },

                                {
                                    "name": "Last Month",
                                    "y": monsale[0]['pmonsaleamt'],

                                },
                                {
                                    "name": "SMLY",
                                    "y": monsale[0]['lstymonth']

                                }
                            ]
                        }
                    ]
                });

                //Yearly sales
                $('#saleyearlydt').highcharts({


                    chart: {
                        type: 'column'
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
                    yAxis: {
                        title: {
                            text: 'Percentage'
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
                                format: '{point.y:.1f} %'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data": [
                                {
                                    "name": "ABP",
                                    "y": yearsale[0]['msaleamt'],

                                },
                                {
                                    "name": "Actual TY",
                                    "y": yearsale[0]['actty'],

                                },

                                {
                                    "name": "Actual LY",
                                    "y": yearsale[0]['actly']

                                }
                            ]
                        }
                    ]
                });
            }
            catch (e)
            {
                alert(e)
            }
            

        }

    </script>
    
    
    
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-4 pading5px asitCol4">

                                <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                    Text="From.:" CssClass="lblTxt lblName"></asp:Label>

                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputtextbox" ClientIDMode="Static"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                                <asp:Label ID="Label6" runat="server"
                                    Text="To:" CssClass="smLbl_to"></asp:Label>

                                <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" ClientIDMode="Static"
                                    Font-Bold="True"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                <asp:LinkButton ID="lbtnOk" runat="server" OnClientClick="GetData();"
                                    CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>

                </fieldset>
                <div class="row">
                           
                                <div class="col-md-6">
                                   
                                        <asp:HyperLink runat="server" Visible="False" CssClass="btn btn-sm btn-success" id="lblmonsal"></asp:HyperLink>
                                     <table id="monsale" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1"></th>
                                                <th class="th2">ABP</th>
                                                <th class="th3">Actual TM</th>
                                                <th class="th4">Last Month</th>
                                                <th class="th5">SMLY</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table> 
                                        
                                    </div>
                                    <div class="col-md-6">
                                        <div id="monsaldt" style="width:600px;height:250px; margin: 0 auto"></div>
                                    </div>
                                
                            
                         </div>
                <hr/>
                <div class="row">
               
                                <div class="col-md-6">
                                      
                                     <asp:HyperLink runat="server" Visible="False" class="btn btn-sm btn-success" id="lblyearlysal"></asp:HyperLink>
                                        <table id="yearsale" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1"></th>
                                                <th class="th2">ABP</th>
                                                <th class="th3">Actual TY</th>
                                                <th class="th4">Actual LY</th> 
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table> 
                                    
                                    </div>
                                    <div class="col-md-6" style="margin-left: -50px">
                                        <div id="saleyearlydt" style="width:600px;height: 250px; margin: 0 auto"></div>
                                    </div>
                          
                </div>

            </div>
        </div>
    </div>
             </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

