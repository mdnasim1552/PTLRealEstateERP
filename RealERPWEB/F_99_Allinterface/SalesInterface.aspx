
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SalesInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.SalesInterface" %>

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
            width: 120px;
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
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #D95350;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #76C9B5;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #769BF4;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #00CBF3;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                background: #4BCF9E;
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
            font-family: Calibri, Arial;
            font-weight: bold;
            height: 100%;
            margin: 1px 0;
            padding: 2px;
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
                border-radius: 5px;
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
    </style>



    <script src="../Scripts/waypoints.min.js"></script>
    <script src="../Scripts/jquery.counterup.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            try {

                //$('.counter').counterUp({
                //    delay: 10,
                //    time: 1000
                //});
              //  $("#lblcons").attr("href", "F_08_PPlan/ConstructionInfo.aspx?Type=Report&comcod=" + comcod);

                var comcod =<%=this.GetCompCode()%>;

              
                switch (comcod)
                {
                    case 3339:
                    case 3101:
                        $('#<%=this.hlnkyearlybudget.ClientID%>').attr("href", "../F_22_Sal/MonthlySalesBudget03");
                        break;

                    default:
                        $('#<%=this.hlnkyearlybudget.ClientID%>').attr("href", "../F_22_Sal/MonthlySalesBudget02");
                        break;


                }

                $('.chzn-select').chosen({ search_contains: true });

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $('#<%=this.gvDayWSale.ClientID %>').tblScrollable();
                $('#<%=this.grvTrnDatWise.ClientID%>').tblScrollable();
                $('#<%=this.gvSpayment.ClientID%>').tblScrollable();
                $('#<%=this.gvcustdues.ClientID%>').tblScrollable();
                $('#<%=this.gvcustduescur.ClientID%>').tblScrollable();
                $('#<%=this.gvcustduesover.ClientID%>').tblScrollable();


            } catch (e) {
                alert(e);
            }



        };

    </script>








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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <div class="form-group">
                                    <label class="control-label   lblmargin-top9px" for="lblfrmdate">Date</label>

                                </div>


                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">

                                <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control   fa fa-pencil-ruler" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>



                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-2 pading5px ">


                            <ul class="sidebarMenu">


                                <li style="background-color: #FFA07A!important; text-align: center !important; padding: 10px !important">
                                    <asp:Label ID="Label2" runat="server" Font-Size="15px" Font-Bold="true" Style="text-align: center; margin-top: 3px !important;" Font-Names="Cambria" ForeColor="Black">Entry</asp:Label>

                                </li>

                                <li>
                                    <asp:HyperLink ID="hlnkyearlybudget" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MonthlySalesBudget02">Target-Yearly</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink18" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MonthlySalesBudget?Type=Monthly">Target-Monthly</asp:HyperLink>
                                </li>

                                <li>
                                    <asp:HyperLink ID="HyperLink13" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MktEntryUnit">Unit Fixation</asp:HyperLink>
                                </li>
                                 
                                <li>
                                    <asp:HyperLink ID="hlnkSalsPaymen" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MktDummySalsPayment?Type=Sales">Dummy Payment Schedule</asp:HyperLink>
                                </li>



                                <li>
                                    <asp:HyperLink ID="HyperLink15" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MktBookigApp?Type=Entry">Booking Application Form</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/MktSalsPayment?Type=Sales">Sales Payment Schedule</asp:HyperLink>
                                </li>
                                 
                                <li>
                                    <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/MktMoneyReceipt?Type=CustCare">Money Receipt Create</asp:HyperLink>

                                </li>
                               
                                <li>
                                    <asp:HyperLink ID="HyperLink16" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_17_Acc/MoneyRecptApprov">Money Receipt Approval</asp:HyperLink>

                                </li>

                                <li style="background-color: #FFA07A!important; text-align: center !important; padding: 10px !important">
                                    <asp:Label ID="lbl" runat="server" Font-Size="15px" Font-Bold="true" Style="text-align: center;" Font-Names="Cambria" ForeColor="Black">Report</asp:Label>

                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/SalesInformation?Type=Report&comcod=">Sales Dashboard</asp:HyperLink>
                                </li>
                                  <li>
                                    <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptMRTopSheet">Money Receipt</asp:HyperLink>

                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_17_Acc/RptAccCollVsClearance?Type=MonSales&comcod=">Month Wise Sales</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink12" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_17_Acc/RptAccCollVsClearance?Type=MonAR&comcod=">Month Wise Collection</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyplDelRoprt" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptReceivedList04?Type=AllProDuesCollect&comcod=">Revenue Status</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/RptSaleSoldunsoldUnit?Type=soldunsold&comcod=&prjcode=&Date1=">Sold & Unslod Info</asp:HyperLink>
                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/RptSaleSoldunsoldUnit?Type=RptDayWSale&comcod=&prjcode=&Date1=">Day Wise Sales</asp:HyperLink>
                                </li>

                                <li>
                                    <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/RptAvailChart?Type=Details">Availability Chart</asp:HyperLink>
                                </li>

                              

                                <li>
                                    <asp:HyperLink ID="HyperLink9" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptCustPayStatus?Type=ClLedger">Client Ledger</asp:HyperLink>

                                </li>


                                <li>
                                    <asp:HyperLink ID="HyperLink10" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/RptSalSummery?Type=dSaleVsColl">Daily Collection & Sales</asp:HyperLink>

                                </li>

                                <li>

                                    <asp:HyperLink ID="HyperLink8" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptReceivedList02?Type=yCollectionfc&prjcode=">Yearly Forcasting</asp:HyperLink>

                                    <%--<asp:HyperLink ID="HyperLink8" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptReceivedList02?Type=yCollectionfc">Yearly Forcasting</asp:HyperLink>--%>
                                
                              
                                </li>

                                <li>
                                    <asp:HyperLink ID="HyperLink14" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_22_Sal/RptSalSummery?Type=dSaleVsColl&comcod=">Month Wise Sales(MKT. Person)</asp:HyperLink>

                                </li>
                                <li>
                                    <asp:HyperLink ID="HyperLink17" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_17_Acc/RptAccCollVsClearance?Type=CollBuyer&comcod=">Month Wise Coll. (Buyer & Project)</asp:HyperLink>

                                </li>
                                  <li>
                                    <asp:HyperLink ID="HyperLink19" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptReceivedList04?Type=MonthlyColSchedule">Monthly Collection Schedule</asp:HyperLink>

                                </li>

                                 <li>
                                    <asp:HyperLink ID="HyperLink21" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptReceivedList04?Type=MonthlyColScheduleSum">Monthly Collection Schedule(Summary)</asp:HyperLink>

                                </li>



                                 <li>
                                    <asp:HyperLink ID="HyperLink20" runat="server" Target="_blank" Font-Size="11px" Font-Names="Cambria" ForeColor="Black" Font-Underline="false" NavigateUrl="~/F_23_CR/RptReceivedList04?Type=MonthlyColl">Monthly Collection(Receipt Type)</asp:HyperLink>

                                </li>

                            </ul>

                        </div>
                        <div id="slSt" class=" col-md-9  pading5px" style="margin-left: 30px;">
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
                                                    <%-- <asp:ListItem Value="6"></asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <div>

                                    <asp:Panel ID="pnlgvDayWSale" runat="server" Visible="false">

                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvDayWSale" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" AllowPaging="False"
                                                OnRowDataBound="gvDayWSale_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDPactdesc" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDcuname" runat="server" Font-Size="10px"
                                                                Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDResDesc" runat="server" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="10px"
                                                                ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgUnit" runat="server" Font-Size="9px"
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "munit"))
                                                                         %>'
                                                                Width="20px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit </Br> Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUSizeds" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price </Br>Per</Br> SFT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUpsft" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sftpr")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Budgeted Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDTAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="75px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sold Amt">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HplgvSAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' Target="_blank"></asp:HyperLink>


                                                            <%-- <asp:Label ID="lgvDSAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDSAmt" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Sold Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDSchdate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                                Width="65px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Team">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgDCper" runat="server" Font-Size="10px"
                                                                Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "conteam"))
                                                                         %>'
                                                                Width="100px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDDisAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDDisAmt" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgDvDisPer" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                                    </asp:Panel>

                                    <asp:Panel ID="pnlinprocess" Visible="false" runat="server">


                                        <div class="col-md-12" style="display: none;">

                                            <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" CssClass="rbtnList1"
                                                RepeatColumns="6"
                                                RepeatDirection="Horizontal" Style="text-align: left" Width="369px">
                                                <asp:ListItem>With Post Dated</asp:ListItem>
                                                <asp:ListItem>Current Dated</asp:ListItem>
                                                <asp:ListItem>Actual Dated</asp:ListItem>
                                            </asp:RadioButtonList>


                                        </div>

                                        <div class="table-responsive col-lg-12">
                                            <asp:GridView ID="grvTrnDatWise" runat="server" AllowPaging="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                AutoGenerateColumns="False"
                                                ShowFooter="True"
                                                OnRowDataBound="grvTrnDatWise_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNomr" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Group Description" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcGrptmr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcProDescmr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUnDesmr" runat="server" Style="text-align: left" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MR No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcMRNomr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Voucher  No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcVoucherNomr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="MR Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcMRDatmr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrdate1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Collection From" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCollFrmmr" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "collfrm")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Received Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcrecptypemr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rectype")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Customer Name ">
                                                        <FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCDTotal" runat="server" Font-Bold="True" Font-Size="10px"
                                                                            ForeColor="Black" Style="text-align: right" Width="80px">Total:</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCDNetTotal" runat="server" Font-Bold="True" Font-Size="10px"
                                                                            ForeColor="Black" Style="text-align: right" Width="80px">Net Total:</asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCuNamemr" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                                Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Cheque No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChNomr" runat="server" Style="text-align: left" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCaAmtmr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cashamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFCashamt" runat="server" Font-Bold="True"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"> </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChAmtmr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chqamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>


                                                        <FooterTemplate>

                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvFChqamt" runat="server" Font-Bold="True"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lgvCDNetTotal" runat="server" Font-Bold="True"
                                                                            ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </FooterTemplate>


                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvChDatmr" runat="server" Style="text-align: left" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqdate")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvBaNomr" runat="server" Style="text-align: left" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Reconciliation Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvRecDat1mr" runat="server" Style="text-align: left" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recndt")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <%--       <asp:TemplateField HeaderText="Bank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbank01mr" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUserNamemr" runat="server" Style="text-align: left" Font-Size="8px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvEntrydatemr" runat="server" Style="text-align: left" Font-Size="8px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entrydat")) %>'
                                                                Width="50px"></asp:Label>
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


                                    </asp:Panel>

                                    <asp:Panel ID="PanelApproved" Visible="false" runat="server">


                                        <div class="table-responsive col-lg-12" style="background: #fff;">

                                            <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                OnRowDataBound="gvSpayment_RowDataBound" PageSize="15">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNosu" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPactdescsu" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="100px" Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCuNamesu" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description of Item">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgcResDescsu" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvditem" runat="server" Text="Total" Font-Bold="True" HorizontalAlign="Left"
                                                                ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Unit ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUnitsu" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Budgeted Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvTqtysu" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFTqty" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budgeted Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvTAmtlgvFTqty" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tuamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFTAmt" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>







                                                    <asp:TemplateField HeaderText="Sold Qty" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvSqty" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px" Style="text-align: right; color: red;"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFSqty" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right; color: red;" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit Size(Sold)" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvsusize" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "susize")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFTsusize" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="40px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rate(Sold)" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvsrate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srate")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Sold Amt" Visible="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lgvSAmt" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px" Style="text-align: right"></asp:Label>--%>

                                                            <asp:HyperLink ID="HplgvAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "suamt")).ToString("#,##0;(#,##0); ") %>' Target="_blank"></asp:HyperLink>

                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFSAmt" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Sold Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvSchdate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                                Width="65px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Unsold Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUsqty" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFUsqty" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit Size(Unsold)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvusize" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFTusize" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Rate(Unsold)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvurate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Unsold Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUsAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usuamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFUsAmt" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Discount" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDisAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDisAmt" runat="server" Font-Bold="True"
                                                                ForeColor="Black" Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDisPer" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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


                                    </asp:Panel>
                                    <asp:Panel ID="pnlReadyDelivery" Visible="false" runat="server">


                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                        </div>



                                    </asp:Panel>
                                    <asp:Panel ID="PanelDelivery" Visible="false" runat="server">


                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                        </div>
                                    </asp:Panel>



                                    <asp:Panel ID="Panelcustdues" Visible="false" runat="server">
                                        <div class="clearfix"></div>

                                        <div class="table-responsive col-lg-12" style="background: #fff;">
                                            <asp:GridView ID="gvcustdues" runat="server" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                AutoGenerateColumns="False"
                                                ShowFooter="True" OnRowDataBound="gvcustdues_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgactdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cutomer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgacuname" runat="server" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))
                                                                         %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Desc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgudesc" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>







                                                    <asp:TemplateField HeaderText="Dues Inst.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDues" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueins")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Previous </br> Dues">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDueAmt" runat="server" Font-Bold="True" Font-Size="10px"
                                                                ForeColor="#000" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "predues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Current Dues">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFcurDueAmt" runat="server" Font-Bold="True" Font-Size="10px"
                                                                ForeColor="#000" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcurDamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curdues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Receivable">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFreceivable" runat="server" Font-Bold="True" Font-Size="10px"
                                                                ForeColor="#000" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvreceivable" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "receivable")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Mr No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvmrno" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                                Width="45px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Mr Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpaiddate" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recdate")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Received">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFpaidamt" runat="server" Font-Bold="True" Font-Size="10px"
                                                                ForeColor="#000" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpaidamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Net Dues">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFnetdues" runat="server" Font-Bold="True" Font-Size="10px"
                                                                ForeColor="#000" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvnetdues" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netdues")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
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


                                    <asp:Panel ID="Panelcustcurdues" Visible="false" runat="server">

                                        <div class="card card-fluid">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        <div class="form-group">
                                                            <label for="lblProjectname" class="control-label  lblmargin-top9px">Project</label>




                                                        </div>

                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class=" form-group">

                                                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass=" form-control chzn-select">
                                                            </asp:DropDownList>

                                                        </div>

                                                    </div>

                                                    <div class="col-md-1">
                                                        <div class="form-group">
                                                            <label for="Label7" id="Label7" class="control-label  lblmargin-top9px">From</label>

                                                        </div>



                                                    </div>

                                                    <div class="col-md-2">

                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass=" form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                                                Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                                        </div>



                                                    </div>

                                                    <div class="col-md-1">
                                                        <div class="form-group">
                                                            <label for="Label8" id="Label8" class="control-label  lblmargin-top9px">To</label>


                                                        </div>


                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                                                CssClass=" form-control"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                                                        </div>

                                                    </div>

                                                    <div class="col-md-1">

                                                        <div class="form-group">
                                                            <asp:LinkButton ID="lnkbtnCurDues" runat="server" CssClass="btn  btn-primary" OnClick="lnkbtnCurDues_Click">Ok</asp:LinkButton>

                                                        </div>


                                                    </div>


                                                </div>


                                            </div>


                                        </div>

                                        <div class="clearfix"></div>
                                        <div class="table-responsive col-lg-12" style="background: #fff;">

                                            <asp:GridView ID="gvcustduescur" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                AutoGenerateColumns="False"
                                                ShowFooter="True"
                                                OnRowDataBound="gvcustduescur_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNocur" runat="server" Font-Bold="True" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgactdesccur" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cutomer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgacunamecur" runat="server"
                                                                Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Desc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgudesccur" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Concern Person">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgCpercur" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cteam")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Installment">

                                                        <FooterTemplate>
                                                            <asp:Label ID="Label6cur" runat="server" Font-Bold="True" ForeColor="#000"
                                                                Text="Grand Total:"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvInstallmentcur" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Schedule Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvschddatecur" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Dues Inst.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDuescur" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueins")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Previous Dues">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDueAmtcur" runat="server" Font-Bold="True"
                                                                ForeColor="#000" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDamtcur" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Current Dues">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFcurDueAmtcur" runat="server" Font-Bold="True"
                                                                ForeColor="#000" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcurDamtcur" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdueamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
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

                                    <asp:Panel ID="Panelcustduesover" Visible="false" runat="server">




                                        <%--  <div class="clearfix"></div>--%>
                                        <div class="table-responsive col-lg-12" style="background: #fff;">



                                            <asp:GridView ID="gvcustduesover" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                AutoGenerateColumns="False"
                                                ShowFooter="True"
                                                OnRowDataBound="gvcustduesover_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNoover" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgactdescover" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cutomer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgacunameover" runat="server"
                                                                Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Desc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgudescover" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Concern Person" Visible="false">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgCperover" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cteam")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Installment">

                                                        <FooterTemplate>
                                                            <asp:Label ID="Label6over" runat="server" Font-Bold="True" ForeColor="#000"
                                                                Text="Grand Total:" Font-Size="9px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvInstallmentover" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Schedule Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvschddateover" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Dues<?br> Inst.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDuesover" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueins")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Previous Dues">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDueAmtover" runat="server" Font-Bold="True"
                                                                ForeColor="#000" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDamtcur" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Current Dues">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFcurDueAmtover" runat="server" Font-Bold="True"
                                                                ForeColor="#000" Width="80px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvcurDamtcur" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdueamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
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
                    </div>
                </div>
            </div>

        </ContentTemplate>



    </asp:UpdatePanel>


</asp:Content>


