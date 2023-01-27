<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BillRegInterface.aspx.cs" Inherits="RealERPWEB.F_15_DPayReg.BillRegInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <style type="text/css">
        .grvHeader th {
            text-align: center;
        }

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

        .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
            background: #12A5A6;
            color: #fff;
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
            background: #fff none repeat scroll 0 0;
            border: 1px solid #000;
            color: #fff;
            cursor: pointer;
            float: left;
            height: 63px;
            list-style: outside none none;
            margin: 0 5px 0 0;
            padding: 0;
            position: relative;
            text-align: center;
            width: 150px;
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
                background: #CEFEB4;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #90FDD4;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
            }



        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
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
            top: 5px;
        }

        span.lbldata2 {
            /*background: #e5dcdd !important;*/
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
            background: #12A5A6;
            color: red;
        }

        .lblactive label span.lbldata2 {
            background: #003cb3 !important;
            color: #fff !important;
            font-size: 14px;
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }



        .fan {
            /*border: 2px solid #f3b728;*/
            border-radius: 50%;
            display: inline-block;
            float: left;
            font-size: 16px;
            padding: 5px;
        }

            .fan:nth-child(1) {
                background-color: #ffffff !important;
                color: #E48F23 !important;
            }

            .fan:nth-child(2) {
                color: #52B641 !important;
                background-color: #56B740 !important;
            }

            .fan:nth-child(3) {
                color: #085407;
                background: #085407 !important;
            }

            .fan:nth-child(4) {
                color: #fff;
                background: #DA3F40 !important;
            }

            .fan:nth-child(5) {
                color: #fff;
                background: #009BFF !important;
            }

            .fan:nth-child(6) {
                color: #E4DDDD;
                background: #539250 !important;
            }

            .fan:nth-child(7) {
                color: #E4DDDD;
                background: #E79956 !important;
            }
    </style>

    <%--<script>
        $(document).ready(function () {
            $("#slSt").load("~/F_23_SaM/SalesInterface");
            var refreshId = setInterval(function () {
                $("#slSt").load('~/F_23_SaM/SalesInterface?randval=' + Math.random());
            }, 10000);
            $.ajaxSetup({ cache: false });
        });



        //var refreshId = setInterval(function()
        //{
        //    $(‘#responsecontainer’).load(‘response.php’);
        //    $(‘#responsecontainer2’).load(‘response2.php’);
        //}, 60000);

        //$(document).ready(function()
        //{
        //    $(‘#responsecontainer’).fadeOut(“slow”).load(‘response.php’).fadeIn(“slow”);
        //    $(‘#responsecontainer2’).fadeOut(“slow”).load(‘response2.php’).fadeIn(“slow”);
        //});
    </script>--%>
    <%--<script src="../Scripts/waypoints.min.js"></script>
    <script src="../Scripts/jquery.counterup.min.js"></script>--%>

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

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                $('#<%=this.gvBillInfo.ClientID %>').tblScrollable();
                $('#<%=this.gvChequeSign.ClientID %>').tblScrollable();
                $('#<%=this.gvforward.ClientID %>').tblScrollable();
                $('#<%=this.grvComp.ClientID %>').tblScrollable();
                $('#<%=this.grvApproved.ClientID %>').tblScrollable();
                $('#<%=this.grvIssued.ClientID %>').tblScrollable();
                $('#<%=this.grvRecm.ClientID %>').tblScrollable();


            }

            catch (e) {

                alert(e.message);
            }


        };

        function FunCheckedBill(url) {
            window.open('' + url + '', '_blank');
        }

        function FunForwordBill(url) {
            window.open('' + url + '', '_blank');
        }

        function FunApprovedBill(url) {
            window.open('' + url + '', '_blank');
        }
