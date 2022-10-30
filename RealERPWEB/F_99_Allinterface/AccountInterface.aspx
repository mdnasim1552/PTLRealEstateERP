<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AccountInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.AccountInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style type="text/css">
        .InBox {
            color: red !important;
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
        /* Chrome, Safari, Opera */
        @-webkit-keyframes example {
            from {
                background-color: pink;
            }

            to {
                background-color: brown;
            }
        }

        /* Standard syntax */
        @keyframes example {
            from {
                background-color: pink;
            }

            to {
                background-color: brown;
            }
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
            /*height: 50px;*/
            height: 36px;
            width: 100%;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 1px 1px;
            color: #fff;
            text-align: center;
            border: 2px solid #9752A2;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
        }




        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            cursor: pointer;
            font-family: Calibri, Arial;
            /*width: 130px;*/
            /*background: whitesmoke;*/
            border-radius: 25px;
            color: #000;
            font-weight: bold;
            padding: 0 0 0 4px;
            line-height: 30px;
            margin: 1px 2px;
            display: block;
            text-align: left;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                background: #12A5A6;
                color: #fff;
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
        }

        .tbMenuWrp table tr td label span.lbldata {
            background: #F3B728;
            border: 1px solid #F3B728;
            border-radius: 50%;
            /*display: block;*/
            height: 32px;
            font-size: 11px;
            font-family: Calibri, Arial;
            line-height: 18px;
            margin: 0 5px 0 0;
            padding: 4px 1px;
            width: 35px;
            float: left;
            text-align: center;
        }

        .tbMenuWrp table tr td label .lblactive {
            background: #12A5A6;
            color: #fff;
        }

        .grvContentarea tr td:last-child {
            width: 120px;
        }
        .grvContentarea {}
    </style>














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


                $("input, select")
                    .bind("keydown",
                        function (event) {
                            var k1 = new KeyPress();
                            k1.textBoxHandler(event);
                        });


                $('#dgPdc').gridviewScroll({
                    width: 1160,
                    height: 420,
                    arrowsize: 30,
                    railsize: 16,
                    barsize: 8,
                    varrowtopimg: "../Image/arrowvt.png",
                    varrowbottomimg: "../Image/arrowvb.png",
                    harrowleftimg: "../Image/arrowhl.png",
                    harrowrightimg: "../Image/arrowhr.png",
                    freezesize: 4


                });

                $('#<%=this.gvSalesUpdate.ClientID%>').tblScrollable();
                $('#<%=this.gvchqdeposit.ClientID%>').tblScrollable();
                $('#<%=this.gvCollUpdate.ClientID%>').tblScrollable();
                $('#<%=this.gvPurchase.ClientID%>').tblScrollable();
                $('#<%=this.gvContUpdate.ClientID%>').tblScrollable();
                $('#<%=this.gvMatTrasfer.ClientID%>').tblScrollable();
                $('#<%=this.gvBankRec.ClientID%>').tblScrollable();
                $('#<%=this.dgPdc.ClientID  %>').tblScrollable();
                $('#<%=this.gvClientMod.ClientID  %>').tblScrollable();



                function FunConfirm() {
                    if (confirm('Are you sure you want to delete this  Item?')) {
                        return;
                    } else {
                        return false;
                    }
                }

            }


            catch (e) {
                alert(e);
            }

        };



        function Search_Gridview(strKey, cellNr, gvname) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                var tbldata;

                switch (gvname) {

                    case 'gvPurchase':
                        tblData = document.getElementById("<%=this.gvPurchase.ClientID %>");
                        break;

                    case 'gvContUpdate':
                        tblData = document.getElementById("<%=this.gvContUpdate.ClientID %>");
                        break;

                    default:
                        tblData = document.getElementById("<%=gvPurchase.ClientID %>");

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


        function Search_Gridview2(strKey) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                /*alert()*/
                var tbldata;
                var rbtn = $("input[name$='RadioButtonList1']:checked").val();


                switch (rbtn) {

                    case '0':
                        tblData = document.getElementById("<%=this.gvSalesUpdate.ClientID %>");
                        break;
                    case '1':
                        tblData = document.getElementById("<%=this.gvchqdeposit.ClientID %>");
                        break;
                    case '2':
                        tblData = document.getElementById("<%=this.gvCollUpdate.ClientID %>");
                        break;
                    case '3':
                        tblData = document.getElementById("<%=this.gvPurchase.ClientID %>");
                        break;
                    case '4':
                        tblData = document.getElementById("<%=this.gvContUpdate.ClientID %>");
                        break;
                    case '5':
                        tblData = document.getElementById("<%=this.gvMatTrasfer.ClientID %>");
                        break;

                    case '6':
                        tblData = document.getElementById("<%=this.dgPdc.ClientID %>");
                        break;
                    case '7':
                        tblData = document.getElementById("<%=this.gvBankRec.ClientID %>");
                        break;

                    case '8':
                        tblData = document.getElementById("<%=this.gvClientMod.ClientID %>");
                        break;
                    case '9':
                        tblData = document.getElementById("<%=this.gvOthBillUp.ClientID %>");
                        break;
                    case '10':
                        tblData = document.getElementById("<%=this.gvlsdUp.ClientID %>");
                        break;
                    case '11':
                        tblData = document.getElementById("<%=this.gvpetty.ClientID %>");
                        break;
                    case '12':
                        tblData = document.getElementById("<%=this.gvAccUnPosted.ClientID %>");
                        break;
                    case '13':
                        tblData = document.getElementById("<%=this.grvMatConversion.ClientID %>");
                        break;

                    case '14':
                        tblData = document.getElementById("<%=this.gvAccUnPostedtrn.ClientID %>");
                        break;


                    case '15':
                        tblData = document.getElementById("<%=this.gvIndAp.ClientID %>");
                        break;



                    default:
                        tblData = document.getElementById("<%=this.gvMatIssue.ClientID %>");
                        break;
                }

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

            catch (e) {
                alert(e.message);

            }

        }

    </script>




    <%-- <asp:ObjectDataSource ID="source_session_online" runat="server" SelectMethod="session_online" TypeName="t_session" />--%>



    <%--<asp:Button ID="Button1" runat="server" Text="Refresh" OnClick="btn_refresh_Click" />--%>



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
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..." onkeyup="Search_Gridview2(this)"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control" placeholder="Ref. No" Visible="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary " OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>

                            </div>
                        </div>

                        <div class="col-md-1">

                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="lnkInteface" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/RptCashFlow">Transactions</asp:HyperLink>
                                        <%-- <asp:LinkButton ID="lnkReports" runat="server" CssClass="dropdown-item" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton>--%>
                                        <asp:HyperLink ID="LinkButton1" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/GeneralAccounts?Mod=Accounts&vounum=">Voucher Entry</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/SuplierPayment?tcode=99&tname=Payment Voucher&Mod=Accounts">Supplier Payment</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/TransectionPrint?Type=AccVoucher&Mod=Accounts">Voucher Print</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink5" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/TransectionPrint?Type=AccCheque&Mod=Accounts">Cheque Print</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink6" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/SupProposeBill?Type=Mgt">Supplier Proposed Payment</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/SupConProposeBill?Type=Mgt">Sub-Contractor Proposed Payment</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_18_MAcc/AccDashBoard">Dashboard</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink7" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/TransectionPrint?Type=AccPostDatChq">Post Dated Cheque Print</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink8" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~//F_17_Acc/AllVoucherTopSheet">Voucher 360 <sup>0</sup></asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink9" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/SuplierPaymentPost?tcode=99&tname=Payment Voucher&Mod=Accounts">Supplier Payment(Post Dated)</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink23" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccPayUpdate?Type=AccIsu">Post Dated Voucher Update</asp:HyperLink>



                                    </div>
                                </div>
                            </div>
                        </div>


                        <%--  </div>--%>


                        <div class="col-md-1 offset-md-1">

                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Reports</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop5" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop5">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink10" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccFinalReports?RepType=BS&comcod=">Statement Of Financial Position</asp:HyperLink>

                                        <asp:HyperLink ID="HyperLink11" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccFinalReports?RepType=IS&comcod=">Statement Of Comprehensive Income</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink12" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/RptBankCheque?Type=CashFlow">Statement Of Cash Flow</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink13" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccTrialBalance?Type=Details&comcod=">Notes: Financial Postion</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink14" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccTrialBalance?Type=INDetails&comcod=">Notes: Comprehensive Income</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink15" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/RptAccDTransaction?Type=Accounts&TrMod=RecPay&comcod=">Receipts & Payment</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink16" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccTrialBalance?Type=Mains&comcod=">Trial Balance</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink17" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccBankRecon?Type=Acc">Bank Reconcillation Statement</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink18" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/RptAccDayTransData">Daily transaction- All</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink19" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccLedgerAll">Accounts Ledger- All</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink20" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccControlSchedule?Type=Type01">Accounts Control Schedule</asp:HyperLink>


                                        <asp:HyperLink ID="HyperLink21" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_17_Acc/AccDetailsSchedule">Accounts Details Schedule</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink22" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_34_Mgt/OtherReqEntry?Type=OreqEdit&prjcode=&genno=">Other Requisition</asp:HyperLink>




                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>









                    <%--   </div>
                </div>--%>

                    <%--  <div class="card card-fluid">
                <div class="card-body">--%>
                    <div class="row">
                        <%--<asp:Panel ID="pnlInterAcc" runat="server">--%>
                        <div class="col-md-2">


                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="tbMenuWrp nav nav-tabs">
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
                                                <asp:ListItem Value="16"></asp:ListItem>

                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>


                        </div>
                        <div id="slSt" class=" col-md-9" style="float: left; margin-left: 0px;">
                            <div class="panel with-nav-tabs panel-primary">

                                <div>

                                    <asp:Panel ID="pnlUpSales" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 550px;">

                                                <asp:GridView ID="gvSalesUpdate" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvSalesUpdate_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusircode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Project Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcentrdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="140px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcustdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usirdesc")) %>'
                                                                    Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmunit" runat="server" BackColor="Transparent" Font-Size="9px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                                    Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit </Br> Size">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunitamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unitsize")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblINAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Unit </Br> Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitmamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unitamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblINAmtTotal1" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Sold Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvsolddate" runat="server" Style="text-align: left; width: 70px;"
                                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"":
                                                                        (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "schdate")).ToString("dd-MMM-yyyy"))%>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Schedule Desc">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvschdesc" runat="server" Style="text-align: center; width: 120px;"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdesc")) %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle HorizontalAlign="center" Width="120px" />
                                                            <FooterStyle HorizontalAlign="left" Font-Bold="true" />
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldgno" runat="server" Style="text-align: center; width: 120px;" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno")) %>'></asp:Label>
                                                                <asp:Label ID="lblsstaus" runat="server" Style="text-align: center; width: 120px;"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sstatus")) %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle HorizontalAlign="center" Width="120px" />
                                                            <FooterStyle HorizontalAlign="left" Font-Bold="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Schedule Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvschcode" runat="server" Style="text-align: center; width: 40px;"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schcode")) %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle HorizontalAlign="center" Width="40px" />
                                                            <FooterStyle HorizontalAlign="left" Font-Bold="true" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" runat="server" CssClass="btn btn-default btn-xs"><span class=" fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelOrder" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>


                                                            </ItemTemplate>
                                                            <ItemStyle Width="20px" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnConso" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
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
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlChequeDeposit" runat="server" Visible="false">

                                        <asp:GridView ID="gvchqdeposit" runat="server" AllowPaging="False"
                                            AutoGenerateColumns="False" OnRowDataBound="gvchqdeposit_RowDataBound"
                                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="593px">

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
                                                <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAcCod" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UsirCode" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcccUcode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Acc. Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcPactdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="130px"></asp:Label>
                                                    </ItemTemplate>
                                                    <%-- <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Description">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcUdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ref.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrmrks" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MR. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvmrno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>' Width="70"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bank Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvBaName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque No">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvtotal" runat="server" Text="Total :" CssClass="btn btn-primary primaryBtn" Width="70"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCheNo" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pay Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvCheDate" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFdramt" runat="server" Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvdrvamt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidamt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>





                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <%--<asp:HyperLink ID="lnkbtnPrintcdep" runat="server" Style="display:inline-block;"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>--%>
                                                        <asp:HyperLink ID="lnkbtnAppcdep" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                        </asp:HyperLink>
                                                        <%--  <asp:LinkButton ID="btnDelOrdercdep" runat="server" Style="display:inline-block;"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    <ControlStyle Width="80px" />

                                                </asp:TemplateField>



                                            </Columns>

                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>


                                    </asp:Panel>

                                    <asp:Panel ID="pnlUpColl" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">
                                                <asp:GridView ID="gvCollUpdate" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvCollUpdate_RowDataBound"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvINpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Project Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MR Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgINmrno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtINgvDate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtINgvRemarks" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                    Width="130px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcactdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvtotal" runat="server" Text="Total :" CssClass="btn btn-primary primaryBtn" Width="70"></asp:Label>
                                                            </FooterTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cheque No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvchqno" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblINgvitmamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFINgvitmamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>


                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" runat="server" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" runat="server" OnClick="btnDelOrder_OnClick" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
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

                                    <asp:Panel ID="pnlPurchase" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvPurchase" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvPurchase_RowDataBound"
                                                    ShowFooter="True" AllowPaging="True" OnPageIndexChanging="gvPurchase_PageIndexChanging" PageSize="100">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoRD" runat="server" Font-Bold="True"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="billno" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvbillno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ssircode" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvssircode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Project Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchProjectName" BackColor="Transparent" BorderStyle="None" runat="server" Width="160px" placeholder="Project Name" onkeyup="Search_Gridview(this,1,'gvPurchase')"></asp:TextBox><br />

                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgvacttdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="160px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bill Number">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBillNumber" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Bill Number." onkeyup="Search_Gridview(this,2,'gvPurchase')"></asp:TextBox><br />

                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgbillno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>                                
                                                        <asp:TemplateField HeaderText="Order No">                                                            
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgorderno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                    Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Bill Ref">

                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBillRef" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Bill Ref" onkeyup="Search_Gridview(this,4,'gvPurchase')"></asp:TextBox><br />

                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgbillrefno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Mrr Ref">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgmrrrefno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrref")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Chalan No" Visible="false">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgchalanno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>





                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvbilldat" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier Name">

                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSupplirName" BackColor="Transparent" BorderStyle="None" runat="server" Width="180px" placeholder="Supplier Name" onkeyup="Search_Gridview(this,7,'gvPurchase')"></asp:TextBox><br />

                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPssirdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFSupp" runat="server" Style="text-align: right"> Total :</asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Bill Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurbillamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0);") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFPurbillamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>





                                                        <asp:TemplateField>

                                                            <HeaderTemplate>
                                                                <table style="width: 120px;">
                                                                    <tr>
                                                                        <td class="style58">
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td class="style60">&nbsp;</td>
                                                                        <td>
                                                                            <asp:HyperLink ID="hylbtn" runat="server" BackColor="#000066"
                                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="120px">All Pending Bill</asp:HyperLink>
                                                                            <%--<asp:HyperLink ID="hylbtn" runat="server" CssClass="btn btn-primary primarygrdBtn" Style="text-align: center" BorderStyle="None" Width="50px">ALL</asp:HyperLink>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintBill" runat="server" CssClass="btn btn-default btn-xs" ToolTip="Print Bill" Target="_blank"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnEditRD" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" OnClick="btnDelPurchase_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                <asp:HyperLink ID="lnkbtnPrintOrder" runat="server" CssClass="btn btn-default btn-xs" ToolTip="Print Order" Target="_blank" ForeColor="green"><span class="fa fa-print"></span></asp:HyperLink>

                                                            </ItemTemplate>
                                                            <ItemStyle Width="120px" />
                                                            <HeaderStyle HorizontalAlign="left" Width="80px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="orderno" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                    </Columns>
                                                    <FooterStyle CssClass="grvFooter" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <RowStyle CssClass="grvRows" />
                                                </asp:GridView>
                                            </div>
                                        </div>


                                    </asp:Panel>

                                    <asp:Panel ID="PanlUpCon" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvContUpdate" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvContUpdate_RowDataBound"
                                                    ShowFooter="True">
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
                                                                <asp:TextBox ID="txtSearchproContUpdate" SortExpression="actdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Project" onkeyup="Search_Gridview(this,1,'gvContUpdate')"></asp:TextBox><br />

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
                                                                    Width="100px"></asp:Label>
                                                                <asp:Label ID="lgAPPcorderno" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAPPgvDate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bill Ref.">

                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSrchbillrefContUpdate" SortExpression="cbillref" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Bill Ref." onkeyup="Search_Gridview(this,4,'gvContUpdate')"></asp:TextBox><br />

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAPPgvcbillref" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cbillref")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contractor name">

                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSrchcsirdescContUpdate" SortExpression="csirdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Contractor" onkeyup="Search_Gridview(this,5,'gvContUpdate')"></asp:TextBox><br />

                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAPPgvcsirdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFCon" runat="server" Style="text-align: right"> Total :</asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Bill Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvbillamt" runat="server" Style="text-align: right" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFlcamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>

                                                            <HeaderTemplate>
                                                                <table style="width: 80px;">
                                                                    <tr>
                                                                        <td class="style58">
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td class="style60">&nbsp;</td>
                                                                        <td>
                                                                            <asp:HyperLink ID="hyconlbtn" runat="server" BackColor="#000066"
                                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="100px">All Pending Bill</asp:HyperLink>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" runat="server" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelConBill" runat="server" OnClick="btnDelConBill_Click" OnClientClick="javascript:return FunConfirm();" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
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

                                    <asp:Panel ID="PnlMTras" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvMatTrasfer" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvMatTrasfer_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoDeliv" runat="server" Font-Bold="True"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Transfer No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvtrnno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno1")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From Project">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgpactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="130px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txttrnsdate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "trnsdate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To Project">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgptpactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tpactdesc")) %>'
                                                                    Width="130px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvtotal" runat="server" Text="Total :" CssClass="btn btn-primary primaryBtn" Width="70"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Ref No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgptrefno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>




                                                        <asp:TemplateField HeaderText="Transfer</br> Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltrsnamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trsnamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFchlamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>

                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField>

                                                            <HeaderTemplate>
                                                                <table style="width: 80px;">
                                                                    <tr>
                                                                        <td class="style58">
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td class="style60">&nbsp;</td>
                                                                        <td>
                                                                            <asp:HyperLink ID="hyperMattrns" runat="server" BackColor="#000066"
                                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="100px">All Pending Transfer</asp:HyperLink>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" runat="server" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span></asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <%--  <asp:TemplateField HeaderText="">--%>
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

                                    <asp:Panel ID="PnlBRec" Visible="false" runat="server">

                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvBankRec" runat="server"
                                                CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                AutoGenerateColumns="False"
                                                ShowFooter="True" Width="104px" PageSize="20">
                                                <RowStyle />


                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnid" runat="server"
                                                                Style="text-align: right; font-size: 11px;"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSUBCODE" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                                Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VOUNUM" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVOUNUM" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                                Width="87px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Recon.Date (dd.mm.yyyy)">
                                                        <EditItemTemplate>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <%--    <asp:Label ID="lblRECNDT" runat="server" Font-Size="11px" 
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")) %>' 
                                                Width="80px"></asp:Label>--%>


                                                            <asp:TextBox ID="txtgvRECNDT" runat="server" Font-Size="11px"
                                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd.MM.yyyy")) %>'
                                                                Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>

                                                            <cc1:CalendarExtender ID="txtgvRECNDT_CalendarExtender" runat="server"
                                                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtgvRECNDT"></cc1:CalendarExtender>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cheq./ Ref. No.">
                                                        <%-- <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                                                </FooterTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblREFNO" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vou.Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVOUDAT" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="66px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vou.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVOUNUM1" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deposit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPayment" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDeposit" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTRANSDES" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="160px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Details Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDetailsHead" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc1")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Narration">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvVarnar" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rpcode" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRpCode" runat="server" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
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
                                    <asp:Panel ID="PnlPDC" Visible="false" runat="server">
                                        <div class="row">

                                            <div class="table-responsive col-lg-12" style="min-height: 350px">
                                                <asp:GridView ID="dgPdc" runat="server" AutoGenerateColumns="False"
                                                    OnRowDataBound="dgPdc_RowDataBound" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="serialnoid" runat="server"
                                                                    Style="text-align: right; font-size: 11px;"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvAccCod" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgcatCod" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ResCode" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcUcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank Name">
                                                            <HeaderTemplate>
                                                                <table style="width: 180px;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblbname" runat="server" Font-Bold="True" Text="Bank Name"
                                                                                Width="100px"></asp:Label>
                                                                        </td>
                                                                        <td class="style60">
                                                                            <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" BackColor="#000066"
                                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                                ForeColor="White" Style="text-align: center" Width="90px" Target="_blank">Export Exel</asp:HyperLink>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvbankname" runat="server" Font-Bold="true"
                                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "cactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")).Trim(): "")  %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvPVnum" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Issue #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvvounum1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvPVDate" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acc. Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgactdesc" runat="server"
                                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                                    Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvtotal" runat="server" Text="Total :" CssClass="btn btn-primary primaryBtn" Width="70"></asp:Label>
                                                            </FooterTemplate>
                                                            <%--<FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdateAllVou" runat="server" Font-Bold="True"
                                                Font-Size="13px" ForeColor="White" OnClick="lbtnUpdateAllVou_Click"
                                                Style="text-align: Center; height: 15px;" Width="80px">Update All</asp:LinkButton>
                                        </FooterTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <FooterStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Issue Ref">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvisunum" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cheque No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvchnono" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cheque Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvchdat" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFCrAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvdramt" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reconcilaition Date">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="txtgvReconDat" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Style="text-align: left; font-size: 11px;"
                                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")%>'
                                                                    Width="70px"></asp:TextBox>--%>

                                                                <asp:TextBox ID="txtgvReconDat" runat="server" CssClass="inputDateBox" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")%>'
                                                                    Style="text-align: center; font-size: 11px;" OnTextChanged="txtgvReconDat_TextChanged" AutoPostBack="true">

                                                                </asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender_txtgvReconDat" runat="server" Enabled="True"
                                                                    Format="dd-MMM-yyyy" TargetControlID="txtgvReconDat"></cc1:CalendarExtender>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkvmrno" runat="server"
                                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                                    Width="20px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnokpdc" runat="server" OnClick="btnokpdc_Click" Width="30px">OK</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Voucher Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvNewVoNum" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "newvocnum")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvParName" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bill No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvBill" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                    Width="100px"></asp:Label>
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

                                    <asp:Panel ID="PnlClientMod" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvClientMod" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvClientMod_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoDeliv" runat="server" Font-Bold="True"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvadno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Project Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcustdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Modification </Br> Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgadno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvtotal" runat="server" Text="Total :" CssClass="btn btn-primary primaryBtn" Width="70"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtaddate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Modification </Br>  Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFsaladjamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>

                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" runat="server" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
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

                                    <asp:Panel ID="PnlotherBill" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvOthBillUp" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvOthBillUp_RowDataBound"
                                                    ShowFooter="True" PageSize="100">
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
                                                                <asp:Label ID="lblgvsupcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supcode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblssirdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="160px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Req Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgreqno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                    Width="100px"></asp:Label>
                                                                <asp:Label ID="lgreqno" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtreqdat" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req Ref.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmrfno" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Contractor name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtAPPgvcsirdesc" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                                        Width="150px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFCon" runat="server" Style="text-align: right"> Total :</asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>--%>


                                                        <asp:TemplateField HeaderText="Req Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>

                                                            <HeaderTemplate>
                                                                <table style="width: 80px;">
                                                                    <tr>
                                                                        <td class="style58">
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td class="style60">&nbsp;</td>
                                                                        <td>
                                                                            <asp:HyperLink ID="hyconlbtn" runat="server" BackColor="#000066"
                                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="100px">All Pending Bill</asp:HyperLink>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" runat="server" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <%--<asp:LinkButton ID="btnDelConBill" runat="server" OnClick="btnDelConBill_Click" OnClientClick="javascript:return FunConfirm();"  CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
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

                                    <asp:Panel ID="pnllsd" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvlsdUp" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvlsdUp_RowDataBound"
                                                    ShowFooter="True">
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
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Project Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpactdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="130px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="LSD Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lglsdno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lsdno1")) %>'
                                                                    Width="100px"></asp:Label>
                                                                <asp:Label ID="lglsdno" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lsdno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txlsddate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lsddate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req Ref.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmrfno" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Contractor name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtAPPgvcsirdesc" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csirdesc")) %>'
                                                                        Width="150px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFCon" runat="server" Style="text-align: right"> Total :</asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>--%>


                                                        <asp:TemplateField HeaderText="LSD Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvlsdamt" runat="server" Style="text-align: right" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsdamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFlsdamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>

                                                            <HeaderTemplate>
                                                                <table style="width: 80px;">
                                                                    <tr>
                                                                        <td class="style58">
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td class="style60">&nbsp;</td>
                                                                        <td>
                                                                            <%--<asp:HyperLink ID="hyconlbtn" runat="server" BackColor="#000066"
                                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="100px">All Pending Bill</asp:HyperLink>--%>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" runat="server" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <%--<asp:LinkButton ID="btnDelConBill" runat="server" OnClick="btnDelConBill_Click" OnClientClick="javascript:return FunConfirm();"  CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" />
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
                                    <asp:Panel ID="pnlPatCash" Visible="false" runat="server">

                                        <asp:GridView ID="gvpetty" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" ShowFooter="True" Width="501px" OnRowDataBound="gvpetty_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMatCode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'></asp:Label>
                                                        <asp:Label ID="lblpcbl" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pcblno1")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PC BILL NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpcbl" runat="server" Width="90px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pcblno1")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbgvEmp" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                            Width="220px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bill Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbilldate" runat="server" Style="text-align: left" BackColor="Transparent" BorderWidth="0"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbliTem" runat="server" Style="text-align: left" BackColor="Transparent" BorderWidth="0"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                            Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblamt" runat="server"
                                                            Style="font-size: 11px; text-align: right;" BackColor="Transparent" BorderWidth="0"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="right"
                                                        VerticalAlign="Middle" Font-Size="12px" />
                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <%--  <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkvmrno" runat="server"
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                            Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HypApprv" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-edit"></span>
                                                        </asp:HyperLink>


                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HypUpdate" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
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

                                    <asp:Panel ID="pnlUnposted" Visible="false" runat="server">

                                        <asp:GridView ID="gvAccUnPosted" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnRowDataBound="gvAccUnPosted_RowDataBound" Width="550px">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvINSlNo0" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvvoudat" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Voucher">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvounum" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reference">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Voucher Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvvouamt" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="90px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkvmrno" runat="server" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                            Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <%-- <asp:LinkButton ID="lnkbtnViewIN" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>
                                                        <asp:HyperLink ID="hlnkVoucherPrint" runat="server" Target="_blank" ToolTip="Voucher Print" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>


                                                        <asp:LinkButton ID="lbtnVoucherApp" ToolTip="Approved" runat="server" OnClick="lbtnVoucherApp_Click" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'><i class=" fa fa-check" aria-hidden="true"></i> </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </asp:Panel>





                                    <asp:Panel ID="pnlMAtConversion" Visible="false" runat="server">

                                        <asp:GridView ID="grvMatConversion" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnRowDataBound="grvMatConversion_RowDataBound" Width="700px">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Conversion No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConvno" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "convrno")) %>'
                                                            Width="110px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConvdate" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "convrdat")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Project">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConvproj" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Convert Material">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConvmat" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfunit" runat="server" Style="text-align: left"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgfspcfdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="lgvfspcfcod" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgfspcfcode" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>' Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Qty ">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lgvMCqty" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>

                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lgvMCrate" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>


                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                                            Width="100px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemTemplate>



                                                        <asp:Label ID="lgvMCamt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>


                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                </asp:TemplateField>





                                                <%--  <asp:TemplateField HeaderText="fsircode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgfsircode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                --%>
                                                <asp:TemplateField HeaderText="   ">
                                                    <ItemTemplate>
                                                        <%-- <asp:HyperLink ID="lnkbtnPrintMC" runat="server" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>--%>
                                                        <asp:HyperLink ID="lnkbtnAppMC" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>
                                                        </asp:HyperLink>
                                                        <%-- <asp:LinkButton ID="btnDelMC" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="300px" VerticalAlign="Top" />
                                                </asp:TemplateField>




                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </asp:Panel>


                                    <asp:Panel ID="pnltrnUnit" Visible="false" runat="server">

                                        <asp:GridView ID="gvAccUnPostedtrn" runat="server" AutoGenerateColumns="False"
                                            CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnRowDataBound="gvAccUnPostedtrn_RowDataBound" Width="550px">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvslnotrn" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvvoudattrn" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Voucher">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvounumtrn" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reference">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtINgvteamdesctrn" runat="server" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Voucher Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvvouamttrn" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFvouamttrn" runat="server" Style="text-align: right" Width="90px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkvmrnotrn" runat="server" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                            Width="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <%-- <asp:LinkButton ID="lnkbtnViewIN" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>
                                                        <asp:HyperLink ID="hlnkVoucherPrinttrn" runat="server" Target="_blank" ToolTip="Voucher Print" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>


                                                        <asp:LinkButton ID="lbtnVoucherApptrn" ToolTip="Approved" runat="server" OnClick="lbtnVoucherApptrn_Click" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'><i class=" fa fa-check" aria-hidden="true"></i> </asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
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
                                                                    <asp:HyperLink ID="lnkbtnAppIN" runat="server" Target="_blank"  Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>

                                                                  

                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" />
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

                                     <asp:Panel ID="pnlMatIssue" runat="server" Visible="false">
                                         <div class="row">
                                              <div class="table-responsive col-lg-12">
                                                       <asp:GridView ID="gvMatIssue" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvMatIssue_RowDataBound" Width="785px">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNomissue" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnomissue" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueno1"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Store Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdescmissue" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="Issue <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvissuedatmissue" runat="server"
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
                                                                    <asp:Label ID="lblgvreqno1missue" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcodemissue" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdescmissue" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>


                                                              <asp:TemplateField HeaderText="Issue Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvIssueAmt" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt"))%>' Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                   <FooterTemplate>
                                                                <asp:Label ID="lblgvFIssueAmt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                  <FooterStyle  HorizontalAlign="Right"/>
                                                            </asp:TemplateField>
                                                            

                                                           






                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hlnkMiasue" runat="server" Target="_blank"  Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>

                                                                  

                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" />
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
                        <%--</asp:Panel>--%>


                        <asp:Panel ID="pnlAcc" runat="server" Visible="false">


                            <div class="form-group">

                                <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                    <ul class="nav colMid" id="SERV">
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccFinalReports?RepType=BS&comcod=")%> " target="_blank">01. Statement Of Financial Position</a>
                                        </li>


                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccFinalReports?RepType=IS&comcod=")%> " target="_blank">02. Statement Of Comprehensive Income</a>
                                        </li>




                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/RptBankCheque?Type=CashFlow")%> " target="_blank">03. Statement Of Cash Flow</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccTrialBalance?Type=Details&comcod=")%> " target="_blank">04. Notes: Financial Postion</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccTrialBalance?Type=INDetails&comcod=")%> " target="_blank">05. Notes: Comprehensive Income</a>
                                        </li>

                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccDTransaction?Type=Accounts&TrMod=RecPay&comcod=&Date1=&Date2=")%> " target="_blank">06. Receipts & Payment</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccTrialBalance?Type=Mains&comcod=")%> " target="_blank">07. Trial Balance</a>
                                        </li>


                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccBankRecon?Type=Acc")%> " target="_blank">08. Bank Reconcillation Statement</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccDayTransData")%> " target="_blank">09. Daily transaction- All</a>
                                        </li>
                                        <%--<li>
                                            <a href="<%=this.ResolveUrl("~/F_15_Acc/RptAccDTransaction?Type=Accounts&TrMod=ProTrans")%> " target="_blank">10. Daily Transaction- Individual</a>
                                      <%--  </li>--%>
                                        <%--          <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccLedger?Type=Ledger&RType=GLedger")%> " target="_blank">10. Ledger</a>
                                        </li>

                                         <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccLedger?Type=SubLedger")%> " target="_blank">11. Subsidary Ledger</a>
                                        </li>
                                         <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccSpLedger?Type=DetailLedger")%> " target="_blank">12. Special Ledger</a>
                                        </li>--%>

                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccLedgerAll")%> " target="_blank">10.  Accounts Ledger- All</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccControlSchedule?Type=Type01")%> " target="_blank">11. Accounts Control Schedule</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_17_Acc/AccDetailsSchedule")%> " target="_blank">12. Accounts Details Schedule</a>
                                        </li>

                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_34_Mgt/OtherReqEntry?Type=OreqEdit&prjcode=&genno=")%> " target="_blank">13. Other Requisition </a>
                                        </li>


                                    </ul>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </asp:Panel>

                    </div>

                </div>
            </div>
        </ContentTemplate>



    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

