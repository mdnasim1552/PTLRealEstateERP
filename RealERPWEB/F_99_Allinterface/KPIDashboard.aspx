<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="KPIDashboard.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.KPIDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 
    
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <style>
           #AsyncFileUpload1 input {
               position: absolute;
               top: 0;
               right: 0;
               margin: 0;
               border: solid transparent;
               border-width: 0 0 100px 200px;
               opacity: 0.0;
               filter: alpha(opacity=0);
               -o-transform: translate(250px, -50px) scale(1);
               -moz-transform: translate(-300px, 0) scale(4);
               direction: ltr;
               cursor: pointer;
           }
       </style>
<script>
    function Search_Gridview(strKey, cellNr) {

        var strData = strKey.value.toLowerCase().split(" ");
        var tblData = document.getElementById("<%=gvAdDetails.ClientID %>");
        var rowData;
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
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });



        function openModal() {
            //    $('#myModal').modal('show');
            $('#contact').modal('toggle');
        }

        function CloseModal() {

            $('#contact').modal('hide');
        }



        function openCommentsModal() {
            //    $('#myModal').modal('show');
            $('#mcomments').modal('toggle');
        }

        function CloseCommentsModal() {

            $('#mcomments').modal('hide');
        }


        function loadModalAssign() {

            $('#AddAssign').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }


        function CloseModalAssign() {
            $('#AddAssign').modal('hide');


        }


        function pageLoaded() {

            try {

                $("input, select").bind("keydown", function (event) {

                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);

                });

                $('.chzn-select').chosen({ search_contains: true });

                var gvclient = $('#<%=this.gvclient.ClientID %>');
                gvclient.tblScrollable();

                //gvclient.gridviewScroll({
                //    width: 1160,
                //    height: 420,
                //    arrowsize: 30,
                //    railsize: 16,
                //    barsize: 8,
                //    varrowtopimg: "../Image/arrowvt.png",
                //    varrowbottomimg: "../Image/arrowvb.png",
                //    harrowleftimg: "../Image/arrowhl.png",
                //    harrowrightimg: "../Image/arrowhr.png",
                //    freezesize: 10
                //});
            }

            catch (e) {

                alert(e.message)

            }

        }


        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }




        function AddButton(id) {
            $(".hiddenb" + id).css("display", "inline");

        }
        function HiddenButton(id) {
            $(".hiddenb" + id).css("display", "none");
        }

        function AddButtonsub(id) {
            $(".hiddensub" + id).css("display", "inline");

        }
        function HiddenButtonsub(id) {
            $(".hiddensub" + id).css("display", "none");
        }


    </script>
    <style type="text/css">
        .modal-dialog {
            margin: 44px auto;
            width: 50%;
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
        }

            ul.sidebarMenu li {
                display: block;
                height: 30px;
                list-style: none;
                border: 1px solid #DFF0D8;
                /*border-bottom: 0;*/
            }

                ul.sidebarMenu li:last-child {
                    /*border-bottom: 1px solid #DFF0D8;*/
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
                height: 50px;
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
                    height: 50px;
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
            background: #199698;
            color: #fff;
        }




        .tbMenuWrp table tr td {
            /*height: 50px;*/
            height: 65px;
            width: 110px;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 1px;
            color: #fff;
            text-align: center;
            border: 2px solid #D1D735;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }

            .tbMenuWrp table tr td:nth-child(1) {
                background: #3BA8E0;
            }

            .tbMenuWrp table tr td:nth-child(2) {
                background: #5EB75B;
            }



            .tbMenuWrp table tr td:nth-child(3) {
                background: #EFAD4D;
                display: none;
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #D95350;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #76C9B5;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #5EB75B;
            }


            .tbMenuWrp table tr td:nth-child(7) {
                background: #00CBF3;
                display: none;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                background: #00CBF3;
            }

        /*.tbMenuWrp table tr td:nth-child(7) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                width: 115px;
                padding: 0 3px;
            }*/


        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 100%;
            margin: 1px 0;
            padding: 1px;
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
            background-color: #00ff21;
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
            /*background: #12A5A6;
            color: #fff;*/
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }



        .fan {
            border: 2px solid #f3b728;
            border-radius: 50%;
            display: inline-block;
            float: left;
            font-size: 18px;
            margin-top: 4px;
            padding: 2px;
        }

            .fan:nth-child(1) {
                background: #FF9C40 !important;
                color: #fff;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(2) {
                color: #E49015;
                background-color: #5EB75A !important;
            }

            .fan:nth-child(3) {
                color: #fff;
                background: #085407 !important;
            }

            .fan:nth-child(4) {
                color: #fff;
                background: #DA3F40 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(5) {
                color: #fff;
                background: #009BFF !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(6) {
                color: #E4DDDD;
                background: #539250 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #E4DDDD;
                background: #E79956 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #fff;
                background: #459A42 !important;
                border: 2px solid #E4DDDD;
            }





        .modalPopup {
            top: 25px !important;
            min-height: 400px;
            overflow: scroll;
        }

        ul.sidebarMenu li {
            display: block;
            height: 36px;
            list-style: none;
            border: 1px solid #9752A2;
            /*border-bottom: 0;*/
        }

            ul.sidebarMenu li a {
                text-align: left;
                display: block;
                line-height: 30px;
                font-size: 14px;
                font-family: Calibri;
                cursor: pointer;
                /*/* width: 130px; */
                /*background: #32CD32;*/
                background: #CCE5FF;
                border-radius: 5;
                color: #000;
                font-weight: bold;
                padding: 0 0 0 4px;
                /* line-height: 30px; */
                margin: 1px 2px;
                display: block;
                text-align: left;
                border-bottom: 1px;
            }

                ul.sidebarMenu li a:hover {
                    background: #006633;
                    color: white !important;
                }

        .pad2px {
            padding-top: 2px;
            padding-bottom: 2px;
        }
    </style>
  
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
                            <div class="col-md-1">
                           <div class="form-group">
                                    <Label for="lblfrmdate"   class="control-label lblmargin-top9px">Date</Label>

                               </div>
                                </div>
                                <div class="col-md-2">
                                       <div class="form-group">
                                             <asp:TextBox ID="txtdate" runat="server" CssClass="form-control " AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                                Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                
                                       </div>
                                </div>
                                <div class="col-md-1">
                                     <div class="form-group">
                                              <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary " OnClick="lnkbtnok_Click">Refresh</asp:LinkButton>&nbsp;&nbsp;

                                         </div>
                                </div>
                                  <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:HyperLink ID="HyperLink3" Target="_blank" NavigateUrl="~/F_21_Mkt/ClientInitial?Type=MktCl" runat="server" CssClass="btn btn-danger ">Primary Lead</asp:HyperLink>

                                            </div>
                                      </div>
                                 <div class="col-md-1">
                                     <div class="form-group">
                                            <Label for="Label1"  class =" control-label lblmargin-top9px">Total Cust.</Label>

                                         </div>
                                     </div>
                            
                                <div class="col-md-1">
                                    <div class="form-group">
                                    <asp:HyperLink ID="ttlcustomer" Target="_blank" NavigateUrl="~/F_21_Mkt/MktEmpKpiEntry?Type=Entry" runat="server" CssClass="btn btn-sm btn-warning pad2px">1000</asp:HyperLink>
                                        </div>
                                  </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                    <asp:HyperLink ID="HyperLink4" Target="_blank" NavigateUrl="~/F_21_Mkt/ClientDiscuDetails" runat="server" CssClass="btn btn-success small"> <span class="	glyphicon glyphicon-eye-open"></span>All Discu.</asp:HyperLink>

                                        </div>

                                </div>

                                <div class="col-md-2">
                                        <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                            <button type="button" class="btn btn-danger">Other Action</button>
                                            <div class="btn-group" role="group">
                                                <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                                    <div class="dropdown-arrow"></div>
                                             <asp:HyperLink ID="HyperLink2"  NavigateUrl="~/F_21_MKT/CrmClientInfo?Type=Entry" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-plus"></span> Add Prospect</asp:HyperLink>

                                                        <%--<a data-toggle="modal" data-target="#contact" Class="dropdown-item"><span class="glyphicon glyphicon-plus"></span>Add New Client    </a>--%>
                                 
                                    <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_21_Mkt/MktEmpKpiEntry?Type=Entry" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-pencil"></span> Add New Discussion</asp:HyperLink>
                                    
                                                      <asp:HyperLink ID="HyperLink7" Target="_blank" NavigateUrl="~/F_64_Mgt/GenCodeBook?Type=81" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-pencil"></span>Discussion Field</asp:HyperLink>
                                                    <asp:HyperLink ID="hllnkCodebook" Target="_blank" NavigateUrl="~/F_21_Mkt/MktGenCodeBook" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-pencil"></span>Code Book</asp:HyperLink>
                                                               </div>
                                            </div>
                                        </div>



                                


                                </div>
                            </div>

                    <div class="row">
                        <div class="col-md-2">


                            <ul class="sidebarMenu">


                                <li style="background-color: #FFA07A!important; text-align: center !important; padding: 10px !important">
                                    <asp:Label ID="Label2" runat="server" Font-Size="15px" Font-Bold="true" Style="text-align: center; margin-top: 3px !important;" Font-Names="Cambria" ForeColor="Black">Entry</asp:Label>

                                </li>

                                <li>
                                    <asp:HyperLink ID="hlnkyearlybudget" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_21_Mkt/AllAdvertisement">Advertisement Setup</asp:HyperLink>
                                </li>
                                 <li>
                                    <asp:HyperLink ID="hylinkClientTrans" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_21_Mkt/ClientTransfer">Client Transfer</asp:HyperLink>
                                </li>
                                <%--<li>
                                    <asp:HyperLink ID="HyperLink21" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MonthlySalesBudget?Type=Monthly">Target-Monthly</asp:HyperLink>
                                </li>

                                <li>
                                    <asp:HyperLink ID="HyperLink13" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MktEntryUnit">Unit Fixation</asp:HyperLink>
                                </li>
                                 <li>
                                    <asp:HyperLink ID="HyperLink20" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MktDummySalsPayment?Type=Sales">Dummy Payment Schedule</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink15" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MktBookigApp?Type=Entry">Booking Application Form</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MktSalsPayment?Type=Sales">Sales Payment Schedule</asp:HyperLink>
                                </li>

                                <li>
                                    <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/MktMoneyReceipt?Type=CustCare">Money Receipt Create</asp:HyperLink>

                                </li>--%>

                                <li style="background-color: #FFA07A!important; text-align: center !important; padding: 10px !important">
                                    <asp:Label ID="lbl" runat="server" Font-Size="15px" Font-Bold="true" Style="text-align: center;" Font-Names="Cambria" ForeColor="Black">Report</asp:Label>

                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_21_Mkt/RptProWiseClOffered?Type=SalesDeamnd">Sales Demand Analysis</asp:HyperLink>
                                </li>

                                <li>
                                    <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_21_Mkt/RptProWiseClOffered?Type=SalesDeci">Sales Decision</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink8" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_21_Mkt/RptProWiseClOffered?Type=Capacity">Client Capacity Analysis</asp:HyperLink>


                                </li>

                                    <li>
                                    <asp:HyperLink ID="HyperLink10" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_21_Mkt/RptCallCenterLead?Type=SourceWise">Source Wise Leads</asp:HyperLink>
                                   


                                </li>
                                <li>

                                     <asp:HyperLink ID="hlnkspwlead" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_21_Mkt/RptCallCenterLead?Type=SalespWise">Sales Person Wise Leads</asp:HyperLink>

                                </li>





                            </ul>

                        </div>
                        <div class="col-md-10">
                            <div class="row">
                                <div id="slSt" class=" col-md-12  pading5px">
                                    <div class="panel with-nav-tabs panel-deafult">
                                        <fieldset class="tabMenu">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="1"></asp:ListItem>
                                                            <asp:ListItem Value="2"></asp:ListItem>
                                                            <asp:ListItem Value="3"></asp:ListItem>
                                                            <asp:ListItem Value="4"></asp:ListItem>
                                                            <asp:ListItem Value="5"></asp:ListItem>
                                                            <asp:ListItem Value="6"></asp:ListItem>
                                                            <asp:ListItem Value="7"></asp:ListItem>
                                                            <asp:ListItem Value="8"></asp:ListItem>
                                                            <asp:ListItem Value="9"></asp:ListItem>

                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <asp:Panel ID="PnlClientAssign" runat="server" Visible="false">

                                    <div class="row" style="margin-left:15px;"">
                                        <asp:GridView ID="gvAdDetails" runat="server" AutoGenerateColumns="False" 
                                            ShowFooter="True" CssClass="table-striped table-hover table-bordered table-responsive grvContentarea">
                                            <RowStyle />
                                            <Columns>

                                                <asp:TemplateField HeaderText="S.L">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialno" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Id">

                                                    <ItemTemplate>
                                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                 <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcredate" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "entdate")).ToString("dd-MMM-yyyy") %>' Width="65px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                 <asp:TemplateField HeaderText="Source">
                                                      <HeaderTemplate>
                                                <asp:TextBox ID="txtsrc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Source" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtpaper"
                                                            runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "papdesc")) %>'
                                                            Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Email" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtclemail"
                                                            runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                            Width="110px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <HeaderTemplate>
                                                <asp:TextBox ID="txtname" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Name" onkeyup="Search_Gridview(this,4)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtclname" runat="server" Style="text-align: left; font-size: 11px;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                            Width="120px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile">
                                                    <HeaderTemplate>
                                                <asp:TextBox ID="txtmob" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Moble" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtclmob" runat="server" Style="text-align: left; font-size: 11px;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mob1")) %>'
                                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                   
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Information">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtclinfo"
                                                            runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "info")) %>'
                                                            Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                                    </ItemTemplate>

                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Location">
                                                    <HeaderTemplate>
                                                <asp:TextBox ID="txtlocation" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Location" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbllocat" runat="server" Style="text-align: left; font-size: 11px;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locat")) %>'
                                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Profession">
                                                    <HeaderTemplate>
                                                <asp:TextBox ID="txtprofession" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Profession" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpro" runat="server" Style="text-align: left; font-size: 11px;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pro")) %>'
                                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />

                                                </asp:TemplateField>


                                                 <asp:TemplateField HeaderText="Interested Project">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvproject" runat="server" Style="text-align: left; font-size: 11px;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="100px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Lead Dept.">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeptLeads" runat="server" Style="text-align: left; font-size: 11px;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                            Width="100px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Flat Size">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtclsize" runat="server" Style="text-align: right; font-size: 11px;"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "size")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>
                                              
                                                 <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>

                                                        <asp:CheckBox ID="chkempid" runat="server"
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                                            Enabled='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True")?false:true %>'
                                                            Width="20px" />
                                                    </ItemTemplate>

                                                     <FooterTemplate>

                                        <asp:LinkButton ID="lnkbtnAssign" runat="server" OnClientClick="return FunAssignConfirm();"
                                             OnClick="lnkbtnAssign_Click" ToolTip="Assign"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                       
                                    </FooterTemplate>

                                                    <HeaderStyle Width="40" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtuserid" runat="server" Style="text-align: left; font-size: 11px;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtclrmks" runat="server" Style="text-align: left; font-size: 11px;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmks")) %>'
                                                            Width="100px" BackColor="Transparent" BorderStyle="Groove"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />

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

                                <asp:Panel ID="pnlMontDisc" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvclient" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvclient_RowDataBound"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />

                                           

                                             <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyplCustomer" runat="server" Font-Size="10px" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="100px"></asp:HyperLink>


                                                 <asp:Label ID="lgvproscode" runat="server" Visible="false" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'
                                                    Width="70px"></asp:Label>
                                                 <asp:Label ID="lgvteamcode" runat="server" Visible="false" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode")) %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText=" Phone No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPhonech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>




                                        <asp:TemplateField HeaderText="Meeting Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMeetingdat" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                                <asp:Label ID="lblcDate" Font-Size="10px" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvkpigrp" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followupdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>

                                                <asp:Panel ID="pnldis" runat="server" ClientIDMode="Static">

                                                <asp:Label ID="lgvDiscussion0"   runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="120px">                                             


                                                </asp:Label>
                                                <asp:LinkButton ID="lnkAdddis" ClientIDMode="Static"   Width="10"  ToolTip="Comments" runat="server" OnClick="lnkAdddis_Click"><span class="fa fa-edit"></span></asp:LinkButton>
                                                </asp:Panel>
                                                 <asp:Label ID="lblgvdisgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "disgnote")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                     

                                        <asp:TemplateField HeaderText="Participants">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpartcilist" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next </br>Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnfollowup" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next <br> Appointment">
                                            <ItemTemplate>
                                                <asp:Label ID="nappdat0" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Panel ID="pnlsub" runat="server" ClientIDMode="Static">
                                                    <asp:Label ID="lgvndissub" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                        Width="100px"></asp:Label>
                                                    <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                                </asp:Panel>
                                                <asp:Label ID="lblgvsubgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subgnote")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassoc" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusername" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstatus" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        
                                    </Columns>

                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="pnlSodlUnit" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvSoldUnit" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvSoldUnit_RowDataBound"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0S" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyplCustomerS" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                            Width="150px"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Phone No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvPhonechS" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpronamechS" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Unit Size">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvunitsizechS" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0; (#,##0); ") %>'
                                                            Width="55px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>                                               
                                              
                                                <asp:TemplateField HeaderText="Discussion" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvDiscussion0S" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>






                                            </Columns>
                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                              <RowStyle CssClass="grvRows" />

                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlAppoinment" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvAppo" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvAppo_RowDataBound"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />

                                             <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoappo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyplCustomerappo" runat="server" Font-Size="10px" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="90px"></asp:HyperLink>


                                                 <asp:Label ID="lgbproscod" runat="server" Visible="false" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'
                                                    Width="70px"></asp:Label>
                                                 <asp:Label ID="lgvteamcode" runat="server" Visible="false" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode")) %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText=" Phone No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPhonech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>




                                        <asp:TemplateField HeaderText="Meeting Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMeetingdatappo" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                                <asp:Label ID="lblcDate" Font-Size="10px" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvkpigrpappo" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followupdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>

                                                

                                                <asp:Label ID="lgvDiscussionappo"   runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="100px">                                             


                                                </asp:Label>
                                               
                                               
                                                 
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                     

                                        <asp:TemplateField HeaderText="Participants">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpartcilistappo" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next </br>Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnfollowupappo" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next <br> Appointment">
                                            <ItemTemplate>
                                                <asp:Label ID="nappdatappo" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                              
                                                    <asp:Label ID="lgvndissubappo" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                        Width="80px"></asp:Label>
                                                    <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                               
                                                
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassocappo" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusernameappo" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstatusappo" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                                  <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                         <asp:HyperLink ID="hlnkAdd" runat="server" ToolTip="Add" CssClass="btn btn-xs btn-default pull-left" Target="_blank"> <i class="fa  fa-check"></i></asp:HyperLink>
                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>

                                                         
                                                        <asp:HyperLink ID="hyledit" runat="server" ToolTip="Edit" CssClass="btn btn-xs btn-default pull-left" Target="_blank"> <i class="fa fa-edit"></i></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                        
                                    </Columns>
                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                              <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlDissAppo" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDisappon" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvDisappon_RowDataBound"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />


                                                <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoapdis" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyplCustomerapdis" runat="server" Font-Size="10px" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="90px"></asp:HyperLink>


                                               

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText=" Phone No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPhonech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>




                                        <asp:TemplateField HeaderText="Meeting Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMeetingdatapdis" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                                <asp:Label ID="lblcDate" Font-Size="10px" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvkpigrpnapdis" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followupdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>

                                                

                                                <asp:Label ID="lgvDiscussionapdis"   runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="100px">                                             


                                                </asp:Label>
                                               
                                               
                                                 
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                     

                                        <asp:TemplateField HeaderText="Participants">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpartcilistnapdis" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next </br>Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnfollowupnapdis" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next <br> Appointment">
                                            <ItemTemplate>
                                                <asp:Label ID="nappdatnapdis" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                              
                                                    <asp:Label ID="lgvndissubapdis" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                        Width="130px"></asp:Label>
                                                    <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                               
                                                
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassocapdis" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusernameapdis" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstatusapdis" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                                

                                            

                                        
                                    </Columns>


                                            
                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                              <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlDissNew" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDissNew" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvDissNew_RowDataBound"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />

                                                     <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNodnew" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyplCustomerdnew" runat="server" Font-Size="10px" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="90px"></asp:HyperLink>


                                               

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText=" Phone No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPhonech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>




                                        <asp:TemplateField HeaderText="Meeting Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMeetingdatdnew" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                                <asp:Label ID="lblcDatednew" Font-Size="10px" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvkpigrpdnew" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followupdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>

                                                

                                                <asp:Label ID="lgvDiscussiondnew"   runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="100px">                                             


                                                </asp:Label>
                                               
                                               
                                                 
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                     

                                        <asp:TemplateField HeaderText="Participants">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpartcilistdnew" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next </br>Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnfollowupndnew" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next <br> Appointment">
                                            <ItemTemplate>
                                                <asp:Label ID="nappdatdnew" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                              
                                                    <asp:Label ID="lgvndissubdnew" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                        Width="130px"></asp:Label>
                                                    <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                               
                                                
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassocdnew" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusernamednew" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstatusdnew" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                                

                                            

                                        
                                    </Columns>
                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                              <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlDataBank" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDataBank" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvDataBank_RowDataBound"
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNodBank" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer Name">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyplrdBank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                            Width="150px"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Phone No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvPhonechdBank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Meeting Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvMeetingdatdBank" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Next Appointment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="nappdat0dBank" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpronamecdBank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discussion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvdBank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                            </Columns>
                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                              <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlNextApponi" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvNextAppt" runat="server"
                                            AutoGenerateColumns="False" PageSize="15" ShowFooter="true" 
                                            CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />

                                           
                                             <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNonapp" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyplCustomernapp" runat="server" Font-Size="10px" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="90px"></asp:HyperLink>


                                               

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText=" Phone No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPhonech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>




                                        <asp:TemplateField HeaderText="Meeting Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMeetingdatnapp" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                                <asp:Label ID="lblcDate" Font-Size="10px" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvkpigrpnapp" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followupdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>

                                                

                                                <asp:Label ID="lgvDiscussionnapp"   runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="100px">                                             


                                                </asp:Label>
                                               
                                               
                                                 
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                     

                                        <asp:TemplateField HeaderText="Participants">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpartcilistnapp" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next </br>Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnfollowupnapp" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next <br> Appointment">
                                            <ItemTemplate>
                                                <asp:Label ID="nappdatnapp" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy hh:ss tt") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                              
                                                    <asp:Label ID="lgvndissubnapp" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                        Width="130px"></asp:Label>
                                                    <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                               
                                                
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassocnapp" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusernamenapp" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstatusnapp" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                                

                                            

                                        
                                    </Columns>

                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                              <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="row">
                                <asp:Panel ID="pnlbirthday" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvClientBrthDay" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            Width="186px" ShowFooter="True">

                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvslnobdte" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClientNamebdte" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nick Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClientnNamebbdte" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>' Width="120px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvbday" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bday")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpredAddressbdte" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                            Width="160px" ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Home Phone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvhomephonebbdte" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvMobNobdte" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Office Phone">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvoffPhonebbdte" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnSendMail" runat="server" Font-Bold="True" CssClass="btn btn-warning"
                                                            Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send Mail</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvemailbbdte" runat="server" Width="140px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnSendSms" runat="server" Font-Bold="True" CssClass="btn btn-primary "
                                                            Font-Size="12px" ForeColor="Black">Send SMS</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
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


                                <asp:Panel ID="PnlMarrDay" runat="server" Visible="false">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvCliMarrDay" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            Width="186px" ShowFooter="True" OnRowDataBound="gvCliMarrDay_RowDataBound">

                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvslnobdte" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClientNamebdte" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>' Width="160px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nick Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvClientnNamebbdte" runat="server"
                                                            Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nickname")) %>' Width="120px"
                                                            ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvbday" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bday")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpredAddressbdte" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                            Width="160px" ForeColor="Black"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Home Phone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvhomephonebbdte" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvMobNobdte" runat="server" Width="100px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Office Phone">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvoffPhonebbdte" runat="server" Width="80px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnSendMail" runat="server" Font-Bold="True" CssClass="btn btn-warning"
                                                            Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send Mail</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvemailbbdte" runat="server" Width="140px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="btnSendSms" runat="server" Font-Bold="True" CssClass="btn btn-primary "
                                                            Font-Size="12px" ForeColor="Black">Send SMS</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" />
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




                   
                            </div>

                        </div>
                    </div>                    
       
                    <div class="row">                      


                <div class="modal fade right"  id="contact" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
  aria-hidden="true" data-backdrop="false">
  <div class="modal-dialog  modal-lg  modal-side modal-bottom-right modal-notify modal-info" role="document">
    <!--Content-->
    <div class="modal-content">
      <!--Header-->
      <div class="modal-header">
        <p class="heading"><h4><span class="glyphicon glyphicon-info-sign"></span>Add New Client</h4>
        </p>

        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true" class="white-text">&times;</span>
        </button>
      </div>

      <!--Body-->
      <div class="modal-body">

        <div class="row">
         
         <div class="col-md-12">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                                                    ShowFooter="True"
                                                                    OnRowDataBound="gvPersonalInfo_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">

                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                                                    ForeColor="Black"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                                    Width="90px" ForeColor="Black"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description of Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                                    Width="150px" ForeColor="Black"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvgval" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Value">

                                                                            <ItemTemplate>

                                                                                <asp:TextBox ID="txtgvValMob" runat="server" Visible="false" TabIndex="30" BackColor="Transparent" BorderStyle="None"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                                    Width="350px"></asp:TextBox>

                                                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                                    Width="350px" TabIndex="31"></asp:TextBox>
                                                                                <asp:DropDownList ID="ddlprofession" TabIndex="32" CssClass="form-control" runat="server" Visible="false">
                                                                                </asp:DropDownList>
                                                                                <asp:TextBox ID="txtgvCal" runat="server" Visible="false" TabIndex="33" BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>

                                                                                <cc1:CalendarExtender ID="txtPublish_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtgvCal"></cc1:CalendarExtender>
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
                                                       
             <div class="col-md-12">

                                                            <div class=" file-upload">

                                                                <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                    OnClientUploadComplete="uploadComplete" runat="server"
                                                                    ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                    CompleteBackColor="White" ToolTip="Browse File"
                                                                    UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                    OnUploadedComplete="FileUploadComplete" Width="250px" />
                                                                <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/uploadnahid.gif" />
                                                                <br />
                                                                <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>

                                                            </div>





                                                        </div>
          
        </div>
      </div>

      <!--Footer-->
      <div class="modal-footer">
       
       
        <asp:LinkButton ID="lUpdatPerInfo" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClick="lUpdatPerInfo_Click" OnClientClick="CloseModal();">Save</asp:LinkButton>
      
         <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true" class="white-text">&times;</span>
        </button>
      
      </div>
    </div>
    <!--/.Content-->
  </div>
