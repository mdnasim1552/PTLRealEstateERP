<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LandInfoDet.aspx.cs" Inherits="RealERPWEB.F_01_LPA.LandInfoDet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style>
        body {
            font-family: "Century Gothic";
        }


        .grvHeader {
            font-family: "Century Gothic" !important;
        }



        .multiselect-container {
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




        .pullright {
            float: right !important;
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



        .checkbox label {
            margin-left: 5px;
            padding-right: 5px;
        }

        .displayhide {
            display: none !important;
        }
    </style>

    <style>
        .flowMenu ul {
            margin: 0;
        }

            .flowMenu ul li {
                list-style: none;
                padding: 5px 0;
                /*border-bottom: 1px solid #e9e9e9;*/
            }

                .flowMenu ul li a {
                    padding-bottom: 8px;
                    color: #000;
                    font-size: 14px;
                    font-weight: normal;
                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                    font-family: 'Times New Roman';
                }

        .flowMenu h3 {
            background: #046971;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            color: #fff;
            font-family: AR CENA;
            font-size: 18px;
            /*font-weight: bold;*/
            line-height: 40px;
            margin: 5px 0 0;
            padding: 0 0;
            text-decoration: none;
            text-align: center;
        }



        ul.sidebarMenu li {
            display: block;
            list-style: none;
            border: 1px solid #00444C;
            padding: 0;
            /* border-bottom: 0; */
        }

            ul.sidebarMenu li a {
                text-align: left;
                display: block;
                cursor: pointer;
                /* background: #32CD32; */
                background: #046971;
                border-radius: 5px;
                text-align: left;
                padding: 0 5px;
                border-bottom: 1px;
                line-height: 30px;
                color: #fff;
                font-size: 13px;
                font-weight: normal;
                text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
            }

                ul.sidebarMenu li a:hover {
                    background: #43b643;
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
            margin-left: 1100px;
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

        .dcomments {
            margin: 10px 0 0 0;
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
    </style>

    <script type="text/javascript">



        function Search_Gridview(strKey, cellNr) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                var tblData = document.getElementById("<%=this.gvSummary.ClientID%>");
                for (var i = 1; i < tblData.rows.length; i++) {

                    rowData = tblData.rows[i].cells[cellNr].innerHTML;
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

            catch (e) {
                alert(e.message);

            }

        }


        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            document.getElementById('<%= Btn_tempBTN.ClientID %>').click();

        });


        function pageLoaded() {


            try {





                $(".chosen-select").chosen({
                    search_contains: true,
                    no_results_text: "Sorry, no match!",
                    allow_single_deselect: true
                });
                $('.chosen-continer').css('width', '600px');


                $('.chzn-select').chosen({ search_contains: true });



                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $('#<%=this.gvkpidet.ClientID%>').tblScrollable();



                $('.datepicker').datepicker({
                    format: 'mm/dd/yyyy',
                });



                var gvSummary = $('#<%=this.gvSummary.ClientID %>');
                gvSummary.Scrollable();





                var gcod;

                //Schedule Reminder  
                var comcod =<%=this.GetComeCode()%>;

                var arrgschcodl = $('#<%=this.gvInfo.ClientID%>').find('[id$="lblgvItmCodedis"]');
                var arrgsschcheckbox = $('#<%=this.gvInfo.ClientID%>').find('input:text[id$="ChkBoxLstFollow"]');

                var txtnfollowupdate, checkboxlastfollowup;

                for (var i = 0; i < arrgschcodl.length; i++) {


                    gcod = $(arrgschcodl[i]).text();

                    var number;
                    switch (gcod) {
                        //Last Followup
                        case '810100102020':
                            number = i;

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

                    $('input[type=checkbox][id^="' + ChkBoxLstFollow + '"]:checked').each(function (index, item) {

                        lastfollowup = $(item).val();

                    });

                    if (lastfollowup.length > 0) {

                        funschedulenumber(comcod, followupdate, lastfollowup, number);
                    }

                });



                //Duplicate Plot

                var arrgschcodp = $('#<%=this.gvplot.ClientID%>').find('[id$="lblgvItmCodeplot"]');
                var txtgvplotno;
                for (var i = 0; i < arrgschcodp.length; i++) {


                    gcod = $(arrgschcodp[i]).text();
                    var number;
                    switch (gcod) {
                        //Plot Number
                        case '0302013':
                            number = i;
                            break;




                    }

                }

                var txtgvplotno = '#ContentPlaceHolder1_gvplot_txtgvValplot_' + number;


                $(txtgvplotno).change(function () {
                    var plotno = $(this).val();

                    // var distrinct= $('#ContentPlaceHolder1_gvplot_ddlvald_1 option:selected').text();
                    var zone = $('#ContentPlaceHolder1_gvplot_ddlvalz_2 option:selected').text();
                    var ps = $('#ContentPlaceHolder1_gvplot_ddlvalp_3 option:selected').text();
                    var area = $('#ContentPlaceHolder1_gvplot_ddlvala_4 option:selected').text();
                    var block = $('#ContentPlaceHolder1_gvplot_ddlblockplot_5 option:selected').text();
                    var road = $('#ContentPlaceHolder1_gvplot_ddlpnlr_6 option:selected').text();

                    var landplotinfo = zone + ", " + ps + ", " + area + ", " + block + ", " + road + ", Plot: " + plotno;

                    if (!$('#divexland').is(":visible")) {
                        funDupPlot(landplotinfo);




                    }






                });
            }


            catch (e) {

                alert(e.message);
            }




        }

        ///////







        function openModal() {

            $('#contact').modal('toggle');
        }

        function CloseModal() {

            $('#contact').modal('hide');
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

        function openComModal() {
            $('#modalComments').modal('toggle');
        }

        function closeComModal() {
            $('#modalComments').modal('hide');
        }





        function openModaldis() {

            $('#mdiscussion').modal('toggle');
            //$('#mdiscussion').modal('toggle');
            $('#lbtntfollowup').click();
        }


        function CloseModaldis() {

            $('#mdiscussion').modal('hide');
        }

        $("#btnModalClose").on("click", function (e) {
            e.preventDefault();
            $("#mdiscussion").modal("hide");
            $('#mdiscussion').data("modal", null);
        });


        //function CloseModaldisReschedule() {
        //    $('#mdiscussion').modal('toggle');
        //    $('#lbtntfollowup').click();
        //}

        //Comment Emdad 20.01.2022

        //function OpenModal() {
        //    $('#detnotification').modal('show');


        //}

        function AddButton(id) {

            $(".hiddenb" + id).css("display", "inline");

        }
        function HiddenButton(id) {
            $(".hiddenb" + id).css("display", "none");
        }



        <%-- 
        function DetNotification(rtype)
        {
            try
            {

         

                var comcod=<%=this.GetComeCode()%>;
                var empid=<%=this.GetEmpID()%>;  
             

                $.ajax({
                    type: "POST",
                    url: "LandInfoDet.aspx/ShowDetNotification",
                    data: '{comcod:"' + comcod + '",  empid: "' + empid + '", rtype:"'+rtype+'" , date: "' + $('#<%=this.txtdate.ClientID%>').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",                   
                  

                    success: function (response) {
                        //console.log(JSON.parse(response.d));
                        var data = response.d;
                        CreateTable(data);
                        //console.log(data['account']);
                       
                    },

                   
                    failure: function (response) {
                      
                        alert("failure");
                    }
                });

            }

            catch(e)
            {
                alert(e.message);

            }       


        }

       
        
      
        function CreateTable(data)
        {


          

           
            try
            {
                var adata=JSON.parse(data);
                var row = '';
                var i=1;
                $.each(adata,
                    function (key, val) {
                    
                        row += "<tr class='grvRows'>";
                        row += "<td class='tSl'>" +  i+ "</td>";
                        row += "<td class='tStatus'>" + val.lid+ "</td>";
                        row += "<td class='tdate'>" + val.generated1+ "</td>";
                        row += "<td class='tdatetime'>" + val.lfollowup+ "</td>";
                        row += "<td class='tDiscussion'>" + val.ownname+ "</td>";
                        row += "<td class='tDiscussion'>" + val.ldetails+ "</td>";
                        row += "<td class='tAssociateadeal'>" + val.assoc+ "</td>";
                        row += "<td class='tAssociateadeal'>" + val.dealname+ "</td>";
                        row += "<td class='tStatus'>" + val.lstatus+ "</td>";                        
                        row += "<td class='tStatus'>" + val.prio+ "</td>";  
                        
                        row += "<td class='tStatus'>" + "<button id='nfollowup' class='btn btn-primary btn-xs' onclick='funfollowup("+val.prio+")' >Followup</button>"+ "</td>";  
                        
                        
                        row += "</tr>";
                        $("#tblinformation tbody").html(row);
                        i++;
                    

               
                    
                    
                    });

                loadModal();

            }

            catch(e)
            {
            
                alert(e.message);
            
            }

        
        

     
                                           
        
        }--%>


        function funschedulenumber(comcod, followupdate, lastfollowup, number) {

            try {

                var empid =<%=this.GetEmpID()%>;

                var lblschedulenumber = '#ContentPlaceHolder1_gvInfo_lblschedulenumber_' + number;




                $.ajax({

                    url: "LandInfoDet.aspx/GetSchedulenumber",
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


        function funPost(date, number) {
            try {


                var comdate = $('#txtcomdate' + number).val();
                var comcod =<%=this.GetComeCode()%>;
                var comments = $('#lblcomments' + number).val();
                var proscod = $('#<%=this.lblproscod.ClientID%>').val();
                var userid =<%=this.GetUserID()%>;


                $.ajax({
                    type: "POST",
                    url: "LandInfoDet.aspx/UpdatePost",
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

        function funDupPlot(landinfo) {
            try {

                var comcod =<%=this.GetComeCode()%>;

                $.ajax({

                    url: "LandInfoDet.aspx/CheckPlotNo",
                    type: "POST",
                    data: '{comcod:"' + comcod + '", landinfo:"' + landinfo + '"}',
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

        function funStatus() {

            try {



                var comcod =<%=this.GetComeCode()%>;
                var proscod = $('#<%=this.lblproscod.ClientID%>').val();
                var statusid = $('#ddlmStatus option:selected').val();
                var empid =<%=this.GetEmpID()%>;

                $.ajax({
                    type: "POST",
                    url: "LandInfoDet.aspx/UpdateStatus",
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






        };




        function funReschedule(cdate, number) {
            try {


                //var  comdate =$('#txtcomdate'+number).val();
                var comcod =<%=this.GetComeCode()%>;
                var empid =<%=this.GetEmpID()%>;
                var proscod = $('#<%=this.lblproscod.ClientID%>').val();



                $.ajax({
                    type: "POST",
                    url: "LandInfoDet.aspx/GetReschedule",
                    data: '{comcod:"' + comcod + '", empid:"' + empid + '",  proscod: "' + proscod + '", cdate:"' + cdate + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",


                    success: function (response) {

                        var data = JSON.parse(response.d);
                        funDataBind(data);
                        //console.log(data);
                        //var date=data[0].gdesc1;
                        //alert(date);



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

                        case "810100102001": //Followup Date                        
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

                        case "810100102002": //New Followup
                            var ChkBoxLstFollow = '#ContentPlaceHolder1_gvInfo_ChkBoxLstFollow_' + number;
                            var newfollowup = data.gdesc1;
                            if (newfollowup.length <= 7) {


                                $('' + ChkBoxLstFollow + '> input').each(function (index, item) {

                                    if ($(item).val() == newfollowup) {
                                        $(item).attr('checked', true);
                                    }


                                });

                            }
                            else {
                                var ar = new Array();
                                //  alert(newfollowup);
                                var j = 0;
                                for (i = 0; i < newfollowup.length; i = i + 7) {
                                    ar[j++] = newfollowup.substr(i, 7);
                                }

                                //console.log(ar);
                                //  alert(ar.length);

                                for (i = 0; i < ar.length; i++) {

                                    $('' + ChkBoxLstFollow + '> input').each(function (index, item) {
                                        if ($(item).val() == ar[i]) {
                                            $(item).attr('checked', true);
                                        }

                                    });


                                }
                            }

                            break;



                        case "810100102019"://Follow

                            var ChkBoxLstFollow = '#ContentPlaceHolder1_gvInfo_ChkBoxLstFollow_' + number;
                            var newfollowup = data.gdesc1;
                            if (newfollowup.length = 7) {

                                $('' + ChkBoxLstFollow + '> input').each(function (index, item) {
                                    if ($(item).val() == newfollowup) {
                                        $(item).attr('checked', true);

                                    }


                                });

                            }
                            break;





                        case "810100102016": //Status



                            var ChkBoxLstStatus = '#ContentPlaceHolder1_gvInfo_ChkBoxLstStatus_' + number;
                            var status = data.gdesc1;
                            if (status.length = 7) {

                                $('' + ChkBoxLstStatus + '> input').each(function (index, item) {
                                    if ($(item).val() == status) {
                                        $(item).attr('checked', true);

                                    }


                                });

                            }


                            break;

                        case "810100102018": //PARTICIPANTS  




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



                        case "810100102015": //Summary
                        case "810100102025": //Subject

                            var txtgvdValdis = '#ContentPlaceHolder1_gvInfo_txtgvValdis_' + number;
                            $(txtgvdValdis).val(data.gdesc1);
                            break;


                        case "810100102020": //next Followup date
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




        $(document).on('show.bs.modal', '.modal', function (event) {
            var zIndex = 1040 + (10 * $('.modal:visible').length);
            $(this).css('z-index', zIndex);
            setTimeout(function () {
                $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
            }, 0);
        });

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
            <asp:LinkButton ID="Btn_tempBTN" OnClick="Btn_tempBTN_Click" Class="btn btn-sm btn-primary d-none" runat="server">Ok</asp:LinkButton>


            <div class="card card-fluid ">
                <div class="card-body bodyheight">
                    <div class="row">
                        <div class="form-group">
                            <div class="row">
                            </div>
                        </div>
                        <div class=" col-md-2 ml-1">
                            <div class="form-group">

                                <label class="control-label">Land Year</label>
                                <asp:DropDownList ID="ddlyearland" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlyearland_SelectedIndexChanged">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class=" col-md-2 ml-1">
                            <div class="form-group">
                                <asp:LinkButton ID="btnaddland" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="btnaddland_Click">Add Land</asp:LinkButton>

                            </div>
                        </div>
                        <div class=" col-md-2 ml-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtPending" runat="server" CssClass="margin-top30px form-control" OnClick="lbtPending_Click"></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2" runat="server" id="divexland">
                            <div class="form-group">

                                <label class="control-label">Existing Land:</label>
                                <asp:Label runat="server" ID="lbllandname" Font-Size="11px" class="form-control"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1" runat="server" id="divddlinfo">
                            <div class="form-group">
                                <label class="control-label">Information</label>
                                <asp:DropDownList ID="ddlInformation" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlInformation_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" runat="server" id="divLaOw">
                            <div class="form-group">
                                <label class="control-label">No of LandOwner</label>
                                <asp:DropDownList ID="ddlNLandOwner" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlNLandOwner_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">

                                <label class="control-label" for="lblfrmdate">Date</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control   fa fa-pencil-ruler"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>



                            </div>

                        </div>




                        <div class="col-md-2">
                            <div class="form-group">

                                <label class="control-label" for="todate" runat="server" id="lbltodatekpi" visible="false">To</label>
                                <asp:TextBox ID="txtkpitodate" runat="server" CssClass="form-control" Visible="false"> </asp:TextBox>
                                <cc1:CalendarExtender ID="txtkpitodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtkpitodate"></cc1:CalendarExtender>
                            </div>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" visible="false">Page</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="custom-select chzn-select"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1000</asp:ListItem>
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>4000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                    <asp:ListItem>10000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="cold-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkOk" runat="server" Text="OK" OnClick="lnkOk_Click" CssClass="margin-top30px btn btn-primary"></asp:LinkButton>
                            </div>
                        </div>

                    </div>




                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewPersonal" runat="server">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>Land Source:</h5>
                                        <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvPersonalInfo_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc1" runat="server" Width="150px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgph" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                            Width="2px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvalper" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>

                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value"))%>'></asp:TextBox>
                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value"))%>'></asp:TextBox>

                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                        <asp:Panel ID="Panegrd" runat="server">

                                                            <div class="form-group">

                                                                <asp:DropDownList ID="ddlval" runat="server" Width="200px" OnSelectedIndexChanged="ddlval_SelectedIndexChanged" CssClass="custom-select chzn-select" AutoPostBack="true">
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


                                        <div class="form-group">
                                            <h5>Owner Information:</h5>

                                            <asp:GridView ID="GvOwnerLand" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc1" runat="server" Width="100px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvgph" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                                Width="2px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgvallowner" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>

                                                        <ItemTemplate>

                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue"))%>'></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>--%>
                                                            <asp:Panel ID="Panegrd" runat="server">

                                                                <div class="form-group">

                                                                    <asp:DropDownList ID="ddlval" runat="server" Width="150px" CssClass="custom-select chzn-select">
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
                                        <div class="form-group">
                                            <h5>Other Information:</h5>
                                            <asp:GridView ID="gvother" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvItmCode" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDesc1" runat="server" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvgph" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                                Width="2px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Font-Bold="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvgvalother" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>

                                                        <ItemTemplate>

                                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" Width="200px"
                                                                BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value"))%>'></asp:TextBox>

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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>Plot Address:</h5>
                                        <asp:GridView ID="gvplot" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" ControlStyle-CssClass="displayhide">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCodeplot" runat="server" ClientIDMode="Static"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc1" runat="server" Width="150px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgph" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                            Width="2px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvalplot" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>

                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtgvValplot" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value"))%>'></asp:TextBox>
                                                        <asp:TextBox ID="txtgvdValplot" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value"))%>'></asp:TextBox>

                                                        <cc1:CalendarExtender ID="txtgvdValplot_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdValplot"></cc1:CalendarExtender>
                                                        <asp:Panel ID="Panegrd" runat="server">
                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlvalplot" runat="server" OnSelectedIndexChanged="ddlvalplot_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnldist" runat="server">

                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlvald" runat="server" OnSelectedIndexChanged="ddlvald_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlz" runat="server">

                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlvalz" runat="server" OnSelectedIndexChanged="ddlvalz_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlp" runat="server">

                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlvalp" runat="server" OnSelectedIndexChanged="ddlvalp_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                        </asp:Panel>
                                                        <asp:Panel ID="pnla" runat="server">

                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlvala" runat="server" OnSelectedIndexChanged="ddlvala_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                        </asp:Panel>
                                                        <asp:Panel ID="PanelBl" runat="server">

                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlblockplot" runat="server" OnSelectedIndexChanged="ddlblockplot_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                        </asp:Panel>
                                                        <asp:Panel ID="Panelr" runat="server">

                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlpnlr" runat="server" OnSelectedIndexChanged="ddlpnlr_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>Property Details</h5>
                                        <asp:GridView ID="gvpropdet" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc1" runat="server" Width="150px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgph" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                            Width="2px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvalplotdet" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>

                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value"))%>'></asp:TextBox>
                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value"))%>'></asp:TextBox>

                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                        <asp:Panel ID="Panegrd" runat="server">

                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlvalprojdet" runat="server" OnSelectedIndexChanged="ddlval_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>


                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlParic" runat="server" Visible="false">
                                                            <asp:ListBox ID="ddlPartic" runat="server" SelectionMode="Multiple"
                                                                data-placeholder="Choose Documents......" multiple="true" class="form-control chosen-select"></asp:ListBox>
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
                            <div class="row">
                            </div>


                        </asp:View>
                        <asp:View ID="View1" runat="server">

                            <div class="row">

                                <div class="col-md-3">
                                    <h4>Owner Information</h4>
                                    <hr />
                                    <asp:GridView ID="gvOwnerInfo" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="100px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgph" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                        Width="2px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue"))%>'></asp:TextBox>
                                                    <%--<asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "value")) %>'></asp:TextBox>

                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>--%>
                                                    <asp:Panel ID="Panegrd" runat="server">

                                                        <div class="form-group">

                                                            <asp:DropDownList ID="ddlval" runat="server" Width="150px" CssClass="custom-select chzn-select">
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


                                <div class="col-md-3">
                                    <h4>Home Address</h4>
                                    <hr />
                                    <asp:GridView ID="gvLOhomeadd" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="100px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgph" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                        Width="2px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue"))%>'></asp:TextBox>
                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue"))%>'></asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                    <asp:Panel ID="Panegrd" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvalplotA" runat="server" OnSelectedIndexChanged="ddlvalplotA_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnldist" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvaldA" runat="server" OnSelectedIndexChanged="ddlvaldA_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlz" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvalzA" runat="server" OnSelectedIndexChanged="ddlvalzA_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlp" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvalpA" runat="server" OnSelectedIndexChanged="ddlvalpA_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                    <asp:Panel ID="pnla" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvalaA" runat="server" OnSelectedIndexChanged="ddlvalaA_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>

                                                    <asp:Panel ID="PanelBlA" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlblockA" runat="server" OnSelectedIndexChanged="ddlblockA_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>

                                                    <asp:Panel ID="PanelrA" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlpnlrA" runat="server" OnSelectedIndexChanged="ddlpnlrA_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
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

                                <div class="col-md-3">
                                    <h4>Business Details</h4>
                                    <hr />
                                    <asp:GridView ID="gvLOBusns" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="100px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgph" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                        Width="2px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue"))%>'></asp:TextBox>
                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue"))%>'></asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                    <asp:Panel ID="Panegrd" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvalplotAB" runat="server" OnSelectedIndexChanged="ddlvalplotAB_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnldist" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvaldAB" runat="server" OnSelectedIndexChanged="ddlvaldAB_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlz" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvalzAB" runat="server" OnSelectedIndexChanged="ddlvalzAB_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlp" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvalpAB" runat="server" OnSelectedIndexChanged="ddlvalpAB_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                    <asp:Panel ID="pnla" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlvalaAB" runat="server" OnSelectedIndexChanged="ddlvalaAB_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                    <asp:Panel ID="PanelBlAB" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlblockAB" runat="server" OnSelectedIndexChanged="ddlblockAB_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>


                                                    </asp:Panel>
                                                    <asp:Panel ID="PanelrAB" runat="server">

                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlpnlrAB" runat="server" OnSelectedIndexChanged="ddlpnlrAB_SelectedIndexChanged" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
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

                                <div class="col-md-3">
                                    <h4>Personal Details</h4>
                                    <hr />
                                    <asp:GridView ID="gvperLO" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server" Width="100px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvgph" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                        Width="2px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>

                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue"))%>'></asp:TextBox>
                                                    <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gvalue"))%>'></asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                    <asp:Panel ID="Panegrd" runat="server">
                                                        <div class="form-group">
                                                            <div class="col-md-12 pading5px">
                                                                <asp:DropDownList ID="ddlval" runat="server" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true" TabIndex="2">
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

                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="row">
                                <label class="control-label">Record Filter</label>
                                <div class="form-group">
                                    <div class="row">
                                        <asp:DropDownList ID="ddlEmpid" data-placeholder="Choose Employee.." runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                        </asp:DropDownList>



                                        <asp:DropDownList ID="ddlDist" runat="server" CssClass="custom-select chzn-select col-md-2 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged">
                                        </asp:DropDownList>



                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                                        </asp:DropDownList>



                                        <asp:DropDownList ID="ddlPStat" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlPStat_SelectedIndexChanged">
                                        </asp:DropDownList>


                                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                        </asp:DropDownList>



                                        <asp:DropDownList ID="ddlBlock" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                        </asp:DropDownList>



                                        <asp:DropDownList ID="ddlRoad" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1">
                                        </asp:DropDownList>



                                        <asp:DropDownList ID="ddlPri" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1">
                                        </asp:DropDownList>



                                        <asp:DropDownList ID="ddlStatus" data-placeholder="Status" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1">
                                        </asp:DropDownList>


                                        <asp:DropDownList ID="ddlOther" runat="server" CssClass="custom-select chzn-select col-md-1 ml-1">
                                            <asp:ListItem Value="1">Plot/House No</asp:ListItem>
                                            <asp:ListItem Value="2">LID</asp:ListItem>
                                            <asp:ListItem Value="3">Land/Owner</asp:ListItem>
                                            <asp:ListItem Value="4">Phone</asp:ListItem>
                                            <asp:ListItem Value="5">Email</asp:ListItem>
                                            <asp:ListItem Value="6">NID</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="7">Choose One..</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtVal" runat="server" CssClass="form-control col-md-1 ml-1" TextMode="Search"></asp:TextBox>

                                        <asp:LinkButton ID="lUpdatInfo" runat="server" OnClick="lUpdatInfo_Click" class="btn btn-success form-control col-md-1 ml-1" OnClientClick="CloseModal();">Search</asp:LinkButton>

                                    </div>
                                </div>

                            </div>

                            <div class="row">

                                <div class="col-md-10">


                                    <asp:HiddenField ID="lblIntputtype" runat="server" />
                                    <asp:HiddenField ID="hdnfrpttype" runat="server" />
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSummary_RowDataBound"
                                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea " AllowPaging="true" OnPageIndexChanging="gvSummary_PageIndexChanging" PageSize="10">
                                            <RowStyle Font-Size="11px" Height="25px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Font-Bold="True" Height="16px" ToolTip="Delete" Style="text-align: right" Text="Delete" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDelete_Click"><span class=" fa   fa-recycle"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "rowid")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lsircode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lsircode1" runat="server" Width="50px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Generated">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgenerated" runat="server" Width="70px"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Followup">
                                                    <ItemTemplate>

                                                        <asp:Panel ID="pnlfollowup" runat="server" Width="110px" ClientIDMode="Static">

                                                            <asp:Label ID="lbllfollowuplink" Width="70px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lfollowup"))%>'>
                                                                                             


                                                            </asp:Label>

                                                            <asp:LinkButton ID="lbtnView" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="View" runat="server" OnClick="lbtnView_Click"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkEditfollowup" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowup_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                        </asp:Panel>




                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Owner Details">

                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtSearchownname" SortExpression="ownname" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Owner Details" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />

                                                    </HeaderTemplate>


                                                    <ItemTemplate>
                                                        <asp:Label ID="lowner" runat="server" Width="90px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ownname"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                            CssClass="btn  btn-primary  btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Font-Bold="True" Height="16px" Style="text-align: center" ToolTip="Edit Land & Owner Info" Text="Edit" OnClientClick="javascript:return  FunConfirmEdit()" OnClick="lnkEdit_Click"> <span class=" fa   fa-edit"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />



                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Property Details">
                                                    <%-- <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchproperty" SortExpression="sirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Property Details" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:Label ID="ldesc" runat="server" Width="180px" Style="font-size: 10px;"
                                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim() %>'>

                                                        </asp:Label>



                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Associate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lassoc" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />


                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Dealing">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbusername" runat="server" Width="90px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <%--  <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchstatus" SortExpression="lstatus" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Status" onkeyup="Search_Gridview(this,11)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lbllstatus" runat="server" Width="60px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus"))%>'></asp:Label>
                                                    </ItemTemplate>


                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Prio">
                                                    <%--  <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprio" SortExpression="prio" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Prio" onkeyup="Search_Gridview(this,12)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lprio" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prio"))%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>


                                                        <asp:LinkButton ID="lnkAct" ClientIDMode="Static" Width="10" ToolTip="Active" runat="server" OnClick="lnkAct_Click">
                                                            <i class="fa fa-toggle-off"  style='font-size:24px;color:red' ></i>
                                                            
                                                        </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last </br>Follow. </br>Dur.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfollowdday" runat="server" Width="20px" Style="text-align: center;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followdday"))%>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvempid" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid"))%>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Phone/Mobile">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcphone" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cphone"))%>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Retreive" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnRetreive" runat="server" Font-Bold="True" Height="12px" ToolTip="Retreive Prospect" Style="text-align: right" OnClientClick="javascript:return  FunConfirm()" 
                                                            OnClick="lnkbtnRetreive_Click"><span><i class="fa fa-undo" Style="text-align: center"></i></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:TemplateField>


                                                <%--<asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" OnClientClick="return confirm('Do You want to edit this item?');" CssClass="btn btn-warning btn-sm"><span class="fa fa-edit"></span></asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return confirm('Do You want to delete this item?');" CssClass="btn btn-danger btn-sm"><span class="fa fa-trash"></span></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
                                                        <asp:Label ID="lblgvFcall" runat="server" Style="text-align: center"
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
                                                        <asp:Label ID="lblgvFexmeeting" runat="server" Style="text-align: center"
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
                                                        <asp:Label ID="lblgvFintmeeting" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Suevey">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpisurvey" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "survey")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFsurvey" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Feasibility">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvkpifeasibility" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "feasibility")).ToString("#,##0;(#,##0); ")%>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFfeasibility" runat="server" Style="text-align: center"
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
                                                        <asp:Label ID="lblgvFproposal" runat="server" Style="text-align: center"
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
                                                        <asp:Label ID="lblgvFleads" runat="server" Style="text-align: center"
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
                                                        <asp:Label ID="lblgvFclosing" runat="server" Style="text-align: center"
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
                                                        <asp:Label ID="lblgvFothers" runat="server" Style="text-align: center"
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
                                                        <asp:Label ID="lblgvFtotal" runat="server" Style="text-align: center"
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
                                            <asp:TemplateField HeaderText="LID">
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

                                                        <asp:Label ID="lbllfollowuplinkkpisum" Width="70px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lfollowup"))%>'>
                                                                                             


                                                        </asp:Label>

                                                        <asp:LinkButton ID="lbtnViewkpisum" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="View" runat="server" OnClick="lbtnViewkpisum_Click"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                        <asp:LinkButton ID="lnkEditfollowupkpisum" ClientIDMode="Static" Style="float: right !important;" Width="10px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowupkpisum_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                    </asp:Panel>




                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="Owner Details">



                                                <ItemTemplate>
                                                    <asp:Label ID="lownerkpisum" runat="server" Width="90px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ownname"))%>'></asp:Label>
                                                </ItemTemplate>
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
                                            <asp:TemplateField HeaderText="Property Details">
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



                                            <asp:TemplateField HeaderText="Associate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lassockpisum" runat="server" Width="60px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />


                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dealing">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbusernamekpisum" runat="server" Width="90px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <%--  <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchstatus" SortExpression="lstatus" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Status" onkeyup="Search_Gridview(this,11)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllstatuskpisum" runat="server" Width="60px" Style="text-align: center"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus"))%>'></asp:Label>
                                                </ItemTemplate>


                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prio">
                                                <%--  <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprio" SortExpression="prio" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Prio" onkeyup="Search_Gridview(this,12)"></asp:TextBox><br />

                                                </HeaderTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:Label ID="lpriokpisum" runat="server" Width="60px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prio"))%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Last </br>Follow. </br>Dur.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfollowddaykpisum" runat="server" Width="20px" Style="text-align: center;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followdday"))%>'></asp:Label>
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

                                <%--    <div class="col-md-1 marapaddingzero">

                                <asp:LinkButton ID="lnkShowNotifcation" runat="server" Text="Show Notification" OnClick="lnkShowNotifcation_Click" CssClass="btn btn-x m-0 p-0 pt-1  pb-5 btn-primary"></asp:LinkButton>

                                    
                                    <asp:HiddenField ID="hdnlblattribute" runat="server" />
                                    <ul style="list-style: none; padding-left: 0px">
                                        <li>
                                            <asp:LinkButton ID="lnkbtnDws" runat="server" OnClick="lnkbtnDws_Click">DWS <sup><span class="badgei" id="cdws" runat="server">0</span></sup></asp:LinkButton>

                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkbtnDwr" runat="server" OnClick="lnkbtnDwr_Click">DWR <sup><span class="badgei" id="cdwr" runat="server">0</span></sup></asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkbtnOther" runat="server" OnClick="lnkbtnOther_Click">Others Activites <sup><span class="badgei" id="cothact" runat="server">0</span></sup></asp:LinkButton>
                                        </li>


                                        <li><a href="#">Email</a></li>
                                        <li>
                                            <asp:LinkButton ID="lnkbtnkpi" runat="server" OnClick="lnkbtnkpi_Click">KPI</asp:LinkButton></li>

                                        <li>
                                            <asp:LinkButton ID="lnkbtnProposal" runat="server" OnClick="lnkbtnProposal_Click">Proposal <sup><span class="badgei" id="cpro" runat="server">0</span></sup></asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkbtnDayPassed" runat="server" OnClick="lnkbtnDayPassed_Click">Day Passed <sup><span class="badgei" id="cdaypassed" runat="server">0</span></sup></asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkCall" runat="server" OnClick="lnkCall_Click">Call <sup><span class="badgei" id="ccall" runat="server">0</span></sup></asp:LinkButton>

                                        </li>

                                        <li>
                                            <asp:LinkButton ID="lnkbtnLome" runat="server" OnClick="lnkbtnLome_Click">LOME <sup><span class="badgei" id="clome" runat="server">0</span></sup></asp:LinkButton>
                                       
                                        <li>
                                            <asp:LinkButton ID="lnkbtnLomi" runat="server" OnClick="lnkbtnLomi_Click">LOMI <sup><span class="badgei" id="clomi" runat="server">0</span></sup></asp:LinkButton>
                                           
                                        <li>
                                            <asp:LinkButton ID="lnkbtnServey" runat="server" OnClick="lnkbtnServey_Click">Survery <sup><span class="badgei" id="csurvey" runat="server">0</span></sup></asp:LinkButton>
                                           
                                        <li>
                                            <asp:LinkButton ID="lnkbtnComment" runat="server" OnClick="lnkbtnComment_Click">Comments <sup><span class="badgei" id="ccomments" runat="server">0</span></sup></asp:LinkButton>
                                            

                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkbtnFreezland" runat="server" OnClick="lnkbtnFreezland_Click">Freezing Lands <sup><span class="badgei" id="cfreezing" runat="server">0</span></sup></asp:LinkButton>
                                          


                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkbtnDead" runat="server" OnClick="lnkbtnDead_Click">Dead Land <sup><span class="badgei" id="cdeadl" runat="server">0</span></sup></asp:LinkButton>
                                        
                                        </li>

                                        <li>
                                            <asp:LinkButton ID="lbtnSigned" runat="server" OnClick="lbtnSigned_Click">Signed<sup><span class="badgei" id="csigned" runat="server">0</span></sup></asp:LinkButton>
                                           
                                        </li>

                                        <li><a href="#" id="areport">Report</a></li>
                                        <li>
                                            <asp:LinkButton ID="lnkbtnReturn" runat="server" OnClick="lnkbtnReturn_Click">Return List</asp:LinkButton>
                                        </li>

                                    </ul>


                                </div>--%>


                                <div class="col-md-2 marapaddingzero">
                                    <asp:LinkButton ID="lnkShowNotifcation" runat="server" Text="Notification" OnClick="lnkShowNotifcation_Click" CssClass="btn btn-primary"></asp:LinkButton>



                                    <div class="list-group list-group-bordered mb-3 notifsectino">
                                        <asp:LinkButton ID="lnkbtnDws" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnDws_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-primary">DWS </div>
                                            </div>
                                            <div class="list-group-item-body">Daily Work Schedule</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lbldws" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkbtnDwr" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnDwr_Click">
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
                                        <asp:LinkButton ID="lnkbtnkpi" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnkpi_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-info">KPI </div>
                                            </div>
                                            <div class="list-group-item-body">
                                                Key Performance Indicator
                                            </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblkpi" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkbtnOther" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnOther_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-primary">OTH </div>
                                            </div>
                                            <div class="list-group-item-body">Others</div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lbloth" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkCall" class="list-group-item list-group-item-action" runat="server" OnClick="lnkCall_Click">
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


                                        <asp:LinkButton ID="lnkbtnDayPassed" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnDayPassed_Click">
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

                                        <asp:LinkButton ID="lnkbtnLome" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnLome_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-pink">LME</div>
                                            </div>
                                            <div class="list-group-item-body">
                                                Land Meeting External
                                            </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblLome" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkbtnLomi" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnLomi_Click">
                                            <div class="list-group-item-figure">
                                                <div class="tile tile-circle bg-dark">LMI</div>
                                            </div>
                                            <div class="list-group-item-body">
                                                Land Meeting Internal
                                            </div>
                                            <div class="list-group-item-figure">
                                                <button class="btn btn-sm btn-light">
                                                    <span class="badge badge-pill badge-danger" id="lblLomi" runat="server">0</span>
                                                </button>
                                            </div>

                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkbtnComment" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnComment_Click">
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

                                        <asp:LinkButton ID="lnkbtnFreezland" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnFreezland_Click">
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

                                        <asp:LinkButton ID="lnkbtnDead" class="list-group-item list-group-item-action" runat="server" OnClick="lnkbtnDead_Click">
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
                                        <asp:HiddenField ID="hdnlblattribute" runat="server" />


                                    </div>



                                    <label class="control-label" style="font-size: 14px; font-weight: bold">Operations</label>
                                    <div class="form-group">
                                        <ul style="list-style: none; padding-left: 0px">
                                            <li>
                                                <asp:HyperLink ID="HyperLink3" Target="_blank" NavigateUrl="~/F_21_Mkt/ClientInitial?Type=MktCl" runat="server">Primary Lead</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="hllnkCodebook" Target="_blank" NavigateUrl="~/F_21_Mkt/MktGenCodeBookLand" runat="server">Code Book</asp:HyperLink>

                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkbtnReturn" runat="server" OnClick="lnkbtnReturn_Click">Return List</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_21_Mkt/RptSalesFunnel" runat="server">Sales Funnel Reports</asp:HyperLink>

                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink4" Target="_blank" NavigateUrl="~/F_21_Mkt/YearlyActivitiesTarget?type=LND" runat="server">Yearly Activities Target Set</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="HyperLink2" Target="_blank" NavigateUrl="~/F_21_Mkt/YearlyTargetVSAchive?type=LND" runat="server">Yearly Target Vs Achievement</asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink ID="hyplnkProspectTransfer" Target="_blank" NavigateUrl="~/F_21_MKT/LandProspectTransfer" runat="server">Land Transfer</asp:HyperLink>
                                            </li>

                                            <li>
                                                <%--<asp:LinkButton ID="lnkbtnNotes" runat="server" OnClick="lnkbtnNotes_Click">Notes</asp:LinkButton>--%>
                                            </li>
                                        </ul>
                                    </div>
                                </div>






                            </div>
                        </asp:View>

                        <asp:View ID="Discoussion" runat="server">
                        </asp:View>
                    </asp:MultiView>


                    <div class="row text-center">
                        <div class="col-md-12">
                            <div class="form-group">

                                <asp:LinkButton ID="lUpdatPerInfo" Visible="false" runat="server" CssClass="btn btn-success  btn-sm" OnClientClick="return confirm('Do You want to Update?');" OnClick="lUpdatPerInfo_Click">Update</asp:LinkButton>
                            </div>
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
                                <h4 id="lblheader" runat="server"><span class="glyphicon glyphicon-info-sign"></span>Land Activation</h4>
                                <p>
                                </p>
                            <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                                <span aria-hidden="true" class="white-text">×</span>
                            </button>


                        </div>

                        <!--Body-->
                        <div class="modal-body">

                            <div class="row">


                                <%--  <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lbldiscussion" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px" style="background:#DFF0D8"></asp:Label>


                                                </div>
                                            </div>--%>






                                <asp:Label ID="lblsircode" runat="server" Visible="false"></asp:Label>





                            </div>
                        </div>

                        <!--Footer-->
                        <div class="modal-footer">


                            <asp:LinkButton ID="btmodal" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="CloseModal();" OnClick="btmodal_Click">Active</asp:LinkButton>

                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true" class="white-text">&times;</span>
                            </button>

                        </div>
                    </div>
                    <!--/.Content-->
                </div>
            </div>


            <div id="detnotification" class="modal fade   animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-full-width  ">
                    <div class="modal-content modal-content-full-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>
                                Information </h4>

                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>


                        </div>
                        <div class="modal-body ">



                            <table id="tblinformation" class="table-striped table-hover table-bordered  grvContentarea">
                                <thead>
                                    <tr class="grvHeader">
                                        <th class='tSl'>Sl</th>
                                        <th class='tStatus'>LID</th>
                                        <th class='tdate'>Generated</th>
                                        <th class='tdatetime'>Last Followup</th>
                                        <th class='tDiscussion'>Owner Details</th>
                                        <th class='tParticipants'>Property Details</th>
                                        <th class='tAssociateadeal'>Associate</th>
                                        <th class='tAssociateadeal'>Dealing</th>
                                        <th class='tStatus'>Status</th>
                                        <th class='tStatus'>Priority</th>
                                        <th class='tStatus'>Followup</th>

                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>










                        </div>




                    </div>
                </div>
            </div>





            <div id="mdiscussion" class="modal fade   animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-full-width  ">
                    <div class="modal-content modal-content-full-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>
                                Discussion </h4>

                            <button type="button" id="btnModalClose" class="btn btn-xs pull-right close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times" aria-hidden="true"></i></button>


                        </div>
                        <div class="modal-body ">



                            <div class="row">

                                <div class="col-xs-9 col-sm-9 col-md-9">
                                    <strong><span id="lbllowner" runat="server"></span></strong>
                                    <br>
                                    <span id="lbllowphone" runat="server"></span>
                                    <br>
                                    <span id="lbllandaddress" runat="server"></span>


                                    <asp:HiddenField ID="lblproscod" runat="server" />
                                    <asp:HiddenField ID="lbleditempid" runat="server" />
                                </div>

                                <div class="col-xs-3 col-sm-3 col-md-3 ">

                                    <%--<asp:LinkButton ID="lbtntfollowupcs" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa fa-handshake"></i> Followup</asp:LinkButton>--%>

                                    <button type="button" class="btn  btn-success btn-xs" id="lbtntfollowup" data-toggle="collapse" data-target="#followup"><i class="fa fa-handshake"></i>Followup</button>

                                    <button type="button" class="btn  btn-success btn-xs" id="lbtnStatus" data-toggle="collapse" data-target="#Status"><i class="fa  fa-star-and-crescent"></i><span id="lbllaststatus" runat="server">Status</span></button>
                                    <button type="button" class="btn  btn-success btn-xs" id="lbtnMap"><i class="fa   fa-map"></i>Map</button>

                                    <%--<asp:LinkButton ID="lbtntfollowup" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa fa-handshake"></i> Followup</asp:LinkButton>--%>
                                    <%-- <asp:LinkButton ID="lbtnStatus" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnStatus_Click"> <i  class="fa  fa-star-and-crescent"></i> Status</asp:LinkButton>
                                 <asp>:LinkButton ID="lbtnMap" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtntfollowup_Click"><i  class="fa   fa-map"></i> Map</asp:LinkButton>--%>
                                </div>

                            </div>


                            <div class="row">
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
                                <div class="col-md-6  col-lg-6">
                                    <asp:Repeater ID="rpclientinfo" OnItemDataBound="rpclientinfo_ItemDataBound" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>


                                            <div class="col-md-12  col-lg-12">
                                                <div class="well">

                                                    <div class="col-sm-12 panel">

                                                        <div class=" col-sm-12">

                                                            <p>
                                                                <strong><%# DataBinder.Eval(Container, "DataItem.lwname")%> </strong><%# DataBinder.Eval(Container, "DataItem.kpigrpdesc").ToString()%> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt")%><br>

                                                                <strong>Participants:</strong> <%# DataBinder.Eval(Container, "DataItem.partcilist").ToString()%><br>

                                                                <strong>Summary:</strong> <%# DataBinder.Eval(Container, "DataItem.discus").ToString()%><br>

                                                                <strong>Next Action:</strong> <%# DataBinder.Eval(Container, "DataItem.nfollowup").ToString()%> on <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.napnt")).ToString("dd-MMM-yyyy hh:mm tt")%><br>
                                                                <strong>Comments:</strong> <%# DataBinder.Eval(Container, "DataItem.disgnote").ToString()%>
                                                                <strong>Subject:</strong> <%# DataBinder.Eval(Container, "DataItem.ndissub").ToString()%>
                                                                <br>
                                                            </p>







                                                        </div>


                                                        <asp:Label ID="lbldes_proscod" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.proscod").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblCdate" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cdate").ToString()%>'></asp:Label>
                                                        <%--<asp:Button Text="Re-Schdule" CssClass="btn btn-primary btn-xs" OnClientClick="javascript:CloseModaldisReschedule();" runat="server" OnClick="GetValue" />--%>


                                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnreschedule" onclick="funReschedule('<%# DataBinder.Eval(Container, "DataItem.cdate").ToString()%>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>')">Re-Schedule</button>


                                                        <asp:LinkButton ID="lbtnCancel" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnCancel_Click"> Cancel</asp:LinkButton>
                                                        <%--<asp:LinkButton ID="lbtnFollowup" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnFollowup_Click"> Followup</asp:LinkButton>--%>

                                                        <button type="button" class="btn btn-primary btn-xs" id="lbtnFollowup" data-toggle="collapse" data-target="#followup">Followup</button>
                                                        <asp:LinkButton ID="lbtnAddition" CssClass="btn btn-primary btn-xs" runat="server" OnClick="Reschdule_Click">Addition</asp:LinkButton>
                                                        <%--<asp:LinkButton ID="lbtnComments" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnComments_Click"    data-toggle="collapse" data-target="#dcomments">Comments</asp:LinkButton>--%>

                                                        <button type="button" class="btn btn-primary btn-xs" runat="server" id="lbtnComments" data-toggle="collapse" data-target='<%# "#dcomments" + DataBinder.Eval(Container, "DataItem.rownum").ToString()%>'>Comments</button>

                                                        <div class="col-md-12 collapse dcomments" id="dcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>">

                                                            <textarea name="lblcomments" id="lblcomments<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>" style="width: 300px"></textarea>
                                                            <br />



                                                            <%--<input type="text" class="form-control">--%>
                                                            <input type="text" name="txtcomdate" class="datepicker" id="txtcomdate<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>" value="<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("MM/dd/yyyy")%>" style="width: 300px"></input>






                                                            <%-- <asp:TextBox ID="txtcomdate" runat="server" CssClass="inputtextbox" ></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                         Format="dd-MMM-yyyy" TargetControlID="txtcomdate"  ></cc1:CalendarExtender>--%>
                                                            <button type="button" class="btn  btn-success btn-xs" id="lbtnpostComments" onclick="funPost('<%# DataBinder.Eval(Container, "DataItem.cdate").ToString()%>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>')">Post</button>



                                                        </div>


                                                    </div>

                                                </div>



                                            </div>

                                        </ItemTemplate>

                                    </asp:Repeater>
                                </div>
                                <div class="col-md-6  col-lg-6">
                                    <div id="followup" class="collapse">
                                        <asp:GridView ID="gvInfo" runat="server" AllowPaging="True" OnRowDataBound="gvInfo_RowDataBound"
                                            AutoGenerateColumns="False" PageSize="25" ShowFooter="true" Width="100%"
                                            CssClass="table-condensed table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNodis" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ControlStyle-CssClass="displayhide">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCodedis" ClientIDMode="Static" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod"))%>'
                                                            Width="49px"></asp:Label>
                                                        <asp:Label ID="lblgvTime" runat="server" BorderWidth="0" BackColor="Transparent" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gtime"))%>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcGrpdis" runat="server"
                                                            Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc")) + "</B>"%>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <%-- <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkTotal_Click">Total :</asp:LinkButton>

                                                </FooterTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc1dis" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgpdis" runat="server" Font-Bold="True" Font-Size="12px"
                                                            Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph"))%>'
                                                            Width="5px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvaldis" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>

                                                    <FooterTemplate>

                                                        <asp:LinkButton ID="lbtnUpdateDiscussiont" runat="server" OnClientClick="CloseModaldis();" OnClick="lbtnUpdateDiscussiont_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>

                                                    </FooterTemplate>
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtgvValdis" runat="server" BorderWidth="0" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1"))%>'></asp:TextBox>


                                                        <div class="col-md-12">

                                                            <asp:TextBox ID="txtgvdValdis" runat="server" BorderWidth="0" Style="width: 80px; float: left;" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1"))%>'></asp:TextBox>
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

                                                        </div>
                                                        <asp:Panel ID="pnlFollow" runat="server" Visible="false">
                                                            <%-- <asp:DropDownList ID="ddlFollow" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>



                                                            <asp:CheckBoxList ID="ChkBoxLstFollow" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server" CssClass="col-md-12 checkbox">
                                                            </asp:CheckBoxList>


                                                        </asp:Panel>





                                                        <asp:Panel ID="pnlStatus" runat="server" Visible="false">
                                                            <%-- <asp:DropDownList ID="ddlStatus" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control">
                                                        </asp:DropDownList>--%>

                                                            <asp:CheckBoxList ID="ChkBoxLstStatus" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server" CssClass="col-md-12 checkbox">
                                                            </asp:CheckBoxList>

                                                        </asp:Panel>



                                                        <asp:Panel ID="pnlParicdis" runat="server" Visible="false">
                                                            <asp:ListBox ID="ddlParticdis" runat="server" SelectionMode="Multiple" Style="width: 300px !important;"
                                                                data-placeholder="Choose Participant......" multiple="true" class="form-control chosen-select"></asp:ListBox>
                                                            <%--<select multiple id="ddlPartic" class="multiuser" runat="server" style="width: 300px">
                                                </select>--%>
                                                            <%--<asp:DropDownList ID="ddlPartic" Visible="false" runat="server" vidible="false" CssClass="chzn-select inputTxt form-control"></asp:DropDownList>--%>
                                                        </asp:Panel>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle />
                                                </asp:TemplateField>


                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <FooterStyle CssClass="grvFooter" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>


                                    </div>
                                </div>
                            </div>



                        </div>




                    </div>
                </div>
            </div>





            <div id="modalComments" class="modal fade   " role="dialog" data-keyboard="false" data-backdrop="static">
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
                                                Style="text-align: left" Font-Size="11px"
                                                Text='<%# Convert.ToString(Container.DataItemIndex + 1) + "."%>' Width="40px">

                                            </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="P-ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomsircode1" runat="server" Width="50px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Generated">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomgenerated" runat="server" Width="60px" Font-Size="11px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prospect Details">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomdesc" runat="server" Width="80px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ownname"))%>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Preferrence ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomprefdesc" runat="server" Width="200px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Associate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomassoc" runat="server" Width="80px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc"))%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Team Leader">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomTeam" runat="server" Width="120px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dealname"))%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Width="250px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gnote"))%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
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
                                            <asp:CheckBox ID="chkCommentView" runat="server" Width="20px" />


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



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

