function ShowData()
{
    StartProgressBar();
    $.ajax({
        
        type: "POST",
        async: true,
        url: "../ASMX_46_GrMgtInter/RptGrpMisDailyActiviteisWebService.asmx/GetDailyGrpRpt",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: Sys.Serialization.JavaScriptSerializer.serialize({'frdate': sdate, 'todate': endDate }),
        success: function onSuccess(data) {
            
            console.log(data);
            HideLabels();
            displayTable(data);
            CreateBarChart();
            ShowLabels();
            $("#pb").hide();
           
        }
       
    });
    this.ValIntialize();
    
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
    // CONSOLE.LOG(data);
    console.log(data);
    ReInitTbl();
    var tbindex = [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1];//array to hold index no of each table

    var hrcomcod = '';
    //A-HEAD
   
    $("#grvToAch").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
             "<th></th>"
            + "<th></th>"
            + "<th colspan=" + "2" + ">SALES & COLLECTION</th>" 
            + "<th colspan=" + "2" + ">HONOURED</th>"
            + "<th colspan=" + "2" + ">CHEQUE</th>"
        + "</tr></thead>");

    $("#grvToAch").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                  "<th>SL</th>"
                + "<th>COMPANY NAME</th>"
                + "<th>SALES</th>"
                + "<th>COLL. FROM SALES</th>"
                + "<th>RECEIPTS</th>"
                + "<th>PAYMENTS</th>"
                + "<th>RECEIPTS</th>"
                + "<th>ISSUED</th>"
        + "</tr></thead>");


    //B-HEAD
    $("#gvDayWSale").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
            "<th></th>"
            + "<th></th>"
            + "<th colspan=" + "2" + ">SALES TARGET</th>"
            + "<th colspan=" + "3" + ">SALES ACHIEVED</th>"
            + "<th colspan=" + "1" + "></th>"
        + "</tr></thead>");

    $("#gvDayWSale").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                "<th>SL</th>"
              + "<th>COMPANY NAME</th>"
              + "<th>TOTAL</th>"
              + "<th>AS OF TODAY</th>"
              + "<th>TOTAL</th>"
              + "<th>TODAY</th>"
              + "<th>%</th>"
              + "<th>GRAPH</th>"
      + "</tr></thead>");



    //C-HEAD
   
    $("#gvCollSt").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>"
                + "<th></th>"
                + "<th></th>"
                + "<th colspan=" + "2" + ">COLLECTION TARGET</th>"
                + "<th colspan=" + "3" + ">COLLECTION ACHIEVED</th>"
                + "<th colspan=" + "1" + "></th>"
        + "</tr></thead>");

    $("#gvCollSt").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" 
                + "<th>SL</th>"
                + "<th>COMPANY NAME</th>"
                + "<th>TOTAL</th>"
                + "<th>AS OF TODAY</th>"
                + "<th>TOTAL</th>"
                + "<th>TODAY</th>"
                + "<th>%</th>"
                + "<th>GRAPH</th>"
        + "</tr></thead>");


    //D-HEAD
    
    $("#gvrcoll").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                "<th></th>"
                + "<th></th>"
                + "<th></th>"
                + "<th ></th>"
                + "<th></th>"
                + "<th colspan="+ "3" + ">IN HAND CHEQUE</th>"
                + "<th></th>"
                + "<th></th>"
        + "</tr></thead>");

    $("#gvrcoll").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>"
                + "<th>SL</th>"
                + "<th>COMPANY NAME</th>"
                + "<th>TOTAL</br>COLLECTION</th>"
                + "<th>BANK</br>CLEARANCE</th>"
                + "<th>CHEQUE</br>DEPOSITED</th>"
                + "<th>RETURNED</th>"
                + "<th>FRESH</th>"
                + "<th>POST DATED</th>"
                + "<th>REPLACEMENT</br>CHEQUE</th>"
                + "<th>NET COLLECTION <br> (WITHOUTE RPP.)</th>"
        + "</tr></thead>");


    //E-HEAD
    
    $("#grvRecPay").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                "<th colspan=" + "1" +"></th>" +
                "<th colspan=" + "1" + "></th>" +
                "<th colspan=" + "4" + ">RECEIPTS HONOURED</th>" +
                "<th colspan=" + "1" + "></th>" +
                "<th colspan=" + "1" + "></th>" +
        "</tr></thead>");

    $("#grvRecPay").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + ">SL</th>" +
               "<th colspan=" + "1" + ">COMPANY</th>" +
               "<th colspan=" + "1" + ">PRE. SALES</th>" +
               "<th colspan=" + "1" + " >CUR. SALES</th>" +
               "<th colspan=" + "1" + ">LOAN &</br> OTHERS</th>" +
               "<th colspan=" + "1" + " >TOTAL</th>" +
               "<th colspan=" + "1" + ">PAYMENT</th>" +
               "<th colspan=" + "1" + ">GRAPH</th>" +
       "</tr></thead>");



    //F-HEAD
   
    $("#grvAvFund").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
                "<th colspan=" + "1" + "></th>" +
                "<th colspan=" + "1" + "></th>" +
                "<th colspan=" + "4" + ">IN HAND CHEQUE</th>" +
                "<th colspan=" + "2" + ">BANK</th>" +
                "<th colspan=" + "1" + "></th>" +
                "<th colspan=" + "1" + "></th>" +
        "</tr></thead>");

    $("#grvAvFund").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + ">SL</th>" +
               "<th colspan=" + "1" + ">COMPANY</th>" +
               "<th colspan=" + "1" + ">FRESH</th>" +
               "<th colspan=" + "1" + " >RETURN</th>" +
               "<th colspan=" + "1" + ">DEPOSIT</th>" +
               "<th colspan=" + "1" + " >REPLACEMENT</th>" +
               "<th colspan=" + "1" + ">BANK</br>BALANCE</th>" +
               "<th colspan=" + "1" + ">LOAN LIMIT</th>" +
               "<th colspan=" + "1" + ">POST DATE</th>" +
               "<th colspan=" + "1" + ">TOTAL</th>" +
       "</tr></thead>");

    //G-HEAD
    
    $("#gvChqIsu").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + ">SL</th>" +
               "<th colspan=" + "1" + ">COMPANY</th>" +
               "<th colspan=" + "1" + ">ASSET</th>" +
               "<th colspan=" + "1" + " >LIABILITY</th>" +
               "<th colspan=" + "1" + ">OPERATION</th>" +
               "<th colspan=" + "1" + " >OVERHEAD</th>" +
               "<th colspan=" + "1" + ">BANK</br>INTEREST</th>" +
               "<th colspan=" + "1" + ">OTHERS</th>" +
               "<th colspan=" + "1" + ">TOTAL</br>AMOUNT</th>" +
       "</tr></thead>");


    //H-HEAD
    
    $("#gvRecPayiss").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + "></th>" +
               "<th colspan=" + "1" + "></th>" +
               "<th colspan=" + "2" + ">CHEQUED</th>" +
       "</tr></thead>");
    $("#gvRecPayiss").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + ">SL</th>" +
               "<th colspan=" + "1" + ">COMPANY</th>" +
               "<th colspan=" + "1" + ">RECEIPT</th>" +
               "<th colspan=" + "1" + ">ISSUED</th>" +
       "</tr></thead>");
   
    //I-HEAD
   
    $("#gvprocure").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + "></th>" +
               "<th colspan=" + "1" + "></th>" +
               "<th colspan=" + "1" + "></th>" +
               "<th colspan=" + "2" + ">WORK</th>" +
       "</tr></thead>");
    $("#gvprocure").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + ">SL</th>" +
               "<th colspan=" + "1" + ">COMPANY NAME</th>" +
               "<th colspan=" + "1" + ">MATERIAL RECEIVED</th>" +
               "<th colspan=" + "1" + ">TARGET</th>" +
               "<th colspan=" + "1" + ">EXECUTION</th>" +
       "</tr></thead>");


    //J-HEAD
   
    $("#gvpstk").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + ">SL</th>" +
               "<th colspan=" + "1" + ">COMPANY NAME</th>" +
               "<th colspan=" + "1" + ">TOTAL REVENUE</th>" +
               "<th colspan=" + "1" + ">UNSOLD AMT</th>" +
               "<th colspan=" + "1" + ">SOLD AMT</th>" +
               "<th colspan=" + "1" + ">TOTAL RECEIVED</th>" +
               "<th colspan=" + "1" + ">RECEIVEABLE</th>" +
       "</tr></thead>");


    //K-HEAD
  
    $("#gvmonprost").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + ">SL</th>" +
               "<th colspan=" + "1" + ">COMPANY NAME</th>" +
               "<th colspan=" + "1" + ">TOTAL COST</th>" +
               "<th colspan=" + "1" + ">TOTAL</br>COLLECTION</th>" +
               "<th colspan=" + "1" + ">NET</br>POSITION</th>" +
       "</tr></thead>");

    //L-HEAD
 
    $("#gvHremp").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr>" +
               "<th colspan=" + "1" + ">SL</th>" +
               "<th colspan=" + "1" + ">COMPANY NAME</th>" +
               "<th colspan=" + "1" + ">TOTAL</br> EMPLOYEE</th>" +
               "<th colspan=" + "1" + ">SALARY</br>COLLECTION</th>" +
       "</tr></thead>");
   



    for (var i = 0 ; i < data.d.length; i++) {
        
        if (i >= data.d.length)
            break;

        

        //A
        if (data.d[i].grp == 'A' && data.d[i].compname != 'TOTAL') {

            $("#grvToAch").append(" <tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[0]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].tsal.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].tcoll.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].trec.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].tpay.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].tcrec.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].tcisu.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                + "</tr></tbody>");

           // (arval.tar).toLocaleString('en-US', { minimumFractionDigits: 0 })

            tsalSum+=data.d[i].tsal;
            tcollSum += data.d[i].tcoll;
            trecSum += data.d[i].trec;
            tpaySum += data.d[i].tpay;
            tcrecSum += data.d[i].tcrec;
            tcisuSum += data.d[i].tcisu;
        }
      
        
        //B
        //tsaleamtSum = 0.00;
        //tosaleamtSum = 0.00;
        //acsaleamtSum = 0.00;
        //tdayamtSum = 0.00;
        //perotsaleSum = 0.00;

        if (data.d[i].grp == 'B' && data.d[i].compname != 'TOTAL') {
            var tsaleamt_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Sales&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var salamt_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RptDayWSale&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            

            var tdayamt_txt_color = GetTxtColor(data.d[i].tdayamt);
            var perotsale_txt_color = GetTxtColor(data.d[i].perotsale);
            //+ '-' + data.d[i].acsaleamt.toLocaleString('en-US', { minimumFractionDigits: 0 })
            $("#gvDayWSale").append(" <tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[1]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + tsaleamt_url + "' target='_blank'><div style=width:100%;height:100%>" +   data.d[i].tsaleamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].tosaleamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + salamt_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].acsaleamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;color:" + tdayamt_txt_color + ";" + ">" +  data.d[i].tdayamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;color:" + perotsale_txt_color + ";" + ">" +  data.d[i].perotsale.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + "GRAPH" + "&nbsp" + "</td>"
            + "</tr></tbody>");

            tsaleamtSum += data.d[i].tsaleamt;
            tosaleamtSum += data.d[i].tosaleamt;
            acsaleamtSum += data.d[i].acsaleamt;
            tdayamtSum += data.d[i].tdayamt;
            perotsaleSum += data.d[i].perotsale;
       
        }
        

        //C
        if (data.d[i].grp == 'C' && data.d[i].compname != 'TOTAL') {

            var tcollamt_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Collection&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            var achamt_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=CollSummary&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
            
            + "<td style=text-align:right;" + "> <a href='" + salamt_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].acsaleamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</div></a></td>"

            $("#gvCollSt").append(" <tbody><tr>" +
                        "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[2]++ + ".</td>" +
                        "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>" +
                         "<td style=text-align:right;" + "> <a href='" + tcollamt_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].tcisu.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].tastcollamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" +
                        "<td style=text-align:right;" + "> <a href='" + achamt_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].accollam.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</div></a></td>"
                        +"<td style=text-align:right;" + ">" + data.d[i].tdaycollamt.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>" +
                        "<td style=text-align:right;" + ">" + data.d[i].perotcoll.toLocaleString('en-US', { minimumFractionDigits: 0 })   + "&nbsp" + "</td>" +
                        "<td style=text-align:right;" + ">" + "GRAPH" + "&nbsp" + "</td>" +
                "</tr></tbody>");

            tcollamtSum += data.d[i].tcollamt;
            tastcollamtSum += data.d[i].tastcollamt;
            tdaycollamtSum += data.d[i].accollam;
            tdayamtSum += data.d[i].tdaycollamt;
            perotcolleSum += data.d[i].perotcoll;

        }


        //D
        if (data.d[i].grp == 'D' && data.d[i].compname != 'TOTAL') {

            $("#gvrcoll").append(" <tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[3]++ + ".</td>"
                    + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].acamt.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].reconamt.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].depchq.toLocaleString('en-US', { minimumFractionDigits: 0 })   + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].inhrchq.toLocaleString('en-US', { minimumFractionDigits: 0 })   + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].inhfchq.toLocaleString('en-US', { minimumFractionDigits: 0 })   + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].inhpchq.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].repchq.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].ncollamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                + "</tr></tbody>");

            acamtSum += data.d[i].acamt;
            reconamtSum += data.d[i].reconamt;
            depchqSum += data.d[i].depchq;
            inhrchqSum += data.d[i].inhrchq;
            inhfchqSum += data.d[i].inhfchq;
            inhpchqSum += data.d[i].inhpchq;
            repchqSum += data.d[i].repchq;
            ncollamtSum += data.d[i].ncollamt;
        }

            //E
            if (data.d[i].grp == 'E' && data.d[i].compname != 'TOTAL') {
                var PayAmt_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RecPay&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
                
                var cmrecamt_txt_color = GetTxtColor(data.d[i].cmrecamt);

                $("#grvRecPay").append(" <tbody><tr>"
                        +"<td style=text-align:center;" + ">" + "&nbsp" + tbindex[4]++ + ".</td>"
                        + "<td style=text-align:left;" + ">"+ "&nbsp" + data.d[i].compname + "</td>" 
                        + "<td style=text-align:right;" + ">"+ data.d[i].lmrecamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" 
                        + "<td style=text-align:right;color:" + cmrecamt_txt_color + ";" + ">" + data.d[i].cmrecamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>" +
                        "<td style=text-align:right;" + ">"+ data.d[i].otrecamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].recpam.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + PayAmt_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].payam.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">"+ "GRAPH" + "&nbsp" + "</td>"
                + "</tr></tbody>");
            lmrecamtSum += data.d[i].lmrecamt;
            cmrecamtSum += data.d[i].cmrecamt;
            otrecamtqSum += data.d[i].otrecamt;
            recpamSum += data.d[i].recpam;
            payamSum += data.d[i].payam;
               
            

            }


        //F
            if (data.d[i].grp == 'F' && data.d[i].compname != 'TOTAL') {
                var ainhpchq_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=ChequeInHand&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
                var closbal_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=BankPosition&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;

               
               
                $("#grvAvFund").append(" <tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[5]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].ainhfchq.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].ainhrchq.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" +
                        "<td style=text-align:right;" + ">" +data.d[i].adepchq.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" +  data.d[i].arepchq.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                         + "<td style=text-align:right;" + "> <a href='" + closbal_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].closbal.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" +data.d[i].bankbal.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                         + "<td style=text-align:right;" + "> <a href='" + ainhpchq_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].ainhpchq.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" +  data.d[i].tavamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                + "</tr></tbody>");

                ainhfchqSum += data.d[i].ainhfchq;
                ainhrchqSum += data.d[i].ainhrchq;
                adepchqSum += data.d[i].adepchq;
                arepchqSum += data.d[i].arepchq;
                closbalSum += data.d[i].closbal;
                bankbalSum += data.d[i].bankbal;
                ainhpchqSum += data.d[i].ainhpchq;
                tavamtSum += data.d[i].tavamt;
            }

        //G
            if (data.d[i].grp == 'G' && data.d[i].compname != 'TOTAL') {
                $("#gvChqIsu").append(" <tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[6]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" +  data.d[i].amt1.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].amt2.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>" +
                        "<td style=text-align:right;" + ">" + data.d[i].amt3.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" +data.d[i].amt4.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].amt5.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" +data.d[i].amt6.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" +  data.d[i].tamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                + "</tr></tbody>");

                amt1Sum += data.d[i].amt1;
                amt2Sum += data.d[i].amt2;
                amt3Sum += data.d[i].amt3;
                amt4Sum += data.d[i].amt4;
                amt5Sum += data.d[i].amt5;
                amt6Sum += data.d[i].amt6;
                tamtSum += data.d[i].tamt;
            }

        //H
            if (data.d[i].grp == 'H' && data.d[i].compname != 'TOTAL') {
                var balamtis_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=IssuedVsCollect&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
              

                $("#gvRecPayiss").append(" <tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[7]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        + "<td style=text-align:right;" + ">" +  data.d[i].recpamis.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + "> <a href='" + balamtis_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].payamis.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</div></a></td>"
                + "</tr></tbody>");

                recpamisSum += data.d[i].recpamis;
                payamisSum += data.d[i].payamis;
               
            }

        //I
            if (data.d[i].grp == 'I' && data.d[i].compname != 'TOTAL') {
                var hlnkgvourchasepro_url = "../F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=Purchase&comcod=" + data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;

                $("#gvprocure").append(" <tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[8]++ + ".</td>"
                        + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                        +"<td style=text-align:right;" + "> <a href='" + hlnkgvourchasepro_url + "' target='_blank'><div style=width:100%;height:100%>" +  data.d[i].mrramt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].monplan.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>" 
                        + "<td style=text-align:right;" + ">" +  data.d[i].excution.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>" 
                + "</tr></tbody>");

                mrramtSum += data.d[i].mrramt;
                monplanSum += data.d[i].monplan;
                excutionSum += data.d[i].excution;

            }

        //J
            if (data.d[i].grp == 'J' && data.d[i].compname != 'TOTAL') {

                var comname_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=SoldUnsold&comcod=" + +data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;

                $("#gvpstk").append(" <tbody><tr>"
                        + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[9]++ + ".</td>"
                        + "<td style=text-align:left;" + "> <a href='" + comname_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].compname + "&nbsp" + "</div></a></td>"
                        + "<td style=text-align:right;" + ">" +  data.d[i].revamt.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" +  data.d[i].usoldamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" +  data.d[i].soldamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].recamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                        + "<td style=text-align:right;" + ">" + data.d[i].recabamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                + "</tr></tbody>");

                revamtSum += data.d[i].revamt;
                usoldamtSum += data.d[i].usoldamt;
                soldamtSum += data.d[i].soldamt;
                recamtSum += data.d[i].recamt;
                recabamtSum += data.d[i].recabamt;

            }


        //K
            if (data.d[i].grp == 'K' && data.d[i].compname != 'TOTAL') {
                var comname_url = "../F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MProStatus&comcod=" + +data.d[i].comcod + "&Date1=" + sdate + "&Date2=" + endDate;
                $("#gvmonprost").append(" <tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[10]++ + ".</td>"
                    + "<td style=text-align:left;" + "> <a href='" + comname_url + "' target='_blank'><div style=width:100%;height:100%>" + data.d[i].compname + "&nbsp" + "</div></a></td>"
                    + "<td style=text-align:right;" + ">" +  data.d[i].costamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].collamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" +  data.d[i].netamt.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                + "</tr></tbody>");

                costamtSum += data.d[i].costamt;
                collamtSum += data.d[i].collamt;
                netamtSum += data.d[i].netamt;
               

            }


        //L
            if (data.d[i].grp == 'L' && data.d[i].compname != 'TOTAL') {

                

                $("#gvHremp").append(" <tbody><tr>"
                    + "<td style=text-align:center;" + ">" + "&nbsp" + tbindex[11]++ + ".</td>"
                    + "<td style=text-align:left;" + ">" + "&nbsp" + data.d[i].compname + "</td>"
                    + "<td style=text-align:right;" + ">" +data.d[i].curempno.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                    + "<td style=text-align:right;" + ">" + data.d[i].curpay.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
                + "</tr></tbody>");

                curempnoSum += data.d[i].curempno;
                curpaySum += data.d[i].curpay;
               


            }


        //get total_link comcod FOR L
           if (data.d[i].comcod == 'AAAA')
            hrcomcod = data.d[i].hrcomcod;
        
        }

        //A-FOOT
        $("#grvToAch").append(" <tfoot><tr><td style=text-align:center;" + "></td>" + "<td style=text-align:right;font-weight:bold;" + ">"
                + "TOTAL" + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + tsalSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + tcollSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + trecSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + tpaySum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + tcrecSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + tcisuSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td></tr></tbody>");
        //data.d[i].tsal.toLocaleString('en-US', { minimumFractionDigits: 0 })

        //B-FOOT
        $("#gvDayWSale").append(" <tfoot><tr><td style=text-align:center;" + "></td>" + "<td style=text-align:right;font-weight:bold;" + ">"
                + "TOTAL" + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + tsaleamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + tosaleamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + acsaleamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + tdayamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + perotsaleSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + " " + "&nbsp" + "</td></tr></tbody>");


        //C-FOOT
        $("#gvCollSt").append(" <tfoot><tr><td style=text-align:center;" + "></td>" + "<td style=text-align:right;font-weight:bold;" + ">"
                + "TOTAL" + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + tcollamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + tastcollamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + tdaycollamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + tdayamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + perotcolleSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + " " + "&nbsp" + "</td></tr></tbody>");


        //D-FOOT
        $("#gvrcoll").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            +"<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + acamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            
                + "<td style=text-align:right;font-weight:bold;" + ">"+ reconamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
                + "<td style=text-align:right;font-weight:bold;" + ">"
                + depchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 })  + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + inhrchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + inhfchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + inhpchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + repchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
               + ncollamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
               +"</tr></tfoot>");


        //E-FOOT
        $("#grvRecPay").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            +"<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" +lmrecamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">"+ cmrecamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">"+ otrecamtqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">"+ recpamSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>" 
            + "<td style=text-align:right;font-weight:bold;" + ">"+ payamSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"  
            + "<td style=text-align:right;font-weight:bold;" + ">"+ "" + "&nbsp" + "</td>"
            + "</tr></tfoot>");


        //F-FOOT
        $("#grvAvFund").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + ainhfchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + ainhrchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + adepchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + arepchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + closbalSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + bankbalSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + ainhpchqSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + tavamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "</tr></tfoot>");

    //G-FOOT
        $("#gvChqIsu").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + amt1Sum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + amt2Sum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + amt3Sum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + amt4Sum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + amt5Sum.toLocaleString('en-US', { minimumFractionDigits: 0 })+ "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + amt6Sum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + tamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "</tr></tfoot>");

    //H-FOOT
        $("#gvRecPayiss").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + recpamisSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + payamisSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "</tr></tfoot>");

    //I-FOOT
        $("#gvprocure").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + mrramtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + monplanSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + excutionSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "</tr></tfoot>");

    //J-FOOT
        $("#gvpstk").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + revamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + usoldamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + soldamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + recamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + recabamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "</tr></tfoot>");


    //K-FOOT
        $("#gvmonprost").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + "TOTAL" + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + costamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + collamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + netamtSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "</tr></tfoot>");

    //L-FOOT
        var total_url = "../F_45_GrAcc/LinkRptMgtInterface.aspx?comcod=" + hrcomcod + "&Date1=" + sdate + "&Date2=" + endDate;
       
        
        $("#gvHremp").append(" <tfoot><tr>"
            + "<td style=text-align:center;" + "></td>"
            + "<td style=text-align:right;font-weight:bold;" + "> <a href='" + total_url + "' target='_blank'><div style=width:100%;height:100%>" + "TOTAL" + "&nbsp" + "</div></a></td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + curempnoSum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "<td style=text-align:right;font-weight:bold;" + ">" + curpaySum.toLocaleString('en-US', { minimumFractionDigits: 0 }) + "&nbsp" + "</td>"
            + "</tr></tfoot>");



 


}
function GetTxtColor(val) {
    if (val < 100)
        return 'red';
    return 'green';
}

