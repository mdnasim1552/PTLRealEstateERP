<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AddLead.aspx.cs" EnableEventValidation="false" ValidateRequest="false" Inherits="RealERPWEB.F_21_MKT.AddLead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        body {
            font-family: "Century Gothic";
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
            font-size: 1.5rem;
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

        /*input[type=text], select {
            width: 100%;
            padding: 12px 20px;
            margin: 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            height: 25px;
        }*/


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





        /*.tSl {
            width: 2%;
        }

        .tPid {
            width: 2%;
        }

        .tdate {
            width: 5%;
        }

        .tCdetails {
            width: 10%;
        }

        .tAssociate {
            width: 10%;
        }

        .tTeamHead {
            width: 10%;
        }

        .tStatus {
            width: 4%;
        }

        .tType {
            width: 2%;
        }

        .tActive {
            width: 2%;
        }

        .tMobile {
            width: 5%;
        }

        .tEmail {
            width: 8%;
        }

        .tOccupation {
            width: 4%;
        }

        .tResidence {
            width: 5%;
        }

        .tIntProject {
            width: 5%;
        }

        .tSource {
            width: 4%;
        }

        .tFeedBack {
            width: 10%;
        }

        .tComments {
            width: 4%;
        }*/

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
    </style>

    <script type="text/javascript">


      

        function Initializescroll() {
            document.cookie = "yPos=!~" + 0 + "~!";
        }

        function SetDivPosition() {


            var intY = document.getElementById("divscroll").scrollTop;
            //console.log(intY);
            // alert(intY);
            document.cookie = "yPos=!~" + intY + "~!";
        }

        var comcod =<%=this.GetComeCode()%>;



        $(document).ready(function () {
            // $(".TxtDateVal").on("change",function (){ 
            //     alert("The text has been changed2.");
            // });
            // $("#txtgvdValdis").change(function(){
            //     alert("The text has been changed.");
            //});
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });





        function pageLoaded() {

            try {
                

                $('.lbtnschedule').click(function () {


                    var subject = $(this).parent().find('#txtdate').val();
                    var lblcdate = $(this).parent().find('#lblcdate').val();

                    //alert(subject);
                    //console.log(lblcdate);
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

                //Company Name change
                //var comcod =<%=this.GetComeCode()%>;

                switch (comcod) {
                    case 3352://p2p360                 
                    case 1205://p2p Construction
                    case 3351://p2p Wecon Properties          

                        $('#<%=this.lblheadprospect.ClientID%>').text(" Project Details");

                        break;

                    default:
                        $('#<%=this.lblheadprospect.ClientID%>').text(" Prospect's Preference	");
                        break;
                }

                var gcod;

                //Schedule Reminder              

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


                var ddlcompany = '#ContentPlaceHolder1_gvInfo_ddlProject_' + numberprj;
                $(ddlcompany).change(function () {

                    var company = $('#ContentPlaceHolder1_gvInfo_ddlCompany_' + numbercom).val();
                    var pactcode = $(this).val();
                    funCompanyProjectUnit(company, pactcode);


                });






                //Lead Reason

                var ddlvisit = '#ContentPlaceHolder1_gvInfo_ddlVisit_' + numberlq;
                $(ddlvisit).change(function () {
                    leadquality = $(this).val();
                    funLeadReason(comcod, leadquality);


                });

                //Duplicate Mobile
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
                    if (comcod == "3354" || comcod == "3364") {
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



                var strCook = document.cookie;
                if (strCook.indexOf("!~") != 0) {
                    var intS = strCook.indexOf("!~");
                    var intE = strCook.indexOf("~!");
                    var strPos = strCook.substring(intS + 2, intE);
                    document.getElementById("divscroll").scrollTop = strPos;

                }





                $('#ChkBoxLstFollow input[type=checkbox]').click(function () {


                    $('#ChkBoxLstFollow >input').each(function (index, item) {




                        if ($(item).is(':checked')) {


                            $('#hdnwinstatus').text($(item).val())

                            switch (comcod) {
                                case 3101:
                                    // case 3354:// Edison   


                                    if ($(item).val() == "9601050") {

                                        $('#divsold').show();
                                    }
                                    else {
                                        $('#divsold').hide();


                                    }

                                    break;

                                default:
                                    break;



                            }


                        }
                        else {



                        }






                    });
                });



                // Status

                $('#ChkBoxLstStatus input[type=checkbox]').click(function () {


                    $('#ChkBoxLstStatus span >input').each(function (index, item) {




                        if ($(item).is(':checked')) {




                            switch (comcod) {
                                case 3101:
                                case 3354:// Edison 
                                    var leadst = $(item).val();

                                    switch (leadst) {
                                        case "9501020"://Hold

                                            $('#divhold').show();
                                            $('#divlost').hide();
                                            $('#divclose').hide();
                                            break;

                                        case "9501028"://Lost                                           
                                            $('#divhold').hide();
                                            $('#divlost').show();
                                            $('#divclose').hide();
                                            break;

                                        case "9501035"://Close
                                            $('#divhold').hide();
                                            $('#divlost').hide();
                                            $('#divclose').show();
                                            break;

                                    }
                                    break;

                                default:
                                    break;



                            }


                        }
                        else {



                        }






                    });
                });




                //Sold Info
                var ddlsoldProject01 = '#ContentPlaceHolder1_rpsold_ddlsoldProject_0';
                var ddlsoldProject02 = '#ContentPlaceHolder1_rpsold_ddlsoldProject_1';
                var ddlsoldProject03 = '#ContentPlaceHolder1_rpsold_ddlsoldProject_2';
                var id;
                $(ddlsoldProject01).change(function () {

                    id = 0;
                    var pactcode = $('' + ddlsoldProject01 + ' option:selected').val();

                    //  var pactcode = $('#ddlsoldProject option:selected').val();                    
                    funProjectUnit(comcod, pactcode, id);

                });

                $(ddlsoldProject02).change(function () {

                    id = 1;
                    var pactcode = $('' + ddlsoldProject02 + ' option:selected').val();

                    //  var pactcode = $('#ddlsoldProject option:selected').val();                    
                    funProjectUnit(comcod, pactcode, id);

                });


                $(ddlsoldProject03).change(function () {

                    id = 2;
                    var pactcode = $('' + ddlsoldProject03 + ' option:selected').val();

                    //  var pactcode = $('#ddlsoldProject option:selected').val();                    
                    funProjectUnit(comcod, pactcode, id);

                });

                var flatcost01 = '#ContentPlaceHolder1_rpsold_txtflatcost_0';
                var utility01 = '#ContentPlaceHolder1_rpsold_txtUtility_0';
                var pamt01 = '#ContentPlaceHolder1_rpsold_txtpamt_0';
                var toamt01 = '#ContentPlaceHolder1_rpsold_txtgrandTotal_0';

                $(flatcost01).change(function () {

                    var flatcost = $(flatcost01).val().length == 0 ? 0 : parseFloat($(flatcost01).val());
                    var utility = $(utility01).val().length == 0 ? 0 : parseFloat($(utility01).val());
                    var pamt = $(pamt01).val().length == 0 ? 0 : parseFloat($(pamt01).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt01).val(tamt.toString());

                });


                $(utility01).change(function () {

                    var flatcost = $(flatcost01).val().length == 0 ? 0 : parseFloat($(flatcost01).val());
                    var utility = $(utility01).val().length == 0 ? 0 : parseFloat($(utility01).val());
                    var pamt = $(pamt01).val().length == 0 ? 0 : parseFloat($(pamt01).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt01).val(tamt.toString());

                });



                $(pamt01).change(function () {

                    var flatcost = $(flatcost01).val().length == 0 ? 0 : parseFloat($(flatcost01).val());
                    var utility = $(utility01).val().length == 0 ? 0 : parseFloat($(utility01).val());
                    var pamt = $(pamt01).val().length == 0 ? 0 : parseFloat($(pamt01).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt01).val(tamt.toString());

                });



                var flatcost02 = '#ContentPlaceHolder1_rpsold_txtflatcost_1';
                var utility02 = '#ContentPlaceHolder1_rpsold_txtUtility_1';
                var pamt02 = '#ContentPlaceHolder1_rpsold_txtpamt_1';
                var toamt02 = '#ContentPlaceHolder1_rpsold_txtgrandTotal_1';

                $(flatcost02).change(function () {

                    var flatcost = $(flatcost02).val().length == 0 ? 0 : parseFloat($(flatcost02).val());
                    var utility = $(utility02).val().length == 0 ? 0 : parseFloat($(utility02).val());
                    var pamt = $(pamt02).val().length == 0 ? 0 : parseFloat($(pamt02).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt02).val(tamt.toString());

                });


                $(utility02).change(function () {

                    var flatcost = $(flatcost02).val().length == 0 ? 0 : parseFloat($(flatcost02).val());
                    var utility = $(utility02).val().length == 0 ? 0 : parseFloat($(utility02).val());
                    var pamt = $(pamt02).val().length == 0 ? 0 : parseFloat($(pamt02).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt02).val(tamt.toString());

                });



                $(pamt02).change(function () {

                    var flatcost = $(flatcost02).val().length == 0 ? 0 : parseFloat($(flatcost02).val());
                    var utility = $(utility02).val().length == 0 ? 0 : parseFloat($(utility02).val());
                    var pamt = $(pamt02).val().length == 0 ? 0 : parseFloat($(pamt02).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt02).val(tamt.toString());

                });


                var flatcost03 = '#ContentPlaceHolder1_rpsold_txtflatcost_2';
                var utility03 = '#ContentPlaceHolder1_rpsold_txtUtility_2';
                var pamt03 = '#ContentPlaceHolder1_rpsold_txtpamt_2';
                var toamt03 = '#ContentPlaceHolder1_rpsold_txtgrandTotal_2';

                $(flatcost03).change(function () {

                    var flatcost = $(flatcost03).val().length == 0 ? 0 : parseFloat($(flatcost03).val());
                    var utility = $(utility03).val().length == 0 ? 0 : parseFloat($(utility03).val());
                    var pamt = $(pamt03).val().length == 0 ? 0 : parseFloat($(pamt03).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt03).val(tamt.toString());

                });


                $(utility03).change(function () {

                    var flatcost = $(flatcost03).val().length == 0 ? 0 : parseFloat($(flatcost03).val());
                    var utility = $(utility03).val().length == 0 ? 0 : parseFloat($(utility03).val());
                    var pamt = $(pamt03).val().length == 0 ? 0 : parseFloat($(pamt03).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt03).val(tamt.toString());

                });



                $(pamt03).change(function () {

                    var flatcost = $(flatcost03).val().length == 0 ? 0 : parseFloat($(flatcost03).val());
                    var utility = $(utility03).val().length == 0 ? 0 : parseFloat($(utility03).val());
                    var pamt = $(pamt03).val().length == 0 ? 0 : parseFloat($(pamt03).val());
                    var tamt = flatcost + utility + pamt;
                    $(toamt03).val(tamt.toString());

                });












                $('#lbtnAddMore').click(function () {

                    alert("add More");


                });


             <%-- $('#ChkBoxLstStatus input[type=checkbox]').click(function ()
                {
                 

                    $('#ChkBoxLstStatus >input').each(function (index, item)
                    {

                        alert("test");

                       
                        if ($(item).is(':checked')) {
                           



                            switch (comcod) {
                                case 3101:
                                case 3354:// Edison
                                    alert($(item).val());
                                    var empid =<%=this.GetEmpID()%>;
                                    var proscod = $('#<%=this.lblproscod.ClientID%>').val();

                                    $.ajax({
                                        type: "POST",
                                        url: "CrmClientInfo.aspx/ShowStatusSerial",
                                        data: '{comcod:"' + comcod + '",  empid: "' + empid + '", proscod:"' + proscod + '"}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",


                                        success: function (response) {
                                            //console.log(JSON.parse(response.d));
                                            var data = response.d;
                                            console.log(data);
                                            //console.log(data['account']);

                                        },


                                        failure: function (response) {
                                            //  alert(response);
                                            alert("failure");
                                        }
                                    });


                                    break;

                                default:
                                    break;



                            }


                        }
                        //else
                        //{
                        //    alert($(item).val());

                        // }






                    });
                });--%>






                //$('#ChkBoxLstStatus').change(function (index, item) {


                //    alert($(item).val());

                //    if ($(this).is(':checked')) {
                //        alert($(item).val());

                //    }
                //    //else
                //    //{

                //    //   // alert($(item).val());


                //    //}





                //});







            }

            catch (e) {
              //  alert(e.message)

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




        function alertmsg(message, faclass) {
            //$('.popup-container').css('display', 'block');
            document.querySelector(".popup-container").style.visibility = "visible";
            setTimeout(function () { document.querySelector(".popup-container").style.visibility = "hidden"; }, 3000);
            //fa-times  fa-trash  fa-check fa-exclamation-circle fa-external-link-square 
            //console.log(document.querySelector("#icons"));            
            document.querySelector("#icons").classList.remove("fa-check")
            document.querySelector("#icons").classList.add(faclass);
            document.querySelector(".msg-text").innerHTML = message;
            if (faclass === "fa-times") {
                document.querySelector(".icons").style.backgroundColor = "red";
                document.querySelector(".popup").style.borderColor = "red";
            }
            if (faclass === "fa-exclamation-circle") {
                document.querySelector(".icons").style.backgroundColor = "red";
                document.querySelector(".popup").style.borderColor = "red";
            }
            $('html, body').animate({ scrollTop: 0 }, 'fast');
        }
        function openModal() {

            $('#contact').modal('toggle');
        }

        function CloseModal() {

            $('#contact').modal('hide');
        }
        function openViewModal() {

            $('#ViewModal').modal('toggle');
        }

        function CloseViewModal() {

            $('#ViewModal').modal('hide');
        }
        function OpenGvModal() {

            $('#GridHeader').modal('toggle');
        }

        function CloseGvModal() {

            $('#GridHeader').modal('hide');
        }
        function OpenAssureModal() {

            $('#modalassure').modal('toggle');
        }
        function CloseAssureModal() {

            $('#modalassure').modal('hide');
        }

        function openComModal() {
            $('#modalComments').modal('toggle');
        }
        function closeComModal() {
            $('#modalComments').modal('hide');
        }


        function openNotesModal() {
            $('#modalNotes').modal('toggle');
        }

        function closeNotesModal() {
            $('#modalNotes').modal('hide');
        }




        function AddButton(id) {

            $(".hiddenb" + id).css("display", "inline");

        }
        function HiddenButton(id) {
            $(".hiddenb" + id).css("display", "none");
        }


        function openModal() {

            $('#contact').modal('toggle');
        }

        function CloseModal() {

            $('#contact').modal('hide');
        }



        function openModaldis() {

            //  $('#mdiscussion').modal('toggle');
            //  $('#lbtntfollowup').click();

            $('#mdiscussion').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });


        }






        function CloseModaldis() {

            $('#mdiscussion').modal('toggle');
        }



    
        function CreateTable(data) {

            try {
                // console.log(data);
                var adata = JSON.parse(data);
                // console.log(adata);
                var row = '';
                var i = 1;
                $.each(adata,
                    function (key, val) {

                        row += "<tr class='grvRows'>";
                        row += "<td class='tSl'>" + i + "</td>";
                        row += "<td class='tStatus'>" + val.pid + "</td>";
                        row += "<td class='tdate'>" + val.generated1 + "</td>";
                        row += "<td class='tdatetime'>" + val.lfollowup + "</td>";
                        row += "<td class='tDiscussion'>" + val.ownname + "</td>";
                        row += "<td class='tDiscussion'>" + val.ldetails + "</td>";
                        row += "<td class='tAssociateadeal'>" + val.assoc + "</td>";
                        row += "<td class='tAssociateadeal'>" + val.dealname + "</td>";
                        row += "<td class='tStatus'>" + val.lstatus + "</td>";
                        row += "<td class='tStatus'>" + val.prio + "</td>";
                        row += "</tr>";
                        //row += "<tr class='grvRows'>";
                        //row += "<td class='tSl'>" +  i + "</td>";
                        //row += "<td class='tPid'>" + val.sircode1+ "</td>";                           
                        //row += "<td class='tdate'>" + val.generated1 + "</td>";
                        //row += "<td class='tCdetails'>" + val.sirdesc + "</td>";
                        //row += "<td class='tAssociate'>" + val.assoc + "</td>";
                        //row += "<td class='tTeamHead'>" + val.teamdesc + "</td>";        
                        //row += "<td class='tStatus'>" + val.lstatus + "</td>"; 
                        //row += "<td class='tType'>" + val.LeadType + "</td>"; 
                        //row += "<td class='tActive'>" + val.active + "</td>"; 
                        //row += "<td class='tMobile'>" + val.phone + "</td>"; 
                        //row += "<td class='tEmail'>" + val.email + "</td>"; 
                        //row += "<td class='tOccupation'>" + val.profession + "</td>"; 
                        //row += "<td class='tResidence' >" + val.caddress + "</td>"; 
                        //row += "<td class='tIntProject'>" + val.pactdesc + "</td>"; 
                        //row += "<td class='tSource'>" + val.LeadSrc + "</td>"; 
                        //row += "<td class='tFeedBack'>" + val.ldiscuss + "</td>"; 
                        //row += "<td class='tComments'>" + val.note + "</td>"; 
                        row += "</tr>";
                        $("#tblinformation tbody").html(row);
                        i++;

                    });
                loadModal();
            }

            catch (e) {

                alert(e.message);

            }

        }


        function loadModal() {

            $('#detnotification').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }

        function CloseModal() {
            $('#detnotification').modal('hide');


        }


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



        function funschedulenumber(comcod, followupdate, lastfollowup, number) {

            try {

                var empid =<%=this.GetEmpID()%>;
                var lblschedulenumber = '#ContentPlaceHolder1_gvInfo_lblschedulenumber_' + number;

                $.ajax({

                    url: "CrmClientInfo.aspx/GetSchedulenumber",
                    type: "POST",
                    data: '{comcod:"' + comcod + '", followupdate:"' + followupdate + '", lastfollowup:"' + lastfollowup + '", empid:"' + empid + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //  async: false,
                    success: function (data) {
                        var jdata = JSON.parse(data.d);

                        var mesult = jdata.result;
                        if (!mesult) {
                            alert(jdata.Message);

                        }
                        else {

                            $(lblschedulenumber).text(jdata.Message);
                        }
                    }
                });

            }


            catch (e) {
                alert(e.message);

            }


        }

      
        // Company Project Unit

     

        function funProjectUnit(comcod, pactcode, id) {
            try {
                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/GetProjectUnit",
                    data: '{comcod:"' + comcod + '", pactcode:"' + pactcode + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d);

                       <%-- var arrgschcodl = $('#<%=this.gvInfo.ClientID %>').find('[id$="lblgvItmCodedis"]');
                        var numberrl;

                        for (var i = 0; i < arrgschcodl.length; i++) {

                            gcod = $(arrgschcodl[i]).text();
                            switch (gcod) {

                                case '810100101003':
                                    numberrl = i;
                                    break;

                            }

                        }--%>

                        console.log(data);

                        var ddlunit = '#ContentPlaceHolder1_rpsold_ddlsoldunit_' + id;


                        //console.log(ddlProject);
                        $(ddlunit).html('');

                        // $(lstProject).empty();
                        $.each(data, function (key, data) {



                            // $('#Select1').append('<option value="5">item 5</option>')
                            $(ddlunit).append("<option value='" + data.usircode + "'>" + data.udesc + "</option>");
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


        function funDataToggle() {

            $('#mdiscussion').modal('toggle');


            //var winstatus = $('#hdnwinstatus').text();

            //if (winstatus == "9601050") {
            //    alert("show")
            //    $('#divsold').show();
            //}
            //else {
            //    $('#divsold').hide();

            //}



        }


    

        function funDupAllMobile() {

            try {


                    //Company Name change
                //    var comcod =<%=this.GetComeCode()%>;
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


        function funStatus() {

            try {



                //    var comcod =<%=this.GetComeCode()%>;
                var proscod = $('#<%=this.lblproscod.ClientID%>').val();
                var statusid = $('#ddlmStatus option:selected').val();
                var empid =<%=this.GetEmpID()%>;

                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/UpdateStatus",
                    data: '{comcod:"' + comcod + '",  proscod: "' + proscod + '", statusid:"' + statusid + '", empid:"' + empid + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",


                    success: function (response) {

                        //var data =JSON.parse(response.d);                     
                        //alert(data.Message);


                        //console.log(data['account']);

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


        function funReschedule(cdate, number) {
            try {


                //var  comdate =$('#txtcomdate'+number).val();

                var empid =<%=this.GetEmpID()%>;
                var proscod = $('#<%=this.lblproscod.ClientID%>').val();



                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/GetReschedule",
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
                            //console.log(data.gdesc1);

                            var ddlcompany = '#ContentPlaceHolder1_gvInfo_ddlCompany_' + number;

                            $(ddlcompany + ' > option').each(function (index, item) {
                                if ($(item).val() == data.gdesc1) {
                                    $(item).attr("selected", true);
                                }


                            });


                            $('#hdncompany').text(data.gdesc1);




                            break;


                        case "810100101003": //Project                               

                            //For Company Chnage                              
                            //  funCompanyProject(comcod, $('#hdncompany').text());
                            var ddlProject = '#ContentPlaceHolder1_gvInfo_ddlProject_' + number;

                            // var ddlProject = "#ddlProject";


                            $(ddlProject + ' > option').each(function (index, item) {
                                if ($(item).val() == data.gdesc1) {

                                    $(item).attr("selected", true);

                                }


                            });




                            break;








                        case "810100101019"://Follow

                            var ChkBoxLstFollow = '#ContentPlaceHolder1_gvInfo_ChkBoxLstnextFollow_' + number;
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
                                        // $(item).attr('disabled', '');

                                    }
                                    else {
                                        $(item).attr('checked', false);
                                        // $(item).attr('disabled', true);


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


        }

        function OpenKpiDetailsModal() {
            $('#modalKpiDetials').modal('toggle');
        }

        function RateUpdate() {

            try {



                  //  var comcod =<%=this.GetComeCode()%>;
                var proscod = $('#<%=this.lblproscod.ClientID%>').val();
                var ratevalue = $('#ddlRating option:selected').val();

                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/UpdateRate",
                    data: '{comcod:"' + comcod + '",  proscod: "' + proscod + '", ratevalue:"' + ratevalue + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",


                    success: function (response) {


                    },


                    failure: function (response) {

                        alert("failure");
                    }
                });



            }

            catch (e) {

                alert(e.message);

            }
        };
        //// for selected follow then selected lead status
        //Retreive Confirmation
        function FunRetProsConfirm() {
            if (confirm('Are you sure you want to retrieve this Prospect?')) {
                return;
            } else {
                return false;
            }
        };


    </script>


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
               <asp:HiddenField ID="lblproscod" runat="server" />
                                        <asp:HiddenField ID="lbleditempid" runat="server" />
                                        <asp:HiddenField ID="lblgeneratedate" runat="server" />
                              
            <div class="card card-fluid container-data">
                <div class="popup-container">
                    <div class="popup">
                        <div class="icons">
                            <i class="fa fa-check" id="icons" aria-hidden="true"></i>
                        </div>
                        <div class="msg-text">
                            Updated Successfully
                        </div>
                    </div>
                </div>

                <div class="card-body" style="min-height: 600px;">
                    <div class="row mb-2 justify-content-between">
                        <div class="col-2">
                            <div class="form-group">

                               
                            </div>
                        </div>
                        <div class="col-3" runat="server" id="divexland">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lbllandname" Font-Size="16px" class="form-control bg-danger font-weight-bold text-white margin-top30px" Visible="false"></asp:Label>
                            </div>
                        </div>

                        


                    </div>

                  
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
                                                                    <asp:DropDownList ID="ddlval" runat="server"  Width="300px" CssClass="custom-select chzn-select">
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
                                                            <asp:Panel ID="pnlIREmp" runat="server" Visible="false">
                                                                <div class="form-group mt-2">
                                                                    <asp:DropDownList ID="ddlIREmp" runat="server" Width="300px" CssClass="custom-select chzn-select">
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
                    <div class="modal fade right" id="contact" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                        aria-hidden="true" data-backdrop="false">
                        <div class="modal-dialog  modal-sm  modal-side modal-bottom-right modal-notify modal-info" role="document">
                            <!--Content-->
                            <div class="modal-content">
                                <!--Header-->
                                <div class="modal-header">
                                    <p class="heading">
                                        <h4 id="lblheader" runat="server"><span class="glyphicon glyphicon-info-sign"></span>Client Activated</h4>
                                        <p>
                                        </p>
                                    <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                                        <span aria-hidden="true" class="white-text">×</span>
                                    </button>


                                </div>

                                <!--Body-->
                                <div class="modal-body">

                                    <div class="row">

                                        <asp:Label ID="lblsircode" runat="server" Visible="false"></asp:Label>


                                    </div>
                                </div>

                                <!--Footer-->
                                <div class="modal-footer">
                                    <asp:LinkButton ID="btmodal" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="CloseModal();" OnClick="btmodal_Click">Active</asp:LinkButton>
                                </div>
                            </div>
                            <!--/.Content-->
                        </div>
                    </div>
            
               
                </div>


            </div>
            <!-- Modal -->
        
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

