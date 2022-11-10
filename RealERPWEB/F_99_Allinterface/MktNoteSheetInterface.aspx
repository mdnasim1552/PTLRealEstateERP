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
            var gvauthorize = $('#<%=this.gvauthorize.ClientID %>');
            var gvrecommended = $('#<%=this.gvrecommended.ClientID %>');
            var gvfirstapp = $('#<%=this.gvfirstapp.ClientID %>');
            var gvfinalapp = $('#<%=this.gvfinalapp.ClientID %>');

            var gvschupdate = $('#<%=this.gvschupdate.ClientID %>');

            gvstatus.Scrollable();
            gvauthorize.Scrollable();
            gvrecommended.Scrollable();
            gvfirstapp.Scrollable();
            gvfinalapp.Scrollable();
            gvschupdate.Scrollable();

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
                            <asp:TextBox ID="txtmtrrf" runat="server" CssClass="form-control" placeholder="Note Sheet ID"></asp:TextBox>
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


                                        <asp:HyperLink ID="HyperLink3" Target="_blank" NavigateUrl="~/F_22_Sal/ClusterSetup?Type=ClusterSetup" runat="server" CssClass="dropdown-item">Cluster Setup</asp:HyperLink>
                                        <div class="dropdown-divider"></div>
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
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-bordered grvContentarea"
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

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtdat" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "noteshtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="noteshtid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "noteshtid"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prospective">
                                            <%-- <HeaderTemplate>
                                                <asp:TextBox ID="txtmtrf" runat="server" Width="100px" placeholder="MTRF NO" onkeyup="Search_Gridview(this,1)" ></asp:TextBox><br />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>

                                                <asp:Label ID="lgvprospective" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="130px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvproject" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvunit" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="100px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusize" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Rate (BDT/Sft)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffurate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Booking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbooking" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffbookingam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Down Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdpayemnt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffdpaymntam")).ToString("#,##0;(#,##0) ;") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvininsam" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Final Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfininsam" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotal" runat="server" Font-Bold="true"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofftunitamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User">

                                            <ItemTemplate>
                                                <asp:Label ID="lbluser" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedempname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Curent Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvstatus" runat="server" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "noteshtst")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                    <HeaderStyle CssClass="grvHeaderNew" />
                                    <PagerStyle CssClass="gvPaginationNew" />
                                    <RowStyle CssClass="grvRowsNew" />
                                    <FooterStyle CssClass="grvFooterNew" />
                                </asp:GridView>

                            </div>

                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlauthorized" Visible="false">

                            <div class="table-responsive">

                                <asp:GridView ID="gvauthorize" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-bordered grvContentarea" OnRowDataBound="gvauthorize_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoaut" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtdataut" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "noteshtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="noteshtid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtidaut" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "noteshtid"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prospective">
                                            <%-- <HeaderTemplate>
                                                <asp:TextBox ID="txtmtrf" runat="server" Width="100px" placeholder="MTRF NO" onkeyup="Search_Gridview(this,1)" ></asp:TextBox><br />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>

                                                <asp:Label ID="lgvprospectiveaut" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="130px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvprojectaut" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvunitaut" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="100px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusizeaut" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Rate (BDT/Sft)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrateaut" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffurate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Booking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbookingaut" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffbookingam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Down Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdpayemntaut" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffdpaymntam")).ToString("#,##0;(#,##0) ;") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvininsamaut" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Final Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfininsamaut" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalaut" runat="server" Font-Bold="true"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofftunitamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User">

                                            <ItemTemplate>
                                                <asp:Label ID="lbluseraut" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedempname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremoveaut" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Are you sure you want delete');" runat="server" ToolTip="Cancel" OnClick="lnkremoveaut_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkauthorize" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Note Sheet Authorized" OnClick="lnkauthorize_Click"><span class=" fa fa-check"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="hlnkView" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="View Details"><i class="fa fa-eye" aria-hidden="true"></i>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="110px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                    <HeaderStyle CssClass="grvHeaderNew" />
                                    <PagerStyle CssClass="gvPaginationNew" />
                                    <RowStyle CssClass="grvRowsNew" />
                                    <FooterStyle CssClass="grvFooterNew" />
                                </asp:GridView>



                            </div>

                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlrecommended" Visible="false">

                            <div class="table-responsive">
                               <asp:GridView ID="gvrecommended" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-bordered grvContentarea" OnRowDataBound="gvrecommended_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNorecm" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtdatrecm" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "noteshtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="noteshtid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtidrecm" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "noteshtid"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prospective">
                                            <%-- <HeaderTemplate>
                                                <asp:TextBox ID="txtmtrf" runat="server" Width="100px" placeholder="MTRF NO" onkeyup="Search_Gridview(this,1)" ></asp:TextBox><br />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>

                                                <asp:Label ID="lgvprospectiverecm" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="130px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvprojectrecm" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvunitrecm" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="100px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusizerecm" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Rate (BDT/Sft)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvraterecm" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffurate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Booking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbookingrecm" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffbookingam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Down Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdpayemntrecm" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffdpaymntam")).ToString("#,##0;(#,##0) ;") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvininsamrecm" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Final Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfininsamrecm" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalrecm" runat="server" Font-Bold="true"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofftunitamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorized">

                                            <ItemTemplate>
                                                <asp:Label ID="lbluserrecm" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "authorizename")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremoverecm" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Are you sure you want delete');" runat="server" ToolTip="Cancel" OnClick="lnkremoverecm_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkrecommended" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Note Sheet Recommended" OnClick="lnkrecommended_Click"><span class=" fa fa-check"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="hlnkViewrecm" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="View Details"><i class="fa fa-eye" aria-hidden="true"></i>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="110px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                    <HeaderStyle CssClass="grvHeaderNew" />
                                    <PagerStyle CssClass="gvPaginationNew" />
                                    <RowStyle CssClass="grvRowsNew" />
                                    <FooterStyle CssClass="grvFooterNew" />
                                </asp:GridView>

                            </div>

                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlfirstApp" Visible="false">

                            <div class="table-responsive">
                                 <asp:GridView ID="gvfirstapp" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-bordered grvContentarea" OnRowDataBound="gvfirstapp_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNofirstapp" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtdatfirstapp" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "noteshtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="noteshtid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtidfirstapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "noteshtid"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prospective">
                                            <%-- <HeaderTemplate>
                                                <asp:TextBox ID="txtmtrf" runat="server" Width="100px" placeholder="MTRF NO" onkeyup="Search_Gridview(this,1)" ></asp:TextBox><br />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>

                                                <asp:Label ID="lgvprospectivefirstapp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="130px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvprojectfirstapp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvunitfirstapp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="100px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusizefirstapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Rate (BDT/Sft)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvratefirstapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffurate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Booking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbookingfirstapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffbookingam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Down Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdpayemntfirstapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffdpaymntam")).ToString("#,##0;(#,##0) ;") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvininsamfirstapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Final Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfininsamfirstapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalfirstapp" runat="server" Font-Bold="true"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofftunitamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recommended">

                                            <ItemTemplate>
                                                <asp:Label ID="lbluserfirstapp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recommendname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremovefirstapp" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Are you sure you want delete');" runat="server" ToolTip="Cancel" OnClick="lnkremovefirstapp_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkfirstapp" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Note Sheet Approval" OnClick="lnkfirstapp_Click"><span class=" fa fa-check"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="hlnkViewrfirstapp" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="View Details"><i class="fa fa-eye" aria-hidden="true"></i>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="110px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                    <HeaderStyle CssClass="grvHeaderNew" />
                                    <PagerStyle CssClass="gvPaginationNew" />
                                    <RowStyle CssClass="grvRowsNew" />
                                    <FooterStyle CssClass="grvFooterNew" />
                                </asp:GridView>


                            </div>

                        </asp:Panel>

                        <asp:Panel runat="server" ID="pnlapproval" Visible="false">
                            <div class="table-responsive">
                              
                                <asp:GridView ID="gvfinalapp" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-bordered grvContentarea" OnRowDataBound="gvfinalapp_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNofinalapp" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtdatfinalapp" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "noteshtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="noteshtid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtidfinalapp" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "noteshtid"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prospective">
                                            <%-- <HeaderTemplate>
                                                <asp:TextBox ID="txtmtrf" runat="server" Width="100px" placeholder="MTRF NO" onkeyup="Search_Gridview(this,1)" ></asp:TextBox><br />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>

                                                <asp:Label ID="lgvprospectivefinalapp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="130px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvprojectfinalapp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvunitfinalapp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="100px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusizefinalapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Rate (BDT/Sft)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvratefinalapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffurate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Booking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbookingfinalapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffbookingam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Down Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdpayemntfinalapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffdpaymntam")).ToString("#,##0;(#,##0) ;") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvininsamfinalapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Final Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfininsamfinalapp" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalfinalapp" runat="server" Font-Bold="true"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofftunitamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved">

                                            <ItemTemplate>
                                                <asp:Label ID="lbluserfinalapp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "firstappname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremovefinalapp" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Are you sure you want delete');" runat="server" ToolTip="Cancel" OnClick="lnkremovefinalapp_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkfinalapp" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Note Sheet Approval" OnClick="lnkfinalapp_Click"><span class=" fa fa-check"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="hlnkViewrfinalapp" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="View Details"><i class="fa fa-eye" aria-hidden="true"></i>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="110px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                    <HeaderStyle CssClass="grvHeaderNew" />
                                    <PagerStyle CssClass="gvPaginationNew" />
                                    <RowStyle CssClass="grvRowsNew" />
                                    <FooterStyle CssClass="grvFooterNew" />
                                </asp:GridView>

                            </div>

                        </asp:Panel>


                        <asp:Panel runat="server" ID="pnlschupdate" Visible="false">


                            <div class="table-responsive">

                             <asp:GridView ID="gvschupdate" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-bordered grvContentarea" OnRowDataBound="gvschupdate_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoschup" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtdatschup" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "noteshtdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="noteshtid" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnoteshtidschup" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "noteshtid"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prospective">
                                            <%-- <HeaderTemplate>
                                                <asp:TextBox ID="txtmtrf" runat="server" Width="100px" placeholder="MTRF NO" onkeyup="Search_Gridview(this,1)" ></asp:TextBox><br />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>

                                                <asp:Label ID="lgvprospectiveschup" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="130px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvprojectschup" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="120px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">

                                            <ItemTemplate>

                                                <asp:Label ID="lgvunitschup" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="100px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvusizeschup" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Rate (BDT/Sft)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrateschup" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffurate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Booking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbookingschup" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffbookingam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Down Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdpayemntschup" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffdpaymntam")).ToString("#,##0;(#,##0) ;") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvininsamschup" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "coffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Final Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfininsamschup" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofffinsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotalschup" runat="server" Font-Bold="true"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cofftunitamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Approved">

                                            <ItemTemplate>
                                                <asp:Label ID="lbluserschup" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "finalappname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                                              
                                                <asp:LinkButton ID="lnkschup" runat="server" CssClass="btn btn-xs btn-default" Visible="false" ToolTip="Note Sheet Approval" OnClick="lnkschup_Click"><span class=" fa fa-check"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="hlnkViewrschup" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="View Details"><i class="fa fa-eye" aria-hidden="true"></i>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="110px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />

                                    <HeaderStyle CssClass="grvHeaderNew" />
                                    <PagerStyle CssClass="gvPaginationNew" />
                                    <RowStyle CssClass="grvRowsNew" />
                                    <FooterStyle CssClass="grvFooterNew" />
                                </asp:GridView>


                            </div>


                        </asp:Panel>
                    </div>



                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