</div>

                    
                    
                    
                   </div>
                     <div class="modal fade right" id="mcomments" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                            aria-hidden="true" data-backdrop="false">
                            <div class="modal-dialog  modal-lg  modal-side modal-bottom-right modal-notify modal-info" role="document">
                                <!--Content-->
                                <div class="modal-content">
                                    <!--Header-->
                                    <div class="modal-header">
                                        <p class="heading">
                                            <h4 id="lblheader" runat="server"><span class="glyphicon glyphicon-info-sign"></span></h4>
                                        </p>

                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true" class="white-text">&times;</span>
                                        </button>
                                    </div>

                                    <!--Body-->
                                    <div class="modal-body">

                                        <div class="row">

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label  id="lbldsi" runat="server" class="control-label lblmargin-top9px"></label>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lbldiscussion" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px" style="background:#DFF0D8"></asp:Label>


                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label for="lblcomm" class="control-label lblmargin-top9px">Comments:</label>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtComm" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px"></asp:TextBox>


                                                </div>
                                            </div>


                                            <asp:Panel ID="pnld" runat="server" Visible="false">

                                            <asp:Label ID="lblEmpid" runat="server"></asp:Label>
                                            <asp:Label ID="lblclient" runat="server"></asp:Label>
                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                           

                                            </asp:Panel>


                                        </div>
                                    </div>

                                    <!--Footer-->
                                    <div class="modal-footer">


                                        <asp:LinkButton ID="lUpdatInfo" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="CloseCommentsModal();" OnClick="lUpdatInfo_Click">Save</asp:LinkButton>

                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true" class="white-text">&times;</span>
                                        </button>

                                    </div>
                                </div>
                                <!--/.Content-->
                            </div>
                        </div>

                </div>
            
           
           
           </div>
    <%-- Client Assign --%>
        
       <div id="AddAssign" class="modal animated slideInTop " role="dialog" data-keyboard="false" data-backdrop="static">
                    <div class="modal-dialog">
                        <div class="modal-content  ">
                            <div class="modal-header">
                                <h4 class="modal-title">  
                                <i class="fa fa-hand-point-right"></i>
                                    Assign Team  </h4>

                                <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>


                            </div>
                            <div class="modal-body form-horizontal">
                                <div class="row-fluid">

                                    <div class="form-horizontal">   
                                        <div class="row">

                                             <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="control-label  lblmargin-top9px" for="lblassigndate" id="lblDaterange" runat="server">Assign Team </label>

                                                </div>
                                            </div>
                                            <div class="col-md-9">
                                                <div class="form-group">
                                                   <asp:DropDownList ID="ddlTeam" runat="server" CssClass="form-control chzn-select">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>



                                          
                                        </div>

                                    </div>




                                </div>


                            </div>
                            <div class="modal-footer ">

                             


                            

                              
                                <asp:LinkButton runat="server" ID="lnkbtnAssignTeam" type="submit" OnClientClick="CloseModalAssign();" CssClass="btn btn-sm btn-success"  OnClick="lnkbtnAssignTeam_Click"><i class="fa fa-save" aria-hidden="true"></i> Update</asp:LinkButton>


                            </div>
                        </div>
                    </div>
                </div>
    
   
  


</asp:Content>