<%--        function Search_Gridview(strKey, cellNr, gvname) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                var tbldata;

                switch (gvname) {

                    case 'gvPurchase':
                        tblData = document.getElementById("<%=this.gvBillInfo.ClientID %>");
                        break;

                    case 'gvContUpdate':
                        tblData = document.getElementById("<%=this.grvRecm.ClientID %>");
                        break;

                    default:
                        tblData = document.getElementById("<%=this.gvforward.ClientID %>");

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

        }--%>
        function Search_Gridview2(strKey) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                /*alert()*/
                var tbldata;
                var rbtn = $("input[name$='RadioButtonList1']:checked").val();


                switch (rbtn) {

                    case '0':
                        tblData = document.getElementById("<%=this.gvBillInfo.ClientID %>");
                        break;
                    case '1':
                        tblData = document.getElementById("<%=this.grvRecm.ClientID %>");
                        break;
                    case '2':
                        tblData = document.getElementById("<%=this.gvforward.ClientID %>");
                        break;
                    case '3':
                        tblData = document.getElementById("<%=this.grvApproved.ClientID %>");
                        break;
                    case '4':
                        tblData = document.getElementById("<%=this.grvIssued.ClientID %>");
                        break;
                    case '5':
                        tblData = document.getElementById("<%=this.gvChequeSign.ClientID %>");
                        break;
                    case '6':
                        tblData = document.getElementById("<%=this.grvComp.ClientID %>");
                        break;
                  

                }

                var rowData;
                for (var i = 0; i < tblData.rows.length; i++) {
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

            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="30000">
            </asp:Timer>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label lblmargin-top9px" for="FromDate">Date</label>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..." onkeyup="Search_Gridview2(this)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label lblmargin-top9px" for="FromDate">Total Bill</label>



                            </div>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:Label ID="lblbill" runat="server" CssClass=" form-control "></asp:Label>


                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label lblmargin-top9px" for="FromDate">Pending Tk</label>


                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">

                                <asp:Label ID="lblpanding" runat="server" CssClass="form-control"></asp:Label>

                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" CssClass="dropdown-item" Visible="false" NavigateUrl="~/F_15_DPayReg/AccOnlinePaymntPlan">Create Proposal</asp:HyperLink>

                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_15_DPayReg/RptBillStatusInf?Type=Report&comcod=">Show Bills</asp:HyperLink>

                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_15_DPayReg/AccOnlinePaymnt">Proposal</asp:HyperLink>

                                        <asp:HyperLink ID="hlnkbillgraph" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_15_DPayReg/RptPaymentGraph">Bills(Graph)</asp:HyperLink>
                                        <div class="dropdown-divider"></div>
                                        <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_17_Acc/RptDailyTransCashBank">Payment Proposal</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk">Purchase Tracking</asp:HyperLink>
                                        <asp:HyperLink ID="hlnkvoucher360" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_17_Acc/AllVoucherTopSheet">Voucher 360<sup>0</sup></asp:HyperLink>
                                        <asp:HyperLink ID="hlnkchequeprint" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_17_Acc/TransectionPrint?Type=AccCheque&Mod=Accounts">Cheque Print</asp:HyperLink>
                                        <asp:HyperLink ID="hlnkpchequeprint" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_17_Acc/TransectionPrint?Type=AccPostDatChq">Post Dated Cheque Print</asp:HyperLink>

                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>




                    <%--  </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">--%>
                    <div class="row">

                        <div class="panel with-nav-tabs panel-primary">
                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="tbMenuWrp nav nav-tabs">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0"></asp:ListItem>
                                                <asp:ListItem Value="1"></asp:ListItem>
                                                <asp:ListItem Value="2"></asp:ListItem>
                                                <asp:ListItem Value="3"></asp:ListItem>
                                                <%-- Added--%>
                                                <%--                                                <asp:ListItem Value="7"></asp:ListItem>--%>

                                                <asp:ListItem Value="4"></asp:ListItem>
                                                <asp:ListItem Value="5"></asp:ListItem>
                                                <asp:ListItem Value="6"></asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div>

                                <asp:Panel ID="pnlBillInfo" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvBillInfo" runat="server" cl OnRowDataBound="gvBillInfo_RowDataBound" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="actcode#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvactcode" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1paym" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>
                                                            <%--   <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="95px"></asp:Label>--%>

                                                            <asp:HyperLink ID="hypBill" Visible="false" runat="server" Target="_blank" ForeColor="Blue" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>' Width="90"></span>
                                                            </asp:HyperLink>



                                                            <%# Eval("billno2") %>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypRq5" runat="server" Target="_blank" ForeColor="Blue" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="90"></span>
                                                            </asp:HyperLink>


                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1bil" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="450px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotal" runat="server" Style="text-align: Right"
                                                                Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountrec" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Amt.">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdate" runat="server" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>



                                                    <%--<asp:TemplateField HeaderText="Curent Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle   HorizontalAlign="Left" />
                                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class=" fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class=" fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" Visible="false" runat="server" Target="_blank" CssClass="btn btn-xs btn-default" ForeColor="Black" Font-Underline="false"><span class="fa fa-edge"></span>
                                                            </asp:HyperLink>


                                                            <asp:LinkButton ID="btnDelOrder" Visible="false" runat="server"><span style="color:red" class="  fa fa-recycle"></span> </asp:LinkButton>
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

                                <asp:Panel ID="PnlRecm" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvRecm" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvRecm_RowDataBound" OnRowDeleting="grvRecm_RowDeleting">
                                                <RowStyle />
                                                <Columns>



                                                    <%--<asp:TemplateField HeaderText="Curent Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle   HorizontalAlign="Left" />
                                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="actcode#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvactcode" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:CommandField ShowDeleteButton="True" />


                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1payma" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>






                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>
                                                            <%# Eval("billno2") %>

                                                            <asp:Label ID="lbgvbillno" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Width="95px"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprReqno11" runat="server" Target="_blank" ForeColor="Blue" Font-Size="10px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="80"></span>
                                                            </asp:HyperLink>

                                                            <%--<asp:Label ID="lbgvreqno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                            Width="90px"></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) 
                                                                        %>'
                                                                Width="400px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotal" runat="server" Style="text-align: Right"
                                                                Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Resource </br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountrecm" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Bill Amount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" CssClass="btn btn-xs btn-default" ForeColor="Blue" Font-Underline="false"><span class=" fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-xs btn-default" ForeColor="Black" Font-Underline="false"><span  class=" fa fa-check "></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" Visible="false" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="btnDelProposal" OnClick="btnDelProposal_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle" ></span> </asp:LinkButton>


                                                            <asp:LinkButton ID="btnDelOrder" Visible="false" runat="server" CssClass="btn btn-xs btn-default"><span style="color:red" class="  fa fa-recycle "></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="110px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />


                                                    </asp:TemplateField>

                                                    <asp:TemplateField>

                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllCheckid" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllCheckid_CheckedChanged" Width="20px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCheckid" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                                                Width="20px" />
                                                        </ItemTemplate>

                                                        <FooterTemplate>

                                                            <asp:LinkButton ID="lnkbtnChekedId" runat="server" OnClientClick="return FunAppConfirm();"
                                                                OnClick="lnkbtnChekedId_Click" ToolTip="Cheked"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                                            <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
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


                                <asp:Panel ID="pnlforward" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvforward" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvforward_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNofr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="actcode#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvactcodefr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqnoBillfr" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1fr" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>

                                                            <%# Eval("billno2") %>

                                                            <asp:Label ID="lbgvbillnofr" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbgvreqnofr" runat="server" Target="_blank" ForeColor="Blue" Font-Size="10px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="95"></span>
                                                            </asp:HyperLink>


                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldatefr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdescfr" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) 
                                                                        %>'
                                                                Width="380px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotal" runat="server" Style="text-align: Right"
                                                                Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <HeaderStyle Font-Size="12px" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Narration">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvnarrationfr" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; font-size: 10px; background-color: Transparent" Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "billrmrks")) 
                                                                        %>'
                                                                Width="240px"></asp:Label>
                                                        </ItemTemplate>



                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountrecmfr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Bill Amtount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotalfr" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamtfr" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdatefr" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>



                                                    <%--<asp:TemplateField HeaderText="Curent Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle   HorizontalAlign="Left" />
                                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrintfr" runat="server" Target="_blank" CssClass="btn btn-xs btn-default" ForeColor="Blue" Font-Underline="false"><span class=" fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <%--   <asp:LinkButton ID="btnDelChecked" OnClick="btnDelChecked_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle" ></span> </asp:LinkButton>--%>

                                                            <%--  <asp:HyperLink ID="lnkbtnEditINfr" Visible="false" runat="server"  CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


                                                            <asp:LinkButton ID="btnDelOrderfr" Visible="false" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="lnkbtnEntryfr" runat="server" Target="_blank" CssClass="btn btn-xs btn-default" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="btnDelChecked" OnClick="btnDelChecked_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle" ></span> </asp:LinkButton>

                                                            <%--  <asp:HyperLink ID="lnkbtnEditINfr" Visible="false" runat="server"  CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


                                                            <asp:LinkButton ID="btnDelOrderfr" Visible="false" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllforwordid" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkAllforwordid_CheckedChanged" Width="20px" />

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkforwordid" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                                                Width="20px" />
                                                        </ItemTemplate>

                                                        <FooterTemplate>

                                                            <asp:LinkButton ID="lnkbtnForword" runat="server" OnClientClick="return FunAppConfirm();"
                                                                OnClick="lnkbtnForword_Click" ToolTip="Forword"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                                            <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
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

                                <asp:Panel ID="PanelApproved" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvApproved" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvApproved_RowDataBound" Width="1070px">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="actcode#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvactcode" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpayidapp" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="70px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>

                                                            <%# Eval("billno2") %>

                                                            <asp:Label ID="lbgvbillno" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>

                                                            <%# Eval("orderno") %>

                                                            <%-- <asp:Label ID="lbgvbillno" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="90px"></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbgvreqno" runat="server" Target="_blank" ForeColor="Blue" Font-Size="10px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="95"></span>
                                                            </asp:HyperLink>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreBillid2" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="70px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotal" runat="server" Style="text-align: Right"
                                                                Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <HeaderStyle Font-Size="12px" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountapp" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Amt.">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apdate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>



                                                    <%--<asp:TemplateField HeaderText="Curent Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle   HorizontalAlign="Left" />
                                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class=" fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class=" fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="btnDelForward" OnClick="btnDelForward_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle" ></span> </asp:LinkButton>

                                                            <asp:HyperLink ID="lnkbtnEditIN" Visible="false" runat="server" Target="_blank" CssClass="btn btn-xs btn-default" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


                                                            <asp:LinkButton ID="btnDelOrder" Visible="false" runat="server" CssClass="btn btn-xs btn-default"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="140px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllpayid" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkAllpayid_CheckedChanged" Width="20px" />

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkpayid" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                                                Width="20px" />
                                                        </ItemTemplate>

                                                        <FooterTemplate>

                                                            <asp:LinkButton ID="lnkbtnPayId" runat="server" OnClientClick="return FunAppConfirm();"
                                                                OnClick="lnkbtnPayId_Click" ToolTip="Approved"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                                            <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
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


                                <asp:Panel ID="PnlIssued" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvIssued" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvIssued_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="actcode#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvactcode" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                           <asp:LinkButton ID="lnkbtnSplit" runat="server" OnClientClick="return FunConfirmSplit();"
                                                                OnClick="lnkbtnSplit_Click" ToolTip="Split" CssClass="btn  btn-default btn-sm"> <i class="fas fa-minus-square"></i> </span> </asp:LinkButton>




                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno12" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkmerge" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                                                Width="20px" />
                                                        </ItemTemplate>

                                                        <FooterTemplate>

                                                            <asp:LinkButton ID="lnkbtnMerge" runat="server" OnClientClick="return FunConfirmMerge();"
                                                                OnClick="lnkbtnMerge_Click" ToolTip="Merge" CssClass="btn  btn-default btn-sm"> <i class="fas fa-plus-square"></i> </span> </asp:LinkButton>


                                                            

                                                            <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>






                                                    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="hypreno2" runat="server" Target="_blank" ForeColor="Blue" Font-Size="10px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="95"></span>
                                                            </asp:HyperLink>



                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>
                                                            <%# Eval("billno2") %>

                                                            <asp:Label ID="lbgvbillno" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBillidissu" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="400px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltotal" runat="server" Style="text-align: Right"
                                                                Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <HeaderStyle Font-Size="12px" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountissued" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Amtount ">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issuedamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvappisedate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appisedate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>




                                                    <%--<asp:TemplateField HeaderText="Curent Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle   HorizontalAlign="Left" />
                                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class=" fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <%-- Added by nIme --%>
                                                            <asp:LinkButton ID="btnDelApproval" OnClick="btnDelApproval_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle" ></span> </asp:LinkButton>
                                                            <%--  End--%>
                                                            <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                </asp:HyperLink>--%>

                                                            <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" CssClass="btn btn-xs btn-default"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                            <asp:LinkButton ID="btnDelCheque" OnClick="btnDelCheque_Click" OnClientClick="javascript:return FunConfirm();" runat="server" ToolTip="Delete" Visible="false"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>





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
                                <asp:Panel ID="pnlChequeSign" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvChequeSign" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno13" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno13" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion" Visible="false">
                                                        <ItemTemplate>

                                                            <%--  <asp:HyperLink ID="hypreno2" runat="server" Target="_blank" ForeColor="Blue" Font-Size="10px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="80"></span>
                                                                </asp:HyperLink>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>


                                                            <asp:Label ID="lbgvreqno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Voucher No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrVoNo" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Party Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvresdesccsign" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))    %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Approved Amtount ">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cheque No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvissnochq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checqno"))  %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBnkname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankinf"))  %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Cheque <br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checqdt")) %>'
                                                                Width="80px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prepared By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preparedid")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Recommendation By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recomid")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "appovedid")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ID #" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sessionid")) %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <%--<asp:TemplateField HeaderText="Curent Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle   HorizontalAlign="Left" />
                                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class=" fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class=" fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                </asp:HyperLink>--%>


                                                            <asp:LinkButton ID="btnDelOrder" Visible="false" runat="server" CssClass="btn btn-xs btn-default"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>
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
                                <asp:Panel ID="PnlComp" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:Label ID="lblmsg" runat="server" Font-Size="12px" Style="font-size: 12px" CssClass="pull-right" Width="100px"></asp:Label>

                                            <asp:GridView ID="grvComp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvComp_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNocpay" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1cpayrID" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion" Visible="false">
                                                        <ItemTemplate>

                                                            <%--  <asp:HyperLink ID="hypreno2" runat="server" Target="_blank" ForeColor="Blue" Font-Size="10px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="80"></span>
                                                                </asp:HyperLink>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>



                                                            <asp:Label ID="lbgvreqnocpayw" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="95px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1cpayq" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Voucher No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1cpayrVon" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesccpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Party Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgresdesccpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))    %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Approved Amt.">
                                                        <%-- <FooterTemplate>
                                                                        <asp:Label ID="lblFTotalcpay" runat="server" ForeColor="White"></asp:Label>
                                                                    </FooterTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamtcpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cheque No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvissnochqcpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checqno"))  %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBnknamecpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankinf"))  %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Cheque Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldatecpay" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checqdt")) %>'
                                                                Width="80px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prepared By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesigcpbypay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preparedid")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Recommendation By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesigcrbypay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recomid")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesigcabypay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "appovedid")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <%-- <asp:TemplateField HeaderText="ID #">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbgvusrdesigcpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sessionid")) %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle   Font-Size="12px" />
                                                                </asp:TemplateField>--%>


                                                    <%--<asp:TemplateField HeaderText="Curent Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle   HorizontalAlign="Left" />
                                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>

                                                    <%--  <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkitem" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="1" %>'
                                                    Width="20px" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                            </HeaderTemplate>
                                        </asp:TemplateField>--%>

                                                    <asp:TemplateField>

                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkAll_CheckedChanged" Text=" ALL" />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="true"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "CQPAYTPPARTY"))=="True" %>' />



                                                            <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                </asp:HyperLink>--%>


                                                            <asp:LinkButton ID="btnDelOrder" Visible="false" runat="server"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnUpdate11" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate11_Click">Update</asp:LinkButton>

                                                        </FooterTemplate>

                                                        <ItemStyle Width="50px" />
                                                        <HeaderStyle HorizontalAlign="center" Width="50px" VerticalAlign="Top" />
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
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>
</asp:Content>

