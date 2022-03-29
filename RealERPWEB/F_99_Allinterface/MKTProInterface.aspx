<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MKTProInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.MKTProInterface" %>

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
            width: 90px;
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
            background-color: #8E44AD;
        }

        .circle-tile-heading.green:hover {
            background-color: #05F37C;
        }

        .circle-tile-heading.orange:hover {
            background-color: #34495E;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #16A085;
        }

        .circle-tile-heading.purple:hover {
            background-color: #E74C3C;
        }

        .circle-tile-heading.deep-sky-blue:hover {
            background-color: #F39C12;
        }

        .circle-tile-heading.deep-pink:hover {
            background-color: #A52A2A;
        }
        .circle-tile-heading.lime:hover {
            background-color: #00BFFF;
        }
        .circle-tile-heading.chocolate:hover {
            background-color: #32CD32;
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

        .deep-sky-blue {
            background-color: #00BFFF;
        }

        .deep-pink {
            background-color: #FF1493;
        }
        .lime{
            background-color:#32CD32;
        }
        .chocolate{
            background-color:#A52A2A;
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

        .text-deep-sky-blue {
            color: #00BFFF;
        }

        .text-deep-pink {
            color: #FF1493;
        }
        .text-lime {
            color: #32CD32;
        }
        .text-chocolate{
            color: #A52A2A;
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
                    case 'gvReqApp':
                        tblData = document.getElementById("<%=this.gvReqApp.ClientID %>");
                        break;

                    case 'gvWrkOrd':
                        tblData = document.getElementById("<%=this.gvWrkOrd.ClientID %>");
                        break;
                    case 'grvMRec':
                        tblData = document.getElementById("<%=this.grvMRec.ClientID %>");
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





                var gvreqinfo = $('#<%=this.gvReqInfo.ClientID %>');
                var gvcheck = $('#<%=this.gvReqChk.ClientID%>');
                var gvreqapp = $('#<%=this.gvReqApp.ClientID %>');

                gvreqinfo.Scrollable();
                gvcheck.Scrollable();
                gvreqapp.Scrollable();


            }

            catch (e) {

                alert(e.message);

            }


        };



        function FunPurchaseOrder(url) {
            window.open('' + url + '', '_blank');

        }

    </script>

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
                <div class="card-body" style="min-height: 550px;">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <span class="input-group-text">From</span>
                                    <%--<button class="btn btn-secondary" type="button">From</button>--%>
                                </div>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                <div class="input-group-prepend ">
                                    <span class="input-group-text">To</span>
                                    <%--<button class="btn btn-secondary" type="button">To</button>--%>
                                </div>

                                <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
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
                        <div class="col-md-2">
                            <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="hyplnkCrReqMkt" runat="server" Target="_blank" NavigateUrl="~/F_28_MPro/MKTPurReqEntry?InputType=Entry&prjcode=&genno=&comcod=" CssClass="dropdown-item" Style="padding: 0 10px">Create Requisition Marketing</asp:HyperLink>
                                        <%--<asp:HyperLink ID="hyplnkCrReqIT" runat="server" Target="_blank" NavigateUrl="~/F_12_Inv/PurReqEntry?InputType=FxtAstEntry&prjcode=&genno=" CssClass="dropdown-item" Style="padding: 0 10px">Create Requisition IT</asp:HyperLink>
                                        <asp:HyperLink ID="hyplnkCrReqAdmin" runat="server" Target="_blank" NavigateUrl="~/F_12_Inv/PurReqEntry?InputType=FxtAstEntry&prjcode=&genno=" CssClass="dropdown-item" Style="padding: 0 10px">Create Requisition Admin</asp:HyperLink>--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2" style="display: none;">

                            <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">Requisition Tracking</asp:Label>
                            <asp:TextBox runat="server" ID="txtTrack" AutoPostBack="true" CssClass="inputtextbox" OnTextChanged="txtTrack_TextChanged"></asp:TextBox>
                            <asp:Button ID="lblMIMEInfo" runat="server" CssClass="smLbl_to" Height="20px" Style="text-align: center; line-height: 20px; padding: 0; float: right;" Text="Search" />
                        </div>

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



                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div>
                                        <asp:Panel ID="pnlReqStatus" runat="server" Visible="false">
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
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

                                        <asp:Panel ID="pnlReqChq" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvReqChk" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvReqChk_RowDataBound">
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
                                                                    <asp:Label ID="lblgvreqnorq" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
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

                                                            <asp:TemplateField HeaderText="">
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
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
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
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
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

                                        <asp:Panel ID="pnlFinalApp" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvReqApp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvReqApp_RowDataBound">
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
                                                                    <asp:Label ID="lblgvreqnorapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
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
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
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
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
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
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
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
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" ToolTip="Print Req Info" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>


                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelReqApp" OnClick="btnDelReqApp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>

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


                                        <asp:Panel ID="pnlcsprepared" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvcsprepared" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvcsprepared_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNocsp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnocsp" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactdesccsp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqdat1csp" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1csp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumcsp" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." ToolTip="Search" onkeyup="Search_Gridview(this,4,'gvRateApp')"></asp:TextBox><br />

                                                                </HeaderTemplate>



                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnocsp" runat="server" Font-Bold="True" Style="text-align: left"
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
                                                                    <asp:Label ID="lblgvpactcodecsp" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchCatagorycsp" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvRateApp')"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagorycsp" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountcsp" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamtcsp" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrintcsp" runat="server" ToolTip="Print Req Info" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>


                                                                    <asp:HyperLink ID="hlnkbtnEntrycsp" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelcsp" OnClick="btnDelReqApp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>

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


                                            <asp:Panel ID="pnlcsapproved" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                   <asp:GridView ID="gvcsapproved" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvcsapproved_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNocsap" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnocsap" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactdesccsap" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqdat1csap" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqnocsap01" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumcsap" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." ToolTip="Search" onkeyup="Search_Gridview(this,4,'gvRateApp')"></asp:TextBox><br />

                                                                </HeaderTemplate>



                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfnocsap" runat="server" Font-Bold="True" Style="text-align: left"
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
                                                                    <asp:Label ID="lblgvpactcodecsap" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Catagory">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchCatagorycsap" SortExpression="catagory" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Catagory" onkeyup="Search_Gridview(this,5,'gvRateApp')"></asp:TextBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcatagorycsap" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catdesc"))%>' Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcountcsap" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamtcsap" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvApamtcsap" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrintcsap" runat="server" ToolTip="Print Req Info" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>

                                                                    </asp:HyperLink>


                                                                    <asp:HyperLink ID="hlnkbtnEntrycsap" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-check"></span>

                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="btnDelcsap" OnClick="btnDelReqApp_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>

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


                                        <asp:Panel ID="pnlWorkOrder" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvWrkOrd" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvWrkOrd_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ReqNo" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">

                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnumporder" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,6, 'gvWrkOrd')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
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
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvWoamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFWoamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:LinkButton ID="btnDelAprovedNo_Click" OnClick="btnDelAprovedNo_Click" OnClientClick="javascript:return FunConfirm();" runat="server" CssClass="btn btn-default btn-xs"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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
                                                                        Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")))=="3101" || (Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")))=="3355" ? true : false %>'
                                                                        Width="50px"></asp:Label>
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
                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
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

                                        <asp:Panel ID="pnlMatRec" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="grvMRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="grvMRec_RowDataBound">
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
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">

                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchrefnummrec" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Mrf No." onkeyup="Search_Gridview(this,6,'grvMRec')"></asp:TextBox><br />

                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
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
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Resource</br> Count">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvWoamt" runat="server" Style="text-align: right"
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

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>
</asp:Content>
