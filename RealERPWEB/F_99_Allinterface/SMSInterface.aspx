﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SMSInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.SMSInterface" %>

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
            width: 100px;
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



            <div class="container moduleItemWrpper">
                <div class="contentPart">

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A ">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <%--<asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to">Date</asp:Label>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>--%>
                                    <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">Date</asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                    <asp:LinkButton ID="lnkok" runat="server" Text="Reload" OnClick="lnkok_Click" Style="height: 25px; vertical-align: middle" CssClass="btn btn-sm btn-primary"></asp:LinkButton>
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
                                                    <asp:ListItem Value="7"></asp:ListItem>
                                                    <asp:ListItem Value="8"></asp:ListItem>
                                                    <asp:ListItem Value="9"></asp:ListItem>
                                                    <asp:ListItem Value="10"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>

                    <asp:Panel runat="server" ID="pnlinitial" Visible="false">
                        <div class="row">

                            <div class="table-responsive">
                                <asp:GridView ID="grvRptCliMod" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" Width="487px" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvRptCliMod_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ADW No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAdwNo" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcUDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamt" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tracking">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrack" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "track"))   %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlcheck" Visible="false">
                        <div class="row">

                            <div class="table-responsive">
                                <asp:GridView ID="gvcltmodchk" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvcltmodchk_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNochk" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMdescchk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ADW No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladdnochk" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblgvAdwNochk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDatechk" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPDescchk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcUDescchk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCDescchk" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamtchk" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkremovechk" runat="server" ToolTip="Unchecked"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>--%>
                                                <asp:HyperLink ID="lnkchk" Target="_blank" runat="server" ToolTip="Checked"><span style="color:green" class="glyphicon glyphicon-check"></span> </asp:HyperLink>
                                                <asp:LinkButton ID="hlnkprintchk" runat="server" ToolTip="Print" ForeColor="Blue" OnClick="hlnkprintchk_Click" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="65px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="65px" VerticalAlign="Top" />
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
                    <asp:Panel runat="server" ID="pnlaudit" Visible="false">
                        <div class="row">

                            <div class="table-responsive">
                                <asp:GridView ID="gvCltmodaduit" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvCltmodaduit_RowDataBound">
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

                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMdescad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ADW No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladdnoad" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblpactcodead" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblgvAdwNoad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDatead" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPDescad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcUDescad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCDescad" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamtad" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremoveadt" runat="server" OnClick="lnkremoveadt_Click" ToolTip="Remove"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="lnkadt" Target="_blank" runat="server" ToolTip="Audit"><span style="color:green" class="glyphicon glyphicon-check"></span> </asp:HyperLink>
                                                <asp:LinkButton ID="hlnkprintadt" runat="server" ToolTip="Print" OnClick="hlnkprintadt_Click" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                </asp:LinkButton>
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

                                </asp:GridView>

                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlapproval" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvCltmodapp" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Style="text-align: left" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvCltmodapp_RowDataBound">
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

                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMdescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ADW No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladdnoap" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblpactcodeap" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="80px"></asp:Label>

                                                <asp:Label ID="lblgvAdwNoap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDateap" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "addate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcUDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCDescap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cusname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkgvamtap" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Font-Underline="false" Style="background-color: Transparent; color: Black;"
                                                    Target="_blank"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkremoveap" runat="server" OnClick="lnkremoveap_Click" ToolTip="Cancel"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                <asp:HyperLink ID="lnkapp" runat="server" Target="_blank" ToolTip="Approval"><span style="color:green" class="glyphicon glyphicon-check"></span> </asp:HyperLink>
                                                <asp:LinkButton ID="hlnkprintapp" runat="server" ToolTip="Print" OnClick="hlnkprintapp_Click" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                </asp:LinkButton>
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

                                </asp:GridView>
                            </div>
                        </div>

                    </asp:Panel>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

