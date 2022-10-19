<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CrmClientInfo.aspx.cs" EnableEventValidation="false" ValidateRequest="false" Inherits="RealERPWEB.F_21_MKT.CrmClientInfo" %>

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
  width:200px;
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


tr#ContentPlaceHolder1_Cal3_daysTableHeaderRow td{


}
    </style>






    <script type="text/javascript">


        //$(document).on('scroll','#divscroll',function(){
        //    alert("scrool");
        //});


        //window.onload = function () {
        //    var strCook = document.cookie;
        //    console.log(strCook);
        //    if (strCook.indexOf("!~") != 0) {
        //        var intS = strCook.indexOf("!~");
        //        var intE = strCook.indexOf("~!");
        //        var strPos = strCook.substring(intS + 2, intE);
        //        document.getElementById("divscroll").scrollTop = strPos;
        //        //console.log("Position"+strPos);
        //    }
        //}

        function Initializescroll() {
            document.cookie = "yPos=!~" + 0 + "~!";
        }

        function SetDivPosition() {


            var intY = document.getElementById("divscroll").scrollTop;
            //console.log(intY);
            // alert(intY);
            document.cookie = "yPos=!~" + intY + "~!";
        }



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
                VisibilitycomNotification();

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
                var comcod =<%=this.GetComeCode()%>;

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

                //Duplicate Mobile
                var sircode = $('#<%=this.lblnewprospect.ClientID%>').val();
                var arrgcodl = $('#<%=this.gvPersonalInfo.ClientID %>').find('[id$="lblgvItmCodeper"]');
                var arraygval = $('#<%=this.gvPersonalInfo.ClientID %>').find('input:text[id$="txtgvVal"]');
                // console.log(arraygval);
                // var txtmobile=arraygval[1];  
                var txtmobile, txtaltmobile1, txtaltmobile2;


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

                $(txtmobile).keyup(function () {
                    var mobile = $(this).val();

                    if (!($.isNumeric(mobile))) {

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
                        alert("test--");
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



 





                var gvSummary = $('#<%=this.gvSummary.ClientID %>');
                gvSummary.Scrollable();



                var strCook = document.cookie;
                if (strCook.indexOf("!~") != 0) {
                    var intS = strCook.indexOf("!~");
                    var intE = strCook.indexOf("~!");
                    var strPos = strCook.substring(intS + 2, intE);
                    document.getElementById("divscroll").scrollTop = strPos;

                }






            }

            catch (e) {

                //  alert(e.message);
            }

        }





        function VisibilitycomNotification() {

            try {

                var comcod =<%=this.GetComeCode()%>;

                switch (comcod) {
                    case 3354://Edison Real Estate                 

                        $('#<%=this.lnkBtnDaypassed.ClientID%>').hide();
                        $('#<%=this.lnkBtnComments.ClientID%>').hide();
                        $('#<%=this.lnkBtnFreezing.ClientID%>').hide();
                        $('#<%=this.lnkBtnDeadProspect.ClientID%>').hide();


                        break;

                    default:

                        break;
                }



            }

            catch (e) {
                alert(e.message);
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

            $('#mdiscussion').modal('toggle');
            //  $('#lbtntfollowup').click();
        }






        function CloseModaldis() {

            $('#mdiscussion').modal('toggle');
        }



        function DetNotification(rtype) {

            try {

                var empid =<%=this.GetEmpID()%>;
                var comcod =<%=this.GetComeCode()%>;



                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/ShowNotifications",
                    data: '{comcod:"' + comcod + '",  empid: "' + empid + '", rtype:"' + rtype + '", fdate: "' + $('#<%=this.txtfrmdate.ClientID%>').val() + '", tdate: "' + $('#<%=this.txttodate.ClientID%>').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",


                    success: function (response) {
                        //console.log(JSON.parse(response.d));
                        var data = response.d;
                        CreateTable(data);
                        //console.log(data['account']);

                    },


                    failure: function (response) {
                        //  alert(response);
                        alert("failure");
                    }
                });

            }

            catch (e) {
                alert(e.message);

            }

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





        function funPost(date, number) {
            try {
                var comdate = $('#txtcomdate' + number).val();
                var comcod =<%=this.GetComeCode()%>;
                var comments = $('#lblcomments' + number).val();

                var proscod = $('#<%=this.lblproscod.ClientID%>').val();
                var userid =<%=this.GetUserID()%>;


                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/UpdatePost",
                    data: '{comcod:"' + comcod + '", userid:"' + userid + '",  proscod: "' + proscod + '", date:"' + date + '" , post: "' + comments + '", comdate: "' + comdate + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",


                    success: function (response) {
                        //console.log(JSON.parse(response.d));
                        var data = JSON.parse(response.d);
                        alert(data.Message);

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




        function funCancel(date) {
            try {
                if (!confirm("Are you sure you want to delete this  Item?")) {
                    return;
                }

                var comcod =<%=this.GetComeCode()%>;
                var proscod = $('#<%=this.lblproscod.ClientID%>').val();
                var userid =<%=this.GetUserID()%>;


                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/FollowupCancel",
                    data: '{comcod:"' + comcod + '", userid:"' + userid + '",  proscod: "' + proscod + '", date:"' + date + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",


                    success: function (response) {
                        //console.log(JSON.parse(response.d));
                        var data = JSON.parse(response.d);
                        //  console.log(data);

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









        //function  funReschedule(date, number)
        //{

        //    try
        //    {

        //        var date=$(this).parent().find('#lblsubjects').val();
        //        alert(date);
        //        console.log(date);

        //        return;

        //    }

        //    catch(e)
        //    {

        //        alert(e.message)

        //    }

        //}



        function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvSummary.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;

            }
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

                console.log(sircode + "" + arrgcodl + "" + arraygval);

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
                            number = gval.length > 0 ? gval + "," : "";
                            break;


                        case '0301004':
                           
                            switch (comcod) {
                                case '3315':
                                case '3316':                                 
                                    break;

                                default:
                                    gval = $(arraygval[i]).val();
                                    number = number + (gval.length > 0 ? gval + ",nahid" : "");
                                    break;
                            }                            
                            break;

                        case '0301005':
                            gval = $(arraygval[i]).val();
                            number = number + (gval.length > 0 ? gval + "," : "");
                            break;
                    }

                }

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



                var comcod =<%=this.GetComeCode()%>;
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



                        $('#<%=this.lbllaststatus.ClientID%>').html("Status:" + "<span style='color:#ffef2f; font-size:14px; font-weight:bold'>"
                            + $('#ddlmStatus option:selected').text() + "</span>");

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
                var comcod =<%=this.GetComeCode()%>;
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
                        //console.log(data);
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


        }

        function OpenKpiDetailsModal() {
            $('#modalKpiDetials').modal('toggle');
        }

        function RateUpdate() {

            try {



                var comcod =<%=this.GetComeCode()%>;
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
                                <asp:LinkButton ID="lbtPending" runat="server" CssClass="d-none margin-top30px form-control" OnClick="lbtPending_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-3" runat="server" id="divexland">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lbllandname" Font-Size="16px" class="form-control bg-danger font-weight-bold text-white margin-top30px" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <asp:LinkButton ID="btnaddland" runat="server" CssClass="mt-2 btn btn-primary align-self-end" OnClick="btnaddland_Click">Add Lead</asp:LinkButton>


                    </div>

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
                                    <asp:LinkButton ID="lnkUpdate" runat="server" OnClientClick="javascript:return funDupAllMobile();"
                                        CssClass="btn btn-primary" OnClick="lnkUpdate_Click">Save</asp:LinkButton>
                                </div>

                            </div>
                        </asp:View>


                        <asp:View ID="View2" runat="server">
                            <asp:Panel ID="pnpRecFilter" runat="server">
                                <div class="row">
                                    <label class="control-label col-md-1">Filter</label>


                                    <asp:DropDownList ID="ddlEmpid" data-placeholder="Choose Employee.." runat="server" CssClass="custom-select chzn-select col-md-2 mr-1 mb-1" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                    </asp:DropDownList>




                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>




                                    <asp:DropDownList ID="ddlDist" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>


                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>



                                    <asp:DropDownList ID="ddlPStat" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlPStat_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>



                                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>


                                    <asp:DropDownList ID="ddlArea" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" Visible="false">
                                    </asp:DropDownList>


                                    <asp:DropDownList ID="ddlPri" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1">
                                    </asp:DropDownList>

                                    <asp:DropDownList ID="ddlStatus" data-placeholder="Choose Status......" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1">
                                    </asp:DropDownList>


                                    <asp:DropDownList ID="ddlOther" runat="server" ClientIDMode="Static" CssClass="custom-select chzn-select scroll col-md-1 ml-1 ">
                                        <asp:ListItem Value="1">Prospect Name</asp:ListItem>
                                        <asp:ListItem Value="2">PID</asp:ListItem>
                                        <asp:ListItem Value="3">Phone</asp:ListItem>
                                        <asp:ListItem Value="4">Email</asp:ListItem>
                                        <asp:ListItem Value="5">NID</asp:ListItem>
                                        <asp:ListItem Value="6">TIN</asp:ListItem>
                                        <asp:ListItem Value="7">Prefered Area</asp:ListItem>
                                        <asp:ListItem Value="8">Profission</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="9">Choose One.....</asp:ListItem>
                                    </asp:DropDownList>






                                    <asp:TextBox ID="txtVal" runat="server" CssClass="form-control col-md-1 ml-1" TextMode="Search"></asp:TextBox>


                                    <div class="col-md-1">


                                        <asp:LinkButton ID="SrchBtn" runat="server" class="btn btn-success" OnClientClick="CloseModal();" OnClick="SrchBtn_Click">Search</asp:LinkButton>


                                    </div>

                                </div>
                            </asp:Panel>

                            <div class="row">


                                <div class="col-md-10">
                                    <div class="row mt-1">

                                        <asp:Label runat="server" ID="lbleads" class="control-label col-md-1">Leads</asp:Label>



                                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control col-md-2  ml-1"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Cal3" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                        <label class="lblmargin-top9px col-md-1 ml-1" for="todate" runat="server" id="lbltodatekpi" visible="false">To</label>



                                        <asp:TextBox ID="txtkpitodate" runat="server" CssClass="form-control col-md-2 ml-1" Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtkpitodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtkpitodate"></cc1:CalendarExtender>




                                        <div class="col-md-2">
                                            <input type="text" id="myInput" onkeyup="Search_Gridview(this);" placeholder="Search.." title="Type" class="form-control">
                                        </div>


                                        <div class="col-md-1">

                                            <%--<label class="control-label">Page</label>--%>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control custom-select"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem Selected="true">50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                                <asp:ListItem>600</asp:ListItem>
                                                <asp:ListItem>900</asp:ListItem>
                                                <asp:ListItem>1000</asp:ListItem>
                                                <asp:ListItem>2000</asp:ListItem>
                                                <asp:ListItem>3000</asp:ListItem>
                                                <asp:ListItem>4000</asp:ListItem>
                                                <asp:ListItem>5000</asp:ListItem>
                                                <asp:ListItem>7000</asp:ListItem>
                                                <asp:ListItem>8000</asp:ListItem>
                                                <asp:ListItem>10000</asp:ListItem>
                                            </asp:DropDownList>


                                        </div>



                                        <%--<asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>--%>

                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control col-md-2 ml-1" Visible="false"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Cal2" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                        <div class="cold-md-1 ml-1">
                                            <asp:LinkButton ID="lnkOk" runat="server" Text="OK" OnClick="lnkOk_Click" CssClass="btn btn-success"></asp:LinkButton>
                                        </div>

                                        <div class="col-md-2 text-danger" id="divPermntDel" runat="server">
                                            <asp:CheckBox ID="Chkpdelete" runat="server" CssClass="form-control checkbox" Text="&nbsp;P.Delete" />

                                        </div>



                                    </div>


                                    <div class="table-responsive">

                                        <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSummary_RowDataBound"
                                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea" AllowPaging="True" OnPageIndexChanging="gvSummary_PageIndexChanging">
                                            <RowStyle Font-Size="12px" Height="25px" Font-Names="Century Gothic" />
                                            <Columns>

                                                <%--0--%>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "rowid")).ToString("#,##0;(#,##0); ")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <%--1--%>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkgvHeader" runat="server" Font-Bold="True" ToolTip="Edit Header" OnClick="lnkgvHeader_Click"><i class="fa fa-th-large" aria-hidden="true"></i></asp:LinkButton>

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete"
                                                            runat="server" Font-Bold="True" ToolTip="Delete" Style="text-align: right;" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDelete_Click">

                                                        <i class=" fa fa-trash"></i></asp:LinkButton>




                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--2--%>


                                                <asp:TemplateField HeaderText="">

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="ViewData" runat="server" Font-Bold="True" Height="12px" ToolTip="View" Style="text-align: right" OnClick="ViewData_Click"><span><i class="fa fa-eye" aria-hidden="true"></i></span></asp:LinkButton>



                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--3--%>

                                                <asp:TemplateField HeaderText="">

                                                    <HeaderTemplate>


                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                            CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Font-Bold="True" Height="12px" Style="text-align: right" ToolTip="Edit Client Info" Text="Edit" OnClick="lnkEdit_Click"> <span class=" fa   fa-edit"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />



                                                </asp:TemplateField>
                                                <%--4--%>

                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lsircode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>

                                                        <asp:Label ID="ldesig" runat="server" Width="40px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--5--%>
                                                <asp:TemplateField HeaderText="P-ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lsircode1" runat="server" Width="40px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--6--%>
                                                <asp:TemplateField HeaderText="Generated">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgenerated" runat="server" Font-Size="11px" Width="70px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--7--%>

                                                <asp:TemplateField HeaderText="Prospect Details">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ldesc" runat="server" Width="130px"
                                                            Text='<%# 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim()
                                                                         
                                                                    %>'>

                                                             


                                                        </asp:Label>



                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <%--8--%>
                                                <asp:TemplateField HeaderText="Followup">
                                                    <ItemTemplate>

                                                        <asp:Panel ID="pnlfollowup" runat="server" Width="110px" ClientIDMode="Static">

                                                            <asp:Label ID="lbllfollowuplink" Width="70px" Font-Size="11px" runat="server" ToolTip="Followup" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'>
                                                            </asp:Label>

                                                            <asp:LinkButton ID="lbtnView" ClientIDMode="Static" Style="float: right !important;" Width="15px" ToolTip="View" runat="server" OnClick="lbtnView_Click" CssClass="d-none"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkEditfollowup" ClientIDMode="Static" Style="float: right !important;" Width="15px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowup_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                        </asp:Panel>



                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <%--9--%>

                                                <asp:TemplateField HeaderText="Lead Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lprefdesc" runat="server" Width="120px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsta")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--10--%>

                                                <asp:TemplateField HeaderText="Associate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lassoc" runat="server" Width="90px"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <%--11--%>

                                                <asp:TemplateField HeaderText="Team Leader">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbusername" runat="server" Width="90px"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <%--12--%>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbllstatus" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--13--%>

                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="llTyp" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadType")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <%-- <asp:TemplateField HeaderText="Approve Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lappdat" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                                <%-- <asp:TemplateField HeaderText="Lead Source" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lLSrc" runat="server" Width="60px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                 <%--14--%>
                                                <asp:TemplateField HeaderText="Active" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAct" ClientIDMode="Static" Width="12" ToolTip="" runat="server" OnClick="lnkAct_Click"><span class="fa fa-edit"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <%--15--%>



                                                <asp:TemplateField HeaderText="Mobile">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvphone" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--16--%>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvemail" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--17--%>


                                                <asp:TemplateField HeaderText="Occupation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvoccupation" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--18--%>


                                                <asp:TemplateField HeaderText="Residence">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcaddress" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--19--%>



                                                <asp:TemplateField HeaderText="Interested Project">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpactdesc" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--20--%>


                                                <asp:TemplateField HeaderText="Source">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvLSrc" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--21--%>


                                                <asp:TemplateField HeaderText="Last discussion">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Width="100px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ldiscuss")) %>'></asp:Label>   
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <%--22--%>


                                                <asp:TemplateField HeaderText="Notes">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvnotes" runat="server" Width="150px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "virnotes")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--23--%>

                                                <asp:TemplateField HeaderText="Prefered Location" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgprefdesc" runat="server" Width="120px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <%--24--%>

                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvempid" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <%--25--%>

                                                <asp:TemplateField HeaderText="Retreive" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnRetreive" runat="server" Font-Bold="True" Height="12px" ToolTip="Retreive Prospect" Style="text-align: right" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkbtnRetreive_Click"><span><i class="fa fa-undo" Style="text-align: center"></i></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                 <%--26--%>
                                                 <asp:TemplateField HeaderText="Next Followup" Visible="false">
                                                    <ItemTemplate> 
                                                             <asp:Label ID="lbllfollowuplinkkpisum" Width="90px" runat="server" 
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy")%>'>                                                               
                                                        </asp:Label>
  
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                 <%--27--%>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                                        <asp:GridView ID="gvkpi" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea ">
                                            <RowStyle Height="25px" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNokpi" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgbempid" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>





                                                <asp:TemplateField HeaderText="Employee Name">


                                                    <HeaderTemplate>
                                                        <div class="row">
                                                            <div class="col-md-9">
                                                                <asp:Label ID="lblgvheadername" runat="server">Employee Name</asp:Label>

                                                            </div>


                                                            <div class="col-md-2">
                                                                <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                                    CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                            </div>


                                                        </div>


                                                    </HeaderTemplate>



                                                    <ItemTemplate>
                                                        <asp:Label ID="lowner" runat="server" Width="200px" Font-Size="10px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtxtTotal" runat="server" Style="text-align: center"
                                                            Width="60px" Text="Total"></asp:Label>
                                                    </FooterTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Call">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpicall" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "call")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFcallsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ext. Meeting">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpiextmeeting" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "extmeeting")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFexmeetingsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Internal. Meeting">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpiintmeeting" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intmeeting")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFintmeetingsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="visit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpivisit" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "visit")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFvisitsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>






                                                <asp:TemplateField HeaderText="Proposal">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpiproposal" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proposal")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFproposalsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Leads">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpileads" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leads")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFleadssum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Closing">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpiclosing" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "close")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFclosingsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Others">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpiothers" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "others")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFotherssum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpitotal" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpitotal_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "total")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFtotalsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="center" VerticalAlign="Middle" Font-Size="12px" />

                                                </asp:TemplateField>






                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>



                                    </div>

                                    <div class="col-md-12">

                                        <asp:Label runat="server" ID="lblkpiDetails" Visible="false" CssClass="d-block" Text="Kpi Details"></asp:Label>

                                    </div>


                                    <asp:GridView ID="gvkpidet" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvkpidet_RowDataBound"
                                        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea " PageSize="10">
                                        <RowStyle Font-Size="11px" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDeletekpisum" runat="server" Font-Bold="True" Height="16px" ToolTip="Delete" Style="text-align: right" Text="Delete" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDeletekpisum_Click"><span class=" fa   fa-recycle"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNokpisum" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "rowid")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lsircodekpisum" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lsircode1kpisum" runat="server" Width="50px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Generated">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgeneratedkpisum" runat="server" Width="70px"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Followup">
                                                <ItemTemplate>

                                                    <asp:Panel ID="pnlfollowupkpisum" runat="server" Width="110px" ClientIDMode="Static">



                                                        <asp:Label ID="lbllfollowuplinkkpisum" Width="70px" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy")%>'>
                                                                                             


                                                        </asp:Label>

                                                        <asp:LinkButton ID="lbtnViewkpisum" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="View" runat="server" OnClick="lbtnViewkpisum_Click"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                        <asp:LinkButton ID="lnkEditfollowupkpisum" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowupkpisum_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                    </asp:Panel>




                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:HyperLink ID="hlbtntbCdataExelkpisum" runat="server"
                                                        CssClass="btn  btn-primary  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEditkpisum" runat="server" Font-Bold="True" Height="16px" Style="text-align: center" ToolTip="Edit Land & Owner Info" Text="Edit" OnClientClick="javascript:return  FunConfirmEdit()" OnClick="lnkEditkpisum_Click"> <span class=" fa   fa-edit"></span></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />



                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prospect Details">
                                                <%-- <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchproperty" SortExpression="sirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Property Details" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="ldesckpisum" runat="server" Width="220px" Style="font-size: 10px;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim() %>'>
                                                    </asp:Label>



                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Lead Status">
                                                <%--  <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchstatus" SortExpression="lstatus" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Status" onkeyup="Search_Gridview(this,11)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllstatuskpisum" runat="server" Width="110px" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsta"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Associate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lassockpisum" runat="server" Width="90px"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                            <%--10--%>

                                            <asp:TemplateField HeaderText="Team Leader">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvteamdesckpi" runat="server" Width="90px"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                                </ItemTemplate>


                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Notes">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnotes" runat="server" Width="150px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "virnotes")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Prefered Location" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgprefdesc" runat="server" Width="120px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>













                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvempidkpisum" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>

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

                                <div class="col-md-2 marapaddingzero">
                                    <label class="control-label" style="font-size: 14px; font-weight: bold">Notification</label>


                                    <div class="list-group list-group-bordered mb-3 notifsectino">
                                        <asp:LinkButton ID="lnkbtnDws" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnDws_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-primary">SW </div>
                                            </div>
                                            <div class="list-group-item-body" id="tdaswhtxt" runat="server">Schedules Work</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lbldws" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkbtnTODayTask" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnTODayTask_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-primary">TD </div>
                                            </div>
                                            <div class="list-group-item-body">To Day Task</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lbltdt" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>


                                        <asp:LinkButton ID="lnkBtnDwr" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnDwr_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-success">DWR </div>
                                            </div>
                                            <div class="list-group-item-body">
                                                Daily Work Report
                                            </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lbldwr" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnKpi" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnKpi_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-info">KPI </div>
                                            </div>
                                            <div class="list-group-item-body">
                                                Key Performance Indicator
                                            </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="Span1" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>



                                        <asp:LinkButton ID="lnkBtnCall" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnCall_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-warning">Call </div>
                                            </div>
                                            <div class="list-group-item-body">Call</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblCall" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkBtnVisit" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnVisit_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-success">Visit </div>
                                            </div>
                                            <div class="list-group-item-body">Visit</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblvisit" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>


                                        <asp:LinkButton ID="lnkBtnDaypassed" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnDaypassed_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-danger">DP  </div>
                                            </div>
                                            <div class="list-group-item-body">Day Passed</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblDayPass" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lbtnpme" class="list-group-item list-group-item-action" runat="server" OnClick="lbtnpme_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-pink">PME</div>
                                            </div>
                                            <div class="list-group-item-body">
                                                Project Meeting External
                                            </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblpme" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lbtnpmi" class="list-group-item list-group-item-action" runat="server" OnClick="lbtnpmi_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-dark">PMI</div>
                                            </div>
                                            <div class="list-group-item-body">
                                                Project Meeting Internal
                                            </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblpmi" runat="server">0</span>
                                                </button>
                                            </div>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkBtnComments" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnComments_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-indigo">COM</div>
                                            </div>
                                            <div class="list-group-item-body">Comments</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblComments" runat="server">0</span>
                                                </button>
                                            </div>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkBtnFreezing" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnFreezing_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-purple">FRE</div>
                                            </div>
                                            <div class="list-group-item-body">Freezing</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblFreez" runat="server">0</span>
                                                </button>
                                            </div>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkBtnDeadProspect" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnDeadProspect_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-pink">DP </div>
                                            </div>
                                            <div class="list-group-item-body">Dead Pros</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblDeadProspect" runat="server">0</span>
                                                </button>
                                            </div>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lbtnSigned" class="list-group-item list-group-item-action" runat="server" OnClick="lbtnSigned_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-dark">Si</div>
                                            </div>
                                            <div class="list-group-item-body">Signed</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblcsigned" runat="server">0</span>
                                                </button>
                                            </div>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkBtnDatablank" class="list-group-item list-group-item-action" runat="server" OnClick="lnkBtnDatablank_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-primary">DB</div>
                                            </div>
                                            <div class="list-group-item-body">Data Bank</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblDatablank" runat="server">0</span>
                                                </button>
                                            </div>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkBtnPotentialPros" runat="server" class="list-group-item list-group-item-action" OnClick="lnkBtnPotentialPros_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-red">PP</div>
                                            </div>
                                            <div class="list-group-item-body">Potential</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblPotential" runat="server">0</span>
                                                </button>
                                            </div>
                                        </asp:LinkButton>


                                        <asp:HiddenField ID="hdnlblattribute" runat="server" />


                                    </div>



                                    <label class="control-label" style="font-size: 14px; font-weight: bold">Operations</label>
                                    <div class="form-group">
                                        <ul style="list-style: none; padding-left: 0px">
                                            <li>
                                                <asp:HyperLink ID="HyperLink3" Target="_blank" NavigateUrl="~/F_21_Mkt/ClientInitial?Type=MktCl" runat="server">Primary Lead</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="hllnkCodebook" Target="_blank" NavigateUrl="~/F_21_Mkt/MktGenCodeBook" runat="server">CRM Code Entry</asp:HyperLink>

                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkbtnReturn" runat="server" OnClick="lnkbtnReturn_Click">Return List</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_21_Mkt/RptSalesFunnel" runat="server">Sales Funnel Reports</asp:HyperLink>

                                            </li>

                                            <li>
                                                <asp:HyperLink ID="hlnkalldiscusssion" Target="_blank" NavigateUrl="~/F_21_Mkt/ClientDiscuDetails" runat="server">All Discussion</asp:HyperLink>
                                            </li>

                                            <li>
                                                <asp:HyperLink ID="HyperLink8" Target="_blank" NavigateUrl="~/F_21_MKT/RptCrmNeedBase?Type=Report" runat="server"> Client Need Base
                                                </asp:HyperLink>
                                            </li>

                                            <li>
                                                <asp:HyperLink ID="HyperLink2" Target="_blank" NavigateUrl="~/F_21_Mkt/RptSalesRegressionFunnel" runat="server">Regression Funnel Stage</asp:HyperLink>

                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkbtnNotes" runat="server" OnClick="lnkbtnNotes_Click">Notes</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="hlnkTeamMember" Target="_blank" NavigateUrl="~/F_21_Mkt/MktTeamMember" runat="server">Team Member</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink4" Target="_blank" NavigateUrl="~/F_99_Allinterface/CRMDashboard" runat="server">CRM Dashboard</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink7" Target="_blank" NavigateUrl="~/F_21_Mkt/MonthsWiseSale?Type=CRM" runat="server">Monthly Sales Report</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink5" Target="_blank" NavigateUrl="~/F_21_Mkt/YearlyActivitiesTarget?Type=CRM" runat="server">Yearly Activities Target Set</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink6" Target="_blank" NavigateUrl="~/F_21_Mkt/YearlyTargetVSAchive?type=CRM" runat="server">Yearly Target Vs Achievement</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink10" Target="_blank" NavigateUrl="~/F_21_Mkt/ProspectTransfer" runat="server">Prospect Transfer</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink9" Target="_blank" NavigateUrl="~/F_21_Mkt/ProspectTransferLog" runat="server">Prospect Transfer Log</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="hlnkProsWorkingReport" runat="server" Target="_blank" NavigateUrl="~/F_21_Mkt/RptProspectWorking">Prospect Working Report</asp:HyperLink>
                                            </li>
                                             <li>
                                                <asp:HyperLink ID="hlnkProsWorkingReportDayWise" runat="server" Target="_blank" NavigateUrl="~/F_21_Mkt/RptProspectWorking?Type=RptDayWise">Associate Working Report (Day Wise)</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkPerDeleteProspect" runat="server" Target="_blank" NavigateUrl="~/F_21_Mkt/RptPerDeleteProspect"> Per. Delete Prospect</asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
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
                    <div class="modal fade" id="ViewModal" tabindex="-1" role="dialog" aria-labelledby="viewModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="viewModalLabel">
                                        <asp:Label runat="server" ID="lheader" CssClass="label-control"></asp:Label></h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>

                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Name</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lname" CssClass="label-control">Some Text</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Primary Phone</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lphn" CssClass="label-control">Some Text</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Designation</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="ldesig" CssClass="label-control">Lead Source</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Lead Source</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lLSrc" CssClass="label-control">Some Text</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Assign Date</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lassdt" CssClass="label-control">Assign To</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Assign To</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lassto" CssClass="label-control">Some Text</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Created By</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lcreateby" CssClass="label-control">Some Text</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Lead Quality</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lleadquality" CssClass="label-control">Assign To</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Sales FeedBack</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lsalfd" CssClass="label-control">Some Text</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Project Name</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="lprjname" CssClass="label-control">Some Text</asp:Label>
                                        </div>
                                        <div class="col-md-4 mb-1">
                                            <asp:Label runat="server" CssClass="label-control">Description</asp:Label>
                                        </div>
                                        <div class="col-md-8 mb-1">
                                            <asp:Label runat="server" ID="ldesc" CssClass="label-control">Some Text</asp:Label>
                                        </div>
                                    </div>


                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="GridHeader" tabindex="-1" role="dialog" aria-labelledby="gridModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="gridModalLabel">Select Grid Header</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            Available Fields
                                    <asp:GridView ID="gvCurrent" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkgv" runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lindex" runat="server" Font-Size="10px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lLSrc" runat="server" Font-Size="13px" CssClass="ml-3"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                                        <div class="col-md-6">
                                            Selected Fields
                                    <asp:GridView ID="gvPrev" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-condensed tblborder grvContentarea ml-3 visibleshow">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkgv" runat="server" Checked="true" />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lindex" runat="server" Font-Size="10px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lLSrc" runat="server" Font-Size="10px" CssClass="ml-3"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <asp:LinkButton ID="lnkgvListShow" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="CloseGvModal();" OnClick="lnkgvListShow_Click">Update List</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>


            </div>
            <!-- Modal -->

            <div id="mdiscussion" class="modal fade   animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-full-width  ">
                    <div class="modal-content modal-content-full-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>
                                Discussion </h4>

                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>


                        </div>
                        <div class="modal-body ">




                            <div class="row">

                                <div class="col-xs-7 col-sm-7 col-md-7">

                                    <p>
                                         <strong>PID: </strong><span id="lblPID" runat="server"></span>
                                        <br>
                                        <strong><span id="lblprosname" runat="server"></span></strong>
                                        <br />
                                        <strong>Contact Person: </strong><span id="lblContactPerson" runat="server"></span>
                                        <br>
                                        <strong>Primary : </strong><span id="lblprosphone" runat="server"></span>

                                        <br>
                                        <strong>Home Address: </strong><span id="lblprosaddress" runat="server"></span>
                                        <br>

                                        <strong>Notes: </strong><span id="lblnotes" runat="server"></span>
                                        <br>
                                    </p>

                                    <p>

                                        <strong>Prefered Area: </strong><span id="lblpreferloc" runat="server"></span>
                                        <br>
                                        <strong>Appartment Size: </strong><span id="lblaptsize" runat="server"></span>
                                         <br>
                                        <strong>Profession: </strong><span id="lblProfession" runat="server"></span>
                                         <br>
                                        <strong>Source: </strong><span id="lblSource" runat="server"></span>

                                        <asp:HiddenField ID="lblproscod" runat="server" />
                                        <asp:HiddenField ID="lbleditempid" runat="server" />
                                        <asp:HiddenField ID="lblgeneratedate" runat="server" />
                                    </p>
                                </div>

                                <div class="col-xs-2 col-sm-2 col-md-2 ">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary ml-1" type="button">Rate</button>
                                        </div>

                                        <asp:DropDownList ID="ddlRating" runat="server" OnSelectedIndexChanged="ddlRating_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0.00">0</asp:ListItem>
                                            <asp:ListItem Value="5.00">5</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>


                                </div>

                                <div class="col-xs-3 col-sm-3 col-md-3 ">

                                    <%--<asp:LinkButton ID="lbtntfollowupcs" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa fa-handshake"></i> Followup</asp:LinkButton>--%>

                                    <button type="button" class="btn  btn-success btn-xs" id="lbtntfollowup" data-toggle="collapse" data-target="#followup"><i class="fa fa-handshake"></i>Followup</button>
                                    <button type="button" class="btn  btn-success btn-xs" id="lbtnStatus"><i class="fa  fa-star-and-crescent"></i><span id="lbllaststatus" runat="server">Status</span></button>
                                    <asp:HiddenField ID="hiddenLedStatus" runat="server" />

                                    <%--<asp:LinkButton ID="lbtntfollowup" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa fa-handshake"></i> Followup</asp:LinkButton>--%>
                                    <%-- <asp:LinkButton ID="lbtnStatus" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnStatus_Click"> <i  class="fa  fa-star-and-crescent"></i> Status</asp:LinkButton>
                                 <asp>:LinkButton ID="lbtnMap" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa   fa-map"></i> Map</asp:LinkButton>--%>
                                </div>

                            </div>


                            <div id="Status" class="col-md-12 collapse">
                                <div class="card-body">
                                    <div class="row">

                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="control-label  lblmargin-top9px">Status</label>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlmStatus" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <button type="button" id="btnStatus" class="btn btn-primary  btn-sm " onclick="funStatus();">Update</button>

                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-12 col-lg-12">

                                    <div id="followup" class="collapse">

                                        <asp:GridView ID="gvInfo" runat="server" AllowPaging="false"
                                            AutoGenerateColumns="False" ShowFooter="true"
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
                                                <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="displayhide">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCodedis" ClientIDMode="Static" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="49px"></asp:Label>
                                                        <asp:Label ID="lblgvTime" runat="server" BorderWidth="0" BackColor="Transparent" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtime")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcGrpdis" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc"))  + "</B>" %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc1dis" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                                                    <FooterTemplate>

                                                        <asp:LinkButton ID="lbtnUpdateDiscussion" runat="server" OnClientClick="CloseModaldis();" OnClick="lbtnUpdateDiscussion_Click" CssClass="btn  btn-success btn-xs ">Final Update</asp:LinkButton>

                                                    </FooterTemplate>
                                                    <ItemTemplate>



                                                        <asp:TextBox ID="txtgvValdis" runat="server" BorderWidth="0" BackColor="Transparent" Font-Size="14px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>




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
                                                            <asp:ListBox ID="ddlPartic" runat="server" SelectionMode="Multiple" class="form-control chosen-select" Style="width: 300px !important;"
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
                                                            <asp:DropDownList ID="ddlVisit" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control" Style="width: 300px !important;">
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
                            <div class="row">


                                <div class="col-md-12 col-lg-12">
                                    <asp:Repeater ID="rpclientinfo" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>


                                            <div class="col-md-12  col-lg-12">
                                                <div class="well">

                                                    <div class="col-sm-12 panel">

                                                        <div class=" col-sm-12">

                                                            <p>
                                                                <strong><%# DataBinder.Eval(Container, "DataItem.prosdesc")%></strong> <%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString() %>  on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %><br>




                                                                <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString() %><br>


                                                                <strong>Summary:</strong><span class="textwrap"><%# DataBinder.Eval(Container, "DataItem.discus").ToString() %></span><br>



                                                                <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString() %> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                                <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString() %>





                                                                <br>
                                                            </p>







                                                        </div>

                                                        <%--<button type="button" class="btn  btn-primary btn-xs" id="lbtnReschdule" data-toggle="collapse" data-target='<%# "#divreschedule"+DataBinder.Eval(Container, "DataItem.rownum").ToString() %>'>Reschedule</button>--%>

                                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnreschedule" onclick="funReschedule('<%# DataBinder.Eval(Container, "DataItem.cdate").ToString()%>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>')">Re-Schdule</button>

                                                        <button type="button" class="btn btn-primary btn-xs" id="lbtnCancel" onclick="funCancel('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>')">Delete</button>
                                                        <asp:LinkButton ID="lbtnFollowup" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnFollowup_Click"> Followup</asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnAddition" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnAddition_Click">Addition</asp:LinkButton>
                                                        <button type="button" class="btn btn-primary btn-xs" runat="server" id="lbtnComments" data-toggle="collapse" data-target='<%# "#dcomments"+DataBinder.Eval(Container, "DataItem.rownum").ToString() %>'>Comments</button>

                                                        <div class="col-md-12 collapse dcomments" id="divreschedule<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>">



                                                            <asp:TextBox ID="txtdate" runat="server" ClientIDMode="Static" CssClass=""></asp:TextBox>
                                                            <cc1:CalendarExtender ID="Cal2" runat="server"
                                                                Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>


                                                            Subject:
                                                    <textarea name="lblsubjects" id="lblsubjects" style="width: 300px"></textarea>
                                                            Reason:
                                                    <textarea name="lblreason" id="lblreason" style="width: 300px"></textarea>

                                                            <%--<button type="button" class="btn  btn-success btn-xs" onclick="funReschedule('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>')">Post</button>--%>
                                                            <button type="button" class="lbtnschedule">Post</button>

                                                            <input type="hidden" id="lblcdate" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>" />


                                                        </div>



                                                        <%--<asp:LinkButton ID="lbtnComments" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnComments_Click"    data-toggle="collapse" data-target="#dcomments">Comments</asp:LinkButton>--%>



                                                        <div class="col-md-12 collapse dcomments" id="dcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>">

                                                            <textarea name="lblcomments" id="lblcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>" style="width: 300px"></textarea>
                                                            <br>
                                                            <input type="text" name="txtcomdate" class="datepicker" id="txtcomdate<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("MM/dd/yyyy") %>" style="width: 300px"></input>

                                                            <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments" onclick="funPost('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString() %>')">Post</button>



                                                        </div>

                                                        <%--  <button type="button" class="btn btn-primary btn-xs" runat="server" id="Button1" data-toggle="collapse" data-target="#dcomments" >Comments</button>

                                    <div class="col-md-12 collapse "  id="dcomments">

                                      <input type="text"  name="lblcomments" id="lblcomments" />
                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments"  >Post</button>
                                      


                                    </div>--%>
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

            <div id="detnotification" class="modal fade   animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-full-width">
                    <div class="modal-content modal-content-full-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Information </h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>

                        </div>
                        <div class="modal-body ">
                            <div class="table-responsive">


                                <table id="tblinformation" class="table-striped table-hover table-bordered  grvContentarea">
                                    <thead>
                                        <tr class="grvHeader">
                                            <th class='tSl'>Sl</th>
                                            <th class='tStatus'>PID</th>
                                            <th class='tdate'>Generated</th>
                                            <th class='tdatetime'>Prospect Details</th>
                                            <th class='tDiscussion'>Follow Up</th>
                                            <th class='tParticipants'>Preferrence</th>
                                            <th class='tAssociateadeal'>Associate</th>
                                            <th class='tAssociateadeal'>Team Lead</th>
                                            <th class='tStatus'>Progress</th>
                                            <th class='tStatus'>Priority</th>

                                            <%--<th class='tSl'>SL</th>
                                    <th class='tPid'>L-ID</th>
                                    <th class='tdate'>Generated</th>
                                    <th class='tCdetails'>Last Followup</th>
                                    <th class='tAssociate'>Ower</th>
                                    <th class='tTeamHead'>Team Head</th>
                                    <th class='tStatus'>Status</th>
                                    <th class='tType'>Type</th>
                                    <th class='tActive'>Active</th>
                                    <th class='tMobile'>Mobile</th>
                                    <th class='tEmail'>Email</th>
                                    <th class='tOccupation'>Occupation</th>
                                    <th class='tResidence'>Residence</th>
                                    <th class='tIntProject'>Interested Project</th>
                                    <th class='tSource'>Source</th>
                                    <th class='tFeedBack'>FeedBack</th>
                                    <th class='tComments'>Commments</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>




            <div id="modalComments" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-mid-width">
                    <div class="modal-content modal-content-mid-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Comments Information </h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>

                        </div>
                        <div class="modal-body ">
                            <asp:GridView ID="gvComments" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered  grvContentarea ml-3">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomSl" runat="server" Height="10px"
                                                Style="text-align: left" Font-Size="12px"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px">

                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="P-ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomsircode1" runat="server" Width="40px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Generated">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomgenerated" runat="server" Width="80px" Font-Size="11px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prospect Details">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomdesc" runat="server" Width="120px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Preferrence ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomprefdesc" runat="server" Width="120px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prefdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Associate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomassoc" runat="server" Width="150px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Team Leader">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomTeam" runat="server" Width="120px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Width="220px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gnote")) %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Views">


                                        <%--<HeaderTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="chklblall" runat="server">Check All</asp:Label>
                                                        <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" Text="" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCommentView" runat="server" Width="40px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>

                        <div class="modal-footer">
                            <asp:LinkButton ID="btnSaveComments" runat="server" OnClick="btnSaveComments_Click" OnClientClick="closeComModal();" CssClass="btn btn-primary">Save</asp:LinkButton>
                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>



            <div id="modalNotes" class="modal fade   animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-sm-width ">
                    <div class="modal-content modal-content-sm-width" style="background-color: darksalmon">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>
                                Basic Information </h4>

                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                        </div>
                        <div class="modal-body ">
                            <div class="row">
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th style="font-size: 14px;">SL.No.</th>
                                            <th style="font-size: 14px;">Note Name</th>
                                            <th style="font-size: 14px;">Description</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>DWS</td>
                                            <td>Daily Work Schedule</td>

                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>DWR</td>
                                            <td>Daily Work Report</td>

                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>KPI</td>
                                            <td>Key Performance Indicator</td>

                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td>PME</td>
                                            <td>Project Meeting External</td>

                                        </tr>
                                        <tr>
                                            <td>5</td>
                                            <td>PMI</td>
                                            <td>Project Meeting Internal</td>

                                        </tr>
                                        <tr>
                                            <td>6</td>
                                            <td>Yellow</td>
                                            <td style="color: yellow; font-weight: bold">Followup Days Between 25 to 35</td>

                                        </tr>
                                        <tr>
                                            <td>7</td>
                                            <td>Red</td>
                                            <td style="color: #8B0000;">Followup Days Larger then 35</td>

                                        </tr>
                                        <tr>
                                            <td>8</td>
                                            <td>Light Cyan</td>
                                            <td style="color: lightcyan;">Followup To Days</td>

                                        </tr>

                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div id="modalKpiDetials" class="modal fade   " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-mid-width">
                    <div class="modal-content modal-content-mid-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Kpi Detials </h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>

                        </div>
                        <div class="modal-body ">
                            <asp:GridView ID="gvKpiDetials" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered  grvContentarea ml-3">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomSl" runat="server" Height="10px"
                                                Style="text-align: left" Font-Size="12px"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px">

                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="P-ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomsircode1" runat="server" Width="40px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lid")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Generated">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomgenerated" runat="server" Width="80px" Font-Size="11px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prospect Details">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomdesc" runat="server" Width="120px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lead Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomdesc" runat="server" Width="120px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadstatus")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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

                        <div class="modal-footer">

                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

