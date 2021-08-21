<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLCInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.RptLCInterface" %>

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


        .tbMenuWrp table tr td {
            /*height: 50px;*/
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

        /*.tbMenuWrp table tr td:nth-child(1) {
                background: #4BCF9E;
            }

            .tbMenuWrp table tr td:nth-child(2) {
                background: #92D14F;
            }

            .tbMenuWrp table tr td:nth-child(3) {
                background: #5EB75B;
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #92D14F;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #00AF50;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #E6A549;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #71A3E4;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #ffffff;
            }*/

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
            width: 145px;
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 18px;
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
            text-transform: uppercase;
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
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


        };

    </script>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div class="nahidProgressbar">
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
                                <div class="col-md-3">
                            <div class="form-group">
                                <asp:Panel ID="plncop" runat="server" Visible="false">
                                   
                                        
                                        <asp:DropDownList ID="ddlCompany" CssClass="chzn-select form-control" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                   
                                </asp:Panel>
                              </div>
                            </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="FromDate">From</label>

                                <asp:TextBox ID="txtFDate" runat="server" CssClass="inputDateBox" AutoPostBack="true"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtFDate_CalendarExtender1" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
             </div>
                        </div>       

                          <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="todate">To </label>
                                 <asp:TextBox ID="txtdate" runat="server" CssClass="inputDateBox" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                             </div>
                        </div>
                         
                          <div class="col-md-1">
                            <div class="form-group">
                                                   <asp:LinkButton ID="lnkbtnok"  runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton></li>

                            </div>
                        </div>
                       <div class="col-md-4">

                                    <asp:LinkButton ID="btnSetup" runat="server" CssClass="btn btn-warning"  OnClick="btnSetup_Click">Setting</asp:LinkButton></li>
                            

                            <asp:LinkButton ID="lnkInteface" runat="server" CssClass="btn btn-success"  OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                                    <asp:LinkButton ID="lnkReports" runat="server" CssClass="btn btn-warning"  OnClick="lnkRept_Click">ALL Reports</asp:LinkButton>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/F_12_Inv/PurReqEntry?InputType=LcEntry&prjcode=&genno=" Target="_blank" CssClass="btn btn-success">Lc Requisition</asp:HyperLink>
                              <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger"></asp:Label>

                                </div>
                        <%--<div class="col-md-1">
                            <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Opera</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/StepofOperation?moduleid=14" CssClass="dropdown-item" Style="padding: 0 10px">Go Purchase</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink8" runat="server" Target="_blank" NavigateUrl="~/F_12_Inv/PurReqEntry?InputType=Entry&prjcode=&genno=&comcod=" CssClass="dropdown-item" Style="padding: 0 10px">Create Requsition</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink9" runat="server" Target="_blank" NavigateUrl="~/F_12_Inv/PurReqEntry?InputType=FxtAstEntry&prjcode=&genno=" CssClass="dropdown-item" Style="padding: 0 10px">Store Requsition</asp:HyperLink>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="dropdown-item" Style="padding: 0 10px" OnClick="lnkInteface_Click">Interface</asp:LinkButton></li>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="dropdown-item" Style="padding: 0 10px" Visible="false" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                                                <asp:HyperLink ID="HyperLink10" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurInformation" CssClass="dropdown-item" Style="padding: 0 10px">Dashboard</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink11" runat="server" Target="_blank" NavigateUrl="~/F_12_Inv/PurTopSheetCashPur?Type=Entry&genno=" CssClass="dropdown-item" Style="padding: 0 10px">Top Sheet(Cash)</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink12" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccSubCodeBook?InputType=Supplier" CssClass="dropdown-item" Style="padding: 0 10px">Create Supplier</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink13" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurMktSurvey?Type=SurveyLink" CssClass="dropdown-item" Style="padding: 0 10px">Survey Link</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurMktSurvey?Type=MktSurvey" CssClass="dropdown-item" Style="padding: 0 10px">Comparative Statement</asp:HyperLink>

                                        <asp:HyperLink ID="HyperLink14" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=BgdBal&comcod=&Date1=&Date2=" CssClass="dropdown-item" Style="padding: 0 10px">Budget Tracking</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink15" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk&comcod=&Date1=&Date2=" CssClass="dropdown-item" Style="padding: 0 10px">Purchase Tracking</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink16" runat="server" Target="_blank" NavigateUrl="~/F_99_Allinterface/PurReportInterface" CssClass="dropdown-item" Style="padding: 0 10px">Reports</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                       
                      

                    </div>
                 

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="50000"></asp:Timer>
                            <triggers> <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" /></triggers>

                            <div class="row">
                                <asp:Panel ID="PnlInt" runat="server" Visible="false">
                                    <div id="slSt" class=" col-md-12">
                                        <div class="panel with-nav-tabs panel-primary">
                                            <fieldset class="tabMenu">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <div class="tbMenuWrp nav nav-tabs">
                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="0"></asp:ListItem>
                                                                <asp:ListItem Value="1"></asp:ListItem>
                                                                <asp:ListItem Value="2" style="display:none;"></asp:ListItem>
                                                                <asp:ListItem Value="3" style="display:none;"></asp:ListItem>
                                                                <asp:ListItem Value="4"></asp:ListItem>
                                                                <asp:ListItem Value="5"></asp:ListItem>
                                                                <asp:ListItem Value="6"></asp:ListItem>
                                                                <asp:ListItem Value="7"></asp:ListItem>

                                                            </asp:RadioButtonList>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                            </fieldset>
                                            <div>

                                                <asp:Panel ID="pnlallRec" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                            <asp:GridView ID="gvprobrec" OnRowDataBound="gvprobrec_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Requistion </br> Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Store Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcenter" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                Width="260px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MRF No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Requistion</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>

                                                                            <%-- <asp:LinkButton ID="lnkbtnEdit" runat="server"><span class="glyphicon glyphicon-pencil"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>

                                                                            <%-- <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                            </asp:HyperLink>
                                                                             <asp:LinkButton ID="btnDelRec" Visible="false" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                                            --%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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
                                                </asp:Panel>

                                                <asp:Panel ID="pnlRecApp" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12">
                                                            <asp:GridView ID="gvprobapp" OnRowDataBound="gvprobapp_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvprobno" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcomcod" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <%--  <asp:TemplateField HeaderText="Center Name ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcenter" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centerdesc")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Store Name ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCustname" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                Width="260px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MRF No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Req</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>

                                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                            </asp:HyperLink>

                                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa  fa-check"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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

                                                </asp:Panel>
                                                   <asp:Panel ID="PanCSCrete" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12">
                                                            <asp:GridView ID="gvcscrte" OnRowDataBound="gvcscrte_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Approval Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvappdate" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chkdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Store Name ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCustname" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                Width="260px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MRF No ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvMrfNo" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Req</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>
                                                                            <asp:HyperLink Target="_blank" ID="lnkbtnEdit" runat="server"><span class="glyphicon glyphicon-ok"></span> </asp:HyperLink>



                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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

                                                </asp:Panel>
                                                 <asp:Panel ID="PanCsAprv" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12">
                                                            <asp:GridView ID="gvcsaprv" OnRowDataBound="gvcsaprv_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                  <%--  <asp:TemplateField HeaderText="Approval Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvappdate" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chkdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>--%>

                                                                    <asp:TemplateField HeaderText="Store Name ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCustname" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                Width="260px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MRF No ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvMrfNo" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Req</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>
                                                                            <asp:HyperLink Target="_blank" ID="lnkbtnEdit" runat="server"><span class="glyphicon glyphicon-ok"></span> </asp:HyperLink>



                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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

                                                </asp:Panel>
                                                <asp:Panel ID="PanelAssorted" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12">
                                                            <asp:GridView ID="gvprobass" OnRowDataBound="gvprobass_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Approval Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvappdate" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chkdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Store Name ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCustname" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                Width="260px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MRF No ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvMrfNo" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Req</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>
                                                                            <asp:HyperLink Target="_blank" ID="lnkbtnEdit" runat="server"><span class="fa fa-check"></span> </asp:HyperLink>



                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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

                                                </asp:Panel>
                                                <asp:Panel ID="pnlAssApp" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                                            <asp:GridView ID="gvprobassapp" OnRowDataBound="gvprobassapp_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgReq" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="actcode" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Req </br> Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvReqno" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="110px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvrecddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Approve Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvasrdat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "arpvdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <%--  <asp:TemplateField HeaderText="Center Name ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcenter" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centerdesc")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Store Name ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCustname" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stordesc")) %>'
                                                                                Width="260px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lc Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                                Width="150px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Order</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvordqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Received</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvrcvqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQCTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>


                                                                            <asp:HyperLink Target="_blank" ID="lnkbtnEdit" runat="server"><span class="fa fa-check"></span> </asp:HyperLink>
                                                                            <%--<asp:LinkButton ID="lnkbtnView" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>

                                                                            <%--   <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                            </asp:HyperLink>--%>
                                                                            <asp:LinkButton ID="btnDelRcv" OnClick="btnDelRcv_Click" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><span style="color:red" class="fa fa-trash"></span> </asp:LinkButton>

                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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


                                                </asp:Panel>

                                                <asp:Panel ID="Pnlbill" Visible="false" runat="server">
                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                            <asp:GridView ID="gvprobbill" OnRowDataBound="gvprobbill_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="lc code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvlccode" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Lc Rec </br> Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvrcvNo" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvno")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <%--    <asp:TemplateField HeaderText="Assorted Approve">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvasrappdat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "asrappdate")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>--%>
                                                                    <%--<asp:TemplateField HeaderText="Center Name ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcenter" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centerdesc")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Store Name ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCustname" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stordesc")) %>'
                                                                                Width="160px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lc Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                                Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Receive</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvRcvqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="QC</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvqcqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQCTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>
                                                                            <asp:HyperLink Target="_blank" ID="HypPreEdit" runat="server"><i class="fa fa-edit" aria-hidden="true"></i> </asp:HyperLink>
                                                                            <asp:HyperLink Target="_blank" ID="lnkbtnEdit" runat="server"><i class="fa fa-check" aria-hidden="true"></i> </asp:HyperLink>


                                                                            <%--  <asp:LinkButton ID="lnkbtnView" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>

                                                                            <%--   <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                            </asp:HyperLink>--%>
                                                                            <asp:LinkButton ID="btnDelQC" OnClick="btnDelQC_Click" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><span style="color:red" class="glyphicon glyphicon-remove"></span> </asp:LinkButton>

                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
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
                                                </asp:Panel>
                                                <asp:Panel ID="PnlCosting" Visible="false" runat="server">
                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                            <asp:GridView ID="gvCosting" OnRowDataBound="gvCosting_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grrno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="lc code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvlccode" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="GRR </br> Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvgrrno" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grrno")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qc Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "qcdate")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Store Name ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCustname" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stordesc")) %>'
                                                                                Width="230px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lc Number">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                                Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="QC</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>
                                                                            <asp:HyperLink ID="HypbtnEdit" Width="30" runat="server" CssClass="btn btn-xs btn-success" Target="_blank"><i class="fa fa-edit"  aria-hidden="true"></i>
                                                                            </asp:HyperLink>
                                                                            <asp:HyperLink Target="_blank" Width="30" ID="lnkbtnForward" CssClass="btn btn-xs btn-default" runat="server"><i class="fa fa-check"  aria-hidden="true"></i> </asp:HyperLink>

                                                                            <%--  <asp:LinkButton ID="lnkbtnView" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>


                                                                            <asp:LinkButton ID="btnDelRec" Width="30" OnClick="btnDelRec_Click" CssClass="btn btn-xs btn-danger" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><i class="fa fa-trash"  aria-hidden="true"></i> </asp:LinkButton>

                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
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
                                                </asp:Panel>



                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>


                                <asp:Panel ID="pnlReprots" runat="server">

                                    <asp:Panel ID="plnMgf" runat="server" Visible="false">
                                        <div class="form-group">

                                            <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                                <ul class="nav colMid " id="SERV">
                                                    <li>
                                                        <a href="<%=this.ResolveUrl("~/F_09_LCM/RptLCStatus?Type=LCCosting")%> " target="_blank">01. LC COSTING REPORT</a>
                                                    </li>
                                                    <li>
                                                        <a href="<%=this.ResolveUrl("~/F_09_LCM/RptLCPosition?Type=LCPosition")%> " target="_blank">01. LC STATUS REPORT</a>
                                                    </li>
                                                    <li>
                                                        <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalSummery?Type=LcCost")%> " target="_blank">01. LC OVERALL COSTING</a>
                                                    </li>
                                                    <li>
                                                        <a href="<%=this.ResolveUrl("~/F_09_LCM/RptLCStatus?Type=LCVari")%> " target="_blank">01. LC VARIANCE REPORTS</a>
                                                    </li>
                                                    <li>
                                                        <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalSummery?Type=LcReceive")%> " target="_blank">01. LC RECEIVED REPORTS</a>
                                                    </li>

                                                </ul>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PnlSalesSetup" runat="server" Visible="false">
                                        <div class="form-group">

                                            <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                                <ul class="nav colMid ">
                                                   
                                                    <li>

                                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/PurReqEntry02?InputType=LCEntry&comcod=&actcode=&genno=")%> " target="_blank">01.L/C REQUISITION INFORMATION</a>
                                                    </li>
                                                    <li>

                                                        <a href="<%=this.ResolveUrl("~/F_09_LCM/LCAllInfo?Type=All")%> " target="_blank">02.LC OPENING</a>
                                                    </li>
                                                    <li>

                                                        <a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation?tname=receive&tid=lc")%> " target="_blank">03. IMPORT MATERIAL RECIVED</a>
                                                    </li>
                                                    <li>

                                                        <a href="<%=this.ResolveUrl("~/F_09_LCM/LCCostingDetails?Type=Entry")%> " target="_blank">04. IMPORT MATERIAL COSTING</a>
                                                    </li>
                                                    <li>

                                                        <a href="<%=this.ResolveUrl("~/F_09_LCM/StandardMatCost")%> " target="_blank">05. STANDARD IMPORTED COSTING</a>
                                                    </li>

                                                     <li>

                                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey?Type=SurveyLink")%> " target="_blank">06. Survey Link</a>
                                                    </li>


                                                </ul>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>



                            </div>




                            <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
                            <script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>
                            <script src="../Scripts/jquery.counterup.min.js"></script>
                            <script>
                                jQuery(document).ready(function ($) {
                                    $('.counter').counterUp({
                                        delay: 10,
                                        time: 1000
                                    });
                                });
                            </script>

                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>




            </div>



        </ContentTemplate>



    </asp:UpdatePanel>

    <%-- <Triggers>

<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>

</Triggers>--%>

    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>


