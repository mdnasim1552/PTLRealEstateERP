<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AdvancedSearchFilter.aspx.cs" Inherits="RealERPWEB.F_21_MKT.AdvancedSearchFilter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <script type="text/javascript" language="javascript">


        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            try {


                var gridViewScroll = new GridViewScroll({
                    elementID: "gvInfo",
                    width: 1000,
                    height: 580,
                    freezeColumn: true,
                    freezeFooter: true,
                    freezeColumnCssClass: "GridViewScrollItemFreeze",
                    freezeFooterCssClass: "GridViewScrollFooterFreeze",
                    freezeHeaderRowCount: 1,
                    freezeColumnCount: 8,

                });

                gridViewScroll.enhance();

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $('.chzn-select').chosen({ search_contains: true });
                var comcod =<%=this.GetComeCode()%>;


                $('.lbtnschedule').click(function () {


                    var subject = $(this).parent().find('#txtdate').val();
                    var lblcdate = $(this).parent().find('#lblcdate').val();

                });


                $('.datepicker').datepicker({
                    format: 'mm/dd/yyyy',
                });



                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $(".chosen-select").chosen({
                    search_contains: true,
                    no_results_text: "Sorry, no match!",
                    allow_single_deselect: true
                });
                $('.chosen-continer').css('width', '600px');

                $('.chzn-select').chosen({ search_contains: true });


                var comcod =<%=this.GetComeCode()%>;

                var gcod;

                var arrgschcodl = $('#<%=this.gvInfo.ClientID %>').find('[id$="lblgvItmCodedis"]');
                var arrgschval = $('#<%=this.gvInfo.ClientID %>').find('input:text[id$="txtgvValdis"]');
                var arrgsschcheckbox = $('#<%=this.gvInfo.ClientID %>').find('input:text[id$="ChkBoxLstFollow"]');
                var txtnfollowupdate, checkboxlastfollowup;
                for (var i = 0; i < arrgschcodl.length; i++) {


                    gcod = $(arrgschcodl[i]).text();
                    var number, numberlq, numbercom;
                    switch (gcod) {


                        //Company
                        case '810100101007':
                            numbercom = i;
                            break;

                        //Last Followup
                        case '810100101020':
                            number = i;
                            break;


                        case '810100101014':
                            numberlq = i;
                            break;




                    }

                }

                var txtgvdValdis = '#ContentPlaceHolder1_gvInfo_txtgvdValdis_' + number;
                var ChkBoxLstFollow = 'ContentPlaceHolder1_gvInfo_ChkBoxLstFollow_' + (number - 1);
                $(txtgvdValdis).change(function () {
                    var followupdate = $(this).val();
                    var lastfollowup = "";
                    $('input[type=checkbox][id^="' + ChkBoxLstFollow + '"]:checked').each(function (index, item) {

                        lastfollowup = $(item).val();

                    });

                    if (lastfollowup.length > 0) {

                        funschedulenumber(comcod, followupdate, lastfollowup, number);
                    }

                });


                $('#' + ChkBoxLstFollow).change(function () {
                    var followupdate = $(txtgvdValdis).val();
                    var lastfollowup = "";
                    alet(followupdate);
                    $('input[type=checkbox][id^="' + ChkBoxLstFollow + '"]:checked').each(function (index, item) {

                        lastfollowup = $(item).val();

                    });

                    if (lastfollowup.length > 0) {

                        funschedulenumber(comcod, followupdate, lastfollowup, number);
                    }

                });

                //Company


                var ddlcompany = '#ContentPlaceHolder1_gvInfo_ddlCompany_' + numbercom;
                $(ddlcompany).change(function () {
                    var company = $(this).val();
                    // console.log(company);
                    funCompanyProject(comcod, company);


                });

                //Lead Reason

                var ddlvisit = '#ContentPlaceHolder1_gvInfo_ddlVisit_' + numberlq;
                $(ddlvisit).change(function () {
                    leadquality = $(this).val();
                    funLeadReason(comcod, leadquality);


                });

                var sircode = $('#<%=this.lblnewprospect.ClientID%>').val();
                var arrgcodl = $('#<%=this.gvPersonalInfo.ClientID %>').find('[id$="lblgvItmCodeper"]');
                var arraygval = $('#<%=this.gvPersonalInfo.ClientID %>').find('input:text[id$="txtgvVal"]');
                //var codePhone = $('#<%=this.gvPersonalInfo.ClientID %>').find('input:option:selected[id$="ddlcountryPhone"]').text;
               // var codePhone = $('#<%=this.gvPersonalInfo.ClientID %>').find('[id$="ddlcountryPhone"]');
              <%-- // var codePhone = $('#<%=this.gvPersonalInfo.ClientID %>').find('input[type=select][id*=ddlcountryPhone]').value;
                //console.log(countryPhone[1]);
                console.log(arraygval);
                console.log(codePhone);
                //elementId: selected
                // var txtmobile=arraygval[1];  --%>
                var txtmobile, txtaltmobile1, txtaltmobile2, countryPhone;

                for (var i = 0; i < arrgcodl.length; i++) {


                    gcod = $(arrgcodl[i]).text();

                    switch (gcod) {

                        case '0301003':
                            txtmobile = arraygval[i];
                            break;

                        case '0301004':
                            switch (comcod) {
                                case "3315":
                                case "3316":
                                    break;

                                default:
                                    txtaltmobile1 = arraygval[i];
                                    break;
                            }
                            break;

                        case '0301005':
                            txtaltmobile2 = arraygval[i];
                            break;
                    }

                }


                //      console.log(countryPhone);
                //console.log("Nahid");

                $(txtmobile).keyup(function () {
                    var mobile = $(this).val(); if (!($.isNumeric(mobile))) {

                        alert("Mobile Number must be numeric");

                        return false;
                    }
                    // funDupMobile(comcod, sircode, mobile);

                });




                $(txtaltmobile1).keyup(function () {
                    var mobile = $(this).val();
                    if (mobile.length != 11) {
                        return false;
                    }

                    if (!($.isNumeric(mobile))) {

                        alert("Mobile Number must be numeric");

                        return false;
                    }
                    if (gcod != "0301025" && (comcod == "3315" || comcod == "3316")) {
                        //alert("test--");
                        funDupMobile(comcod, sircode, mobile);
                    }

                });


                $(txtaltmobile2).keyup(function () {

                    var mobile = $(this).val();
                    if (mobile.length != 11) {


                        return false;
                    }

                    if (!($.isNumeric(mobile))) {

                        alert("Mobile Number must be numeric");

                        return false;
                    }
                    funDupMobile(comcod, sircode, mobile);

                });
            }

            catch (e) {
                alert(e);
            }

        }
        $(document).on('click', '.panel-heading span.clickable', function (e) {



            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('fa fa-minus').addClass('fa fa-plus');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('fa fa-plus').addClass('fa fa-minus');
            }
        });
        function funDupMobile(comcod, sircode, mobile) {

            try {
                $.ajax({

                    url: "CrmClientInfo.aspx/CheckMobile",
                    type: "POST",
                    data: '{comcod:"' + comcod + '", sircode:"' + sircode + '", mobile:"' + mobile + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //  async: false,
                    success: function (data) {
                        var jdata = JSON.parse(data.d);

                        var mesult = jdata.result;
                        if (!mesult) {
                            alert(jdata.Message);

                        }
                    }
                });

            }


            catch (e) {
                alert(e.message);

            }

        }

        function funReschedule(cdate, number) {
            try {


                //var  comdate =$('#txtcomdate'+number).val();
                var comcod =<%=this.GetComeCode()%>;
                var empid =<%=this.GetEmpID()%>;
                var proscod = $('#<%=this.lblproscod.ClientID%>').val();



                $.ajax({
                    type: "POST",
                    url: "AdvancedSearchFilter.aspx/GetReschedule",
                    data: '{comcod:"' + comcod + '", empid:"' + empid + '",  proscod: "' + proscod + '", cdate:"' + cdate + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",


                    success: function (response) {

                        var data = JSON.parse(response.d);
                        funDataBind(data);
                        console.log(data);
                        //var date=data[0].gdesc1;
                        //alert(date);
                        $('#lbtntfollowup').click();


                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }




        }




        function funCompanyProject(comcod, company) {
            try {
                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/GetCompanyProject",
                    data: '{comcod:"' + comcod + '", company:"' + company + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d);

                        var arrgschcodl = $('#<%=this.gvInfo.ClientID %>').find('[id$="lblgvItmCodedis"]');
                        var numberrl;

                        for (var i = 0; i < arrgschcodl.length; i++) {

                            gcod = $(arrgschcodl[i]).text();
                            switch (gcod) {

                                case '810100101003':
                                    numberrl = i;
                                    break;

                            }

                        }



                        //    ContentPlaceHolder1_gvInfo_checkboxReson_6_chzn

                        var ddlProject = '#ContentPlaceHolder1_gvInfo_ddlProject_' + numberrl;

                        //console.log(ddlProject);
                        $(ddlProject).html('');
                        $.each(data, function (key, data) {

                            $(ddlProject).append("<option value='" + data.actcode + "'>" + data.actdesc + "</option>");
                        });






                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }



        }



        function funLeadReason(comcod, leadquality) {

            try {
                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/GetLeadReason",
                    data: '{comcod:"' + comcod + '", leadquality:"' + leadquality + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var data = JSON.parse(response.d);

                        var arrgschcodl = $('#<%=this.gvInfo.ClientID %>').find('[id$="lblgvItmCodedis"]');
                        var numberrl;

                        for (var i = 0; i < arrgschcodl.length; i++) {

                            gcod = $(arrgschcodl[i]).text();
                            switch (gcod) {

                                case '810100101012':
                                    numberrl = i;
                                    break;
                            }
                        }

                        //    ContentPlaceHolder1_gvInfo_checkboxReson_6_chzn
                        var ddllreason = '#ContentPlaceHolder1_gvInfo_checkboxReson_' + numberrl;
                        $(ddllreason).html('');
                        $.each(data, function (key, data) {

                            $(ddllreason).append("<option value='" + data.gcod + "'>" + data.gdesc + "</option>");
                        });

                        // console.log(data);
                        //  funDataBind(data);                      

                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }


        }


        function funDupAllMobile() {

            try {


                //Company Name change
                var comcod =<%=this.GetComeCode()%>;
                var sircode = $('#<%=this.lblnewprospect.ClientID%>').val();
                var arrgcodl = $('#<%=this.gvPersonalInfo.ClientID %>').find('[id$="lblgvItmCodeper"]');
                var arraygval = $('#<%=this.gvPersonalInfo.ClientID %>').find('input:text[id$="txtgvVal"]');
                var arryccc = $('#<%=this.gvPersonalInfo.ClientID %>').find('input:select[id$="ddlcountryPhone"]');

                console.log(sircode + "" + arrgcodl + "" + arraygval + "" + arryccc);

                var cc0 = "";
                var cc1 = "";
                var cc2 = "";
                var number = "";
                var gval;
                //number = gval.Length > 0 ? gval + "," : "";
                //number = number + (gval.Length > 0 ? gval + "," : "");
                //number = number + (gval.Length > 0 ? gval + "," : "");
                //number = number.Length > 0 ? number.Substring(0, number.Length - 1) : number;

                for (var i = 0; i < arrgcodl.length; i++) {


                    var gcod = $(arrgcodl[i]).text();

                    var number;
                    switch (gcod) {

                        case '0301003':
                            gval = $(arraygval[i]).val();
                            cc0 = $(arryccc[i]).val();
                            number = gval.length > 0 ? gval + "," : "";
                            console.log(cc0);

                            break;


                        case '0301004':

                            switch (comcod) {
                                case '3315':
                                case '3316':
                                    break;

                                default:
                                    gval = $(arraygval[i]).val();
                                    number = number + (gval.length > 0 ? gval + "," : "");
                                    break;
                            }
                            break;

                        case '0301005':
                            gval = $(arraygval[i]).val();
                            number = number + (gval.length > 0 ? gval + "," : "");
                            break;
                    }

                }
                alert(cc0);

                number = number.length > 0 ? number.substring(0, number.length - 1) : number;
                var objchkmob = new RealERPScript();
                var res = objchkmob.DupAllMobile(comcod, sircode, number);
                var jdata = JSON.parse(res);
                if (!jdata.result) {


                    alert(jdata.Message);
                    return false;

                }
                else {

                    return true;

                }
            }


            catch (e) {
                // alert(e.message);

            }

        }


e>
        body {
function funDataBind(data) {
            try {
                var number = 0;
                $.each(data, function (index, data) {

                    var gcod = data.gcod;


                    switch (gcod) {

                        case "810100101001": //Followup Date                        
                            var txtgvdValdis = '#ContentPlaceHolder1_gvInfo_txtgvdValdis_' + number;
                            $(txtgvdValdis).val(data.gdesc1);
                            $(txtgvdValdis).attr("disabled", true);

                            //var dropdown
                            var dtimehour = data.gtime;
                            var ddlhour = '#ContentPlaceHolder1_gvInfo_ddlhour_' + number;
                            $(ddlhour).val(dtimehour.substr(0, 2));
                            $(ddlhour).attr("disabled", true);

                            var ddlmin = '#ContentPlaceHolder1_gvInfo_ddlMmin_' + number;
                            $(ddlmin).val(dtimehour.substr(3, 2));
                            $(ddlmin).attr("disabled", true);

                            var ddlslb = '#ContentPlaceHolder1_gvInfo_ddlslb_' + number;
                            $(ddlslb).val(dtimehour.substr(6, 2));
                            $(ddlslb).attr("disabled", true);
                            break;

                        case "810100101002": //New Followup
                            var ChkBoxLstFollow = '#ContentPlaceHolder1_gvInfo_ChkBoxLstFollow_' + number;
                            //alert(data.gdesc1);
                            var newfollowup = data.gdesc1;
                            if (newfollowup.length <= 7) {


                                $('' + ChkBoxLstFollow + '> input').each(function (index, item) {

                                    if ($(item).val() == newfollowup) {
                                        $(item).attr('checked', true);
                                    }
                                    else {

                                        $(item).attr('checked', false);

                                    }


                                });

                            }
                            else {
                                var ar = new Array();
                                // alert(newfollowup);
                                var j = 0;
                                for (i = 0; i < newfollowup.length; i = i + 7) {
                                    ar[j++] = newfollowup.substr(i, 7);
                                }

                                //console.log(ar);
                                //alert(ar.length);

                                for (i = 0; i < ar.length; i++) {

                                    $('' + ChkBoxLstFollow + '> input').each(function (index, item) {
                                        if ($(item).val() == ar[i]) {
                                            $(item).attr('checked', true);
                                        }
                                        else {
                                            $(item).attr('checked', false);

                                        }

                                    });


                                }
                            }

                            break;



                        case "810100101007": //Company
                            console.log(data.gdesc1);

                            var ddlcompany = '#ContentPlaceHolder1_gvInfo_ddlCompany_' + number;

                            $(ddlcompany + ' > option').each(function (index, item) {
                                if ($(item).val() == data.gdesc1) {
                                    $(item).attr("selected", true);
                                }


                            });



                            break;


                        case "810100101003": //Project

                            var ddlProject = '#ContentPlaceHolder1_gvInfo_ddlProject_' + number;



                            $(ddlProject + ' > option').each(function (index, item) {
                                if ($(item).val() == data.gdesc1) {
                                    $(item).attr("selected", true);
                                }


                            });




                            break;








                        case "810100101019"://Follow

                            var ChkBoxLstFollow = '#ContentPlaceHolder1_gvInfo_ChkBoxLstFollow_' + number;
                            var newfollowup = data.gdesc1;
                            if (newfollowup.length = 7) {

                                $('' + ChkBoxLstFollow + '> input').each(function (index, item) {
                                    if ($(item).val() == newfollowup) {
                                        $(item).attr('checked', true);

                                    }
                                    else {

                                        $(item).attr('checked', false);

                                    }


                                });

                            }
                            break;





                        case "810100101016": //Status



                            var ChkBoxLstStatus = '#ContentPlaceHolder1_gvInfo_ChkBoxLstStatus_' + number;
                            var status = data.gdesc1;
                            if (status.length = 7) {

                                $('' + ChkBoxLstStatus + '> input').each(function (index, item) {
                                    if ($(item).val() == status) {
                                        $(item).attr('checked', true);

                                    }
                                    else {
                                        $(item).attr('checked', false);


                                    }


                                });

                            }


                            break;

                        case "810100101018": //PARTICIPANTS  




                            //var ddlParticipant='#ContentPlaceHolder1_gvInfo_ddlParticdis_'+number;  
                            //var participant=data.gdesc1;


                            //if(participant.length=12)
                            //{ 

                            //    var inci=1;
                            //    $('#ContentPlaceHolder1_gvInfo_ddlParticdis_4_chzn .chzn-choices').html('');

                            //    $(''+ddlParticipant+' > option').each(function (index,item) 
                            //    {  


                            //        alert($(item).text());
                            //        if($(item).val()==participant)
                            //        {




                            //            $('.chzn-choices').append('<li class="search-choice" id="ContentPlaceHolder1_gvInfo_ddlParticdis_4_chzn_c_'+inci+'"><span>'+$(item).text()+'</span><a href="javascript:void(0)" class="search-choice-close" rel="'+inci+'"></a></li>')

                            //            $(' .chzn-choices .chzn-results #ContentPlaceHolder1_gvInfo_ddlParticdis_4_chzn_o_'+inci-1+'').removeClass('active-result');
                            //            $('.chzn-choices .chzn-results #ContentPlaceHolder1_gvInfo_ddlParticdis_4_chzn_o_'+inci-1+'').addClass('result-selected');

                            //        }
                            //        else{
                            //            $('.chzn-choices .chzn-results #ContentPlaceHolder1_gvInfo_ddlParticdis_4_chzn_o_'+inci-1+'').removeClass('result-selected');
                            //            $('.chzn-choices .chzn-results #ContentPlaceHolder1_gvInfo_ddlParticdis_4_chzn_o_'+inci-1+'').addClass('active-result');
                            //        }

                            //        inci++;
                            //    });



                            //    $('.chzn-choices').append('<li class="search-field"><input type="text" value="Choose Participant......" class="" autocomplete="off" style="width: 25px;"></li>');







                            // }





                            break;



                        case "810100101015": //Summary
                        case "810100101025": //Subject

                            var txtgvdValdis = '#ContentPlaceHolder1_gvInfo_txtgvValdis_' + number;
                            $(txtgvdValdis).val(data.gdesc1);
                            break;


                        case "810100101020": //next Followup date
                            var txtgvdValdis = '#ContentPlaceHolder1_gvInfo_txtgvdValdis_' + number;
                            $(txtgvdValdis).val(data.gdesc1);
                            //var dropdown
                            var dtimehour = data.gtime;
                            var ddlhour = '#ContentPlaceHolder1_gvInfo_ddlhour_' + number;
                            $(ddlhour).val(dtimehour.substr(0, 2));
                            var ddlmin = '#ContentPlaceHolder1_gvInfo_ddlMmin_' + number;
                            $(ddlmin).val(dtimehour.substr(3, 2));
                            var ddlslb = '#ContentPlaceHolder1_gvInfo_ddlslb_' + number;
                            $(ddlslb).val(dtimehour.substr(6, 2));


                            break;


                        default:


                            break;

                    }
                    number++;


                });
            }



            catch (e) {
                alert(e.message);

            }


        }            font-family: "Century Gothic";
        }


        .panel {
            margin-bottom: 20px;
            background-color: #fff;
            border: 1px solid transparent;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0,0,0,.05);
            box-shadow: 0 1px 1px rgba(0,0,0,.05);
        }

        .panel-default {
            border-color: #ddd;
        }

            .panel-default > .panel-heading {
                color: #333;
                background-color: #f5f5f5;
                border-color: #ddd;
            }

        .panel-heading {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        .panel-body {
            padding: 15px;
        }


        hr {
            border-color: lightgray;
        }


        .brradius {
            border-bottom-left-radius: 0;
            border-bottom-right-radius: 0;
        }

        .modal .modal-body {
            max-height: 75vh;
            overflow-y: auto;
        }

        .tblborder {
            border: none;
        }

            .tblborder td {
                border: none;
            }

        .visibleshow .grvHeader, .visibleshow .grvFooter {
            display: none;
        }

        .container-data {
            box-sizing: border-box;
        }

        @media (max-width: 1230px) {
            .column {
                width: 98%;
            }
        }

        @media (min-width: 1231px) {
            .column {
                width: 48%;
            }
        }
        /* Alert Message */
        .popup-container {
            display: flex;
            justify-content: center;
            visibility: hidden;
        }



        .popup {
            position: absolute;
            z-index: 2;
            top: 20px;
            border: 1px solid blue;
            border-radius: 5px;
            display: flex;
            justify-content: space-between;
        }

        .zindexset {
            position: relative;
            z-index: 1;
        }

        .icons {
            display: inline;
            padding: 10px 20px;
            background: blue;
            color: white;
        }

        .msg-text {
            display: inline;
            text-align: center;
            flex-grow: 1;
            background-color: white;
            padding: 10px 20px;
        }

        .bgrnd {
            background-color: #346;
        }

        .card-header span {
            font-size: 14px;
            font-weight: bolder;
        }

        .btnsavefix {
            position: fixed;
            bottom: 15px;
            width: 90%;
            background: white;
            text-align: center;
        }

        .col-md-4 span {
            color: grey;
        }


        /**  modal design start **/
        .ddmlist .btn-group button {
            width: 630px;
        }

        .ddmlist .multiselect-container {
            width: 100%;
            overflow-y: scroll !important;
            max-height: 300px !important;
        }

        .chzn-container-multi .chzn-choices .search-field .default {
            color: #999;
            height: 30px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btnadd {
            margin-left: 200px;
        }

        .displayhide {
            display: none !important;
        }

        .displayshow {
            display: block;
        }

        .btncolortrash {
            background-color: darkred;
        }

            .btncolortrash:hover {
                background-color: red;
            }



        .btnalign {
            text-align: right;
        }

        .notification {
            float: left;
            width: 120px;
        }


        .marapaddingzero {
            margin: 0px 0px !important;
            padding: 0px !important;
            font-size: 12px !important;
        }

        .tSl {
            width: 40px;
        }

        .tDescription {
            width: 300px;
        }

        .tdate {
            width: 70px;
        }

        .tdatetime {
            width: 130px;
        }


        .tAssociateadeal {
            width: 120px;
        }


        .tDiscussion {
            width: 200px;
        }


        .tParticipants {
            width: 220px;
        }


        .tSubject {
            width: 100px;
        }


        .tStatus {
            width: 70px;
        }


        .classhidden {
            display: none;
        }


        .modal-dialog-full-width {
            width: 85% !important;
            height: 100% !important;
            margin: auto !important;
            padding: 0 !important;
            max-width: none !important;
        }

        .modal-content-full-width {
            height: auto !important;
            min-height: 70% !important;
            border-radius: 0 !important;
        }

        .modal-dialog-mid-width {
            width: 75% !important;
            height: 100% !important;
            margin: auto !important;
            padding: 0 !important;
            max-width: none !important;
        }

        .modal-content-mid-width {
            height: auto !important;
            min-height: 70% !important;
            border-radius: 0 !important;
        }

        .textwrap {
            word-wrap: break-word;
        }

        td, th {
            font-weight: bold;
        }

        .notifsectino .tile {
            font-size: 12px !important;
        }

        .notifsectino .list-group-item {
            padding: 2px 5px;
        }


        .notifsectino .list-group-item-body {
            font-size: 10px !important;
        }

        /*You can use [title] selector as well*/
        [data-title] {
            font-size: 30px; /*optional styling*/
            position: relative;
            cursor: help;
            width: 200px;
        }

            [data-title]:hover::before {
                content: attr(data-title);
                position: absolute;
                bottom: -26px;
                display: inline-block;
                padding: 3px 6px;
                border-radius: 2px;
                background: #000;
                color: #fff;
                font-size: 12px;
                white-space: pre-wrap;
            }

            [data-title]:hover::after {
                content: '';
                position: absolute;
                bottom: -10px;
                left: 8px;
                display: inline-block;
                color: #fff;
                border: 0px solid transparent;
                border-bottom: 0px solid #000;
            }


        tr#ContentPlaceHolder1_Cal3_daysTableHeaderRow td {
        }

        .GridViewScrollHeader TH, .GridViewScrollHeader TD, .GridViewScroll1Header TH, .GridViewScroll1Header TD, .GridViewScroll2Header TH, .GridViewScroll2Header TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }


        .GridViewScrollItem TD, .GridViewScroll1Item TD, .GridViewScroll2Item TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD, .GridViewScroll1ItemFreeze TD, .GridViewScroll2ItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD, .GridViewScroll1FooterFreeze TD, .GridViewScroll2FooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }

        .grvHeader {
            height: 38px !important;
        }

        .WrpTxt {
            white-space: normal !important;
            word-break: break-word !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .table th, .table td, card-header {
            padding: 0px;
        }

        .pnlSidebarCl {
            width: 100%;
            height: 100vh;
            position: absolute;
            right: 0;
        }

            .pnlSidebarCl .form-control {
                height: 25px;
                line-height: 25px;
                padding: 2px;
            }

        .divPnl table tr td, .divPnl table tr th {
            padding: 5px 5px;
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

            <div class="card mt-4 pb-4" runat="server" id="pnlsrc" visible="true">
                <div class="card-body">
                    <div class="row ml-2">

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <asp:Label ID="lblem" runat="server" CssClass="form-label">Associate Name</asp:Label>
                            <asp:DropDownList ID="ddlEmpid" data-placeholder="Choose Employee.." runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label1" runat="server" CssClass="form-label">Search Type</asp:Label>
                            <asp:DropDownList ID="ddlOther" runat="server" ClientIDMode="Static" CssClass="custom-select chzn-select">
                                <asp:ListItem Value="1">Prospect Name</asp:ListItem>
                                <asp:ListItem Value="2">PID</asp:ListItem>
                                <asp:ListItem Value="3">Phone</asp:ListItem>
                                <asp:ListItem Value="4">Email</asp:ListItem>
                                <asp:ListItem Value="5">NID</asp:ListItem>
                                <asp:ListItem Value="6">TIN</asp:ListItem>
                                <%--  <asp:ListItem Value="7">Prefered Area</asp:ListItem>
                                <asp:ListItem Value="8">Profission</asp:ListItem>--%>
                                <asp:ListItem Selected="True" Value="9">Choose Filter Key.........................</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" CssClass="form-label">Search </asp:Label>
                            <asp:TextBox ID="txtVal" runat="server" CssClass="form-control form-control-sm" TextMode="Search" autocomplete="off"></asp:TextBox>

                        </div>

                        <div class="col-md-1" style="margin-top: 22px">

                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-success btn-sm" OnClick="lnkbtnOk_Click" AutoPostBack="True">Show</asp:LinkButton>

                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="btnaddland" runat="server" ToolTip="Add Lead" CssClass="mt-4 btn btn-primary btn-sm  align-self-end" style="margin-top:20px;" OnClick="btnaddland_Click">Add Lead</asp:LinkButton>
                        </div>

                    </div>

                </div>

            </div>

            <div class="card pt-2 pb-2" runat="server" id="pnlempinfo" style="background-color: whitesmoke; align-content: center">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-4">
                            <div class="card">
                                <div class="pt-2 pb-2 pl-4 bg-light"><span class="font-weight-bold text-muted">Employee Information</span></div>

                                <div class="card-body" runat="server" id="engst">
                                    <img src="~/../../../Upload/UserImages/3365001.png" style="display: block; margin-left: auto; margin-right: auto; width: 30%;" alt="User Image">
                                    <table class="table table-striped table-hober tblEMPinfo mt-2">
                                        <%--                <thead>
                                        <tr>
                                            <th></th>
                                            <th></th>

                                        </tr>
                                    </thead>--%>
                                        <tbody class="">
                                            <tr>
                                                <td class="font-weight-bold">PID</td>
                                                <td>
                                                    <asp:Label ID="lblname" runat="server">N/A</asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold ">Prospect Name</td>
                                                <td>
                                                    <asp:Label ID="lblconper" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold">Primary Mobile</td>
                                                <td>
                                                    <asp:Label ID="lblmbl" runat="server"></asp:Label>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td class="font-weight-bold">Home Address</td>
                                                <td>
                                                    <asp:Label ID="lblhomead" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="font-weight-bold">Profession</td>
                                                <td>
                                                    <asp:Label ID="lblprof" runat="server"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="font-weight-bold">Status</td>
                                                <td>
                                                    <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                    <td id="pnlretrive" runat="server" visible="false">
                                                        <asp:LinkButton ID="lnkbtnRetreive" runat="server" Font-Bold="True" Height="12px" ToolTip="Retreive Prospect" Style="text-align: right" OnClientClick="javascript:return  confirm('Do You Want to Retreive Prospect?');" Text="" OnClick="lnkbtnRetreive_Click"><span><i class="fa fa-undo" Style="text-align: center"></i></span></asp:LinkButton>
                                                    </td>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td></td>

                                                <td id="pnledit" runat="server" visible="false">

                                                    <asp:LinkButton ID="lnkEdit" runat="server" Height="22px" class="btn btn-xs  text-center" Font-Bold="True" ToolTip="Edit Client Info" Text="Edit" OnClick="lnkEdit_Click"><span class=" fa   fa-edit"></span></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <asp:HiddenField ID="lblproscod" runat="server" />
                                            <asp:HiddenField ID="lbleditempid" runat="server" />
                                            <asp:HiddenField ID="lblgeneratedate" runat="server" />
                                            <asp:HiddenField ID="hiddenLedStatus" runat="server" />
                                        </tbody>
                                    </table>



                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="card mb-3">

                                <div class="pb-2 pt-2 pl-5 bg-light"><span class="font-weight-bold text-muted">Followup Summary</span></div>
                                <div id="pnlflw" runat="server" visible="false" class="card-header bg-light">
                                    <span class="font-weight-bold text-muted">
                                        <asp:LinkButton runat="server" type="button" class="btn  btn-success btn-sm mt-2" ID="lbtntfollowup" data-target="#followup" OnClick="btnqclink_Click">FollowUp</asp:LinkButton></span>
                                    <div class="col-3" runat="server" id="divexland">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lbllandname" Font-Size="16px" class="form-control bg-danger font-weight-bold text-white margin-top30px" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body" style="min-height: 300px" id="pnlfollowup" runat="server">
                                    <asp:Repeater ID="rpclientinfo" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <div class="col-md-12  col-lg-12 ">


                                                <div class="col-sm-12 panel" style="background-color: whitesmoke">

                                                    <div class=" col-sm-12 pt-3 pb-3">

                                                        <p>
                                                            <strong><%# DataBinder.Eval(Container, "DataItem.prosdesc")%></strong> <%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString() %>  on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %><br>




                                                            <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString() %><br>


                                                            <strong>Summary:</strong><span class="textwrap"><%# DataBinder.Eval(Container, "DataItem.discus").ToString() %></span><br>



                                                            <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString() %> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                            <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString() %>





                                                            <br>
                                                        </p>



                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12 mb-2">




                                                            <asp:LinkButton runat="server" type="button" class="btn  btn-success btn-sm mt-2" ID="lbtntfollowupf" data-target="#followup" OnClick="btnqclink_Clck"><span>FollowUp</span></asp:LinkButton>
        FollowUp                          <button type="button" class="btn  btn-primary btn-sm" id="lbtnreschedule" style="margisuccesspx" oxslick="funReschedule('tainer, "DataItem.cdate").ToString()%>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>')">Re-Schdule</button>
                                                            <%--<button type="button" class="btn  btn-success btn-xs" >Re-Schdule</button>--%>




                                                        </div>

                                                    </div>

                                                </div>




                                            </div>

                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>


                            </div>




                        </div>

                    </div>

                </div>
            </div>
            <div class="col-md-2">
            </div>
            <div class="col-md-10" id="pnlSidebar" runat="server" visible="false">
                <div class="divPnl">
                    <div class="card pnlSidebarCl mt-4">
                        <div class="modal-content">
                            <div class="modal-header bg-light pt-2 pb-2 ml-2">
                                <div class="bg-light"><span class="font-weight-bold text-muted" style="padding: 0px;">Add FollowUp</span></div>

                                <asp:LinkButton ID="pnlsidebarClose" OnClick="pnlsidebarClose_Click" ToolTip="Close the Window" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>
                            </div>
                            <div class="modal-body" id="followup">

                                <asp:GridView ID="gvInfo" runat="server" AllowPaging="false"
                                    AutoGenerateColumns="False" ShowFooter="true" ClientIDMode="Static"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNodis" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-CssClass="displayhide">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCodedis" ClientIDMode="Static" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="70px"></asp:Label>
                                                <asp:Label ID="lblgvTime" runat="server" BorderWidth="0" BackColor="Transparent" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtime")) %>'></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcGrpdis" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc"))  + "</B>" %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1dis" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle VerticalAlign="Middle" Width="130px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lgpdis" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                    Width="5px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgvaldis" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>


                                            <ItemTemplate>



                                                <asp:TextBox ID="txtgvValdis" runat="server" BorderWidth="0" BackColor="Transparent" Font-Size="14px" Style="width: 80px; float: left;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdValdis"></cc1:CalendarExtender>

                                                <asp:TextBox ID="txtgvdValdis" CssClass="disable_past_dates" runat="server" BorderWidth="0" Style="width: 80px; float: left;" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvdValdis_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdValdis"></cc1:CalendarExtender>

                                                <asp:Panel ID="pnlTime" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddlhour" runat="server" CssClass="inputTxt ddlPage" Style="width: 50px; line-height: 22px;">
                                                        <asp:ListItem Value="01">01</asp:ListItem>
                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                        <asp:ListItem Value="09" Selected="True">09</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlMmin" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                                        <asp:ListItem Value="00">00</asp:ListItem>
                                                        <asp:ListItem Value="01">01</asp:ListItem>
                                                        <asp:ListItem Value="02">02</asp:ListItem>
                                                        <asp:ListItem Value="03">03</asp:ListItem>
                                                        <asp:ListItem Value="04">04</asp:ListItem>
                                                        <asp:ListItem Value="05">05</asp:ListItem>
                                                        <asp:ListItem Value="06">06</asp:ListItem>
                                                        <asp:ListItem Value="07">07</asp:ListItem>
                                                        <asp:ListItem Value="08">08</asp:ListItem>
                                                        <asp:ListItem Value="09">09</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                        <asp:ListItem Value="17">17</asp:ListItem>
                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="26">26</asp:ListItem>
                                                        <asp:ListItem Value="27">27</asp:ListItem>
                                                        <asp:ListItem Value="28">28</asp:ListItem>
                                                        <asp:ListItem Value="29">29</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="31">31</asp:ListItem>
                                                        <asp:ListItem Value="32">32</asp:ListItem>
                                                        <asp:ListItem Value="33">33</asp:ListItem>
                                                        <asp:ListItem Value="34">34</asp:ListItem>
                                                        <asp:ListItem Value="35">35</asp:ListItem>
                                                        <asp:ListItem Value="36">36</asp:ListItem>
                                                        <asp:ListItem Value="37">37</asp:ListItem>
                                                        <asp:ListItem Value="38">38</asp:ListItem>
                                                        <asp:ListItem Value="39">39</asp:ListItem>
                                                        <asp:ListItem Value="40">40</asp:ListItem>
                                                        <asp:ListItem Value="41">41</asp:ListItem>
                                                        <asp:ListItem Value="42">42</asp:ListItem>
                                                        <asp:ListItem Value="43">43</asp:ListItem>
                                                        <asp:ListItem Value="44">44</asp:ListItem>
                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                        <asp:ListItem Value="46">46</asp:ListItem>
                                                        <asp:ListItem Value="47">47</asp:ListItem>
                                                        <asp:ListItem Value="48">48</asp:ListItem>
                                                        <asp:ListItem Value="49">49</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="51">51</asp:ListItem>
                                                        <asp:ListItem Value="52">52</asp:ListItem>
                                                        <asp:ListItem Value="53">53</asp:ListItem>
                                                        <asp:ListItem Value="54">54</asp:ListItem>
                                                        <asp:ListItem Value="55">55</asp:ListItem>
                                                        <asp:ListItem Value="56">56</asp:ListItem>
                                                        <asp:ListItem Value="57">57</asp:ListItem>
                                                        <asp:ListItem Value="58">58</asp:ListItem>
                                                        <asp:ListItem Value="59">59</asp:ListItem>


                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlslb" runat="server" CssClass="ddlPage" Style="width: 50px; line-height: 22px;">
                                                        <asp:ListItem Value="AM">AM</asp:ListItem>
                                                        <asp:ListItem Value="PM">PM</asp:ListItem>




                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblschedulenumber" runat="server" BorderWidth="0" CssClass="btn btn-success btn-xs" Font-Size="14px"
                                                        Text="Schedule(0)"></asp:Label>


                                                </asp:Panel>
                                                <asp:Panel ID="pnlStatus" runat="server" Visible="false">


                                                    <asp:CheckBoxList ID="ChkBoxLstStatus" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                        runat="server" CssClass="form-control checkbox">
                                                    </asp:CheckBoxList>

                                                </asp:Panel>

                                                <asp:Panel ID="pnlParic" runat="server" Visible="false">
                                                    <asp:ListBox ID="ddlPartic" runat="server" SelectionMode="Multiple" class="form-control chzn-select" Style="width: 300px !important;"
                                                        data-placeholder="Choose Person......" multiple="true"></asp:ListBox>

                                                </asp:Panel>


                                                <%-- <asp:Panel ID="Pnlcompany" runat="server">--%>
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;"
                                                    TabIndex="12">
                                                </asp:DropDownList>
                                                <%--</asp:Panel>--%>


                                                <asp:Panel ID="PnlProject" runat="server">
                                                    <asp:DropDownList ID="ddlProject" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;"
                                                        TabIndex="12">
                                                    </asp:DropDownList>
                                                </asp:Panel>
                                                <asp:Panel ID="PnlUnit" runat="server">
                                                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="chzn-select inputTxt form-control" Style="width: 300px !important;"
                                                        TabIndex="12">
                                                    </asp:DropDownList>
                                                </asp:Panel>


                                                <asp:Panel ID="pnlVisit" runat="server" Visible="false">
                                                    <asp:DropDownList ID="ddlVisit" Visible="false" runat="server" CssClass="form-control" Style="width: 300px !important;">
                                                    </asp:DropDownList>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlFollow" runat="server" Visible="false">
                                                    <%-- <asp:DropDownList ID="ddlFollow" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>



                                                    <asp:CheckBoxList ID="ChkBoxLstFollow" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                        runat="server" CssClass="form-control checkbox">
                                                    </asp:CheckBoxList>


                                                </asp:Panel>
                                                <asp:Panel ID="pnlLostResion" runat="server" Visible="false">
                                                    <%-- <asp:DropDownList ID="ddlFollow" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>


                                                    <asp:DropDownList ID="checkboxReson" Visible="false" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;">
                                                    </asp:DropDownList>


                                                </asp:Panel>



                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:LinkButton ID="lbtnUpdateDiscussion" runat="server" OnClientClick="CloseModaldis();" OnClick="lbtnUpdateDiscussion_Click" CssClass="btn  btn-success btn-xs ">Final Update</asp:LinkButton>

                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Width="700px" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                            </div>

                        </div>


                    </div>
                </div>
            </div>
            <div class="col-md-12" id="pnlEditProspect" runat="server" visible="false">
                <div class="divPnl">
                    <div class="card pnlEditProspect mt-4">
                        <div class="modal-content">
                            <div class="modal-header pt-2 pb-2 ml-2 bg-light">
                                <div class="bg-light" id="lblprospect"><span class="font-weight-bold text-muted">Edit Prospect</span></div>

                                <asp:LinkButton ID="pnlEditProspectClose" OnClick="pnlEditProspectClose_Click" ToolTip="Close the Window" CssClass="btn btn-danger  btn-sm pr-2 pl-2" runat="server">&times;</asp:LinkButton>
                            </div>
                            <div class="modal-body" id="followup">

                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View runat="server">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">

                                                        <asp:HiddenField ID="lblnewprospect" runat="server" />
                                                        <h4 class="panel-title"><span class="clickable small "><i class="fa fa-minus "></i></span>Basic Information
    					
                                                        </h4>
                                                    </div>
                                                    <div class="panel-body">
                                                        <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                                            ShowFooter="True" OnRowDataBound="gvPersonalInfo_RowDataBound" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="classhidden">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvItmCodeper" ClientIDMode="Static" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvgph" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                            Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle Font-Bold="True" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvgval" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>

                                                                    <ItemTemplate>

                                                                        <asp:DropDownList ID="ddlcountryPhone" runat="server" CssClass="custom-select chzn-select" Style="float: left; padding-left: 0; padding-right: 0" Visible="false"
                                                                            Width="120px">
                                                                            <asp:ListItem Selected="True" Value="+88">+88</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:TextBox ID="txtgvVal" ClientIDMode="Static" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" OnTextChanged="txtgvVal_TextChanged1" AutoPostBack="true"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                                        <asp:Panel ID="Panegrd" runat="server">

                                                                            <div class="form-group">
                                                                                <asp:DropDownList ID="ddlval" runat="server" OnDataBound="ddlval_DataBound" Width="300px" CssClass="custom-select chzn-select">
                                                                                </asp:DropDownList>
                                                                            </div>


                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="grvFooter" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title"><span class="clickable small "><i class="fa fa-minus "></i></span>Source Information 					
                                                        </h4>
                                                    </div>
                                                    <div class="panel-body">
                                                        <asp:GridView ID="gvSourceInfo" runat="server" AutoGenerateColumns="False"
                                                            ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow" OnRowDataBound="gvSourceInfo_RowDataBound">
                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvItmCode" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcResDescsr" runat="server" Width="170px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvgph" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                            Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle Font-Bold="True" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvgvalsr" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>

                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>
                                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                                        <asp:Panel ID="Panegrd" runat="server">

                                                                            <div class="form-group mt-2">

                                                                                <asp:DropDownList ID="ddlval" runat="server" Width="300px" OnSelectedIndexChanged="ddlval_SelectedIndexChanged" AutoPostBack="true" CssClass="custom-select chzn-select">
                                                                                </asp:DropDownList>


                                                                            </div>


                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="grvFooter" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" runat="server" id="hpref"><span class="clickable small"><i class="fa fa-plus "></i></span>
                                                            <asp:Label ID="lblheadprospect" runat="server" Text="Prospect's Preference"></asp:Label>
                                                        </h4>
                                                    </div>
                                                    <div class="panel-body">
                                                        <asp:GridView ID="gvpinfo" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvpinfo_RowDataBound"
                                                            ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvItmCode" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvgph" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                            Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle Font-Bold="True" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvgvalpinf" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>

                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                                        <asp:Panel ID="Panegrd" runat="server">



                                                                            <div class="form-group mt-2">

                                                                                <asp:DropDownList ID="ddlvalcom" runat="server" Width="300px" OnSelectedIndexChanged="ddlvalcom_SelectedIndexChanged" AutoPostBack="true" CssClass="custom-select chzn-select">
                                                                                </asp:DropDownList>


                                                                            </div>



                                                                            <div class="form-group mt-2">

                                                                                <asp:DropDownList ID="ddlvalpros" runat="server" Width="300px" CssClass="custom-select chzn-select">
                                                                                </asp:DropDownList>


                                                                            </div>


                                                                        </asp:Panel>


                                                                        <asp:Panel ID="pnlMullocation" runat="server" Visible="false">
                                                                            <asp:ListBox ID="lstlocation" runat="server" SelectionMode="Multiple" Style="width: 300px !important;"
                                                                                data-placeholder="Choose Location......" multiple="true" class="form-control chosen-select"></asp:ListBox>

                                                                        </asp:Panel>

                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="grvFooter" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title"><span class="clickable small panel-collapsed"><i class="fa fa-plus "></i></span>Home Information  					
                                                        </h4>
                                                    </div>
                                                    <div class="panel-body" style="display: none;">
                                                        <asp:GridView ID="gvplot" runat="server" AutoGenerateColumns="False"
                                                            ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                            <RowStyle />
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvItmCode" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcResDescp" runat="server" Width="170px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvgph" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                            Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle Font-Bold="True" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvgvalplot" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>

                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>




                                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                                        <asp:Panel ID="Panegrd" runat="server">
                                                                            <div class="form-group">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvalplot" runat="server" CssClass="ddlcountry chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalplot_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnldist" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvald" runat="server" CssClass=" chzn-select form-control" Width="300px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlvald_SelectedIndexChanged">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlz" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvalz" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalz_SelectedIndexChanged">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>


                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlp" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvalp" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalp_SelectedIndexChanged">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>


                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnla" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvala" runat="server" CssClass=" chzn-select form-control" Width="300px">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>


                                                                        </asp:Panel>
                                                                        <asp:Panel ID="PanelBl" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlblock" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlblock_SelectedIndexChanged">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>


                                                                        </asp:Panel>

                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="grvFooter" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title"><span class="clickable small panel-collapsed"><i class="fa fa-plus "></i></span>Business Address  					
                                                        </h4>
                                                    </div>
                                                    <div class="panel-body" style="display: none;">
                                                        <asp:GridView ID="gvbusinfo" runat="server" AutoGenerateColumns="False"
                                                            ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvItmCode" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvgph" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                            Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle Font-Bold="True" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvgvalbuinf" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>

                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>
                                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server" CssClass="ml-1 form-control"
                                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                                        <asp:Panel ID="Panegrd" runat="server">
                                                                            <div class="form-group">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvalplot" runat="server" CssClass="ddlcountry chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalbusinfo_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnldist" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvald" runat="server" CssClass=" chzn-select form-control" Width="300px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlvaldbusinfo_SelectedIndexChanged">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlz" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvalz" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalzbusinfo_SelectedIndexChanged">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>


                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnlp" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvalp" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlvalpbusinfo_SelectedIndexChanged">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>


                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pnla" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlvala" runat="server" CssClass=" chzn-select form-control" Width="300px">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>


                                                                        </asp:Panel>
                                                                        <asp:Panel ID="PanelBl" runat="server">

                                                                            <div class="form-group mt-2">
                                                                                <div class="col-md-12 pading5px">
                                                                                    <asp:DropDownList ID="ddlblock" runat="server" CssClass=" chzn-select form-control" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlblockbusinfo_SelectedIndexChanged">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>


                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="grvFooter" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title"><span class="clickable small panel-collapsed"><i class="fa fa-plus "></i></span>More Information 					
                                                        </h4>
                                                    </div>
                                                    <div class="panel-body" style="display: none;">
                                                        <asp:GridView ID="gvMoreInfo" runat="server" AutoGenerateColumns="False"
                                                            ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvItmCode" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Description">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgcResDesc1" runat="server" Width="170px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvgph" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                                            Width="20px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    <ItemStyle Font-Bold="True" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lgvgvalminfo" runat="server"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>

                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>
                                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" CssClass="ml-1 form-control"
                                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                                        <asp:Panel ID="Panegrd" runat="server">

                                                                            <div class="form-group mt-2">

                                                                                <asp:DropDownList ID="ddlval" runat="server" Width="300px" CssClass="custom-select chzn-select">
                                                                                </asp:DropDownList>


                                                                            </div>


                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle CssClass="grvFooter" />
                                                            <EditRowStyle />
                                                            <AlternatingRowStyle />
                                                            <PagerStyle CssClass="gvPagination" />
                                                            <HeaderStyle CssClass="grvHeader" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row mb-2 btnsavefix">

                                            <div class="w-100">
                                                <%--//OnClientClick="javascript:return funDupAllMobile();"--%> <%--Req by Emdad by for new add country code 20221023--%>
                                                <asp:LinkButton ID="lnkUpdate" runat="server"
                                                    CssClass="btn btn-primary" OnClick="lnkUpdate_Click">Save</asp:LinkButton>
                                            </div>

                                        </div>
                                    </asp:View>

                                </asp:MultiView>
                                <div class="modal" tabindex="-1" role="dialog" id="modalassure">
                                    <div class="modal-dialog " role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title">Lead</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row mt-3">

                                                    <div class="col-md-4">
                                                        <label class="control-label">Date:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtentrydate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtentrydate"></cc1:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div class="row mt-2">
                                                    <div class="col-md-4">
                                                        <label class="control-label">Client Name:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtentryClient" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-2">
                                                    <div class="col-md-4">
                                                        <label class="control-label">Mobile Number:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtentrymobile" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-2">
                                                    <div class="col-md-4 ">
                                                        <label class="control-label">Email:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="txtentryemail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-2">
                                                    <div class="col-md-4">
                                                        <label class="control-label">Source:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlgval" runat="server" CssClass="custom-select chzn-select" TabIndex="2">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <asp:Label runat="server" ID="txtentryEmpID" Visible="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="modal-footer">

                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                <asp:LinkButton ID="lnkSaveModalEntry" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="CloseAssureModal();" OnClick="lnkSaveModalEntry_Click">Save</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
