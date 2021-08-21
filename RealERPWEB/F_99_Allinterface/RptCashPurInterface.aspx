<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptCashPurInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.RptCashPurInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../js/bootstrap.js"></script>
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
            width: 120px;
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
                .tbMenuWrp table tr td:nth-child(9) {
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




      
           .modalPopup{
            top:25px !important;
            min-height:400px;
            overflow:scroll;
       
        }
    </style>

    <script>
        //$(document).ready(function () {
        //    $("#slSt").load("~/F_23_SaM/SalesInterface");
        //    var refreshId = setInterval(function () {
        //        $("#slSt").load('~/F_23_SaM/SalesInterface?randval=' + Math.random());
        //    }, 10000);
        //    $.ajaxSetup({ cache: false });
        //});



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

        function FunConfirm() {
            if (confirm('Are you sure you want to delete this  Item?')) {
                return;
            } else {
                return false;
            }
        }

    </script>




    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            function loadModal() {
                $('#exampleModal3').modal('show');
            }

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });



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

          <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="5000">
            </asp:Timer>

            <%-- <triggers>
 
                   <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>
            <%-- </ContentTemplate>

    </asp:UpdatePanel>--%>


            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A ">
                            <div class="form-group">



                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to">Date</asp:Label>
                                    
                                    <asp:TextBox ID="txtfrmdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " AutoPostBack="true"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                   

                                    <%--<asp:Label ID="lbldate" runat="server" CssClass=" smLbl_to"> To</asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " AutoPostBack="true"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txttodate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>--%>
                                    <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton></li>


                                </div>

                                <div class="col-md-4">
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_12_Inv/PurReqEntry?InputType=Entry&prjcode=&genno=" CssClass="btn btn-warning btn-sm" Style="padding: 0 10px">Create Requsition</asp:HyperLink>

                                    <asp:LinkButton ID="lnkInteface" runat="server" CssClass="btn btn-success btn-sm" Style="padding: 0 10px" OnClick="lnkInteface_Click">Interface</asp:LinkButton></li>
                                <asp:LinkButton ID="lnkReports" runat="server" CssClass="btn btn-warning btn-sm" Style="padding: 0 10px" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_14_Pro/PurInformation" CssClass="btn btn-success btn-sm" Style="padding: 0 10px">Dashboard</asp:HyperLink>

                                </div>
                                <div class="col-md-4 pull-right">

                                    <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">Requisition Tracking</asp:Label>
                                    <asp:TextBox runat="server" ID="txtTrack" AutoPostBack="true" OnTextChanged="txtTrack_TextChanged"></asp:TextBox>
                                    <asp:Button ID="lblMIMEInfo" runat="server" CssClass="lblTxt lblName" Height="23px" Style="text-align: center; line-height: 23px; padding: 0; float: right;" Text="Search" />

                                </div>

                            </div>
                            <div class="clearfix"></div>
                        </fieldset>
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
                                                        ShowFooter="True" OnRowDataBound="gvReqInfo_RowDataBound">
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

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <ItemTemplate>

                                                                     <asp:HyperLink ID="hlnkgvgvmrfno" runat="server"  BorderStyle="none"
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
                                                                        Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
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
                                        <asp:Panel ID="PnlReqChq" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvReqChk" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True"  OnRowDataBound="gvReqChk_RowDataBound">
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
                                                                    <asp:Label ID="lblgvreqnorq" runat="server" Font-Bold="True" Style="text-align: right"
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

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1rq" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                          <%--  <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
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
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
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
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                   <%-- <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>--%>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                    </asp:HyperLink>

                                                                     <asp:LinkButton ID="btnDelReq" OnClick="btnDelReq_Click"  OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>

                                                                   <%-- <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
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
                                        <asp:Panel ID="pnlRatePro" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvRatePro" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True"  OnRowDataBound="gvRatePro_RowDataBound">
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
                                                                    <asp:Label ID="lblgvreqnocheck" runat="server" Font-Bold="True" Style="text-align: right"
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

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1check" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                          <%--  <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
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
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
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
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server"  Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                    </asp:HyperLink>


                                                                     <asp:LinkButton ID="btnDelReqCheck" OnClick="btnDelReqCheck_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>

                                                                 <%--   <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>--%>


                                                                    <%--<asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
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
                                        <asp:Panel ID="pnlRateApp" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvRateApp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvRateApp_RowDataBound">
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
                                                                    <asp:Label ID="lblgvreqnorapp" runat="server" Font-Bold="True" Style="text-align: right"
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

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1rapp" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
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
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
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
                                                                    <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="80px" />
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                    </asp:HyperLink>

                                                                     <asp:LinkButton ID="btnDelReqRateApp" OnClick="btnDelReqRateApp_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                                 <%--   <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
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

                                       
                                       
                                        <asp:Panel ID="PanelRecv" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="grvMRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="grvMRec_RowDataBound">
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
                                                            <%--<asp:TemplateField HeaderText="aprovno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaprovno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsuppliermrr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvorderdat" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                           <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
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
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvWoamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFWoamt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Upto Received">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreceivedamtor" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFreceivedamtor" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>


                                                             <asp:TemplateField HeaderText="Balance Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvbalamtor" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFbalamtor" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>



                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                    </asp:HyperLink>

                                                                        <asp:LinkButton ID="btnDelOrder" OnClick="btnDelOrder_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>

                                                                  <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
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

                                        <asp:Panel ID="PanelBill" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="gvPurBill" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvPurBill_RowDataBound">
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
                                                            <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Supplier">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsupplierbill" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="150px" Font-Bold="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Recevied <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvmrrdat" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRR No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                           <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
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
                                                                    <asp:Label ID="lblitemcount" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrramt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFmrramt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                    </asp:HyperLink>
                                                                        <asp:LinkButton ID="btnDelBill" OnClick="btnDelBill_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
