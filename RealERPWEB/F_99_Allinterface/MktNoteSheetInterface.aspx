<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="MktNoteSheetInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.MktNoteSheetInterface" %>

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
            display: inline-block;
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
            width: 140px;
        }

        .tbMenuWrp table tr td {
            /*height: 50px;*/
            /*width: 81px;*/
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
            height: 38px;
            margin: -2px auto -22px;
            padding: 8px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 40px;
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
    <script>
        function Search_Gridview(strKey, cellNr) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvstatus.ClientID %>");
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

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            var comcod = <%=this.GetCompCode()%>;
            
            var gvstatus = $('#<%=this.gvstatus.ClientID %>');
            var gvreqchk = $('#<%=this.gvreqchk.ClientID %>');
            var gvreqaprv = $('#<%=this.gvreqaprv.ClientID %>');
            var gvgatepass = $('#<%=this.gvgatepass.ClientID %>');
            var gvapproval = $('#<%=this.gvapproval.ClientID %>');
            var gvaudit = $('#<%=this.gvaudit.ClientID %>');
            var gvaccount = $('#<%=this.gvaccount.ClientID %>');

            gvstatus.Scrollable();
            gvreqchk.Scrollable();
            gvreqaprv.Scrollable();
            gvgatepass.Scrollable();
            gvapproval.Scrollable();
            gvaudit.Scrollable();
            gvaccount.Scrollable();

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            //$('.chzn-select').chosen({ search_contains: true });


            function openModal() {

                $('#modalReqEdit').modal('toggle');
            }

            function CloseModal() {

                $('#modalReqEdit').modal('hide');
            }
        }

    </script>

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
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary" type="button">From</button>
                                </div>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <button class="btn btn-secondary" type="button">To</button>
                                </div>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txttodate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txtmtrrf" runat="server" CssClass="form-control" placeholder="MTR Ref..."></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary" OnClick="lnkok_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink2" Target="_blank" NavigateUrl="~/F_22_Sal/MktSampleNoteSheet?Type=Entry" runat="server" CssClass="dropdown-item">Sample Note Sheet</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_22_Sal/MktGrandNoteSheet?Type=Entry" runat="server" CssClass="dropdown-item">Grand Note Sheet</asp:HyperLink>
                                       
                                      


                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
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

                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                        <asp:Panel runat="server" ID="pnlstatus" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvstatus" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvstatus_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="mtreqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MTRF NO">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtmtrf" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="MTRF NO" onkeyup="Search_Gridview(this,1)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:Label ID="lgvmtrno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1")) %>'
                                                    Width="100px"></asp:Label>


                                                <%-- <asp:HyperLink ID="lgvmtrno" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1"))%>' Width="100px">
                                      
                                                </asp:HyperLink>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MTRF Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrd" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MTRF No</Br> (Manual)">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblmtrfno" runat="server"
                                                    Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref"))%>'
                                                    Width="70px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="From Project">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtfrmprj" BackColor="Transparent" BorderStyle="None" runat="server" Width="180px" placeholder="From Project" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgtfprj" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Project">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txttoprj" BackColor="Transparent" BorderStyle="None" runat="server" Width="180px" placeholder="To Project" onkeyup="Search_Gridview(this,4)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgttprj" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lgresc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrfqty")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tracking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrack" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rstatus"))   %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtuser" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="User Name" onkeyup="Search_Gridview(this,8)"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbluser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                    Width="60px"></asp:Label>
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

                        <asp:Panel runat="server" ID="pnlreqchk" Visible="false">

                            <div class="table-responsive">
                                <asp:GridView ID="gvreqchk" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvreqchk_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNochk" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MTRF NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtreqnochk" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno")) %>'
                                                    Width="100px"></asp:Label>
                                                <asp:Label ID="lblmtreqnochk1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MTRF Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrdatrchk" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MTRF No</Br> (Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrrefchk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="From Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgtfprjchk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgttprjchk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lgrescchk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrfqty")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamtchk" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremovechk" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Are you sure you want delete');" runat="server" ToolTip="Cancel" OnClick="lnkremovechk_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="lnkreqchk" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Requisition Checked"><span class=" fa fa-check"></span> </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Top" />
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

                        <asp:Panel runat="server" ID="pnlReqAprv" Visible="false">

                            <div class="table-responsive">
                                <asp:GridView ID="gvreqaprv" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvreqaprv_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNorap" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MTRF NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltrnnorap" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno")) %>'
                                                    Width="100px"></asp:Label>
                                                <asp:Label ID="lgvmtrnorap1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MTRF Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrdatrap" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MTRF No</Br> (Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrrefrap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="From Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgtfprjrap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgttprjrap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lgrescrap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrfqty")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamtrap" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremoverap" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Are you sure you want delete');" runat="server" ToolTip="Cancel" OnClick="lnkremoverap_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="lnkreqaprv" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Gate Pass"><span class=" fa fa-check"></span> </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Top" />
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

                        <asp:Panel runat="server" ID="pnlgatepass" Visible="false">

                            <div class="table-responsive">
                                <asp:GridView ID="gvgatepass" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvgatepass_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNogp" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MTRF NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltrnnog" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno")) %>'
                                                    Width="100px"></asp:Label>
                                                <asp:Label ID="lgvmtrnogp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MTRF Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrdgp" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MTRF No</Br> (Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrdgpmanual" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="From Prj code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgtfprjgpcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactcode"))%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgtfprjgp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgttprjgp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lgrescgp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrfqty")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamtgp" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Operations">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremovegp" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Are you sure you want delete');" runat="server" ToolTip="Cancel" OnClick="lnkremovegp_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="lnkgpass" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Gate Pass"><span class=" fa fa-check"></span> </asp:HyperLink>
                                                <asp:HyperLink ID="lnkgpareqedit" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Requisition Edit"><span class=" fa fa-edit"></span> </asp:HyperLink>

                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
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

                        <asp:Panel runat="server" ID="pnlapproval" Visible="false">
                            <div class="table-responsive">
                                <asp:GridView ID="gvapproval" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvapproval_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoap" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gate Pass No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgetpasno" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpno")) %>'
                                                    Width="100px"></asp:Label>
                                                <asp:Label ID="lgvmtrnoap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpno1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Getpass No</Br> (Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldapgetmanual" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MTRF No </Br>(Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrno1" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno")) %>'
                                                    Width="70px"></asp:Label>

                                                <asp:Label ID="lblmtrdapmanual" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MTRF Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrdap" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="From Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgtfprjap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgttprjap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lgrescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrfqty")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvamtap" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremoveap" CssClass="btn btn-xs btn-default" OnClick="lnkremoveap_Click" OnClientClick="return cofirm('Are you want to delete?');" runat="server" ToolTip="Cancel"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="lnkapp" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Approval"><span style="color:green" class="fa fa-check"></span> </asp:HyperLink>
                                                <asp:HyperLink ID="lnkgpapdit" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Approval Edit"><span class=" fa fa-edit"></span> </asp:HyperLink>

                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Top" />
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
                        <asp:Panel runat="server" ID="pnlaudit" Visible="false">

                            <div class="table-responsive">

                                <asp:GridView ID="gvaudit" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvaudit_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoad" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgetrnno" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno")) %>'
                                                    Width="100px"></asp:Label>
                                                <asp:Label ID="lgvmtrnoad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="MTRF No </Br> (Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrdauditmanu" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Getpass No</Br> (Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblauditgetmanual" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrdad" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="From Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgtfprjad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgttprjad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lgrescad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrfqty")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamtad" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyInprPrintRecv" runat="server" ToolTip="Print MTR Received Info" Target="_blank" CssClass="btn btn-default btn-xs"><span class="fa fa-print"></span></asp:HyperLink>
                                                <asp:HyperLink ID="lnkad" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ToolTip="Audit"><span style="color:green" class="fa fa-check"></span> </asp:HyperLink>

                                                <asp:LinkButton ID="lnkremovead" CssClass="btn btn-xs btn-default" runat="server" OnClick="lnkremovead_Click" OnClientClick="return confirm('Are you sure want to delete?');" ToolTip="Cancel"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>


                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
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

                        <asp:Panel runat="server" ID="pnlaccount" Visible="false">


                            <div class="table-responsive">

                                <asp:GridView ID="gvaccount" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoac" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transfer No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltrannoac" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno")) %>'
                                                    Width="100px"></asp:Label>
                                                <asp:Label ID="lgvmtrnoac" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno1")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MTRF No </Br> (Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtracctmanu" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Getpass No</Br> (Manual) ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaccgetmanual" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpref")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Transfer Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmtrdac" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgtfprjac" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc"))%>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Project">
                                            <ItemTemplate>
                                                <asp:Label ID="lgttprjac" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lgrescac" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrfqty")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamtac" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremoveac" CssClass="btn btn-xs btn-default" OnClick="lnkremoveac_Click" OnClientClick="return confirm('Are you sure want to delete?');" runat="server" ToolTip="Remove"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Top" />
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


            <%--            <div id="modalReqEdit" class="modal fade   " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <i class="fa fa-hand-point-right"></i>Requisition Information </h4>
                            <button type="button" class="btn btn-xs pull-right" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>

                        </div>
                        <div class="modal-body ">
                            <div class="row">
                                <asp:GridView ID="gvMtrReInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="16px" OnRowDeleting="gvMtrReInfo_RowDeleting">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reqno" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtfpactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvttpactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server"
                                                    Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))   %>'
                                                    Width="100px">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspecification" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlspecification" runat="server" Width="120px">
                                                </asp:DropDownList>
                                            </EditItemTemplate>

                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqNo1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno1")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnResFooterTotal" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lbtnResFooterTotal_Click">Total :</asp:LinkButton>

                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmrfno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmtrfqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Bal. Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbalqty" runat="server" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Approved Qty.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvaprovedQty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "getpqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved </br> " Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvaprovRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:CommandField ShowDeleteButton="True" />

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:LinkButton ID="btnSaveComments" runat="server" OnClientClick="CloseModal();" CssClass="btn btn-primary">Save</asp:LinkButton>
                            <button class="btn btn-primary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            --%>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
