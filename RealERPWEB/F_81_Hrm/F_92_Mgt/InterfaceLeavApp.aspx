<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="InterfaceLeavApp.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.InterfaceLeavApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
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

        ul.sidebarMenu {
            margin: 0;
            padding: 0;
            width: 115%;
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
            height: 65px;
            width: 140px;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 1px;
            color: #fff;
            text-align: center;
            border: 2px solid #D1D735;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }

            .tbMenuWrp table tr td:nth-child(1) {
                background: #3BA8E0;
            }

            .tbMenuWrp table tr td:nth-child(2) {
                background: #5EB75B;
            }

            .tbMenuWrp table tr td:nth-child(3) {
                background: #EFAD4D;
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #D95350;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #76C9B5;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #769BF4;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #00CBF3;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
            }

        /*.tbMenuWrp table tr td:nth-child(7) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                width: 115px;
                padding: 0 3px;
            }*/


        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 100%;
            margin: 1px 0;
            padding: 2px;
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
            background-color: #00ff21;
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
            /*background: #12A5A6;
            color: #fff;*/
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }



        .fan {
            border: 2px solid #f3b728;
            border-radius: 50%;
            display: inline-block;
            float: left;
            font-size: 18px;
            margin-top: 4px;
            padding: 2px;
        }

            .fan:nth-child(1) {
                background: #FF9C40 !important;
                color: #fff;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(2) {
                color: #E49015;
                background-color: #5EB75A !important;
            }

            .fan:nth-child(3) {
                color: #fff;
                background: #085407 !important;
            }

            .fan:nth-child(4) {
                color: #fff;
                background: #DA3F40 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(5) {
                color: #fff;
                background: #009BFF !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(6) {
                color: #E4DDDD;
                background: #539250 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #E4DDDD;
                background: #E79956 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #fff;
                background: #459A42 !important;
                border: 2px solid #E4DDDD;
            }




        /*.modalPopup{
            top:185px !important;
        }*/
    </style>

    <%--<script>
        $(document).ready(function () {
            $("#slSt").load("~/F_23_SaM/SalesInterface.aspx");
            var refreshId = setInterval(function () {
                $("#slSt").load('~/F_23_SaM/SalesInterface.aspx?randval=' + Math.random());
            }, 10000);
            $.ajaxSetup({ cache: false });
        });



        //var refreshId = setInterval(function()
        //{
        //    $(‘#responsecontainer’).load(‘response.php’);
        //    $(‘#responsecontainer2’).load(‘response2.php’);
        //}, 60000);

        //$(document).ready(function()
        //{
        //    $(‘#responsecontainer’).fadeOut(“slow”).load(‘response.php’).fadeIn(“slow”);
        //    $(‘#responsecontainer2’).fadeOut(“slow”).load(‘response2.php’).fadeIn(“slow”);
        //});
    </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });



            comcod = <%=this.GetCompCode()%>;
            switch(comcod)
            {
               
                case 3348:   // Credence   
                case 3101:   // Credence   
                case 3315:   // Credence 
                case 3347:   // Credence   
                    
                    break;

                default:                       
                    //    $("table[id*=RadioButtonList1] input:first").next().next().hide();
                    $(".tbMenuWrp table tr td:nth-child(4)").hide();
                  
                    break;

                
                
            }







            <%--var gvProSlInfo = $('#<%=this.gvProSlInfo.ClientID %>');
            gvProSlInfo.ScrollableGv();--%>
            <%-- var gvInprocess = $('#<%=this.gvInprocess.ClientID %>');
            gvInprocess.ScrollableGv();
            var gvApproved = $('#<%=this.gvApproved.ClientID %>');
            gvApproved.ScrollableGv();
            var gvReadyDelivery = $('#<%=this.gvReadyDelivery.ClientID %>');
            gvReadyDelivery.ScrollableGv();
            var gvPayConfr = $('#<%=this.gvPayConfr.ClientID %>');
            gvPayConfr.ScrollableGv();
            var gvDispatch = $('#<%=this.gvDispatch.ClientID %>');
            gvDispatch.ScrollableGv();--%>
            <%--var gvDeliverd = $('#<%=this.gvDeliverd.ClientID %>');
            gvDeliverd.ScrollableGv();--%>
        };

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
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="100000">
            </asp:Timer>

            <triggers>
 
                   <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>
            <%-- </ContentTemplate>

    </asp:UpdatePanel>--%>


            <%--  <div class="container moduleItemWrpper">
                <div class="contentPart">--%>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="lblfrmdate">From</label>


                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txFdate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txFdate"></cc1:CalendarExtender>

                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="lblfrmdateto">To</label>

                            </div>

                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"  Width="160px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                            </div>




                        </div>

                        
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-primary" OnClick="lnkbtnok_Click">Ok</asp:LinkButton></li>
                            </div>
                        </div>




                         <div class="col-md-1">
                            <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User" CssClass="dropdown-item" Style="padding: 0 10px">Leave</asp:HyperLink>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>




                    </div>

                </div>

            </div>



            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                       <%-- <div class="col-md-2 pading5px">

                            <ul class="sidebarMenu">


                                <li>
                                    <asp:HyperLink ID="HyperLink13" runat="server" Target="_blank" Font-Size="14px" ForeColor="green" Style="text-align: center" Font-Bold="true" Font-Underline="false" NavigateUrl="~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User">Leave Application</asp:HyperLink>
                                </li>

                              
                            </ul>

                        </div>--%>

                        <asp:Panel ID="pnlInt" runat="server" Visible="false">
                            <div id="slSt" class=" col-md-12"  style=" float:left;clear:both;">
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
                                                    </asp:RadioButtonList>

                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div>


                                        <asp:Panel ID="pnlallReq" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvLvReq" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvLvReq_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                               <asp:TemplateField HeaderText="leaveId" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempleaveId" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="110px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Current Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvCust" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>

                                                                    <%-- <asp:LinkButton ID="lnkbtnEdit" runat="server"><span class="glyphicon glyphicon-pencil"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>

                                                                    <asp:HyperLink ID="HyperApplyPrint" runat="server" Target="_blank" ForeColor="Black" CssClass="btn btn-xs btn-default" Font-Underline="false"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
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
                                        <asp:Panel ID="PnlProcess" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvInprocess" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvInprocess_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="116px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HylvPrint" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-default"> <span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-default"> <span class="fa fa-edit"></i>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-default"><span  class=" fa fa-check "></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" HorizontalAlign="Center" />
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
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PnlApp" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvApproved" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvApproved_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HylvPrint" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-default"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" CssClass="btn btn-xs btn-default" ><span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" CssClass="btn btn-xs btn-default"><span  class=" fa fa-check "></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" HorizontalAlign="Center" />
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


                                          <asp:Panel ID="pnlFApp" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvfiApproved" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvfiApproved_RowDataBound" >
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNofi" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempidfi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempnamefi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcardfi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanmefi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesigfi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtypefi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydatfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdatfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddatfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldurationfi" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotalfi" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HylvPrintfi" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-default"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnEditINfi" runat="server" Target="_blank" CssClass="btn btn-xs btn-default" ><span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnAppfi" runat="server" Target="_blank" CssClass="btn btn-xs btn-default"><span  class=" fa fa-check "></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="110px" HorizontalAlign="Center" />
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

                                        <asp:Panel ID="PnlConfrm" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvConfirm" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvProSlInfo_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="150px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>

                                                                    <%-- <asp:LinkButton ID="lnkbtnEdit" runat="server"><span class="glyphicon glyphicon-pencil"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>

                                                                    <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank"  CssClass="btn btn-xs btn-default"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
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

                        </asp:Panel>

                    </div>
                </div>
            </div>



            <asp:Panel ID="pnlReport" runat="server" Visible="False">
                <asp:Panel ID="pnlTrade" runat="server" Visible="false">
                    <div class="form-group">

                        <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                            <ul class="nav colMid " id="SERV">
                                <li>

                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/SalesStatusGraph.aspx")%> " target="_blank">01. Sales (Graph)</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalesPer")%> " target="_blank">02. Sales Performance</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=ProIssue")%> " target="_blank">03. Daywise Issue</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalReg")%> " target="_blank">04. Region Wise Lifting</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=ProDelivery")%> " target="_blank">05. Order Status</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=Prolift")%> " target="_blank">06. Lifting Status</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=ProDel")%> " target="_blank">07. Delivery Status</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=TarVSAch")%> " target="_blank">08. Seg. Wise Tar. vs Achi.</a>

                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=TarVSAch2")%> " target="_blank">09. Team Wise Tar. vs Achi.</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalLedger")%> " target="_blank">10. Customer Ledger</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptDelvIMEIHistory.aspx")%> " target="_blank">11. Delivery IMEI Status</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=PayHis")%> " target="_blank">12. Payment History</a>
                                </li>

                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductModelWise.aspx")%> " target="_blank">13. Product Issuance</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport_02.aspx?Type=ProHis")%> " target="_blank">14. Product Wise History</a>
                                </li>
                            </ul>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="plnMgf" runat="server" Visible="false">
                    <div class="form-group">

                        <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                            <ul class="nav colMid " id="SERV">
                                <li>

                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/SalesStatusGraph.aspx")%> " target="_blank">01. Sales (Graph)</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalesPer")%> " target="_blank">02. Sales Performance</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=ProIssue")%> " target="_blank">03. Daywise Issue</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalReg")%> " target="_blank">04. Region Wise Lifting</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=ProDelivery")%> " target="_blank">05. Order Status</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=Prolift")%> " target="_blank">06. Lifting Status</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=ProDel")%> " target="_blank">07. Delivery Status</a>
                                </li>
                                <%-- <li>
                                            <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=TarVSAch")%> " target="_blank">08. Seg. Wise Tar. vs Achi.</a>

                                        </li>--%>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=TarVSAch2")%> " target="_blank">09. Team Wise Tar. vs Achi.</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalLedger")%> " target="_blank">10. Customer Ledger</a>
                                </li>

                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=PayHis")%> " target="_blank">12. Payment History</a>
                                </li>

                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductModelWise.aspx")%> " target="_blank">13. Product Issuance</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport_02.aspx?Type=ProHis")%> " target="_blank">14. Product Wise History</a>
                                </li>

                            </ul>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </asp:Panel>
            </asp:Panel>
            </div>
                </div>




            </div>
            </div>

        </ContentTemplate>


        <%-- <Triggers>

<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>

</Triggers>--%>
    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

