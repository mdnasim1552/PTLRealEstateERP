function RealERPScript() {

    

    this.GetModule = function (ModuleId, InputName) {

        var results = new Array();



        jQuery.ajax({
            type: "POST",
            async: false,
           url: 'Service/UserService.asmx/GetModule',
           // url: 'UserService.asmx/GetModule',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'ModuleId': ModuleId, 'InputName': InputName }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });


        //jQuery.ajax({
        //    type: "POST",
        //    async: false,
        //    url: 'Service/UserService.asmx/GetModule',
        //    data: Sys.Serialization.JavaScriptSerializer.serialize({ 'ModuleId': ModuleId, 'InputName': InputName }),
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (response) {
        //        if (typeof (response) == 'string')
        //            results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
        //        else
        //            results = response.d;
        //    },
        //    failure: function (msg) {
        //        alert('Service Error');
        //    },
        //    error: function (res, status) {
        //        if (status == "error") {
        //            alert(res.responseText);
        //        }
        //    }
        //});

        return results;

    };

    this.GetProjectName = function (SeachProject) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: '../Service/UserService.asmx/GetProjectName',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'SeachProject': SeachProject }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };
    this.GetCompInf = function (date) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: '../../Service/UserService.asmx/GetCompInf',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'date': date }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };



    this.GetEmpMonEva = function (empid, frmdate, todate) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: '../Service/UserService.asmx/GetEmpMonEva',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'empid': empid, 'frmdate': frmdate, 'todate': todate }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };

    this.GetEmpMonEva02 = function (empid, frmdate, todate, type) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: '../Service/UserService.asmx/GetEmpMonEva02',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'empid': empid, 'frmdate': frmdate, 'todate': todate, 'type': type }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };



    this.GetEmpMonEva04 = function (empid, frmdate, todate) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: '../Service/UserService.asmx/GetEmpMonEva04',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'empid': empid, 'frmdate': frmdate, 'todate': todate }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };


    this.GetMonthlyGraph = function (date) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: '../../Service/UserService.asmx/GetMonthlyGraph',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'date': date }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };


    this.GetShortCut = function () {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: 'Service/UserService.asmx/GetShortCut',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };




    this.DupAllMobile = function (comcod, sircode, mobile) {

        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: 'CrmClientInfo.aspx/CheckMobile',
           
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'comcod': comcod, 'sircode': sircode, 'mobile': mobile}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });

        return results;

    };



    this.GetNotifications = function (userid, url) {
        var results = new Array();
        jQuery.ajax({
            type: "POST",
            async: false,
            url: url,
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'userid': userid}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    console.log(res.responseText);
                    alert(res.responseText);
                }
            }
        });

       

        return results;



    };





    this.GetProspective = function (comcod, empid, type) {

        var results = new Array();



        jQuery.ajax({
            type: "POST",
            async: false,
            url: 'MktSampleNoteSheet.aspx/GetProspective',
            // url: 'UserService.asmx/GetModule',
            data: Sys.Serialization.JavaScriptSerializer.serialize({ 'comcod': comcod,'empid': empid, 'type': type }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (typeof (response) == 'string')
                    results = Sys.Serialization.JavaScriptSerializer.deserialize(response).d;
                else
                    results = response.d;
            },
            failure: function (msg) {
                alert('Service Error');
            },
            error: function (res, status) {
                if (status == "error") {
                    alert(res.responseText);
                }
            }
        });



        return results;

    };




 }