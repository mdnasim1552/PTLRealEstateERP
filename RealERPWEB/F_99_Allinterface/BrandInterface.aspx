<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="BrandInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.BrandInterface" %>

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
            /*border: 3px solid rgba(255, 255, 255, 0.3);*/
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 18px;
            /*height: 36px;*/
            height: 30px;
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




            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">


                        <div class="col-md-2">
                            <label class="control-label">From</label>

                            <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control"></asp:TextBox>
                            <cc1:CalendarExtender ID="Cal2" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">To</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>



                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary  " OnClick="lnkok_Click">Ok</asp:LinkButton>

                            </div>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label">SMS/Mail</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlSmsMail" runat="server" OnSelectedIndexChanged="ddlSmsMail_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                                        <asp:ListItem Value="01">SMS</asp:ListItem>
                                        <asp:ListItem Value="02">Mail</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <label class="control-label">Template</label>
                            <asp:DropDownList ID="ddlSMSMAILTEMP" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSMSMAILTEMP_SelectedIndexChanged" CssClass="form-control">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-3">
                            <label class="control-label text-success">Content:</label>
                            <asp:Label ID="lblTemMSg" runat="server" Text="This is Test for Template "></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkSend" runat="server" CssClass="margin-top30px btn btn-success btn-sm" OnClick="lnkSend_Click" Text="Send"></asp:LinkButton>

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
                                                    <%-- <asp:ListItem Value="4"></asp:ListItem>
                                                    <asp:ListItem Value="5"></asp:ListItem>
                                                    <asp:ListItem Value="6"></asp:ListItem>
                                                    <asp:ListItem Value="7"></asp:ListItem>
                                                    <asp:ListItem Value="8"></asp:ListItem>
                                                    <asp:ListItem Value="9"></asp:ListItem>
                                                    <asp:ListItem Value="10"></asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>

                    <asp:Panel runat="server" ID="pnlBusDev" Visible="false">
                        <div class="row">


                            <div class="col-md-1">


                                <asp:DropDownList ID="ddlyearland" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlyearland_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-3">
                            </div>





                        </div>

                        <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dealcode")) %>'></asp:Label>
                                        <asp:Label ID="lsircode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LID">
                                    <ItemTemplate>
                                        <asp:Label ID="lsircode1" runat="server" Width="60px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Generated">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgenerated" runat="server" Width="60px" Font-Size="10px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Followup">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllfollowup" runat="server" Width="60px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lfollowup")) %>'></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="Owner Details">
                                    <ItemTemplate>
                                        <asp:Label ID="lowner" runat="server" Width="170px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ownname")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Property Details">
                                    <ItemTemplate>
                                        <asp:Label ID="ldesc" runat="server" Width="200px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Associate">
                                    <ItemTemplate>
                                        <asp:Label ID="lassoc" runat="server" Width="90px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dealing">
                                    <ItemTemplate>

                                        <asp:Label ID="lblbusername" runat="server" Width="90px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delname")) %>'></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllstatus" runat="server" Width="60px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Prio">
                                    <ItemTemplate>
                                        <asp:Label ID="lprio" runat="server" Width="90px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prio")) %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Last </br>Follow. </br>Dur.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfollowdday" runat="server" Width="20px" Font-Size="10px" Style="text-align: center;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "followdday")) %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Phone" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPhone" runat="server" Width="90px" Font-Size="10px" Style="text-align: center;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cphone")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMail" runat="server" Width="90px" Font-Size="10px" Style="text-align: center;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cmail")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">

                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllBD" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkAllBD_CheckedChanged" Text="ALL" />
                                    </HeaderTemplate>

                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkstatus" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smstatus"))=="1" %>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlSuspect" Visible="false">
                        <div class="row">




                            <div class="col-md-2">
                                <asp:Label ID="lblLTypeName" CssClass="smLbl_to" runat="server" Text="Lead Type"></asp:Label>
                                <asp:DropDownList ID="ddlLeadType" runat="server" OnSelectedIndexChanged="ddlLeadType_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                                </asp:DropDownList>


                                <asp:TextBox ID="txtmobno" runat="server" CssClass=" inputtextbox" Style="display: none"></asp:TextBox>
                            </div>

                            <%-- <div class="col-md-1">
                                <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-success btn-sm" Style="margin-top: 24px;" OnClick="lbtnSearch_OnClick" Text="OK"></asp:LinkButton>

                            </div>--%>
                            <div class="col-md-3">
                                <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Text="Search By"></asp:Label>

                                <input type="text" id="myInput" onkeyup="Search_Gridview(this);" placeholder="Search.." title="Type" class="form-control">
                            </div>

                        </div>



                        <asp:GridView ID="gvAdDetails" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="S.L">

                                    <ItemTemplate>
                                        <asp:Label ID="serialno" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvusrid" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>' Width="55px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Call Centre">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvbranch" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Call Centre" onkeyup="Search_Gridview(this,2)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbranch" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brname")) %>'
                                            Width="110px" BackColor="Transparent" BorderStyle="None"></asp:Label>


                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Name">
                                    <HeaderTemplate>

                                        <asp:TextBox ID="txtgvName" BackColor="Transparent" BorderStyle="None" runat="server" Width="130px" SortExpression="name" placeholder="Name" onkeyup="Search_Gridview(this,3)"></asp:TextBox>


                                    </HeaderTemplate>



                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclname" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                            Width="130px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">

                                    <HeaderTemplate>

                                        <asp:TextBox ID="txtgmobile" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" SortExpression="mob" placeholder="Mobile" onkeyup="Search_Gridview(this,4)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclmob" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mob")) %>'
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvemail" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" SortExpression="email" placeholder="Email" onkeyup="Search_Gridview(this,5)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclemail"
                                            runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                            Width="110px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="Location">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvlocation" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Location" onkeyup="Search_Gridview(this,6)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lbllocat" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locat")) %>'
                                            Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        <%--<asp:DropDownList ID="ddllocat" runat="server" Width="150" CssClass="form-control inputTxt pull-left">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profession">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvprofession" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Profession" onkeyup="Search_Gridview(this,7)"></asp:TextBox>


                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblpro" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pro")) %>'
                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>


                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lead Source">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvadvertise" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Lead Source" onkeyup="Search_Gridview(this,8)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdno" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "adno")) %>'></asp:Label>
                                        <asp:Label ID="lbladvertise" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addesc")) %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="L.Quality">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvleadq" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="L.Quality" onkeyup="Search_Gridview(this,9)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvleadq" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leaddesc")) %>'
                                            Width="50px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lead Status">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvleadst" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Lead Status" onkeyup="Search_Gridview(this,10)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvleadst" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leadstatus")) %>'
                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Interested Project">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvproject" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Interested Project" onkeyup="Search_Gridview(this,11)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblprodid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proid"))%>'></asp:Label>
                                        <asp:Label ID="lblPactCode" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode"))%>'></asp:Label>
                                        <asp:Label ID="lblgvproject" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="100px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Description">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtgvfeedback" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Description" onkeyup="Search_Gridview(this,12)"></asp:TextBox>


                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinfo"
                                            runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "info")) %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lead Dept.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeadDept" runat="server" Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")).ToString() %>'
                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdType" runat="server" Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proddesc")).ToString() %>'
                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclsize" runat="server" Style="text-align: right; font-size: 11px;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "size")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="60px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send To" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtclsendto" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sendto")) %>'
                                            Width="130px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">

                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllSus" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkAllSus_CheckedChanged" Text="ALL" />
                                    </HeaderTemplate>


                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkSpec" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chekstatus"))=="1" %>'
                                            Width="20px" />
                                    </ItemTemplate>

                                    <FooterTemplate>

                                        <asp:LinkButton ID="lnkbtnAssign" Visible="false" runat="server" OnClientClick="return FunOrdConfirm();"
                                            ToolTip="Assign"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                        <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                    </FooterTemplate>





                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="branchcode" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbranchcode"
                                            runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "branch")) %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlProspect" Visible="false">
                        <asp:GridView ID="gvProspect" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>






                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>

                                        <asp:Label ID="lblusercode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usercode")) %>'></asp:Label>
                                        <asp:Label ID="lsircode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'></asp:Label>

                                        <asp:Label ID="ldesig" runat="server" Width="40px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P-ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lsircode1" runat="server" Width="40px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode1")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Generated">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgenerated" runat="server" Width="60px" Font-Size="10px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "generated")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Client Details">
                                    <ItemTemplate>
                                        <asp:Label ID="ldesc" runat="server" Width="100px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Associate">
                                    <ItemTemplate>
                                        <asp:Label ID="lassoc" runat="server" Width="120px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assoc")) %>'></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Team Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbusername" runat="server" Width="120px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "teamdesc")) %>'></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllstatus" runat="server" Width="50px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstatus")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="llTyp" runat="server" Width="40px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadType")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Mobile"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvphone" runat="server" Width="80px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvemail" runat="server" Width="120px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Occupation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoccupation" runat="server" Width="80px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "profession")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Residence" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcaddress" runat="server" Width="120px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <%-- <asp:TemplateField HeaderText="Interested Project">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpactdesc" runat="server" Width="90px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>


                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvLSrc" runat="server" Width="70px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LeadSrc")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdiss" ClientIDMode="Static" Width="20px" ToolTip="Add Discussion" runat="server">
                                                    <img src="../Image/meeting.svg" width="20" height="20" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllPros" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkAllPros_CheckedChanged" Text="ALL" />
                                    </HeaderTemplate>

                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkstatus" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smstatus"))=="1" %>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Feedback">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvfeedback" runat="server" Width="120px" Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ldiscuss")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlCustomer" Visible="false">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control inputTxt chzn-select smDropDown"
                                     OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" 
                                    Width="70px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="800">400</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>



                        <asp:GridView ID="gvClientLetter" runat="server"
                            AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True"
                            Width="614px">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="">
                                    
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkletterc" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>' />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvletcodec" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                   
                                    <HeaderTemplate>




                                        <asp:Label ID="lblgvhproname" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text="Project Name"> </asp:Label>




                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server"
                                            CssClass="btn   btn-xs" ToolTip="Export Excel"><span class="fa fa-file-excel"></span>
                                        </asp:HyperLink>







                                    </HeaderTemplate>
                                    
                                    
                                     <ItemTemplate>
                                        <asp:Label ID="lgvpactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclinetname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclientAddress" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paddress")) %>'
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclientMob" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvclientemail" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>' Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL" />
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:CheckBox ID="chkstatus" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "smstatus"))=="1" %>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                    </asp:Panel>



                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

