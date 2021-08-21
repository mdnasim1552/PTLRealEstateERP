

function Search() {
    var srchstr = $('#txtSrchSalesTeam').val();
    var date = $('#txttodate').val();
  
    $.ajax({

        type: "POST",
        async: true,
        url: "../S_05_MyPage/RptEmpMonthWiseEva03.asmx/GetEmployeeList02",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: Sys.Serialization.JavaScriptSerializer.serialize({ 'txtSrchSalesTeam': srchstr, 'queryString': qryString, 'date': date }),
        success: function onSuccess(data) {

            //alert(data.d[0].empname);
            $('#ddlEmpid').empty();
            for (var i = 0; i < data.d.length;i++)
             $('#ddlEmpid').append($("<option></option>").val(data.d[i].empid).html(data.d[i].empname));
            
           
        }

    });

}



function GetfilterEmployee() {
    

    var srchemp = $('#txtSrchSalesTeam').val();
    $.ajax({

        
        type: "POST",
        async: true,
        url: "../S_05_MyPage/RptEmpMonthWiseEva03.asmx/GetFilterEmployeeList02",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: Sys.Serialization.JavaScriptSerializer.serialize({ 'txtSrchSalesTeam': srchemp }),
        success: function onSuccess(data) {

            //alert(data.d[0].empname);
            $('#ddlEmpid').empty();
            for (var i = 0; i < data.d.length; i++)
                $('#ddlEmpid').append($("<option></option>").val(data.d[i].empid).html(data.d[i].empname));


        }

    });

}