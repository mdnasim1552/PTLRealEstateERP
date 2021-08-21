<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptAuditInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.RptAuditInterface" %>

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
            width: 120px;
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
            font-family:Calibri;
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

    <script src="../Scripts/waypoints.min.js"></script>
    <script src="../Scripts/jquery.counterup.min.js"></script>
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

                $('#<%=this.gvAccVoucher.ClientID%>').tblScrollable();
                $('#<%=this.gvReadytoAudit.ClientID%>').tblScrollable();
                $('#<%=this.gvAudit.ClientID%>').tblScrollable();

                funComRadiButtonHidden();
            }
            catch (e) {
                alert(e);
            }
        };
        function funComRadiButtonHidden() {
            try {
                comcod = <%=this.GetCompCode()%>;
                switch(comcod)
                {
                    case 3101:   //ASIT
                    case 1103:   //Tanvir
                       // $(".tbMenuWrp table tr td:nth-child(2)").hide();
                        break;
                    default:               
                        //$(".tbMenuWrp table tr td:nth-child(4)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(5)").hide();
                        //$(".tbMenuWrp table tr td:nth-child(6)").hide();                 
                        //$("table[id*=RadioButtonList1] input:first").next().next().hide();                 
                        break;
                }
            } 
            catch (e) {
                alert(e);
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
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="30000">
            </asp:Timer>

            <%--<triggers>
 
                   <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>
            --%>




            <div class=" card card-fluid">
                <div class=" card-body">
                    <div class="row">

                        <div class="col-md-1">
                            <div class="  form-group">
                                  <Label for="Label1"  class =" control-label lblmargin-top9px ">From</Label>


                            </div>

                        </div>

                          <div class="col-md-2">
                            <div class="  form-group">
                                 <asp:TextBox ID="txFdate" runat="server" CssClass=" form-control " AutoPostBack="true"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1_txFdate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txFdate"></cc1:CalendarExtender>


                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="  form-group">
                                  <Label for="lbldate"  class =" control-label lblmargin-top9px ">To</Label>


                            </div>

                        </div>

                          <div class="col-md-2">
                            <div class="  form-group">
                                  <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control " AutoPostBack="true"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>


                            </div>

                        </div>

                         <div class="col-md-1">
                            <div class="  form-group">
                                  <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>


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
                                <asp:GridView ID="gvAccVoucher" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True"  AllowSorting="True">
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

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvoudat" runat="server" BackColor="Transparent"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voucher">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvou" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="txtINgvtdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchequedat" runat="server" BackColor="Transparent"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Narration">
                                            <%--  <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchvenar" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Narration" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNarrat" runat="server" BackColor="Transparent" Style="word-break: break-all"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="payto">
                                            <%--<HeaderTemplate>
                                        <asp:TextBox ID="txtSearchpayto" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Party Name" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblogvpayto" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Vou. Amount">
                                            <%--<HeaderTemplate>
                                        <asp:TextBox ID="txtSearchamt" BackColor="Transparent" BorderStyle="None" runat="server" Width="75px" placeholder="Vou. Amount" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnam" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <%--  <FooterTemplate>
                                        <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvusernam" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usernam")) %>'
                                                    Width="150px"></asp:Label>
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
                                <asp:GridView ID="gvReadytoAudit" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvReadytoAudit_RowDataBound" ShowFooter="True" AllowSorting="True">
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

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvoudat" runat="server" BackColor="Transparent"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voucher">
                                          
                                         <%--   <ItemTemplate>
                                                <asp:Label ID="lblvou" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>--%>

                                                <ItemTemplate>
                                                     <asp:Label ID="lblvou" runat="server"  Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'></asp:Label>
                                                <asp:HyperLink ID="hlnkgvType" runat="server" Font-Size="10px" Target="_blank" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>'
                                                    Width="110px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="txtINgvtdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchequedat" runat="server" BackColor="Transparent"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Narration">
                                            <%--  <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchvenar" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Narration" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNarrat" runat="server" BackColor="Transparent" Style="word-break: break-all"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="payto">
                                            <%--<HeaderTemplate>
                                        <asp:TextBox ID="txtSearchpayto" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Party Name" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblogvpayto" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Vou. Amount">
                                            <%--<HeaderTemplate>
                                        <asp:TextBox ID="txtSearchamt" BackColor="Transparent" BorderStyle="None" runat="server" Width="75px" placeholder="Vou. Amount" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnam" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <%--  <FooterTemplate>
                                        <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvusernam" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usernam")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAuditid" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chakaut"))=="True" %>'
                                                    Width="20px" />
                                                <asp:LinkButton ID="lnkbtnAudit" runat="server" ForeColor="Black" Font-Underline="false" OnClick="lnkbtnAudit_Click"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                                            </ItemTemplate>

                                            <ItemStyle Width="40px" />
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




                        <asp:Panel ID="pnlfrec" runat="server" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvAudit" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvAudit_RowDataBound" ShowFooter="True" AllowSorting="True">
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

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblvoudat" runat="server" BackColor="Transparent"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voucher">
                                            <%-- <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchvounum1" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Voucher No" onkeyup="Search_Gridview(this,2)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                          <%--  <ItemTemplate>
                                                <asp:Label ID="lblvou" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>--%>

                                              <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvAudit" runat="server" Font-Size="10px" Target="_blank" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>'
                                                    Width="110px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque No">
                                            <ItemTemplate>
                                                <asp:Label ID="txtINgvtdesc" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cheque Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblchequedat" runat="server" BackColor="Transparent"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Narration">
                                            <%--  <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchvenar" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Narration" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNarrat" runat="server" BackColor="Transparent" Style="word-break: break-all"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="payto">
                                            <%--<HeaderTemplate>
                                        <asp:TextBox ID="txtSearchpayto" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Party Name" onkeyup="Search_Gridview(this,7)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblogvpayto" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Vou. Amount">
                                            <%--<HeaderTemplate>
                                        <asp:TextBox ID="txtSearchamt" BackColor="Transparent" BorderStyle="None" runat="server" Width="75px" placeholder="Vou. Amount" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                    </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtrnam" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <%--  <FooterTemplate>
                                        <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="75px"></asp:Label>
                                    </FooterTemplate>--%>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                     <%--   <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvusernam" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usernam")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>


                                            <asp:TemplateField HeaderText="Audit Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblauditbydate" runat="server" BackColor="Transparent"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "auditbydate")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Audit Session">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvusernam" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "auditbysession")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Audit By">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvauditbyid" runat="server" BackColor="Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usernam")) %>'
                                                    Width="150px"></asp:Label>
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

