<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PurReportInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.PurReportInterface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
            text-align: center;
        }

        .th1 {
            width: 250px;
            text-align: center;
        }

        .th2 {
            width: 90px;
            text-align: center;
        }

        .th3 {
            width: 70px;
            text-align: center;
        }
    </style>

   
    <script src="../Scripts/highchart2.js"></script>
    
    

    <script language="javascript" type="text/javascript">

        var comcod, Date1, Date2;
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
          
            //GetData();
            //GetData();
           

        }



        function GetData(graphdata) {
            try {
               
                var result = JSON.parse(graphdata);
                comcod = <%=this.GetCompCode()%>;
                Date1 = $('#<%=this.txtDateFrom.ClientID%>').val();
                Date2 = $('#<%=this.txtDateto.ClientID%>').val();
                
                <%-- comcod = <%=this.GetCompCode()%>; 
                var temp =comcod.toString();
                var com = temp.slice(0, 1);
                Date2 = $('#txtDateto').val();
                Date1 = $('#txtDateFrom').val();
                Date1 = $('#<%=this.txtDateFrom.ClientID%>').val()
                $("#lblpurchase").attr("href","../../F_14_Pro/PurInformation?Type=Report&comcod="+comcod);
                /////Footer Linking -------------------
                
                ///Purchase Link---
                //$("#lnkdaywpur").attr("href", "F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=DaywPur&comcod=" + comcod);
                //$("#lnkpursum").attr("href", "F_14_Pro/RptPurchaseStatus02?Type=Purchase&comcod=" + comcod);
                //$("#lnkpurhis").attr("href", "F_14_Pro/RptMatPurHistory?Type=Report&comcod=" + comcod);
                //$("#lnkpurhissup").attr("href", "F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=IndSup&comcod="+comcod);
                //$("#lnkratev").attr("href", "F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=MatRateVar&comcod="+comcod);
                //$("#lnkmatdel").attr("href", "F_14_Pro/RptDeliveryEfficiency?Type=Report&comcod="+comcod);
                //$("#lnksupoveral").attr("href", "F_14_Pro/RptSupCreditLimit?Type=RptSupCredit&comcod="+comcod);
                
              
              
          
                $.ajax({
                    type: "POST",
                    url: "PurReportInterface/GetAllData",
                    data: '{comcod:"'+comcod+'",  date1: "' + $('#<%=this.txtDateFrom.ClientID%>').val() + '" , date2: "' + $('#<%=this.txtDateto.ClientID%>').val() + '"}',
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
                });--%>
               
                ExecuteGraph(result);
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

                var bgddata = bgd;
                console.log(bgddata);

               


              

                //Purchase Legend
                var purchasedata = bgddata['purchase'];
                var purchasehead = [];
                for (var i = 0; i < purchasedata.length; i++) {
                    purchasehead[i] = purchasedata[i]["head"];
                }

                var paycode='2600000000';
                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(purchasedata,
                    function(i, item) {
                        ar1 = (item.gcod == "01001")
                            ? '<a target=_blank href=' + encodeURI('../F_14_Pro/PurSumMatWise?Type=Report'+ '&comcod=' +comcod+ '&Date1=' +Date1 + '&Date2=' + Date2) + '>'
                            : item.gcod == "01002"? '<a target=_blank href=' + encodeURI('../F_17_Acc/LinkRptReciptPayment?Type=payment&comcod=' + comcod + '&Date1=' +Date1 + '&Date2=' + Date2+'&paycode=' + paycode) + '>'
                            : '';
                        ar2 = (item.gcod == "01001") || (item.gcod == "01002") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + ar1 + item.head + ar2 + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                            "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#purchase tbody").html(row);
                    });
               
                
                
              
                //Purchase
                Highcharts.chart('purchasedt',
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

                                for (var key in purchasedata) {
                                    if (purchasedata.hasOwnProperty(key)) {
                                        data.push([
                                            purchasedata[key].head,
                                            purchasedata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });


                

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

                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                               
                                <div class="form-group">
                                    <div class="col-md-12  pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName"
                                            Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom"></cc1:CalendarExtender>


                                        <asp:Label ID="lbldateTo" runat="server" Font-Bold="True"
                                            Text="Date:" CssClass="smLbl_to" Visible="true"></asp:Label>

                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttoDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                                        
                                      
                                       
                                       
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                         <%--<asp:LinkButton ID="btnok" runat="server" CssClass="btn btn-primary okBtn" OnClientClick="GetData();">OK</asp:LinkButton>--%>
                                    </div>
                                
                                    


                                </div>
                            </div>
                                
                        </fieldset>
                        <%--<div class="row" style="margin-bottom: 30px;">

                            <div class="col-md-4">

                                <div>
                                    <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                    <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"
                                        ClientIDMode="Static"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>


                                    <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                    <asp:TextBox ID="txtDateto" ClientIDMode="Static" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>

                                    <div class="colMdbtn">

                                        <asp:LinkButton ID="btnok" runat="server" CssClass="btn btn-primary okBtn" OnClientClick="GetData();">OK</asp:LinkButton>

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
                                <asp:Label runat="server" CssClass="col-md-offset-4" Style="font-size: 16px; font-family: sans-serif">Taka In Lac</asp:Label>

                            </div>


                        </div>--%>
                        
                        
                                <div class="row">
                            
                            <div class="col-md-12" style="border: 1px solid #EDEDED;" runat="server" visible="false" id="graphpart">
                                <div class="col-md-6">
                                    <div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" Target="_blank" Visible="False" Style="font-size: 16px; margin-left: -15px; color: #70737c; font-weight: bold;" ID="lblpurchase" ClientIDMode="Static">Purchase</asp:HyperLink>
                                    </div>
                                    <table id="purchase" class="table-striped table-hover table-bordered">
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
                                    <div id="purchasedt" style="width: 400px; height: 300px; margin: 0 auto"></div>
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


