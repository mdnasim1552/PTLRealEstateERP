<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CalenderTest.aspx.cs" Inherits="RealERPWEB.CalenderTest" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel='stylesheet' href='assets\css\style.css' type='text/css' media='all' />
    <link rel='stylesheet' href='assets\css\colors.css' type='text/css' media='all' />
    <link rel='stylesheet' href='assets\css\comments.css' type='text/css' media='all' />
    <link rel='stylesheet' id='responsiveslides-css' href='assets\css\responsiveslides.css' type='text/css' media='all' />
    <link rel='stylesheet' id='reponsive-css' href='assets\css\reponsive.css' type='text/css' media='all' />
    <link rel='stylesheet' id='animate-custom-css' href='assets\css\animate-custom.css' type='text/css' media='all' />

    <link href="https://fonts.googleapis.com/css?family=Roboto:100,100i,300,300i,400,400i,500,500i,700,700i,900,900i" rel="stylesheet" />

    <%--<script src="Scripts/clock-1.1.0.min.js"></script>--%>
    <script src="Scripts/clock-1.1.0.min.js"></script>
    <script src="assets/js/calendarJs.js"></script>
    <link href="assets/css/calendarCss.css" rel="stylesheet" />

    <script>

        $(document).ready(function () {
           
            MyClock();


            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

           

            $('#external-events div.external-event').each(function () {

              
                var eventObject = {
                    title: $.trim($(this).text()) // use the element's text as the event title
                };

                // store the Event Object in the DOM element so we can get to it later
                $(this).data('eventObject', eventObject);

                // make the event draggable using jQuery UI
                $(this).draggable({
                    zIndex: 999,
                    revert: true,     
                    revertDuration: 0 
                });

            });


            var calendar = $('#calendar').fullCalendar({
                header: {
                    left: 'title'
                    //center: 'agendaDay,agendaWeek,month',
                    //right: 'prev,next today'
                },
                editable: true,
                firstDay: 1, //  1(Monday) this can be changed to 0(Sunday) for the USA system
                selectable: true,
                defaultView: 'month',

                axisFormat: 'h:mm',
                columnFormat: {
                    month: 'ddd',    // Mon
                    week: 'ddd d', // Mon 7
                    day: 'dddd M/d',  // Monday 9/7
                    agendaDay: 'dddd d'
                },
                titleFormat: {
                    month: 'MMM yyyy', // September 2009
                    week: "MMMM yyyy", // September 2009
                    day: 'MMMM yyyy'                  // Tuesday, Sep 8, 2009
                },
                allDaySlot: false,
                selectHelper: true,
                select: function (start, end, allDay) {
                    var title = prompt('Event Title:');
                    if (title) {
                        calendar.fullCalendar('renderEvent',
                            {
                                title: title,
                                start: start,
                                end: end,
                                allDay: allDay
                            },
                            true // make the event "stick"
                        );
                    }
                    calendar.fullCalendar('unselect');
                },
                droppable: true, // this allows things to be dropped onto the calendar !!!
                drop: function (date, allDay) { // this function is called when something is dropped

                    // retrieve the dropped element's stored Event Object
                    var originalEventObject = $(this).data('eventObject');

                    // we need to copy it, so that multiple events don't have a reference to the same object
                    var copiedEventObject = $.extend({}, originalEventObject);

                    // assign it the date that was reported
                    copiedEventObject.start = date;
                    copiedEventObject.allDay = allDay;

                    // render the event on the calendar
                    // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                    $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        $(this).remove();
                    }

                },

                events: [
                    
                ],
            });
        });

        //For Digital Clock

        function MyClock() {
            var clock = $("#clock").clock({
                theme: 't2'
            }),
            data = clock.data('clock');

           

            var d = new Date();
            d.setHours(9);
            d.setMinutes(51);
            d.setSeconds(20);

            var clock1 = $("#clock1").clock({
                theme: 't2',
                date: d
            });

            var clock2 = $("#clock2").clock({
                theme: 't3'
            });



            var _gaq = _gaq || [];
            _gaq.push(['_setAccount', 'UA-36251023-1']);
            _gaq.push(['_setDomainName', 'jqueryscript.net']);
            _gaq.push(['_trackPageview']);

            (function () {
                var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
            })();
        }


    </script>


    <style>
       

        #external-events {
            float: left;
            width: 150px;
            padding: 0 10px;
            text-align: left;
        }

            #external-events h4 {
                font-size: 16px;
                margin-top: 0;
                padding-top: 1em;
            }

        .external-event { /* try to mimick the look of a real event */
            margin: 10px 0;
            padding: 2px 4px;
            background: #3366CC;
            color: #fff;
            font-size: .85em;
            cursor: pointer;
        }

        #external-events p {
            margin: 1.5em 0;
            font-size: 11px;
            color: #666;
        }

            #external-events p input {
                margin: 0;
                vertical-align: middle;
            }

        #calendar {
            /* 		float: right; */
            /*margin: 0 auto;*/
            width: 332px;
            background-color: #FFFFFF;
            border-radius: 1px;
            box-shadow: 0 1px 2px #C3C3C3;
            -webkit-box-shadow: 0px 0px 21px 2px rgba(0,0,0,0.18);
            -moz-box-shadow: 0px 0px 21px 2px rgba(0,0,0,0.18);
            box-shadow: 0px 0px 21px 2px rgba(0,0,0,0.18);
        }

        canvas {
            width: 200px !important;
        }
         .boxtex3 {
               color: #000 !important;
               padding-bottom: 5px;
           }
    </style>
    <%--<asp:updatepanel id="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">


                        <div id='wrap' class="col-md-3">
                            
                            <div class="col-md-12">
                                
                                 <div class="clock" id="clock"></div>
                                <div style="margin-top:-50px;">
                                     <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div >

                                                 <div>
                                                    <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8010")%>" target="_blank" style="padding-top:0px;">
                                                         
                                                        <span  class='glyph-icon flaticon-business73 box-title boxtex3 boxdsh'>&nbsp;&nbsp;&nbsp; Flow</span>
                                                         <br/> 
                                                      
                                                         

                                                          <br/>   
                                                    </a>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                
                               
                                </div>
                               
                             </div>
                                
                           
                           
                            <%--<div id='calendar'></div>--%>

                            <div style='clear: both'></div>
                        </div>
                        <div class="col-md-9">
                          
                            <asp:Panel runat="server" CssClass="pnlflowchart" ID="panelConstruction" >

                            <style>
                                html body {
                                    background-color: #ffffff !important;
                                }

                                .sidepnaelMenu {
                                    background: none !important;
                                    color: #000 !important;
                                    height: 214px;
                                }

                                .metro-panel .dropSideMenu a {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .fa {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .dropSideMenu li a {
                                    background: #fff none repeat scroll 0 0;
                                    color: #000;
                                    display: block;
                                    font-size: 13px;
                                    height: 68px !important;
                                    padding: 1px 20px !important;
                                    text-align: center;
                                    vertical-align: middle;
                                    width: 90px;
                                }

                                .sidepnaelMenu .dropSideMenu {
                                    color: #000 !important;
                                    height: 228px;
                                    margin-left: -182px !important;
                                    right: 0;
                                    top: 0;
                                    width: 406px !important;
                                }

                                .box-title, .featured-box-title, .featured-title {
                                    /**/
                                }

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: normal;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
                                    color: #fff;
                                }


                                .page-section.home-page {
                                    /*background: url(assets/images/bg-3.jpg) #3495F3;*/
                                    background-color: #E0E0E0;
                                    color: #001840 !important;
                                    background-size: cover;
                                }


                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                }


                                .boxShadow {
                                    -webkit-border-bottom-right-radius: 5px;
                                    -webkit-border-bottom-left-radius: 5px;
                                    -moz-border-radius-bottomright: 5px;
                                    -moz-border-radius-bottomleft: 5px;
                                    border-bottom-right-radius: 5px;
                                    border-bottom-left-radius: 5px;
                                    box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
                                    border-bottom: 1px solid rgba(101, 125, 142, 0.75);
                                }

                                .boxInn {
                                    padding-bottom: 5px;
                                }

                                .fa {
                                    color: #fff;
                                    display: inline-block;
                                    font-family: FontAwesome;
                                    font-feature-settings: normal;
                                    font-kerning: auto;
                                    font-language-override: normal;
                                    font-size: inherit;
                                    font-size-adjust: none;
                                    font-stretch: normal;
                                    font-style: normal;
                                    font-synthesis: weight style;
                                    font-variant: normal;
                                    font-weight: normal;
                                    line-height: 1;
                                    text-rendering: auto;
                                    transform: translate(0px, 0px);
                                    float: left;
                                    margin-left: 15px;
                                }

                                .boxtex {
                                    /*color: #758388;*/
                                    color: #000;
                                    padding-bottom: 7px;
                                }

                                .boxtex1 {
                                    color: #fff;
                                    padding-bottom: 7PX;
                                    padding-left: 22PX;
                                }

                                .boxtexdoc {
                                    color: #fff;
                                    padding-bottom: 7PX;
                                    padding-left: 12PX;
                                }
                         .boxtexstep {
                                    color: #fff;
                                    padding-bottom: 7PX;
                                    padding-left: 42PX;
                                }
           

                                .boxtex2 {
                                    color: #fff;
                                    padding-bottom: 7PX;
                                    padding-left: 13PX;
                                }

                                .boxtex3 {
                                    color: #ADC3C0;
                                    padding-bottom: 5px;
                                }

                                .color-71 {
                                    background: #00AF50;
                                }

                                .color-72 {
                                    background: #2F74B5;
                                }

                                .color-73 {
                                    background: #92D14F;
                                }

                                .color-74 {
                                    background: #5A9BD5;
                                }

                                .color-75 {
                                    background: #538234;
                                }

                                .color-76 {
                                    background: #00AF50;
                                }

                                .color-77 {
                                    background: #808080;
                                }

                                .color-78 {
                                    background: #263A4A;
                                }

                                .color-79 {
                                    background: #01B0F1;
                                }

                                .color-80 {
                                    /*background: grey;*/
                                    background: #ADC3C0;
                                    border-radius: 5px;
                                }

                                /*.color-con {
                                   
                                    background: #fafcfb;
                                    
                                }*/
                                .color-das {
                                    background: #576069;
                                    border-radius: 5px;
                                }

                                .color-81 {
                                    background: #833D0C;
                                }

                                .color-82 {
                                    background: #8397B0;
                                }

                                .color-83 {
                                    background: #4473C5;
                                }

                                .color-84 {
                                    background: #70AD46;
                                }

                                .colorcons {
                                    color: grey;
                                }
                            </style>

                           


                            <div class="row" style="padding: 0 6px;">

                                <div id="before-tiles" class="large-12 columns"></div>
                                <%--<div class="boxInn">--%>


                                    

                                    

                                    <div class="four large-12 columns" style="margin-left:50px;">
                                        <h4></h4>
                                        <div class="row">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-5">
                                             <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class='color-80'>

                                                 <div class='color-80' style="padding-bottom: 0px;padding-top:0">
                                                    <asp:LinkButton ID="lnkabp" runat="server" OnClick="lnkabp_Click"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>1. Annual Business Plan</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                </div>
                                                    


                                                </div>
                                            </div>

                                            <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class="color-con">

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                      <asp:LinkButton ID="lnkquotation" runat="server" OnClick="lbtnTender_Click"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>2. Quotation</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                    <%--<a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>2. Quotation</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>--%>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class='color-80'>

                                                 <div class='color-80' style="padding-bottom: 0px;padding-top:0">
                                                     <asp:LinkButton ID="lnkbudget" runat="server" OnClick="lnkbtnMatr_Click"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>3. Budget</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                   <%-- <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>" target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>3. Budget</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>--%>
                                                </div>
                                                    


                                                </div>
                                            </div>

                                            <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class="color-con">

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                     <asp:LinkButton ID="lnkprpln" runat="server" OnClick="lnkbtnMatr_Click"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>4. Project Planning</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                   <%-- <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>" target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>4. Project Planning</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>--%>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class='color-80'>

                                                 <div class='color-80' style="padding-bottom: 0px;padding-top:0">
                                                     <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkbtnMatr_Click"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>5. Project Implementation</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                   <%-- <a href="<%=this.ResolveUrl("~/F_99_Allinterface/BudgetInterface.aspx")%>" target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>5. Project Implementation</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>--%>
                                                </div>
                                                    


                                                </div>
                                            </div>

                                            <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class="color-con">
                                                    
                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                      <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnkbtnGoodsInv_Click"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>6. Inventory Control</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                    <%--<a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>6. Inventory Control</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>--%>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class='color-80'>

                                                 <div class='color-80' style="padding-bottom: 0px;padding-top:0">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>" target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>7. Purchase</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                               
                                            <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class="color-con">

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=")%>" target="_blank style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>8. Contracting</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>
                                                </div>
                                                    


                                                </div>
                                            </div>

                                            </div>
                                            <div class="col-md-1"></div>
                                            <div class="col-md-5">
                                                <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class="color-80">

                                                 <div  style="padding-bottom: 0px;padding-top:0"> 
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>" target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>9. General Bill</span>
                                                         <br/> 
                                                      

                                                          <br/>   
                                                    </a>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class='color-con'>

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                      
                                                    <a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=")%>" target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>10. Bill Register</span>
                                                         <br/> 
                                                       
                                                      
                                                          <br/>   
                                                    </a>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                 <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class="color-80">

                                                 <div  style="padding-bottom: 0px;padding-top:0">

                                                      <asp:LinkButton ID="lbkbill" runat="server" OnClick="lnkBill_OnClick"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>11. Billing</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                   <%-- <a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>11. Billing</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>--%>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class='color-con'>

                                                 <div  style="padding-bottom: 0px;padding-top:0">

                                                     <asp:LinkButton ID="lnkrecovery" runat="server" OnClick="lnkBill_OnClick"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>12. Recovery</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                    <%--<a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>12. Recovery</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>--%>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                 <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class="color-80">

                                                 <div  style="padding-bottom: 0px;padding-top:0">

                                                     <asp:LinkButton ID="lnkfixasset" runat="server" OnClick="lnkbtnAssets_Click"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>13. Fixed Assets</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                    <%--<a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>13. Fixed Assets</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>--%>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class='color-con'>

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=")%>" target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>14. Accounts</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-12 columns contact-box space' style="margin-bottom:4px;">
                                                <div class="color-80">

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                    <a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex boxdsh'>15. My HRM</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                            </div>

                                           

                                            
                                          
                                        </div>








                                        <div class="row" style="margin-top:30px;">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-5">
                                                <div class='six small-6 columns contact-box'>
                                                <div class="color-das"">

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                     <asp:LinkButton ID="lnkdoc" runat="server" OnClick="linkdocumt_Click"  style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtexdoc boxdsh'>Documentation</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                    <%--<a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtexdoc boxdsh'>Documentation</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>
                                                      --%>
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-6 columns contact-boxS'>
                                                <div class="color-das">

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                    <asp:LinkButton  ID="lnkconp" OnClick="lnkconp_Click"  runat="server"   style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex2 boxdsh'>Control Panel </span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                      
                                                </div>
                                                    


                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-1"></div>
                                            <div class="col-md-5">
                                                <div class='six small-6 columns contact-box'>
                                                <div class="color-das"">

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                     

                                                     <asp:LinkButton  ID="lnkdash" OnClick="lnkbtnGeneral_Click"  runat="server"   style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex1 boxdsh'>Dashboard</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </asp:LinkButton>
                                                    <%--<a href="<%=this.ResolveUrl("~/CompanyOverAllReport.aspx?comcod=")%>"  target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex1 boxdsh'>Dashboard</span>
                                                         <br/> 
                                                   

                                                          <br/>   
                                                    </a>--%>
                                                      
                                                </div>
                                                    


                                                </div>
                                            </div>
                                                <div class='six small-6 columns contact-boxS'>
                                                <div class="color-das">

                                                 <div  style="padding-bottom: 0px;padding-top:0">
                                                    <a href="<%=this.ResolveUrl("~/HRMAllInOne.aspx")%>" target="_blank" style="padding-top:0px;">
                                                          
                                                        <span class='box-title boxtex2 boxdsh'>Dashboard HRM</span>
                                                         <br/> 
                                                       

                                                          <br/>   
                                                    </a>
                                                      
                                                </div>
                                                    


                                                </div>
                                            </div>
                                            </div>
                                            


                                        </div>

                                    


                                </div>

                            </div>
                            <div class="clearfix"></div>


                        </asp:Panel>

                        </div>
                    </div>
                    
                    <%--<div class="modal fade" id="squarespaceModal" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
                       <div class="modal-dialog">
	                    <div class="modal-content">
		                    <div class="modal-header">
			                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
			                  
		                    </div>
		                    <div class="modal-body">
			                        <div id='calendar'></div>
                              
			                    <form>
                                  <div class="form-group" style="margin:0 auto">
                                   <div id='calendar'  style="margin:0 auto"></div>
                                  </div>
                                 
                                </form>

		                    </div>
		                    <div class="modal-footer">
			                    <div class="btn-group btn-group-justified" role="group" aria-label="group button">
				                    <div class="btn-group" role="group">
					                    <button type="button" class="btn btn-default" data-dismiss="modal"  role="button">Close</button>
				                    </div>
				                   
			                    </div>
		                    </div>
	                    </div>
                      </div>
                    </div>--%>


                </div>
            </div>

        <%--</ContentTemplate>
    </asp:updatepanel>--%>
</asp:Content>

