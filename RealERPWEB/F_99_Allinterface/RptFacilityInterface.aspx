﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptFacilityInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.RptFacilityInterface" %>

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
            width: 81px;
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
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });




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
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-primary" OnClick="lnkbtnok_Click">Ok</asp:LinkButton></li>
                            </div>
                        </div>


                        <div class="col-md-4">
                            <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operation</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/F_30_Facility/ComplainForm.aspx" CssClass="dropdown-item" Style="padding: 0 10px">Create Complaints</asp:HyperLink>
                                        <%--<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_30_Facility/EngrCheck.aspx" CssClass="dropdown-item" Style="padding: 0 10px">Engr. Check</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_30_Facility/BudgetForm.aspx" CssClass="dropdown-item" Style="padding: 0 10px">Budget</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_30_Facility/BudgetForm.aspx?Type=Approval" CssClass="dropdown-item" Style="padding: 0 10px">Approval</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_30_Facility/Quotation.aspx" CssClass="dropdown-item" Style="padding: 0 10px">Quotation</asp:HyperLink>--%>


                                    </div>
                                </div>
                            </div>
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
                                                        <asp:ListItem Value="9"></asp:ListItem>




                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div>
                                        <asp:Panel ID="pnlComplainCount" runat="server">
                                            <div class="row">
                                                <h6 class="mx-4">Complaints</h6>
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvComplainList" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvComplainList_RowDataBound" OnRowCommand="gvComplainList_RowCommand" >
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkedit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComplno" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno"))%>' Width="50px"></asp:Label>
                                                                      <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "engrcheck"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lblgvcomplno" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Complaints<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "compldate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcustdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server"  Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Warranty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwarranty" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "warrantydesc"))%>' >
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Communication">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comdesc"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issue Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamt" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuedesc"))%>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>                                                                   
                                                                    <asp:LinkButton ID="lnkProceed" runat="server"  CssClass="btn btn-default btn-xs" CommandName="Select" 
                                                                        CommandArgument="<%# Container.DataItemIndex %>"><span class="fa fa-arrow-right"
                                                                            ></span></asp:LinkButton>
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
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlComplainToDiagnosis" runat="server" Visible="false">
                                            <div class="row">
                                                <h6 class="mx-4">Engineer Check</h6>
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvCmpltoDg" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvCmpltoDg_RowDataBound" OnRowCommand="gvCmpltoDg_RowCommand">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>      
                                                             <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkedit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lbldgno" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno"))%>' Width="50px"></asp:Label>
                                                                     <asp:Label ID="lblisBudget" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isbudget"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lblgvcomplno" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Complaints<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "compldate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcustdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server"  Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Warranty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwarranty" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "warrantydesc"))%>' Width="90px">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Communication">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comdesc"))%>' Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issue Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvApamt" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuedesc"))%>'
                                                                        Width="90px"></asp:Label>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkdg" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>

                                                                     <asp:LinkButton ID="lnkProceed" runat="server"  CssClass="btn btn-default btn-xs" CommandName="Select" 
                                                                        CommandArgument="<%# Container.DataItemIndex %>"><span class=" fa fa-arrow-right"
                                                                            ></span></asp:LinkButton>

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
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlDiagnosis" runat="server" Visible="false">
                                            <div class="row">
                                                <h6 class="mx-4">Budget</h6>
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvDiagnosis" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvDiagnosis_RowDataBound" OnRowCommand="gvDiagnosis_RowCommand" >
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkedit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Complain ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComplain" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno"))%>' Width="50px"></asp:Label>
                                                                     <asp:Label ID="lblisApproval" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isApproval"))%>' Width="50px"></asp:Label>

                                                                    <asp:Label ID="lblisBudget" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isbudgetVal"))%>' Width="50px"></asp:Label>


                                                                    
                                                                    <asp:Label ID="lblComplain1" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Complaints<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "compldate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Diagnosis ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldgno" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lblDiagnosis1" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Diagnosis<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDgDate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dgdate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcustdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Warranty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwarranty" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "warrantydesc"))%>' Width="90px">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Site Visited<br> Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSiteVisitedDate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sitevisiteddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estimated Work<br> Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstimatedWorkDone" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "estimatedwddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Additional Notes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAdditionalNotes" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addremarks"))%>'
                                                                        Width="200px"></asp:Label>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkdg" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
                                                                    </asp:HyperLink>

                                                                     <asp:LinkButton ID="lnkProceed" runat="server"  CssClass="btn btn-default btn-xs" CommandName="Select" 
                                                                        CommandArgument="<%# Container.DataItemIndex %>"><span class=" fa fa-arrow-right"
                                                                            ></span></asp:LinkButton>


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
                                            </div>
                                        </asp:Panel>


                                        <asp:Panel ID="pnlBudget" runat="server" Visible="false">
                                            <div class="row">
                                                <h6 class="mx-4">Approval</h6>
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvBudget" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvBudget_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkedit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Complain ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComplain" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lblComplain1" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Complaints<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComplainDate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "compldate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Diagnosis ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDgNo" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lblDgNo1" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Budget<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbgddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dgdate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcustdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Warranty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwarranty" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "warrantydesc"))%>' Width="90px">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Site Visited<br> Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsitevisiteddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sitevisiteddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Estimated Work<br> Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltestimateddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "estimatedwddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Additional Notes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdnotes" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addremarks"))%>'
                                                                        Width="200px"></asp:Label>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkdg" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
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
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlApproval" runat="server" Visible="false">
                                            <div class="row table-responsive">
                                                <h6 class="mx-4">Quotation</h6>
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvApproval" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvApproval_RowDataBound" OnRowCommand="gvApproval_RowCommand">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>                                                         
                                                            <asp:TemplateField HeaderText="Complain ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComplain" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lblComplain1" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Complain<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComplainDate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "compldate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Diagnosis ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDgNo" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno"))%>' Width="50px"></asp:Label>
                                                                    <asp:HyperLink ID="lblDgNo1" runat="server" Style="text-align: center" Target="_blank"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno1"))%>' Width="50px"></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbgddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "approvalDate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcustdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                        Width="80px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Warranty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwarranty" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "warrantydesc"))%>' Width="90px">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Site Visited<br> Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsitevisiteddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sitevisiteddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Est. Work Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltestimateddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "estimatedwddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Notes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdnotes" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notes"))%>'
                                                                        Width="180px"></asp:Label>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-Width="100px">
                                                                <ItemTemplate>
                                                                     <asp:LinkButton ID="lnkProceed" runat="server"  CssClass="btn btn-default btn-xs" CommandName="Select" 
                                                                        CommandArgument="<%# Container.DataItemIndex %>"><span class=" fa fa-check"
                                                                            ></span></asp:LinkButton>
                                                                    <asp:HyperLink ID="hnkCollection" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs">
                                                                        <span class=" fa fa-receipt"></span>
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
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlMatReq" runat="server" Visible="false">
                                            <div class="row table-responsive">
                                                <h6 class="mx-4">Material Requisition</h6>
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvMatReq" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvMatReq_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkedit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Complain ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComplain" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lblComplain1" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "complno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Complaints<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComplainDate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "compldate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Diagnosis ID #">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDgNo" runat="server" Font-Bold="True" Style="text-align: center" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno"))%>' Width="50px"></asp:Label>
                                                                    <asp:Label ID="lblDgNo1" runat="server"  Style="text-align: center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dgno1"))%>' Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval<br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbgddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "approvalDate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Project Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Customer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcustdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                        Width="180px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Unit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                        Width="80px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Warranty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwarranty" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "warrantydesc"))%>' Width="90px">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                        Width="120px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Site Visited<br> Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsitevisiteddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "sitevisiteddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Est. Work Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltestimateddate" runat="server"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "estimatedwddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Notes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdnotes" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notes"))%>'
                                                                        Width="200px"></asp:Label>
                                                                </ItemTemplate>


                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkdg" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs"><span class=" fa fa-check"></span>
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
                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
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


