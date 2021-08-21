
function hideGrpLbls() {
    $('#lblMonSalCol').hide();
    $('#lblMonPurPay').hide();
    $('#lblMonResPay').hide();
    $('#lblMonTarEx').hide();
    $('#lblSalesNext').hide();
    $('#lblPurNext').hide();
    $('#lblAccNext').hide();
    //$('#lblMonTarExNext').hide();
}
function GetMonthlyInfo() {
    StartProgressBar();
    

    $.ajax({
        type: "POST",
        async: true,
        url: '../Service/ASMX_34_Mgt/DashBoardService.asmx/GetMonthlySalCalLP',
        //url: "DashBoardAll.aspx/GetMonthlySalCal",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: Sys.Serialization.JavaScriptSerializer.serialize({ 'selectedDate': fromDate, 'comcod': comcod }),
        //data: '{selectedDate:"' + fromDate + '"}',
        //async: true,
        success: function onSuccess(data) {
            
            Populate(data);
            displayTable();
            CreateBarChart();
            ShowGrpLbls();
            $("#pb").hide();
            
        }
    });
}

function IndeterminateProgressBar() {
    $("#pb").css({ "padding-left": "0%", "padding-right": "20%" });
    $("#pb").progressbar("option", "value", 100);
    $("#pb").animate({ paddingLeft: "20%", paddingRight: "0%" }, 3000, "linear",
        function () { IndeterminateProgressBar(pb); });
}
function StartProgressBar() {
    $("#pb").show();
    $("#pb").progressbar({ value: 100 });
    IndeterminateProgressBar($("#pb"));
}
function ShowGrpLbls() {
    $('#lblMonSalCol').show();
    $('#lblMonPurPay').show();
    $('#lblMonResPay').show();
    $('#lblMonTarEx').show();
    $('#lblSalesNext').show();
    $('#lblPurNext').show();
    $('#lblAccNext').show();
    //$('#lblMonTarExNext').show();
}


function SetTableLength(data) {
    tbLen = data.d.length;
}

function Populate(data) {
    var tsal = 0;
    var tcal = 0;

    var tpar = 0;
    var tpay = 0;

    var trec = 0;
    var tpayment = 0;

    var target = 0;
    var exec = 0;

    SetTableLength(data);
    $(data.d).each(function (index, value) {
        Records[index] = new Array(7);
        grphRecords[index] = new Array(7);

        Records[index][0] = value.yearmon;

        //sales
       
        value.ttlsalamt == 0 ? Records[index][1] = '' : Records[index][1] = Math.ceil(value.ttlsalamt).toLocaleString('en-US', { minimumFractionDigits: 0 });
        value.collamt == 0 ? Records[index][2] = '' : Records[index][2] = Math.ceil(value.collamt).toLocaleString('en-US', { minimumFractionDigits: 0 });
    


        grphRecords[index][0] = value.yearmon1;
        grphRecords[index][1] =value.ttlsalamt;
        grphRecords[index][2] = value.collamt;

        tsal += value.ttlsalamt;
        tcal += value.collamt;
        salSum = Math.ceil(tsal).toLocaleString('en-US', { minimumFractionDigits: 0 });
        colSum = Math.ceil(tcal).toLocaleString('en-US', { minimumFractionDigits: 0 });

        //purchase
        value.ttlpuramt == 0 ? Records[index][3] = '' : Records[index][3] = Math.ceil(value.ttlpuramt).toLocaleString('en-US', { minimumFractionDigits: 0 });
        value.tpayamt == 0 ? Records[index][4] = '' : Records[index][4] = Math.ceil(value.tpayamt).toLocaleString('en-US', { minimumFractionDigits: 0 });
        grphRecords[index][3] = value.ttlpuramt;
        grphRecords[index][4] = value.tpayamt;

        tpar += value.ttlpuramt;
        tpay += value.tpayamt;
        purSum = Math.ceil(tpar).toLocaleString('en-US', { minimumFractionDigits: 0 });
        paySum = Math.ceil(tpay).toLocaleString('en-US', { minimumFractionDigits: 0 });

        //acc
        value.cram == 0 ? Records[index][5] = '' : Records[index][5] = Math.ceil(value.cram).toLocaleString('en-US', { minimumFractionDigits: 0 });
        value.dram == 0 ? Records[index][6] = '' : Records[index][6] = Math.ceil(value.dram).toLocaleString('en-US', { minimumFractionDigits: 0 });
        grphRecords[index][5] = value.cram;
        grphRecords[index][6] = value.dram;

        trec += value.cram;
        tpayment += value.dram;
        resSum = Math.ceil(trec).toLocaleString('en-US', { minimumFractionDigits: 0 });
        paymentSum = Math.ceil(tpayment).toLocaleString('en-US', { minimumFractionDigits: 0 });

        ////Target Vs Exe
        //value.taramt == 0 ? Records[index][7] = '' : Records[index][7] = Math.ceil(value.taramt).toLocaleString('en-US', { minimumFractionDigits: 0 });
        //value.examt == 0 ? Records[index][8] = '' : Records[index][8] = Math.ceil(value.examt).toLocaleString('en-US', { minimumFractionDigits: 0 });
        //grphRecords[index][7] = value.taramt;
        //grphRecords[index][8] = value.examt;

        //target += value.taramt;
        //exec += value.examt;
        //TargetSum = Math.ceil(target).toLocaleString('en-US', { minimumFractionDigits: 0 });
        //ExecSum = Math.ceil(exec).toLocaleString('en-US', { minimumFractionDigits: 0 });
    });
}


