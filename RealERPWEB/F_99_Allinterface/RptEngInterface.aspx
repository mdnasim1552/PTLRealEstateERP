<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEngInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.RptEngInterface" %>

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
            width: 112px;
        }

        .tbMenuWrp table tr td {
            /*height: 50px;*/
            /*width:90px;*/
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
            font-family: Calibri;
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
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            function loadModal() {
                $('#exampleModal3').modal('show');
            }

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


                $('#<%=this.gvReqInfo.ClientID%>').tblScrollable();
                $('#<%=this.gvPenApproval.ClientID%>').tblScrollable();
                $('#<%=this.gvFinlApproval.ClientID%>').tblScrollable();
                $('#<%=this.gvPayOrder.ClientID%>').tblScrollable();
                $('#<%=this.gvReqInfo1.ClientID%>').tblScrollable();

                funComRadiButtonHidden();
            }

            catch (e) {
                alert(e);
            }




        };

        function funComRadiButtonHidden() {

            try {

                //Table Index start with 1

                comcod = <%=this.GetCompCode()%>;
                switch (comcod) {


                    //case 3101:   //ASIT
                    case 1103:   //Tanvir
                        $(".tbMenuWrp table tr td:nth-child(2)").hide();
                        break;

                    case 1102://IBCEL
                        $(".tbMenuWrp table tr td:nth-child(6)").hide();
                        break;


                    default:
                        $(".tbMenuWrp table tr td:nth-child(4)").hide();
                        $(".tbMenuWrp table tr td:nth-child(5)").hide();
                        $(".tbMenuWrp table tr td:nth-child(6)").hide();

                        //    $("table[id*=RadioButtonList1] input:first").next().next().hide();

                        break;



                }



            }
            catch (e) {


                alert(e);

            }

        }

        function FunFinalApproval(url) {
            window.open('' + url + '', '_blank');
        }

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
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="30000">
            </asp:Timer>

            <%--<triggers>
 
                   <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>
            --%>




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
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_34_Mgt/OtherReqEntry?Type=OreqEntry&prjcode=&genno=" CssClass="dropdown-item">Create Requsition</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccGenAdjJournal" CssClass="dropdown-item">Petty Cash Adjustment </asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_17_Acc/AccVoucherUnposted" CssClass="dropdown-item"> Adjustment Final Approval </asp:HyperLink>

                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                    <div class="row">
                        <asp:Panel ID="pnlInterf" runat="server">

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
                                </div>
                            </div>



                        </asp:Panel>

                        <asp:Panel ID="pnlReqInfo" runat="server" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvReqInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvReqInfo_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource head">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsupdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:Label ID="hypMrfno" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent;" Font-Underline="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                </asp:Label>


                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
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
                                                <asp:Label ID="lblrescount" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Curent Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Attached file" Visible="true">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutation" runat="server" Text="Attachded Quotation" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="HyInprPrint11" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>



                                                <asp:LinkButton ID="btnDelOrder11" runat="server" CssClass="btn btn-xs btn-default"><span style="color:red" class=" fa   fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdetpname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detpname")) %>'
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

                        </asp:Panel>


                        <asp:Panel ID="pnlPendapp" runat="server" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPenApproval" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvPenApproval_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNopapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnopapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdescpapr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdescf" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource head">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsupdescf" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1papr" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1papr" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkgvgvmrfnopapr" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                </asp:HyperLink>


                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="420px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactcodepapr" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtpapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtpapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtpapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="CS Attached file">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutation" runat="server" Text="Attachded Qutation" Style="width: 100px;" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="HyInprPrintfapproved" CssClass="btn btn-xs btn-default" runat="server" Visible="false" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="lnkbtnEditIN" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false">
                                               <span class="fa fa-edit"></span>
                                                </asp:HyperLink>

                                                <asp:LinkButton ID="btnDelOrderfapproved" runat="server" CssClass="btn btn-xs btn-default" OnClick="btnDelOrderfapproved_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>



                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Bundle">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkbudleno" runat="server" Target="_blank" ForeColor="Blue"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bundno")) %>'
                                                    Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>





                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Requisition Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleuser" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rusername")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdetpname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detpname")) %>'
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


                        </asp:Panel>

                        <asp:Panel ID="pnlfrec" runat="server" Visible="false">

                            <div class="table-responsive">
                                <asp:GridView ID="gvfrec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvfrec_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNofrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnofrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdescfrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdescfrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1FnApp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1frec" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="420px"></asp:Label>
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


                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountfrec" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtfrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtfrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtfrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="CS Attached file">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutationfapp" runat="server" Text="Attachded Qutation" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkbtnEntryfrec" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                </asp:HyperLink>
                                                <asp:LinkButton ID="btnDelfrec" runat="server" CssClass="btn btn-xs btn-default" OnClick="btnDelfrec_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bundle">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkbudlefrec" runat="server" Target="_blank" ForeColor="Blue"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bundno")) %>'
                                                    Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>





                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Requisition Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleuserfrec" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rusername")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="First Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblapeuserfrec" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvname")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdetpname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detpname")) %>'
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


                        </asp:Panel>


                        <asp:Panel ID="pnlsrec" runat="server" Visible="false">

                            <div class="table-responsive">
                                <asp:GridView ID="gvsrec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvsrec_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNosrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnosrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdescsrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdescsrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1FnApp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1srec" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="420px"></asp:Label>
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


                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountsrec" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtsrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtsrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtsrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="CS Attached file">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutationfapp" runat="server" Text="Attachded Qutation" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>



                                                <asp:HyperLink ID="lnkbtnEntrysrec" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                </asp:HyperLink>




                                                <asp:LinkButton ID="btnDelsrec" runat="server" CssClass="btn btn-xs btn-default" OnClick="btnDelsrec_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bundle">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkbudlesrec" runat="server" Target="_blank" ForeColor="Blue"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bundno")) %>'
                                                    Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>





                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Requisition Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleusersrec" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rusername")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="First Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblapeusersrec" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvname")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdetpname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detpname")) %>'
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


                        </asp:Panel>

                        <asp:Panel ID="pnlthrec" runat="server" Visible="false">

                            <div class="table-responsive">
                                <asp:GridView ID="gvthrec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvthrec_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNothrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnothrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdescthrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdescthrec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1FnApp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1threc" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="420px"></asp:Label>
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


                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountthrec" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtthrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtthrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtthrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="CS Attached file">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutationfapp" runat="server" Text="Attachded Qutation" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>


                                                <asp:HyperLink ID="lnkbtnEntrythrec" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="lnkbtnEditthrec" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false">
                                        <span class="fa fa-edit"></span>
                                                </asp:HyperLink>


                                                <asp:LinkButton ID="btnDelthrec" runat="server" CssClass="btn btn-xs btn-default" OnClick="btnDelthrec_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bundle">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkbudlethrec" runat="server" Target="_blank" ForeColor="Blue"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bundno")) %>'
                                                    Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>





                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Requisition Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleuserthrec" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rusername")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="First Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblapeuserthrec" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvname")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdetpname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detpname")) %>'
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


                        </asp:Panel>

                        <asp:Panel ID="pnlFinalApp" runat="server" Visible="false">

                            <div class="table-responsive">
                                <asp:GridView ID="gvFinlApproval" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvFinlApproval_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNoFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnoFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdescFnApp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdescfi" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource head">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsupdescfi" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1FnApp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1FnApp" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkgvgvmrfnoFnApp" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                </asp:HyperLink>


                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="420px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactcodeFnApp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="CS Attached file">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutationfapp" runat="server" Text="Attachded Qutation" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="HyInprPrint" runat="server" CssClass="btn btn-xs btn-default" Visible="false" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="lnkbtnEditIN" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Black" Font-Underline="false">
                                            <span class="fa fa-edit"></span>
                                                </asp:HyperLink>


                                                <asp:LinkButton ID="btnDelOrder" runat="server" CssClass="btn btn-xs btn-default" OnClick="btnDelOrder_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllfinalAprv" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkAllfinalAprv_CheckedChanged" Width="20px" />

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkfinalAprv" runat="server" Width="20px" />
                                            </ItemTemplate>

                                            <FooterTemplate>

                                                <asp:LinkButton ID="lnkbtnfinalAprv" runat="server" OnClientClick="return FunAppConfirm();"
                                                    OnClick="lnkbtnfinalAprv_Click" ToolTip="Approval Print"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bundle">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkbudlenoapp" runat="server" Target="_blank" ForeColor="Blue"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bundno")) %>'
                                                    Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>





                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Req. Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleuserfapp" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rusername")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="First Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblapeuserfapp" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvname")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdetpname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detpname")) %>'
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


                        </asp:Panel>

                        <asp:Panel ID="pnlpayOrder" runat="server" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPayOrder" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvPayOrder_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNoPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnopPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsupcode" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supcode")) %>'
                                                    Width="170px"></asp:Label>
                                                <asp:Label ID="lblrescode" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="170px"></asp:Label>
                                                <asp:Label ID="lblgvpatcdescpPor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvapyor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource Head">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgdeshead" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1pPor" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1pPor" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkgvgvmrfnopPor" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                </asp:HyperLink>


                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="420px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactcodepPor" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountpayord" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFResource" runat="server" Style="text-align: right">Total :</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requistion <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtpPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtpPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="60px" />

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtpPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Curent Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurrentStpPor" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyInprPrintpay" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>
                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hlnkprjlink" CssClass="btn btn-xs btn-default" runat="server" ToolTip="Project Linking" OnClientClick="Confirm (Do You Want To Link this Project?) return;" OnClick="hlnkprjlink_Click" Font-Underline="false"><span class="fa fa-check"></span>
                                                </asp:LinkButton>
                                                <asp:HyperLink ID="hlnkpaymentSch" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Go Payment Schedule" runat="server" Font-Size="15px"><span  class="fa fa-check"></span> </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdetpname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detpname")) %>'
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


                        </asp:Panel>


                        <asp:Panel ID="PnlreqInfo1" runat="server" Visible="false">

                            <asp:GridView ID="gvReqInfo1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>


                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDSlNoreq" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="reqno#" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreqnoreq" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <%--                                    <asp:TemplateField HeaderText="Project Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpcodereq" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                Width="160px" ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>


                                    <%--                                    <asp:TemplateField HeaderText="Project Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpatcdescreq" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Requistion <br>Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lnkgvreqrat1req" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Requistion <br> No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreqno1req" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MRF No.">
                                        <ItemTemplate>

                                            <asp:Label ID="hypMrfnoreq" runat="server" BorderStyle="none"
                                                Font-Size="11px" Style="background-color: Transparent;" Font-Underline="false"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                            </asp:Label>


                                        </ItemTemplate>
                                        <ItemStyle Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="420px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                    <%-- <asp:TemplateField HeaderText="pactcode" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvpactcodereq" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Resource</br> Count">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrescountreq" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Requistion <br>Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvreqamtreq" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Approved Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvApamtreq" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPaidamtreq" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="First Approval">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfapprv" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvname")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                        <FooterStyle HorizontalAlign="right" Width="60px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Final Approval">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvfinal" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "faprvname")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                                                    <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>--%>
                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdetpname" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detpname")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <%--     <asp:TemplateField HeaderText="Curent Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcurrentStreq" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle  HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>

                                    <%--   <asp:TemplateField HeaderText="CS Attached file" Visible="false">
                                        <ItemTemplate>

                                            <asp:HyperLink ID="hlkQutationrq" runat="server" Text="Attachded Qutation" Target="_blank"></asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle  HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>


                                    <%--                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:HyperLink ID="HyInprPrint11" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                            </asp:HyperLink>



                                            <asp:LinkButton ID="btnDelOrder11" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                    </asp:TemplateField>--%>
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



            <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script> 
<script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>
            
            <script src="../Scripts/jquery.counterup.min.js"></script>
            <script>
                jQuery(document).ready(function ($) {
                    $('.counter').counterUp({
                        delay: 10,
                        time: 1000
                    });
                });
</script>--%>
        </ContentTemplate>



    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

