<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="InterfaceAttApp.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.InterfaceAttApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .nav-tabs {
            border-bottom: 0 !important;
        }

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
            background-color: #0179A8;
        }

        .circle-tile-heading.deep-pink:hover {
            background-color: #B76BA3
        }

        .circle-tile-heading.lime:hover {
            background-color: #00BFFF;
        }

        .circle-tile-heading.chocolate:hover {
            background-color: #32CD32;
        }

        .circle-tile-heading.blue-violet:hover {
            background-color: #FF1493;
        }

        .tile-img {
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
        }


        .green {
            background-color: #16A085;
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



        .yellow {
            background-color: #F1C40F;
        }

        .purple {
            background-color: #8E44AD;
        }

        .deep-sky-blue {
            background-color: #0179A8;
        }

        .deep-pink {
            background-color: #B76BA3;
        }



        .text-lime {
            color: #32CD32;
        }

        .deep-green {
            background: #00A28A;
        }

        .txt-white {
            color: white;
        }
    </style>



    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        }


        var gvAttReq = $('#<%=this.gvAttReq.ClientID %>');
        gvAttReq.Scrollable();

        var gvInprocess = $('#<%=this.gvInprocess.ClientID %>');
        gvInprocess.Scrollable();

        var gvApproved = $('#<%=this.gvApproved.ClientID %>');
        gvApproved.Scrollable();

        var gvConfirm = $('#<%=this.gvConfirm.ClientID %>');
        gvConfirm.Scrollable();


        function Search_Gridview2(strKey) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                /*alert()*/
                var tblData = document.getElementById("<%=this.gvAttReq.ClientID %>");

                var rowData;
                for (var i = 1; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].innerHTML;
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
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="100000">
            </asp:Timer>

            <triggers>

                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />

            </triggers>

            <div class="card card-fluid">
                <div class="card-body mt-2">
                    <div class="row">

                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label8" runat="server">Company</asp:Label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6" id="divBracnhLsit" runat="server">
                            <asp:Label ID="Label9" runat="server">Branch</asp:Label>
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label10" runat="server">Department</asp:Label>
                            <asp:DropDownList ID="ddlProjectName" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass="form-control chzn-select" TabIndex="6">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlProjectName">
                            </cc1:ListSearchExtender>
                            <asp:Label ID="lblComBonLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                            <asp:Label ID="lblComSalLock" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:Label ID="Label11" runat="server">Section</asp:Label>
                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" TabIndex="6" AutoPostBack="true">
                            </asp:DropDownList>

                            <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                QueryPattern="Contains" TargetControlID="ddlSection">
                            </cc1:ListSearchExtender>
                        </div>







                    </div>
                    <div class="row mb-2 mt-1">

                        <div class="col-md-2 col-lg-2 col-sm-6 mt-1">
                            <asp:Label ID="Label1" runat="server">From</asp:Label>
                            <%-- <label class="control-label  lblmargin-top9px" for="lblfrmdate">From</label>--%>
                            <asp:TextBox ID="txFdate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txFdate"></cc1:CalendarExtender>


                        </div>



                        <div class="col-md-2 col-lg-2 col-sm-6 mt-1">
                            <asp:Label ID="Label5" runat="server">To</asp:Label>

                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6 mt-2">
                            <asp:Label ID="Label6" runat="server" CssClass="form-label">Request Type</asp:Label>
                            <%--    <asp:LinkButton ID="imgbtngrade" runat="server" OnClick="imgbtngrade_Click"><i class="fas fa-search"></i></asp:LinkButton>--%>

                            <asp:DropDownList ID="ddrequesttype" runat="server" AutoPostBack="true" CssClass="form-control chzn-select" TabIndex="6">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2 col-lg-2 col-sm-6  mt-2">
                            <asp:Label ID="Label7" runat="server" CssClass="form-label">Card #</asp:Label>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Type ID CARD..."></asp:TextBox>

                        </div>


                        

                        <div class="col-md-2 col-lg-2 col-sm-6 mt-2">
                           
                               
                                    <asp:Label ID="Label3" runat="server" CssClass="form-label">Filter</asp:Label>
                              
                                <asp:DropDownList ID="ddlfilterby" runat="server" CssClass="form-control chzn-select" AutoPostBack="True">
                                    <asp:ListItem Value="%%">All</asp:ListItem>
                         <%--           <asp:ListItem Value="LP">Late Present Approval Request</asp:ListItem>--%>
                                    <asp:ListItem Value="TC">Time Correction Approval Request</asp:ListItem>
                                    <asp:ListItem Value="AB">Absent Approval Request</asp:ListItem>
                        <%--            <asp:ListItem Value="LA">Late Approval Request</asp:ListItem>
                                    <asp:ListItem Value="TLV">Time of Leave</asp:ListItem>--%>
                                </asp:DropDownList>
                            
                        </div>                      
                        <div class="col-md-1 col-lg-1 col-sm-6 mt-4">
                         
                             
                                
                                    <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary" OnClick="lnkbtnok_Click">Ok</asp:LinkButton></li>
                                
                            
                        </div>


                           <%-- <asp:LinkButton ID="lnkbtnok" runat="server" CssClass=" btn btn-primary" OnClick="lnkbtnok_Click">Ok</asp:LinkButton></li>--%>

                        </div>
                        <%--  <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="Label1" runat="server" CssClass="btn btn-secondary btn-sm">Search</asp:Label>
                                </div>
                                <asp:TextBox ID="txtSearch" Style="height: 29px" runat="server" CssClass="form-control" placeholder="Search..." onkeyup="Search_Gridview2(this)"></asp:TextBox>

                            </div>
                        </div>--%>
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
                            <div id="slSt" class=" col-md-12" style="float: left; clear: both;">
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

                                                    <asp:GridView ID="gvAttReq" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvAttReq_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="leaveId" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempleaveId" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="200px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="150px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="200px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAttReq" runat="server" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype"))=="TLV"?"bg-green d-block fsize": ""%>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attstatus")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequest" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="In Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>

                                                                    <asp:Label ID="lbloutstats" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavo")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblinstat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavin")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reason">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempreson" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empreson")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Current Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvCust" runat="server" BackColor="Transparent" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="Label3" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvstatus1")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkbtnEditUser" Visible="false" CssClass="btn btn-xs btn-default" Target="_blank" runat="server">
                                                                        <span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="HyperApplyPrint" runat="server" Target="_blank" Visible="false"
                                                                        ForeColor="Black" CssClass="btn btn-xs btn-default" Font-Underline="false">
                                                                        <span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="left" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" />
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
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                    <asp:Label ID="lblLeavId" runat="server" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="116px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Request Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAttReq" runat="server" Visible="false"
                                                                        CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype"))=="TLV"?"bg-green d-block fsize": ""%>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype"))=="TLV"?"bg-green d-block fsize": ""%>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attstatus")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequest" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="In Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblInoutstats" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavo")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblinstatIn" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavin")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: Center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reason">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempreson" runat="server" Width="150px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empreson")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Current Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvCust" runat="server" BackColor="Transparent" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="Label2" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvstatus1")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "susrid")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HylvPrint" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" Visible="false" CssClass="btn btn-xs btn-default"> <span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Visible="false" Font-Underline="false" CssClass="btn btn-xs btn-default"> <span class="fa fa-edit"></i>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-success"><span  class=" fa fa-check "></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnDptApp" runat="server" Visible="false" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-success"><span  class=" fa fa-check "></span>
                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="lnkRemove" runat="server" Visible="false" ForeColor="red" OnClientClick="return confirm('Are you sure to delete this item?');" OnClick="lnkRemove_Click" Font-Underline="false" CssClass="btn btn-xs btn-default"><span  class="fa fa-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="150px" HorizontalAlign="left" />
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
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                    <asp:Label ID="lblLeavId" runat="server" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAttReq" runat="server" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype"))=="TLV"?"bg-green d-block fsize": ""%>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attstatus")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Request Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequest" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="In Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblApoutstats" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavo")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblApinstat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavin")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: Center"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reason">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempresons" runat="server" Width="150px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empreson")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HylvPrint" runat="server" Target="_blank" ForeColor="Black" Visible="false" Font-Underline="false" CssClass="btn btn-xs btn-default"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnEditIN" Visible="false" runat="server" Target="_blank" CssClass="btn btn-xs btn-default"><span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" CssClass="btn btn-xs btn-default"><span  class=" fa fa-check "></span>
                                                                    </asp:HyperLink>
                                                                    <asp:LinkButton ID="lnkRemoveApp" Visible="false" runat="server" ForeColor="red" OnClientClick="return confirm('Are you sure to delete this item?');" OnClick="lnkRemoveApp_Click" Font-Underline="false" CssClass="btn btn-xs btn-default"><span  class="fa fa-trash"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="150px" HorizontalAlign="left" />
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
                                                        ShowFooter="True" OnRowDataBound="gvfiApproved_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNofi" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempidfi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                    <asp:Label ID="lblLeavIdfi" runat="server" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempnamefi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcardfi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanmefi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesigfi" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydatfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAttReqfi" runat="server" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lglvtypefi" runat="server"
                                                                        CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype"))=="TLV"?"bg-green d-block fsize": ""%>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attstatus")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Request Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequestfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="In Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdatfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblInoutstatsfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavo")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddatfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblinstatfi" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavin")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldurationfi" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotalfi" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Reason">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempresons" runat="server" Width="150px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empreson")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="HylvPrintfi" runat="server" Target="_blank" ForeColor="Black" Visible="false" Font-Underline="false" CssClass="btn btn-xs btn-default"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnEditINfi" runat="server" Target="_blank" Visible="false" CssClass="btn btn-xs btn-default"><span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnAppfi" runat="server" Target="_blank" Visible="false" CssClass="btn btn-xs btn btn-success"><span  class=" fa fa-check "></span>
                                                                    </asp:HyperLink>
                                                                    <asp:LinkButton ID="lnkRemoveFAp" runat="server" ForeColor="red" OnClientClick="return confirm('Are you sure to forward this item?');" OnClick="lnkRemoveFAp_Click" Font-Underline="false" CssClass="btn btn-xs btn-default"><span  class="fa fa-undo"></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="150px" HorizontalAlign="Center" />
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
                                                        ShowFooter="True" OnRowDataBound="gvConfirm_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldptusid" runat="server" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptusid")) %>'
                                                                        Width="49px"></asp:Label>

                                                                    <asp:Label ID="lblLeavId" runat="server" Visible="false"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                    <asp:Label ID="lblgvempid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="150px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype"))=="TLV"?"bg-green d-block fsize": ""%>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attstatus")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvRequest" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="In Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblInoutstatsff" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavo")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Out Time">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToLongTimeString() %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblinstatff" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toleavin")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "duration")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reason">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempresons" runat="server" Width="150px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empreson")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>

                                                                    <%-- <asp:LinkButton ID="lnkbtnEdit" runat="server"><span class="glyphicon glyphicon-pencil"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>

                                                                    <asp:HyperLink ID="HyOrderPrint" runat="server" Visible="false" Target="_blank" CssClass="btn btn-xs btn-default"><span class=" fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="lnkRemoveForward" Visible="false" runat="server" ForeColor="red" OnClientClick="return confirm('Are you sure to Forward this Request?');" OnClick="lnkRemoveForward_Click" Font-Underline="false" CssClass="btn btn-xs btn-default"><span  class="fa fa-undo"></span>
                                                                    </asp:LinkButton>

                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="left" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" />
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


        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>
</asp:Content>