function displayTable() {
    $("#grvMonthlySales").empty();
    $("#grvMonthlySales").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr><th>SL</th><th>MONTH</th>" +
        "<th>SALES</th><th>COLLECTION</th></tr></thead>");



    for (var i = 0 ; i < tbLen; i++) {
        if (i >= tbLen)
            break;
        $("#grvMonthlySales").append(" <tbody><tr><td style=text-align:center;" + ">" + "&nbsp" + (i + 1) + "</td>" + "<td style=text-align:left;" + ">"
            + "&nbsp" + Records[i][0] + "</td>" + "<td style=text-align:right;" + ">" + "&nbsp" + Records[i][1] + "</td>" + "<td style=text-align:right;" + ">"
            + "&nbsp" + Records[i][2] + "</td></tr></tbody>");

    }
    $("#grvMonthlySales").append(" <tfoot><tr><td style=text-align:center;></td>" + "<td style=text-align:right;font-weight:bold;>"
            + "TOTAL" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + salSum + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
            + "&nbsp" + colSum + "</td></tr></tfoot>");


    //purchase

    $("#grvMonthlyPurcse").empty();
    $("#grvMonthlyPurcse").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr><th>SL</th><th>MONTH</th>" +
        "<th>PURCHASE</th><th>PAYMENT</th></tr></thead>");



    for (var i = 0 ; i < tbLen; i++) {
        if (i >= tbLen)
            break;
        $("#grvMonthlyPurcse").append(" <tbody><tr><td style=text-align:center;" + ">" + "&nbsp" + (i + 1) + "</td>" + "<td style=text-align:left;" + ">"
            + "&nbsp" + Records[i][0] + "</td>" + "<td style=text-align:right;" + ">" + "&nbsp" + Records[i][3] + "</td>" + "<td style=text-align:right;" + ">"
            + "&nbsp" + Records[i][4] + "</td></tr></tbody>");

    }
    $("#grvMonthlyPurcse").append(" <tfoot><tr><td style=text-align:center;></td>" + "<td style=text-align:right;font-weight:bold;>"
            + "TOTAL" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + purSum + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
            + "&nbsp" + paySum + "</td></tr></tfoot>");


    //account

    $("#grvMonthlyAcc").empty();
    $("#grvMonthlyAcc").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr><th>SL</th><th>MONTH</th>" +
        "<th>RECEIPT</th><th>PAYMENT</th></tr></thead>");



    for (var i = 0 ; i < tbLen; i++) {
        if (i >= tbLen)
            break;
        $("#grvMonthlyAcc").append(" <tbody><tr><td style=text-align:center;" + ">" + "&nbsp" + (i + 1) + "</td>" + "<td style=text-align:left;" + ">"
            + "&nbsp" + Records[i][0] + "</td>" + "<td style=text-align:right;" + ">" + "&nbsp" + Records[i][5] + "</td>" + "<td style=text-align:right;" + ">"
            + "&nbsp" + Records[i][6] + "</td></tr></tbody>");

    }
    $("#grvMonthlyAcc").append(" <tfoot><tr><td style=text-align:center;></td>" + "<td style=text-align:right;font-weight:bold;>"
            + "TOTAL" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + resSum + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
            + "&nbsp" + paymentSum + "</td></tr></tfoot>");



    //Target vs Ee

    //$("#grvMonTarEx").empty();
    //$("#grvMonTarEx").append(" <thead class=grvHeader style=" + "font-weight:bolder;" + "><tr><th>SL</th><th>MONTH</th>" +
    //    "<th>CONS. TARGET</th><th>EXECUTION</th></tr></thead>");



    //for (var i = 0 ; i < tbLen; i++) {
    //    if (i >= tbLen)
    //        break;
    //    $("#grvMonTarEx").append(" <tbody><tr><td style=text-align:center;" + ">" + "&nbsp" + (i + 1) + "</td>" + "<td style=text-align:left;" + ">"
    //        + "&nbsp" + Records[i][0] + "</td>" + "<td style=text-align:right;" + ">" + "&nbsp" + Records[i][7] + "</td>" + "<td style=text-align:right;" + ">"
    //        + "&nbsp" + Records[i][8] + "</td></tr></tbody>");

    //}
    //$("#grvMonTarEx").append(" <tfoot><tr><td style=text-align:center;></td>" + "<td style=text-align:right;font-weight:bold;>"
    //        + "TOTAL" + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">" + "&nbsp" + TargetSum + "</td>" + "<td style=text-align:right;font-weight:bold;" + ">"
    //        + "&nbsp" + ExecSum + "</td></tr></tfoot>");

}

