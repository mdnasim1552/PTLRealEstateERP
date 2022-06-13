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
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-primary">Ok</asp:LinkButton></li>
                            </div>
                        </div>


                        <div class="col-md-4">
                            <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operation</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/F_30_Facility/ComplainForm.aspx" CssClass="dropdown-item" Style="padding: 0 10px">Complains</asp:HyperLink>
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
                                        <asp:Panel ID="pnlReqInfo" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvReqInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True">
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


