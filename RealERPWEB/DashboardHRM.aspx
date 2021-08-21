<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DashboardHRM.aspx.cs" Inherits="RealERPWEB.DashboardHRM" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel='stylesheet' href='assets\css\style.css' type='text/css' media='all' />
    <%--<link rel='stylesheet' href='assets\css\colors.css' type='text/css' media='all' />--%>
    <%--<link rel='stylesheet' href='assets\css\comments.css' type='text/css' media='all' />--%>
    <%--<link rel='stylesheet' id='responsiveslides-css' href='assets\css\responsiveslides.css' type='text/css' media='all' />--%>
    <link rel='stylesheet' id='reponsive-css' href='assets\css\reponsive.css' type='text/css' media='all' />
    <%--<link rel='stylesheet' id='animate-custom-css' href='assets\css\animate-custom.css' type='text/css' media='all' />--%>

    <style>
        .dropdown-menu {
            min-width: 139px !important;
        }

        

       


        body {
           font-family: Calibri,Arial !important;
            font-size: 10px !important;
        }

        .listStyle1 {
            padding-top: 4px !important;
            background-color: Transparent !important;
        }

        .listStyle2 {
            padding-top: 4px !important;
            background-color: #f9f9f9 !important;
        }

       
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   


    <script>
        $(document).ready(function () {


            $('#ddlreport').on('click', function () {

                var ddlvalue = $(this).val();
                //$('#ddlreport option').attr('selected', '');
              
                switch (parseInt(ddlvalue)) {

                    case 1:
                        window.open("F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpAllInfo&amp;comcod=");


                        break;
                    case 2:
                        window.open("F_81_Hrm/F_83_Att/RptAttendenceSheet.aspx");
                        break;
                    case 3:
                        window.open("F_81_Hrm/F_84_Lea/RptHREmpLeave.aspx?Type=EmpLeaveSt");
                        break;
                    case 4:
                       
                        window.open("F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll");
                        break;
                    case 5:
                        window.open("F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip");


                        break;
                    case 6:
                        window.open("F_81_Hrm/F_90_PF/RptAccProFund.aspx?Type=Pffund");
                        break;
                    case 7:
                        window.open("F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=TransList&amp;comcod=");
                        break;
                    case 8:
                        window.open("F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpHold&amp;comcod=")
                        break;
                    case 9:
                        window.open("F_81_Hrm/F_89_Pay/EmpMonthSummary.aspx?Type=salati");
                        break;
                    case 10:
                        window.open("F_81_Hrm/F_93_AnnInc/RptIncrement.aspx");
                        break;
                    case 11:
                        window.open("F_81_Hrm/F_91_ACR/RptPerAppraisal.aspx");
                        break;

                    default:
                        // alert(ddlvalue);
                        break;
                }
            });




        })



    </script>


    <%--<div class="myBody">
        <div id="spaces-main" class="pt-perspective">
            <section class="page-section home-page">--%>
    <div class="container">
        <div class="row metro-panel pt-page-moveFromRight">
            <asp:Panel ID="PanTrading" runat="server">
                <style>
                    html body {
                        background-color: #fff;
                    }


                    @font-face {
                        font-family: 'Agency FB';
                        src: url('fonts/AgencyFB-Bold.eot?#iefix') format('embedded-opentype'), url('fonts/AgencyFB-Bold.woff') format('woff'), url('fonts/AgencyFB-Bold.ttf') format('truetype'), url('fonts/AgencyFB-Bold.svg#AgencyFB-Bold') format('svg');
                        font-weight: normal;
                        font-style: normal;
                    }

                    .box-title,
                    .featured-box-title,
                    .featured-title {
                        position: absolute;
                        bottom: 5px;
                        left: 15px;
                       font-size: 14px;
                       
                        font-weight: normal;
                        text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                        /*font-family: 'inherit' !important;*/
                        
                       
                    }

                    .page-section.home-page {
                        background-color: #E0E0E0;
                        color: #001840 !important;
                        background-size: cover;
                    }

                    .box-title, .featured-box-title, .featured-title {
                        left: 6px;
                        font-family:initial !important;
                    }

                    .boxShadow1 {
                        /*box-shadow: 0 0 20px #888888;*/
                        -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                        -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                        box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                    }

                    /*.metro-panel .space > div {
                                    margin: 1px -4px;
                                }*/

                    .fadeInRightBig h3 {
                        font-family: 'Agency FB';
                        background: #CED666;
                        /*border: 1px solid #699f44;*/
                        /*box-shadow: 0 0 4px 2px #bec9b6 inset;*/
                        color: #7f6930;
                        font-family: "Agency FB";
                        font-size: 22px;
                        line-height: 40px;
                        margin: 5px -3px 0;
                        text-align: center;
                        text-decoration: underline;
                        font-weight: normal;
                    }

                    .mid h3 {
                        background: #CED666;
                    }

                    .fa-4x {
                        font-size: 17px !important;
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

                    .arrow {
                        display: inline-block;
                        float: left;
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
                        line-height: 2;
                        padding-left: 70px !important;
                        padding-top: 17px !important;
                        text-rendering: auto;
                        color: lavender;
                    }

                    .boxShadow {
                        /*background-image:url("Image/bg.png") !important;*/
                    }

                    .incolor {
                        color: #575624 !important;
                    }



                    /****************************************   Custom Style   **************************************/

                    .color-1 {
                        background: #564825;
                    }

                    .color-2 {
                        background: #6B5B86;
                    }

                    .color-3 {
                        background: #000000; /* fallback for old browsers */
                        background: -webkit-linear-gradient(to left, #000000, #434343); /* Chrome 10-25, Safari 5.1-6 */
                        background: linear-gradient(to left, #000000, #434343); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                    }


                    .color-4 {
                        /* fallback DIY*/
                        /* Safari 4-5, Chrome 1-9 */
                        background: -webkit-gradient(linear, left top, right top, from(#2F2727), color-stop(0.05, #1a82f7), color-stop(0.5, #2F2727), color-stop(0.95, #1a82f7), to(#2F2727));
                        /* Safari 5.1+, Chrome 10+ */
                        background: -webkit-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                        /* Firefox 3.6+ */
                        background: -moz-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                        /* IE 10 */
                        background: -ms-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                        /* Opera 11.10+ */
                        background: -o-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                    }

                    .color-5 {
                        background: #141E30; /* fallback for old browsers */
                        background: -webkit-linear-gradient(to left, #141E30, #243B55); /* Chrome 10-25, Safari 5.1-6 */
                        background: linear-gradient(to left, #141E30, #243B55); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                        /*background: #004FF9; 
background: -webkit-linear-gradient(to left, #004FF9 , #FFF94C);
background: linear-gradient(to left, #004FF9 , #FFF94C);*/
                    }

                    .color-6 {
                        /* fallback DIY*/
                        /* Safari 4-5, Chrome 1-9 */
                        background: -webkit-gradient(linear, left top, right top, from(#2F2727), color-stop(0.05, #1a82f7), color-stop(0.5, #2F2727), color-stop(0.95, #1a82f7), to(#2F2727));
                        /* Safari 5.1+, Chrome 10+ */
                        background: -webkit-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                        /* Firefox 3.6+ */
                        background: -moz-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                        /* IE 10 */
                        background: -ms-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                        /* Opera 11.10+ */
                        background: -o-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                    }

                    .color-7 {
                        /*background: #6A9113;*/
                        /*background: #D6CFBF;*/
                        /*background: #576069;*/
                        background:#337ab7;
                    }

                    .metro-panel .space > div {
                        min-height: 59px;
                        text-align: center !important;
                        position: relative;
                        overflow: hidden;
                        /* margin: 3px -3px; */
                        margin: 3px -3px;
                    }

                    .color-7 .fa {
                        /*right:0;*/
                        margin-top: -15px;
                        color: #f1eee8 !important;
                    }

                    .color-7 .box-title {
                        bottom: 33%;
                        left: 9px;
                        color: #938562 !important;
                    }

                    .color-8 .fa {
                        padding-top: 15px;
                        margin-left: 4px;
                        color: #938562;
                        font-size: 12px !important;
                    }

                    .color-8 .box-title {
                        bottom: 33%;
                        left: 24px;
                        color: #938562 !important;
                    }

                    .color-8 .leftIssue {
                        left: 50%;
                    }

                    .color-8 .leftIssue1 {
                        margin-left: 75px;
                    }

                    .color-8 {
                        /*background-color:#089BA6;*/
                        /*background-color:#2B6457;*/
                        background-color: #FCF5E5;
                    }

                    .color-9 {
                        background-color: #ebebeb;
                    }

                        .color-9 .box-title {
                            color: #089bad !important;
                        }

                        .color-9 .fa {
                            color: #089bad !important;
                        }

                    .color-10 {
                        background: #43cea2; /* fallback for old browsers */
                        background: -webkit-linear-gradient(to left, #43cea2, #185a9d); /* Chrome 10-25, Safari 5.1-6 */
                        background: linear-gradient(to left, #43cea2, #185a9d); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                    }

                    .color-11 {
                        background-color: #089BA6;
                    }

                    .color-12 {
                        background: #00223E; /* fallback for old browsers */
                        background: -webkit-linear-gradient(to left, #00223E); /* Chrome 10-25, Safari 5.1-6 */
                        background: linear-gradient(to left, #00223E); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                    }

                    .color-13 {
                        background: #A26B57;
                        /*background: #780206;*/ /* fallback for old browsers */
                        /*background: -webkit-linear-gradient(to left, #780206 , #061161);*/ /* Chrome 10-25, Safari 5.1-6 */
                        /*background: linear-gradient(to left, #780206 , #061161);*/ /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                    }

                    .color-14 {
                        background: #000000; /* fallback for old browsers */
                        background: -webkit-linear-gradient(to left, #000000, #53346D); /* Chrome 10-25, Safari 5.1-6 */
                        background: linear-gradient(to left, #000000, #53346D); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                    }

                    .color-15 {
                        background: #606c88; /* fallback for old browsers */
                        background: -webkit-linear-gradient(to left, #606c88, #3f4c6b); /* Chrome 10-25, Safari 5.1-6 */
                        background: linear-gradient(to left, #606c88, #3f4c6b); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                    }

                    /*.color-16 {
                                    background-color: #68a5ad;
                                }*/



                    .color-17 {
                        background: #16222A; /* fallback for old browsers */
                        background: -webkit-linear-gradient(to left, #16222A, #3A6073); /* Chrome 10-25, Safari 5.1-6 */
                        background: linear-gradient(to left, #16222A, #3A6073); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                    }

                    .color-18 {
                        background: #4E4261;
                    }

                    .color-19 {
                        background: #2C632E; /* fallback for old browsers */
                    }

                    .color-20 {
                        background-color: #E0E0E0;
                    }

                    .color-21 {
                        background-color: #4B281B;
                    }

                    .color-22 {
                        background-color: #897770;
                    }

                    .color-23 {
                        background-color: #62875d;
                    }

                    .color-24 {
                        background-color: #5d8787;
                    }

                    .color-25 {
                        background-color: #7a94cc;
                    }

                    .color-26 {
                        background-color: #8D5E6B;
                    }

                    .color-27 {
                        background-color: #355A3E;
                    }

                    .color-28 {
                        background-color: #9F8F19;
                    }

                    /*.color-29{
                                    background-color: #089bad;
                                }*/

                    /*.color-30 {
                                    background-color: #68a5ad;
                                }*/

                    .color-91 {
                        background: #135058;
                        color: #fff;
                    }

                    .color-92 {
                        background: #bf8f00;
                        color: #fff;
                    }

                    .color-94 {
                        background: #68A5AD;
                        color: #fff;
                    }

                    .color-95 {
                        background: #92D14F;
                        color: #fff;
                    }

                    .color-96 {
                        background: #089BA6;
                        color: #fff;
                    }

                    .color-97 {
                        background: #538234;
                        color: #fff;
                    }

                    .color-98 {
                        background: #3C6B95;
                        color: #fff;
                    }

                    .color-99 {
                        background: #B26711;
                        color: #fff;
                    }

                    .color-100 {
                        background: #833D0C;
                        color: #fff;
                    }

                    .color-101 {
                        background: #2E8B57;
                        color: #fff;
                    }

                    .box-title {
                        color: #38331a !important;
                        padding-bottom: 16px;
                    }

                    .co4 {
                        color: #7F6930 !important;
                    }





                    .color-103 {
                        background: #089BAD;
                        color: #fff !important;
                    }

                   



                    .Cl .box-title {
                        font-size: 16px;
                       
                    }

                    .Cl2 .box-title {
                        font-size: 14px;
                       s
                    }

                    .colRed {
                        color: cornsilk;
                    }

                    .Cldash .fa {
                        font-size: 28px !important;
                    }

                    .Cldash .box-title {
                        font-size: 18px !important;
                    }

                    form .row .row .column, form .row .row .columns {
                        padding: 0 5px;
                    }

                    .color-93 {
                        background: #00AF50 !important;
                        color: #fff !important;
                    }

                        .color-93 .box-title {
                            color: #fff !important;
                        }

                        .color-93 .fa {
                            color: #fff !important;
                        }

                    .color-103 .box-title {
                        color: #fff !important;
                    }

                    .color-103 .fa .co6 {
                        color: #fff !important;
                    }

                    .color-102 {
                        background: #EDF5EA;
                        color: #0A3662 !important;
                    }

                        .color-102 .fa {
                            color: #0A3662 !important;
                        }

                        .color-102 .box-title {
                            color: #0A3662 !important;
                        }


                    .color-105 {
                        /*background: #6A9113;*/
                        background: #fcf5f6;
                    }

                        .color-105 .fa {
                            /*right:0;*/
                            margin-top: -15px;
                            color: #938562 !important;
                        }

                        .color-105 .box-title {
                            bottom: 33%;
                            left: 15px;
                            color: #938562 !important;
                        }

                    .dshna .fa {
                        margin-left: 129px;
                        margin-top: 9px;
                    }

                    .colorcons {
                        color: grey;
                    }

                    .margin-bottom {
                        margin-bottom: 4px;
                    }

                    .menu-height {
                        height: 42px;
                    }

                    .menu-heightR {
                        height: 62px; /*42*/
                    }

                    .color-80 {
                        /*background: grey;*/
                        background: #e4e2e27a;
                        border-radius: 5px;
                        color: #576069;
                    }



                    .fa {
                        display: inline-block;
                        float: left;
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
                        line-height: 1.5;
                        margin-left: 19px;
                        text-rendering: auto;
                        color: #1b1919;
                    }

                    .fa-long-arrow-right {
                        color: #fff !important;
                    }

                    .color-das {
                        background: #ADC3C0;
                        border-radius: 5px;
                    }

                    canvas {
                        width: 120px !important;
                        margin-left: 8px;
                        margin-top: 60px;
                    }

                    .boxtex3 {
                        color: #43484c !important;
                        padding-bottom: 5px;
                    }

                    span.box-title.boxtex {
                        color: #fff !important;
                        padding-top: 10px !important;
                        padding-bottom: 0px !important;
                    }

                    .boxtex {
                        /*color: #000;*/
                        color: #fff;
                        padding-bottom: 7px;
                    }

                    .boxtex1 {
                        color: #43484c !important;
                        /*padding-bottom: 9px;*/
                        padding-left: 47PX;
                    }

                    .boxtexdoc {
                        color: #43484c !important;
                        padding-bottom: 9PX;
                        padding-left: 27PX;
                    }

                    .boxtexstep {
                        /*color: #fff;*/
                        color: #43484c !important;
                        padding-bottom: 7PX;
                        padding-left: 42PX;
                    }


                    .boxtex2 {
                        color: #43484c !important;
                        /*padding-bottom: 9PX;*/
                        padding-left: 32PX;
                    }


                    .Cl .box-title {
                        font-size: 16px;
                        color: #43484c !important;
                    }
                </style>

                <div class="row  boxShadow">

                    <div id='wrap' class="four large-2 columns mid">

                        <div class="col-md-12">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <img src="Image/business_logo/hrm 2.png" class="img img-responsive img-circle" />

                            <div style="margin-top: 100px;">
                                <div class="dropdown">
                                    <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                                        More Function 
    <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li><a tabindex="-1" href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/HRCodeBook.aspx")%>" target="_blank">Information Code</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubCodeBook.aspx?InputType=DeptCode")%>" target="_blank">Department Code</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/HRDesigCode.aspx")%>" target="_blank">Designations Code</a></li>
                                        <!--<li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccVoucherUnposted.aspx")%>" target="_blank">Approval Process</a></li>-->
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <div style='clear: both'></div>
                    </div>
                    <br />
                    <div class="four large-7 columns mid">
                        <%-- <h4><a href="<%=this.ResolveUrl("~/StandardFlow.aspx")%>">Entry (Manufacturing)</a></h4>--%>

                        <div class="row">



                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_81_Rec/JobAdvertisement.aspx?Type=Entry")%>" target="_blank">
                                <div class='six small-2  columns contact-box space'>
                                    <div class='color-7'>

                                        <span class='box-title boxtex'>Recuritment</span>
                                        <br />
                                        <i class='fa fa-long-arrow-right fa-4x arrow'></i>


                                    </div>
                                </div>
                            </a>


                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_81_Rec/JobAdvertisement.aspx?Type=App")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Job Adv. Approved</span>
                                        <%-- <br>
                                                        <i class='fa fa-th-large'></i>--%>
                                    </div>
                                </div>
                            </a>
                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=SList")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Sort Listing</span>
                                        <%-- <br>
                                                        <i class='fa fa-th-large'></i>--%>
                                    </div>
                                </div>
                            </a>

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=IResult")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Interview Result</span>
                                        <%-- <br>
                                                        <i class='fa fa-th-large'></i>--%>
                                    </div>
                                </div>
                            </a>

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=Fselection")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Final Selection</span>
                                        
                                    </div>
                                </div>
                            </a>

                             <a href="<%=this.ResolveUrl("F_81_Hrm/F_81_Rec/LetterOfAppoinment.aspx?Type=LCreate")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Appiontment Letter</span>
                                        
                                    </div>
                                </div>
                            </a>
                          


                            
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-7'>

                                        <span class='box-title boxtex' style="font-size: 14px;">Appointment</span>
                                        <br>
                                        <i class='fa fa-long-arrow-right fa-4x arrow'></i>

                                    </div>
                                </div>
                           

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_82_App/EmpEntry01.aspx?Type=Entry&amp;empid=")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Personal Information</span>


                                    </div>
                                </div>
                            </a>



                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_82_App/HREmpEntry.aspx?Type=Aggrement")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Agreement</span>
                                       
                                    </div>
                                </div>
                            </a>


                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_82_App/ImgUpload.aspx?Type=Entry&amp;empid=")%>" target="_blank">
                                <div class='six small-6 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Image Upload</span>


                                    </div>
                                </div>
                            </a>

                           <!-- <a href="<%=this.ResolveUrl("~")%>">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'></span>
                                        <%--<br>
                                                          <i class='fa fa-th-large'></i>--%>
                                    </div>
                                </div>
                            </a>


                            <a href="<%=this.ResolveUrl("~")%>">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'></span>
                                        <%--<br>
                                                        <i class='fa fa-th-large'></i>--%>
                                    </div>
                                </div>
                            </a>-->




                          
                            <div class='six small-2 columns contact-box space'>
                                <div class='color-7'>
                                    <span class='box-title boxtex'>Attendance</span>
                                    <br>
                                    <i class='fa fa-long-arrow-right fa-4x arrow'></i>

                                </div>
                            </div>

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_83_Att/HREmpOffDays.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Off Days</span>


                                    </div>
                                </div>
                            </a>

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_83_Att/HRDailyAtten.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>System Atten.</span>
                                       
                                    </div>
                                </div>
                            </a>




                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_83_Att/HRDailyAttenManually.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Manual Atten.</span>
                                       
                                    </div>
                                </div>
                            </a>




                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_83_Att/EmpMonLateApproval.aspx?Type=MLateAppDay")%>" target="_blank">
                                <div class='six small-4 columns contact-box space'>
                                    <div class='color-80'>
                                        <span class='box-title'>Late Approval</span>

                                    </div>
                                </div>
                            </a>

                            

                           
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-7'>

                                        <span class='box-title boxtex'>Leave</span>
                                        <br>
                                        <i class='fa fa-long-arrow-right fa-4x arrow'></i>

                                    </div>
                                </div>
                          

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_92_Mgt/InterfaceLeavApp.aspx?Type=Ind")%>" target="_blank">
                                <div class='six small-10 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Leave Interface</span>


                                    </div>
                                </div>
                            </a>

                            <!--<a href="<%=this.ResolveUrl("~")%>">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Process</span>

                                    </div>
                                </div>
                            </a>



                            <a href="<%=this.ResolveUrl("~")%>">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'></span>


                                    </div>
                                </div>
                            </a>










                           
                         





                            <a href="<%=this.ResolveUrl("~")%>">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'></span>

                                    </div>
                                </div>
                            </a>




                            <a href="<%=this.ResolveUrl("~")%>">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'></span>


                                    </div>
                                </div>
                            </a>-->




                           
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-7'>

                                        <span class='box-title boxtex'>Evaluation</span>
                                        <br>
                                        <i class='fa fa-long-arrow-right fa-4x arrow'></i>

                                    </div>
                                </div>
                         
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_87_Tra/HREmpTransfer.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Transfer</span>


                                    </div>
                                </div>
                            </a>
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/RetiredEmployee.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Termination</span>


                                    </div>
                                </div>
                            </a>
                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_92_Mgt/EmpHold.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Hold</span>


                                    </div>
                                </div>
                            </a>

                             <a href="<%=this.ResolveUrl("F_81_Hrm/F_92_Mgt/EmpPro.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Promotion</span>


                                    </div>
                                </div>
                            </a>

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_92_Mgt/RetiredEmployee.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'>Resignation</span>


                                    </div>
                                </div>
                            </a>

                            <!--<a href="<%=this.ResolveUrl("")%>">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'> </span>


                                    </div>
                                </div>
                            </a>

                            <a href="<%=this.ResolveUrl("")%>">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>

                                        <span class='box-title'></span>


                                    </div>
                                </div>
                            </a>-->



                            <%--    <a href="<%=this.ResolveUrl("#")%>">--%>
                            <div class='six small-2 columns contact-box space'>
                                <div class='color-7'>
                                    <span class='box-title boxtex'>Loan & PF</span>
                                    <br>
                                    <i class='fa fa-long-arrow-right fa-4x arrow'></i>
                                </div>
                            </div>
                            <%--    </a>--%>

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_85_Lon/EmpLoanInfo.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>
                                        <span class='box-title'>Loan Installment</span>
                                    </div>
                                </div>
                            </a>
                             <a href="<%=this.ResolveUrl("F_81_Hrm/F_86_All/EmpOvertime.aspx?Type=OtherDeduction")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>
                                        <span class='box-title'>Deduction</span>
                                    </div>
                                </div>
                            </a>


                           
                          
                                <div class='six small-6 columns contact-box space'>
                                    <div class='color-80'>
                                        <span class='box-title'>PF Auto Update</span>
                                    </div>
                                </div>
                          

                           <!-- <a href="<%=this.ResolveUrl("")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>
                                        <span class='box-title'></span>
                                    </div>
                                </div>
                            </a>

                            <a href="<%=this.ResolveUrl("F_81_Hrm/F_92_Mgt/RetiredEmployee.aspx")%>" target="_blank">
                                <div class='six small-2 columns contact-box space'>
                                    <div class='color-80'>
                                        <span class='box-title'></span>
                                    </div>
                                </div>
                            </a>-->



                            <div class="row" style="margin-top: 13px; margin-bottom: 20px;">
                                <div class="col-md-6" style="margin-top: 13px; padding-left: 0px; padding-right: 0px">
                                    <div class='six small-6 columns'>
                                        <div class="color-das menu-height">

                                            <a href="<%=this.ResolveUrl("~/StepofOperation.aspx?moduleid=81")%>" style="padding-top: 0px; color: black" target="_blank">
                                                <span class='box-title boxtexdoc'>Modules</span>
                                                <br />

                                            </a>

                                        </div>
                                    </div>
                                    <div class='six small-6 columns'>
                                        <div class="color-das menu-height">
                                            <div style="padding-bottom: 0px; padding-top: 0">
                                                <a href="<%=this.ResolveUrl("~/StepofOperation.aspx?moduleid=81")%>" style="padding-top: 0px; color: black">
                                                    <span class='box-title boxtexdoc'>Control Panel</span>
                                                    <br />
                                                    <br />
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6" style="margin-top: 13px; padding-right: 0px; padding-left: 0px;">

                                    <div class='six small-6 columns'>
                                        <div class="color-das menu-height">

                                            <div style="padding-bottom: 0px; padding-top: 0">
                                                <a href="<%=this.ResolveUrl("~/StepofOperation.aspx?moduleid=81")%>" style="padding-top: 0px; color: black">

                                                    <span class='box-title boxtexdoc'>Dashboard HRM</span>
                                                    <br />
                                                    <br />
                                                </a>

                                            </div>
                                        </div>
                                    </div>
                                    <div class='six small-6 columns'>
                                        <div class="color-das menu-height">
                                            <div style="padding-bottom: 0px; padding-top: 0">
                                                 <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_93_AnnInc/AnnualIncrement.aspx")%>" style="padding-top: 0px; color: black" target="_blank">                                                 
                                                        <span class='box-title boxtexdoc'>Annual Increment</span>
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

                    <div class="four large-3 columns Cl">
                        <div class="col-md-12">
                            <div class="bg-primary menu-heightR">
                                <div style="padding-bottom: 0px; padding-top: 0">
                                    <span class='box-title boxtex2' style="font-size: 18px; padding-left: 28px; color: #ffffff !important"><span class="glyphicon glyphicon-hand-right"></span>&nbsp;&nbsp;Reports</span>
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:ListBox ID="ddlreport" runat="server" ClientIDMode="Static"
                                Height="325px"
                                SelectionMode="Multiple" TabIndex="12" CssClass="form-control ddlRptMenufont">


                                <asp:ListItem Value="0" style="display: none;">Select Report</asp:ListItem>
                                <asp:ListItem Value="1" class="listStyle1"> 01. Employee Information </asp:ListItem>
                                <asp:ListItem Value="2" class="listStyle2"> 02. Attendance Information </asp:ListItem>
                                <asp:ListItem Value="3" class="listStyle1"> 03.Individual Employee Leave Status</asp:ListItem>
                                <asp:ListItem Value="4" class="listStyle2"> 04. Actual Salary Sheet </asp:ListItem>
                                <asp:ListItem Value="5" class="listStyle1"> 05. Pay Slip </asp:ListItem>
                                <asp:ListItem Value="6" class="listStyle2"> 06. Total P.F Fund Report </asp:ListItem>

                                <asp:ListItem Value="7" class="listStyle1"> 07. Employee Transfer List </asp:ListItem>
                                <asp:ListItem Value="8" class="listStyle2"> 08. Employee Hold List </asp:ListItem>

                                <asp:ListItem Value="9" class="listStyle1"> 09. AIT Purpose Salary Statement </asp:ListItem>
                                <asp:ListItem Value="10" class="listStyle2"> 10. Increment Report </asp:ListItem>
                                <asp:ListItem Value="11" class="listStyle1"> 11. Employee Performance Appraisal </asp:ListItem>

                            </asp:ListBox>

                            <div class="row">

                                <div class="" style="margin-top: 5px;">
                                    <a href="F_81_Hrm/F_92_Mgt/AllEmpList.aspx?Type=Report&comcod=" class="btn btn-danger btn-xs" target="_blank"  role="button"><span class="glyphicon glyphicon-list-alt"></span>
                                        <br />
                                       Members</a>
                                    <a href="F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=SalSummary" class="btn btn-warning btn-xs" target="_blank" role="button"><span class="glyphicon glyphicon-file"></span>
                                        <br />
                                        Salary</a>
                                    <a href="F_81_Hrm/F_83_Att/RptAttendenceSheet.aspx" class="btn btn-primary btn-xs" target="_blank" role="button"><span class="glyphicon glyphicon-signal"></span>
                                        <br />
                                        Atten.</a>
                                    <a href="F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02.aspx?Type=EmpLeaveStatus" class="btn btn-info btn-xs" target="_blank" role="button"><span class="glyphicon glyphicon-usd"></span>
                                        <br />
                                        Leave</a>

                                    <a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType" class="btn btn-success btn-xs" target="_blank" role="button"><span class="glyphicon glyphicon-dashboard"></span>
                                        <br />
                                       Seperation</a>
                                </div>
                            </div>




                        </div>
                    </div>
                </div>


            </asp:Panel>
        </div>
    </div>
    <%-- </section>
                </div>
               
           

        </div>--%>





    <script type='text/javascript' src='assets\js\jquery\jquery.js'></script>
    <script type='text/javascript' src='assets\js\jquery\jquery-migrate.min.js'></script>
    <script type='text/javascript' src='assets/js/comment-reply.min.js'></script>
    <script type='text/javascript' src='assetsjs/vendor/custom.modernizr.js'></script>
    <script type='text/javascript' src='assets\js\foundation.min.js'></script>
    <script type='text/javascript' src='assets\js\modernizr.custom.js'></script>
    <script type='text/javascript' src='assets\js\foundation\foundation.section.js'></script>
    <script type='text/javascript' src='assets\js\responsiveslides.js'></script>
    <script type='text/javascript' src='assets\js\scripts.js'></script>




    <!-- jQuery library -->
    <script src="assets\js\jquery.min.js"></script>

    <script type="text/javascript" src="assets\js\clock-1.1.0.min.js"></script>
    <script type='text/javascript' src='assets\js\wd-ajax-load\js\load-posts.js'></script>
    <script type='text/javascript' src='assets/js/jquery.form.min.js'></script>




</asp:Content>