<%--                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>
                                                    

                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
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

                                        <asp:Panel ID="PanelComp" Visible="false" runat="server">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12">

                                                    <asp:GridView ID="grvComp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="grvComp_RowDataBound">
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
                                                            <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno" runat="server" Font-Bold="True" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
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
                                                            <asp:TemplateField HeaderText="Bill <br>  Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvmrrdat" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat1")) %>'
                                                                        Width="70px"></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRR No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrrno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvorderno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=" Req. No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mrf No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvmrfno" runat="server" Font-Bold="True" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                       <%--     <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
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
                                                                    <asp:Label ID="lblgvbillamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0);") %>'
                                                                        Width="50px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvFmrramt" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                    </asp:HyperLink>

                                                                    <%--<asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>


                                                                    <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
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

                            </div>

                        </asp:Panel>


                        <asp:Panel ID="pnlPurchase" runat="server" Visible="False">
                            <div class="form-group">

                                <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                    <ul class="nav colMid " id="SERV">
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=DaywPur")%> " target="_blank">01. Day Wise Purchase</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=PurSum")%> " target="_blank">02. Purchase Summary</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptMatPurHistory")%> " target="_blank">03. Purchase History-Materials Wise</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=IndSup")%> " target="_blank">04. Purchase History-Supplier Wise</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptWorkOrderVsSupply?Type=OrdVsSup")%> " target="_blank">05. Work Order-Supplier Wise</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk")%> " target="_blank">06. Purchase Tracking</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptWorkOrderVsSupply?Type=OrderTk")%> " target="_blank">07. Order Tracking</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=PurBilltk")%> " target="_blank">08. Bill Tracking</a>

                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_Pro/RptDateWiseReq?Type=PendingStatus")%> " target="_blank">09. Pending Status</a>
                                        </li>
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_15_Acc/RptAccSpLedger?Type=ASPayment")%> " target="_blank">10. Supplier Overall Position</a>
                                        </li>





                                    </ul>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </asp:Panel>

                    </div>




                <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="lblMIMEInfo"
                    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; margin:10px; Height:500px">

                    <div>

                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title" id="myModalLabel">Purchase History</h4>
                                </div>
                                <div class="modal-body">

                                    <asp:GridView ID="gvPurstk01" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass=" table-responsive  table-hover table-bordered grvContentarea">

                                        <PagerSettings Position="Top" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref. No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="25px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvAppRate01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                        Width="130px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
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
                                <div class="modal-footer">
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-danger btn-sm pull-right" Text="Close" />



                                </div>
                            </div>
                        </div>

                    </div>

                </asp:Panel>
                <!-- ModalPopupExtender -->



            </div>





            </div>


                      <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script> 
<script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>
            
            <script src="../Scripts/jquery.counterup.min.js"></script>
            <script>
                jQuery(document).ready(function ($) {
                    $('.counter').counterUp({
                        delay: 10,
                        time: 1000
                    });
                });
            </script>

        </ContentTemplate>



    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

