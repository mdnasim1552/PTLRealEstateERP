function LoadInfo() {
    StartProgressBar();
    $.ajax({

        type: "POST",
        async: true,
        url: "../ASMX_F_45_GrAcc/RptGrpMisDailyActiviteis.asmx/GetRptGrpMisDailyActiviteis",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: Sys.Serialization.JavaScriptSerializer.serialize({ 'comp1': comnams, 'frdate': sdate, 'endDate': endDate }),
        success: function onSuccess(data) {
            
            displayTable(data);
            //alert('success');
            ShowLabels();
            $("#pb").hide();

        }

    });

}

function HideLabels() {
    $('#lblSales').hide();//A
    $('#lblCollectionStatus').hide();//B
    $('#lblChequeInHand').hide();//C
    $('#lblRecAPayEncash').hide();//D
    $('#lblBankPosition').hide();//E
    $('#lblRecAPayIssue').hide();//F
    $('#lblPDCCheque').hide();//G
    $('#lblProcurement').hide();//H
    $('#lblConstruction').hide();//I
    $('#lblFeasibility').hide();//J
    $('#lblStock').hide();//M
    $('#lblProjectStatus').hide();//N
    $('#lblMonProStatus').hide();//O
    $('#lblInventorystock').hide();//P
    $('#lblMarketing').hide();//Q
    $('#lblFixedAssets').hide();//R
    $('#lblFinancialst').hide();//S
    $('#lblHrMgt').hide();//T

}
function ShowLabels() {
    $('#lblSales').show();//A
    $('#lblCollectionStatus').show();//B
    $('#lblChequeInHand').show();//C
    $('#lblRecAPayEncash').show();//D
    $('#lblBankPosition').show();//E
    $('#lblRecAPayIssue').show();//F
    $('#lblPDCCheque').show();//G
    $('#lblProcurement').show();//H
    $('#lblConstruction').show();//I
    $('#lblFeasibility').show();//J
    $('#lblStock').show();//M
    $('#lblProjectStatus').show();//N
    $('#lblMonProStatus').show();//O
    $('#lblInventorystock').show();//P
    $('#lblMarketing').show();//Q
    $('#lblFixedAssets').show();//R
    $('#lblFinancialst').show();//S
    $('#lblHrMgt').show();//T
}

function StartProgressBar() {
    $("#pb").show();
    $("#pb").progressbar({ value: 100 });
    IndeterminateProgressBar($("#pb"));
    
}
function IndeterminateProgressBar() {
    $("#pb").css({ "padding-left": "0%", "padding-right": "20%" });
    $("#pb").progressbar("option", "value", 100);
    $("#pb").animate({ paddingLeft: "20%", paddingRight: "0%" }, 3000, "linear",
        function () { IndeterminateProgressBar(pb); });
}

