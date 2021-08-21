<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LandInterface_Land.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.LandInterface_Land" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function openModalLandInfo() {            
            $('#ModalprojectSpecificaiton').modal('toggle');
        }

        function openModalLand() {            
            $('#ModalAddLand').modal('toggle');
        }


        function openModalImages() {
            $('#modalDocView').modal('toggle');
        }


        function CloseModal_AlrtMsg() {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('.modal ').modal('hide');

        };

        function CloseModal() {

            $('#ModalprojectSpecificaiton').modal('hide');
        }

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

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
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label for="lblprj" class="control-label  lblmargin-top9px">Project</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList runat="server" CssClass="form-control chzn-select" AutoPostBack="true" ID="ddlProjectName" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>


                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="LnkAddDetails" CssClass="btn btn-primary" Visible="false" runat="server" OnClick="LnkAddDetails_Click" TabIndex="2">
                                       Add Land
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkAddLand" CssClass="btn btn-primary" runat="server" OnClick="lnkAddLand_Click" TabIndex="2">
                                <span class="glyphicon glyphicon-plus"></span> Add New Land
                            </asp:LinkButton>
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
                                                <asp:Label ID="lblpactdesc" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'></asp:Label>
                                                
                                                <asp:Label ID="lbllandcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landcode"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="lgvpronamenn" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Creation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcdate" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="65px"></asp:Label>



                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgbunit" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                             



                                          <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgRate" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgQty" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrack" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "track"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lknEdit" OnClick="lknEdit_Click" CssClass="btn btn-sm btn-primary" runat="server">
                                                    Edit
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDel" OnClick="lnkDel_Click" CssClass="btn btn-sm btn-danger" runat="server">

                                                    <i class="fa fa-trash" aria-hidden="true"></i>

                                                </asp:LinkButton>




                                            </ItemTemplate>
                                              <ItemStyle Width="120px" />
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
                    <asp:Panel runat="server" ID="pnlLandinfo" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvLandInfo" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvLandInfo_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                

                                                <asp:Label ID="lblpactdesc" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'></asp:Label>
                                                <asp:Label ID="lbllandcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landcode"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="lgvpronamenn" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Creation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcdate" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgbunitLandInfo" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgRateLandInfo" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgQtyLandInfo" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>






                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrack" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "track"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkAddInfo" runat="server" CssClass="btn btn-sm btn-primary btn-sm" Target="_blank"> Add Info</asp:HyperLink>
                                                

                                                <asp:LinkButton ID="lnkInofApproved" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name"))==""? false: true %>' OnClick="lnkInofApproved_Click" CssClass="btn btn-sm btn-success btn-sm" runat="server">
                                                  <i class="fa fa-check"></i>
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                              <ItemStyle Width="120px" />
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

                                <asp:GridView ID="gvCheck" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvCheck_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                <asp:Label ID="lblpactdescgvCheck" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'></asp:Label>
                                                <asp:Label ID="lbllandcodegvCheck" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landcode"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="lgvpronamenn" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Creation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcdate" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvname" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="gvAddress" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landaddress"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Land Decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlanddecinmal" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landdecimal"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cell Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="gvCell" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cellphone"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dalil No.">
                                            <ItemTemplate>


                                                <asp:Label ID="gvDalil" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dalno"))%>'
                                                    Width="80px"></asp:Label>


                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtracks" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "track"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                               
                                               
                                                <asp:HyperLink ID="hyyLnikGvCheck" runat="server" CssClass="btn btn-sm btn-primary btn-sm" Target="_blank"><i class="fa fa-eye"></i></asp:HyperLink>

                                              

                                                <asp:LinkButton ID="lnkIsCheked" OnClick="lnkIsCheked_Click" CssClass="btn btn-sm btn-success btn-sm" runat="server">
                                                  <i class="fa fa-check"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkForwardsIsCheked" OnClick="lnkForwardIsCheked_Click" CssClass="btn btn-sm btn-danger btn-sm" runat="server">
                                                  <i class="fa fa-step-backward"></i>
                                                </asp:LinkButton>



                                            </ItemTemplate>
                                              <ItemStyle Width="120px" />
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

                    <asp:Panel runat="server" ID="pnldocument" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                <asp:GridView ID="gvDocument" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvDocument_RowDataBound1"
                                    >
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDocSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                <asp:Label ID="lblpactdescDoc" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'></asp:Label>
                                                <asp:Label ID="lbllandcodeDoc" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landcode"))%>'></asp:Label>
                                                <asp:Label ID="dlgcod1" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dlgcod"))%>'></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="lgvDocpronamenn" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Creation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcdateDoc" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvDocname" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="gvDocAddress" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landaddress"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Land Decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="gvDocDecimal" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landdecimal"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cell Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="gvCellPhone" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cellphone"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dalil No.">
                                            <ItemTemplate>
                                                

                                               <asp:LinkButton id="lnkDalilNo1" Width="80px" runat="server"  OnClick="lnkDalilNo1_Click">

                                                   <asp:Image ID="imgDal" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dalimg"))%>' />
                                                   <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dalno"))%>
                                               </asp:LinkButton>
                                                

                                                
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CS Khatin No.">
                                            <ItemTemplate>
                                               <asp:LinkButton id="lnkcsknoNo1" Width="80px" runat="server" OnClick="lnkcsknoNo1_Click">

                                                   <asp:Image ID="imgcsknoimg" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csknoimg"))%>' />
                                                   <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cskno"))%>
                                               </asp:LinkButton>

                                                 <asp:Label ID="cskgcod" runat="server" AutoCompleteType="Disabled" BackColor="Transparent" Visible="false"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cskgcod"))%>'
                                                    Width="80px"></asp:Label>

                                              
                                               
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="RS Khatin No.">
                                            <ItemTemplate>
                                                 <asp:LinkButton id="lnkrsknoNo1" Width="80px" runat="server"  OnClick="lnkrsknoNo1_Click">
                                                   <asp:Image ID="imgrsknoimg" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsknoimg"))%>' />
                                                     <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rskno"))%>
                                                 </asp:LinkButton>

                                                 <asp:Label ID="rsknogcod" runat="server" AutoCompleteType="Disabled" BackColor="Transparent" Visible="false"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsknogcod"))%>'
                                                    Width="80px"></asp:Label>

                                               
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="BS Khatin No.">
                                            <ItemTemplate>
                                                 <asp:LinkButton id="bsknoq" Width="80px" runat="server"   OnClick="bsknoq_Click">
                                                     <asp:Image ID="imgbsknoqmg" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bsknoimg"))%>' />
                                                     <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bskno"))%>
                                                 </asp:LinkButton>

                                                 <asp:Label ID="bsknogcod" runat="server" AutoCompleteType="Disabled" BackColor="Transparent" Visible="false"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bsknogcod"))%>'
                                                    Width="80px"></asp:Label>


                                           
                                               
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrackss" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "track"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyyLnikGvDocs" runat="server" CssClass="btn btn-sm btn-primary btn-sm" Target="_blank"><i class="fa fa-eye"></i></asp:HyperLink>


                                              

                                                <asp:LinkButton ID="lnkDocCheked" OnClick="lnkDocCheked_Click" CssClass="btn btn-sm btn-success btn-sm" runat="server">
                                                  <i class="fa fa-check"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="lnkForwardGv" OnClick="lnkForwardDoc_Click" CssClass="btn btn-sm btn-danger btn-sm" runat="server">
                                                  <i class="fa fa-step-backward"></i>
                                                </asp:LinkButton>



                                            </ItemTemplate>
                                              <ItemStyle Width="120px" />
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
                    <asp:Panel runat="server" ID="pnlmoredoc">
                        <div class="row">

                            <div class="table-responsive">
                                <asp:GridView ID="gvPending" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvPending_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMoreDocSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                <asp:Label ID="lblpactdescgvPending" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'></asp:Label>
                                              
                                                <asp:Label ID="dlgcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dlgcod"))%>'></asp:Label>
                                                <asp:Label ID="lbllandcodegvPending" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landcode"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="lggvPending" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Creation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPending" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPendingname" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPendingadd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landaddress"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Land Decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPendingDecimal" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landdecimal"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cell Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblphonegvPending" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cellphone"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dalil No.">
                                            <ItemTemplate>
                                               
                                                <asp:Label ID="lbldlgcods" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dlgcod"))%>'></asp:Label>
                                               <asp:LinkButton id="lnkDalilNo" Width="80px" runat="server"  OnClick="lnkDalilNo_Click">
                                                    <asp:Image ID="imgdalimg" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dalimg"))%>' />
                                                     <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dalno"))%>
                                               </asp:LinkButton>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CS Khatin No.">
                                            <ItemTemplate>
                                               <asp:LinkButton id="lnkcsknoNo" Width="80px" runat="server"  OnClick="lnkcsknoNo_Click">
                                                    <asp:Image ID="imgcsknoimg2" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csknoimg"))%>' />
                                                     <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cskno"))%>
                                               </asp:LinkButton>
                                                <asp:Label ID="cskgcod2" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cskgcod"))%>'></asp:Label>

                                             
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="RS Khatin No.">
                                            <ItemTemplate>
                                               <asp:LinkButton id="lnkrskno" Width="80px" runat="server"   OnClick="lnkrskno_Click">
                                                   <asp:Image ID="imgrsknoimg" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsknoimg"))%>' />
                                                     <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rskno"))%>
                                               </asp:LinkButton>
                                                <asp:Label ID="rsknogcod2" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsknogcod"))%>'></asp:Label>

 
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="BS Khatin No.">
                                            <ItemTemplate>
                                               <asp:LinkButton id="lnkBSkno" Width="80px" runat="server"   OnClick="lnkBSkno_Click">
                                                    <asp:Image ID="imgbsknoimglNo" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bsknoimg"))%>' />
                                                     <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bskno"))%>

                                               </asp:LinkButton>
                                                <asp:Label ID="bsknogcod2" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bsknogcod"))%>'></asp:Label>


                                             
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlLandStatus" CssClass="form-control" AutoPostBack="true" runat="server">
                                                    <asp:ListItem>LAND REGISTER</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrackssgvPending" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Font-Bold="true" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "track"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypGvpenView" runat="server" CssClass="btn btn-sm btn-primary btn-sm" Target="_blank"><i class="fa fa-eye"></i></asp:HyperLink>

                                                 <asp:LinkButton ID="lnkSavePend" OnClick="lnkSavePend_Click" CssClass="btn btn-sm btn-primary btn-sm" runat="server">
                                                  <i class="fa fa-upload"></i>
                                                </asp:LinkButton>



                                                <asp:LinkButton ID="lnkgvPendingChekced" OnClick="lnkgvPendingChekced_Click" Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landstatus"))=="0100000"? true: false %>' CssClass="btn btn-sm btn-success btn-sm" runat="server">
                                                  <i class="fa fa-check"></i>
                                                </asp:LinkButton>


                                                <asp:LinkButton ID="lnkForwardgvPending" OnClick="lnkForwardgvPending_Click" CssClass="btn btn-sm btn-danger btn-sm" runat="server">
                                                  <i class="fa fa-step-backward"></i>
                                                </asp:LinkButton>



                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
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
                     

                    
                    <asp:Panel runat="server" ID="pnlapp" Visible="false">
                        <div class="row">

                            <div class="table-responsive">

                                 <asp:GridView ID="gvApprov" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblactcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>'></asp:Label>

                                                <asp:Label ID="lblgvApprovsl" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                <asp:Label ID="lblgvApprovPact" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'></asp:Label>
                                                <asp:Label ID="lblqty" Visible="false" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                <asp:Label ID="lblrate" Visible="false" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                <asp:Label ID="lblunit" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit"))%>'></asp:Label>
                                                <asp:Label ID="lblSircode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode"))%>'></asp:Label>
                                                <asp:Label ID="lblgvApprovlcode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landcode"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="lggvApprovpact" Target="_blank" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc"))%>'
                                                    Width="150px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Creation Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvApprovCdate" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApprovname" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvApprovgadd" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landaddress"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Land Decimal">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprovDecimal" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "landdecimal"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Cell Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApprovCeell" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cellphone"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Dalil No.">
                                            <ItemTemplate>
                                               
                                                <asp:Label ID="dlgcod3" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dlgcod"))%>'></asp:Label>

                                               <asp:LinkButton id="lnkDalilNoapp3" Width="80px" runat="server" OnClick="lnkDalilNoapp3_Click">
                                                    <asp:Image ID="imgDal3" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dalimg"))%>' />
                                                   <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dalno"))%>
                                               </asp:LinkButton>

                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CS Khatin No.">
                                            <ItemTemplate>
                                               <asp:LinkButton id="lnkcsknoNoapp" Width="80px" runat="server"  OnClick="lnkcsknoNoapp_Click">
                                                     <asp:Image ID="imgcsknoimg3" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "csknoimg"))%>' />
                                                   <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cskno"))%>
                                               </asp:LinkButton>
                                                <asp:Label ID="cskgcod3" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cskgcod"))%>'></asp:Label>

                                             
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="RS Khatin No.">
                                            <ItemTemplate>
                                               <asp:LinkButton id="lnkrsknoapp" Width="80px" runat="server"   OnClick="lnkrskno3_Click">

                                                    <asp:Image ID="imgrsknoimg3" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsknoimg"))%>' />
                                                     <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rskno"))%>
                                               </asp:LinkButton>
                                                <asp:Label ID="rsknogcod3" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsknogcod"))%>'></asp:Label>

 
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="BS Khatin No.">
                                            <ItemTemplate>
                                               <asp:LinkButton id="lnkBSknoapp" Width="80px" runat="server"   OnClick="lnkBSkno3_Click">

                                                    <asp:Image ID="imgbsknoimglNo3" Width="20" runat="server" ImageUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bsknoimg"))%>' />
                                                     <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bskno"))%>


                                               </asp:LinkButton>

                                                 
                                                <asp:Label ID="bsknogcod3" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bsknogcod"))%>'></asp:Label>


                                             
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                  <asp:Label ID="gvAtrack" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "track"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                               

                                                 

                                                <asp:LinkButton ID="lnkForwardgvApprov" OnClick="lnkForwardgvApprov_Click" CssClass="btn btn-sm btn-danger btn-sm" runat="server">
                                                  <i class="fa fa-step-backward"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="LnikAddland" OnClick="LnikAddland_Click" CssClass="btn btn-sm btn-primary btn-sm" runat="server">
                                                  <i class="fa fa-link"></i>
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                              <ItemStyle Width="120px" />
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
                 

                </div>
            </div>

             <asp:HiddenField ID="hiddenLandCode" runat="server" />
            <!-- Modal -->
             
            <div id="ModalAddLand" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">

                          
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>

                            
                        </div>
                        <div class="modal-body form-horizontal">
                            <div>
                            <h4 class="modal-title" id="LandmodalTitel" runat="server"></h4>

                                <asp:HiddenField ID="hiddenPrjCode" runat="server" />
                                <asp:HiddenField ID="hidlandcode" runat="server" />



                                <div class="row pt-2 pb-1">
                                    <label class="col-md-4">Description EN</label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtLandName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="row pb-1">
                                    <label class="col-md-4">Unit </label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtunit" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>




                                    
                                </div>
                                <div class="row pb-1">
                                    <label class="col-md-4">Standard Rate </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtstdrate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row pb-1">
                                    <label class="col-md-4">Quantity </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtqnty" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>



                                <div class="row pb-1 d-none">
                                    <label id="lblddlproject" runat="server" class="col-md-4">Department</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>


                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="bnSaveLandCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>


                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>



            <div id="modalDocView" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">

                          
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>

                            
                        </div>
                        <div class="modal-body form-horizontal">
                         
                            <asp:Image ID="imgData" Width="500" runat="server" />

                        </div>
                        <div class="modal-footer ">
                           
                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

