<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ASITNEW.Master" CodeBehind="RptIndentPurInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.RptIndentPurInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .modal-dialog {
            margin: 44px auto;
            width: 100%;
        }

        .InBox {
            color: red !important;
        }

        .ServProdInfo .panel-body {
            padding: 0 5px 2px;
        }


        .ServProdInfo label {
            margin-bottom: 0;
        }


        .ServProdInfo .panel {
            margin-bottom: 5px;
        }

        .ServProdInfo .panel-heading {
            padding: 1px 15px;
            font-weight: bold;
            font-size: 16px;
        }

        .menuheading {
            font-size: 16px;
            color: darkcyan;
            padding-left: 10px;
            font-weight: bold;
        }


        .modal-title {
            font-weight: bold;
            color: #000;
        }

            .modal-title span {
                color: red;
            }


        .wrntLbl {
            float: right;
            width: 60%;
            background: #DFF0D8;
            border: 1px solid #DFF0D8;
        }

        .contentPart .ServProdInfo .form-group {
            overflow: hidden;
        }


        .OverAll {
            /*animation-name: example;
            animation-duration: 4s;
            animation-iteration-count: 5;*/
            /*font-size: 18px;*/
            color: black;
            font-size: 14px;
            text-align: center !important;
            margin-top: 0px;
        }


        ul.sidebarMenu {
            margin: 0;
            padding: 0;
            width: 115%;
        }

            ul.sidebarMenu li {
                display: block;
                height: 30px;
                list-style: none;
                border: 1px solid #DFF0D8;
                border-bottom: 0;
            }

                ul.sidebarMenu li:last-child {
                    border-bottom: 1px solid #DFF0D8;
                }

                ul.sidebarMenu li a {
                    text-align: left;
                    display: block;
                    line-height: 30px;
                    font-size: 14px;
                    font-family: Calibri;
                }

                ul.sidebarMenu li h4 {
                    line-height: 50px;
                    text-align: center;
                    display: block;
                }

                ul.sidebarMenu li a:hover {
                    background: #D7E6D1;
                    color: black;
                }

        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                width: 155px;
                padding: 0px 0;
                float: left;
                list-style: none;
                margin: 0 2px;
                color: #fff;
                background: #5F5F5F;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

                ul.tbMenuWrp li a {
                    padding: 0 0;
                    background: #5F5F5F;
                    -webkit-border-radius: 4px;
                    -moz-border-radius: 4px;
                    border-radius: 4px;
                    display: block;
                    color: #fff;
                    padding: 0px 0 0 0;
                    vertical-align: middle;
                    border: none !important;
                }

                    ul.tbMenuWrp li a:hover {
                        background: #12A5A6;
                    }

                    ul.tbMenuWrp li a:focus {
                        outline: none;
                        outline-offset: 0;
                    }

                    ul.tbMenuWrp li a label {
                        color: #fff;
                        background: none;
                        border: none;
                        text-align: center;
                        font-weight: bold;
                        font-size: 16px;
                        display: block;
                        cursor: pointer;
                        width: 100%;
                    }

        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #472AC6 !important;
            color: #fff;
        }





        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 35px;
            margin: 1px 0;
            /*padding: 2px;*/
            width: 100%;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                /*background: #12A5A6;*/
                /*color: #fff;*/
            }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background: #fff;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }

        .rptPurInt span.lbldata2 {
            background: #e5dcdd none repeat scroll 0 0;
            border: 1px solid #3ba8e0;
            display: block;
            font-size: 12px;
            line-height: 22px;
            margin: 14px 0 0;
            padding: 0;
            text-align: center;
        }

        .tbMenuWrp table tr td label .lblactive {
            background: #667DE8;
            color: #000000;
        }

        .lblactive label tr td {
            background: #667DE8 !important;
            color: #000 !important;
        }

        .blink_me {
            animation: blinker 5s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }


        .fan:nth-child(1) {
            background-color: #e6b0e1;
            color: #fff;
            height: 100%;
            line-height: 32px;
        }


        .fan {
            border-radius: 0;
            px display: inline-block;
            float: left;
            font-size: 18px;
            padding: 8px;
        }

            .fan:nth-child(1) {
                background-color: #817E24;
                border-bottom: 2px solid red;
                /* border-top: 2px solid red; */
                /* border-left: 3px solid #4800ff; */
                color: #fff;
                height: 35px;
                line-height: 14px;
            }

            .fan:nth-child(2) {
            }

            .fan:nth-child(3) {
            }

            .fan:nth-child(4) {
            }

            .fan:nth-child(5) {
            }

            .fan:nth-child(6) {
            }

            .fan:nth-child(7) {
            }
        /* for interface*/

        .circle-tile {
            margin-bottom: 15px;
            text-align: center;
            width: 87px;
        }

        .tbMenuWrp table tr td {
            /*height: 50px;*/
            width: 81px;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 3px;
            color: #fff;
            text-align: center;
            /*border: 2px solid #D1D735;*/
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }


        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 12px;
            font-family: Calibri,Arial !important;
            height: 38px;
            margin: -2px auto -22px;
            padding: 8px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 42px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 18px;
            border-radius: 0px 15px;
            font-family: Calibri;
            font-size: 12px;
        }

        .circle-tile-number {
            font-size: 26px;
            font-weight: 700;
            line-height: 1;
            padding: 5px 0 15px;
        }

        .circle-tile-description {
            text-transform: capitalize;
        }

        .circle-tile-footer {
            background-color: rgba(0, 0, 0, 0.1);
            color: rgba(255, 255, 255, 0.5);
            display: block;
            padding: 5px;
            transition: all 0.3s ease-in-out 0s;
        }

            .circle-tile-footer:hover {
                background-color: rgba(0, 0, 0, 0.2);
                color: rgba(255, 255, 255, 0.5);
                text-decoration: none;
            }

        .circle-tile-heading.dark-blue:hover {
            background-color: #2E4154;
        }

        .circle-tile-heading.green:hover {
            background-color: #138F77;
        }

        .circle-tile-heading.orange:hover {
            background-color: #DA8C10;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #CF4435;
        }

        .circle-tile-heading.purple:hover {
            background-color: #7F3D9B;
        }

        .tile-img {
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
        }

        .dark-blue {
            background-color: #34495E;
        }

        .green {
            background-color: #16A085;
        }

        .blue {
            background-color: #2980B9;
        }

        .orange {
            background-color: #F39C12;
        }

        .red {
            background-color: #E74C3C;
        }

        .purple {
            background-color: #8E44AD;
        }

        .dark-gray {
            background-color: #7F8C8D;
        }

        .gray {
            background-color: #95A5A6;
        }

        .light-gray {
            background-color: #BDC3C7;
        }

        .yellow {
            background-color: #F1C40F;
        }

        .text-dark-blue {
            color: #34495E;
        }

        .text-green {
            color: #16A085;
        }

        .text-blue {
            color: #2980B9;
        }

        .text-orange {
            color: #F39C12;
        }

        .text-red {
            color: #E74C3C;
        }

        .text-purple {
            color: #8E44AD;
        }

        .text-faded {
            color: rgba(255, 255, 255, 0.7);
        }
    </style>

    <script type="text/javascript">


        function Search_Gridview(strKey, cellNr, gvname) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                var tbldata;

                switch (gvname) {

                    case 'gvReqInfo':
                        tblData = document.getElementById("<%=this.gvReqInfo.ClientID %>");
                        break;
                    case 'gvReqChk':
                        tblData = document.getElementById("<%=this.gvReqChk.ClientID %>");
                        break;
                    case 'gvreqfapproved':
                        tblData = document.getElementById("<%=this.gvreqfapproved.ClientID %>");
                        break;
                    case 'gvreqsecapproved':
                        tblData = document.getElementById("<%=this.gvreqsecapproved.ClientID %>");
                        break;
                    case 'gvRatePro':
                        tblData = document.getElementById("<%=this.gvRatePro.ClientID %>");
                        break;
                    case 'gvFRec':
                        tblData = document.getElementById("<%=this.gvFRec.ClientID %>");
                        break;
                    case 'gvSecRec':
                        tblData = document.getElementById("<%=this.gvSecRec.ClientID %>");
                        break;
                    case 'gvThRec':
                        tblData = document.getElementById("<%=this.gvThRec.ClientID %>");
                        break;
                    case 'gvRateApp':
                        tblData = document.getElementById("<%=this.gvRateApp.ClientID %>");
                        break;
                    case 'gvOrdeProc':
                        tblData = document.getElementById("<%=this.gvOrdeProc.ClientID %>");
                        break;
                    case 'gvWrkOrd':
                        tblData = document.getElementById("<%=this.gvWrkOrd.ClientID %>");
                        break;
                    case 'gvordfapp':
                        tblData = document.getElementById("<%=this.gvordfapp.ClientID %>");
                        break;
                    case 'gvordsapp':
                        tblData = document.getElementById("<%=this.gvordsapp.ClientID %>");
                        break;
                    case 'grvMRec':
                        tblData = document.getElementById("<%=this.grvMRec.ClientID %>");
                        break;
                    case 'gvmrrapp':
                        tblData = document.getElementById("<%=this.gvmrrapp.ClientID %>");
                        break;

                    case 'gvPurBill':
                        tblData = document.getElementById("<%=this.gvPurBill.ClientID %>");
                        break;
                    default:
                        tblData = document.getElementById("<%=gvReqInfo.ClientID %>");

                        break;



                }


                var rowData;
                for (var i = 0; i < tblData.rows.length; i++) {
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

            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });



        function pageLoaded() {

            try {


                var comcod = <%=this.GetCompCode()%>;

                switch (comcod) {

                    case 3338:   // ACME   
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();
                        //  case 3101:   //ASIT
                        break;


                    case 3335:   // Edison  
                        // case 3101:   //ASIT
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();
                        break;

                    case 3355:   // Green Wood  
                        // case 3101:   //ASIT
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval                      
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide()
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(12)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();
                        break;



                    case 3354:  //Edison Real Estate

                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval                      
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide()
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(12)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();
                        break;





                    case 1205:  //P2P Construction
                    case 3351:  //wecon Properties
                    case 3352:  //p2p360
                        //case 3101:   //ASIT
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval                      
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide()
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(12)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();
                        break;






                    case 1103:   //Tanvir
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();
                        break;


                    case 3340://Urban
                        // $(".tbMenuWrp table tr td:nth-child(15)").show();
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();

                        break;


                    //case 3101://Credence
                    case 3348://Credence
                        // $(".tbMenuWrp table tr td:nth-child(15)").show();
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();

                        break;

                    //case 3101:
                    case 1108:
                    case 1109:
                    case 3316://Assure
                    case 3315://Assure
                    case 3317://Assure
                        //case 3101://Assure
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();

                        break;

                    case 3368:  //Finlay
                    //case 3101:
                 

                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check  
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval                      
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide()
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();  //Work Order(1st Appr)
                        $(".tbMenuWrp table tr td:nth-child(14)").hide(); //Work Order(2nd Appr)
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();


                        break;

                    case 3366:


                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check                    
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();//Received Approval
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();
                        $(".tbMenuWrp table tr td:nth-child(20)").hide();
                        $(".tbMenuWrp table tr td:nth-child(21)").hide();
                        break;

                    case 3367:  //Epic  
                   // case 3101:  //Epic

                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check      
                        //$(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();//Received Approval
                        // $(".tbMenuWrp table tr td:nth-child(18)").hide(); 
                        break;

                    default:
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();//CRM Check                    
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();//1st Approval
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();//2nd Approval
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(16)").hide();//Received Approval
                        $(".tbMenuWrp table tr td:nth-child(18)").hide();
                        //Added 
                        // $(".tbMenuWrp table tr td:nth-child(16)").hide();

                        break;



                }



                var gv1 = $('#<%=this.gvReqInfo.ClientID %>');
                var gv2 = $('#<%=this.gvReqChk.ClientID%>');
                var gv3 = $('#<%=this.gvreqfapproved.ClientID %>');
                var gv4 = $('#<%=this.gvRatePro.ClientID%>');
                var gv5 = $('#<%=this.gvFRec.ClientID %>');
                var gv6 = $('#<%=this.gvSecRec.ClientID%>');
                var gv7 = $('#<%=this.gvThRec.ClientID %>');
                var gv8 = $('#<%=this.gvRateApp.ClientID%>');
                var gv9 = $('#<%=this.gvOrdeProc.ClientID %>');
                var gv10 = $('#<%=this.gvWrkOrd.ClientID%>');
                var gv11 = $('#<%=this.gvordfapp.ClientID %>');
                var gv12 = $('#<%=this.gvordsapp.ClientID%>');
                var gv13 = $('#<%=this.grvMRec.ClientID %>');
                var gvmapp = $('#<%=this.gvmrrapp.ClientID %>');
                var gv14 = $('#<%=this.gvPurBill.ClientID%>');
                var gv15 = $('#<%=this.gvreqsecapproved.ClientID %>');


                gv1.Scrollable();
                gv2.Scrollable();
                gv3.Scrollable();
                gv4.Scrollable();
                gv5.Scrollable();
                gv6.Scrollable();
                gv7.Scrollable();
                gv8.Scrollable();
                gv9.Scrollable();
                gv10.Scrollable();
                gv11.Scrollable();
                gv12.Scrollable();
                gv13.Scrollable();
                gvmapp.Scrollable();
                gv14.Scrollable();
                gv15.Scrollable();


                //$("input, select").bind("keydown", function (event) {
                //    var k1 = new KeyPress();
                //    k1.textBoxHandler(event);
                //});

              <%--  $('#<%=this.gvReqInfo.ClientID%>').tblScrollable();
                $('#<%=this.gvReqChk.ClientID%>').tblScrollable();
                $('#<%=this.gvRatePro.ClientID%>').tblScrollable();
                $('#<%=this.gvRateApp.ClientID%>').tblScrollable();
                $('#<%=this.gvOrdeProc.ClientID%>').tblScrollable();
                $('#<%=this.gvWrkOrd.ClientID%>').tblScrollable();
                $('#<%=this.grvMRec.ClientID%>').tblScrollable();
                $('#<%=this.gvPurBill.ClientID%>').tblScrollable();
                $('#<%=this.grvComp.ClientID%>').tblScrollable();--%>
            }

            catch (e) {

                alert(e.message);

            }


        };



        function FunPurchaseOrder(url) {
            window.open('' + url + '', '_blank');

        }

    </script>


    <%-- <asp:ObjectDataSource ID="source_session_online" runat="server" SelectMethod="session_online" TypeName="t_session" />--%>



    <%--<asp:Button ID="Button1" runat="server" Text="Refresh" OnClick="btn_refresh_Click" />--%>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>


    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
    </asp:Timer>







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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="FromDate">From</label>

                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" inputDateBox"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>




                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="todate">To </label>
                                <asp:TextBox ID="txttoDate" runat="server" CssClass="inputDateBox"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txttoDate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                            </div>
                        </div>





                        <div class="col-md-1">
                            <div class="form-group">



                                <asp:TextBox ID="txtmrfno" runat="server" CssClass="form-control" placeholder="MRF NO"></asp:TextBox>


                            </div>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton></li>
                            </div>
                        </div>


                        <div class="col-md-1">
                            <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Opera</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/StepofOperationNew?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Go Purchase</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink8" runat="server" Target="_blank" NavigateUrl="~/F_12_Inv/PurReqEntry?InputType=IndentEntry&prjcode=&genno=&comcod=" CssClass="dropdown-item" Style="padding: 0 10px">Indent Requsition</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=res" CssClass="dropdown-item" Style="padding: 0 10px">Indent Item List</asp:HyperLink>

                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="dropdown-item" Style="padding: 0 10px" OnClick="lnkInteface_Click">Interface</asp:LinkButton></li>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="dropdown-item" Style="padding: 0 10px" Visible="false" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                                                <asp:HyperLink ID="HyperLink10" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurInformation" CssClass="dropdown-item" Style="padding: 0 10px">Dashboard</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" NavigateUrl="~/F_12_Inv/PurTopSheetCashPur?Type=Entry&genno=" CssClass="dropdown-item" Style="padding: 0 10px">Top Sheet(Cash)</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink12" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=Supplier" CssClass="dropdown-item" Style="padding: 0 10px">Create Supplier</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink13" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurMktSurvey?Type=SurveyLink" CssClass="dropdown-item" Style="padding: 0 10px">Survey Link</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurMktSurvey?Type=MktSurvey" CssClass="dropdown-item" Style="padding: 0 10px">Comparative Statement</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink17" runat="server" Target="_blank" NavigateUrl="~//F_14_Pro/PurMktSurvey02?Type=CS" CssClass="dropdown-item" Style="padding: 0 10px">Comparative Statement-02</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink14" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=BgdBal&comcod=&Date1=&Date2=" CssClass="dropdown-item" Style="padding: 0 10px">Budget Tracking</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink15" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk&comcod=&Date1=&Date2=" CssClass="dropdown-item" Style="padding: 0 10px">Purchase Tracking</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink16" runat="server" Target="_blank" NavigateUrl="~/F_99_Allinterface/PurReportInterface" CssClass="dropdown-item" Style="padding: 0 10px">Reports</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2" id="MultCom" runat="server">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlCompany" CssClass="chzn-select form-control" Width="200px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" style="display: none;">

                            <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">Requisition Tracking</asp:Label>
                            <asp:TextBox runat="server" ID="txtTrack" AutoPostBack="true" CssClass="inputtextbox" OnTextChanged="txtTrack_TextChanged"></asp:TextBox>
                            <asp:Button ID="lblMIMEInfo" runat="server" CssClass="smLbl_to" Height="20px" Style="text-align: center; line-height: 20px; padding: 0; float: right;" Text="Search" />
                        </div>

                        <%-- <div class="col-md-1">
                            <div class="form-group">     

                                <asp:HyperLink ID="hlnkMktInterface" runat="server" Target="_blank" NavigateUrl="~/F_99_Allinterface/MKTProInterface?Type=Report" Visible="false" CssClass=" btn btn-warning">Mar. Interface</asp:HyperLink>
                            </div>
                        </div>--%>
                    </div>

                    <div class="row">
                        <asp:Panel ID="pnlInterf" runat="server">
                            <div id="slSt" class=" col-md-12 ServProdInfo">
                                <div class="panel with-nav-tabs panel-primary">
                                    <fieldset class="tabMenu">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0"></asp:ListItem>
                                                        <asp:ListItem Value="1"></asp:ListItem>

                                                        <asp:ListItem Value="2"></asp:ListItem>
                                                        <asp:ListItem Value="3"></asp:ListItem>

                                                        <asp:ListItem Value="4"></asp:ListItem>
                                                        <asp:ListItem Value="5"></asp:ListItem>
                                                        <asp:ListItem Value="6"></asp:ListItem>
                                                        <asp:ListItem Value="7"></asp:ListItem>
                                                        <asp:ListItem Value="8"></asp:ListItem>
                                                        <asp:ListItem Value="9"></asp:ListItem>

                                                        <asp:ListItem Value="10"></asp:ListItem>
                                                        <asp:ListItem Value="11"></asp:ListItem>
                                                        <asp:ListItem Value="12"></asp:ListItem>
                                                        <asp:ListItem Value="13"></asp:ListItem>
                                                        <asp:ListItem Value="14"></asp:ListItem>
                                                        <asp:ListItem Value="15"></asp:ListItem>
                                                        <%--Material Received(Approved)--%>
                                                        <asp:ListItem Value="16"></asp:ListItem>
                                                        <asp:ListItem Value="17"></asp:ListItem>
                                                        <asp:ListItem Value="18"></asp:ListItem>
                                                        <asp:ListItem Value="19"></asp:ListItem>
                                                        <asp:ListItem Value="20"></asp:ListItem>


                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div>
                                        <asp:Panel ID="pnlReqInfo" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvReqInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvReqInfo_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">

                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnum" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" onkeyup="Search_Gridview(this,4,'gvReqInfo')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="hlnkgvgvmrfno" runat="server" BorderStyle="none"
                                                                        Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">

                                      
                                                                    </asp:HyperLink>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="420px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Dept Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptcode" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>






                                                            <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false" CssClass="btn  btn-default btn-xs"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-warning btn-xs"><span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>


                                                                    <%--<asp:LinkButton ID="btnDelOrder" runat="server" CssClass="btn btn-info btn-xs"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp--%>

                                                            <%--<asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ToolTip="Edit And Approve" ID="BtnEdit" CssClass="btn btn-xs btn-success" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                  <asp:HyperLink ID="HypRDDoPrint" runat="server" Target="_blank"  CssClass="btn btn-xs btn-danger"><span class="fa fa-print"></span>
                                                                        </asp:HyperLink>
                                                <asp:LinkButton ID="LbtnDelete" CssClass="btn btn-warning btn-xs" OnClick="LbtnDelete_Click" OnClientClick="return confirm('Do You want to delete this item?');" runat="server"><span class="glyphicon glyphicon-remove"></span></asp:LinkButton>
                                            </ItemTemplate>                                  
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlCRM" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvCRM" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvCRM_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnorq" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1rq" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumrchq" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4,'gvReqChk')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--  <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchCataReqChk" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvReqChk')"></asp:TextBox>
                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagorychk" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label7" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnCrmDelReq" OnClick="btnCrmDelReq_Click" CssClass="btn btn-default  btn-xs" ToolTip="Cancel" OnClientClick="javascript:return FunConfirm();" runat="server"><span  style="color:red"  class="fa   fa-recycle "></span> </asp:LinkButton>

                                                                    <%-- <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphlabelicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </asp:Panel>
                                        <asp:Panel ID="PnlReqChq" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvReqChk" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvReqChk_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label9" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label11" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumrchq" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4,'gvReqChk')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--  <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label14" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="TextBox2" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvReqChk')"></asp:TextBox>
                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label17" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label18" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReq" OnClick="btnDelReq_Click" CssClass="btn btn-default  btn-xs" ToolTip="Cancel" OnClientClick="javascript:return FunConfirm();" runat="server"><span  style="color:red"  class="fa   fa-recycle "></span> </asp:LinkButton>

                                                                    <%-- <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphlabelicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                    <asp:LinkButton ID="btnDirecdelReq" Visible="false" OnClick="btnDirecdelReq_Click" CssClass="btn btn-default  btn-xs" ToolTip="Delete" OnClientClick="javascript:return FunConfirm();" runat="server"><span  style="color:red"  class="fa fa-trash-alt "></span> </asp:LinkButton>
                                                                </ItemTemplate>



                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="167px" VerticalAlign="Top" />
                                                            </asp:TemplateField>





                                                            <asp:TemplateField HeaderText="CCD Status" Visible="false">
                                                                <ItemTemplate>


                                                                    <asp:Label ID="lblgvApamtf" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "iscrchecked"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                            </asp:TemplateField>



                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </asp:Panel>


                                        <asp:Panel ID="Pnlfirstapp" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvreqfapproved" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvreqfapproved_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNofapproved" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnorqfapproved" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescfapproved" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqratfapproved" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1rqfapproved" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumfapproved" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4,'gvreqfapproved')"></asp:TextBox><br />

                                                                </HeaderTemplate>


                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnofapproved" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--  <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodefapproved" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchCatagoryReqFApp" runat="server" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvreqfapproved')"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagoryfapproved" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountfapproved" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamtfapproved" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label19" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <%-- <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>--%>

                                                                    <asp:HyperLink ID="hlnkbtnEntryfapproved" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReqfapproved" OnClick="btnDelfapproved_Click" CssClass="btn btn-default btn-xs" OnClientClick="javascript:return FunConfirm();" runat="server"><span  style="color:red"  class="fa  fa-recycle "></span> </asp:LinkButton>

                                                                    <%-- <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </asp:Panel>



                                        <asp:Panel ID="Pnlsecapp" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvreqsecapproved" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvreqsecapproved_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNosecapproved" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnorqsecapproved" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescsecapproved" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqratsecapproved" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1rqsecapproved" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumsecapproved" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4,'gvreqsecapproved')"></asp:TextBox><br />

                                                                </HeaderTemplate>


                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnosecapproved" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--  <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodesecapproved" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchCatagoryReqSApp" runat="server" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvreqsecapproved')"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label20" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountsecapproved" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamtsecapproved" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label21" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <%-- <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>--%>

                                                                    <asp:HyperLink ID="hlnkbtnEntrysecapproved" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReqsecapproved" OnClick="btnDelReqsecapproved_Click" CssClass="btn btn-default btn-xs" OnClientClick="javascript:return FunConfirm();" runat="server"><span  style="color:red"  class="fa  fa-recycle "></span> </asp:LinkButton>

                                                                    <%-- <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </asp:Panel>


                                        <asp:Panel ID="pnlRatePro" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvRatePro" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvRatePro_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label22" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnocheck" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label23" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label24" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="1st App. <br>  Date" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgv1stapdate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "fappdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="2nd App. <br>  Date" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgv2ndapdate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sappdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1check" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">


                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumratepro" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4,'gvRatePro')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label25" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--  <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label26" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchCatagoryRatPro" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvRatePro')"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagory" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label27" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label28" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label29" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>


                                                            <asp:TemplateField HeaderText="MSR No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvMsrno2" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno"))%>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" ToolTip="Print Req Info" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="HyInprPrintCS" runat="server" ToolTip="Print CS" Target="_blank" CssClass="btn btn-default btn-xs" Visible="false"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelReqCheck" OnClick="btnDelReqCheck_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="140px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="140px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="pnlfRec" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvFRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvFRec_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNofrec" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnofrec" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescfrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqratfrec" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1frec" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumfrec" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4,'gvFRec')"></asp:TextBox><br />

                                                                </HeaderTemplate>



                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnofrec" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodefrec" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagoryfrec" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountfrec" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamtfrec" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label30" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrintfrec" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntryfrec" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReqfrec" OnClick="btnDelReqfrec_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>
                                                                    <%--   <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                        </asp:Panel>


                                        <asp:Panel ID="pnlSecRec" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvSecRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvSecRec_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNosrec" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnosrec" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescsrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqratsrec" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1srec" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumsrec" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4,'gvSecRec')"></asp:TextBox><br />

                                                                </HeaderTemplate>



                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnosrec" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodesrec" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagorysrec" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountsrec" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamtsrec" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label31" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrintsrec" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa  fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntrysrec" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReqsrec" OnClick="btnDelReqsrec_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class=" fa fa-recyle"></span> </asp:LinkButton>
                                                                    <%--   <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                        </asp:Panel>


                                        <asp:Panel ID="pnlThRec" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvThRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvThRec_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNothrec" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnothrec" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescthrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqratthrec" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1threc" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumthrec" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4, 'gvThRec')"></asp:TextBox><br />

                                                                </HeaderTemplate>



                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnothrec" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodethrec" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagorythrec" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountthrec" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamtthrec" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label32" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrintthrec" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntrythrec" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReqthrec" OnClick="btnDelReqthrec_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                    <%--   <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                        </asp:Panel>



                                        <asp:Panel ID="pnlRateApp" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvRateApp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvRateApp_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label33" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnorapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label34" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label35" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1rapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnuml" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." ToolTip="Search" onkeyup="Search_Gridview(this,4,'gvRateApp')"></asp:TextBox><br />

                                                                </HeaderTemplate>



                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label36" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label37" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchCatagoryRatApp" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvRateApp')"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagoryapp" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label38" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label39" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label40" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="MSR No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvMsrno" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno"))%>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" ToolTip="Print Req Info" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="HyInprPrintCS" runat="server" ToolTip="Print CS " Target="_blank" CssClass="btn btn-default btn-xs" Visible="false"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReqRateApp" OnClick="btnDelReqRateApp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>
                                                                    <%--   <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="140px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="140px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                        </asp:Panel>

                                        <asp:Panel ID="PanelOrProc" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvOrdeProc" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvOrdeProc_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label41" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label42" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label43" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label44" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label45" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumordpro" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,4, 'gvOrdeProc')"></asp:TextBox><br />

                                                                </HeaderTemplate>


                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label46" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label47" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchCatagoryOrdPro" runat="server" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvOrdeProc')"></asp:TextBox>
                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagorypurpro" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label48" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label49" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFOrProAmt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprFAppPrint" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReqApproval_Click" OnClick="btnDelReqApproval_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>

                                                                    <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PaneWorder" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvWrkOrd" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvWrkOrd_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label50" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label51" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="aprovno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaprovno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label52" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsupplier" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Approved <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvaprovdat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Approved No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaprovno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label53" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">

                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumporder" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,6, 'gvWrkOrd')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label54" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label55" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label56" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvWoamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "woamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFWoamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>

                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelAprovedNo_Click" OnClick="btnDelAprovedNo_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                    <%-- <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkorder" runat="server"
                                                                        Width="20px" />
                                                                </ItemTemplate>

                                                                <FooterTemplate>

                                                                    <asp:LinkButton ID="lnkbtnOrder" runat="server" OnClientClick="return FunOrdConfirm();"
                                                                        OnClick="lnkbtnOrder_Click" ToolTip="Order"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                                                    <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="ssircode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvssircode" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlorderfapp" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvordfapp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvordfapp_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNoofapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnoofapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvordernoofapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescofapp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsupplierofapp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Approved <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvaprovdat1ofapp" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno1ofapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1ofapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumofap" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,6,'gvordfapp')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnoofapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodeofapp" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountofapp" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvWoamtofapp" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "woamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFWoamtofapp" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrintofapp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntryofapp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnofapp" OnClick="btnofapp_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>
                                                                    <%-- <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>--%>


                                                                    <asp:LinkButton ID="btnDelOrderAprv" runat="server" OnClick="btnDelOrderAprv_Click" OnClientClick="javascript:return FunConfirm();" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlordersapp" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvordsapp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvordsapp_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNoosapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnoosapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvordernosapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescosapp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsupplierosapp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Order <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvaprovdat1osapp" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvordernoosapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1osapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">

                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumosapp" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,6,'gvordsapp')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnoosapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodeosapp" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountosapp" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvWoamtosapp" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "woamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="50px"></asp:Label>
                                                                    <%--Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")))=="3101" || (Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")))=="3355" ? true : false %>'--%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFWoamtosapp" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrintosappReq" runat="server" Target="_blank" ToolTip="Print Req Info" CssClass="btn btn-default btn-xs"> <span  style="color:green" class="fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="HyInprPrintosapp" runat="server" Target="_blank" ToolTip="Print Order Approval" CssClass="btn btn-default btn-xs"><span style="color:deepskyblue" class="fa fa-print"></span>
                                                                    </asp:HyperLink>



                                                                    <asp:HyperLink ID="lnkbtnEntryosapp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <%--<asp:LinkButton ID="btnosapp" OnClick="btnosapp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                    <asp:HyperLink ID="HyperLink25" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnosapp" OnClick="btnosapp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="150px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnoapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelRecv" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="grvMRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="grvMRec_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label57" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label58" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="aprovno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaprovno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label59" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsuppliermrr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvorderdat" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label60" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">

                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnummrec" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,6,'grvMRec')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label61" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label62" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label63" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label64" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFWoamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Upto Received">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreceivedamtor" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFreceivedamtor" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Balance Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvbalamtor" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFbalamtor" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>



                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                     <asp:HyperLink ID="lnkbtnPOEdit" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-edit"></span>

                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" ToolTip="Print Crystal" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="HyperLink2" runat="server" ToolTip="Print RDLC" Target="_blank" CssClass="btn btn-default btn-xs" Visible="true"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>
                                                                   

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelOrder" OnClick="btnDelOrder_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>


                                                                    <asp:LinkButton ID="lbtnSendMail" OnClick="lbtnSendMail_Click" ToolTip="Send mail" OnClientClick="javascript:return FunConfirmMail() ;" runat="server" CssClass="btn btn-default btn-xs"><span style="color:black" class=" fa fa-mail-bulk"></span> </asp:LinkButton>


                                                                    <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="180px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="180px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlmrrapp" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvmrrapp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvmrrapp_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNomapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnomapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrnomapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvordernomapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchProNamemapp" runat="server" BackColor="Transparent" BorderStyle="None" SortExpression="project name" Width="180px" placeholder="Project Name" onkeyup="Search_Gridview(this,1,'gvmrrapp')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescmapp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchSuppliermapp" runat="server" BackColor="Transparent" BorderStyle="None" SortExpression="supplier" Width="150px" placeholder="Supplier" onkeyup="Search_Gridview(this,2,'gvmrrapp')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsuppliermapp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Recevied <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvmrrdatmapp" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRR No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrno1mapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="MRR Ref">


                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrrefmapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno1mapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1mapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnummapp" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,8,'gvmrrapp')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnomapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>





                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodemapp" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountmapp" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrramtmapp" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFmrramtmapp" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrintmapp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntrymapp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>
                                                                    <asp:LinkButton ID="btnDelBillmapp" OnClick="btnDelBillmapp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                    <%--                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>
                                                    

                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="PanelBill" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvPurBill" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvPurBill_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label66" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label67" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label68" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchProNameBill" runat="server" BackColor="Transparent" BorderStyle="None" SortExpression="project name" Width="180px" placeholder="Project Name" onkeyup="Search_Gridview(this,1,'gvPurBill')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label69" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchSupplierBill" runat="server" BackColor="Transparent" BorderStyle="None" SortExpression="supplier" Width="150px" placeholder="Supplier" onkeyup="Search_Gridview(this,2,'gvPurBill')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsupplierbill" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Recevied <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvmrrdat" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRR No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="MRR Ref">


                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrref" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label70" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label71" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumbill" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,8,'gvPurBill')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label72" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label73" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label74" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrramt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFmrramt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>


                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>
                                                                    <asp:LinkButton ID="btnDelBill" OnClick="btnDelBill_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                    <%--                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>
                                                    

                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelComp" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="grvComp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="grvComp_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label75" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label76" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label77" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label78" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label79" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bill <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label80" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRR No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label81" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label82" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label83" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label84" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--     <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label85" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label86" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvbillamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label87" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyperLink31" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="HyperLink32" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <%--<asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelBillAudit" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvPurBillAudit" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvPurBillAudit_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNoaud" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnoaud" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrnoaud" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvordernoaud" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescaud" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsupplierbillaud" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bill No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvbillnonoaud" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Bill No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvbillnono1aud" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Bill Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvmrrdataud" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="MRR No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrno1aud" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Bill Ref">


                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrrefaud" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno1aud" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1aud" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <%--   <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumbill" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,8,'gvPurBill')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>

                                                            <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodeaud" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountaud" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrramtaud" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="Label88" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyperLink33" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntryBillAudit" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>
                                                                    <asp:LinkButton ID="btnDelBillAudit" OnClick="btnDelBillAudit_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle" ></span> </asp:LinkButton>
                                                                    <%--                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>
                                                    

                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="fa fa-recyle"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="130px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="130px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelInd" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvInd" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvInd_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">

                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnum" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" onkeyup="Search_Gridview(this,4,'gvReqInfo')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="hlnkgvgvmrfno" runat="server" BorderStyle="none"
                                                                        Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">

                                      
                                                                    </asp:HyperLink>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Dept Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptcode" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>







                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <%-- <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false" CssClass="btn  btn-default btn-xs"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>--%>

                                                                    <asp:HyperLink ID="lnkbtnInd" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>

                                                                    <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-warning btn-xs"><span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PanelIndAp" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvIndAp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvIndAp_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueno1"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptdesc" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issue <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvissuedat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuedat1")) %>'
                                                                        Width="70px"></asp:Label>
                                                                    <%--<asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>--%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Issue. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="">

                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnum" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" onkeyup="Search_Gridview(this,4,'gvReqInfo')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="hlnkgvgvmrfno" runat="server" BorderStyle="none"
                                                                        Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">

                                      
                                                                    </asp:HyperLink>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Dept Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldeptcode" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>







                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkbtnAppIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>

                                                                  

                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                </div>

                            </div>

                        </asp:Panel>


                        <asp:Panel ID="pnlPurchase" runat="server" Visible="False">
                            <div class="form-group">

                                <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                    <ul class="nav colMid " id="SERV">
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=DaywPur")%> " target="_blank">01. Day Wise Purchase</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=PurSum")%> " target="_blank">02. Purchase Summary</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptMatPurHistory")%> " target="_blank">03. Purchase History-Materials Wise</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=IndSup")%> " target="_blank">04. Purchase History-Supplier Wise</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptWorkOrderVsSupply?Type=OrdVsSup")%> " target="_blank">05. Work Order-Supplier Wise</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk")%> " target="_blank">06. Purchase Tracking</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptWorkOrderVsSupply?Type=OrderTk")%> " target="_blank">07. Order Tracking</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=PurBilltk")%> " target="_blank">08. Bill Tracking</a>

                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptDateWiseReq?Type=PendingStatus")%> " target="_blank">09. Pending Status</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_15_Acc/RptAccSpLedger?Type=ASPayment")%> " target="_blank">10. Supplier Overall Position</a>
                                        </li>





                                    </ul>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </asp:Panel>

                    </div>


                </div>

            </div>






            <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="lblMIMEInfo"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; margin: 10px; height: 500px">

                <div>

                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="myModalLabel">Purchase History</h4>
                            </div>
                            <div class="modal-body">

                                <asp:GridView ID="gvPurstk01" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass=" table-responsive  table-hover table-bordered grvContentarea">

                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAppRate01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                                <asp:Button ID="btnClose" runat="server" CssClass="btn btn-danger btn-sm pull-right" Text="Close" />



                            </div>
                        </div>
                    </div>

                </div>

            </asp:Panel>
            <!-- ModalPopupExtender -->



            </div>





            </div>


                     

        </ContentTemplate>



    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>
