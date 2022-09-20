<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CompanyOverAllReport.aspx.cs" Inherits="RealERPWEB.CompanyOverAllReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
            text-align: center;
        }

        .th1 {
            width: 200px;
            text-align: center;
        }

        .th2 {
            width: 100px;
            text-align: center;
        }

        .th3 {
            width: 100px;
            text-align: center;
        }
    </style>

    <script src="Scripts/highchart2.js"></script>
    <script src="Scripts/highchartexporting.js"></script>

    <script language="javascript" type="text/javascript">

        var comcod, Date1, Date2, ispostback=1;
        $(document).ready(function () {

            const urlParams = new URLSearchParams(window.location.search);
            const myParam = urlParams.get('Type');
            
            if(myParam==null){                
                $('#aBudgettab')[0].click();
            }


            if(myParam=="sales")
            {
               
                $('#aSales')[0].click();
                $("#tb_subCon").hide();
                $("#tb_purchasetab").hide();
                $("#tb_Budget").hide();
                $("#tb_consttab").hide();
                $("#tb_PrjReptab").hide();
                $("#tb_invtab").hide();
                $("#tb_Stocktab").hide();
                $("#tb_Accountstab").hide();
                $("#tb_cashbalancetab").hide();
                $("#tb_PendingPaytab").hide();
                $("#tb_FutureFundtab").hide();
                $("#tb_DueOduestab").hide();
                $("#tb_FutureCsttab").hide();
                $("#tb_FuturefundvsCsttab").hide();
             //   $("#tb_Salestab").hide();
                 
            }
            if(myParam=="Accounts")
            {
               
                $('#aAccounts')[0].click();
                $("#tb_subCon").hide();
                $("#tb_purchasetab").hide();
                $("#tb_Budget").hide();
                $("#tb_consttab").hide();
                $("#tb_PrjReptab").hide();
                $("#tb_invtab").hide();
                $("#tb_Stocktab").hide();
               // $("#tb_Accountstab").hide();
                $("#tb_cashbalancetab").hide();
                $("#tb_PendingPaytab").hide();
                $("#tb_FutureFundtab").hide();
                $("#tb_DueOduestab").hide();
                $("#tb_FutureCsttab").hide();
                $("#tb_FuturefundvsCsttab").hide();
                   $("#tb_Salestab").hide();
                 
                
            }
          
            if(myParam=="Procurement")
            {
               
                $('#aProcurement')[0].click();
                $("#tb_subCon").hide();
               // $("#tb_purchasetab").hide();
                $("#tb_Budget").hide();
                $("#tb_consttab").hide();
                $("#tb_PrjReptab").hide();
                $("#tb_invtab").hide();
                $("#tb_Stocktab").hide();
                 $("#tb_Accountstab").hide();
                $("#tb_cashbalancetab").hide();
                $("#tb_PendingPaytab").hide();
                $("#tb_FutureFundtab").hide();
                $("#tb_DueOduestab").hide();
                $("#tb_FutureCsttab").hide();
                $("#tb_FuturefundvsCsttab").hide();
                $("#tb_Salestab").hide();
            }
            if(myParam=="Construction")
            {
               
                $('#aSubconsttab')[0].click();
                $("#tb_subCon").hide();
                 $("#tb_purchasetab").hide();
                $("#tb_Budget").hide();
                //$("#tb_consttab").hide();
                $("#tb_PrjReptab").hide();
                $("#tb_invtab").hide();
                $("#tb_Stocktab").hide();
                $("#tb_Accountstab").hide();
                $("#tb_cashbalancetab").hide();
                $("#tb_PendingPaytab").hide();
                $("#tb_FutureFundtab").hide();
                $("#tb_DueOduestab").hide();
                $("#tb_FutureCsttab").hide();
                $("#tb_FuturefundvsCsttab").hide();
                $("#tb_Salestab").hide();
            }
            if(myParam=="SubContractor")
            {
               
                $('#aSubconsttab')[0].click();
                //$("#tb_subCon").hide();
                $("#tb_purchasetab").hide();
                $("#tb_Budget").hide();
                $("#tb_consttab").hide();
                $("#tb_PrjReptab").hide();
                $("#tb_invtab").hide();
                $("#tb_Stocktab").hide();
                $("#tb_Accountstab").hide();
                $("#tb_cashbalancetab").hide();
                $("#tb_PendingPaytab").hide();
                $("#tb_FutureFundtab").hide();
                $("#tb_DueOduestab").hide();
                $("#tb_FutureCsttab").hide();
                $("#tb_FuturefundvsCsttab").hide();
                $("#tb_Salestab").hide();
            }
            
           
            $("#btnok").click();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.RealProgressbar').hide();
           

          
            //alert("I m In");
             GetData();
            //if(ispostback)
            //{
            //    $('.RealProgressbar').hide();
            //    ispostback=0;
            //}

            
           
           
        }



        function GetData() {
            try {
                 
                comcod = <%=this.GetCompCode()%>;
                var temp = comcod.toString();
                var com = temp.slice(0, 1);
                Date2 = $('#txtDateto').val();
                Date1 = $('#txtDateFrom').val();
                NavigateUrl = "~/F_99_Allinterface/SalesInterface.aspx";

                if (com == "1"){
                    
                    $("#aSales").text("Billing");
                    $("#lblsales").text("Billing");
                }

                if (com == "2") {
                    $("#lblcons").text("Land Procurement");
                    $("#lblcons").attr("href", "F_14_Pro/RptPaymetDueAllPrj.aspx");
                }
                else {
                    $("#lblcons").text("Construction");
                    $("#lblcons").attr("href", "F_08_PPlan/ConstructionInfo.aspx?Type=Report&comcod=" + comcod);
                    $("#lblscon").attr("href", "F_08_PPlan/SubConInformation.aspx?Type=Report&comcod=" + comcod);

                }
                //alert(comcod);
                $("#lblsales").attr("href", "F_22_Sal/SalesInformation.aspx?Type=Report&comcod=" + comcod + "&Date2=" + Date2);
                $("#lblpurchase").attr("href", "F_14_Pro/PurInformation.aspx?Type=Report&comcod=" + comcod + "&Date2=" + Date2 + "&prjcode=");
                

                $("#lblaccount").attr("href", "F_18_MAcc/AccDashBoard.aspx?Type=Report&comcod=" + comcod);
                $("#hlnkinventory").attr("href", "F_12_Inv/RptInventoryAll.aspx?Type=Report&comcod=" + comcod + "&Date1=" + Date1 + "&Date2=" + Date2);

                $("#lblbbalance").attr("href", "F_17_Acc/AccTrialBalance.aspx?Type=BankPosition&comcod=" + comcod + "&Date1=" + Date1 + "&Date2=" + Date2);
                $("#lbldues").attr("href", "F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=" + comcod);
                $("#lblmanpower").attr("href", "F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod=" + comcod + "&prjcode=");
                $("#lblsalary").attr("href", "F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan&comcod=" + comcod);
                $("#lblbudget").attr("href", "F_04_Bgd/RptBgdAll.aspx?comcod=" + comcod);
                $("#lnkratio").attr("href", "F_32_Mis/RptRatioAnalisiys.aspx?Type=Report&comcod=" + comcod);

                //-----------------------------------////
                $("#lnkratio1").attr("href", "F_32_Mis/RatioAnaWithGraph.aspx?grp=01&comcod=" + comcod);
                $("#lnkratio2").attr("href", "F_32_Mis/RatioAnaWithGraph.aspx?grp=02&comcod=" + comcod);
                $("#lnkratio3").attr("href", "F_32_Mis/RatioAnaWithGraph.aspx?grp=03&comcod=" + comcod);
                $("#lnkratio4").attr("href", "F_32_Mis/RatioAnaWithGraph.aspx?grp=04&comcod=" + comcod);
                $("#lnkratio5").attr("href", "F_32_Mis/RatioAnaWithGraph.aspx?grp=05&comcod=" + comcod);

              



                $("#actinterface").attr("href", "F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=" + comcod);
                $("#purinter").attr("href", "F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=" + comcod);
                $("#salesinter").attr("href", "F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=" + comcod);
                $("#coninter").attr("href", "F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=" + comcod);
                $("#crminter").attr("href", "F_99_Allinterface/KPIDashboard.aspx?Type=Report&comcod=" + comcod);
                $("#lblbill").attr("href", "F_15_DPayReg/RptPaymentGraph.aspx?Type=Report&comcod=" + comcod);
                /////Footer Linking -------------------
                $("#lnkfnst").attr("href", "F_17_Acc/AccFincStatmnt.aspx?Type=Report&comcod=" + comcod);
                $("#lnktrialbal").attr("href", "F_17_Acc/AccTrialBalance.aspx?Type=Mains&comcod=" + comcod);

                
                $("#lnkincst").attr("href", "F_32_Mis/ProjectSummary.aspx?Type=Report&comcod=" + comcod);
                $("#lnkprjres").attr("href", "F_32_Mis/RptProjectStatus.aspx?Type=Prjwiseres&comcod=" + comcod);
                // $("#lnkrationana").attr("href", "F_32_Mis/RptRatioAnalisiys.aspx?Type=Report&comcod=" + comcod);
                $("#lnkpayable").attr("href", "F_17_Acc/RptAccSpLedger.aspx?Type=ASupConPayment&comcod=" + comcod);
                $("#lnkfinanper").attr("href", "F_05_Busi/YearlyPlanningSt.aspx?Type=Income&comcod=" + comcod);
                $("#lnkcashbg").attr("href", "F_05_Busi/YearlyPlanningSt.aspx?Type=CBudget&comcod=" + comcod);
                $("#lnkrecv").attr("href", "F_23_CR/RptReceivedList04.aspx?Type=AllProDuesCollect&comcod=" + comcod);
                //$("#lnkoverall").attr("href", "F_46_GrMgtInter/RptGrpDailyReportJq.aspx?Type=Report&comcod=" + comcod);
                ///Purchase Link---  
                $("#lnkdaywpur").attr("href", "F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur&comcod=&Date1=&Date2=" + comcod);
                $("#lnkpursum").attr("href", "F_14_Pro/RptPurchaseStatus02.aspx?Type=Purchase&comcod=" + comcod);
                $("#lnkpurhis").attr("href", "F_14_Pro/RptMatPurHistory.aspx?Type=Report&comcod=" + comcod);
                $("#lnkpurhissup").attr("href", "F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=IndSup&comcod=&Date1=&Date2=" + comcod);
                $("#lnkratev").attr("href", "F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=MatRateVar&comcod=" + comcod);
                $("#lnkmatdel").attr("href", "F_14_Pro/RptDeliveryEfficiency.aspx?Type=Report&comcod=" + comcod);
                $("#lnksupoveral").attr("href", "F_14_Pro/RptSupCreditLimit.aspx?Type=RptSupCredit&comcod=" + comcod);

                ///Land Feasibility
                $("#lnklnddata").attr("href", "F_01_LPA/RptAllProTopSheet.aspx?Type=Report&comcod=" + comcod);
                ////Sales 
                if (com == "1"){

                    $("#dSalesF").hide();
                }
                $("#lnksalinv").attr("href", "F_22_Sal/RptSalesInventory.aspx?Type=Report&comcod=" + comcod);
                $("#lnksalinvdt").attr("href", "F_22_Sal/RptRateChart.aspx?Type=Report&comcod=" + comcod);
                $("#lnksolduns").attr("href", "F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod=" + comcod + "&prjcode=");
                $("#lnkdaysal").attr("href", "F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=RptDayWSale&comcod=" + comcod + "&prjcode=");
                $("#lnkmonsal").attr("href", "F_17_Acc/RptAccCollVsClearance.aspx?Type=MonSales&comcod=" + comcod);
                $("#lnkbokinf").attr("href", "F_22_Sal/RptBookingDues.aspx?Type=DuesCollect&comcod=" + comcod);
                $("#lnkcustdinf").attr("href", "F_23_CR/RptCustomerDues.aspx?Type=Report&comcod=" + comcod);
                

                ////Budget
                $("#lnkbgdsum").attr("href", "F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdGrWise&comcod=" + comcod + "&prjcode=");
                $("#lnkbgdres").attr("href", "F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgd&comcod=" + comcod + "&prjcode=");
                $("#lnkbgdvsexp").attr("href", "F_17_Acc/AccFinalReports.aspx?RepType=BE&comcod=" + comcod);
                /////Project
                $("#lnkprjana").attr("href", "F_32_Mis/ProjectAnalysis.aspx?Type=Report&comcod=" + comcod);
                $("#lnkinflation").attr("href", "F_32_Mis/RptPrjCostPerSFT.aspx?Type=RemainingCost&comcod=" + comcod + "&prjcode=");
                ///Project Planning
                $("#lnkcashflw").attr("href", "F_08_PPlan/RptProTarget.aspx?Type=RealFlow&comcod=" + comcod);
                /////Inventory
                $("#lnkisubas").attr("href", "F_12_Inv/RptPrurVarAna.aspx?Type=IssueBasis&comcod=" + comcod);
                $("#lnkprgbas").attr("href", "F_12_Inv/RptPrurVarAna.aspx?Type=StkBasis&comcod=" + comcod);
                $("#hlnkDetails").attr("href", "F_16_Bill/RptBilligSummary.aspx?Date1=" + Date1 + "&Date2=" + Date2);
                $("#hlnksubconbill").attr("href", "F_09_PImp/RptConsConBillStatus.aspx?Type=ConBillSummary&Date1=" + Date1 + "&Date2=" + Date2);
                $("#hlnklandprocurement").attr("href", "F_14_Pro/RptPaymetDueAllPrj.aspx");







                if (comcod.toString().substring(0, 1) == "1") {

                    $("#lblstock").attr("href", "F_16_Bill/RptBilligSummary.aspx?Date1=" + Date1 + "&Date2=" + Date2);
                    $("#lbldues").attr("href", "F_41_GAcc/RptProBillStatus.aspx?Type=Billstatus&prjcode=");

                }
                else {

                    $("#lblstock").attr("href", "F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod=" + comcod + "&prjcode=&Date1=" + Date2);
                    $("#lbldues").attr("href", "F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=" + comcod);



                }



             

                $.ajax({
                    type: "POST",
                    url: "CompanyOverAllReport.aspx/GetAllData",
                    data: '{comcod:"' + comcod + '",  date1: "' + $('#<%=this.txtDateFrom.ClientID%>').val() + '" , date2: "' + $('#<%=this.txtDateto.ClientID%>').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    
                    beforeSend: function () {
                        $(".RealProgressbar").show();
                    },

                    success: function (response) {
                        //console.log(JSON.parse(response.d));
                        var data = response.d;

                        //console.log(data['account']);
                        ExecuteGraph(data);
                    },

                    complete: function (data) {
                        $(".RealProgressbar").hide();
                      
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



        function ExecuteGraph(bgd) {
            try {

                Highcharts.setOptions({
                    lang: {
                        decimalPoint: '.',
                        thousandsSep: ' '
                    }
                });

                var bgddata = JSON.parse(bgd);
                console.log(bgd);

                //Acc Legend
                var accdata = bgddata['account'];
                var armainhead = [];
                for (var i = 0; i < accdata.length; i++) {
                    armainhead[i] = accdata[i]["head"];
                }

                // console.log(accdata);
                var ar1 = '';
                var ar2 = '';
                var row = '';
                //  comcod = <%=this.GetCompCode()%>;

                // Date2 = $('#txtDateto').val();

                $.each(accdata,
                    function (i, item) {
                        ar1 = (item.grp == "A") ? '<a target=_blank href=' + encodeURI('F_17_Acc/LinkRptReciptPayment.aspx?Type=receipt&comcod=' + comcod + '&Date1=' + Date1 + '&Date2=' + Date2 + '&paycode=') + '>'
                            : item.grp == "B" ? '<a target=_blank href=' + encodeURI('F_17_Acc/LinkRptReciptPayment.aspx?Type=payment&comcod=' + comcod + '&Date1=' + Date1 + '&Date2=' + Date2 + '&paycode=') + '>'
                                : '';
                        ar2 = (item.grp == "A") || (item.grp == "B") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + ar1 + item.head + ar2 + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#AccTable tbody").html(row);
                    });


                //Inventory Legend
                var invdata = bgddata['inventory'];
                var invhead = [];
                for (var i = 0; i < invdata.length; i++) {
                    invhead[i] = invdata[i]["head"];
                }
                //var ar1 = '';
                //var ar2 = '';
                var row = '';
                //  comcod = <%=this.GetCompCode()%>;

                // Date2 = $('#txtDateto').val();

                $.each(invdata,
                    function (i, item) {
                        //ar1 = (item.grp == "A")? '<a target=_blank href=' + encodeURI('F_17_Acc/LinkRptReciptPayment.aspx?Type=receipt&comcod=' + comcod + '&Date1=' +Date1 + '&Date2=' + Date2+'&paycode=') + '>'
                        //    : item.grp == "B"? '<a target=_blank href=' + encodeURI('F_17_Acc/LinkRptReciptPayment.aspx?Type=payment&comcod=' + comcod + '&Date1=' +Date1 + '&Date2=' + Date2+'&paycode=') + '>'
                        //    : '';
                        //ar2 = (item.grp == "A") || (item.grp == "B") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#InvenTable tbody").html(row);
                    });


                //Sales Legend
                var salesdata = bgddata['sales'];

                var saleshead = [];
                for (var i = 0; i < salesdata.length; i++) {
                    saleshead[i] = salesdata[i]["head"];
                }

                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(salesdata,
                    function (i, item) {
                        ar1 = (item.gcod == "01001")
                            ? '<a target=_blank href=' + encodeURI('F_22_Sal/RptSaleMonYear.aspx?Type=Report&comcod=' + comcod + '&Date1=' + Date1 + '&Date2=' + Date2) + '>'
                            : item.gcod == "01002"
                                ? '<a target=_blank href=' + encodeURI('F_22_Sal/RptCollMonYear.aspx?Type=Report&comcod=' + comcod + '&Date1=' + Date1 + '&Date2=' + Date2) + '>'
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
                        $("#sales tbody").html(row);
                    });

                //Purchase Legend
                var purchasedata = bgddata['purchase'];
                var purchasehead = [];
                for (var i = 0; i < purchasedata.length; i++) {
                    purchasehead[i] = purchasedata[i]["head"];
                }

                var paycode = '2600000000';
                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(purchasedata,
                    function (i, item) {
                        ar1 = (item.gcod == "01001")
                            ? '<a target=_blank href=' + encodeURI('F_14_Pro/PurSumMatWise.aspx?Type=Report' + '&comcod=' + comcod + '&Date1=' + Date1 + '&Date2=' + Date2) + '&prjcode=' +'>'
                            : item.gcod == "01002" ? '<a target=_blank href=' + encodeURI('F_17_Acc/LinkRptReciptPayment.aspx?Type=payment&comcod=' + comcod + '&Date1=' + Date1 + '&Date2=' + Date2 + '&paycode=' + paycode) + '>'
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

                //SubCont Legend
                var subcontractordata = bgddata['subcontractor'];
                var subcontractorhead = [];
                for (var i = 0; i < subcontractordata.length; i++) {
                    subcontractorhead[i] = subcontractordata[i]["head"];
                }

                var paycode = '2600000000';
                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(subcontractordata,
                    function (i, item) {
                        ar1 = (item.gcod == "01001")
                            ? '<a target=_blank href=' + encodeURI('F_14_Pro/PurSumMatWise.aspx?Type=Report' + '&comcod=' + comcod + '&Date1=' + Date1 + '&Date2=' + Date2) + '>'
                            : item.gcod == "01002" ? '<a target=_blank href=' + encodeURI('F_17_Acc/LinkRptReciptPayment.aspx?Type=payment&comcod=' + comcod + '&Date1=' + Date1 + '&Date2=' + Date2 + '&paycode=' + paycode) + '>'
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
                        $("#subcontractor tbody").html(row);
                    });



                var temp = comcod.toString();
                var com = temp.slice(0, 1);
                var landdata = bgddata['landpro'];
                //console.log(landdata);
                var landhead = [];
                for (var i = 0; i < landdata.length; i++) {
                    landhead[i] = landdata[i]["head"];
                }


                var consdata = bgddata['construction'];
                var conshead = [];
                for (var i = 0; i < consdata.length; i++) {
                    conshead[i] = consdata[i]["head"];
                }

                //alert(com);
                if (com == "2") {
                    var row = '';
                    $.each(landdata,
                        function (i, key) {
                            //console.log(item.head);
                            row += "<tr>";
                            row += "<td>" + key.head + "</td>";
                            row += "<td style=text-align:right;>" +
                                ((key.amount == 0)
                                    ? ''
                                    : (key.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                                "</td>";
                            row += "<td style=text-align:right;>" +
                                ((key.peramt == 0) ? '' : (key.peramt.toFixed(2)).toLocaleString('en-US')) +
                                "</td>";
                            row += "</tr>";
                            $("#construction tbody").html(row);
                            //$("#construction").find("tbody tr").remove();
                        });
                }


                else {
                    //Cons Legend

                    var ar1 = '';
                    var ar2 = '';
                    var row = '';

                    $.each(consdata,
                        function (i, item) {
                            ar1 = (item.gcod == "01001")
                                ? '<a target=_blank href=' + encodeURI('F_32_Mis/RptConstruProgressSum.aspx?Type=Report&comcod=' + comcod) + '>'
                                : item.gcod == "01002"
                                    ? '<a target=_blank href=' +
                                    encodeURI('F_09_PImp/RptImpExeStatus.aspx?Type=DayWiseExecution&comcod=' + comcod + '&prjcode=&Date1=' + Date1 + '&Date2=' + Date2) +
                                    '>'
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
                            $("#construction tbody").html(row);
                        });

                }



                //Bank Legend
                var bankdata = bgddata['bankbalance'];
                var bankhead = [];
                for (var i = 0; i < bankdata.length; i++) {
                    bankhead[i] = bankdata[i]["head"];
                }
                var row = '';
                $.each(bankdata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#cashbankbal tbody").html(row);
                    });
                //Stock Legend
                var stockdata = bgddata['stock'];
                var stockhead = [];
                for (var i = 0; i < stockdata.length; i++) {
                    stockhead[i] = stockdata[i]["head"];
                }

                var row = '';
                $.each(stockdata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#stocktable tbody").html(row);
                    });



                //Dues Over Dues
                var duesdata = bgddata['dues'];
                var dueshead = [];
                for (var i = 0; i < duesdata.length; i++) {
                    dueshead[i] = duesdata[i]["head"];
                }

                var row = '';
                $.each(duesdata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#duestable tbody").html(row);
                    });



                //Pending Bill Legend
                var penbildata = bgddata['penbil'];
                var penbilhead = [];
                for (var i = 0; i < penbildata.length; i++) {
                    penbilhead[i] = penbildata[i]["head"];
                }
                var row = '';
                $.each(penbildata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#penbillpart tbody").html(row);
                    });




                //Future Fund
                var ffunddata = bgddata['ffund'];
                var ffundhead = [];
                for (var i = 0; i < ffunddata.length; i++) {
                    ffundhead[i] = ffunddata[i]["head"];
                }



                var row = '';
                $.each(ffunddata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#futuretable tbody").html(row);
                    });

                //Construction Progress
                var conprodata = bgddata['conprogress'];
                var conprohead = [];
                for (var i = 0; i < conprodata.length; i++) {
                    conprohead[i] = conprodata[i]["head"];
                }

                var row = '';
                $.each(conprodata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#prjrpttable tbody").html(row);
                    });

                //Ratio Analysis
                //var ratiodata = bgddata['ratio'];

                //var row = '';
                //$.each(ratiodata,
                //    function (i, item) {

                //        ar1 = (item.gcod == "01")
                //            ? '<a target=_blank href=' + encodeURI('F_32_Mis/RatioAnaWithGraph.aspx?grp=01&comcod=' + comcod) + '>'
                //            : item.gcod == "02"
                //                ? '<a target=_blank href=' + encodeURI('F_32_Mis/RatioAnaWithGraph.aspx?grp=02&comcod=' + comcod) + '>'
                //                : item.gcod == "03"
                //                    ? '<a target=_blank href=' + encodeURI('F_32_Mis/RatioAnaWithGraph.aspx?grp=03&comcod=' + comcod) + '>'
                //                    : item.gcod == "04"
                //                        ? '<a target=_blank href=' + encodeURI('F_32_Mis/RatioAnaWithGraph.aspx?grp=04&comcod=' + comcod) + '>'
                //                        : item.gcod == "05"
                //                            ? '<a target=_blank href=' + encodeURI('F_32_Mis/RatioAnaWithGraph.aspx?grp=05&comcod=' + comcod) + '>'
                //                            : '';
                //        ar2 = (item.gcod == "01") || (item.gcod == "02") || (item.gcod == "03") || (item.gcod == "04") || (item.gcod == "05") ? '</a>' : '';
                //        row += "<tr>";
                //        row += "<td style='padding-left:3px;'>" + item.gcod + "</td>";
                //        row += "<td style='padding-left:5px;'>" + ar1 + item.head + ar2 + "</td>";

                //        row += "</tr>";
                //        $("#ratiotable tbody").html(row);
                //    });
                //Construction Progress
                var prjrptdata = bgddata['prjrpt'];
                var prjrpthead = [];
                for (var i = 0; i < prjrptdata.length; i++) {
                    prjrpthead[i] = prjrptdata[i]["head"];
                }

                var row = '';
                $.each(prjrptdata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#prjrpttabl tbody").html(row);
                    });

                //Future Cost 
                var fcostdata = bgddata['fcost'];
                var fcosthead = [];
                for (var i = 0; i < fcostdata.length; i++) {
                    fcosthead[i] = fcostdata[i]["head"];
                }
                var row = '';
                $.each(fcostdata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#costtable tbody").html(row);
                    });

                //Future Fund Vs Cost 
                var fvscostdata = bgddata['fundcost'];
                var fvscosthead = [];
                for (var i = 0; i < fvscostdata.length; i++) {
                    fvscosthead[i] = fvscostdata[i]["head"];
                }
                var row = '';
                $.each(fvscostdata,
                    function (i, item) {

                        row += "<tr>";
                        row += "<td>" + item.head + "</td>";
                        row += "<td style=text-align:right;>" + ((item.amount == 0) ? '' : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) + "</td>";
                        row += "<td style=text-align:right;>" + ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) + "</td>";
                        row += "</tr>";
                        $("#funvscosttable tbody").html(row);
                    });

                //Accounts
                Highcharts.chart('Accountsdt',
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
                                formatter: function () {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in accdata) {
                                            if (accdata.hasOwnProperty(key)) {
                                                data.push([
                                                    accdata[key].head,
                                                    accdata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });
                ///Inventory Graph 

                Highcharts.chart('Inventorydt',
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
                                formatter: function () {
                                    if ($.inArray(this.value, invhead) !== -1) {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in invdata) {
                                            if (invdata.hasOwnProperty(key)) {
                                                data.push([
                                                    invdata[key].head,
                                                    invdata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });
                //sales
                Highcharts.chart('Salesdt',
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
                                formatter: function () {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in salesdata) {
                                            if (salesdata.hasOwnProperty(key)) {
                                                data.push([
                                                    salesdata[key].head,
                                                    salesdata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
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
                                formatter: function () {
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
                                    (function () {
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
                //SubContractor
                Highcharts.chart('scontractordt',
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
                                formatter: function () {
                                    if ($.inArray(this.value, subcontractorhead) !== -1) {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in subcontractordata) {
                                            if (subcontractordata.hasOwnProperty(key)) {
                                                data.push([
                                                    subcontractordata[key].head,
                                                    subcontractordata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });


                if (com == "2") {
                    //Construction
                    Highcharts.chart('Consdt',
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
                                    formatter: function () {
                                        if ($.inArray(this.value, landhead) !== -1) {
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
                                        (function () {
                                            // generate an array of random data
                                            var data = [],

                                                i;

                                            for (var key in landdata) {
                                                if (landdata.hasOwnProperty(key)) {
                                                    data.push([
                                                        landdata[key].head,
                                                        landdata[key].amount, false
                                                    ]);
                                                }
                                            }
                                            return data;
                                        }())
                                }
                            ]
                        });
                }
                else {
                    //Construction
                    Highcharts.chart('Consdt',
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
                                    formatter: function () {
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
                                        (function () {
                                            // generate an array of random data
                                            var data = [],

                                                i;

                                            for (var key in consdata) {
                                                if (consdata.hasOwnProperty(key)) {
                                                    data.push([
                                                        consdata[key].head,
                                                        consdata[key].amount, false
                                                    ]);
                                                }
                                            }
                                            return data;
                                        }())
                                }
                            ]
                        });
                }


                //Bank balance
                Highcharts.chart('bbalancedt',
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
                                formatter: function () {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in bankdata) {
                                            if (bankdata.hasOwnProperty(key)) {
                                                data.push([
                                                    bankdata[key].head,
                                                    bankdata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });

                //Stock
                Highcharts.chart('stockdt',
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
                                formatter: function () {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in stockdata) {
                                            if (stockdata.hasOwnProperty(key)) {
                                                data.push([
                                                    stockdata[key].head,
                                                    stockdata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });

                //Dues Over Dues
                Highcharts.chart('duesdt',
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
                                formatter: function () {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in duesdata) {
                                            if (duesdata.hasOwnProperty(key)) {
                                                data.push([
                                                    duesdata[key].head,
                                                    duesdata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });

                //Pending Bill
                Highcharts.chart('billdt',
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
                                formatter: function () {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in penbildata) {
                                            if (penbildata.hasOwnProperty(key)) {
                                                data.push([
                                                    penbildata[key].head,
                                                    penbildata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });

                //Man Power
                Highcharts.chart('ffunddt',
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
                                formatter: function () {
                                    if ($.inArray(this.value, ffundhead) !== -1) {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in ffunddata) {
                                            if (ffunddata.hasOwnProperty(key)) {
                                                data.push([
                                                    ffunddata[key].head,
                                                    ffunddata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });

                //Construction Progress
                Highcharts.chart('conprodt',
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
                                formatter: function () {
                                    if ($.inArray(this.value, conprohead) !== -1) {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in conprodata) {
                                            if (conprodata.hasOwnProperty(key)) {
                                                data.push([
                                                    conprodata[key].head,
                                                    conprodata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });
                //PRoject Report
                Highcharts.chart('conprod',
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
                                formatter: function () {
                                    if ($.inArray(this.value, prjrpthead) !== -1) {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in prjrptdata) {
                                            if (prjrptdata.hasOwnProperty(key)) {
                                                data.push([
                                                    prjrptdata[key].head,
                                                    prjrptdata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });
                ///Future Cost
                Highcharts.chart('fcostdt',
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
                                formatter: function () {
                                    if ($.inArray(this.value, fcosthead) !== -1) {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in fcostdata) {
                                            if (fcostdata.hasOwnProperty(key)) {
                                                data.push([
                                                    fcostdata[key].head,
                                                    fcostdata[key].amount, false
                                                ]);
                                            }
                                        }
                                        return data;
                                    }())
                            }
                        ]
                    });

                //Fund Vs Cost
                Highcharts.chart('fundcstdt',
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
                                formatter: function () {
                                    if ($.inArray(this.value, fvscosthead) !== -1) {
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
                                    (function () {
                                        // generate an array of random data
                                        var data = [],

                                            i;

                                        for (var key in fvscostdata) {
                                            if (fvscostdata.hasOwnProperty(key)) {
                                                data.push([
                                                    fvscostdata[key].head,
                                                    fvscostdata[key].amount, false
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


    <div class="RealProgressbar">

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

                    </div>


    <div class="card card-fluid">
        <div class="card-body">
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">

                        <asp:Label ID="lblDate" runat="server" CssClass="control-label" Text="From"></asp:Label>
                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="form-control"
                            ClientIDMode="Static"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>

                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lbltoDate" runat="server" CssClass="control-label" Text="To"></asp:Label>
                        <asp:TextBox ID="txtDateto" ClientIDMode="Static" runat="server" AutoCompleteType="Disabled" CssClass="form-control" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <input type="button" id="btnok" style="margin-top: 20px;" class=" btn btn-primary" onclick="GetData();" value="Ok" />



                        <%--<asp:LinkButton ID="btnok" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-primary" OnClientClick="GetData();">OK</asp:LinkButton>--%>
                        <%--<button id="btnok"  Style="margin-top: 20px;"  class=" btn btn-primary" onclick="GetData();">Ok</button>--%>
                    </div>
                </div>

                <div class="col-md-2">
                    <a style="margin-top: 20px;" href="<%=this.ResolveUrl ("~/AllGraph.aspx")%>" target="_blank" class="btn btn-block btn-success">View Graph <span class="glyphicon glyphicon-dashboard"></span></a>
                </div>
                <div class="col-md-4">
                    <%--<div class="col-md-3 pull-left">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                            AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass="lblProgressBar" Text="Please wait . . . . . . ."
                                                    Width="218px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>--%>
                    <asp:Label runat="server" class="btn btn-block btn-deafult text-danger" Style="font-size: 14px; font-family: sans-serif; margin-top: 20px;">All Value show Taka In Lac</asp:Label>

                </div>
             

            </div>





        </div>
    </div>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           




            <div class="card card-fluid">

                <div class="card-header">
                    <!-- .nav-tabs -->
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item" id="tb_Budget">
                            <a class="nav-link" data-toggle="tab" id="aBudgettab" href="#Budgettab">Budget</a>
                        </li>
                        <li class="nav-item" id="tb_purchasetab">
                            <a class="nav-link show " data-toggle="tab" id="aProcurement" href="#purchasetab">Procurement</a>
                        </li>
                        <li class="nav-item" id="tb_subCon">
                            <a class="nav-link show " data-toggle="tab" id="aSubconsttab" href="#subCon">Sub-Contractor</a>
                        </li>
                        <li class="nav-item" id="tb_consttab">
                            <a class="nav-link show" data-toggle="tab" id="aconsttab" href="#consttab">Construction</a>
                        </li>
                        <li class="nav-item" id="tb_invtab">
                            <a class="nav-link show" data-toggle="tab" href="#invtab">Inventory</a>
                        </li>
                        <li class="nav-item" id="tb_PrjReptab">
                            <a class="nav-link show" data-toggle="tab" href="#PrjReptab">Project</a>
                        </li>

                        <li class="nav-item" id="tb_Salestab">
                            <a class="nav-link show" data-toggle="tab" id="aSales" href="#Salestab">Sales</a>
                        </li>
                        <li class="nav-item" id="tb_Stocktab">
                            <a class="nav-link show" data-toggle="tab" href="#Stocktab">Stock</a>
                        </li>

                        <li class="nav-item" id="tb_Accountstab">
                            <a class="nav-link show" data-toggle="tab" id="aAccounts" href="#Accountstab">Accounts</a>
                        </li>
                        <li class="nav-item" id="tb_cashbalancetab">
                            <a class="nav-link show" data-toggle="tab" href="#cashbalancetab">Cash & Bank</a>
                        </li>
                        <li class="nav-item" id="tb_PendingPaytab">
                            <a class="nav-link show" data-toggle="tab" href="#PendingPaytab">Pending Pay.</a>
                        </li>

                        <li class="nav-item" id="tb_DueOduestab">
                            <a class="nav-link show" data-toggle="tab" href="#DueOduestab">Dues</a>
                        </li>
                        <li class="nav-item" id="tb_FutureFundtab">
                            <a class="nav-link show" data-toggle="tab" href="#FutureFundtab">Future Fund</a>
                        </li>
                        <li class="nav-item" id="tb_FutureCsttab">
                            <a class="nav-link show" data-toggle="tab" href="#FutureCsttab">Future Cost</a>
                        </li>
                        <li class="nav-item" id="tb_FuturefundvsCsttab">
                            <a class="nav-link show" data-toggle="tab" href="#FuturefundvsCsttab">Fund VS Cost</a>
                        </li>


                        <%--  <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#RatioAnatab">Ratio Analysis</a>
                        </li>--%>
                    </ul>
                    <!-- /.nav-tabs -->
                </div>

                <div class="card-body">
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane fade" id="Budgettab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblbudget" Style="font-size: 16px; color: #70737c; font-weight: bold;">Budget</asp:HyperLink>

                                    <table id="prjrpttable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=04")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    
                                </div>
                                <div class="col-md-7">
                                    <div id="conprodt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="Salestab">
                            <div class="row">

                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ClientIDMode="Static" ID="lblsales">Sales Details</asp:HyperLink>

                                    <table id="sales" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_34_Mgt/RptAllDashboard?Type=Sales")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=22")%>" class="btn btn-primary">Module</a>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div id="Salesdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="purchasetab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Visible="False" Target="_blank" ID="lblpurchase" Style="font-size: 16px; color: #70737c; font-weight: bold;" ClientIDMode="Static">Procurement Details</asp:HyperLink>

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
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_34_Mgt/RptAllDashboard?Type=Purchase")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=14")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="purchasedt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="consttab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Visible="False" Target="_blank" ID="lblcons" Style="font-size: 16px; color: #70737c; font-weight: bold;" ClientIDMode="Static"></asp:HyperLink>

                                    <table id="construction" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_34_Mgt/RptAllDashboard?Type=Construction")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=08")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="Consdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>


                        <div class="tab-pane fade" id="invtab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Visible="False" ClientIDMode="Static" Target="_blank" ID="hlnkinventory" Style="font-size: 16px; color: #70737c; font-weight: bold;">Inventory Details</asp:HyperLink>

                                    <table id="InvenTable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/InventoryInterface.aspx")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=12")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="Inventorydt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="Accountstab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblaccount" Style="font-size: 16px; color: #70737c; font-weight: bold;">Accounts Details</asp:HyperLink>

                                    <table id="AccTable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_34_Mgt/RptAllDashboard?Type=Accounts")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=17")%>" class="btn btn-primary">Module</a>
                                        <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                            <button type="button" class="btn btn-danger">Ratio Analysis</button>
                                            <div class="btn-group" role="group">
                                                <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                                    <div class="dropdown-arrow"></div>
                                                    <asp:HyperLink ID="lnkratio1" ClientIDMode="Static" runat="server" Target="_blank" CssClass="dropdown-item">Liquidety Ratio</asp:HyperLink>
                                                    <asp:HyperLink ID="lnkratio2" ClientIDMode="Static" runat="server" Target="_blank" CssClass="dropdown-item">Financial Leveragde</asp:HyperLink>
                                                    <asp:HyperLink ID="lnkratio3" ClientIDMode="Static" runat="server" Target="_blank" CssClass="dropdown-item">Profitability Ratio</asp:HyperLink>
                                                    <asp:HyperLink ID="lnkratio4" ClientIDMode="Static" runat="server" Target="_blank" CssClass="dropdown-item">Dividend Policey</asp:HyperLink>
                                                    <asp:HyperLink ID="lnkratio5" ClientIDMode="Static" runat="server" Target="_blank" CssClass="dropdown-item">Turn Over Ratio</asp:HyperLink>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-7">
                                    <div id="Accountsdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="cashbalancetab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblbbalance" Style="font-size: 16px; color: #70737c; font-weight: bold;">Cash & Bank Balance Details</asp:HyperLink>

                                    <table id="cashbankbal" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=17")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="bbalancedt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="PendingPaytab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblbill" Style="font-size: 16px; color: #70737c; font-weight: bold;">Pending Payments Details</asp:HyperLink>

                                    <table id="penbillpart" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=17")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="billdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="Stocktab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblstock" Style="font-size: 16px; color: #70737c; font-weight: bold;">Stock Details</asp:HyperLink>

                                    <table id="stocktable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~/")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=23")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="stockdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="DueOduestab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lbldues" Style="font-size: 16px; color: #70737c; font-weight: bold;">Dues Over Dues Details</asp:HyperLink>

                                    <table id="duestable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~/")%>" class="btn btn-danger">Analysis Graph</a>--%>

                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=23")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="duesdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="FutureFundtab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblmanpower" Style="font-size: 16px; color: #70737c; font-weight: bold;">Future Fund Details</asp:HyperLink>

                                    <table id="futuretable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=23")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="ffunddt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="FutureCsttab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblfcost" Style="font-size: 16px; color: #70737c; font-weight: bold;">Future Cost Details</asp:HyperLink>

                                    <table id="costtable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=23")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="fcostdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                        <div class="tab-pane fade" id="FuturefundvsCsttab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblfunvscost" Style="font-size: 16px; color: #70737c; font-weight: bold;">Future Fund VS Cost Details</asp:HyperLink>

                                    <table id="funvscosttable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=23")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="fundcstdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>



                        <div class="tab-pane fade" id="PrjReptab">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" ClientIDMode="Static" Visible="False" Target="_blank" ID="lblsalary" Style="font-size: 16px; color: #70737c; font-weight: bold;">Project Report Details</asp:HyperLink>

                                    <table id="prjrpttabl" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_32_Mis/ProjectAnalysis?Type=Report&comcod=")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=04")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary.aspx" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="conprod" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>


                        <div class="tab-pane fade" id="subCon">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Visible="False" Target="_blank" ID="lblscon" Style="font-size: 16px; color: #70737c; font-weight: bold;" ClientIDMode="Static">Sub-Contractor Details</asp:HyperLink>

                                    <table id="subcontractor" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report")%>" class="btn btn-primary">Interfaces</a>
                                        <%--<a target="_blank" href="<%=this.ResolveUrl("~")%>" class="btn btn-danger">Analysis Graph</a>--%>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew.aspx?moduleid=14")%>" class="btn btn-primary">Module</a>
                                    </div>

                                </div>
                                <div class="col-md-7">
                                    <div id="scontractordt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>
                        


                        

                    </div>
                    <div class="footer row" style="margin-top:150px">
                            <div class="splitter">
                               
                            </div>
                          
                            <div class="container-fluid" >
                                <div class="row">
                                    <%--   <div class="col-sm-1 col-md-1 col-lg-1"></div>--%>
                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div style="margin-left: 15px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Budget</h4>
                                            <ul class="Menulinks">
                                                <li><a id="lnkbgdsum" target="_blank">Income Statement-Summary</a></li>
                                                <li><a id="lnkbgdres" target="_blank">Income Statement-Resource</a></li>
                                                <li><a id="lnkbgdvsexp" target="_blank">Budget VS Expenses</a></li>
                                            </ul>
                                        </div>
                                        <div style="margin-left: 15px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Project</h4>
                                            <ul class="Menulinks">
                                                <li><a id="lnkprjana" target="_blank">Project Analysis</a></li>
                                                <li><a id="lnkinflation" target="_blank">Inflation</a></li>
                                            </ul>
                                        </div>
                                        <br />


                                    </div>

                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div style="margin-left: 10px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Purchase</h4>
                                            <ul class="Menulinks">
                                                <li>
                                                    <a id="lnkdaywpur" target="_blank">Day Wise Purchase </a>
                                                </li>
                                                <%-- <li><a id="lnkpursum" target="_blank">Purchae Summary With Opening</a></li>--%>
                                                <li><a id="lnkpurhis" target="_blank">History -Material Wise </a></li>
                                                <li><a id="lnkpurhissup" target="_blank">History -Supplier Wise</a></li>
                                                <li><a id="lnkratev" target="_blank">Rate Variance -Materials</a></li>
                                                <li><a id="lnkmatdel" target="_blank">Material Delivery Efficiency</a></li>
                                                <li><a id="lnksupoveral" target="_blank">Supplier Overall position </a></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3" id="dSalesF">
                                        <div style="margin-left: 20px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Sales </h4>
                                            <ul class="Menulinks">
                                                <li><a id="lnksalinv" target="_blank">Sales Inventory (Summary) </a></li>
                                                <li><a id="lnksalinvdt" target="_blank">Sales Inventory (Details) </a></li>
                                                <li><a id="lnksolduns" target="_blank">Sold & Unsold Information </a></li>
                                                <li><a id="lnkdaysal" target="_blank">Day Wise Sales </a></li>
                                                <li><a id="lnkmonsal" target="_blank">Month Wise Sales </a></li>
                                                <li><a id="lnkbokinf" target="_blank">Booking Dues</a></li>
                                                <li><a id="lnkcustdinf" target="_blank">Customer Dues Information</a></li>

                                            </ul>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 col-md-2 col-lg-3">
                                        <div style="margin-left: 25px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Accounts</h4>
                                            <ul class="Menulinks">
                                                <li>
                                                    <a id="lnktrialbal" target="_blank">Trial Balance</a>
                                                </li>

                                                <li>
                                                    <a id="lnkfnst" target="_blank">Financial Statement</a>
                                                </li>
                                                <li><a id="lnkincst" target="_blank">Income Statement Project</a></li>
                                                <li><a id="lnkprjres" target="_blank">Project Resource</a></li>
                                                <%-- <li><a id="lnkrationana" target="_blank">Ratio Analysis</a></li>--%>
                                                <li><a id="lnkpayable" target="_blank">Payable</a></li>
                                                <li><a id="lnkrecv" target="_blank" hr>Receivable</a></li>
                                                <%--<li><a id="lnkoverall" target="_blank">Overall</a></li>--%>
                                            </ul>

                                        </div>

                                    </div>

                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div style="margin-left: -30px;" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">CRM</h4>
                                            <ul class="Menulinks">
                                                <li><a href="<%=this.ResolveUrl("TestScrollDatat.aspx")%>" target="_blank">On Scrolling Data Bind</a></li>
                                                <li><a href="<%=this.ResolveUrl("CalenderTest.aspx")%>" target="_blank">Calender Test</a></li>

                                            </ul>
                                        </div>
                                        <br />

                                        <div style="margin-left: -30px;" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Inventory</h4>
                                            <ul class="Menulinks">
                                                <li><a id="lnkisubas" target="_blank">Inventory -Issue Basis</a></li>
                                                <li><a id="lnkprgbas" target="_blank">Inventory -Progress Basis </a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/RptInvResourceConsum.aspx")%>" target="_blank">Material Comsuption</a></li>

                                            </ul>
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






