<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CrmClientInfo02.aspx.cs" Inherits="RealERPWEB.F_21_MKT.CrmClientInfo02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/crm-new-dashboard.css" rel="stylesheet" />
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
                //var comcod =<%=this.GetComeCode()%>;

               <%-- switch (comcod) {
                    case 3352://p2p360                 
                    case 1205://p2p Construction
                    case 3351://p2p Wecon Properties          

                        $('#<%=this.lblheadprospect.ClientID%>').text(" Project Details");

                        break;

                    default:
                        $('#<%=this.lblheadprospect.ClientID%>').text(" Prospect's Preference	");
                        break;
                }--%>

                var gcod;

                //Schedule Reminder              

                var arrgschcodl = $('#<%=this.gvInfo.ClientID %>').find('[id$="lblgvItmCodedis"]');
                var arrgschval = $('#<%=this.gvInfo.ClientID %>').find('input:text[id$="txtgvValdis"]');
                var arrgsschcheckbox = $('#<%=this.gvInfo.ClientID %>').find('input:text[id$="ChkBoxLstFollow"]');
                var txtnfollowupdate, checkboxlastfollowup;
                for (var i = 0; i < arrgschcodl.length; i++) {


                    gcod = $(arrgschcodl[i]).text();
                    var number, numberlq, numbercom, numberprj;
                    switch (gcod) {


                        //Company
                        case '810100101007':
                            numbercom = i;
                            break;

                        //Project
                        case '810100101003':
                            numberprj = i;
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






<%--                var gvSummary = $('#<%=this.gvSummary.ClientID %>');
                gvSummary.Scrollable();--%>



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
                // alert(e.message)

            }

        }





        function VisibilitycomNotification() {

            try {

                  //  var comcod =<%=this.GetComeCode()%>;

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



        function DetNotification(rtype) {

            try {

                var empid =<%=this.GetEmpID()%>;
               // var comcod =<%=this.GetComeCode()%>;



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
                 //   var comcod =<%=this.GetComeCode()%>;
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

                 //   var comcod =<%=this.GetComeCode()%>;
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

        // Company Project
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

                        console.log(data);


                        //    ContentPlaceHolder1_gvInfo_checkboxReson_6_chzn

                        var ddlProject = '#ContentPlaceHolder1_gvInfo_ddlProject_' + numberrl;

                        //var ddlProject = '#ddlProject';


                        //console.log(ddlProject);
                        $(ddlProject).html('');

                        // $(lstProject).empty();
                        $.each(data, function (key, data) {



                            // $('#Select1').append('<option value="5">item 5</option>')
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
        // Company Project Unit

        function funCompanyProjectUnit(company, pactcode) {
            try {
                $.ajax({
                    type: "POST",
                    url: "CrmClientInfo.aspx/GetProjectUnit",
                    data: '{comcod:"' + company + '", pactcode:"' + pactcode + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var data = JSON.parse(response.d);

                        var arrgschcodl = $('#<%=this.gvInfo.ClientID %>').find('[id$="lblgvItmCodedis"]');
                        var numberrl;

                        for (var i = 0; i < arrgschcodl.length; i++) {

                            gcod = $(arrgschcodl[i]).text();
                            switch (gcod) {

                                case '810100101004':
                                    numberrl = i;
                                    break;

                            }

                        }

                        console.log(data);


                        //    ContentPlaceHolder1_gvInfo_checkboxReson_6_chzn

                        var ddlUnit = '#ContentPlaceHolder1_gvInfo_ddlUnit_' + numberrl;

                        //var ddlProject = '#ddlProject';



                        $(ddlUnit).html('');

                        // $(lstProject).empty();
                        $.each(data, function (key, data) {



                            // $('#Select1').append('<option value="5">item 5</option>')
                            $(ddlUnit).append("<option value='" + data.usircode + "'>" + data.udesc + "</option>");
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
    <script>
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);           

            $(document).on("change", "#DdlDateType", function () {
                // $("#DdlDateType").change(function () {
                var status = this.value;
                // alert(status);
                if (status == "7") {
                    $("#exampleModalSm").modal("toggle");
                }

            });
        });
        function pageLoaded() {
            try {
                let floatingContainer = $(".floating");
                let floatingBtn = $(".floating-btn");
                let floatingHeader = $(".floating-header");

                floatingBtn.click(() => {
                    floatingContainer.addClass("active");
                });
                floatingHeader.click(() => {
                    floatingContainer.removeClass("active");
                });
                //Dashboard Link
                $("#btnDashboard").click(function () {
                    window.location.href = "../F_99_Allinterface/CRMDashboard03?Type=Report";
                });

            }
            catch (e) {

            }
        };
    </script>
    
    <style>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="wrapper">
                <div class="page mt-4">
                    <div class="page">
                        <div class="row mb-4">
                            <div class="col-md-8">
                                <div class="row align-items-end">
                                    <div class="col-2">
                                        <div class="form-group mb-0">
                                            <label for="form-date-range" class="form-label">
                                                Date</label>

                                             <asp:DropDownList ID="DdlDateType" runat="server" ClientIDMode="Static" class="form-select">
                                                <asp:ListItem Value="0">Today</asp:ListItem>
                                                <asp:ListItem Value="1">Yesterday</asp:ListItem>
                                                <asp:ListItem Value="2">Last 7 Days</asp:ListItem>
                                                <asp:ListItem Value="3">This Month</asp:ListItem>
                                                <asp:ListItem Value="4">Last Month</asp:ListItem>
                                                <asp:ListItem Value="5">This Year</asp:ListItem>
                                                <asp:ListItem Value="6">last Year</asp:ListItem>
                                                <asp:ListItem Value="7">Custom</asp:ListItem>

                                            </asp:DropDownList>
                                            
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-3">
                                        <div class="form-group mb-0">
                                             <asp:DropDownList ID="ddlEmpid" data-placeholder="Choose Employee.." runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpid_SelectedIndexChanged">
                                    </asp:DropDownList>
                                            
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-3">
                                        <div class="form-group mb-0">
                                            <asp:DropDownList ID="DdlProjec" runat="server" CssClass="form-select"></asp:DropDownList>
                                         
                                        </div>
                                    </div>
                                    <!-- END -->
                                    <div class="col-4">
                                        <div class="d-flex">
                                            <div class="input-group form-search mb-0 mr-3">
                                                <div class="input-group-prepend">
                                                    <div class="input-group-text">
                                                        <i class="fas fa-search"></i>
                                                    </div>
                                                </div>

                                    <asp:TextBox ID="txtVal" runat="server" CssClass="form-control" TextMode="Search"  placeholder="Search Here"></asp:TextBox>

                                             
                                            </div>
                                            <asp:LinkButton ID="lnkOk" runat="server" Text="OK" OnClick="lnkOk_Click" CssClass="mmbd-btn mmbd-btn-primary"></asp:LinkButton>

                                         <%--   <button class="mmbd-btn mmbd-btn-primary">
                                                Apply
                                            </button>--%>
                                        </div>
                                    </div>
                                    <!-- END -->
                                </div>
                            </div>
                            <div class="col-md-4 align-self-end">
                                <div class="d-flex justify-content-end">
                                    <button class="mmbd-btn mmbd-btn-primary mr-2" id="btnaddland" runat="server">
                                        <strong><i class="fas fa-user-plus"></i>&nbsp;Add Lead</strong>
                                    </button>
                                    <button class="mmbd-btn mmbd-btn-primary" id="btnDashboard">
                                        <img
                                            src="../assets/new-ui/images/equalizer.svg"
                                            alt="Dashboard"
                                            class="img-responsive" />
                                        <strong>Dashboard</strong>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- END HEAD -->
                        <div class="mb-4">
                                            <asp:HiddenField ID="lblnewprospect" runat="server" />
                                        <asp:HiddenField ID="hdnlblattribute" runat="server" />

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card-flash h-100 px-4 pt-3 pb-4">
                                        <div class="card-body pb-0">
                                            <asp:MultiView ID="MultiView1" runat="server">
                                                <asp:View ID="ViewPersonalInfo" runat="server">
                                                    <div class="row">
                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                    <div class="row mb-2 btnsavefix">
                                                    </div>
                                                </asp:View>
                                                <asp:View ID="ViewSummary" runat="server">
                                                            <div class="table-responsive">
                                                    <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvSummary_RowDataBound"
                                            ShowFooter="false" CssClass="table table-bordered mmbd-table table-striped table-header-flash" AllowPaging="True" OnPageIndexChanging="gvSummary_PageIndexChanging">
                                            <RowStyle />
                                            <Columns>

                                                <%--0--%>
                                                <asp:TemplateField HeaderText="Sl" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "rowid")).ToString("#,##0;(#,##0); ")  %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <%--1--%>

                                             
                                                <%--4--%>

                                                <asp:TemplateField HeaderText="Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lsircode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>

                                                        <asp:Label ID="ldesig" runat="server" Width="40px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--5--%>
                                                <asp:TemplateField HeaderText="P-ID">
                                                    <HeaderTemplate>
                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <div
                                                                                class="dropdown mmbd-dropdown-icon mr-2">
                                                                                <button
                                                                                    class="btn btn-secondary dropdown-toggle"
                                                                                    type="button"
                                                                                    data-toggle="dropdown"
                                                                                    aria-expanded="false">
                                                                                    <i class="fa fa-filter"></i>
                                                                                </button>
                                                                                <div class="dropdown-menu">
                                                                                    <span class="dropdown-item">Green
                                                                                    </span>
                                                                                    <span class="dropdown-item">Red
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <span class="header-label">P-ID </span>
                                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                           <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                 <asp:Label ID="lsircode1" runat="server" Width="40px" CssClass="text-success"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                                                                           
                                                                        </div>
                                                      
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--6--%>
                                                <asp:TemplateField HeaderText="Generated Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgenerated" runat="server" Font-Size="11px" Width="70px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reassign Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvrassigndate" CssClass="badge badge-light" runat="server" Width="70px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rassigndat"))%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <%--7--%>
                                                   <asp:TemplateField HeaderText="Associate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lassoc" runat="server" Width="90px"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpactdesc" runat="server" Width="250px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Source">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvLSrc" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) + (Convert.ToString(DataBinder.Eval(Container.DataItem, "irpersonname"))=="" ?"": "(" + Convert.ToString(DataBinder.Eval(Container.DataItem, "irpersonname")) + ")")
                                                                
                                                            %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Followup">
                                                    <ItemTemplate>

                                                        <asp:Panel ID="pnlfollowup" runat="server" Width="90px" ClientIDMode="Static">

                                                            <asp:Label ID="lbllfollowuplink" Width="70px" Font-Size="11px" runat="server" ToolTip="Followup" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy")=="01-Jan-1900"?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appbydat")).ToString("dd-MMM-yyyy") %>'>
                                                            </asp:Label>

                                                            <asp:LinkButton ID="lbtnView" ClientIDMode="Static" Style="float: right !important;" Width="15px" ToolTip="View" runat="server" OnClick="lbtnView_Click" CssClass="d-none"><span class="fa  fa-eye"></span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkEditfollowup" ClientIDMode="Static" Style="float: right !important;" Width="15px" ToolTip="Discoussion" runat="server" OnClick="lnkEditfollowup_Click"><span class="fa fa-edit"></span></asp:LinkButton>



                                                        </asp:Panel>



                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Lead Status">
                                                    <HeaderTemplate>
                                                        <%--<div class="d-flex align-items-center justify-content-center">
                                      <div class="dropdown mmbd-dropdown-icon mr-2">
                                        <button class="btn btn-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false">
                                          <i class="fa fa-filter"></i>
                                        </button>
                                        <div class="dropdown-menu">
                                          <span class="dropdown-item">
                                            Query
                                          </span>
                                          <span class="dropdown-item">
                                            Lead
                                          </span>
                                             <span class="dropdown-item">

                                          </span>
                                        </div>
                                      </div>
                                      <span class="header-label">
                                        Lead Status
                                      </span>
                                    </div>--%>
                                                                                      <asp:DropDownList ID="ddlStatus" runat="server" CssClass="custom-select"></asp:DropDownList>
                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lprefdesc" CssClass="badge badge-light" runat="server" Width="70px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadsta")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prospect Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ldesc" runat="server" Width="130px"
                                                            Text='<%# 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim()
                                                                         
                                                                    %>'>

                                                             


                                                        </asp:Label>



                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                            

                                                <asp:TemplateField HeaderText="Team Leader" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbusername" runat="server" Width="90px"
                                                            Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <%--12--%>

                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbllstatus" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--13--%>

                                                <asp:TemplateField HeaderText="Type" Visible="false">
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
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--15--%>



                                                <asp:TemplateField HeaderText="Mobile" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvphone" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--16--%>
                                                <asp:TemplateField HeaderText="Email" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvemail" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--17--%>


                                                <asp:TemplateField HeaderText="Occupation" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvoccupation" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--18--%>


                                                <asp:TemplateField HeaderText="Residence" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcaddress" runat="server" Width="60px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--19--%>



                                          

                                                <asp:TemplateField HeaderText="Last discussion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Width="300px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ldiscuss")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--22--%>


                                                <asp:TemplateField HeaderText="Notes" Visible="false">
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
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <%--25--%>

                                                <asp:TemplateField HeaderText="Retreive" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnRetreive" runat="server" Font-Bold="True" Height="12px" ToolTip="Retrieve Prospect" Style="text-align: right" OnClientClick="javascript:return  FunRetProsConfirm()" OnClick="lnkbtnRetreive_Click"><span><i class="fa fa-undo" Style="text-align: center"></i></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--27--%>
                                                <asp:TemplateField HeaderText="Project Visit<br>Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvprojvisit" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projvisit")) %>'></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--28--%>
                                                <asp:TemplateField HeaderText="Entry By" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgventryby" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entryby")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
   <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkgvHeader" runat="server" Font-Bold="True" ToolTip="Edit Header" OnClick="lnkgvHeader_Click"><i class="fa fa-th-large" aria-hidden="true"></i></asp:LinkButton>

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete"
                                                            runat="server" Font-Bold="True" ToolTip="Delete" Style="text-align: right;" OnClientClick="javascript:return  FunConfirm()" OnClick="lnkDelete_Click">

                                                        <i class=" fa fa-trash"></i></asp:LinkButton>




                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--2--%>


                                                <asp:TemplateField HeaderText="Action">

                                                    <ItemTemplate>
                                                       
                                                        <div class="d-flex">
                                                             <asp:LinkButton ID="ViewData" runat="server" CssClass="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1"  ToolTip="View"  OnClick="ViewData_Click"><span><i class="fas fa-eye" aria-hidden="true"></i></span></asp:LinkButton>
                                                                 
                                                             <asp:LinkButton ID="lnkEdit" runat="server" CssClass="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1"  ToolTip="Edit Client Info" Text="Edit" OnClick="lnkEdit_Click"> <span class="fas   fa-edit"></span></asp:LinkButton>
                                                                           
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>


                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--3--%>

                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                                            CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa  fa-file-excel "></span></asp:HyperLink>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                       
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />



                                                </asp:TemplateField>
                                            </Columns>
                                            <%--<FooterStyle CssClass="grvFooter" />--%>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <%--<PagerStyle CssClass="gvPagination" />--%>
                                            <%--<HeaderStyle CssClass="grvHeader" />--%>
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
                                                        <asp:LinkButton ID="lblgvkpicall" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpicall_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "call")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
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
                                                        <asp:LinkButton ID="lblgvkpiextmeeting" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiextmeeting_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "extmeeting")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
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
                                                        <asp:LinkButton ID="lblgvkpiintmeeting" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiintmeeting_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "intmeeting")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
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
                                                        <asp:LinkButton ID="lblgvkpivisit" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpivisit_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "visit")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFvisitsum" runat="server" Style="text-align: center"
                                                            Width="60px"></asp:Label>
                                                    </FooterTemplate>

                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>






                                                <asp:TemplateField HeaderText="Proposal">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvkpiproposal" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiproposal_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proposal")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
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
                                                        <asp:LinkButton ID="lblgvkpileads" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpileads_Click1"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leads")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
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
                                                        <asp:LinkButton ID="lblgvkpiclosing" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiclosing_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "close")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
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
                                                        <asp:LinkButton ID="lblgvkpiothers" runat="server" Width="60px" Font-Size="10px" Style="text-align: center;" OnClick="lblgvkpiothers_Click"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "others")).ToString("#,##0;(#,##0); ")%>'></asp:LinkButton>
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
                                            
                                                        <%--<table
                                                            class="table table-bordered mmbd-table table-striped table-header-flash">
                                                            <thead>
                                                                <tr>
                                                                    <th scope="col" class="text-left">
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <div
                                                                                class="dropdown mmbd-dropdown-icon mr-2">
                                                                                <button
                                                                                    class="btn btn-secondary dropdown-toggle"
                                                                                    type="button"
                                                                                    data-toggle="dropdown"
                                                                                    aria-expanded="false">
                                                                                    <i class="fa fa-filter"></i>
                                                                                </button>
                                                                                <div class="dropdown-menu">
                                                                                    <span class="dropdown-item">Green
                                                                                    </span>
                                                                                    <span class="dropdown-item">Red
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <span class="header-label">P-ID </span>
                                                                        </div>
                                                                    </th>
                                                                    <th scope="col" class="text-center">Generated Date
                                                                    </th>
                                                                    <th scope="col" class="text-center">Associate Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Project Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Source Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Followup Date
                                                                    </th>
                                                                    <th scope="col" class="text-center">
                                                                        <div
                                                                            class="d-flex align-items-center justify-content-center">
                                                                            <div
                                                                                class="dropdown mmbd-dropdown-icon mr-2">
                                                                                <button
                                                                                    class="btn btn-secondary dropdown-toggle"
                                                                                    type="button"
                                                                                    data-toggle="dropdown"
                                                                                    aria-expanded="false">
                                                                                    <i class="fa fa-filter"></i>
                                                                                </button>
                                                                                <div class="dropdown-menu">
                                                                                    <span class="dropdown-item">Query
                                                                                    </span>
                                                                                    <span class="dropdown-item">Lead
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                            <span class="header-label">Lead Status
                                                                            </span>
                                                                        </div>
                                                                    </th>
                                                                    <th scope="col" class="text-center">Prospect Name
                                                                    </th>
                                                                    <th scope="col" class="text-center">Last Discussion
                                                                    </th>
                                                                    <th scope="col" class="text-center w-70px">ACTION
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                    I have talked. I have talked. I have talked.
                                                                    </td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="d-flex align-items-center">
                                                                            <div class="w-30px">
                                                                                <div
                                                                                    class="form-check form-check-sm form-check-custom form-check-solid">
                                                                                    <input
                                                                                        class="form-check-input"
                                                                                        type="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                            <span class="text-success">#3066 </span>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">
                                                                        <div class="d-flex flex-column">
                                                                            Jan 6, 2022
                                      <div class="badge badge-light">
                                          RE: Jan 20,2022
                                      </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">Edison Amour</td>
                                                                    <td class="text-center">Facebook</td>
                                                                    <td class="text-center">Jan 6, 2022</td>
                                                                    <td class="text-center">
                                                                        <div class="badge badge-light">
                                                                            <i class="fas fa-check"></i>Query
                                                                        </div>
                                                                    </td>
                                                                    <td class="text-center">Md. Ariful Islam Shanto
                                                                    </td>
                                                                    <td class="text-center">I have talked...</td>
                                                                    <td class="text-right">
                                                                        <div class="d-flex">
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fas fa-eye"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light mr-1">
                                                                                <i class="fa fa-edit"></i>
                                                                            </button>
                                                                            <button
                                                                                class="mmbd-btn mmbd-btn-icon mmbd-btn-primary-light">
                                                                                <i class="fa fa-handshake"></i>
                                                                            </button>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>--%>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <div class="d-flex align-items-center">
                                                                <div class="length">
                                                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm mb-0"
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
                                                                <div class="info ml-2">
                                                                    Showing 1 to 19 of 19 records
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-8">
                                                            <%--<nav class="table-pagination">
                                                                <ul class="pagination">
                                                                    <li class="page-item mr-3">
                                                                        <a class="page-link previews" href="#"><i class="fas fa-arrow-left"></i>
                                                                            Previous</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link active" href="#">1</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">2</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">3</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link">...</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">8</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">9</a>
                                                                    </li>
                                                                    <li class="page-item">
                                                                        <a class="page-link" href="#">10</a>
                                                                    </li>
                                                                    <li class="page-item ml-3">
                                                                        <a class="page-link next" href="#">Next<i class="fas fa-arrow-right"></i></a>
                                                                    </li>
                                                                </ul>
                                                            </nav>--%>
                                                        </div>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END -->
                        <div class="floating">
                            <div class="floating-btn">
                                <i class="fas fa-angle-up"></i>
                                <span>To</span>
                                <span>Do</span>
                            </div>
                            <div class="floating-panel">
                                <div class="floating-header">
                                    <div class="floating-title">TO DO</div>
                                    <div class="floating-action">
                                        <i class="fas fa-angle-down down"></i>
                                        <i class="fas fa-angle-up up"></i>
                                    </div>
                                </div>
                                <div class="floating-body">
                                    <ul>
                                        <li class="d-flex align-items-center justify-content-between">
                                           <div class="d-flex align-items-center" >
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-primary">SW</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                     <asp:LinkButton ID="lnkbtnDws" CssClass="fs-3" OnClick="lnkbtnDws_Click" runat="server">
                                                   Schedules Work
                                                         </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbldws" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-purple">TD</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                  <asp:LinkButton ID="lnkbtnTODayTask" runat="server" OnClick="lnkbtnTODayTask_Click" class="fs-3">To Day Task</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbltdt" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-info">DWR</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                     <asp:LinkButton ID="lnkBtnDwr" class="fs-3" runat="server" OnClick="lnkBtnDwr_Click">Daily Work Report</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lbldwr" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-dark">KPI</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                      <asp:LinkButton ID="lnkbtnKpi" runat="server" OnClick="lnkbtnKpi_Click" class="fs-3">Key Performance Indicator</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-blue">Call</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                       <asp:LinkButton ID="lnkBtnCall" class="fs-3" runat="server" OnClick="lnkBtnCall_Click">Call</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblCall" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-success">Visit</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                      <asp:LinkButton ID="lnkBtnVisit" class="fs-3" runat="server" OnClick="lnkBtnVisit_Click">Visit</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblvisit" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-red">DP</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                     <asp:LinkButton ID="lnkBtnDaypassed" class="fs-3" runat="server" OnClick="lnkBtnDaypassed_Click">Day Passed</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDayPass" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-warning">PME</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <asp:LinkButton ID="lbtnpme" class="fs-3" runat="server" OnClick="lbtnpme_Click">Project Meeting External</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblpme" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-info">PMI</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <asp:LinkButton ID="lbtnpmi" class="fs-3" runat="server" OnClick="lbtnpmi_Click">Project Meeting Internal</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblpmi" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-indigo">COM</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                     <asp:LinkButton ID="lnkBtnComments" class="fs-3" runat="server" OnClick="lnkBtnComments_Click">Comments</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblComments" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-orange">FRE</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                   <asp:LinkButton ID="lnkBtnFreezing" class="fs-3" runat="server" OnClick="lnkBtnFreezing_Click">Freezing</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblFreez" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-cyan">DP</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                   <asp:LinkButton ID="lnkBtnDeadProspect" class="fs-3" runat="server" OnClick="lnkBtnDeadProspect_Click">Dead Pros</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDeadProspect" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-success">Si</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                     <asp:LinkButton ID="lbtnSigned" class="fs-3"  runat="server" OnClick="lbtnSigned_Click">Signed</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblcsigned" runat="server">0</div>
                                        </li>
                                        <li class="d-flex align-items-center justify-content-between">
                                            <div class="d-flex align-items-center">
                                                <div class="symbol symbol-circle symbol-30px overflow-hidden mr-3">
                                                    <a href="#">
                                                        <div class="symbol-label bg-danger">DB</div>
                                                    </a>
                                                </div>
                                                <div class="d-flex flex-column">
                                                    <asp:LinkButton ID="lnkBtnDatablank" class="fs-3" runat="server" OnClick="lnkBtnDatablank_Click">Data Bank</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="badge badge-danger" id="lblDatablank" runat="server">0</div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal -->

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
                <div id="exampleModalSm" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-sm" role="document">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header">
                            <h5 class="modal-title">Chose Date Range </h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server">From</asp:Label>
                                        <asp:TextBox ID="txtfrmdate" autocomplete="off" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server">To</asp:Label>
                                        <asp:TextBox ID="txttodate" autocomplete="off" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-warning" data-dismiss="modal">Set & Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
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

                                    <button type="button" class="btn  btn-success btn-xs" id="lbtnprestatus" runat="server"><i class="fa  fa-star-and-crescent"></i><span id="lblprelaststatus" runat="server">Previous</span></button>
                                    <asp:HiddenField ID="hiddenLedStatus" runat="server" />
                                    <asp:HiddenField ID="hdlpreleadst" runat="server" />
                                    <asp:HiddenField ID="hdncompany" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnwinstatus" ClientIDMode="Static" runat="server" />

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

                                <div class="col-md-8 col-lg-8">

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


                                                            <asp:CheckBoxList ID="ChkBoxLstStatus" ClientIDMode="Static" RepeatLayout="Flow" RepeatDirection="Horizontal"
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

                                                            <%--   <asp:ListBox ID="lstProject"  SelectionMode="Multiple" runat="server"  class="form-control chosen-select" Style="width: 300px !important;"
                                                                data-placeholder="Choose Project" multiple="true"></asp:ListBox>--%>
                                                        </asp:Panel>
                                                        <asp:Panel ID="PnlUnit" runat="server">
                                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="inputTxt form-control" Style="width: 300px !important;"
                                                                TabIndex="12">
                                                            </asp:DropDownList>
                                                        </asp:Panel>


                                                        <asp:Panel ID="pnlVisit" runat="server" Visible="false">
                                                            <asp:DropDownList ID="ddlVisit" Visible="false" runat="server" CssClass="chzn-select inputTxt form-control" Style="width: 300px !important;">
                                                            </asp:DropDownList>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlFollow" runat="server" Visible="false">




                                                            <asp:CheckBoxList ID="ChkBoxLstFollow" ClientIDMode="Static" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server" CssClass="form-control checkbox">
                                                            </asp:CheckBoxList>


                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlnextFollow" runat="server" Visible="false">




                                                            <asp:CheckBoxList ID="ChkBoxLstnextFollow" ClientIDMode="Static" RepeatLayout="Flow" RepeatDirection="Horizontal" Width="650px"
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

                                <div class=" col-md-4 col-lg-4">
                                    <div id="divsold" style="display: none;">
                                        <strong>Sold Information</strong>
                                        <asp:Repeater ID="rpsold" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>


                                                <div class="col-md-12  col-lg-12">
                                                    <div class="well">

                                                        <div class="col-md-12 panel">

                                                            <div class=" col-md-12">

                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <asp:Label ID="lblProject" runat="server" Text="Project"></asp:Label>
                                                                            <asp:DropDownList ID="ddlsoldProject" runat="server" CssClass="form-control form-control-sm">
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-12">

                                                                        <div class="form-group">
                                                                            <asp:Label ID="lblunit" runat="server" Text="Unit"></asp:Label>
                                                                            <asp:DropDownList ID="ddlsoldunit" runat="server" CssClass="form-control  form-control-sm  ">
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">

                                                                    <div class="col-md-6">
                                                                        <div class="form-group">

                                                                            <label id="lblflatcost">Flat Cost</label>
                                                                            <asp:TextBox ID="txtflatcost" runat="server" TabIndex="2" CssClass="form-control form-control-sm textalignright"></asp:TextBox>

                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group">

                                                                            <label id="lblutility">Utility</label>
                                                                            <asp:TextBox ID="txtUtility" runat="server" TabIndex="2" CssClass="form-control form-control-sm textalignright"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">


                                                                    <div class="col-md-6">
                                                                        <div class="form-group">

                                                                            <label id="lblparking">Parking</label>
                                                                            <asp:TextBox ID="txtpamt" runat="server" TabIndex="2" CssClass="form-control form-control-sm textalignright"></asp:TextBox>

                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group">

                                                                            <label id="lblbookingmoney">Booking Money</label>
                                                                            <asp:TextBox ID="txtbookingmoney" runat="server" TabIndex="2" CssClass="form-control form-control-sm textalignright"></asp:TextBox>

                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">




                                                                    <div class="col-md-12">
                                                                        <div class="form-group">

                                                                            <label id="lblagreement">Agreement Status</label>
                                                                            <asp:DropDownList ID="ddlaggst" runat="server" CssClass="form-control form-control-sm">
                                                                            </asp:DropDownList>


                                                                        </div>
                                                                    </div>



                                                                </div>

                                                                <div class="row">


                                                                    <div class="col-md-6">
                                                                        <div class="form-group">

                                                                            <label id="lblsolddate">Solddate</label>
                                                                            <asp:TextBox ID="txtrpsolddate" runat="server" TabIndex="2" CssClass="form-control form-control-sm"></asp:TextBox>

                                                                            <cc1:CalendarExtender ID="txtrpsolddate_CalendarExtender" runat="server" Enabled="True"
                                                                                Format="dd-MMM-yyyy" TargetControlID="txtrpsolddate"></cc1:CalendarExtender>

                                                                        </div>
                                                                    </div>



                                                                    <div class="col-md-6">
                                                                        <div class="form-group">

                                                                            <label id="lblgrandtotal">Grand Total</label>
                                                                            <asp:TextBox ID="txtgrandTotal" runat="server" TabIndex="2" CssClass="form-control form-control-sm textalignright" ReadOnly="true"></asp:TextBox>



                                                                        </div>
                                                                    </div>
                                                                </div>




                                                            </div>
                                                        </div>



                                                    </div>
                                                </div>

                                            </ItemTemplate>

                                        </asp:Repeater>

                                        <asp:LinkButton ID="lbtnAddMore" OnClientClick="funDataToggle();" CssClass="btn btn-info btn-sm fa-pull-right" runat="server" OnClick="lbtnAddMore_Click">Add More</asp:LinkButton>

                                        <%--<asp:LinkButton ID="lbtnAddMore"  OnClientClick="funDataToggle();" CssClass="btn btn-info btn-sm fa-pull-right" runat="server"> Add More</asp:LinkButton>--%>
                                    </div>


                                    <div id="divhold" style="display: none">
                                        <div class="card card-fluid">
                                            <div class="card-body">

                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">
                                                            <label>Reason of Hold</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-8">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txtreason" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>




                                    </div>

                                    <div id="divlost" style="display: none">
                                        <div class="card card-fluid">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">
                                                            <label>Different Location</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txtdifflocation" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">
                                                            <label>Low Budget</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txtlowbudged" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <label id="chkbod" runat="server" class="switch">
                                                            <asp:CheckBox ID="chkintended" runat="server" />
                                                            <span class="btn btn-xs slider round"></span>
                                                        </label>
                                                        <label>Not intended to by Within 6 months</label>



                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">
                                                            <label>Others</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txtothers" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divclose" style="display: none">
                                        <div class="card card-fluid">
                                            <div class="card-body">
                                              
                                               
                                                
                                                 <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">
                                                            <label>Brought From Other</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txtbroughtfothers" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">
                                                            <label>Price doesn’t match</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="tctpricenotmatch" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">
                                                            <label>Others Reason</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group lblmargin">

                                                            <asp:TextBox ID="txtcloseoreason" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                

                                            </div>
                                        </div>


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
                                                        <asp:LinkButton ID="lbtnFollowup" CssClass="btn btn-primary btn-xs d-none" runat="server" OnClick="lbtnFollowup_Click"> Followup</asp:LinkButton>
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
                    <div class="modal-content modal-content-sm-width">
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
                                            <td style="color: #F3E7F7;">Followup To Days</td>

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



            <div id="msold" class="modal fade   animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-dialog-full-width  ">
                    <div class="modal-content modal-content-full-width">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>
                                Discussion </h4>

                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>


                        </div>
                        <div class="modal-body ">







                            <div id="div" class="col-md-12">
                                <div class="card-body">
                                    <div class="row">
                                    </div>
                                </div>
                            </div>


                            <div class="row">


                                <div class="col-md-12 col-lg-12">
                                    <asp:Repeater ID="Repeater1" runat="server">
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



                                                        <button type="button" class="btn  btn-success btn-xs" id="lbtnreschedule" onclick="funReschedule('<%# DataBinder.Eval(Container, "DataItem.cdate").ToString()%>', '<%# DataBinder.Eval(Container, "DataItem.rownum").ToString()%>')">Re-Schdule</button>

                                                        <button type="button" class="btn btn-primary btn-xs" id="lbtnCancel" onclick="funCancel('<%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>')">Delete</button>
                                                        <asp:LinkButton ID="lbtnFollowup" CssClass="btn btn-primary btn-xs d-none" runat="server" OnClick="lbtnFollowup_Click"> Followup</asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnAddition" CssClass="btn btn-primary btn-xs" runat="server" OnClick="lbtnAddition_Click">Addition</asp:LinkButton>
                                                        <button type="button" class="btn btn-primary btn-xs" runat="server" id="lbtnComments" data-toggle="collapse" data-target='<%# "#dcomments"+DataBinder.Eval(Container, "DataItem.rownum").ToString() %>'>Comments</button>











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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
