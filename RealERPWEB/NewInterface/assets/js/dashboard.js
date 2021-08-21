

document.addEventListener("DOMContentLoaded", function(){   
    $.ajax({
        type: "POST",
        url: "../Dashboard.aspx/GetTopData",   
        contentType: "application/json; charset=utf-8",
        data: {},
        dataType: "json",
        success: function (response) {
            //console.log(JSON.parse(response.d));
            let data = JSON.parse(response.d);            
            console.log(data);
            document.getElementById("todaywrkcount").innerHTML=data.TopActivity;
            document.getElementById("useroffline").innerHTML=data.UserOffline;
            let sallist=data.salGraph;
            Graph("sales", data);
            let salbtn= document.querySelector("#sale");
            let purbtn= document.querySelector("#purchase");
            let accbtn= document.querySelector("#accounts");
            let construction= document.querySelector("#construction");
            let subconstractor= document.querySelector("#subconstractor");
            salbtn.addEventListener("click", function () {
                Graph("sales", data);
            });
            purbtn.addEventListener("click", function () {
                Graph("purchase", data);
            });
            accbtn.addEventListener("click", function () {
                Graph("accounts", data);
            });
            construction.addEventListener("click", function () {
                Graph("const", data);
            });
            subconstractor.addEventListener("click", function () {
                Graph("subcons", data);
            });


        },
        failure: function (response) {
            //  alert(response);
            console.log("f");
        }
    });
});

const primaryColor = '#4834d4'
const warningColor = '#f0932b'
const successColor = '#6ab04c'
const dangerColor = '#eb4d4b'

var dropdown = document.getElementsByClassName("drop-down-nav");
let i;
for (i = 0; i < dropdown.length; i++) {
  dropdown[i].addEventListener("click", function() {
  
  var dropdownContent = this.nextElementSibling;
  if (dropdownContent.style.display === "block") {
  dropdownContent.style.display = "none";
  } else {
  dropdownContent.style.display = "block";
  }
  });
}

const adminSection = document.getElementById("admin");
const userSection = document.getElementById("user");

function myFunction() {
  var x = document.getElementById("sel1").value;
  if (x === "Admin") {
    adminSection.style.display = "block";
    userSection.style.display = "none";
  }
  else {
    adminSection.style.display = "none";
    userSection.style.display = "block";
  }
}
const graphHead=document.querySelector("#graphhead");
function Graph(type, list){
    console.log(list);
    let graphData=[];
    let grapghactData=[];
    let legend1="";
    let legend2="";
    if(type=="sales"){
        graphHead.innerHTML="Sales Monthly Target Vs Actual";
        legend1="Target";
        legend2="Actual";
        graphData=[];
        grapghactData=[];
        list.salGraph.forEach(function(item){           
            graphData.push(item.targtsaleamtcore);
            grapghactData.push(item.ttlsalamtcore);
        })
    }
    else if(type=="purchase"){
        graphHead.innerHTML="Monthly Purchase Vs Payment";
        legend1="Purchase";
        legend2="Payment";
        graphData=[];
        grapghactData=[];
        list.purGraph.forEach(function(item){           
            graphData.push(item.ttlsalamtcore);
            grapghactData.push(item.tpayamtcore);
        })
    }
    else if(type=="accounts"){
        graphHead.innerHTML="Monthly Receipts Vs Payment";
        legend1="Receipt";
        legend2="Payment";
        graphData=[];
        grapghactData=[];
        list.accGraph.forEach(function(item){
            graphData.push(item.cramcore);
            grapghactData.push(item.dramcore);
        })
    }
    else if (type == "const") {
        graphHead.innerHTML="Construction Monthly Target Vs Actual";
        legend1 = "Target";
        legend2 = "Actual";
        graphData=[];
        grapghactData=[];
        list.consGraph.forEach(function (item) {
            graphData.push(item.taramtcore);
            grapghactData.push(item.examtcore);
        })
    }
    else if (type == "subcons") {
        graphHead.innerHTML = "Sub-Contractor Bill Vs Payment";
        legend1 = "Bill";
        legend2 = "Payment";
        graphData = [];
        grapghactData = [];
        list.sconGraph.forEach(function (item) {
            graphData.push(item.tcbamtcore);
            grapghactData.push(item.tcbpayamtcore);
        })
    }
    
    
    var chart= Highcharts.chart('container', {
        chart: {
            type: 'column'
        },
        title: {
            text: ''
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
                text: 'Amount (Crore)'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
              '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [{
            name: legend1,
            data:graphData

        }, {
            name: legend2,
            data: grapghactData

        }]
    });
    let w = $(".graph-main").width();
    chart.setSize(w);
  

    const elem = $(".graph-main")[0];

    let resizeObserver = new ResizeObserver(function () {
        chart.setSize(w);
        w = $(".graph-main").width();
    });

    resizeObserver.observe(elem);
}
$(function () {
    // Sidebar toggle behavior
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar, #content').toggleClass('active')

    });
});