<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="FeasibilityInterface.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.FeasibilityInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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

           <%-- var gvclient = $('#<%=this.gvPrjInfo.ClientID %>');

            gvclient.gridviewScroll({
                width: 1160,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 10
            });--%>

        }

        <%--   
        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }--%>

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
            margin-right: 5px;
            width: 110px;
        }

        .tbMenuWrp table tr td {
            /*height: 50px;*/
            /*width: 104px;*/
            padding: 0 0;
            float: left;
            list-style: none;
            /*margin: 0 1px;*/
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



            <div class="card  card-fluid ">
                <div class=" card-body">

                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <label for="lblfrmdate" class="control-label  lblmargin-top9px">Date</label>
                            </div>


                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control " AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>


                        </div>

                        <div class="col-md-1">
                            <div class="form-group">

                                <label for="Label1" class="control-label   lblmargin-top9px">To</label>
                            </div>


                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control " AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>


                        </div>

                        <div class="col-md-1">
                            <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" Target="_blank" NavigateUrl="~/F_01_LPA/PriLandProposal?Type=Report&prjcode=" runat="server" CssClass="  dropdown-item"> Land Proposal</asp:HyperLink>
                                        <a href="<%=this.ResolveUrl("~/F_01_LPA/LpSCodeBook?BookName=Project")%>" target="_blank" class="  dropdown-item">Project Code</a>
                                        <a href="<%=this.ResolveUrl("~/F_04_Bgd/PRCodeBook")%>" target="_blank" class="  dropdown-item">Project Information Code</a>
                                        <a href="<%=this.ResolveUrl("~/F_01_LPA/LpSCodeBook?BookName=Resource02")%>" target="_blank" class="  dropdown-item">Revenue Code</a>
                                        <a href="<%=this.ResolveUrl("~/F_01_LPA/LpSCodeBook?BookName=Cost02")%>" target="_blank" class="  dropdown-item">Cost Code</a>
                                        <a href="<%=this.ResolveUrl("~/F_01_LPA/LpSCodeBook?BookName=Other02")%>" target="_blank" class="  dropdown-item">Other Code</a>


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
                                                    <asp:ListItem Value="6"></asp:ListItem>
                                                    <asp:ListItem Value="7"></asp:ListItem>
                                                    <%--<asp:ListItem Value="8"></asp:ListItem>--%>
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

                                <asp:GridView ID="gvFeaPrjLand" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvFeaPrjLand_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvproname" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Offered Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcdate" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate"))   %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Owner's Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlownername" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lwnname"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Media Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmedia" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "media"))   %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdress" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvllocation" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlcategory" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "category"))   %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LAND AREA <br/> KHATA">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltotallanare" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Road Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlroadwidth" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roadwidth"))   %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="pnlcheck" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvCheck" runat="server" OnRowDataBound="gvCheck_RowDataBound" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3c" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcode" runat="server" AutoCompleteType="Disabled" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>


                                                <asp:HyperLink ID="lgvpronamec" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Offered Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcdatec" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cdate"))   %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Owner's Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlownernamec" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lwnname"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Media Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmediac" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "media"))   %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdressc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="LAND AREA <br/> KHATA">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltotallanarec" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Road Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlroadwidthc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roadwidth"))   %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEntry" ToolTip="Check" runat="server" OnClick="lnkbtnEntry_Click" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs "><span class="fa fa-check"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnPreEntry" ToolTip="Cancel" runat="server" OnClick="lnkbtnPreEntry_Click" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-danger"><span class=" fa fa-trash"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
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
                    <asp:Panel runat="server" ID="pnlfeasibility" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvFeasibility" runat="server" OnRowDataBound="gvFeasibility_RowDataBound" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3f" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcodefe" runat="server" AutoCompleteType="Disabled" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>

                                                <asp:HyperLink ID="lgvpronamef" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdressf" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvllocation" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LAND AREA <br/> KHATA">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltotallanaref" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Road Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlroadwidthf" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roadwidth"))   %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GP % on Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgpper" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NP  % on Cost">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvnpper" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                    Target="_blank" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="50px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEntryf" runat="server" OnClick="lnkbtnEntryf_Click" ForeColor="Black" Font-Underline="false" CssClass="btn btn-default btn-xs "><span class="fa fa-check"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
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
                    <asp:Panel runat="server" ID="pnldocument" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvDocument" runat="server" OnRowDataBound="gvDocument_RowDataBound" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3d" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcodedoc" runat="server" AutoCompleteType="Disabled" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>


                                                <asp:HyperLink ID="lgvpronamed" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdressd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvllocationd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LAND AREA <br/> KHATA">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltotallanared" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Road Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlroadwidthd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roadwidth"))   %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GP % on Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgpperd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NP  % on Cost">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvnpperd" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                    Target="_blank" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="50px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEntryd" runat="server" OnClick="lnkbtnEntryd_Click" ForeColor="Black" Font-Underline="false" CssClass="btn btn default btn-xs "><span class="fa fa-check"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
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
                    <asp:Panel runat="server" ID="pnlmoredoc">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvMoreDoc" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvMoreDoc_RowDataBound"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3md" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcodemdoc" runat="server" AutoCompleteType="Disabled" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>


                                                <asp:HyperLink ID="lgvpronamemd" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdressmd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvllocationmd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LAND AREA <br/> KHATA">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltotallanaremd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Road Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlroadwidthmd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roadwidth"))   %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GP % on Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgppermd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NP  % on Cost">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvnppermd" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                    Target="_blank" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="50px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEntrymd" runat="server" OnClick="lnkbtnEntrymd_Click" ForeColor="Black" Font-Underline="false" CssClass="btn btn-xs btn-default"><span class="fa fa-check"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
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
                    <asp:Panel runat="server" ID="pnllegal" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvLegal" runat="server" OnRowDataBound="gvLegal_RowDataBound" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3l" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcodele" runat="server" AutoCompleteType="Disabled" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>

                                                <asp:HyperLink ID="lgvpronamel" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdressl" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvllocationl" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LAND AREA <br/> KHATA">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltotallanarel" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Road Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlroadwidthl" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roadwidth"))   %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GP % on Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgpperl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NP  % on Cost">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvnpperl" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                    Target="_blank" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="50px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbakword" runat="server" ForeColor="Black" ToolTip="Backward" OnClick="lnkbakword_Click" Font-Underline="false" CssClass="btn btn-xs  btn-default"><span class="fa fa-backward"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkforword" runat="server" ForeColor="Black" OnClick="lnkforword_Click" Font-Underline="false" ToolTip="Forward" CssClass="btn btn-xs   btn-default"><span class=" fa fa-forward"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
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

                    <asp:Panel runat="server" ID="pnlforword" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvForward" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3for" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcofor" runat="server" AutoCompleteType="Disabled" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>

                                                <asp:HyperLink ID="lgvpronamefor" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdressfor" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvllocationfor" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LAND AREA <br/> KHATA">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltotallanarefor" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Road Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlroadwidthfor" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roadwidth"))   %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GP % on Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgpperfor" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NP  % on Cost">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvnpperfor" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                    Target="_blank" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="50px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkconfor" runat="server" OnClick="lnkconfor_Click" ForeColor="Black" ToolTip="Confirm Forward" Font-Underline="false" CssClass="btn btn-xs btn-default"><span class=" fa fa-check"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
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
                    <asp:Panel runat="server" ID="pnlapp" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvApprov" runat="server" OnRowDataBound="gvApprov_RowDataBound" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3ap" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpactcoap" runat="server" AutoCompleteType="Disabled" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>

                                                <asp:HyperLink ID="lgvpronameap" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))   %>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Land Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdressap" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "laddress"))   %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvllocationap" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location"))   %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <%--  <asp:TemplateField HeaderText="LAND AREA <br/> KHATA">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvltotallanareap" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totallanare")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Road Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlroadwidthfor" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roadwidth"))   %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="GP % on Cost">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgpperfor" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    Font-Size="11px" Style="text-align: right" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gpper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NP  % on Cost">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvnpperfor" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                                    Target="_blank" Text='<%# "<B>"+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "npper")).ToString("#,##0.00;(#,##0.00); ") +"</B>" %>'
                                                    Width="50px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Initial Details">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvintialdtap" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="15px"
                                                    Width="50px" CssClass="btn btn-xs btn-link" ToolTip="Intial Details"><span class="glyphicon glyphicon-new-window"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feasibility">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvfesibilityap" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="15px"
                                                    Width="50px" CssClass="btn   btn-default" ToolTip="Feasibility Details"><i class="fa fa-windows" aria-hidden="true"></i></asp:HyperLink>



                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Final Document">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lgvfinaldocap" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="15px"
                                                    Width="50px" CssClass="btn   btn-default" ToolTip="Final Document"><i class="fa fa-windows"  aria-hidden="true"></i></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkaprrov" runat="server" OnClick="lnkaprrov_Click" ForeColor="Black" ToolTip="Approved" Font-Underline="false" CssClass="btn btn-xs btn-default"><span class="fa fa-check"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
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
                    <asp:Panel runat="server" ID="pnlplanning" Visible="false">
                    </asp:Panel>


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

