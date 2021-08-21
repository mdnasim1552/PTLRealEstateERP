<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BusinessDashboard.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.BusinessDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function AddButton(id) {
            $(".hiddenb" + id).css("display", "inline");

        }
        function HiddenButton(id) {
            $(".hiddenb" + id).css("display", "none");
        }

        function AddButtonsub(id) {
            $(".hiddensub" + id).css("display", "inline");

        }
        function HiddenButtonsub(id) {
            $(".hiddensub" + id).css("display", "none");
        }
    </script>
    <style type="text/css">
        #AsyncFileUpload1 input {
            position: absolute;
            top: 0;
            right: 0;
            margin: 0;
            border: solid transparent;
            border-width: 0 0 100px 200px;
            opacity: 0.0;
            filter: alpha(opacity=0);
            -o-transform: translate(250px, -50px) scale(1);
            -moz-transform: translate(-300px, 0) scale(4);
            direction: ltr;
            cursor: pointer;
        }

        .modal-dialog {
            margin: 44px auto;
            width: 50%;
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
        }

            ul.sidebarMenu li {
                display: block;
                height: 30px;
                list-style: none;
                border: 1px solid #DFF0D8;
                /*border-bottom: 0;*/
            }

                ul.sidebarMenu li:last-child {
                    /*border-bottom: 1px solid #DFF0D8;*/
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
            width: 115px;
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
                display: none;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #5EB75B;
                display: none;
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
            padding: 1px;
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





        .modalPopup {
            top: 25px !important;
            min-height: 400px;
            overflow: scroll;
        }

        ul.sidebarMenu li {
            display: block;
            height: 36px;
            list-style: none;
            border: 1px solid #9752A2;
            /*border-bottom: 0;*/
        }

            ul.sidebarMenu li a {
                text-align: left;
                display: block;
                line-height: 30px;
                font-size: 14px;
                font-family: Calibri;
                cursor: pointer;
                /*/* width: 130px; */
                /*background: #32CD32;*/
                background: #CCE5FF;
                border-radius: 5;
                color: #000;
                font-weight: bold;
                padding: 0 0 0 4px;
                /* line-height: 30px; */
                margin: 1px 2px;
                display: block;
                text-align: left;
                border-bottom: 1px;
            }

                ul.sidebarMenu li a:hover {
                    background: #006633;
                    color: white !important;
                }

        .pad2px {
            padding-top: 2px;
            padding-bottom: 2px;
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">From</label>
                                <asp:TextBox ID="txtFdate" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">To</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>




                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary  " OnClick="lnkbtnok_Click">Ok</asp:LinkButton>

                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label">Total Land</label>
                                <asp:HyperLink ID="hlnkLandowner" Target="_blank" NavigateUrl="~/F_01_LPA/MktLandOwnerDiscus?Type=Entry&clientid=&nfollow=" runat="server" CssClass="btn btn-warning ">1000</asp:HyperLink>

                            </div>
                        </div>




                        <div class="col-md-2">
                            <div class=" btn-group margin-top30px" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Other Action</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink2"  NavigateUrl="~/F_01_LPA/LandInfoDet?Type=Entry" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-plus"></span> Land/Owner Information</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" Target="_blank" NavigateUrl="~/F_01_LPA/LandOwCodeBook" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-pencil"></span>Land/Owner Code Book</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_01_LPA/MktLandOwnerDiscus?Type=Entry&clientid=&nfollow=" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-pencil"></span> Add New Discussion</asp:HyperLink>



                                        <asp:HyperLink ID="hlnkBasicInfo" Target="_blank" NavigateUrl="~/F_64_Mgt/GenCodeBook?Type=81" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-pencil"></span>Discussion Field</asp:HyperLink>

                                    </div>
                                </div>
                            </div>



                        </div>


                        <div class="col-md-2">
                            <div class=" btn-group margin-top30px" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Reports</button>
                                <div class="btn-group" role="group">
                                    <button id="btnRpt" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnRpt" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink4" Target="_blank" NavigateUrl="~/F_01_LPA/LandDiscuDetails?Type=Report&prjcode=&Date1=" runat="server" CssClass="dropdown-item"> <span class="glyphicon glyphicon-plus"></span> Landowner Discussion</asp:HyperLink>

                                    </div>
                                </div>
                            </div>



                        </div>
                    </div>

                </div>

                <div class="row">

                    <div class="col-md-12 ">
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

                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <asp:Panel ID="pnlMontDisc" runat="server" Visible="false">

                                <asp:GridView ID="gvclient" runat="server"
                                    AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvclient_RowDataBound"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Description">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyplCustomer" runat="server" Font-Size="10px" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                    Width="250px"></asp:HyperLink>


                                                 <asp:Label ID="lgvproscode" runat="server" Visible="false" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'
                                                    Width="70px"></asp:Label>
                                                 <asp:Label ID="lgvteamcode" runat="server" Visible="false" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode")) %>'
                                                    Width="70px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText=" Phone No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPhonech" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>




                                        <asp:TemplateField HeaderText="Meeting Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvMeetingdat" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="60px"></asp:Label>
                                                <asp:Label ID="lblcDate" Font-Size="10px" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvkpigrp" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "kpigrpdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>

                                                <asp:Panel ID="pnldis" runat="server" ClientIDMode="Static">

                                                <asp:Label ID="lgvDiscussion0"   runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="150px">                                             


                                                </asp:Label>
                                                <asp:LinkButton ID="lnkAdddis" ClientIDMode="Static"   Width="10"  ToolTip="Comments" runat="server"  OnClick="lnkAdddis_Click"><span class="fa fa-edit"></span></asp:LinkButton>
                                                </asp:Panel>
                                                 <asp:Label ID="lblgvdisgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "disgnote")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                       <%-- <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvDiscussion0" Font-Size="10px" Target="_blank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                    Width="180px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Participants">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpartcilist" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next </br>Followup">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnfollowup" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next <br> Appointment">
                                            <ItemTemplate>
                                                <asp:Label ID="nappdat0" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Panel ID="pnlsub" runat="server" ClientIDMode="Static">
                                                    <asp:Label ID="lgvndissub" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                        Width="100px"></asp:Label>
                                                    <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                                </asp:Panel>
                                                <asp:Label ID="lblgvsubgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subgnote")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassoc" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusername" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlstatus" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Land Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunitsizech" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsize")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                                        <%-- <asp:TemplateField HeaderText="Land Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacparkingch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Broker Amour">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacotherch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "broamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Total  Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvactoamtch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tlamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Offered Land Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvofratech" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofpamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText=" Offered broker Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvofparkingch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ofothamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                                        <%--<asp:TemplateField HeaderText="Asking Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvoftoamtch" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oftuamt")).ToString("#,##0; (#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="Diff. In %">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdiffinper" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dinper")).ToString("#,##0.00; (#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>


                            </asp:Panel>

                            <asp:Panel ID="pnlAppoinment" runat="server" Visible="false">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvAppo" runat="server"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvAppo_RowDataBound"
                                        CssClass="table-condensed table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0Appo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgbproscod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'
                                                        Width="80px"></asp:Label>

                                                    <asp:HyperLink ID="hyplCustomerAppo" Target="_blank" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                        Width="300px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText=" Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhonechAppo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Meeting Date" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdatA" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Today Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdat0Appo" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy HH:mm") %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Prev. Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDiscussion0Appo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Today </br>Followup">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnfollowup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject">
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlsub" runat="server" ClientIDMode="Static">
                                                        <asp:Label ID="lgvndissub" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                            Width="100px"></asp:Label>
                                                        <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                                    </asp:Panel>
                                                    <asp:Label ID="lblgvsubgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subgnote")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>

                                                    <asp:HyperLink ID="hlink1" runat="server" CssClass="btn btn-xs btn-success" Target="_blank"> <span class="fa fa-check"></span></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassoc" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusername" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>



                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlDissAppo" runat="server" Visible="false">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDisappon" runat="server"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvDisappon_RowDataBound"
                                        CssClass="table-condensed table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoDisAppo" runat="server" Font-Bold="True" Font-Size="10px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Description">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hyplrDisAppo" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                        Width="250px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText=" Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhonechDisAppo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Meeting Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdatDisAppo" runat="server" Font-Size="10px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Followup">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvkpigrp" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "kpigrpdesc")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDisAppo" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Participants">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpartcilist" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next </br>Followup">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnfollowup" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdat0DisAppo" runat="server" Font-Size="10px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy HH:mm") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject">
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlsub" runat="server" ClientIDMode="Static">
                                                        <asp:Label ID="lgvndissub" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                            Width="100px"></asp:Label>
                                                        <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                                    </asp:Panel>
                                                    <asp:Label ID="lblgvsubgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subgnote")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassoc" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusername" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlstatus" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                            </asp:Panel>
                            <asp:Panel ID="pnlDissNew" runat="server" Visible="false">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDissNew" runat="server"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvDissNew_RowDataBound"
                                        CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoDisAppo" runat="server" Font-Bold="True" Font-Size="10px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Description">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hyplrDisAppo" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                        Width="250px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText=" Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhonechDisAppo" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Meeting Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMeetingdatDisAppo" runat="server" Font-Size="10px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Followup">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvkpigrp" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "kpigrpdesc")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvDisAppo" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Participants">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpartcilist" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "partcilist")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next </br>Followup">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnfollowup" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdat0DisAppo" runat="server" Font-Size="10px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy HH:mm") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject">
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlsub" runat="server" ClientIDMode="Static">
                                                        <asp:Label ID="lgvndissub" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                            Width="100px"></asp:Label>
                                                        <%--<asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>--%>
                                                    </asp:Panel>
                                                    <asp:Label ID="lblgvsubgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subgnote")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassoc" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusername" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvlstatus" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
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
                            </asp:Panel>

                            <asp:Panel ID="pnlNextApponi" runat="server" Visible="false">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvNextAppt" runat="server"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true" OnRowDataBound="gvNextAppt_RowDataBound"
                                        CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNoNewApp" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Description">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hyplrNewApp" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                        Width="350px"></asp:HyperLink>

                                                     <asp:Label ID="lgvproscode" runat="server" Visible="false" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscod")) %>'
                                                    Width="70px"></asp:Label>
                                                 <asp:Label ID="lgvteamcode" runat="server" Visible="false" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamcode")) %>'
                                                    Width="70px"></asp:Label>

                                                     <asp:Label ID="lblcDate" Font-Size="10px" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate")) %>'
                                                    Width="60px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText=" Phone No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvPhonechNewApp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Prv. Discussion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvNewApp" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next </br>Followup">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnfollowup" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nfollowup")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Appointment">
                                                <ItemTemplate>
                                                    <asp:Label ID="nappdat0NewApp" Font-Size="10px" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "napnt")).ToString("dd-MMM-yyyy HH:mm") %>'
                                                        Width="110px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Subject">
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlsub" runat="server" ClientIDMode="Static">
                                                        <asp:Label ID="lgvndissub" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ndissub")) %>'
                                                            Width="100px"></asp:Label>
                                                        <asp:LinkButton ID="lnkAdddissub" Width="10" ClientIDMode="Static" ToolTip="Comments" runat="server" OnClick="lnkAdddissub_Click"><span class="fa fa-edit"></span></asp:LinkButton>
                                                    </asp:Panel>
                                                    <asp:Label ID="lblgvsubgnote" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subgnote")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Associate">
                                            <ItemTemplate>
                                                <asp:Label ID="lassoc" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dealing">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbusername" runat="server" Width="100px" Font-Size="10px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>

                            </asp:Panel>
                        </div>
                        <div class="row">
                            <asp:Panel ID="pnlbirthday" runat="server" Visible="false">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvClientBrthDay" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Width="186px" ShowFooter="True">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvslnobdte" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Land Description">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hyplrNewApp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                        Width="250px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Owner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvClientNamebdte" runat="server"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cname")) %>' Width="160px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            
                                            <asp:TemplateField HeaderText="Day">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbday" runat="server" Width="100px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cbday")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpredAddressbdte" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                        Width="160px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Home Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvhomephonebbdte" runat="server" Width="80px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Mobile">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMobNobdte" runat="server" Width="100px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cphone")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <%--<asp:TemplateField HeaderText="Office Phone">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoffPhonebbdte" runat="server" Width="80px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnSendMail" runat="server" Font-Bold="True" CssClass="btn btn-warning"
                                                        Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send Mail</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Email">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvemailbbdte" runat="server" Width="140px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cemail")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnSendSms" runat="server" Font-Bold="True" CssClass="btn btn-primary "
                                                        Font-Size="12px" ForeColor="Black">Send SMS</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>


                            <asp:Panel ID="PnlMarrDay" runat="server" Visible="false">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvCliMarrDay" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Width="186px" ShowFooter="True" OnRowDataBound="gvCliMarrDay_RowDataBound">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvslnobdte" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Description">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hyplrNewApp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                                        Width="250px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Land Owner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvClientNamebdte" runat="server"
                                                        Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cname")) %>' Width="160px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            

                                          
                                            <asp:TemplateField HeaderText="Day">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvbday" runat="server" Width="100px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cmday")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpredAddressbdte" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preaddress")) %>'
                                                        Width="160px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Home Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvhomephonebbdte" runat="server" Width="80px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "homephone")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />

                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Mobile">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvMobNobdte" runat="server" Width="100px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cphone")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <%--<asp:TemplateField HeaderText="Office Phone">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvoffPhonebbdte" runat="server" Width="80px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "offphone")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnSendMail" runat="server" Font-Bold="True" CssClass="btn btn-warning"
                                                        Style="text-decaration: none;" Font-Size="12px" ForeColor="Black">Send Mail</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Email">

                                                <ItemTemplate>
                                                    <asp:Label ID="lgvemailbbdte" runat="server" Width="140px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cemail")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="btnSendSms" runat="server" Font-Bold="True" CssClass="btn btn-primary "
                                                        Font-Size="12px" ForeColor="Black">Send SMS</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:Panel>





                        </div>

                    </div>
                </div>

            </div>
            </div>
            
                        <div class="modal fade right" id="contact" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                            aria-hidden="true" data-backdrop="false">
                            <div class="modal-dialog  modal-lg  modal-side modal-bottom-right modal-notify modal-info" role="document">
                                <!--Content-->
                                <div class="modal-content">
                                    <!--Header-->
                                    <div class="modal-header">
                                        <p class="heading">
                                            <h4 id="lblheader" runat="server"><span class="glyphicon glyphicon-info-sign"></span></h4>
                                        </p>

                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true" class="white-text">&times;</span>
                                        </button>
                                    </div>

                                    <!--Body-->
                                    <div class="modal-body">

                                        <div class="row">

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label  id="lbldsi" runat="server" class="control-label lblmargin-top9px"></label>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lbldiscussion" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px" style="background:#DFF0D8"></asp:Label>


                                                </div>
                                            </div>

                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label for="lblcomm" class="control-label lblmargin-top9px">Comments:</label>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtComm" runat="server" CssClass="form-control " TextMode="MultiLine" Height="100px"></asp:TextBox>


                                                </div>
                                            </div>


                                            <asp:Panel ID="pnld" runat="server" Visible="false">

                                            <asp:Label ID="lblEmpid" runat="server"></asp:Label>
                                            <asp:Label ID="lblclient" runat="server"></asp:Label>
                                            <asp:Label ID="lbldate" runat="server"></asp:Label>
                                           

                                            </asp:Panel>


                                        </div>
                                    </div>

                                    <!--Footer-->
                                    <div class="modal-footer">


                                        <asp:LinkButton ID="lUpdatInfo" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClientClick="CloseModal();" OnClick="lUpdatInfo_Click">Save</asp:LinkButton>

                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true" class="white-text">&times;</span>
                                        </button>

                                    </div>
                                </div>
                                <!--/.Content-->
                            </div>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function openModal() {
            //    $('#myModal').modal('show');
            $('#contact').modal('toggle');
        }

        function CloseModal() {

            $('#contact').modal('hide');
        }

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {

                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            <%--var gvclient = $('#<%=this.gvclient.ClientID %>');

            gvclient.gridviewScroll({
                width: 1000,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 7
            });--%>

        }




    </script>

</asp:Content>