function ReInitTbl() {
    $("#grvToAch").empty();//A
    $("#gvDayWSale").empty();//B
    $("#gvCollSt").empty();//C
    $("#gvrcoll").empty();//D
    $("#grvRecPay").empty();//E
    $("#grvAvFund").empty();//F
    $("#gvChqIsu").empty();//G
    $("#gvRecPayiss").empty();//H
    $("#gvprocure").empty();//I
    $("#gvpstk").empty();//J
    $("#gvmonprost").empty();//K
    $("#gvHremp").empty();//L
}

    function CreateBarChart() {
        InitCanvas();

        //A-GRPH
        var ToAchbrchrt = document.getElementById('ToAchbrchrt').getContext('2d');

        var fstpt = (tcrecSum / 100000).toFixed(2);
        var sndtpt = (tpaySum / 100000).toFixed(2); 

   

        var data = {

            labels: ["RECEIPTS", "PAYMENTS"],
            datasets: [
                {
              
                    data: [fstpt, sndtpt]
                }
            ]
        };

  
        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END A-GRPH



        //B-GRPH
        var ToAchbrchrt = document.getElementById('gvDayWSalebrchrt').getContext('2d');
        var fstpt = (tosaleamtSum / 100000).toFixed(2);
        var sndtpt = (acsaleamtSum / 100000).toFixed(2);


        var data = {

            labels: ["TARGET", "ACHIEVED"],
            datasets: [
                {

                    data: [fstpt, sndtpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END B-GRPH


        //C-GRPH
        var ToAchbrchrt = document.getElementById('gvCollStbrchrt').getContext('2d');

        var fstpt = (tdaycollamtSum / 100000).toFixed(2);
        var sndtpt = (tdayamtSum / 100000).toFixed(2);

        var data = {

            labels: ["TARGET", "ACHIEVED"],
            datasets: [
                {

                    data: [fstpt, sndtpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END C-GRPH


        //E-GRPH
        var ToAchbrchrt = document.getElementById('grvRecPaytbrchrt').getContext('2d');

        var fstpt = (recpamSum / 100000).toFixed(2);
        var sndtpt = (payamSum / 100000).toFixed(2);

        var data = {

            labels: ["RECEIPTS", "PAYMENTS"],
            datasets: [
                {

                    data: [fstpt, sndtpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END E-GRPH

        //F-GRPH
        var ToAchbrchrt = document.getElementById('grvAvFundbrchrt').getContext('2d');
        var fstpt = (closbalSum / 100000).toFixed(2);
        var sndtpt = (tavamtSum / 100000).toFixed(2);


        var data = {

            labels: ["BANK BALANCE", "TOTAL"],
            datasets: [
                {

                    data: [fstpt, sndtpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END F-GRPH


        //G-GRPH
        var ToAchbrchrt = document.getElementById('gvChqIsubrchrt').getContext('2d');
        var fstpt = (amt1Sum / 100000).toFixed(2);
        var sndtpt = (amt2Sum / 100000).toFixed(2);
        var thrdpt = (amt3Sum / 100000).toFixed(2);
        var frthpt = (amt4Sum / 100000).toFixed(2);
        var fivthpt = (amt5Sum / 100000).toFixed(2);


        var data = {

            labels: ["ASSET", "LIABILITIES","OPERATION","OVERHEAD","INTEREST"],
            datasets: [
                {

                    data: [fstpt, sndtpt, thrdpt, frthpt, fivthpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.datasets[0].bars[2].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[2].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[2].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[2].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[3].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[3].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[3].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[3].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.datasets[0].bars[4].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[4].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[4].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[4].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.update();
        //END G-GRPH


        //H-GRPH
        var ToAchbrchrt = document.getElementById('gvRecPayissbrchrt').getContext('2d');
        var fstpt = (recpamisSum / 100000).toFixed(2);
        var sndtpt = (payamisSum / 100000).toFixed(2);


        var data = {

            labels: ["RECEIPTS", "ISSUED"],
            datasets: [
                {

                    data: [fstpt,sndtpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END H-GRPH


        //I-GRPH
        var ToAchbrchrt = document.getElementById('gvprocurebrchrt').getContext('2d');
        var fstpt = (monplanSum / 100000).toFixed(2);
        var sndtpt = (excutionSum / 100000).toFixed(2);


        var data = {

            labels: ["TARGET", "EXECUTION"],
            datasets: [
                {

                    data: [fstpt, sndtpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END I-GRPH

        //J-GRPH  
        var ToAchbrchrt = document.getElementById('gvpstkbrchrt').getContext('2d');
        var fstpt = (usoldamtSum / 100000).toFixed(2);
        var sndtpt = (soldamtSum / 100000).toFixed(2);


        var data = {

            labels: ["UNSOLD AMT", "SOLD AMT"],
            datasets: [
                {

                    data: [fstpt, sndtpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END J-GRPH


        //K-GRPH  
        var ToAchbrchrt = document.getElementById('gvmonprostbrchrt').getContext('2d');
        var fstpt = (costamtSum / 100000).toFixed(2);
        var sndtpt = (collamtSum / 100000).toFixed(2);


        var data = {

            labels: ["TOTAL COST", "COLLECTION"],
            datasets: [
                {

                    data: [fstpt, sndtpt]
                }
            ]
        };


        ToAchbrchrt = new Chart(ToAchbrchrt).Bar(data);

        ToAchbrchrt.datasets[0].bars[0].fillColor = "rgba(51,255,47,0.5)";
        ToAchbrchrt.datasets[0].bars[0].highlightFill = "rgba(0,204,0,0.75)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";
        ToAchbrchrt.datasets[0].bars[0].highlightStroke = "rgba(220,220,220,1)";

        ToAchbrchrt.datasets[0].bars[1].fillColor = "rgba(151,187,205,0.5)";
        ToAchbrchrt.datasets[0].bars[1].highlightFill = "rgba(151,187,205,0.8)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";
        ToAchbrchrt.datasets[0].bars[1].highlightStroke = "rgba(151,187,205,1)";

        ToAchbrchrt.update();
        //END J-GRPH

        

    }
    function InitCanvas() {
        //A-GRPH
        $('#grvToAchDiv').html('');
        $('#grvToAchDiv').html('<canvas id="ToAchbrchrt" width="236" height="200" backgroundColor: "#F5DEB3"></canvas>');

        //B-GRPH
        $('#gvDayWSaleDiv').html('');
        $('#gvDayWSaleDiv').html('<canvas id="gvDayWSalebrchrt" width="236" height="200"></canvas>');

        //C-GRPH
        $('#gvCollStbrchrtDiv').html('');
        $('#gvCollStbrchrtDiv').html('<canvas id="gvCollStbrchrt" width="236" height="200"></canvas>');

        //E-GRPH
        $('#grvRecPaytDiv').html('');
        $('#grvRecPaytDiv').html('<canvas id="grvRecPaytbrchrt" width="236" height="200"></canvas>');

        //F-GRPH
        $('#grvAvFundiv').html('');
        $('#grvAvFundiv').html('<canvas id="grvAvFundbrchrt" width="236" height="200"></canvas>');

        //G-GRPH
        $('#gvChqIsudiv').html('');
        $('#gvChqIsudiv').html('<canvas id="gvChqIsubrchrt" width="236" height="200"></canvas>');

        //H-GRPH
        $('#gvRecPayissDiv').html('');
        $('#gvRecPayissDiv').html('<canvas id="gvRecPayissbrchrt" width="236" height="200"></canvas>');

        //I-GRPH
        $('#gvprocureDiv').html('');
        $('#gvprocureDiv').html('<canvas id="gvprocurebrchrt" width="236" height="200"></canvas>');

        //j-GRPH
        $('#gvpstkDiv').html('');
        $('#gvpstkDiv').html('<canvas id="gvpstkbrchrt" width="236" height="200"></canvas>');

        //k-GRPH
        $('#gvmonprostDiv').html('');
        $('#gvmonprostDiv').html('<canvas id="gvmonprostbrchrt" width="300" height="200"></canvas>');


    }

    function GetFormattedValue(amt){
        if (amt == 0)
            return '';
        return parseInt(amt).toFixed(0);

    }

    function GetFormattedValue2dp(amt) {
        if (amt == 0)
            return '';
        return parseInt(amt).toFixed(2);

    }

    function GetStartDate() {
        sdate = $('#txtDate').val();
    }
    function GetEndDate() {
        endDate = $('#txttodate').val();
    }

    function HideLabels() {
        $('#lblToDayAc').hide();
        $('#lblSales').hide();
        $('#lblColl').hide();
        $('#lblCollBrk').hide();
        $('#lblRecPay').hide();
        $('#lblAvFund').hide();
        $('#lblChqisu').hide();
        $('#lblRecPayiss').hide();
        $('#lblProcurement').hide();
        $('#lblStock').hide();
        $('#lblMonProStatus').hide();
        $('#lblHrMgt').hide();
        
    }
    function ShowLabels() {
        $('#lblToDayAc').show();
        $('#lblSales').show();
        $('#lblColl').show();
        $('#lblCollBrk').show();
        $('#lblRecPay').show();
        $('#lblAvFund').show();
        $('#lblChqisu').show();
        $('#lblRecPayiss').show();
        $('#lblProcurement').show();
        $('#lblStock').show();
        $('#lblMonProStatus').show();
        $('#lblHrMgt').show();
    }

    function ValIntialize()
    {
        tsalSum = 0;
        tcollSum = 0;
        trecSum = 0;
        tpaySum = 0;
        tcrecSum = 0;
        tcisuSum = 0;
        tsaleamtSum = 0;
        tosaleamtSum = 0;
        acsaleamtSum = 0;
        tdayamtSum = 0;
        perotsaleSum = 0;

        //fotter & graph iables for C
        tcollamtSum = 0;
        tastcollamtSum = 0;
        tdaycollamtSum = 0;
        tdayamtSum = 0;
        perotcolleSum = 0;

        //fotter & graph iables for D
        acamtSum = 0;
        reconamtSum = 0;
        depchqSum = 0;
        inhrchqSum = 0;
        inhfchqSum = 0;
        inhpchqSum = 0;
        repchqSum = 0;
        ncollamtSum = 0;

        //fotter & graph iables for E
        lmrecamtSum = 0;
        cmrecamtSum = 0;
        otrecamtqSum = 0;
        recpamSum = 0;
        payamSum = 0;

        //fotter & graph iables for F
        ainhfchqSum = 0;
        ainhrchqSum = 0;
        adepchqSum = 0;
        arepchqSum = 0;
        closbalSum = 0;
        bankbalSum = 0;
        ainhpchqSum = 0;
        tavamtSum = 0;

        //fotter & graph variables for G
        amt1Sum = 0;
        amt2Sum = 0;
        amt3Sum = 0;
        amt4Sum = 0;
        amt5Sum = 0;
        amt6Sum = 0;
        tamtSum = 0;

        //fotter & graph iables for H
        recpamisSum = 0;
        payamisSum = 0;

        //fotter & graph iables for I
        mrramtSum = 0;
        monplanSum = 0;
        excutionSum = 0;

        //fotter & graph iables for J
        revamtSum = 0;
        usoldamtSum = 0;
        soldamtSum = 0;
        recamtSum = 0;
        recabamtSum = 0;

        //fotter & graph iables for K
        costamtSum = 0;
        collamtSum = 0;
        netamtSum = 0;

        //fotter & graph iables for L
        curempnoSum = 0;
        curpaySum = 0;


    }


