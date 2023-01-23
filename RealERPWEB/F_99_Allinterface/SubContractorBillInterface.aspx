<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SubContractorBillInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.SubContractorBillInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
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
            width: 100px;
        }

        .tbMenuWrp table tr td {
            /*height: 50px;*/
            width: 95px;
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
            font-size: 11px;
            height: 36px;
            margin: -2px auto -22px;
            padding: 8px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 36px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 18px;
            border-radius: 0px 15px;
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

    <script src="../Scripts/waypoints.min.js"></script>
    <script src="../Scripts/jquery.counterup.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            pageLoaded();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {
                funComRadiButtonHidden();
                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });
                $('#<%=this.gvexecution.ClientID%>').tblScrollable();
                $('#<%=this.gvsubbill.ClientID%>').tblScrollable();
                $('#<%=this.gvfinal.ClientID%>').tblScrollable();
                $('#<%=this.gvAllReq.ClientID%>').tblScrollable();
                $('#<%=this.grvImple.ClientID%>').tblScrollable();
                $('#<%=this.gvlabbillreq.ClientID%>').tblScrollable();
                $('#<%=this.gvbillapp.ClientID%>').tblScrollable();
                $('#<%=this.gvsrec.ClientID%>').tblScrollable();
                $('#<%=this.gvfrec.ClientID%>').tblScrollable();
                $('#<%=this.gvthrec.ClientID%>').tblScrollable();
                $('#<%=this.gvfinalapp.ClientID%>').tblScrollable();
                $('#<%=this.gvConUpdat.ClientID%>').tblScrollable();
            }
            catch (e) {
                alert(e);
            }
        };

        function funComRadiButtonHidden() {
            try {
                comcod = <%=this.GetCompCode()%>;
                switch (comcod) {

                    
                    case 1205:   //p2p
                    case 3351:   //p2p
                    case 3352:   //p2p
                        //case 3101:   //p2p
                        //case 3355:   //greenwood
                        $(".tbMenuWrp table tr td:nth-child(3)").show();
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(11)").hide(); // 9 - for billApproval
                       // $(".tbMenuWrp table tr td:nth-child(10)").hide(); // 9 - for billApproval Emdad22.12.2022
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(15)").hide();
                        $('#<%=this.txtrefno.ClientID%>').prop('readonly', false);
                        break;

                    case 3101://ASIT
                    case 3370:   //cpdl
                    case 3368: // Finlay
                        $(".tbMenuWrp table tr td:nth-child(1)").hide();
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();
                        $(".tbMenuWrp table tr td:nth-child(3)").show();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(15)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(9)").hide(); // 9 - for billApprova
                        $('#<%=this.txtrefno.ClientID%>').prop('readonly', true);
                        break;


                    case 1103:   //tanvir

                        $(".tbMenuWrp table tr td:nth-child(4)").hide();
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();
                        $(".tbMenuWrp table tr td:nth-child(6)").hide();
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(11)").hide(); // 9 - for billApproval
                        break;


<%--                    case 3368: // Finlay
                        //$(".tbMenuWrp table tr td:nth-child(3)").hide();
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();
                        $(".tbMenuWrp table tr td:nth-child(6)").hide();
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(15)").hide();
                        $('#<%=this.txtrefno.ClientID%>').prop('readonly', true);
                        break;--%>


                    case 3367:
                        $(".tbMenuWrp table tr td:nth-child(3)").hide();
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();
                        $(".tbMenuWrp table tr td:nth-child(6)").hide();
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(15)").hide();
                        break;

                    //case 3101:
                    case 3366:
                        $(".tbMenuWrp table tr td:nth-child(1)").hide();
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();
                        $(".tbMenuWrp table tr td:nth-child(3)").hide();
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();
                        $(".tbMenuWrp table tr td:nth-child(6)").hide();
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(11)").hide(); // 9 - for billApproval
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(15)").hide();
                        break;


                    default:
                        $(".tbMenuWrp table tr td:nth-child(3)").hide();
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();
                        $(".tbMenuWrp table tr td:nth-child(6)").hide();
                        $(".tbMenuWrp table tr td:nth-child(7)").hide();
                        $(".tbMenuWrp table tr td:nth-child(8)").hide();
                        $(".tbMenuWrp table tr td:nth-child(9)").hide();
                        $(".tbMenuWrp table tr td:nth-child(11)").hide(); // 9 - for billApproval
                        $(".tbMenuWrp table tr td:nth-child(13)").hide();
                        $(".tbMenuWrp table tr td:nth-child(14)").hide();
                        $(".tbMenuWrp table tr td:nth-child(15)").hide();
                        break;
                }
            }
            catch (e) {
                alert(e);
            }
        }
        function Search_Gridview(strKey, cellNr, gvname) {
            try {
                var strData = strKey.value.toLowerCase().split(" ");
                var tbldata;
                switch (gvname) {

                    case 'gvAllReq':
                        tblData = document.getElementById("<%=this.gvAllReq.ClientID %>");
                        break;
                    case 'gvlabbillreq':
                        tblData = document.getElementById("<%=this.gvlabbillreq.ClientID %>");
                        break;
                    case 'gvsubbill':
                        tblData = document.getElementById("<%=this.gvsubbill.ClientID %>");
                        break;
                    case 'gvbillapp':
                        tblData = document.getElementById("<%=this.gvbillapp.ClientID %>");
                        break;
                    case 'gvfinal':
                        tblData = document.getElementById("<%=this.gvfinal.ClientID %>");
                        break;
                    case 'gvsrec':
                        tblData = document.getElementById("<%=this.gvsrec.ClientID %>");
                        break;
                    case 'gvfrec':
                        tblData = document.getElementById("<%=this.gvfrec.ClientID %>");
                        break;
                    case 'gvthrec':
                        tblData = document.getElementById("<%=this.gvthrec.ClientID %>");
                        break;
                    case 'gvfinalapp':
                        tblData = document.getElementById("<%=this.gvfinalapp.ClientID %>");
                        break;
                    case 'gvConUpdat':
                        tblData = document.getElementById("<%=this.gvConUpdat.ClientID %>");
                        break;
                    case 'gvbillcs':
                        tblData = document.getElementById("<%=this.gvbillcs.ClientID %>");
                        break;
                    case 'gvWorkOrder':
                        tblData = document.getElementById("<%=this.gvWorkOrder.ClientID %>");
                        break;
                    case 'gvmbookapp':
                        tblData = document.getElementById("<%=this.gvmbookapp.ClientID %>");
                        break;
                    case 'gvmbook':
                        tblData = document.getElementById("<%=this.gvmbook.ClientID %>");
                        break;
                    case 'gvReadyForBill':
                        tblData = document.getElementById("<%=this.gvReadyForBill.ClientID %>");
                        break;
                    default:
                        tblData = document.getElementById("<%=gvsubbill.ClientID %>");
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
    </script>

    <%-- <asp:ObjectDataSource ID="source_session_online" runat="server" SelectMethod="session_online" TypeName="t_session" />--%>



    <%--<asp:Button ID="Button1" runat="server" Text="Refresh" OnClick="btn_refresh_Click" />--%>

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

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



            <%-- <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="5000">
            </asp:Timer>--%>

            <%-- <triggers>
 
                   <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>--%>
            <%-- </ContentTemplate>

    </asp:UpdatePanel>--%>


            <%--            <div class="container moduleItemWrpper">
                <div class="contentPart">--%>
            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="txtdate">Date</label>

                                <asp:TextBox ID="txtdate" runat="server" CssClass="inputDateBox" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control" placeholder="Ref No..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink16" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=Res" CssClass="dropdown-item" Style="padding: 0 15px">Resouce Code</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurSupplierinfo?Type=Entry" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Information</asp:HyperLink>
                                        <asp:HyperLink ID="hlnksubconpayslip" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/RptSubConPaySlip" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Payment Slip</asp:HyperLink>
                                        <asp:HyperLink ID="hlnksubconbundle" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurBillBundle?Type=ContEntry" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Bundle Entry </asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink12" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubConOverall02" CssClass="dropdown-item" Style="padding: 0 15px">Sub Contractor Budget </asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink34" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurSubConBillFinal?Type=BillEntry&prjcode=&genno=&sircode=" CssClass="dropdown-item" Style="padding: 0 15px">Top Sheet</asp:HyperLink>
                                    </div>
                                </div>

                            </div>


                        </div>
                        <div class="col-md-2">
                            <asp:Panel ID="pnlAll" runat="server" Visible="false">
                                <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                    <button type="button" class="btn btn-success">Entry</button>
                                    <div class="btn-group" role="group">
                                        <button id="btnGroupDrop5" type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop5" style="">
                                            <div class="dropdown-arrow"></div>
                                            <asp:HyperLink ID="HyperLink35" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/ImplementPlan" CssClass="dropdown-item" Style="padding: 0 15px">Monthly Plan</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink36" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurIssueEntry?Type=Report&prjcode=" CssClass="dropdown-item" Style="padding: 0 15px">Work Execution-Category Wise</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink37" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurIssueWorkWiseEntry" CssClass="dropdown-item" Style="padding: 0 15px">Work Execution-Work Wise</asp:HyperLink>
                                            <%--<asp:HyperLink ID="HyperLink38" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurConWrkOrderEntry02?Type=Entry" CssClass="dropdown-item" Style="padding: 0 15px">Work Order </asp:HyperLink>--%>
                                            <asp:HyperLink ID="hlnkworkorder" runat="server" Target="_blank" CssClass="dropdown-item" Style="padding: 0 15px">Work Order </asp:HyperLink>


                                            <asp:HyperLink ID="HyperLink39" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurLabIssue?Type=Current&prjcode=&genno=&sircode=" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Bill-Floor Wise </asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink40" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurLabIssue2?Type=Current&prjcode=&genno=&sircode=" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Bill-Work Wise</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurLabRequisition?Type=Entry&prjcode=&genno=&sircode=" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Bill Requisition(Floor Wise)</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink8" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurLabRequisition02?Type=Entry&prjcode=&genno=&sircode=" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Bill Requisition(Work Wise)</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurMktSurvey02?Type=CS" CssClass="dropdown-item" Style="padding: 0 15px">Comparative Statement - Purchase 02</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurMktSurveyCont?Type=ConCS&lisuno=&pactcode=" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Comparative Statement</asp:HyperLink>
                                        </div>
                                    </div>

                                </div>

                            </asp:Panel>


                            <asp:Panel ID="pnltan" runat="server" Visible="false">

                                <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                    <button type="button" class="btn btn-success">Entry</button>
                                    <div class="btn-group" role="group">
                                        <button id="btnGroupDrop7" type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop7" style="">
                                            <div class="dropdown-arrow"></div>
                                            <asp:HyperLink ID="HyperLink23" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/ImplementPlan" CssClass="dropdown-item" Style="padding: 0 15px">Monthly Plan</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink24" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurIssueEntry?Type=Report&prjcode=" CssClass="dropdown-item" Style="padding: 0 15px">Work Execution -Category Wise</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink25" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurConWrkOrderEntry02?Type=Entry" CssClass="dropdown-item" Style="padding: 0 15px">Work Order</asp:HyperLink>
                                            <%--                                            <asp:HyperLink ID="HyperLink26" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurLabIssue2?Type=Current" CssClass="dropdown-item" Style="padding: 0 15px">>Sub-Contractor Bill-Work Wise</asp:HyperLink>--%>
                                            <asp:HyperLink ID="HyperLink27" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurLabIssue?Type=Current&prjcode=&genno=&sircode=" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Bill-Category Wise </asp:HyperLink>
                                            <%--                                            <asp:HyperLink ID="HyperLink28" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurLabIssue2?Type=Current&prjcode=&genno=&sircode=" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Bill-Work Wise</asp:HyperLink>--%>
                                        </div>
                                    </div>

                                </div>



                            </asp:Panel>



                        </div>
                        <div class="col-md-2">
                            <asp:Panel ID="Panelsuvastu" runat="server" Visible="false">
                                <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                    <button type="button" class="btn btn-primary">Reports</button>
                                    <div class="btn-group" role="group">
                                        <button id="btnGroupDrop6" type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop6" style="">
                                            <div class="dropdown-arrow"></div>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubConOverall02" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Budget</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptConTractorBillAll" CssClass="dropdown-item" Style="padding: 0 15px">Sub-contractor Bill(All Work Wise)</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubContractorTopSheet" CssClass="dropdown-item" Style="padding: 0 15px">Sub_Contractor Top Sheet</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/SubConPrjWiseBilldetails" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor's Project Wise bill Report</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink13" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptProjWiseSubContractorBillReport" CssClass="dropdown-item" Style="padding: 0 15px">Project Wise Sub-Contractor's bill Report </asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink32" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubConBillStatus" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Bill Status</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink15" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptProjectWiseBillReport" CssClass="dropdown-item" Style="padding: 0 15px">Project Wise Bill</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink18" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/SubConPrjWiseBilldetails" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Wise Bill Report</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink19" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptPrjGrpWrkBillReport?Type=BillStatus" CssClass="dropdown-item" Style="padding: 0 15px">Group Wise Bill Report</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink20" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubConBillStatus" CssClass="dropdown-item" Style="padding: 0 15px">Project Wise Bill Quantity Report</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink21" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubconWiseWrkOrder" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Wise Work Order Report</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink22" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/SubConBillWiseReport" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Wise Bill Report</asp:HyperLink>

                                        </div>
                                    </div>

                                </div>

                            </asp:Panel>

                            <asp:Panel ID="Panelrpt" runat="server" Visible="false">
                                <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                    <button type="button" class="btn btn-primary">Reports</button>
                                    <div class="btn-group" role="group">
                                        <button id="btnGroupDrop8" type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop8" style="">
                                            <div class="dropdown-arrow"></div>
                                            <asp:HyperLink ID="HyperLink29" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptImpExeStatus?Type=PlanVSEx" CssClass="dropdown-item" Style="padding: 0 15px">Monthly Plan Vs Execution</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink30" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubConBill?Type=SubBill" CssClass="dropdown-item" Style="padding: 0 15px">Sub-contractor Bill</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink31" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubConBill?Type=SubConBill" CssClass="dropdown-item" Style="padding: 0 15px">Periodic Subcontractor Bill</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink33" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubContractorSd?Type=BillRAWise" CssClass="dropdown-item" Style="padding: 0 15px"> R/A Bill - Individual</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink41" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubContractorSd?Type=BillDetails" CssClass="dropdown-item" Style="padding: 0 15px"> R/A Bill ALL</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink42" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/RptAccSpLedger?Type=AConPayment" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Overall Position</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink43" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubContractorTopSheet" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Top Sheet</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink44" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/SubConPrjWiseBilldetails" CssClass="dropdown-item" Style="padding: 0 15px">Sub-Contractor Wise Bill Report</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink45" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptProjWiseSubContractorBillReport" CssClass="dropdown-item" Style="padding: 0 15px">Project Wise Bill Report</asp:HyperLink>


                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>



                        <%--<div class="col-md-108 pading5px ">

                                    <asp:HyperLink ID="HyperLink16" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=Res" CssClass="btn btn-success btn-sm" Style="padding: 0 15px">Resouce Code</asp:HyperLink>
                                    <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurSupplierinfo" CssClass="btn btn-warning btn-sm" Style="padding: 0 15px">Sub-Contractor Information</asp:HyperLink>
                                    <asp:HyperLink ID="hlnksubconpayslip" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/RptSubConPaySlip" CssClass="btn btn-success btn-sm" Style="padding: 0 15px">Sub-Contractor Payment Slip</asp:HyperLink>
                                    <asp:HyperLink ID="hlnksubconbundle" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurBillBundle?Type=ContEntry" CssClass="btn btn-warning btn-sm" Style="padding: 0 15px">Sub-Contractor Bundle Entry </asp:HyperLink>
                                    <asp:HyperLink ID="HyperLink12" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/RptSubConOverall02" CssClass="btn btn-success btn-sm" Style="padding: 0 15px">Sub Contractor Budget </asp:HyperLink>
                                    <asp:HyperLink ID="HyperLink34" runat="server" Target="_blank" NavigateUrl="~/F_09_PImp/PurSubConBillFinal?Type=BillEntry&prjcode=&genno=&sircode=" CssClass="btn btn-success btn-sm" Style="padding: 0 15px">Top Sheet</asp:HyperLink>




                                </div>--%>





                        <%--  </div>--%>
                        <%--  <div class="clearfix"></div>--%>
                        <%--</fieldset>--%>
                    </div>


                    <div class="row">

                        <div id="slSt" class=" col-md-12">
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
                                                    <%--bill Cs--%>
                                                    <asp:ListItem Value="4"></asp:ListItem>
                                                    <%--bill  cs Approve--%>
                                                    <asp:ListItem Value="5"></asp:ListItem>
                                                    <%--bill work Order--%>

                                                    <asp:ListItem Value="6"></asp:ListItem>
                                                    <%--mb book entry--%>
                                                    <asp:ListItem Value="7"></asp:ListItem>
                                                    <%--mb book Approval--%>
                                                    <asp:ListItem Value="8"></asp:ListItem>
                                                    <%--Ready for Bill--%>
                                                    <asp:ListItem Value="9"></asp:ListItem>
                                                    <asp:ListItem Value="10"></asp:ListItem>
                                                    <asp:ListItem Value="11"></asp:ListItem>
                                                    <asp:ListItem Value="12"></asp:ListItem>
                                                    <asp:ListItem Value="13"></asp:ListItem>
                                                    <asp:ListItem Value="14"></asp:ListItem>
                                                    <asp:ListItem Value="15"></asp:ListItem>
                                                    <asp:ListItem Value="16"></asp:ListItem>

                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <asp:Panel ID="PnlImp" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grvImple" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False"
                                            ShowFooter="True"
                                            OnPageIndexChanging="grvImple_PageIndexChanging">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Implement No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vouno")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Implement date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvComQty" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "impdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Work Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbalqty" runat="server" Font-Bold="False" Font-Size="12px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="right" />
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


                                </asp:Panel>
                                <asp:Panel ID="pnlgvupdate" runat="server" Visible="false">

                                    <asp:GridView ID="gvConUpdat" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvConUpdat_OnRowDataBound"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="false">
                                        <RowStyle />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAPPSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAPPcentrid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchproConUpdat" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvConUpdat')"></asp:TextBox><br />
                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPPgvactdesc" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgAPPcbillno1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                        Width="70px"></asp:Label>
                                                    <asp:Label ID="lgAPPcorderno" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="MB No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmbno1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mbno1")) %>'
                                                        Width="70px"></asp:Label>
                                                  
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPPgvDate" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPPgvcbillref" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cbillref")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contractor name">


                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchconConUpdat" SortExpression="sirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Sub-Contractor" onkeyup="Search_Gridview(this,5,'gvConUpdat')"></asp:TextBox><br />

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPPgvcsirdesc" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bundle No">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtbundno" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bundno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Bill Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFlcamt" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Security Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsdamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFsdamt" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Deduction Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdedamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFdedamt" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Penalty Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgpenamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "penamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFpenamt" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Reward">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReward" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reward")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFReward" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Net Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnetamt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFnetamt" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkbtnPrintBU" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>

                                                      

                                               
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                   

                                                        <asp:HyperLink ID="hlnkconBillDetaitls" runat="server" ToolTip="MB Details" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"
                                                        Visible='<%# (Convert.ToBoolean((Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) == "3370") || (Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) == "3101")) ? true : false)%>'>  
                                                         <i class=" fa fa-info-circle" aria-hidden="false"></i>
                                                    </asp:HyperLink>


                                               
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            


                                         





                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>





                                </asp:Panel>
                                <asp:Panel ID="PnlExe" Visible="false" runat="server">
                                    <div class="table-responsive col-lg-12" style="background: #fff;">
                                        <asp:GridView ID="gvexecution" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvexecution_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Project code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPactcode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                            Width="200px" Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPactdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                            Width="200px" Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue No" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblisuno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCuName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno1")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "isudat")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Issue Amount">

                                                    <ItemTemplate>


                                                        <asp:HyperLink ID="hlnkgvacamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                            Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px">
                                                        </asp:HyperLink>


                                                        <%--   <asp:hyperlink"lgIsuamt" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:hyperlink=>--%>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
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

                                </asp:Panel>
                                <asp:Panel ID="pnlSubbill" Visible="false" runat="server">
                                    <asp:GridView ID="gvsubbill" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvsubbill_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprocbill" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvsubbill')"></asp:TextBox><br />

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescs" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="160px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnuno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="R/A No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissueref" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisurefno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescs" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "isudat")).ToString("dd-MM-yyyy") %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sub-Contractor">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchconcbill" SortExpression="sirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Sub-Contractor" onkeyup="Search_Gridview(this,5,'gvsubbill')"></asp:TextBox><br />

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescsc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <%--  <asp:TemplateField HeaderText="Bill No">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcbillno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                               
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcamt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bundle No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblbundno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bundno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>



                                                    <asp:HyperLink ID="lnkbtnPrintIN" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>

                                                    <asp:LinkButton ID="btnDelOrder" runat="server" CssClass="btn btn-xs btn-default"><span style="color:red" class="fa fa-recycle "></span> </asp:LinkButton>
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






                                </asp:Panel>

                                <asp:Panel ID="pnlbillapp" Visible="false" runat="server">
                                    <asp:GridView ID="gvbillapp" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvbillapp_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillapp" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchproConUpdat" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvbillapp')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillappactdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillapplisuno1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="R/A No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillapplisurefno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisurefno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillappisudat" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "isudat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sub-Contractor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllisuno2" runat="server" Visible="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno")) %>'></asp:Label>
                                                    <asp:Label ID="lblissustatus2" runat="server" Visible="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issustatus")) %>'></asp:Label>
                                                    <asp:Label ID="lblgvbillappsirdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillappisuamt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <%--<asp:HyperLink ID="lnkbtnPrintIN" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>--%>
                                                    <asp:HyperLink ID="lnkbtnbillapp" runat="server" ToolTip="Bill Approval" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                    </asp:HyperLink>

                                                    <asp:HyperLink ID="hlnkBillDetails" runat="server" ToolTip="MB Details" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"
                                                        Visible='<%# (Convert.ToBoolean((Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) == "3370") || (Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) == "3101")) ? true : false)%>'>  
                                                         <i class=" fa fa-info-circle" aria-hidden="false"></i>
                                                    </asp:HyperLink>

                                                    <%-- <asp:HyperLink ID="lnkbtnEditBilll" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span style="color:black" class="fas fa-edit"></span>
                                                    </asp:HyperLink>--%>
                                                    <asp:LinkButton ID="btnDelbillapp" OnClick="btnDelbillapp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs" ToolTip="Delete Bill Checked">
                                                        <span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>


                                </asp:Panel>

                                <asp:Panel ID="pnlfrec" Visible="false" runat="server">
                                    <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                        <asp:GridView ID="gvfrec" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvfrec_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo5frec" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAPPcentridfrec" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtSearchprogvfrec" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvfrec')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAPPgvactdescfrec" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgAPPcbillno1frec" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                            Width="70px"></asp:Label>
                                                        <asp:Label ID="lgvbillnofrec" runat="server" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAPPgvDatefrec" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MM-yyyy") %>'
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bill ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAPPgvcbillreffrec" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cbillref")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contractor name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAPPgvcsirdescfrec" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                            Width="130px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Bill Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbillamtfrec" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFlcamtfrec" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Security Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsdamtfrec" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFsdamtfrec" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Deduction Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdedamtfrec" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFdedamtfrec" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>






                                                <asp:TemplateField HeaderText="Net Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvnetamtfrec" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFnetamtfrec" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkbtnPrintfrec" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>


                                                        <asp:HyperLink ID="lnkbtnfrec" runat="server" ToolTip="Add To Bill Finalization" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                        </asp:HyperLink>
                                                        <asp:HyperLink ID="lnkbtnEditfrec" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span style="color:black" class="fas fa-edit">
                                                        </asp:HyperLink>
                                                        <asp:LinkButton ID="btnDelfrec" OnClick="btnDelfrec_OnClick" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" />
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
                                </asp:Panel>


                                <asp:Panel ID="pnlsrec" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                            <asp:GridView ID="gvsrec" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvsrec_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo5srec" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvAPPcentridsrec" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchprogvsrec" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvsrec')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtAPPgvactdescsrec" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgAPPcbillno1srec" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                                Width="70px"></asp:Label>
                                                            <asp:Label ID="lgvbillnonosrec" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="txtAPPgvDatesrec" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MM-yyyy") %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtAPPgvcbillrefsrec" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cbillref")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contractor name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtAPPgvcsirdescsrec" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                                Width="130px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Bill Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbillamtsrec" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFlcamtsrec" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Security Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsdamtsrec" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFsdamtsrec" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Deduction Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdedamtsrec" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFdedamtsrec" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>







                                                    <asp:TemplateField HeaderText="Net Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvnetamtsrec" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFnetamtsrec" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnPrintsrec" runat="server" Target="_blank" CssClass="btn btn-xs btn-default"><span class=" fa fa-print"></span></asp:HyperLink>


                                                            <asp:HyperLink ID="lnkbtnsrec" runat="server" ToolTip="Add To Bill Finalization" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>
                                                            <asp:HyperLink ID="lnkbtnEditsrec" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span style="color:black" class="fas fa-edit">
                                                            </asp:HyperLink>
                                                            <asp:LinkButton ID="btnDelsrec" OnClick="btnDelsrec_OnClick" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
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

                                <asp:Panel ID="pnlthrec" Visible="false" runat="server">

                                    <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                        <asp:GridView ID="gvthrec" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvthrec_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo5threc" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAPPcentridthrec" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Name">
                                                    <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchprogvthrec" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvthrec')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAPPgvactdescthrec" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgAPPcbillno1threc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                            Width="70px"></asp:Label>
                                                        <asp:Label ID="lgvbillnothrec" runat="server" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAPPgvDatethrec" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MM-yyyy") %>'
                                                            Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bill ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAPPgvcbillrefthrec" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cbillref")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contractor name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAPPgvcsirdescthrec" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                            Width="130px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Bill Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbillamtthrec" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFlcamtthrec" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Security Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsdamtthrec" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFsdamtthrec" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Deduction Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdedamtthrec" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFdedamtthrec" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Net Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvnetamtthrec" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFnetamtthrec" runat="server" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkbtnPrintthrec" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>


                                                        <asp:HyperLink ID="lnkbtnthrec" runat="server" ToolTip="Add To Bill Finalization" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                        </asp:HyperLink>
                                                        <asp:HyperLink ID="lnkbtnEditthrec" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span style="color:black" class="fas fa-edit">
                                                        </asp:HyperLink>
                                                        <asp:LinkButton ID="btnDelthrec" OnClick="btnDelthrec_OnClick" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
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

                                </asp:Panel>

                                <asp:Panel ID="Pnlbillf" Visible="false" runat="server">
                                    <asp:GridView ID="gvfinal" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvfinal_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprofinal" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvfinal')"></asp:TextBox><br />
                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescsf1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnunof" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="R/A No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissuerefbill" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisurefno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescsf" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "isudat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sub-Contractor">


                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchconfinal" SortExpression="sirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Sub-Contractor" onkeyup="Search_Gridview(this,5,'gvfinal')"></asp:TextBox><br />

                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllisuno" runat="server" Visible="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lisuno")) %>'></asp:Label>
                                                    <asp:Label ID="lblissustatus" runat="server" Visible="False"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issustatus")) %>'></asp:Label>
                                                    <asp:Label ID="lgcResDescscf" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <%--  <asp:TemplateField HeaderText="Bill No">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcbillno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                               
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcamtf" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkbtnPrintIN" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>


                                                    <asp:HyperLink ID="lnkbtnApp" runat="server" ToolTip="Add To Bill Finalization" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                    </asp:HyperLink>
                                                    <asp:HyperLink ID="lnkbtnEditBilll" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span style="color:black" class="fas fa-edit"></span>
                                                    </asp:HyperLink>
                                                    <asp:LinkButton ID="btnDelReqCheck" OnClick="btnDelReqCheck_OnClick" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkBillDetailsfin" runat="server" ToolTip="MB Details" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs" Visible='<%# (Convert.ToBoolean((Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) == "3370") || (Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) == "3101")) ? true : false)%>'><i class=" fa fa-info-circle" aria-hidden="false"></i>
                                                    </asp:HyperLink>
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



                                </asp:Panel>

                                <asp:Panel ID="Pnlbillfapp" Visible="false" runat="server">

                                    <asp:GridView ID="gvfinalapp" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvfinalapp_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5fiapp" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAPPcentridfiapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                        Width="49px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprofinalapp" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvfinalapp')"></asp:TextBox><br />

                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPPgvactdescfiapp" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgAPPcbillno1fiapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                        Width="70px"></asp:Label>
                                                    <asp:Label ID="lgAPPcordernofiapp" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                        Width="70px"></asp:Label>
                                                    <asp:Label ID="lblbillstatus" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billstatus")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPPgvDatefiapp" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MM-yyyy") %>'
                                                        Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPPgvcbillreffiapp" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cbillref")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contractor name">


                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchconfinalapp" SortExpression="sirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Sub-Contractor" onkeyup="Search_Gridview(this,5,'gvfinalapp')"></asp:TextBox><br />

                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="txtAPPgvcsirdescfiapp" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bundle No" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtbundnofiapp" runat="server" BackColor="Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bundno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Bill Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillamtfiapp" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFlcamtfiapp" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Security Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsdamtfiapp" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sdamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFsdamtfiapp" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Deduction Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdedamtfiapp" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFdedamtfiapp" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Penalty Amount" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgpenamtfiapp" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "penamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFpenamtfiapp" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Reward" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRewardfiapp" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reward")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFRewardfiapp" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Net Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvnetamtfiapp" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFnetamtfiapp" runat="server" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkbtnPrintINapp" runat="server" Target="_blank"><span class=" fa fa-print"></span></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>



                                                    <asp:HyperLink ID="lnkbtnbfinapp" runat="server" ToolTip="Add To Bill Confirmed" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-default"><span  class=" fa fa-check "></span>
                                                    </asp:HyperLink>


                                                </ItemTemplate>
                                                <ItemStyle Width="40px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelfinapp" OnClick="btnDelfinapp_OnClick" ToolTip="Cancel" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkBillDetailsfinapp" runat="server" ToolTip="MB Details" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"
                                                        Visible='<%# (Convert.ToBoolean((Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) == "3370") || (Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) == "3101")) ? true : false)%>'><i class=" fa fa-info-circle" aria-hidden="false"></i> 
                                                    </asp:HyperLink>
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



                                </asp:Panel>

                                <!-- panel Bill Requsition Safi-->
                                <asp:Panel ID="pnlAllReq" runat="server">
                                    <asp:GridView ID="gvAllReq" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprofinal" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvAllReq')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescsf1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Requistion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnunof" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ref No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissuerefbill" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescsf" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvisbilstatus" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bilstatus")) %>'
                                                        Width="120px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>
                                </asp:Panel>



                                <!-- panel Bill Requsition Safi-->
                                <asp:Panel ID="PanelBillReq" runat="server">
                                    <asp:GridView ID="gvlabbillreq" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvlabbillreq_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprofinal" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvlabbillreq')"></asp:TextBox><br />

                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescsf1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Requistion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnunof" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Ref No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissuerefbill" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescsf" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqItem" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqQty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkbtnPrintIN" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>

                                                    <asp:HyperLink ID="lnkBillCS" runat="server" ToolTip="CS" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                    </asp:HyperLink>
                                                    <asp:HyperLink ID="lnkbtnEditBilllReq" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span style="color:black" class="fas fa-edit"></span>
                                                    </asp:HyperLink>

                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>
                                </asp:Panel>


                                <asp:Panel ID="PanelBillCs" runat="server">
                                    <asp:GridView ID="gvbillcs" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvbillcs_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprogvbillcs" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvbillcs')"></asp:TextBox><br />

                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescsf1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Requistion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnunof" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Requistion 02" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillreqno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Ref No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissuerefbill" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescsf" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Survey No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSurveyNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqItem" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqQty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkbtnPrintCSApp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                                    <asp:HyperLink ID="lnkBillCSApp" runat="server" ToolTip="CS" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                    </asp:HyperLink>
                                                    <asp:LinkButton ID="btnDelReqCSApp" OnClick="btnDelReqCSApp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>
                                </asp:Panel>

                                <asp:Panel ID="PanelWorkOrder" runat="server">
                                    <asp:GridView ID="gvWorkOrder" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvWorkOrder_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprofinal" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvWorkOrder')"></asp:TextBox><br />

                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescsf1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Requistion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnunof" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Requistion 2" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillreq2" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcsircode2" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csircode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Ref No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissuerefbill" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescsf" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Survey No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSurveyNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contactor Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvContractorName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqItem" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqQty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkWorkOrder" runat="server" ToolTip='<%# (this.GetCompCode()=="3370" || this.GetCompCode()=="3101") ? "MB Info" : "Work Order"%>' Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                    </asp:HyperLink>
                                                    <asp:LinkButton ID="btnDelWrkodr" OnClick="btnDelWrkodr_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>
                                                    <asp:HyperLink ID="hlnkCsAppEdit" runat="server" ToolTip="Bill CS Approval Edit" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span style="color:blue" class="fa fa-edit"></span>
                                                    </asp:HyperLink>
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
                                </asp:Panel>
                                <asp:Panel ID="pnlMbook" runat="server">
                                    <asp:GridView ID="gvmbook" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-bordered grvContentarea" OnRowDataBound="gvmbook_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNomb" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">
                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprogvmbook" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvmbook')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescmb" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Requistion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnunomb" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Requistion 2" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillreqmb" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcsircodemb" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csircode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Ref No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissuerefmb" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcReqdatemb" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Survey No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSurveyNomb" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contactor Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvContractorNamemb" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqItemmb" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqQtymb" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamountmb" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lbtnPrintWorkOrder" runat="server" Target="_blank" CssClass="btn btn-default btn-xs" ToolTip="Print WorkOrder"><span style="color:green" class="fa fa-print"></span></asp:HyperLink>
                                                    <asp:HyperLink ID="hlnklnkmb" runat="server" ToolTip="Bill Generate" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                    </asp:HyperLink>
                                                    <asp:LinkButton ID="btnDelmb" OnClick="btnDelmb_Click" ToolTip="Work Order Delete" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>
                                                    <asp:HyperLink ID="hlnkOrderEdit" runat="server" Target="_blank" CssClass="btn btn-default btn-xs" ToolTip="Work Order Edit"><span style="color:blue" class="fa fa-edit"></span></asp:HyperLink>

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
                                </asp:Panel>

                                <asp:Panel ID="pnlMbookApp" runat="server">
                                    <asp:GridView ID="gvmbookapp" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvmbookapp_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNombapp" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprofinalmbapp" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvmbookapp')"></asp:TextBox><br />

                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescmbapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Requistion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnunombapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Requistion 2" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbillreqmbapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcsircodembapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csircode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Ref No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissuerefmbapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcReqdatembapp" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Survey No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSurveyNombapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contactor Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvContractorNamembapp" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqItemmbapp" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqQtymbapp" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamountmbapp" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnklnkmbapp" runat="server" ToolTip="CS" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                    </asp:HyperLink>

                                                    <asp:LinkButton ID="btnDelmbapp" OnClick="btnDelmbapp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>


                                                </ItemTemplate>
                                                <ItemStyle Width="90px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="90px" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <RowStyle CssClass="grvRows" />
                                    </asp:GridView>

                                </asp:Panel>

                                <asp:Panel ID="PanelReadyForBil" runat="server">
                                    <asp:GridView ID="gvReadyForBill" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvReadyForBill_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name">

                                                <HeaderTemplate>
                                                    <asp:TextBox ID="txtSearchprogvReadyForBill" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvReadyForBill')"></asp:TextBox><br />

                                                </HeaderTemplate>


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPactdescsf1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill Requistion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissnunof" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Requistion 2" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvlreq2" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreqno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Ref No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvissuerefbill" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDescsf" runat="server"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Survey No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSurveyNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contactor Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvContractorName" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Work Order">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrerNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MB No">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvmbno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mbno1")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req Item">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqItem" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req Qty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvReqQty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0").ToString() %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkbtnPrintWorkOrder" runat="server" Target="_blank" CssClass="btn btn-default btn-xs" ToolTip="Print Contractor WorkOrder"
                                                        Visible='<%# Convert.ToBoolean(Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")).ToString()=="3370"?false:true)%>'>
                                                        <span style="color:green" class="fa fa-print"></span></asp:HyperLink>

                                                    <asp:HyperLink ID="lnkWorkOrder" runat="server" ToolTip="CS" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                    </asp:HyperLink>
                                                    <asp:LinkButton ID="btnDelReadyBill" OnClick="btnDelReadyBill_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa  fa-recycle"></span> </asp:LinkButton>


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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>


        <%-- <Triggers>

<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>

</Triggers>--%>
    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

