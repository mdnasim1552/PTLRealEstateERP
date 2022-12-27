<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptBgdPrjojectNew.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.RptBgdPrjojectNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .multiselect {
            width: 350px !important;
            height: 27px !important;
        }

        .multiselect-text {
            width: 200px !important;
        }

        .multiselect-container {
            height: 250px !important;
            width: 350px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }
    </style>
    <style type="text/css">
        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                width: 140px;
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


        .tbMenuWrp table tr td {
            /*height: 50px;*/
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
            width: 95px;
            font-size: 10px;
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 15px;
            height: 39px;
            margin: -2px auto -22px;
            padding: 4px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 42px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 8px;
            padding-bottom: 6px;
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
            /*color: rgba(255, 255, 255, 0.7);*/
            color: #000;
        }

        .common_color {
            background-color: #faf5f5;
        }
        .table th , .table td{
            padding:4px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            try {
                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);
                });

                var gv1 = $('#<%=this.gvBudgetBal.ClientID %>');
                gv1.Scrollable();

              <%--  var gvBgd = $('#<%=this.gvBgd.ClientID %>');
                gvBgd.Scrollable();--%>

                var gvadwrk = $('#<%=this.gvadwrk.ClientID %>');
                $(function () {
                    $('[id*=DropCheck1]').multiselect({
                        includeSelectAllOption: true

                    });

                });

                gvadwrk.gridviewScroll({
                    width: 1140,
                    height: 420,
                    arrowsize: 30,
                    railsize: 16,
                    barsize: 12,
                    varrowtopimg: "../Image/arrowvt.png",
                    varrowbottomimg: "../Image/arrowvb.png",
                    harrowleftimg: "../Image/arrowhl.png",
                    harrowrightimg: "../Image/arrowhr.png",
                    freezesize: 6
                });

                $('#<%=this.gvRptResBasis.ClientID%>').tblScrollable();
                $('#<%=this.gvmatreq.ClientID%>').tblScrollable();
                $('.chzn-select').chosen({ search_contains: true });

            }

            catch (e) {
                alert(e);
            }

        };
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
            <div class="card mt-2">
                <div class="card-body">

                    <div class="row mt-4">
                        <class class="tabMenu">

                            <div class="tbMenuWrp nav nav-tabs">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">

                                    <asp:ListItem Value="MasterBgdGrWise"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Income Statement</div></div></div></asp:ListItem>
                                    <%--<asp:ListItem Value="MasterBgd"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>IS-(Resource)</div></div></div></asp:ListItem>--%>
                                    <asp:ListItem Value="WrkVsResource"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Work Vs.Resource</div></div></div></asp:ListItem>
                                    <asp:ListItem Value="BudgetBal"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Budget Balance</div></div></div></asp:ListItem>
                                    <asp:ListItem Value="MasterBgdResGrWiseDet"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Project Costing</div></div></div></asp:ListItem>
                                    <asp:ListItem Value="BgdCostResBasis02"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Mat. Group Cost</div></div></div></asp:ListItem>
                                    <%--<asp:ListItem Value="MasterBgdGrWiseDet"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Cost-Details(Work)</div></div></div></asp:ListItem>--%>
                                    <asp:ListItem Value="MasterBgdFlrDet"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Details(Floor Wise)</div></div></div></asp:ListItem>
                                    <asp:ListItem Value="BgdAlocBal"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Balance Info.</div></div></div></asp:ListItem>
                                    <%--<asp:ListItem Value="MasterBgdAcWk"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>IS-(Work)</div></div></div></asp:ListItem>--%>

                                    <%--<asp:ListItem Value="BudgetedCost"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Total Cost</div></div></div></asp:ListItem>--%>


                                    <%--<asp:ListItem Value="BgdWkVsActual"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>IS-(Budget Vs Actual)</div></div></div></asp:ListItem>--%>




                                    <%--<asp:ListItem Value="BudgetAlocation"><div class='circle-tile'><div class='circle-tile-content common_color'><div class='circle-tile-description text-faded'>Balance After App.</div></div></div></asp:ListItem>--%>
                                </asp:RadioButtonList>

                            </div>
                    </div>

                

                
                        <asp:Panel ID="panelHead" runat="server">
                            <div class="row mt-3">

                                <div class=" col-md-1 d-none">
                                    <asp:TextBox ID="txtSrcProject" runat="server" CssClass=" inputtextbox" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="imgbtnFindProject" Visible="false" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnFindProject_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblProjectList" CssClass="lblTxt lblName " runat="server" Text="Project Name:"></asp:Label>

                                    <asp:DropDownList ID="ddlProjectName" runat="server" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="13" AutoPostBack="true">
                                    </asp:DropDownList>

                                </div>

                                <div class="col-md-1 ml-2" style="margin-top:19px;">
                                    <asp:LinkButton ID="lbtOk" runat="server" CssClass="btn btn-primary" OnClick="lbtOk_Click">Ok</asp:LinkButton>

                                </div>
                                
                            <div class=" col-md-2" runat="server" visible="false" id="rptgrp" >

                                <asp:Label ID="Label2" CssClass="smLbl_to" runat="server" Text="Report"></asp:Label>
                                <asp:DropDownList ID="ddlReport" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                    <asp:ListItem Value="MasterBgdGrWise">IS-Summary</asp:ListItem>
                                    <asp:ListItem Value="MasterBgd">IS-(Resource)</asp:ListItem>
                                    <asp:ListItem Value="MasterBgdGrWiseDet">Cost-Details(Work)</asp:ListItem>

                                </asp:DropDownList>

                            </div>
                                <div class=" col-md-1">

                                <asp:Label ID="lblPage" CssClass="smLbl_to" runat="server" Text="Size"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                                 <div class="col-md-1" style="margin-top:19px;">
                                <asp:CheckBox ID="chkSum" runat="server" TabIndex="10" Text="Summary" CssClass="btn btn-primary checkBox" Visible="false" Checked="true" />

                            </div>
                            </div>



                            <div class="row mt-2 pb-3">
                                
                            <div class="col-md-2">
                                 <asp:Label ID="lblfrmdate" runat="server" CssClass="form-label" Visible="false">Date</asp:Label>

                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" AutoComplete="off" Visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>

                            <div class="col-md-2">
                             


                                <asp:Label ID="lbltodate" runat="server" CssClass="form-label" Visible="false">To</asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" AutoComplete="off" Visible="false"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                            </div>
                           












                        </asp:Panel>
                 
                            <div class="row">
                                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="ViewBgd" runat="server">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="form-group">

                                <div class=" col-md-3  pading5px">

                                    <asp:Label ID="lblConArea" CssClass="smLbl_to" runat="server"></asp:Label>
                                    <asp:Label ID="lblSalArea" CssClass=" smLbl_to" runat="server"></asp:Label>
                                </div>

                                <div class=" col-md-3  pading5px" id="lblrptgrp" runat="server" visible="false">

                                    <asp:Label ID="lblRptGroup" CssClass="lblTxt lblName" runat="server" Text="Group"></asp:Label>
                                    <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                        <asp:ListItem>Main</asp:ListItem>
                                        <asp:ListItem>Sub-1</asp:ListItem>
                                        <asp:ListItem>Sub-2</asp:ListItem>
                                        <asp:ListItem>Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                                <div class=" col-md-1 pading5px " id="lblgrp" runat="server" visible="false">
                                    <asp:Label ID="lblgroup" runat="server" CssClass=" lblTxt lblName">Group</asp:Label>
                                    <asp:TextBox ID="txtSrcGroup" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindgroup" runat="server" Visible="false" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindgroup_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>
                                <div class="col-md-3" id="ddlgrp" runat="server" visible="false">
                                    <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control multiselect-search" Style="min-width: 100px !important;" SelectionMode="Multiple"></asp:ListBox>


                                </div>


                            </div>

                            <div class="form-group">
                            </div>
                            <div class=" clearfix"></div>
                        </asp:Panel>
                        <asp:GridView ID="gvBgd" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="640px" AllowPaging="True" OnPageIndexChanging="gvBgd_PageIndexChanging"
                            OnRowDataBound="gvBgd_RowDataBound" OnRowCreated="gvBgd_RowCreated">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")   %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdRat" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "devcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saleable Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvacamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconcostwinfla" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adevcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saleable Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalcostwinfla" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "asalcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>




                            </Columns>








                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>



                        <asp:GridView ID="gvbgdgrwise" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvbgdgrwise_RowDataBound"
                            ShowFooter="True" Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkgvActDescgrwise" runat="server" ForeColor="Black"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                          
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")): "")   %>'
                                            Width="300px" OnClick="lnkgvActDescgrwise_OnClick">



                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnitgrwise" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvBgdamtgrwise" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Cost Per SFT">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconcostgrwise" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "devcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saleable Cost Per SFT">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalcostgrwise" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>


                        <asp:GridView ID="gvbgdgrwisedet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvbgdgrwisedet_RowDataBound"
                            ShowFooter="True" Width="630px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkgvActDescgrwisedet" runat="server" ForeColor="Black" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                          
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")): "")   %>'
                                            Width="390px" OnClick="lnkgvActDescgrwisedet_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnitgrwisedet" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvBgdamtgrwisedet" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Cost Per SFT">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconcostgrwisedet" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "devcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saleable Cost Per SFT">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalcostgrwisedet" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>

                    </asp:View>
                    <asp:View ID="PrjInfo" runat="server">
                        <asp:GridView ID="gvprjInf" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim(): "") %>'
                                            Width="270px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvval" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>

                    </asp:View>
                    <asp:View ID="RptMasterBgdSp" runat="server">
                        <asp:Panel ID="Panel2" runat="server">

                            <div class="form-group">
                                <div class=" col-md-8 pading5px">

                                    <asp:Label ID="lblConAreasp" CssClass="smLbl_to" runat="server"></asp:Label>
                                    <asp:Label ID="lblSalAreasp" CssClass=" smLbl_to" runat="server"></asp:Label>



                                </div>
                                <div class=" clearfix"></div>
                            </div>

                            <div class=" clearfix"></div>
                        </asp:Panel>

                        <asp:GridView ID="gvBgdsp" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvBgdsp_PageIndexChanging" OnRowDataBound="gvBgdsp_RowDataBound"
                            ShowFooter="True" Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDescsp" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")   %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnitsp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvBgdamtsp" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconcostsp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "devcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saleable Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalcostsp" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>

                    </asp:View>
                    <asp:View ID="ViewWorkVsResource" runat="server">
                       
                            <div class="col-md-12">
                                <asp:Panel ID="Panel3" runat="server">
                            <div class="row pb-2 mt-2">
                                <div class=" col-md-3  pading5px asitCol3">
                                    <asp:LinkButton ID="lbtnFloorList" runat="server" CssClass="form-label" OnClick="lbtnFloorList_Click">Floor</asp:LinkButton>

                                    <asp:DropDownList ID="ddlFloorList" runat="server" CssClass="form-control">
                                        <asp:ListItem>Main</asp:ListItem>
                                        <asp:ListItem>Sub-1</asp:ListItem>
                                        <asp:ListItem>Sub-2</asp:ListItem>
                                        <asp:ListItem>Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </asp:Panel>
                            </div>
                        
                        
                        <div class="table table-responsive">
                            <asp:GridView ID="gvWrkVsRes" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnPageIndexChanging="gvWrkVsRes_PageIndexChanging" ShowFooter="True" Width="640px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvFloor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Description" FooterText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemDes" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc"))     %>'
                                                Width="270px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resource">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResDes" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))     %>'
                                                Width="270px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvresqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvresamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:View>
                    <asp:View ID="ViewBdgetedCost" runat="server">
                        <asp:Panel ID="Panel4" runat="server">

                            <div class="form-group">
                                <div class=" col-md-3  pading5px asitCol3">

                                    <asp:Label ID="lblConAreatc" CssClass="lblTxt lblName" runat="server"></asp:Label>
                                    <asp:Label ID="lblSalAreatc" CssClass=" smLbl_to" runat="server"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:GridView ID="gvBgdtc" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvBgdtc_PageIndexChanging" OnRowDataBound="gvBgdtc_RowDataBound"
                            ShowFooter="True" Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDesctc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")   %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnittc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdqtytc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdratetc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdamttc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconcosttc" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "devcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </asp:View>
                    <asp:View ID="ViewBGDAllC" runat="server">



                        <asp:GridView ID="gvBgdAlc" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="640px" AllowPaging="True" OnPageIndexChanging="gvBgdAlc_PageIndexChanging"
                            OnRowDataBound="gvBgdAlc_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")   %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Master Budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budget Approved">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBGDAl" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "alcamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance in %">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBalP" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balp")).ToString("#,##0.00;(#,##0.00); ") + "%" %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>



                    </asp:View>
                    <asp:View ID="ViewBgdBalVsAlc" runat="server">
                     
                            <div class="col-md-12">
                                <asp:Panel ID="Panel6" runat="server">

                            <div class="row mt-2 pb-2">
                                <div class=" col-md-2">
                                    <asp:LinkButton ID="lbtnBgdFlr" runat="server" CssClass="form-label" OnClick="lbtnBgdFlr_Click">Floor</asp:LinkButton>

                                    <asp:DropDownList ID="ddlBgdFloor" runat="server" CssClass="form-control">
                                        <asp:ListItem>Main</asp:ListItem>
                                        <asp:ListItem>Sub-1</asp:ListItem>
                                        <asp:ListItem>Sub-2</asp:ListItem>
                                        <asp:ListItem>Sub-3</asp:ListItem>
                                        <asp:ListItem Selected="True">Details</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class=" clearfix"></div>
                            </div>


                        </asp:Panel>
                            </div>
                       
                        

                        <asp:GridView ID="gvBgdAlcBal" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvBgdAlcBal_PageIndexChanging" ShowFooter="True" Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Floor">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvFloor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description" FooterText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvItemDes" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc"))     %>'
                                            Width="220px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvItemUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvItemQty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvResDes" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))     %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mat. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvresamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=""></asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWorkqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "abgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mat. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvMatqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate(Actual)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "alrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFActAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdalamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=""></asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDifqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDifAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                            Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDifamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "difamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>

                    </asp:View>
                    <asp:View ID="ViewLandPurchase" runat="server">
                        <asp:GridView ID="gvLandPurreg" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvLandPurreg_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgpslno" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "groupdesc")) 
                                                                           %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDesclp" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) 
                                                                           %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnitlp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dhag No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdhagno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dhagno1")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Purchase Target">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpurtarget" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dhagtpur")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpurbal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dhagbal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Land Owner">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlandowner" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Land Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlandsize" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsize")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Purchases">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlpurchase" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purchase")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvlnbalance" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bal")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewBudgetBalance" runat="server">
                        
                            <div class="col-md-12">
                                <asp:Panel ID="Panel7" runat="server">
                            <div class="row mt-2 pb-2">

                                <div class=" col-md-2">
                                    <asp:Label ID="lblFdate" runat="server" CssClass="form-label"
                                        Text="Date"></asp:Label>

                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>

                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>



                                </div>
                                <div class=" clearfix"></div>
                            </div>


                        </asp:Panel>
                            </div>
                       
                        
                        <asp:GridView ID="gvBudgetBal" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvBudgetBal_RowDataBound" ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Material Name">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFTotalbbal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: left" Width="80px">Total</asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmatbbal" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) 
                                                                           %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnitbbal" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbgdqtybal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0;(#,##0);") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbgdratebal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0;(#,##0);") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBgdAmtBal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbgdamtbbal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0);") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvacqtybal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0;(#,##0);") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvacratebal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvrate")).ToString("#,##0;(#,##0);") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAcAmtBal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvacamtbal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvamt")).ToString("#,##0;(#,##0);") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbalqtybal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0);") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbalratebal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balrate")).ToString("#,##0;(#,##0);") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBalAmtBal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbalamtbbal" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </asp:View>
                    <asp:View ID="ViewMasterbgdgrwise" runat="server">

                        <div class="form-group">
                            <div class=" col-md-8 pading5px">

                                <asp:Label ID="lblConAreagrwise" CssClass="smLbl_to" runat="server"></asp:Label>
                                <asp:Label ID="lblSalAreagrwise" CssClass=" smLbl_to" runat="server"></asp:Label>



                            </div>
                            <div class=" clearfix"></div>
                        </div>
                        <%--<asp:GridView ID="gvbgdgrwise" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnRowDataBound="gvbgdgrwise_RowDataBound"
                                ShowFooter="True" Width="640px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Description">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkgvActDescgrwise" runat="server" ForeColor="Black"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                          
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")): "")   %>'
                                                Width="300px" OnClick="lnkgvActDescgrwise_OnClick">



                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnitgrwise" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvBgdamtgrwise" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Construction Cost Per SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvconcostgrwise" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "devcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Saleable Cost Per SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsalcostgrwise" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>--%>
                    </asp:View>
                    <asp:View ID="ViewMasterbgdgrwiseDet" runat="server">
                        <div class="form-group">


                            <%--<div class=" col-md-1 pading5px ">
                                    <asp:Label ID="lblgroup" runat="server" CssClass=" lblTxt lblName">Group</asp:Label>
                                    <asp:TextBox ID="txtSrcGroup" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnFindgroup" runat="server" Visible="false" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindgroup_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>
                                <div class="col-md-3">
                                    <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control multiselect-search" Style="min-width: 100px !important;" SelectionMode="Multiple"></asp:ListBox>


                                </div>--%>
                            <div class=" col-md-3 pading5px  asitCol3">
                                <asp:Label ID="lblConAreagrwisedet" CssClass="smLbl_to" runat="server"></asp:Label>
                                <asp:Label ID="lblSalAreagrwisedet" CssClass=" smLbl_to" runat="server"></asp:Label>



                            </div>
                            <div class=" clearfix"></div>
                        </div>
                        <%--<asp:GridView ID="gvbgdgrwisedet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnRowDataBound="gvbgdgrwisedet_RowDataBound"
                                ShowFooter="True" Width="630px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Description">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkgvActDescgrwisedet" runat="server" ForeColor="Black" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                          
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")): "")   %>'
                                                Width="390px" OnClick="lnkgvActDescgrwisedet_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnitgrwisedet" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvBgdamtgrwisedet" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Construction Cost Per SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvconcostgrwisedet" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "devcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Saleable Cost Per SFT">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsalcostgrwisedet" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>--%>
                    </asp:View>
                    <asp:View ID="ViewAddbudget" runat="server">
                        <div class="row">
                            <asp:GridView ID="gvadwrk" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" OnRowCreated="gvadwrk_RowCreated">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNoadw" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Floor">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvFlooradw" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdes")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Description" FooterText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemDesadw" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc"))     %>'
                                                Width="270px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvItemUnitadw" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdrateadw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" budgeted Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdqtyadw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Budgeted Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFbgdamtad" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbgdamtad" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Additional-1 <br/>Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvadqty1adw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adqty1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Additional-1 <br/> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvadamt1adw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adamt1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFadamt1adw" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Additional-2 <br/> Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvadqty2adw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adqty2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Additional-2 <br/> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvadamt2adw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adamt2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFadamt2adw" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Additional-3 <br/> Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvadqty3adw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adqty3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="  Additional-3 <br/>Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvadamt3adw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adamt3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFadamt3adw" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" Total Addition">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtoadqtyadw" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toadqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Additional Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFaddamtad" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvaddamtad" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toadamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>









                                    <asp:TemplateField HeaderText=" Total Budgeted Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtobgdamtad" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgvtobgdamtad" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tobgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>


                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </asp:View>

                    <asp:View ID="ViewBudgetVsActual" runat="server">
                        <asp:GridView ID="gvbgdvac" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvbgdvac_RowDataBound"
                            ShowFooter="True" Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNobgdvac" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">

                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblheader" runat="server" Text="Description"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-danger btn-xs fa fa-file-excel-o" ToolTip="Export to Excel"></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lgvActDescbgdvac" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")   %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnitbgdvac" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvBgdamtbgdvac" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvactualcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>

                    </asp:View>
                    <asp:View ID="Viewbgdcostres02" runat="server">
                        <asp:GridView ID="gvRptResBasis" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            Width="633px" ShowFooter="True" OnRowDataBound="gvRptResBasis_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"
                                            Font-Size="12px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Resource Description">
                                    <FooterTemplate>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="text-align: right" Text="Total Cost:" Width="110px"></asp:Label>

                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'
                                            Width="300px"></asp:Label>
                                        <asp:Label ID="Label11" runat="server" Font-Bold="False" Style="display: none;" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc1")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <FooterTemplate>
                                        <asp:Label ID="lbftTqty" runat="server" Font-Size="Small"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptQty1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bgd. Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptRat1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>

                                        <asp:Label ID="lblgvFTotalCost" runat="server" Font-Bold="True"
                                            Font-Size="12px" Style="text-align: right" Width="80px"></asp:Label>



                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptAmt1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rptamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" VerticalAlign="top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Percentage">
                                    <FooterTemplate>
                                        <table style="width: 10%; height: 48px;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblgvFPercent" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPer" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00);")+"%" %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" Font-Size="14px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:View>
                    <asp:View runat="server" ID="ViewBgdFloor">
                        <asp:GridView ID="gvBgdFloor" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True" Width="640px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvflrDescs" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "flrdesc"))%>' Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Budgeted">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvBgdamtflF" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBgdamtfl" runat="server" Font-Bold="false" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Construction Cost">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvconcostflF" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvconcostfl" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "concost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Saleable Cost">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvsalcostflF" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvsalcostfl" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </asp:View>
                    <asp:View runat="server" ID="ViewMatRequirement">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class=" form-horizontal">
                                    <div class="form-group">

                                        <div class=" col-md-4  pading5px asitCol4">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName"
                                                Text="Date"></asp:Label>

                                            <asp:TextBox ID="txtDatemreq" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                            <cc1:CalendarExtender ID="txtDatemreq_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDatemreq"></cc1:CalendarExtender>



                                        </div>
                                        <div class=" clearfix"></div>
                                    </div>
                            </fieldset>

                        </div>
                        <asp:GridView ID="gvmatreq" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="True" Width="640px" OnRowDataBound="gvmatreq_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0mreq" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <ItemTemplate>
                                        <asp:HyperLink Target="_blank" ID="hlnkgvrsirdescmreq" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="300px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvunitmreq" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit"))%>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Received">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvrcvqty" runat="server" Font-Bold="false" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transfer In">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvrtrnin" runat="server" Font-Bold="false" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trninqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Transfer Out">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvrtrnout" runat="server" Font-Bold="false" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnoutqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Inprocess">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvinproqty" runat="server" Font-Bold="false" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inproqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Total">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtqty" runat="server" Font-Bold="false" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Budgeted Analysis">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvbgdqtymreq" runat="server" Font-Bold="false" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Requisition">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvrequiredqty" runat="server" Font-Bold="false" ForeColor="Black" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "requirqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>




                    </asp:View>
                    <asp:View runat="server" ID="ViewMasterbudgetresdet">

                        <asp:GridView ID="gvbgrgrpdisedet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvbgrgrpdisedet_RowDataBound"
                            ShowFooter="True" Width="630px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Description">
                                    <HeaderTemplate>
                                        <table style="width: 30%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="xLabel13" runat="server" Font-Bold="True"
                                                        Text="Description" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCBdataExel" runat="server" CssClass="btn btn-xs"><i class="fa fa-file-excel"></i></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkgvActDescgrwisedetres" runat="server" ForeColor="Black" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                          
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")): "")   %>'
                                            Width="390px" OnClick="lnkgvActDescgrwisedetres_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnitgrwisedetres" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Budgeted">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvBgdamtgrwisedetres" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Actual Cost">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvacamgrwisedetres" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Actual<br/>(Current Month)">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvacamdrgrwisedetres" runat="server" Font-Bold="false" ForeColor="Black" Target="_blank" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamdr")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>
                    </asp:View>
                </asp:MultiView>
                            </div>
                            
                        </div>
                    </div>
                



            





        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