function SelectFromDate() {
    fromDate = $("#txtDatefrom").val();
}

function CreateBarChart() {
    InitCanvas();
    var salesbrchrt = document.getElementById('salesbrchrt').getContext('2d');

    var purchasechrt = document.getElementById('purchasechrt').getContext('2d');
   
    var accchrt = document.getElementById('accchrt').getContext('2d');

    //var TarExchrt = document.getElementById('TarExchrt').getContext('2d');
    

    var data = {

        labels: [],
        datasets: [
            {
                label: "Sales",
                fillColor: "rgba(51,255,47,0.5)",
                strokeColor: "rgba(220,220,220,0.8)",
                highlightFill: "rgba(0,204,0,0.75)",
                highlightStroke: "rgba(220,220,220,1)",
                data: []
            },
            {
                label: "Collection",
                fillColor: "rgba(151,187,205,0.5)",
                strokeColor: "rgba(151,187,205,0.8)",
                highlightFill: "rgba(151,187,205,1)",
                highlightStroke: "rgba(151,187,205,1)",
                data: []
            }
        ]
    };
    for (var i = 0; i < tbLen; i++)
        data.labels[i] = grphRecords[i][0];  //Math.ceil(value.tllpuramt).toLocaleString('en-US', { minimumFractionDigits: 0 }); 
    for (var i = 0; i < tbLen; i++)
        data.datasets[0].data[i] = grphRecords[i][1] / 100000;  //(grphRecords[i][1] / 100000).toLocaleString('en-US', { minimumFractionDigits: 2 });
    for (var i = 0; i < tbLen; i++)
        data.datasets[1].data[i] = grphRecords[i][2] / 100000;

    salesbrchrt = new Chart(salesbrchrt).Bar(data);
    


    //pur grp
    var data = {

        labels: [],
        datasets: [
            {
                label: "Purchase",
                fillColor: "rgba(51,255,47,0.5)",
                strokeColor: "rgba(220,220,220,0.8)",
                highlightFill: "rgba(0,204,0,0.75)",
                highlightStroke: "rgba(220,220,220,1)",
                data: []
            },
            {
                label: "Payment",
                fillColor: "rgba(151,187,205,0.5)",
                strokeColor: "rgba(151,187,205,0.8)",
                highlightFill: "rgba(151,187,205,1)",
                highlightStroke: "rgba(151,187,205,1)",
                data: []
            }
        ]
    };
    for (var i = 0; i < tbLen; i++)
        data.labels[i] = grphRecords[i][0];
    for (var i = 0; i < tbLen; i++)
        data.datasets[0].data[i] = grphRecords[i][3] / 100000;
    for (var i = 0; i < tbLen; i++)
        data.datasets[1].data[i] = grphRecords[i][4] / 100000;
    
    purchasechrt = new Chart(purchasechrt).Bar(data);

    //acc grp
    var data = {

        labels: [],
        datasets: [
            {
                label: "Recepit",
                fillColor: "rgba(51,255,47,0.5)",
                strokeColor: "rgba(220,220,220,0.8)",
                highlightFill: "rgba(0,204,0,0.75)",
                highlightStroke: "rgba(220,220,220,1)",
                data: []
            },
            {
                label: "Payment",
                fillColor: "rgba(151,187,205,0.5)",
                strokeColor: "rgba(151,187,205,0.8)",
                highlightFill: "rgba(151,187,205,1)",
                highlightStroke: "rgba(151,187,205,1)",
                data: []
            }
        ]
    };
    for (var i = 0; i < tbLen; i++)
        data.labels[i] = grphRecords[i][0];
    for (var i = 0; i < tbLen; i++)
        data.datasets[0].data[i] = grphRecords[i][5] / 100000;
    for (var i = 0; i < tbLen; i++)
        data.datasets[1].data[i] = grphRecords[i][6] / 100000;

    accchrt = new Chart(accchrt).Bar(data);

    ////Target grp
    //var data = {

    //    labels: [],
    //    datasets: [
    //        {
    //            label: "Target",
    //            fillColor: "rgba(51,255,47,0.5)",
    //            strokeColor: "rgba(220,220,220,0.8)",
    //            highlightFill: "rgba(0,204,0,0.75)",
    //            highlightStroke: "rgba(220,220,220,1)",
    //            data: []
    //        },
    //        {
    //            label: "Execution",
    //            fillColor: "rgba(151,187,205,0.5)",
    //            strokeColor: "rgba(151,187,205,0.8)",
    //            highlightFill: "rgba(151,187,205,1)",
    //            highlightStroke: "rgba(151,187,205,1)",
    //            data: []
    //        }
    //    ]
    //};
    //for (var i = 0; i < tbLen; i++)
    //    data.labels[i] = grphRecords[i][0];
    //for (var i = 0; i < tbLen; i++)
    //    data.datasets[0].data[i] = grphRecords[i][7] / 100000;
    //for (var i = 0; i < tbLen; i++)
    //    data.datasets[1].data[i] = grphRecords[i][8] / 100000;

    //TarExchrt = new Chart(TarExchrt).Bar(data);

  
}
function InitCanvas() {
    $('#saleCanvDiv').html('');
    $('#saleCanvDiv').html('<canvas id="salesbrchrt" width="750" height="290"></canvas>');

    $('#purCanvDiv').html('');
    $('#purCanvDiv').html('<canvas id="purchasechrt" width="750" height="290"></canvas>');

    $('#accCanvDiv').html('');
    $('#accCanvDiv').html('<canvas id="accchrt" width="750" height="290"></canvas>');

    //$('#TarExCanvDiv').html('');
    //$('#TarExCanvDiv').html('<canvas id="TarExchrt" width="750" height="290"></canvas>');
}