function displayTable(data) {

    var tbJUnqueComNams=new Array();

    ReInitTbl();
    var tbindex = [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1];//array to hold index no of each table
    var hrcomcod = '';

    //A-HEAD
    $("#gvDayWSale").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;"+">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">CAPACITY</br>AVAIALABLE</th>"
                + "<th style=width:100px;" + ">TARGET AS</br> PER YEARLY PLAN</th>"
                + "<th style=width:100px;" + ">BREAK-</br>EVEN</th>"
                 + "<th style=width:100px;" + ">MONTHLY</br> TARGET</th>"
                + "<th style=width:100px;" + ">MONTHLY</br> TARGET</br> AS ON</br> TODAY</th>"
                + "<th style=width:100px;" + ">ACTUAL SALES</th>"
                + "<th style=width:100px;" + ">ACHIEVED</br> IN %</th>"
                + "<th style=width:100px;" + ">GRAPH</th>"
        + "</tr></thead>");

    //B-HEAD
    $("#gvrcoll").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">CAPACITY</br>AVAIALABLE</th>"
                + "<th style=width:100px;" + ">TARGET AS</br> PER YEARLY PLAN</th>"
                + "<th style=width:100px;" + ">BREAK-</br>EVEN</th>"
                 + "<th style=width:100px;" + ">MONTHLY</br> TARGET</th>"
                + "<th style=width:100px;" + ">MONTHLY</br> TARGET</br> AS ON</br> TODAY</th>"
                + "<th style=width:100px;" + ">ACHIEVEMENT</th>"
                + "<th style=width:100px;" + ">ACHIEVED</br> IN %</th>"
                + "<th style=width:100px;" + ">GRAPH</th>"
        + "</tr></thead>");


    //C-HEAD
    $("#gvchequeinhand").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">IN HAND</br>(RETURNED</br> CHEQUE)</th>"
                + "<th style=width:100px;" + ">IN HAND</br>(FRESH</br> CHEQUE)</th>"
                + "<th style=width:100px;" + ">IN HAND</br>(POST</br>DATED</br> CHEQUE)</th>"
                + "<th style=width:100px;" + ">TOTAL</br> TARGET</th>"
        + "</tr></thead>");

    
    //D-HEAD
    $("#gvarecandpay").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY </th>"
                + "<th style=width:100px;" + ">RECEIPT AMT.</th>"
                + "<th style=width:100px;" + ">PAYMENT AMT.</th>"
                + "<th style=width:100px;" + ">BALANCE AMT.</th>"
        + "</tr></thead>");

    //E-HEAD
    $("#gvBankPosition").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">DESCRIPTION </th>"
                + "<th style=width:100px;" + ">BANK</br>BALANCE</th>"
                + "<th style=width:100px;" + ">BANK</br>LIABILITIES</th>"
                + "<th style=width:100px;" + ">AVA.</br>LOAN</br>LIMIT</th>"
        + "</tr></thead>");

    //F-HEAD
    $("#gvarecandpayis").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY </th>"
                + "<th style=width:100px;" + ">RECEIPT AMT.</th>"
                + "<th style=width:100px;" + ">PAYMENT</br> AMT.</th>"
                + "<th style=width:100px;" + ">BALANCE</br>AMT.</th>"
               
        + "</tr></thead>");
    //G-HEAD
    $("#gvpdc").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY </th>"
                + "<th style=width:100px;" + ">RECEIPT AMT.</th>"
                + "<th style=width:100px;" + ">PAYMENT</br> AMT.</th>"
                + "<th style=width:100px;" + ">BALANCE</br>AMT.</th>"

        + "</tr></thead>");

    //H-HEAD
    $("#gvprocure").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">MATERIAL</br> NOT</br> YET</br> RECEIVED</th>"
                + "<th style=width:100px;" + ">MATERIAL</br>RECEIVED</th>"
                + "<th style=width:100px;" + ">BILL</br> COMPLETED</th>"
                + "<th style=width:100px;" + ">PURCHASE</br>HISTORY</br>MATERIAL</br>WISE</th>"
                + "<th style=width:100px;" + ">PURCHASE</br>HISTORY</br>SUPPLIER</br>WISE</th>"
        + "</tr></thead>");
    //I-HEAD
    $("#gvMMPlanVsAch").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">WORK</br> TARGET</br>  AS PER</br>  MASTER PLAN</br>  (UPTO DATE)</th>"
                + "<th style=width:100px;" + ">MONTHLY </br>WORK</br> TARGET</th>"
                + "<th style=width:100px;" + ">MONTHLY </br> EXECUTION</th>"
                + "<th style=width:100px;" + ">ACHEIVEMENT (%)</br> ON MAS.</br> PLAN</th>"
                + "<th style=width:100px;" + ">ACHEIVEMENT (%)</br> ON MONTHLY</br> PLAN</th>"
                + "<th style=width:100px;" + ">FLOOR</br> WISE</br> PROGRESS</th>"
                + "<th style=width:100px;" + ">SYSTEM</br> GENERATED</br> TARGET</th>"
                + "<th style=width:100px;" + ">INFLATION</br> EFFECT</th>"
        + "</tr></thead>");

    //J-HEAD
    $("#gvFeasibility").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">DESCRIPTION</th>"
                + "<th style=width:100px;" + ">ORIGINAL</th>"
                + "<th style=width:100px;" + ">REVISED</th>"
                + "<th style=width:100px;" + ">CHANGE</th>"
                + "<th style=width:100px;" + ">%</th>"
        + "</tr></thead>");

    //M-HEAD
    $("#gvpstk").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY</th>"
                + "<th style=width:100px;" + ">TOTAL REVENUE</th>"
                + "<th style=width:100px;" + ">UNSOLD AMOUNT</th>"
                + "<th style=width:100px;" + ">SOLD AMOUNT</th>"
                + "<th style=width:100px;" + ">TOTAL RECEIVED</th>"
                + "<th style=width:100px;" + ">RECEIVEABLE</th>"
                + "<th style=width:100px;" + ">NOT DUES</th>"
                + "<th style=width:100px;" + ">PREVIOUS DEUES</th>"
                + "<th style=width:100px;" + ">CURRENT DUES</th>"
                + "<th style=width:100px;" + ">DELAY AND RETURN CHARGE</th>"
        + "</tr></thead>");

    
    //N-HEAD
    $("#gvProjectStatus01").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY</th>"
                + "<th style=width:100px;" + ">SALES</th>"
                + "<th style=width:100px;" + ">TOTAL RECEIVED</th>"
                + "<th style=width:100px;" + ">EXPENCES</th>"
                + "<th style=width:100px;" + ">LIABILITIES</th>"
                + "<th style=width:100px;" + ">NET</br>POSITION</th>"
                + "<th style=width:100px;" + ">PROJECT</br>REPORT</br>WITH</br>QUANTITY</th>"
                + "<th style=width:100px;" + ">PROJECT</br>BUDGET</br>VS.</br>EXPENCES</th>"
                + "<th style=width:100px;" + ">PROJECT</br>TRANSACTION</br>DAY-</br>WISE</th>"
        + "</tr></thead>");

    //O-HEAD
    $("#gvmonprost").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">TOTAL COST</th>"
                + "<th style=width:100px;" + ">TOTAL COLLECTION</th>"
                + "<th style=width:100px;" + ">NET POSITION</th>"
        + "</tr></thead>");

    //P-HEAD
    $("#gvInventory").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">ISSUE BASIS</th>"
                + "<th style=width:100px;" + ">PROGRESS BASIS</th>"
                + "<th style=width:100px;" + ">MATERIAL CONSUMPTION</th>"
        + "</tr></thead>");

    //Q-HEAD
    $("#gvcomwclients").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">DESCRIPTION</th>"
                + "<th style=width:100px;" + ">Sales</br> Demand</br>  Analysis</th>"
                + "<th style=width:100px;" + ">Sales</br> Decision</th>"
                + "<th style=width:100px;" + ">Client</br>Capacity</br> Analysis</th>"
                + "<th style=width:100px;" + ">Client</br> History</th>"
                + "<th style=width:100px;" + ">Sales</br> Person</br> History</th>"
        + "</tr></thead>");

    //R-HEAD
    $("#gvFxtAssets").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">AMOUNT</th>"
        + "</tr></thead>");


    //S-HEAD
    $("#gvFinalcialst").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">INCOME</br> STATEMENT</th>"
                + "<th style=width:100px;" + ">BALANCE</br>SHEET</th>"
                 + "<th style=width:100px;" + ">REAL INFLOW &</br>OUTFLOW</th>"
                + "<th style=width:100px;" + ">REAL</br> PAYMENT</br>SUMMARY</th>"
                + "<th style=width:100px;" + ">INVESTMENT</br>PLAN</th>"
                + "<th style=width:100px;" + ">PROJECT</br>REPORT</br>AT</br>A GLANCE</th>"
        + "</tr></thead>");

    //T-HEAD
    $("#gvHremp").append("<thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th style=width:30px;" + ">SL</th>"
                + "<th style=width:140px;" + ">COMPANY NAME</th>"
                + "<th style=width:100px;" + ">TOTAL</br> EMPLOYEE</th>"
                + "<th style=width:100px;" + ">SALARY</th>"
        + "</tr></thead>");


       for (var i = 0 ; i < data.d.length; i++) {

        if (i >= data.d.length)
            break;

        //A
        if (data.d[i].grp == 'A' && data.d[i].compname != 'Total') {

            var tsaleamt_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Sales&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var salamt_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RptDayWSale&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var graph_NavigateUrl = "../F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + data.d[i].comcod + "&capacity=" + data.d[i].capacity + "&masbgd=" + data.d[i].masbgd + "&bep=" + data.d[i].bep + "&ttargetamt=" + data.d[i].tsaleamt + "&targetamt=" + data.d[i].tosaleamt + "&acsalamt=" + data.d[i].suamt;
            //graph.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + code + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&ttargetamt=" + ttargetamt1 + "&targetamt=" + targetamt1 + "&acsalamt=" + acsalamt1;

            $("#gvDayWSale").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[0]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].capacity) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].masbgd) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].bep) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + tsaleamt_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].tsaleamt) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].tosaleamt) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + salamt_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].suamt) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValueSecondVersion(data.d[i].perotsal) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + graph_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "GRAPH" + "&nbsp" + "</div></a></td>"
                + "</tr></tbody>");
            
            
            capacitySum += data.d[i].capacity;
            masbgdSum += data.d[i].masbgd;
            bepSum += data.d[i].bep;
            tsaleamtSum += data.d[i].tsaleamt;
            tosaleamtSum += data.d[i].tosaleamt;
            suamtSum += data.d[i].suamt;
            perotsalSum += data.d[i].perotsal;
         
        }

        //B
        if (data.d[i].grp == 'B' && data.d[i].compname !='Total') {

            var tcollamt_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Collection&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var achamt_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=CollSummary&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var graph_NavigateUrl = "../F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + data.d[i].comcod + "&capacity=" + data.d[i].capacity + "&masbgd=" + data.d[i].masbgd + "&bep=" + data.d[i].bep + "&ttargetamt=" + data.d[i].tsaleamt + "&targetamt=" + data.d[i].tastcollamt + "&acsalamt=" + data.d[i].acamt;


            $("#gvrcoll").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[1]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].capacity) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].masbgd) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].bep) + "&nbsp" + "</td>"
                        +"<td style=text-align:right;" + "> <a href='" + tcollamt_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].tcollamt) + "&nbsp" + "</div></a></td>"
                       // + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].tcollamt) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].tastcollamt) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + achamt_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].acamt) + "&nbsp" + "</div></a></td>"
                       // + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].acamt) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValueSecondVersion(data.d[i].perotcoll) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + graph_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "GRAPH" + "&nbsp" + "</div></a></td>"
                        //+ "<td style=text-align:right;" + ">" + "GRAPH" + "&nbsp" + "</td>"
                + "</tr></tbody>");


            capacitySumB += data.d[i].capacity;
            masbgdSumB += data.d[i].masbgd;
            bepSumB += data.d[i].bep;
            tcollamtSumB += data.d[i].tcollamt;
            tastcollamtSumB += data.d[i].tastcollamt;
            acamtSumB += data.d[i].acamt;
            perotcollSumB += data.d[i].perotcoll;
            
        }

        //C
        if (data.d[i].grp == 'C' && data.d[i].compname != 'Total') {

            var amount_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=ChequeInHand&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            $("#gvchequeinhand").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[2]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].inhrchq) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].inhfchq) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].inhpchq) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + amount_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].chqinhand) + "&nbsp" + "</div></a></td>"
                        //+ "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].chqinhand) + "&nbsp" + "</td>"
                + "</tr></tbody>");

            inhrchqSum += data.d[i].inhrchq;
            inhfchqSum += data.d[i].inhfchq;
            inhpchqSum += data.d[i].inhpchq;
            chqinhandSum += data.d[i].chqinhand;
        }

        //D
        if (data.d[i].grp == 'D' && data.d[i].compname != 'Total') {

            var Balamt_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RecPay&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;

            $("#gvarecandpay").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[3]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].recpam) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].payam) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + Balamt_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].balam) + "&nbsp" + "</div></a></td>"
                     // + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].balam) + "&nbsp" + "</td>"
                + "</tr></tbody>");

            recpamSum += data.d[i].recpam;
            payamSum += data.d[i].payam;
            balamSum += data.d[i].balam;
        }

        //E
        if (data.d[i].grp == 'E' && data.d[i].compname != 'Total') {
            var bankposition_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=BankPosition&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            $("#gvBankPosition").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[4]++ + ".</td>"
                        + "<td style=text-align:left;" + "> <a href='" + bankposition_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].compname) + "&nbsp" + "</div></a></td>"
                        //+ "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].closbal) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].closlia) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].avloan) + "&nbsp" + "</td>"
                + "</tr></tbody>");

            closbalSum += data.d[i].closbal;
            closliaSum += data.d[i].closlia;
            avloanSum += data.d[i].avloan;
        }

         //F
        if (data.d[i].grp == 'F' && data.d[i].compname != 'Total') {
            var Balamt_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=IssuedVsCollect&comcod="+ data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            //$("#gvBankPosition").append("<tbody><tr>"
            $("#gvarecandpayis").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[5]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].recpam) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].payam) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + Balamt_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].balam) + "&nbsp" + "</div></a></td>"
                       //+ "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].balam) + "&nbsp" + "</td>"
                + "</tr></tbody>");

            recpamSumF += data.d[i].recpam;
            payamSumCF += data.d[i].payam;
            balamSumF += data.d[i].balam;
        }
        //G
        if (data.d[i].grp == 'G' && data.d[i].compname != 'Total') {

            var HLgvDescpaysum_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PDCSummary&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            $("#gvpdc").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[6]++ + ".</td>"
                        + "<td style=text-align:left;" + "> <a href='" + HLgvDescpaysum_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].compname) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].toam) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].dueam) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].pdcam) + "&nbsp" + "</td>"
                + "</tr></tbody>");

            toamSumG += data.d[i].toam;
            payamSumCG += data.d[i].dueam;
            pdcamSumG += data.d[i].pdcam;
        }

        //H
        if (data.d[i].grp == 'H' && data.d[i].compname != 'Total') {

            var hlnkgvreqpro_NavigateUrl = "../F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=PendingStatus&comcod=" + data.d[i].comcod + "&Date=" + endDate;
            var hlnkgvourchasepro_NavigateUrl = "../F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=Purchase&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var hlnkgvbillpro_NavigateUrl = "../F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=ConBill&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var purhmwisepro_NavigateUrl = "../F_45_GrAcc/LinkGrpMatPurHistory.aspx?Type=PurHisMatWise&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var purhswisepro_NavigateUrl = "../F_45_GrAcc/LinkGrpMatPurHistory.aspx?Type=PurHisSupWise&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;

            $("#gvprocure").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[7]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + hlnkgvreqpro_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].reqamt) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + "> <a href='" + hlnkgvourchasepro_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].mrramt) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + "> <a href='" + hlnkgvbillpro_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].billamt) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + "> <a href='" + purhmwisepro_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL PROJECTS " + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + "> <a href='" + purhswisepro_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL PROJECTS " + "&nbsp" + "</div></a></td>"
                + "</tr></tbody>");

            reqamtSumH += data.d[i].reqamt;
            mrramtSumCH += data.d[i].mrramt;
            billamtSumH += data.d[i].billamt;
        }

        //I
        if (data.d[i].grp == 'I' && data.d[i].compname != 'Total') {

            var comname_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MasPVsMonPVsExAllPro&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var flrwiseprogress_NavigateUrl = "../F_45_GrAcc/LinkConstruProgress.aspx?comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var sysgentarget_NavigateUrl = "../F_45_GrAcc/LinkGrpSysGenTarget.aspx?Type=SymGenTar&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var ineffect_NavigateUrl = "../F_45_GrAcc/LinkGrpInflaEffect.aspx?Type=RemainingCost&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;

            $("#gvMMPlanVsAch").append("<tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[8]++ + ".</td>"
                        + "<td style=text-align:left;" + "> <a href='" + comname_NavigateUrl+"'target='_blank'><div style=width:100%;height:100%>" + GetFormattedValue(data.d[i].compname) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].masplan) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].monplan) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].excution) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValueSecondVersion(data.d[i].peromasp) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + GetFormattedValueSecondVersion(data.d[i].peromonp) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + flrwiseprogress_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL PROJECTS  " + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + "> <a href='" + sysgentarget_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL PROJECTS  " + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + "> <a href='" + ineffect_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL PROJECTS  " + "&nbsp" + "</div></a></td>"
                     
                + "</tr></tbody>");

            masplanSumI += data.d[i].masplan;
            monplanSumCI += data.d[i].monplan;
            excutionSumI += data.d[i].excution;
            peromaspSumI += data.d[i].peromasp;
            peromonpSumI += data.d[i].peromonp;
        }
        
           
         //J
        if (data.d[i].grp == 'J' && data.d[i].compname != 'Total') {

            var Feadesc_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=FeaAllProject&comcod=" + data.d[i].comcod + "&date=" + sdate;
            var  revcost_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=GPNPALLPRO&comcod="+ data.d[i].comcod + "&date=" + sdate;
            var comnam =data.d[i].compname;

            if (AlreadyContains(comnam, tbJUnqueComNams) == true)
                comnam = '';
            else
                AddToComNamArr(comnam, tbJUnqueComNams);

            

            var ttlNsubs = '';
            if (data.d[i].deptname == 'Margin' & data.d[i].comcod != 'AAAA')
            {
                    ttlNsubs = "<td style=text-align:center;" + ">" + comnam + " <a href='" + Feadesc_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + data.d[i].deptname + "&nbsp" + "</div></a></td>";
                
            }

            else
                ttlNsubs = "<td style=text-align:left;" + ">" + comnam + " &nbsp<b></b></br>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + data.d[i].deptname + "</td>";
           

            var ttlNsubs2 = '';

            if (data.d[i].deptname == 'Margin % on Cost' & data.d[i].comcod != 'AAAA') {
                ttlNsubs2 += "</br><td style=text-align:right;" + "> <a href='" + revcost_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "</br>" + GetFormattedValue(data.d[i].revamt) + "&nbsp" + "</div></a></td>";

            }

            else
                ttlNsubs2 += "<td style=text-align:right;" + ">" +"</br>"+ GetFormattedValue(data.d[i].revamt) + "&nbsp" + "</td>";



            $("#gvFeasibility").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[9]++ + ".</td>"
                    + ttlNsubs
                    + "<td style=text-align:right;" + ">" +"</br>"+ GetFormattedValue(data.d[i].oramt) + "&nbsp" + "</td>"
                    + ttlNsubs2
                    //+"<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].revamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + "</br>" + GetFormattedValue(data.d[i].maramt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + "</br>" + GetFormattedValueSecondVersion(data.d[i].chngeper) + "&nbsp" + "</td>"
            + "</tr></tbody>");

        }

        //M
        if (data.d[i].grp == 'M' && data.d[i].compname != 'Total') {
            var comname_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=SoldUnsold&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            $("#gvpstk").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[10]++ + ".</td>"
                    + "<td style=text-align:left;" + "> <a href='" + comname_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + data.d[i].compname + "&nbsp" + "</div></a></td>"
                    //+ "<td style=text-align:left;" + ">" + "&nbsp<b>" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].toam) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].unsoldam) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].soldam) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].ramt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].atodues) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].bamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].ptodues) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].ctodues) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + "Details " + "&nbsp" + "</td>"
                   
            + "</tr></tbody>");


            toamSumM += data.d[i].toam;
            unsoldamSumCM += data.d[i].unsoldam;
            soldamSumM += data.d[i].soldam;
            ramtSumM += data.d[i].ramt;
            atoduesSumM += data.d[i].atodues;
            bamtSumM += data.d[i].bamt;
            ptoduesSumM += data.d[i].ptodues;
            ctoduesSumM += data.d[i].ctodues;

        }
        //N
        if (data.d[i].grp == 'N' && data.d[i].compname != 'Total') {

            var comname_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PrjStatus&comcod=" + data.d[i].comcod + "&date=" + sdate;
            var prorptwqty_NavigateUrl = "../F_45_GrAcc/LinkGrpProjReport02.aspx?comcod=" + data.d[i].comcod + "&date=" + endDate;
            var probgdvsexp_NavigateUrl = "../F_45_GrAcc/LinkFinncialReports.aspx?Type=BE&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var protrdaywise_NavigateUrl = "../F_45_GrAcc/RptGrpAccDailyTransaction.aspx?Type=GrpProTrans&comcod=" + data.d[i].comcod;


            $("#gvProjectStatus01").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[11]++ + ".</td>"
                    + "<td style=text-align:left;" + "> <a href='" + comname_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + data.d[i].compname + "&nbsp" + "</div></a></td>"
                   // + "<td style=text-align:left;" + ">" + "&nbsp<b>" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].salamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].trecamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].netexpamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].liaamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].netlnamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + "> <a href='" + prorptwqty_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL PROJECTS " + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + probgdvsexp_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL PROJECTS " + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + protrdaywise_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL PROJECTS " + "&nbsp" + "</div></a></td>"
                    //+ "<td style=text-align:right;" + ">" +"ALL PROJECTS"+"&nbsp" + "</td>"
                    //+ "<td style=text-align:right;" + ">" +"ALL PROJECTS"+"&nbsp" + "</td>"
            + "</tr></tbody>");


            salamtSumM += data.d[i].salamt;
            trecamtSumCM += data.d[i].trecamt;
            netexpamtSumM += data.d[i].netexpamt;
            liaamtSumM += data.d[i].liaamt;
            netlnamtSumM += data.d[i].netlnamt;
           
          

        }

        //O
        if (data.d[i].grp == 'P' && data.d[i].compname != 'Total') {

            var Compname_NavigateUrl = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MProStatus&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;

            $("#gvmonprost").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[12]++ + ".</td>"
                    + "<td style=text-align:left;" + "> <a href='" + comname_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + data.d[i].compname + "&nbsp" + "</div></a></td>"
                   // + "<td style=text-align:left;" + ">" + "&nbsp<b>" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].costamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].collamt) + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].netamt) + "&nbsp" + "</td>"
            + "</tr></tbody>");


            costamtSumO += data.d[i].costamt;
            collamtSumCO += data.d[i].collamt;
            netamtSumO += data.d[i].netamt;
            

        }

        //P
        if (data.d[i].grp == 'Q' && data.d[i].compname != 'Total') {

            var hlnkgvissuebasisinv_NavigateUrl = "../F_45_GrAcc/LinkGrpPrurVarAna.aspx?Type=IssueBasis&comcod=" + data.d[i].comcod + "&Date=" + sdate;
            var hlnkgvprobasisinv_NavigateUrl = "../F_45_GrAcc/LinkGrpPrurVarAna.aspx?Type=StkBasis&comcod=" + data.d[i].comcod + "&Date=" + sdate;
            var hlnkgvmatconinv_NavigateUrl = "../F_45_GrAcc/LinkGrpIndResourceConsum.aspx?comcod=" + data.d[i].comcod + "&Date=" + sdate;


            $("#gvInventory").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[13]++ + ".</td>"
                    + "<td style=text-align:left;" + ">" + "&nbsp<b>" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + "> <a href='" + hlnkgvissuebasisinv_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + data.d[i].issuebasis + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + hlnkgvprobasisinv_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + data.d[i].probasis + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + hlnkgvmatconinv_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + data.d[i].matcon + "&nbsp" + "</div></a></td>"
                   // + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].issuebasis) + "&nbsp" + "</td>"
                   // + "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].probasis) + "&nbsp" + "</td>"
                    //+ "<td style=text-align:right;" + ">" + GetFormattedValue(data.d[i].matcon) + "&nbsp" + "</td>"
            + "</tr></tbody>");

            issuebasisSumP += data.d[i].issuebasis;
            probasisSumP += data.d[i].probasis;
            matconSumP += data.d[i].matcon;
        }

        //Q
        if (data.d[i].grp == 'R' && data.d[i].compname != 'Total') {

            var SalDem_NavigateUrl = "../F_45_GrAcc/LinkGrpProWiseClOffered.aspx?Type=SalesDeamnd&comcod=" + data.d[i].comcod + "&Date2=" + endDate;

           
            var SalDec_NavigateUrl = "../F_45_GrAcc/LinkGrpProWiseClOffered.aspx?Type=SalesDeci&comcod=" + data.d[i].comcod + "&Date2=" + endDate;

           
            var CliCap_NavigateUrl = "../F_45_GrAcc/LinkGrpProWiseClOffered.aspx?Type=Capacity&comcod=" + data.d[i].comcod + "&Date2=" + endDate;

            
            var CliHis_NavigateUrl = "../F_45_GrAcc/LinkGrpMktAppointment.aspx?Type=DiscussHis&UType=Mgt&comcod=" + data.d[i].comcod + "&Date2=" + endDate;

            
            var SalPHis_NavigateUrl = "../F_45_GrAcc/LinkGrpMktAppointment.aspx?Type=OffPerformance&UType=Mgt&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;


            $("#gvcomwclients").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[14]++ + ".</td>"
                    + "<td style=text-align:left;" + ">" + "&nbsp<b>" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + "> <a href='" + SalDem_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" +"All Projects" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + SalDec_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" +"All Projects" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + CliCap_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" +"All Projects" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + CliHis_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" +"All Projects" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + SalPHis_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" +"All Projects" + "&nbsp" + "</div></a></td>"
                    //+ "<td style=text-align:right;" + ">" + "All Projects" + "&nbsp" + "</td>"
                    //+ "<td style=text-align:right;" + ">" + "All Projects" + "&nbsp" + "</td>"
                    //+ "<td style=text-align:right;" + ">" + "All Projects" + "&nbsp" + "</td>"
                    //+ "<td style=text-align:right;" + ">" + "All Projects" + "&nbsp" + "</td>"
                    //+ "<td style=text-align:right;" + ">" + "All Projects" + "&nbsp" + "</td>"
            + "</tr></tbody>");
        }

        //R
        if (data.d[i].grp == 'S' && data.d[i].compname != 'Total') {

            var Company_NavigateUrl = "../F_45_GrAcc/LinkAccMultiReport.aspx?rpttype=FxtAsset&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;


            $("#gvFxtAssets").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[15]++ + ".</td>"
                    +"<td style=text-align:left;" + "> <a href='" + Company_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + data.d[i].compname + "&nbsp" + "</div></a></td>"
                   // + "<td style=text-align:left;" + ">" + "&nbsp<b>" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + ">" + "&nbsp<b>" + GetFormattedValue(data.d[i].toam)+ "</td>"
            + "</tr></tbody>");
        
            toamSumR+=data.d[i].toam;
        }

        //S
        if (data.d[i].grp == 'T' && data.d[i].compname != 'Total') {

            var incomest_NavigateUrl = "../F_45_GrAcc/LinkFinncialReports.aspx?Type=IS&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var balancesheet_NavigateUrl = "../F_45_GrAcc/LinkFinncialReports.aspx?Type=BS&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var inaoutflow_NavigateUrl = "../F_45_GrAcc/LinkGrpRealInOutFlow.aspx?Type=RealFlow&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var realpsum_NavigateUrl = "../F_45_GrAcc/LinkGrpRealPaySummary.aspx?Type=MonPaymentSumm&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var inplanfin_NavigateUrl = "../F_45_GrAcc/LinkGrpInvestmentPlan.aspx?Type=ColVsExpenses&comcod=" + data.d[i].comcod + "&Date=" + sdate ;
            var pstaaglance_NavigateUrl = "../F_45_GrAcc/LinkGrpProjectSummary.aspx?comcod=" + data.d[i].comcod;

            $("#gvFinalcialst").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[16]++ + ".</td>"
                    + "<td style=text-align:left;" + ">" + "&nbsp<b>" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + "> <a href='" + incomest_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + balancesheet_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + inaoutflow_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + realpsum_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + inplanfin_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL" + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + "> <a href='" + pstaaglance_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "ALL" + "&nbsp" + "</div></a></td>"
            + "</tr></tbody>");
        }

        //T
        if (data.d[i].grp == 'U' && data.d[i].compname != 'Total') {
            $("#gvHremp").append("<tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[17]++ + ".</td>"
                    + "<td style=text-align:left;" + ">" + "&nbsp<b>" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + ">" + "&nbsp<b>" + GetFormattedValue(data.d[i].toemp) + "</td>"
                    + "<td style=text-align:right;" + ">" + "&nbsp<b>" + GetFormattedValue(data.d[i].netpay) + "</td>"
            + "</tr></tbody>");

            toempSumT += data.d[i].toemp;
            netpaySumT += data.d[i].netpay;
            if (data.d[i].comcod == 'AAAA')
                hrcomcod = data.d[i].hrcomcod;
        }
 
       }

       

        // A-FOOT
       $("#gvDayWSale").append(" <tfoot class=grvFooter>"+"<tr>"
               + "<td style=text-align:center;"+"></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(capacitySum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(masbgdSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(bepSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(tsaleamtSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(tosaleamtSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(suamtSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValueSecondVersion(perotsalSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + " " + "&nbsp" + "</td>"
               
           + "</tr></tfoot>");
        // B-FOOT
       $("#gvrcoll").append(" <tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(capacitySumB) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(masbgdSumB) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(bepSumB) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(tcollamtSumB) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(tastcollamtSumB) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(acamtSumB) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValueSecondVersion(perotcollSumB) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + " " + "&nbsp" + "</td>"
           + "</tr></tfoot>");

        // C-FOOT
       $("#gvchequeinhand").append(" <tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(inhrchqSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(inhfchqSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(inhpchqSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(chqinhandSum) + "&nbsp" + "</td>"
           + "</tr></tfoot>");

        // D-FOOT
       $("#gvarecandpay").append(" <tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(recpamSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(payamSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(balamSum) + "&nbsp" + "</td>"
           + "</tr></tfoot>");

        // E-FOOT
       $("#gvBankPosition").append(" <tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(closbalSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(closliaSum) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(avloanSum) + "&nbsp" + "</td>"
           + "</tr></tfoot>");

        // F-FOOT
       $("#gvarecandpayis").append(" <tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(recpamSumF) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(payamSumCF) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(balamSumF) + "&nbsp" + "</td>"
           + "</tr></tfoot>");

        // G-FOOT
       $("#gvpdc").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(toamSumG) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(payamSumCG) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(pdcamSumG) + "&nbsp" + "</td>"
           + "</tr></tfoot>");

        // H-FOOT
       $("#gvprocure").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(reqamtSumH) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(mrramtSumCH) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(billamtSumH) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" +"" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
           + "</tr></tfoot>");

       // I-FOOT
       $("#gvMMPlanVsAch").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(masplanSumI) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(monplanSumCI) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(excutionSumI) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValueSecondVersion(peromaspSumI) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValueSecondVersion(peromonpSumI) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
           + "</tr></tfoot>");

    // J-FOOT(no footer)

    // M-FOOT
       $("#gvpstk").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(toamSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(unsoldamSumCM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(soldamSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(ramtSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(atoduesSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(bamtSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(ptoduesSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(ctoduesSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
           + "</tr></tfoot>");

    // N-FOOT
       $("#gvProjectStatus01").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(salamtSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(trecamtSumCM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(netexpamtSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(liaamtSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(netlnamtSumM) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" +""+ "&nbsp" + "</td>"
           + "</tr></tfoot>");

    // O-FOOT
       $("#gvmonprost").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(costamtSumO) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(collamtSumCO) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(netamtSumO) + "&nbsp" + "</td>"
           + "</tr></tfoot>");

    //P-FOOT
       $("#gvInventory").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
           + "</tr></tfoot>");

    //Q-FOOT
       $("#gvcomwclients").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "" + "&nbsp" + "</td>"
           + "</tr></tfoot>");

    //R-FOOT
       $("#gvFxtAssets").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(toamSumR) + "&nbsp" + "</td>"
           + "</tr></tfoot>");

   
    //T-FOOT
       var comname_NavigateUrl = "../F_45_GrAcc/LinkRptMgtInterface.aspx?comcod=" + hrcomcod + "&Date1=" + sdate + "&Date2=" + endDate;
       //alert(comname_NavigateUrl);
       $("#gvHremp").append("<tfoot><tr>"
               + "<td style=text-align:center;" + "></td>"
               + "<td style=text-align:right;" + "> <a href='" + comname_NavigateUrl + "'target='_blank'><div style=width:100%;height:100%>" + "Total" + "&nbsp" + "</div></a></td>"
               //+ "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(toempSumT) + "&nbsp" + "</td>"
               + "<td style=text-align:right;font-weight:bold;" + ">" + GetFormattedValue(netpaySumT) + "&nbsp" + "</td>"
           + "</tr></tfoot>");
}

function AddToComNamArr(val, tbJUnqueComNams) {
    if (AlreadyContains(val,tbJUnqueComNams) == false)
            tbJUnqueComNams.push(val);
}
function AlreadyContains(val,tbJUnqueComNams)
{
    for (var i = 0; i < tbJUnqueComNams.length; i++)
        if (tbJUnqueComNams[i] == val)
            return true;
    return false;
}

function GetFormattedValueSecondVersion(amt) {
    if (amt == 0)
        return '';
    var result = amt;
    if (result < 0) {
        result *= -1;
        return "(" + result.toFixed(2) + ")";
    }
    return result.toFixed(2);


}
function GetFormattedValue(amt) {
    if (amt == 0)
        return '';
    var result = amt;
    if (result < 0) {
        result *= -1;
        return "(" + result.toLocaleString('en-US', { maximumFractionDigits: 0 }) + ")";
    }
    return result.toLocaleString('en-US', { maximumFractionDigits: 0 });
    

}
function GetTxtColor(val) {
    if (val<100)
        return 'red';
    return 'green';
}

function ReInitTbl() {
   
    $("#gvDayWSale").empty();//A
    $("#gvrcoll").empty();//B
    $("#gvchequeinhand").empty();//C
    $("#gvarecandpay").empty();//D
    $("#gvBankPosition").empty();//E
    $("#gvarecandpayis").empty();//F
    $("#gvpdc").empty();//G
    $("#gvprocure").empty();//H 
    $("#gvMMPlanVsAch").empty();//I 
    $("#gvFeasibility").empty();//J
    $("#gvpstk").empty();//M 
    $("#gvProjectStatus01").empty();//N
    $("#gvmonprost").empty();//O
    $("#gvInventory").empty();//P
    $("#gvcomwclients").empty();//Q
    $("#gvFxtAssets").empty();//R
    $("#gvFinalcialst").empty();//S
    $("#gvHremp").empty();//T
}

function OnConsolidateChng() {
    $.ajax({

        type: "POST",
        async: true,
        url: "../ASMX_F_45_GrAcc/RptGrpMisDailyActiviteis.asmx/CallCompanyList",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: Sys.Serialization.JavaScriptSerializer.serialize({ 'isConsolidate': consolidateCB }),
        success: function onSuccess(data) {
            noOfCompany = GetComCount(data);
            ConsolidateChkLst(data);
            $('#chkall').prop('checked', false);
            
        }

    });

}
function GetComCount(data) {
    return data.d.length;
}

function ConsolidateChkLst(data)
{
    $('#cblCompany').html('');
    for (var i = 0; i <noOfCompany; i++) {
        curCkbx = '<div style="width:250px;height:30px;float:left;"><input type="checkbox" name="company" value="' + data.d[i].comcod + '"/>' + data.d[i].comsnam+'</div>';
        $('#cblCompany').append(curCkbx);
        
    }
}

function CheckAllComp() {
   
    for (var i = 0; i <noOfCompany; i++) {
        $('#cblCompany :checkbox:eq(' + i + ')').prop('checked', true);
    }
        
}

function UnCheckAllComp() {
    
    for (var i = 0; i <noOfCompany; i++)
        $('#cblCompany :checkbox:eq(' + i + ')').prop('checked', false);
}


function GetStartDate() {
    sdate = $('#txtDate').val();
}
function GetEndDate() {
    endDate = $('#txttodate').val();
}

function GetSelectedCompanies() {
    comnams = '';
    for (var i = 0; i < noOfCompany; i++) {

        if ($('#cblCompany :checkbox:eq(' + i + ')').is(':checked')) {
            comnams+=$('#cblCompany :checkbox:eq(' + i + ')').val();
        }


        
    }
    
}
